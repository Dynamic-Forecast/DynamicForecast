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

namespace DynamicForecast.Areas.ModeloRecomendacion.Controllers
{
    [Area("ModeloRecomendacion")]
    [Route("ModeloRecomendacion/[controller]/[action]")]
    [Autenticado]
    [BreadCrumb(Title = "ModeloRecomendacions", Url = "/Viaje/ModeloRecomendacion/Index", Order = 0, IgnoreAjaxRequests = true)]

    public class ModeloRecomendacionController : Controller
    {
        private readonly DynamicForecastContext FsvrConn;
        private readonly IWebHostEnvironment _oIHostingEnvironment;

        public ModeloRecomendacionController(DynamicForecastContext svrConn, IWebHostEnvironment oIHostingEnvironment)
        {
            FsvrConn = svrConn;
            _oIHostingEnvironment = oIHostingEnvironment;

        }
        public IActionResult Index()
        {
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            IModeloRecomendacion ModeloRecomendacion = new IModeloRecomendacion(FsvrConn);
            var lstModeloRecomendacion = ModeloRecomendacion.GetModeloRecomendacions(fEmpresaId).DefaultIfEmpty();
            ViewBag.ModeloRecomendacionCreado = false;
            ViewBag.Error = "";
            ViewBag.ModeloRecomendacionEliminado = false;

            return View(lstModeloRecomendacion.ToList());
        }

        public IActionResult CrearModeloRecomendacion()
        {
            ViewBag.Error = "";

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearModeloRecomendacionAsync(IFormFile fileobj, AP_ModeloRecomendacion c)
        {
            ViewBag.Error = "";
            ViewBag.ModeloRecomendacionEliminado = false;
            IModeloRecomendacion ModeloRecomendacion = new IModeloRecomendacion(FsvrConn);

            int ModeloRecomendacionId = 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();
            var fileSize = Convert.ToInt32(ByteSize.FromBytes(fileobj.Length).Megabytes);
            var maxAdjunto = 2;
            try
            {
                var lstModeloRecomendacion = ModeloRecomendacion.GetModeloRecomendacions(fEmpresaId).DefaultIfEmpty().FirstOrDefault();


                var ModeloRecomendacionIdAnterior = lstModeloRecomendacion.ModeloRecomendacionId;

                if (lstModeloRecomendacion.ModeloRecomendacionId > 0)
                    ModeloRecomendacionId = ModeloRecomendacionIdAnterior + 1;
                else
                    ModeloRecomendacionId = 1;


                if (fileSize <= maxAdjunto)
                {
                    var extension = Path.GetExtension(fileobj.FileName);
                    extension = extension.ToLower();

                    if (extension == ".jpg" || extension == ".png" || extension == ".pdf" || extension == ".doc" || extension == ".docx"
                       || extension == ".xls" || extension == ".xlsx")
                    {

                        string webRoot = _oIHostingEnvironment.WebRootPath.ToString();
                        string dateTimeFile = DateTime.Now.ToShortDateString().ToString().Replace(" ", "").Replace("/", "").Replace(".", "").Replace(":", "").Replace(";", "");
                        string pathUpload = webRoot + "\\Adjunto\\" + "\\ModeloRecomendacion\\" + fEmpresaId.ToString() + "\\" + dateTimeFile + "_" + fileobj.FileName;
                        string pathDownload = "..\\.." + "\\Adjunto\\" + "\\ModeloRecomendacion\\" + fEmpresaId.ToString() + "\\" + dateTimeFile + "_" + fileobj.FileName;

                        Directory.CreateDirectory(Path.GetDirectoryName(pathUpload));
                        var stream = new FileStream(pathUpload, FileMode.Create);
                        await fileobj.CopyToAsync(stream);

                        c.ModeloRecomendacionId = ModeloRecomendacionId;
                        c.EmpresaId = fEmpresaId;
                        c.Estado = "AC";
                        c.FechaIng = DateTime.Now;
                        c.FechaMod = DateTime.Now;
                        c.RutaArchivo = pathDownload;
                        ModeloRecomendacion.AgregarModeloRecomendacion(c);

                        await FsvrConn.SaveChangesAsync();


                        dbTran.Commit();

                    }
                    else
                    {

                        ViewBag.Error = "La extensión del archivo no es valida.";
                    }
                }
                else
                {

                    ViewBag.Error = "El tamaño del archivo supera el máximo permitido.";
                }




                var lstModeloRecomendaciones = ModeloRecomendacion.GetModeloRecomendacions(fEmpresaId).DefaultIfEmpty();

                ViewBag.ModeloRecomendacionCreado = true;
                return View("~/Areas/ModeloRecomendacion/Views/ModeloRecomendacion/Index.cshtml", lstModeloRecomendaciones.ToList());
            }
            catch (Exception ex)
            {
                dbTran.Rollback();
                ViewBag.Error = "Error al crear" + ex.InnerException + "<hr />MENSAJE--> " + ex.Message;
                ViewBag.ListUsuarios = ModeloRecomendacion.GetModeloRecomendacions(fEmpresaId).DefaultIfEmpty().OrderByDescending(c => c.ModeloRecomendacionId);
                ViewBag.ModeloRecomendacionCreado = false;
                var lstModeloRecomendaciones = ModeloRecomendacion.GetModeloRecomendacions(fEmpresaId).DefaultIfEmpty();

                return View("~/Areas/ModeloRecomendacion/Views/ModeloRecomendacion/Index.cshtml", lstModeloRecomendaciones.ToList());
            }


        }

        public IActionResult EliminarModeloRecomendacion(int? ModeloRecomendacionId)
        {
            ViewBag.Error = "";
            IModeloRecomendacion ModeloRecomendacion = new IModeloRecomendacion(FsvrConn);
            ViewBag.ModeloRecomendacionCreado = false;


            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            if (ModeloRecomendacionId != null && ModeloRecomendacionId > 0)
            {
                var ModeloRecomendacionEliminar = ModeloRecomendacion.GetModeloRecomendacion(fEmpresaId, (int)ModeloRecomendacionId).DefaultIfEmpty().FirstOrDefault();
                if (ModeloRecomendacionEliminar != null)
                {
                    ModeloRecomendacion.EliminarModeloRecomendacion(ModeloRecomendacionEliminar);
                    ViewBag.ModeloRecomendacionEliminado = true;
                }
                else
                {
                    ViewBag.Error = "No se puede eliminar el ModeloRecomendacion, ya se ha eliminado.";
                    ViewBag.ModeloRecomendacionEliminado = false;
                }

            }
            else
            {
                ViewBag.Error = "No se puede eliminar el evento";
                ViewBag.ModeloRecomendacionEliminado = false;


            }
            var lstModeloRecomendaciones = ModeloRecomendacion.GetModeloRecomendacions(fEmpresaId).DefaultIfEmpty();

            return View("~/Areas/ModeloRecomendacion/Views/ModeloRecomendacion/Index.cshtml", lstModeloRecomendaciones.ToList());
        }


        public ActionResult GetModeloRecomendacionsFind(string q)
        {
            var fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            string estadoPermitido = "AC";

            IModeloRecomendacion ModeloRecomendacion = new IModeloRecomendacion(FsvrConn);
            var results = ModeloRecomendacion.GetModeloRecomendacionLike(fEmpresaId, q).Where(h => h.Estado.Equals(estadoPermitido)).
                          Select(h => new { id = h.ModeloRecomendacionId, text = h.ModeloRecomendacionId + " - " + h.Nombre }).ToList().Take(15);
            return Json(new { results });
        }



    }
}