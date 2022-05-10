using DynamicForecast.Clases;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using DynamicForecast.Areas.Viaje.Models;
using DynamicForecast.Servicios;
using Microsoft.AspNetCore.Hosting;
using Humanizer.Bytes;
using System.IO;
using System.Threading.Tasks;
using DynamicForecast.Models;

namespace DynamicForecast.Controllers
{

    [Autenticado]

    public class UsuarioController : Controller
    {
        private readonly DynamicForecastContext FsvrConn;

        public UsuarioController(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;

        }
        public IActionResult Index()
        {
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            IUsuario Usuario = new IUsuario(FsvrConn);
            var lstUsuario = Usuario.GetUsuarios(fEmpresaId).DefaultIfEmpty().ToList();
            ViewBag.UsuarioCreado = false;
            ViewBag.Error = "";
            ViewBag.UsuarioEliminado = false;

            return View(lstUsuario.ToList());
        }

        public IActionResult CrearUsuario()
        {
            ViewBag.Error = "";

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearUsuarioAsync(DT_Usuario c)
        {
            ViewBag.Error = "";
            ViewBag.UsuarioEliminado = false;
            IUsuario Usuario = new IUsuario(FsvrConn);

            int UsuarioId = 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();

            if (c.PalabraClave == c.PalabraClaveActual)
            {

                try
                {
                    var lstUsuario = Usuario.GetUsuarios(fEmpresaId).DefaultIfEmpty().FirstOrDefault();


                    var UsuarioIdAnterior = lstUsuario.UsuarioId;

                    if (lstUsuario.UsuarioId > 0)
                        UsuarioId = UsuarioIdAnterior + 1;
                    else
                        UsuarioId = 1;



                    c.UsuarioId = UsuarioId;
                    c.EmpresaId = fEmpresaId;
                    c.Estado = "AC";
                    c.CausaEstado = "CRE";
                    c.PalabraClaveNueva = null;
                    c.PalabraClaveNueva = null;
                    c.TipoUsuario = 1;

                    c.FechaIng = DateTime.Now;
                    c.FechaMod = DateTime.Now;
                    Usuario.AgregarUsuario(c);

                    await FsvrConn.SaveChangesAsync();


                    dbTran.Commit();



                    var lstUsuarioes = Usuario.GetUsuarios(fEmpresaId).DefaultIfEmpty();

                    ViewBag.UsuarioCreado = true;
                    return View("~/Views/Usuario/Index.cshtml", lstUsuarioes.ToList());
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    ViewBag.Error = "Error al crear" + ex.InnerException + "<hr />MENSAJE--> " + ex.Message;
                    ViewBag.ListUsuarios = Usuario.GetUsuarios(fEmpresaId).DefaultIfEmpty().OrderByDescending(c => c.UsuarioId);
                    ViewBag.UsuarioCreado = false;
                    var lstUsuarioes = Usuario.GetUsuarios(fEmpresaId).DefaultIfEmpty();

                    return View("~/Views/Usuario/Index.cshtml", lstUsuarioes.ToList());
                }

            }
            else
            {
                dbTran.Rollback();
                ViewBag.Error = "LAS CONTRASEÑAS NO COINCIDEN";
                ViewBag.ListUsuarios = Usuario.GetUsuarios(fEmpresaId).DefaultIfEmpty().OrderByDescending(c => c.UsuarioId);
                ViewBag.UsuarioCreado = false;
                var lstUsuarioes = Usuario.GetUsuarios(fEmpresaId).DefaultIfEmpty();

                return View("~/Views/Usuario/Index.cshtml", lstUsuarioes.ToList());
            }


        }

        public IActionResult EliminarUsuario(int? UsuarioId)
        {
            ViewBag.Error = "";
            IUsuario Usuario = new IUsuario(FsvrConn);
            ViewBag.UsuarioCreado = false;


            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            if (UsuarioId != null && UsuarioId > 0)
            {
                var UsuarioEliminar = Usuario.GetUsuario(fEmpresaId, (int)UsuarioId).DefaultIfEmpty().FirstOrDefault();
                if (UsuarioEliminar != null)
                {
                    Usuario.EliminarUsuario(UsuarioEliminar);
                    ViewBag.UsuarioEliminado = true;
                }
                else
                {
                    ViewBag.Error = "No se puede eliminar el Usuario, ya se ha eliminado.";
                    ViewBag.UsuarioEliminado = false;
                }

            }
            else
            {
                ViewBag.Error = "No se puede eliminar el evento";
                ViewBag.UsuarioEliminado = false;


            }
            var lstUsuarioes = Usuario.GetUsuarios(fEmpresaId).DefaultIfEmpty();

            return View("~/Views/Usuario/Index.cshtml", lstUsuarioes.ToList());
        }


        public ActionResult GetUsuariosFind(string q)
        {
            var fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            string estadoPermitido = "AC";

            IUsuario Usuario = new IUsuario(FsvrConn);
            var results = Usuario.GetUsuarioLike(fEmpresaId, q).Where(h => h.Estado.Equals(estadoPermitido)).
                          Select(h => new { id = h.UsuarioId, text = h.UsuarioId + " - " + h.NombreUsr }).ToList().Take(15);
            return Json(new { results });
        }



    }
}