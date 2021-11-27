using DynamicForecast.Areas.Conductor.Models;
using DynamicForecast.Clases;
using DynamicForecast.Models;
using DynamicForecast.Servicios;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer.Bytes;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace DynamicForecast.Areas.Conductor.Controllers
{
    [Area("Conductor")]
    [Route("Conductor/[controller]/[action]")]
    [Autenticado]
    [BreadCrumb(Title = "Conductores", Url = "/Conductor/Index", Order = 0, IgnoreAjaxRequests = true)]

    public class ConductorController : Controller
    {
        private readonly DynamicForecastContext FsvrConn;
        //private readonly IWebHostEnvironment _oIHostingEnvironment;

        //public ConductorController(DynamicForecastContext svrConn, IWebHostEnvironment oIHostingEnvironment)
        //{
        //    FsvrConn = svrConn;
        //    _oIHostingEnvironment = oIHostingEnvironment;

        //}
        public ConductorController(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IActionResult Index()
        {
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            IConductor Conductor = new IConductor(FsvrConn);
            var lstConductor = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty();
            ViewBag.ConductorCreado = false;
            ViewBag.Error = "";
            ViewBag.ConductorEliminado = false;

            return View(lstConductor.ToList());
        }

        [BreadCrumb(Title = "Crear Conductor", Url = "/Conductor/CrearConductor", Order = 1, IgnoreAjaxRequests = true)]
        public IActionResult CrearConductor()
        {
            ViewBag.Error = "";
            ViewBag.ConductorCreado = false;
            ViewBag.ConductorEliminado = false;

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearConductor([Bind] DT_Conductor c)
        {
            ViewBag.Error = "";
            ViewBag.ConductorEliminado = false;

            IConductor Conductor = new IConductor(FsvrConn);

            int conductorId = 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "ModelState no valido";
            }
            if (ViewBag.Error == "")  // Sí no hay errores
            {
                using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();

                try
                {
                    var lstConductor = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty().FirstOrDefault();

                    if (lstConductor != null)
                    {
                        if (lstConductor.ConductorId > 0)
                            conductorId = lstConductor.ConductorId++;
                        else
                            conductorId = 1;
                    }
                    else
                        conductorId = 1;

                    c.ConductorId = conductorId;
                    c.EmpresaId = fEmpresaId;
                    c.Estado = "AC";
                    c.FechaIng = DateTime.Now;
                    c.FechaMod = DateTime.Now;
                    Conductor.AgregarConductor(c);
                    dbTran.Commit();
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    ViewBag.Error = "Error al crear" + ex.InnerException + "<hr />MENSAJE--> " + ex.Message;
                    ViewBag.ListUsuarios = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty().OrderByDescending(c => c.ConductorId);
                    ViewBag.ConductorCreado = false;
                    var lstConductores = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty();

                    return View("~/Areas/Conductor/Views/Conductor/Index.cshtml", lstConductores.ToList());
                }
            }
            if (ViewBag.Error == "")
            {
                var lstConductores = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty();

                ViewBag.ConductorCreado = true;
                return View("~/Areas/Conductor/Views/Conductor/Index.cshtml", lstConductores.ToList());
            }
            else
            {
                var lstConductores = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty();

                ViewBag.ConductorCreado = false;
                return View("~/Areas/Conductor/Views/Conductor/Index.cshtml", lstConductores.ToList());
            }
        }

        public IActionResult EliminarConductor(int? ConductorId)
        {
            ViewBag.Error = "";
            IConductor Conductor = new IConductor(FsvrConn);
            ViewBag.ConductorCreado = false;


            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            if (ConductorId != null && ConductorId > 0)
            {
                var ConductorEliminar = Conductor.GetConductor(fEmpresaId, (int)ConductorId).DefaultIfEmpty().FirstOrDefault();
                if (ConductorEliminar != null)
                {
                    Conductor.EliminarConductor(ConductorEliminar);
                    ViewBag.ConductorEliminado = true;
                }
                else
                {
                    ViewBag.Error = "No se puede eliminar el conductor, ya se ha eliminado.";
                    ViewBag.ConductorEliminado = false;
                }

            }
            else
            {
                ViewBag.Error = "No se puede eliminar el evento";
                ViewBag.ConductorEliminado = false;


            }
            var lstConductores = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty();

            return View("~/Areas/Conductor/Views/Conductor/Index.cshtml", lstConductores.ToList());
        }

    }
}