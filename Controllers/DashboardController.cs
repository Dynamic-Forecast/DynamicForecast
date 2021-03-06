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
            CargarDatos();
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
                ViewBag.ViajesPendientesDeFinalizar = ListViajes.Where(m => m.Estado.Equals("EP") || m.Estado.Equals("IN")).Count();
                ViewBag.NumViajesCreados = ViajesCreados;
                double porcentajeViajesConcretados;
                ViewBag.NumViajesCreadosFinalizados = ViajesFinalizados;
                if (ViajesCreados > 0)
                    porcentajeViajesConcretados = ((double)ViajesFinalizados / (double)ViajesCreados) * 100;
                else
                    porcentajeViajesConcretados = 0;

                ViewBag.PorcentajeViajesConcretados = porcentajeViajesConcretados;

                IVehiculo Vehiculo = new IVehiculo(FsvrConn);
                IConductor Conductor = new IConductor(FsvrConn);

                var ListVehiculos = Vehiculo.GetVehiculos(fEmpresaId).DefaultIfEmpty();
                var ListConductores = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty();

                var VehiculosCreados = ListVehiculos.Count();
                var VehiculosCreadosOcupados = ListVehiculos.Where(m => m.Estado.Equals("OC")).Count();

                var ConductoresCreados = ListConductores.Count();
                var ConductoresCreadosOcupados = ListConductores.Where(m => m.Estado.Equals("OC")).Count();

                double porcentajeVehiculosEnUso;
                if (VehiculosCreados > 0)
                    porcentajeVehiculosEnUso = ((double)VehiculosCreadosOcupados / (double)VehiculosCreados) * 100;
                else
                    porcentajeVehiculosEnUso = 0;

                double porcentajeConductoreEnUso;
                if (ConductoresCreados > 0)
                    porcentajeConductoreEnUso = ((double)ConductoresCreadosOcupados / (double)ConductoresCreados) * 100;
                else
                    porcentajeConductoreEnUso = 0;

                ViewBag.NumConductores = ConductoresCreados;
                ViewBag.NumVehiculos = VehiculosCreados;

                ViewBag.PorcentajeConductoresOcupados = porcentajeConductoreEnUso;
                ViewBag.PorcentajeVehiculosOcupados = porcentajeVehiculosEnUso;

            }
            else
            {
                ViewBag.UltimoViaje = null;
                ViewBag.PromedioTiempo = 0;
                ViewBag.VentasTotales = 0;
                ViewBag.PorcentajeViajesConcretados = 0;
                ViewBag.ViajesPendientesDeFinalizar = 0;
                ViewBag.PromedioSatisfaccion = 0;

                ViewBag.NumConductores = 0;
                ViewBag.NumVehiculos = 0;

                ViewBag.PorcentajeConductoresOcupados = 0;
                ViewBag.PorcentajeVehiculosOcupados = 0;
            }
        }

    }
}