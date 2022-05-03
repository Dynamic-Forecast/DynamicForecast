using DynamicForecast.Areas.Certificado.Models;
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

namespace DynamicForecast.Areas.Certificado.Controllers
{
    [Area("Certificado")]
    [Route("Certificado/[controller]/[action]")]
    [Autenticado]
    [BreadCrumb(Title = "Certificados", Url = "/Certificado/Certificado/Index", Order = 0, IgnoreAjaxRequests = true)]

    public class CertificadoController : Controller
    {
        private readonly DynamicForecastContext FsvrConn;

        public CertificadoController(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IActionResult Index()
        {
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            ICertificado Certificado = new ICertificado(FsvrConn);
            var lstCertificado = Certificado.GetCertificados(fEmpresaId).DefaultIfEmpty();
            ViewBag.CertificadoCreado = false;
            ViewBag.Error = "";
            ViewBag.CertificadoEliminado = false;

            return View(lstCertificado.ToList());
        }

        public IActionResult CrearCertificado()
        {
            ViewBag.Error = "";

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearCertificado([Bind] DT_Certificado c)
        {
            ViewBag.Error = "";
            ViewBag.CertificadoEliminado = false;
            ICertificado Certificado = new ICertificado(FsvrConn);

            int CertificadoId = 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();

            try
            {
                var lstCertificado = Certificado.GetCertificados(fEmpresaId).DefaultIfEmpty().FirstOrDefault();

                if (lstCertificado != null)
                {
                    var CertificadoIdAnterior = lstCertificado.CertificadoId;

                    if (lstCertificado.CertificadoId > 0)
                        CertificadoId = CertificadoIdAnterior + 1;
                    else
                        CertificadoId = 1;
                }
                else
                    CertificadoId = 1;

                c.CertificadoId = CertificadoId;
                c.EmpresaId = fEmpresaId;
                c.Estado = "AC";
                c.FechaIng = DateTime.Now;
                c.FechaMod = DateTime.Now;
                Certificado.AgregarCertificado(c);
                dbTran.Commit();

                var lstCertificadoes = Certificado.GetCertificados(fEmpresaId).DefaultIfEmpty();

                ViewBag.CertificadoCreado = true;
                return View("~/Areas/Certificado/Views/Certificado/Index.cshtml", lstCertificadoes.ToList());
            }
            catch (Exception ex)
            {
                dbTran.Rollback();
                ViewBag.Error = "Error al crear" + ex.InnerException + "<hr />MENSAJE--> " + ex.Message;
                ViewBag.ListUsuarios = Certificado.GetCertificados(fEmpresaId).DefaultIfEmpty().OrderByDescending(c => c.CertificadoId);
                ViewBag.CertificadoCreado = false;
                var lstCertificadoes = Certificado.GetCertificados(fEmpresaId).DefaultIfEmpty();

                return View("~/Areas/Certificado/Views/Certificado/Index.cshtml", lstCertificadoes.ToList());
            }


        }

        public IActionResult EliminarCertificado(int? CertificadoId)
        {
            ViewBag.Error = "";
            ICertificado Certificado = new ICertificado(FsvrConn);
            ViewBag.CertificadoCreado = false;


            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            if (CertificadoId != null && CertificadoId > 0)
            {
                var CertificadoEliminar = Certificado.GetCertificado(fEmpresaId, (int)CertificadoId).DefaultIfEmpty().FirstOrDefault();
                if (CertificadoEliminar != null)
                {
                    Certificado.EliminarCertificado(CertificadoEliminar);
                    ViewBag.CertificadoEliminado = true;
                }
                else
                {
                    ViewBag.Error = "No se puede eliminar el Certificado, ya se ha eliminado.";
                    ViewBag.CertificadoEliminado = false;
                }

            }
            else
            {
                ViewBag.Error = "No se puede eliminar el evento";
                ViewBag.CertificadoEliminado = false;


            }
            var lstCertificadoes = Certificado.GetCertificados(fEmpresaId).DefaultIfEmpty();

            return View("~/Areas/Certificado/Views/Certificado/Index.cshtml", lstCertificadoes.ToList());
        }


        public ActionResult GetCertificadosFind(string q)
        {
            var fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            string estadoPermitido = "AC";

            ICertificado Certificado = new ICertificado(FsvrConn);
            var results = Certificado.GetCertificadoLike(fEmpresaId, q).Where(h => h.Estado.Equals(estadoPermitido)).
                          Select(h => new { id = h.CertificadoId, text = h.CertificadoId + " - Cod Certificado: " + h.CodCertificado + " - " + h.DescripcionCertificado }).ToList().Take(15);
            return Json(new { results });
        }



    }
}