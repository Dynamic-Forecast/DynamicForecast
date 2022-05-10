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

    public class TerceroController : Controller
    {
        private readonly DynamicForecastContext FsvrConn;

        public TerceroController(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;

        }
        public IActionResult Index()
        {
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            ITercero Tercero = new ITercero(FsvrConn);
            var lstTercero = Tercero.GetTerceroes(fEmpresaId).DefaultIfEmpty();
            ViewBag.TerceroCreado = false;
            ViewBag.Error = "";
            ViewBag.TerceroEliminado = false;

            return View(lstTercero.ToList());
        }

        public IActionResult CrearTercero()
        {
            ViewBag.Error = "";

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearTerceroAsync(CT_Tercero c)
        {
            ViewBag.Error = "";
            ViewBag.TerceroEliminado = false;
            ITercero Tercero = new ITercero(FsvrConn);

            int TerceroId = 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();

            try
            {
                var lstTercero = Tercero.GetTerceroes(fEmpresaId).DefaultIfEmpty().FirstOrDefault();


                var TerceroIdAnterior = lstTercero.TerceroId;

                if (lstTercero.TerceroId > 0)
                    TerceroId = TerceroIdAnterior + 1;
                else
                    TerceroId = 1;



                c.TerceroId = TerceroId;
                c.EmpresaId = fEmpresaId;
                c.Estado = "AC";
                c.CausaEstado = "CRE";
                c.FechaIng = DateTime.Now;
                c.FechaMod = DateTime.Now;
                Tercero.AgregarTercero(c);

                await FsvrConn.SaveChangesAsync();


                dbTran.Commit();




                var lstTerceroes = Tercero.GetTerceroes(fEmpresaId).DefaultIfEmpty();

                ViewBag.TerceroCreado = true;
                return View("~/Views/Tercero/Index.cshtml", lstTerceroes.ToList());
            }
            catch (Exception ex)
            {
                dbTran.Rollback();
                ViewBag.Error = "Error al crear" + ex.InnerException + "<hr />MENSAJE--> " + ex.Message;
                ViewBag.ListUsuarios = Tercero.GetTerceroes(fEmpresaId).DefaultIfEmpty().OrderByDescending(c => c.TerceroId);
                ViewBag.TerceroCreado = false;
                var lstTerceroes = Tercero.GetTerceroes(fEmpresaId).DefaultIfEmpty();

                return View("~/Views/Tercero/Index.cshtml", lstTerceroes.ToList());
            }


        }

        public IActionResult EliminarTercero(int? TerceroId)
        {
            ViewBag.Error = "";
            ITercero Tercero = new ITercero(FsvrConn);
            ViewBag.TerceroCreado = false;


            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            if (TerceroId != null && TerceroId > 0)
            {
                var TerceroEliminar = Tercero.GetTercero(fEmpresaId, (int)TerceroId).DefaultIfEmpty().FirstOrDefault();
                if (TerceroEliminar != null)
                {
                    Tercero.EliminarTercero(TerceroEliminar);
                    ViewBag.TerceroEliminado = true;
                }
                else
                {
                    ViewBag.Error = "No se puede eliminar el Tercero, ya se ha eliminado.";
                    ViewBag.TerceroEliminado = false;
                }

            }
            else
            {
                ViewBag.Error = "No se puede eliminar el evento";
                ViewBag.TerceroEliminado = false;


            }
            var lstTerceroes = Tercero.GetTerceroes(fEmpresaId).DefaultIfEmpty();

            return View("~/Views/Tercero/Index.cshtml", lstTerceroes.ToList());
        }


        public ActionResult GetTercerosFind(string q)
        {
            var fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            string estadoPermitido = "AC";

            ITercero Tercero = new ITercero(FsvrConn);
            var results = Tercero.GetTerceroLike(fEmpresaId, q).Where(h => h.Estado.Equals(estadoPermitido)).
                          Select(h => new { id = h.TerceroId, text = h.TerceroId + " - " + h.NombreTer }).ToList().Take(15);
            return Json(new { results });
        }



    }
}