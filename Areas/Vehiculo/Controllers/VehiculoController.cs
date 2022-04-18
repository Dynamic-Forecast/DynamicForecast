using DynamicForecast.Areas.Vehiculo.Models;
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

namespace DynamicForecast.Areas.Vehiculo.Controllers
{
    [Area("Vehiculo")]
    [Route("Vehiculo/[controller]/[action]")]
    [Autenticado]
    [BreadCrumb(Title = "Vehiculos", Url = "/Vehiculo/Vehiculo/Index", Order = 0, IgnoreAjaxRequests = true)]

    public class VehiculoController : Controller
    {
        private readonly DynamicForecastContext FsvrConn;
        //private readonly IWebHostEnvironment _oIHostingEnvironment;

        //public VehiculoController(DynamicForecastContext svrConn, IWebHostEnvironment oIHostingEnvironment)
        //{
        //    FsvrConn = svrConn;
        //    _oIHostingEnvironment = oIHostingEnvironment;

        //}
        public VehiculoController(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IActionResult Index()
        {
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            IVehiculo Vehiculo = new IVehiculo(FsvrConn);
            var lstVehiculo = Vehiculo.GetVehiculos(fEmpresaId).DefaultIfEmpty();
            ViewBag.VehiculoCreado = false;
            ViewBag.CertificadoVehiculoCreado = false;
            ViewBag.VehiculoVehiculoCreado = false;
            ViewBag.Error = "";
            ViewBag.VehiculoEliminado = false;

            return View(lstVehiculo.ToList());
        }

        //[BreadCrumb(Title = "Crear Vehiculo", Url = "/Vehiculo/CrearVehiculo", Order = 1, IgnoreAjaxRequests = true)]
        public IActionResult CrearVehiculo()
        {
            ViewBag.Error = "";
            //ViewBag.VehiculoCreado = false;
            //ViewBag.VehiculoEliminado = false;

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearVehiculo([Bind] DT_Vehiculo c)
        {
            ViewBag.Error = "";
            ViewBag.VehiculoEliminado = false;
            ViewBag.CertificadoVehiculoCreado = false;
            ViewBag.VehiculoVehiculoCreado = false;

            IVehiculo Vehiculo = new IVehiculo(FsvrConn);

            int VehiculoId = 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();

            try
            {
                var lstVehiculo = Vehiculo.GetVehiculos(fEmpresaId).DefaultIfEmpty().FirstOrDefault();

                if (lstVehiculo != null)
                {
                    var VehiculoIdAnterior = lstVehiculo.VehiculoId;

                    if (lstVehiculo.VehiculoId > 0)
                        VehiculoId = VehiculoIdAnterior + 1;
                    else
                        VehiculoId = 1;
                }
                else
                    VehiculoId = 1;

                c.VehiculoId = VehiculoId;
                c.EmpresaId = fEmpresaId;
                c.Estado = "AC";
                c.FechaIng = DateTime.Now;
                c.FechaMod = DateTime.Now;
                Vehiculo.AgregarVehiculo(c);
                dbTran.Commit();

                var lstVehiculoes = Vehiculo.GetVehiculos(fEmpresaId).DefaultIfEmpty();

                ViewBag.VehiculoCreado = true;
                return View("~/Areas/Vehiculo/Views/Vehiculo/Index.cshtml", lstVehiculoes.ToList());

            }
            catch (Exception ex)
            {
                dbTran.Rollback();
                ViewBag.Error = "Error al crear" + ex.InnerException + "<hr />MENSAJE--> " + ex.Message;
                ViewBag.ListUsuarios = Vehiculo.GetVehiculos(fEmpresaId).DefaultIfEmpty().OrderByDescending(c => c.VehiculoId);
                ViewBag.VehiculoCreado = false;
                var lstVehiculoes = Vehiculo.GetVehiculos(fEmpresaId).DefaultIfEmpty();

                return View("~/Areas/Vehiculo/Views/Vehiculo/Index.cshtml", lstVehiculoes.ToList());
            }

        }

        public IActionResult EliminarVehiculo(int? VehiculoId)
        {
            ViewBag.Error = "";
            IVehiculo Vehiculo = new IVehiculo(FsvrConn);
            ViewBag.VehiculoCreado = false;


            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            if (VehiculoId != null && VehiculoId > 0)
            {
                var VehiculoEliminar = Vehiculo.GetVehiculo(fEmpresaId, (int)VehiculoId).DefaultIfEmpty().FirstOrDefault();
                if (VehiculoEliminar != null)
                {
                    Vehiculo.EliminarVehiculo(VehiculoEliminar);
                    ViewBag.VehiculoEliminado = true;
                }
                else
                {
                    ViewBag.Error = "No se puede eliminar el Vehiculo, ya se ha eliminado.";
                    ViewBag.VehiculoEliminado = false;
                }

            }
            else
            {
                ViewBag.Error = "No se puede eliminar el evento";
                ViewBag.VehiculoEliminado = false;


            }
            var lstVehiculoes = Vehiculo.GetVehiculos(fEmpresaId).DefaultIfEmpty();

            return View("~/Areas/Vehiculo/Views/Vehiculo/Index.cshtml", lstVehiculoes.ToList());
        }
        public IActionResult EliminarCertificadoVehiculo(int CertificadoVehiculoId)
        {
            ViewBag.Error = "";
            IVehiculo Vehiculo = new IVehiculo(FsvrConn);
            ICertificadoVehiculo CertificadoVehiculo = new ICertificadoVehiculo(FsvrConn);
            ViewBag.VehiculoCreado = false;


            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            if (CertificadoVehiculoId > 0)
            {
                var CertificadoVehiculoEliminar = CertificadoVehiculo.GetCertificadosXCertificadosVehiculo(fEmpresaId, (int)CertificadoVehiculoId).DefaultIfEmpty().FirstOrDefault();
                if (CertificadoVehiculoEliminar != null)
                {
                    CertificadoVehiculo.EliminarCertificadoVehiculo(CertificadoVehiculoEliminar);
                    ViewBag.VehiculoEliminado = false;
                }
                else
                {
                    ViewBag.Error = "No se puede eliminar el certificado al Vehiculo, ya se ha eliminado o no existe.";
                    ViewBag.VehiculoEliminado = false;
                }

            }
            else
            {
                ViewBag.Error = "No se puede eliminar el certificado al Vehiculo";
                ViewBag.VehiculoEliminado = false;


            }
            var lstVehiculoes = Vehiculo.GetVehiculos(fEmpresaId).DefaultIfEmpty();

            return View("~/Areas/Vehiculo/Views/Vehiculo/Index.cshtml", lstVehiculoes.ToList());
        }

        public IActionResult AgregarCertificadoAlVehiculo(int VehiculoId)
        {
            ViewBag.Error = "";
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            IVehiculo Vehiculo = new IVehiculo(FsvrConn);
            ViewBag.datosVehiculo = Vehiculo.GetVehiculo(fEmpresaId, VehiculoId).DefaultIfEmpty().FirstOrDefault();

            return PartialView();
        }

        public IActionResult AgregarCertificadoAlVehiculoResult(int VehiculoId, int CertificadoId)
        {

            ViewBag.Error = "";
            ViewBag.VehiculoEliminado = false;
            ViewBag.VehiculoVehiculoCreado = false;
            ViewBag.VehiculoCreado = false;
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            ICertificadoVehiculo CertificadoVehiculo = new ICertificadoVehiculo(FsvrConn);

            if (CertificadoId > 0)
            {

                IVehiculo Vehiculo = new IVehiculo(FsvrConn);

                int certificadoVehiculoId = 0;
                int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;


                using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();

                try
                {
                    var lstVehiculo = Vehiculo.GetVehiculos(fEmpresaId).DefaultIfEmpty().FirstOrDefault();
                    var lstCertificadosVehiculo = CertificadoVehiculo.GetCertificadoVehiculos(fEmpresaId).DefaultIfEmpty().FirstOrDefault();

                    if (lstCertificadosVehiculo != null)
                    {
                        var CertificadosVehiculoIdAnterior = lstCertificadosVehiculo.CertificadoVehiculoId;

                        if (lstCertificadosVehiculo.CertificadoVehiculoId > 0)
                            certificadoVehiculoId = CertificadosVehiculoIdAnterior + 1;
                        else
                            certificadoVehiculoId = 1;
                    }
                    else
                        certificadoVehiculoId = 1;
                    var c = new DT_CertificadoVehiculo
                    {
                        VehiculoId = VehiculoId,
                        CertificadoId = CertificadoId,
                        EmpresaId = fEmpresaId,
                        CertificadoVehiculoId = certificadoVehiculoId,
                        Estado = "AC",
                        FechaIng = DateTime.Now,
                        FechaMod = DateTime.Now,

                    };

                    CertificadoVehiculo.AgregarCertificadoVehiculo(c);
                    dbTran.Commit();


                    var lstCertificadosVehiculoes = CertificadoVehiculo.GetCertificadosXVehiculo(fEmpresaId, VehiculoId).DefaultIfEmpty();

                    ViewBag.CertificadoVehiculoCreado = true;

                    return PartialView(lstCertificadosVehiculoes.ToList());
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    ViewBag.Error = "Error al crear" + ex.InnerException + "<hr />MENSAJE--> " + ex.Message;
                    ViewBag.ListUsuarios = Vehiculo.GetVehiculos(fEmpresaId).DefaultIfEmpty().OrderByDescending(c => c.VehiculoId);
                    var lstCertificadosVehiculoes = CertificadoVehiculo.GetCertificadosXVehiculo(fEmpresaId, VehiculoId).DefaultIfEmpty();

                    ViewBag.CertificadoVehiculoCreado = false;

                    return PartialView(lstCertificadosVehiculoes.ToList());
                }

            }
            else
            {
                var lstCertificadosVehiculoes = CertificadoVehiculo.GetCertificadosXVehiculo(fEmpresaId, VehiculoId).DefaultIfEmpty();

                ViewBag.CertificadoVehiculoCreado = false;

                return PartialView(lstCertificadosVehiculoes.ToList());
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
            string tipoCertificadoVehiculo = "COD-CERT1";

            ICertificado Certificado = new ICertificado(FsvrConn);
            var results = Certificado.GetCertificadoLike(fEmpresaId, q).Where(h => h.Estado.Equals(estadoPermitido) && h.TipoCertificado.Equals(tipoCertificadoVehiculo)).
                          Select(h => new { id = h.CertificadoId, text = h.CertificadoId + " - COD: " + h.CodCertificado + " - " + h.NombreCertificado }).ToList().Take(15);
            return Json(new { results });
        }

    }
}