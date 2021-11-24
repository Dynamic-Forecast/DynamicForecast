using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DynamicForecast.Models;
using Microsoft.AspNetCore.Http;
using Helper;
using Microsoft.Extensions.Primitives;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using DynamicForecast.Clases;
using Microsoft.EntityFrameworkCore;
using DynamicForecast.Servicios;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using DNTBreadCrumb.Core;

namespace DynamicForecast.Controllers
{
    [BreadCrumb(Title = "Inicio", UseDefaultRouteUrl = true, Order = 0, IgnoreAjaxRequests = true)]
    public class HomeController : Controller
    {

        private readonly DynamicForecastContext FsvrConn;
        private readonly IConfiguration _configuration;

        public HomeController(DynamicForecastContext svrConn, IConfiguration configuration)
        {
            FsvrConn = svrConn;
            _configuration = configuration;
        }

        //--------------------------------------------------------------------------
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CodigoUsr")))
            {
                ViewBag.Error = "";

                return View();
            }
            else
            {
                ViewBag.Error = "";

                return RedirectToAction("Dashboard", "Dashboard");
            }


        }

        //--------------------------------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> Index(string E_Mail, string PalabraClave)
        {
            ViewBag.Error = "";
            try
            {
                HttpContext.Session.SetString("PalabraClave", PalabraClave);
                if (!string.IsNullOrEmpty(PalabraClave))
                    PalabraClave = HashHelper.MD5(PalabraClave);

                var usuario = FsvrConn.DT_Usuario.
                              Where(h => (h.E_Mail.Equals(E_Mail) || h.CodigoUsr.Equals(E_Mail))).
                              Where(h => h.PalabraClave.Equals(PalabraClave)).
                              Where(h => h.Estado.Equals("AC")).FirstOrDefault();

                if (usuario != null)
                {
                    HttpContext.Session.SetInt32("UsuarioId", usuario.UsuarioId);
                    HttpContext.Session.SetString("CodigoUsr", usuario.CodigoUsr);
                    HttpContext.Session.SetString("Nombre1", usuario.Nombre1);
                    HttpContext.Session.SetString("NombreUsr", usuario.NombreUsr);
                    HttpContext.Session.SetInt32("EmpresaId", usuario.EmpresaId);
                    HttpContext.Session.SetInt32("TipoUsuario", usuario.TipoUsuario);
                    HttpContext.Session.SetString("UsuarioAdmin", usuario.UsuarioAdmin ?? "");
                    HttpContext.Session.SetString("NombreEmpr", usuario.CT_Empresa.NombreEmpr);
                    HttpContext.Session.SetObjectAsJson("MyMenu", null);

                    //Se agregan variables de sessión para las laertas, se inicializan en ceros (0)
                    HttpContext.Session.SetInt32("Alertas", 0);
                    HttpContext.Session.SetInt32("InterConsultas", 0);

                    var ReCaptcha = Request.Form["g-recaptcha-response"];
                    var result = await VerifyCaptcha(ReCaptcha);
                    if (result.Success)
                    {
                        return RedirectToAction("Dashboard", "Dashboard");
                    }
                    else
                    {
                        HttpContext.Session.Clear();
                        ViewBag.Error = "Captcha invalido";
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Se presento un error: " + e.Message;
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CodigoUsr")))
            {
                if (ViewBag.Error == "")
                    ViewBag.Error = "Correo o Palabra clave invalido";
            }

            return View();

        }

        //--------------------------------------------------------------------------
        private async Task<CaptchaVerification> VerifyCaptcha(string captchaResponse)
        {
            string userIP = string.Empty;
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
            if (ipAddress != null) userIP = ipAddress.MapToIPv4().ToString();

            //var captchaResponse = Request.Form["g-recaptcha-response"];
            var payload = string.Format("&secret={0}&remoteip={1}&response={2}", "6LdF2tEUAAAAAINMMgFFXYCLJrR35AuI551WGD_x", userIP, captchaResponse);

            var client = new HttpClient
            {
                BaseAddress = new Uri("https://www.google.com")
            };
            var request = new HttpRequestMessage(HttpMethod.Post, "/recaptcha/api/siteverify")
            {
                Content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded")
            };
            var response = await client.SendAsync(request);
            return JsonConvert.DeserializeObject<CaptchaVerification>(response.Content.ReadAsStringAsync().Result);
        }
        //--------------------------------------------------------------------------

        public IActionResult Privacy()
        {
            return View();
        }

        //--------------------------------------------------------------------------
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //--------------------------------------------------------------------------


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("~/Views/Home/Index.cshtml");
        }

        //--------------------------------------------------------------------------

        public IActionResult Recover()
        {
            ViewBag.Actual = "";
            ViewBag.Nueva = "";
            ViewBag.Nuevarep = "";

            return View();
        }

        //--------------------------------------------------------------------------

        public IActionResult CambiarClave()
        {
            return View();
        }

        [Autenticado]
        public IActionResult MensajeActualizacion()
        {
            return View();
        }



        //--------------------------------------------------------------------------
        [HttpPost]
        public IActionResult CambiarClave(string PalabraClaveActual, string PalabraClaveNueva, string PalabraClaveNueva2)
        {
            ViewBag.Actual = PalabraClaveActual;
            ViewBag.Nueva = PalabraClaveNueva;
            ViewBag.Nuevarep = PalabraClaveNueva2;

            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            ViewBag.Error = "";
            string ca = "^,<>/_:;.()!$%&=?¿¡*+";
            string Caracteres = "N";

            string PalabraClave = FsvrConn.DT_Usuario.DefaultIfEmpty().
                                  Where(h => h.UsuarioId == fUsuarioId).First().PalabraClave;

            PalabraClaveActual = HashHelper.MD5(PalabraClaveActual).ToUpper();

            if (PalabraClave != PalabraClaveActual)
            {
                ViewBag.Error = "La palabra clave actual no Corresponde";
            }
            else
            {
                if (PalabraClaveNueva != PalabraClaveNueva2)
                {
                    ViewBag.Error = "Las palabras claves nuevas no coinciden";
                }

                if (!PalabraClaveNueva.Any(c => char.IsUpper(c)))
                {
                    ViewBag.Error = "La palabra clave no CUMPLE con los requisitos de seguridad requeridos (Mínimo un caracter en Mayúscula)";
                }

                if (!PalabraClaveNueva.Any(c => char.IsLower(c)))
                {
                    ViewBag.Error = "La palabra clave no CUMPLE con los requisitos de seguridad requeridos (Mínimo un caracter en Minúscula";
                }

                if (!PalabraClaveNueva.Any(c => char.IsNumber(c)))
                {
                    ViewBag.Error = "La palabra clave no CUMPLE con los requisitos de seguridad requeridos (Mínimo un caracter numérico)";
                }

                foreach (char c in ca)
                {
                    if (PalabraClaveNueva.IndexOf(c) > 0) { Caracteres = "S"; }
                };

                if (Caracteres == "N")
                {
                    ViewBag.Error = "La palabra clave no CUMPLE con los requisitos de seguridad requeridos (Mínimo un caracter especial  ^,<>/_:;.()!$%&=?¿¡*+)";
                }

                if (ViewBag.Error == "")
                {
                    PalabraClaveNueva = HashHelper.MD5(PalabraClaveNueva).ToUpper();

                    var Usuario = FsvrConn.DT_Usuario.DefaultIfEmpty().Where(h => h.UsuarioId == fUsuarioId).First();
                    Usuario.PalabraClave = PalabraClaveNueva;

                    FsvrConn.DT_Usuario.Update(Usuario);
                    FsvrConn.SaveChanges();

                    ViewBag.Error = "PALABRA CLAVE CAMBIADA CON EXITO !!";
                }

            }

            return View();
        }







        //--------------------------------------------------------------------------
        public IActionResult EnviarCorreoContrasena()
        {
            //IComunes com = new IComunes(FsvrConn);

            // Se envia Email
            //var Host = _configuration.GetSection("EmailConfiguration").GetSection("Host").Value;
            //var Port = _configuration.GetSection("EmailConfiguration").GetSection("Port").Value;
            //var EnableSsl = _configuration.GetSection("EmailConfiguration").GetSection("EnableSsl").Value;
            //var UseDefaultCredentials = _configuration.GetSection("EmailConfiguration").GetSection("UseDefaultCredentials").Value;
            //var Username = _configuration.GetSection("EmailConfiguration").GetSection("Username").Value;
            //var Password = _configuration.GetSection("EmailConfiguration").GetSection("Password").Value;

            //SmtpClient smtp = new SmtpClient
            //{
            //    Host = Host,
            //    Port = Convert.ToInt32(Port),
            //    EnableSsl = Convert.ToBoolean(EnableSsl),
            //    UseDefaultCredentials = Convert.ToBoolean(UseDefaultCredentials),
            //    Credentials = new NetworkCredential(Username, Password)
            //};

            //MailMessage mm = new MailMessage
            //{
            //    IsBodyHtml = true,
            //    Priority = MailPriority.Normal,
            //    From = new MailAddress(_Correo.MailEnvio),
            //    Subject = _Correo.Asunto,
            //    Body = "<h4> Email enviado desde contáctenos de la tienda </h4>"
            //};

            //mm.Body += "<p>" + _Correo.Mensaje + "</p>  <br /> <br />";
            //mm.Body += "<p> Correo enviado por : " + _Correo.EnviadoPor + "</p>";
            //mm.Body += "<p>" + _Correo.EnviadoPor + "</p>";
            //mm.To.Add(new MailAddress(fCorreoEmpresa));
            //smtp.Send(mm); // Enviar el mensaje

            return View();
        }

        //----------------------------------------------------------------
        public ActionResult CambiarDeEmpresa()
        {
            ViewBag.Error = "";
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int tipoUsuario = HttpContext.Session.GetInt32("TipoUsuario") ?? 0;

            try
            {
                if (tipoUsuario == 4)
                {
                    ViewBag.Empresas = FsvrConn.CT_Empresa.AsNoTracking().DefaultIfEmpty().
                                       Where(h => h.Estado == "AC").OrderBy(h => h.NombreEmpr).
                                       Select(h => new { h.EmpresaId, NombreEmpr = h.EmpresaId + " - " + h.NombreEmpr }).ToList();
                }
                else
                {
                    ViewBag.Empresas = (from ue in FsvrConn.DT_UsuarioXEmpr
                                        where ue.UsuarioId == fUsuarioId
                                        select new
                                        {
                                            ue.EmpresaId,
                                            ue.CT_Empresa.NombreEmpr
                                        }).Union
                                        (from us in FsvrConn.DT_Usuario
                                         from em in FsvrConn.CT_Empresa
                                         where
                                         us.UsuarioId == fUsuarioId &&
                                         em.EmpresaId == us.EmpresaId
                                         select new
                                         {
                                             em.EmpresaId,
                                             em.NombreEmpr
                                         }).OrderBy(h => h.NombreEmpr).
                                       Select(h => new { h.EmpresaId, NombreEmpr = h.EmpresaId + " - " + h.NombreEmpr }).ToList();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Se presento un error : " + ex.Message;
            }

            return PartialView();

        }

        //-----------------------------------------------------------------------------------------------------------------------

        public ActionResult CambiarEmpresaPost(int empresaId)
        {
            ViewBag.Error = "";

            try
            {
                int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
                int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
                string fPalabraActual = HttpContext.Session.GetString("PalabraClave");

                if (fEmpresaId != empresaId)
                {
                    HttpContext.Session.Clear();

                    var usuario = FsvrConn.DT_Usuario.AsNoTracking().DefaultIfEmpty().
                                  Include(h => h.CT_Empresa).DefaultIfEmpty().
                                  Where(h => h.UsuarioId == fUsuarioId).FirstOrDefault();

                    usuario.EmpresaId = empresaId;

                    FsvrConn.Entry(usuario).State = EntityState.Modified;
                    FsvrConn.SaveChanges();

                    usuario = FsvrConn.DT_Usuario.AsNoTracking().DefaultIfEmpty().
                              Include(h => h.CT_Empresa).DefaultIfEmpty().
                              Where(h => h.UsuarioId == fUsuarioId).FirstOrDefault();

                    if (usuario != null)
                    {

                        HttpContext.Session.SetInt32("UsuarioId", usuario.UsuarioId);
                        HttpContext.Session.SetString("CodigoUsr", usuario.CodigoUsr);
                        HttpContext.Session.SetString("Nombre1", usuario.Nombre1);
                        HttpContext.Session.SetString("NombreUsr", usuario.NombreUsr);
                        HttpContext.Session.SetInt32("EmpresaId", usuario.EmpresaId);
                        HttpContext.Session.SetInt32("TipoUsuario", usuario.TipoUsuario);
                        HttpContext.Session.SetString("UsuarioAdmin", usuario.UsuarioAdmin ?? "");
                        HttpContext.Session.SetString("NombreEmpr", usuario.CT_Empresa.NombreEmpr);
                        HttpContext.Session.SetString("PalabraClave", fPalabraActual);
                        HttpContext.Session.SetObjectAsJson("MyMenu", null);

                        //Se agregan variables de sessión para las laertas, se inicializan en ceros (0)
                        HttpContext.Session.SetInt32("Alertas", 0);
                        HttpContext.Session.SetInt32("InterConsultas", 0);
                    }

                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Se presento un error : " + ex.Message;
            }

            return RedirectToAction("Dashboard", "Dashboard");

        }

        //----------------------------------------------------------------

    }
}
