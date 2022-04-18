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
    [BreadCrumb(Title = "Conductores", Url = "/Conductor/Conductor/Index", Order = 0, IgnoreAjaxRequests = true)]

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
            ViewBag.CertificadoConductorCreado = false;
            ViewBag.VehiculoConductorCreado = false;
            ViewBag.Error = "";
            ViewBag.ConductorEliminado = false;

            return View(lstConductor.ToList());
        }

        //[BreadCrumb(Title = "Crear Conductor", Url = "/Conductor/CrearConductor", Order = 1, IgnoreAjaxRequests = true)]
        public IActionResult CrearConductor()
        {
            ViewBag.Error = "";
            //ViewBag.ConductorCreado = false;
            //ViewBag.ConductorEliminado = false;

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearConductor([Bind] DT_Conductor c)
        {
            ViewBag.Error = "";
            ViewBag.ConductorEliminado = false;
            ViewBag.CertificadoConductorCreado = false;
            ViewBag.VehiculoConductorCreado = false;

            IConductor Conductor = new IConductor(FsvrConn);

            int conductorId = 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();

            try
            {
                var lstConductor = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty().FirstOrDefault();

                if (lstConductor != null)
                {
                    var ConductorIdAnterior = lstConductor.ConductorId;
                    if (lstConductor.ConductorId > 0)
                        conductorId = (ConductorIdAnterior + 1);
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
                var lstConductores = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty();

                ViewBag.ConductorCreado = true;
                return View("~/Areas/Conductor/Views/Conductor/Index.cshtml", lstConductores.ToList());
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

        public IActionResult EliminarVehiculoConductor(int VehiculoConductorId)
        {
            ViewBag.Error = "";
            IConductor Conductor = new IConductor(FsvrConn);
            IVehiculoConductor VehiculoConductor = new IVehiculoConductor(FsvrConn);
            ViewBag.ConductorCreado = false;


            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            if (VehiculoConductorId != null && VehiculoConductorId > 0)
            {
                var VehiculoConductorEliminar = VehiculoConductor.GetVehiculoConductorXId(fEmpresaId, (int)VehiculoConductorId).DefaultIfEmpty().FirstOrDefault();
                if (VehiculoConductorEliminar != null)
                {
                    VehiculoConductor.EliminarVehiculoConductor(VehiculoConductorEliminar);
                    ViewBag.ConductorEliminado = false;
                }
                else
                {
                    ViewBag.Error = "No se puede eliminar el vehículo al conductor, ya se ha eliminado o no existe.";
                    ViewBag.ConductorEliminado = false;
                }

            }
            else
            {
                ViewBag.Error = "No se puede eliminar el vehículo al conductor";
                ViewBag.ConductorEliminado = false;


            }
            var lstConductores = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty();

            return View("~/Areas/Conductor/Views/Conductor/Index.cshtml", lstConductores.ToList());
        }
        public IActionResult EliminarCertificadoConductor(int CertificadoConductorId)
        {
            ViewBag.Error = "";
            IConductor Conductor = new IConductor(FsvrConn);
            ICertificadoConductor CertificadoConductor = new ICertificadoConductor(FsvrConn);
            ViewBag.ConductorCreado = false;


            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            if (CertificadoConductorId != null && CertificadoConductorId > 0)
            {
                var CertificadoConductorEliminar = CertificadoConductor.GetCertificadosXCertificadosConductor(fEmpresaId, (int)CertificadoConductorId).DefaultIfEmpty().FirstOrDefault();
                if (CertificadoConductorEliminar != null)
                {
                    CertificadoConductor.EliminarCertificadoConductor(CertificadoConductorEliminar);
                    ViewBag.ConductorEliminado = false;
                }
                else
                {
                    ViewBag.Error = "No se puede eliminar el certificado al conductor, ya se ha eliminado o no existe.";
                    ViewBag.ConductorEliminado = false;
                }

            }
            else
            {
                ViewBag.Error = "No se puede eliminar el certificado al conductor";
                ViewBag.ConductorEliminado = false;


            }
            var lstConductores = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty();

            return View("~/Areas/Conductor/Views/Conductor/Index.cshtml", lstConductores.ToList());
        }

        public IActionResult AgregarCertificadoAlConductor(int ConductorId)
        {
            ViewBag.Error = "";
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            IConductor Conductor = new IConductor(FsvrConn);
            ViewBag.datosConductor = Conductor.GetConductor(fEmpresaId, ConductorId).DefaultIfEmpty().FirstOrDefault();

            return PartialView();
        }

        public IActionResult AgregarCertificadoAlConductorResult(int ConductorId, int CertificadoId)
        {

            ViewBag.Error = "";
            ViewBag.ConductorEliminado = false;
            ViewBag.VehiculoConductorCreado = false;
            ViewBag.ConductorCreado = false;
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            ICertificadoConductor CertificadoConductor = new ICertificadoConductor(FsvrConn);

            if (CertificadoId > 0 && ConductorId > 0)
            {

                IConductor Conductor = new IConductor(FsvrConn);

                int certificadoConductorId = 0;
                int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;

                using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();

                try
                {
                    var lstConductor = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty().FirstOrDefault();
                    var lstCertificadosConductor = CertificadoConductor.GetCertificadoConductores(fEmpresaId).DefaultIfEmpty().FirstOrDefault();

                    if (lstCertificadosConductor != null)
                    {
                        var ConductorIdAnterior = lstCertificadosConductor.CertificadoConductorId;

                        if (lstCertificadosConductor.CertificadoConductorId > 0)

                            certificadoConductorId = ConductorIdAnterior + 1;
                        else
                            certificadoConductorId = 1;
                    }
                    else
                        certificadoConductorId = 1;
                    var c = new DT_CertificadoConductor
                    {
                        ConductorId = ConductorId,
                        CertificadoId = CertificadoId,
                        EmpresaId = fEmpresaId,
                        CertificadoConductorId = certificadoConductorId,
                        Estado = "AC",
                        FechaIng = DateTime.Now,
                        FechaMod = DateTime.Now,
                    };

                    CertificadoConductor.AgregarCertificadoConductor(c);
                    dbTran.Commit();

                    var lstCertificadosConductores = CertificadoConductor.GetCertificadosXConductor(fEmpresaId, ConductorId).DefaultIfEmpty();

                    ViewBag.CertificadoConductorCreado = true;

                    return PartialView(lstCertificadosConductores.ToList());
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    ViewBag.Error = "Error al crear" + ex.InnerException + "<hr />MENSAJE--> " + ex.Message;
                    ViewBag.ListUsuarios = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty().OrderByDescending(c => c.ConductorId);
                    var lstCertificadosConductores = CertificadoConductor.GetCertificadosXConductor(fEmpresaId, ConductorId).DefaultIfEmpty();

                    ViewBag.CertificadoConductorCreado = false;

                    return PartialView(lstCertificadosConductores.ToList());
                }

            }
            else
            {
                var lstCertificadosConductores = CertificadoConductor.GetCertificadosXConductor(fEmpresaId, ConductorId).DefaultIfEmpty();

                ViewBag.CertificadoConductorCreado = false;

                return PartialView(lstCertificadosConductores.ToList());
            }

        }

        public IActionResult AgregarVehiculoAlConductor(int ConductorId)
        {
            ViewBag.Error = "";
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            IConductor Conductor = new IConductor(FsvrConn);
            ViewBag.datosConductor = Conductor.GetConductor(fEmpresaId, ConductorId).DefaultIfEmpty().FirstOrDefault();


            return PartialView();
        }

        public IActionResult AgregarVehiculoAlConductorResult(int ConductorId, int VehiculoId)
        {
            ViewBag.Error = "";
            ViewBag.ConductorVehiculoCreado = false;

            IVehiculo Vehiculo = new IVehiculo(FsvrConn);
            IVehiculoConductor VehiculoConductor = new IVehiculoConductor(FsvrConn);
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            if (VehiculoId > 0 && ConductorId > 0)
            {

                int certificadoVehiculoId = 0;
                int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;


                using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();

                try
                {
                    var lstVehiculo = Vehiculo.GetVehiculos(fEmpresaId).DefaultIfEmpty().FirstOrDefault();
                    var lstVehiculoConductor = VehiculoConductor.GetVehiculoConductores(fEmpresaId).DefaultIfEmpty().FirstOrDefault();

                    if (lstVehiculoConductor != null)
                    {
                        var ConductorIdAnterior = lstVehiculoConductor.VehiculoConductorId;

                        if (lstVehiculoConductor.VehiculoConductorId > 0)
                            certificadoVehiculoId = ConductorIdAnterior + 1;
                        else
                            certificadoVehiculoId = 1;
                    }
                    else
                        certificadoVehiculoId = 1;
                    var c = new DT_VehiculoConductor
                    {
                        EmpresaId = fEmpresaId,
                        VehiculoId = VehiculoId,
                        ConductorId = ConductorId,
                        VehiculoConductorId = certificadoVehiculoId,
                        Estado = "AC",
                        FechaIng = DateTime.Now,
                        FechaMod = DateTime.Now

                    };

                    VehiculoConductor.AgregarVehiculoConductor(c);
                    dbTran.Commit();

                    var lstVehiculosXConductor = VehiculoConductor.GetVehiculosConductorXConductor(fEmpresaId, ConductorId).DefaultIfEmpty();

                    ViewBag.ConductorVehiculoCreado = true;
                    return PartialView(lstVehiculosXConductor.ToList());
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    ViewBag.Error = "Error al crear" + ex.InnerException + "<hr />MENSAJE--> " + ex.Message;
                    ViewBag.ListUsuarios = Vehiculo.GetVehiculos(fEmpresaId).DefaultIfEmpty().OrderByDescending(c => c.VehiculoId);
                    var lstVehiculosXConductor = VehiculoConductor.GetVehiculosConductorXConductor(fEmpresaId, ConductorId).DefaultIfEmpty();

                    ViewBag.ConductorVehiculoCreado = false;
                    return PartialView(lstVehiculosXConductor.ToList());


                }

            }
            else
            {
                var lstVehiculosXConductor = VehiculoConductor.GetVehiculosConductorXConductor(fEmpresaId, ConductorId).DefaultIfEmpty();

                ViewBag.VehiculoVehiculoCreado = false;
                return PartialView(lstVehiculosXConductor.ToList());


            }
        }

        public ActionResult GetVehiculosFind(string q)
        {
            var fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            string estadoPermitido = "AC";

            IVehiculo Vehiculo = new IVehiculo(FsvrConn);
            var results = Vehiculo.GetVehiculoLike(fEmpresaId, q).Where(h => h.Estado.Equals(estadoPermitido)).
                          Select(h => new { id = h.VehiculoId, text = h.VehiculoId + " - Placa: " + h.CodPlacas + " - " + h.MarcaEmpresa + "/ " + h.Modelo }).ToList().Take(15);
            return Json(new { results });
        }

        public ActionResult GetCertificadosFind(string q)
        {
            var fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            string estadoPermitido = "AC";
            string tipoCertificadoConductor = "COD-CERT2";

            ICertificado Certificado = new ICertificado(FsvrConn);
            var results = Certificado.GetCertificadoLike(fEmpresaId, q).Where(h => h.Estado.Equals(estadoPermitido) && h.TipoCertificado.Equals(tipoCertificadoConductor)).
                          Select(h => new { id = h.CertificadoId, text = h.CertificadoId + " - COD: " + h.CodCertificado + " - " + h.NombreCertificado }).ToList().Take(15);
            return Json(new { results });
        }

    }
}