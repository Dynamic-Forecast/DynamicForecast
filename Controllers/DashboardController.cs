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
            return View();
        }
        public void CargarDatos()
        {
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;

            IViaje Vaije = new IViaje(FsvrConn);

            var ListViajes = Vaije.GetViajes(fEmpresaId).DefaultIfEmpty();

            if (ListViajes.FirstOrDefault().ViajeId > 0)
            {
                ViewBag.UltimoViaje = ListViajes.OrderByDescending(h => h.FechaIng).FirstOrDefault();

                ViewBag.PromedioTiempo = ListViajes.Average(m => m.TiempoTotalViaje);
                ViewBag.PromedioSatisfaccion = ListViajes.Average(m => m.Satisfaccion);
                ViewBag.VentasTotales = ListViajes.Sum(m => m.ValorPagado);

                var ViajesCreados = ListViajes.Count();
                var ViajesFinalizados = ListViajes.Where(m => m.Estado.Equals("FN")).Count();
                ViewBag.ViajesPendientesDeFinalizar = ListViajes.Where(m => m.Estado.Equals("EV")).Count();
                ViewBag.NumViajesCreados = ViajesCreados;
                if (ViajesCreados > 0)
                    ViewBag.PorcentajeViajesConcretados = Math.Truncate((double)((ViajesFinalizados / ViajesCreados) * 100));
                else
                    ViewBag.PorcentajeViajesConcretados = 0;
            }
            else
            {
                ViewBag.UltimoViaje = null;
                ViewBag.PromedioTiempo = 0;
                ViewBag.VentasTotales = 0;
                ViewBag.PorcentajeViajesConcretados = 0;
                ViewBag.ViajesPendientesDeFinalizar = 0;
                ViewBag.PromedioSatisfaccion = 0;
            }
        }

    }
}