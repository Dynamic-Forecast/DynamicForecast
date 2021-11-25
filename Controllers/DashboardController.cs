using System;
using System.Data;
using System.Linq;
using DynamicForecast.Clases;
using DynamicForecast.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
//using DynamicForecast.Areas.Inventarios.Models;
//using DynamicForecast.Areas.Salud.Models;

namespace DynamicForecast.Controllers
{
    [Autenticado]

    public class DashboardController : Controller
    {
        // GET: Dashboard
        private readonly DynamicForecastContext FsvrConn;

        public DashboardController(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }
        public IActionResult Dashboard()
        {
            ViewBag.permitido = false;
            ViewBag.Error = "";

            //int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            //int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            return View();
        }

    }
}