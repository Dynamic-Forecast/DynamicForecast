using DynamicForecast.Clases;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using DynamicForecast.Areas.Viaje.Models;
using DynamicForecast.Servicios;

namespace DynamicForecast.Areas.Viaje.Controllers
{
    [Area("Viaje")]
    [Route("Viaje/[controller]/[action]")]
    [Autenticado]
    [BreadCrumb(Title = "Viajes", Url = "/Viaje/Viaje/Index", Order = 0, IgnoreAjaxRequests = true)]

    public class ViajeController : Controller
    {
        private readonly DynamicForecastContext FsvrConn;

        public ViajeController(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IActionResult Index()
        {
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            IViaje Viaje = new IViaje(FsvrConn);
            var lstViaje = Viaje.GetViajes(fEmpresaId).DefaultIfEmpty();
            ViewBag.ViajeCreado = false;
            ViewBag.Error = "";
            ViewBag.ViajeEliminado = false;

            return View(lstViaje.ToList());
        }

        public IActionResult CrearViaje()
        {
            ViewBag.Error = "";

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearViaje([Bind] AP_Viaje c)
        {
            ViewBag.Error = "";
            ViewBag.ViajeEliminado = false;
            IViaje Viaje = new IViaje(FsvrConn);

            int ViajeId = 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();

            try
            {
                var lstViaje = Viaje.GetViajes(fEmpresaId).DefaultIfEmpty().FirstOrDefault();

                if (lstViaje != null)
                {
                    var ViajeIdAnterior = lstViaje.ViajeId;

                    if (lstViaje.ViajeId > 0)
                        ViajeId = ViajeIdAnterior + 1;
                    else
                        ViajeId = 1;
                }
                else
                    ViajeId = 1;

                c.ViajeId = ViajeId;
                c.EmpresaId = fEmpresaId;
                c.Estado = "AC";
                c.FechaIng = DateTime.Now;
                c.FechaMod = DateTime.Now;
                Viaje.AgregarViaje(c);
                dbTran.Commit();

                var lstViajees = Viaje.GetViajes(fEmpresaId).DefaultIfEmpty();

                ViewBag.ViajeCreado = true;
                return View("~/Areas/Viaje/Views/Viaje/Index.cshtml", lstViajees.ToList());
            }
            catch (Exception ex)
            {
                dbTran.Rollback();
                ViewBag.Error = "Error al crear" + ex.InnerException + "<hr />MENSAJE--> " + ex.Message;
                ViewBag.ListUsuarios = Viaje.GetViajes(fEmpresaId).DefaultIfEmpty().OrderByDescending(c => c.ViajeId);
                ViewBag.ViajeCreado = false;
                var lstViajees = Viaje.GetViajes(fEmpresaId).DefaultIfEmpty();

                return View("~/Areas/Viaje/Views/Viaje/Index.cshtml", lstViajees.ToList());
            }


        }

        public IActionResult EliminarViaje(int? ViajeId)
        {
            ViewBag.Error = "";
            IViaje Viaje = new IViaje(FsvrConn);
            ViewBag.ViajeCreado = false;


            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            if (ViajeId != null && ViajeId > 0)
            {
                var ViajeEliminar = Viaje.GetViaje(fEmpresaId, (int)ViajeId).DefaultIfEmpty().FirstOrDefault();
                if (ViajeEliminar != null)
                {
                    Viaje.EliminarViaje(ViajeEliminar);
                    ViewBag.ViajeEliminado = true;
                }
                else
                {
                    ViewBag.Error = "No se puede eliminar el Viaje, ya se ha eliminado.";
                    ViewBag.ViajeEliminado = false;
                }

            }
            else
            {
                ViewBag.Error = "No se puede eliminar el evento";
                ViewBag.ViajeEliminado = false;


            }
            var lstViajees = Viaje.GetViajes(fEmpresaId).DefaultIfEmpty();

            return View("~/Areas/Viaje/Views/Viaje/Index.cshtml", lstViajees.ToList());
        }


        public ActionResult GetViajesFind(string q)
        {
            var fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            string estadoPermitido = "AC";

            IViaje Viaje = new IViaje(FsvrConn);
            var results = Viaje.GetViajeLike(fEmpresaId, q).Where(h => h.Estado.Equals(estadoPermitido)).
                          Select(h => new { id = h.ViajeId, text = h.ViajeId + " - Cod Viaje: " + h.CodViaje + " - " + h.Descripcion }).ToList().Take(15);
            return Json(new { results });
        }



    }
}