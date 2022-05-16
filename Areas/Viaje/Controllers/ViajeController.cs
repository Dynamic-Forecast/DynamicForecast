using DynamicForecast.Clases;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using DynamicForecast.Areas.Viaje.Models;
using DynamicForecast.Servicios;
using System.Collections.Generic;
using DynamicForecast.Areas.Vehiculo.Models;
using System.Collections.ObjectModel;
using DynamicForecast.Areas.Conductor.Models;
using System.Collections;

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
            IList lstViaje = CargarDatosIndex();

            return View(lstViaje);
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
            ISimulacion Simulacion = new ISimulacion(FsvrConn);
            ViewBag.ErrorRecomendacion = "";
            ViewBag.ErrorFinalizacion = "";
            ViewBag.ErrorEditar = "";

            int ViajeId = 0;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();

            try
            {

                // Creación viaje
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
                c.Estado = "IN"; //Creado


                c.FechaIng = DateTime.Now;
                c.FechaMod = DateTime.Now;
                c.FechaFinalizacion = null;
                c.FechaInicioViaje = null;
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

        public IActionResult EditarViaje(int? ViajeId)
<<<<<<< Updated upstream
=======
        {
            ViewBag.Error = "";
            IViaje Viaje = new IViaje(FsvrConn);
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            var ViajeEditar = Viaje.GetViajeCompleto(fEmpresaId, (int)ViajeId).DefaultIfEmpty().FirstOrDefault();


            return PartialView(ViajeEditar);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarViaje([Bind] AP_Viaje c)
>>>>>>> Stashed changes
        {
            ViewBag.Error = "";
            ViewBag.ViajeEliminado = false;
            IViaje Viaje = new IViaje(FsvrConn);
<<<<<<< Updated upstream
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            var ViajeEditar = Viaje.GetViajeCompleto(fEmpresaId, (int)ViajeId).DefaultIfEmpty().FirstOrDefault();


            return PartialView(ViajeEditar);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarViaje([Bind] AP_Viaje c)
        {
            ViewBag.Error = "";
            ViewBag.ViajeEliminado = false;
            IViaje Viaje = new IViaje(FsvrConn);
=======
>>>>>>> Stashed changes
            ISimulacion Simulacion = new ISimulacion(FsvrConn);
            ViewBag.ErrorRecomendacion = "";
            ViewBag.ErrorFinalizacion = "";
            ViewBag.ErrorEditar = "";
            ViewBag.ViajeCreado = false;
            int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();

            try
            {

                c.EmpresaId = fEmpresaId;
                c.Estado = "IN"; //Creado


                c.FechaMod = DateTime.Now;
                c.FechaFinalizacion = null;
                c.FechaInicioViaje = null;
                Viaje.ActualizarViaje(c);
                dbTran.Commit();

                var lstViajees = Viaje.GetViajes(fEmpresaId).DefaultIfEmpty();



                return View("~/Areas/Viaje/Views/Viaje/Index.cshtml", lstViajees.ToList());

            }
            catch (Exception ex)
            {
                dbTran.Rollback();
                ViewBag.Error = "Error al crear" + ex.InnerException + "<hr />MENSAJE--> " + ex.Message;
                ViewBag.ListUsuarios = Viaje.GetViajes(fEmpresaId).DefaultIfEmpty().OrderByDescending(c => c.ViajeId);

                var lstViajees = Viaje.GetViajes(fEmpresaId).DefaultIfEmpty();
                ViewBag.ErrorEditar = "HA OCURRIDO UN ERROR AL EDITAR EL VIAJE, POR FAVOR INTETE DE NUEVO. " + ViewBag.Error;

                return View("~/Areas/Viaje/Views/Viaje/Index.cshtml", lstViajees.ToList());
            }


        }



        public IActionResult IniciarViaje(int? ViajeId)
        {
            ViewBag.ErrorRecomendacion = "";
            ViewBag.ErrorFinalizacion = "";
            ViewBag.ErrorEditar = "";
            IList lstViaje = CargarDatosIndex();


            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            if (ViajeId != null && ViajeId > 0)
            {
                try
                {
                    IViaje Viaje = new IViaje(FsvrConn);
                    IConductor Conductor = new IConductor(FsvrConn);
                    IVehiculo Vehiculo = new IVehiculo(FsvrConn);
                    ICertificadoVehiculo CertificadoVehiculo = new ICertificadoVehiculo(FsvrConn);
                    ICertificadoConductor CertificadoConductor = new ICertificadoConductor(FsvrConn);
                    IModeloRecomendacion ModeloRecomendacion = new IModeloRecomendacion(FsvrConn);

                    var ViajeSeleccionado = Viaje.GetViaje(fEmpresaId, (int)ViajeId).DefaultIfEmpty().FirstOrDefault();

                    ViajeSeleccionado.FechaInicioViaje = DateTime.Now;
                    //var ModeloRecomendacionActual = ModeloRecomendacion.GetModeloRecomendacions(fEmpresaId).DefaultIfEmpty().Where(h => h.Estado == "AC").FirstOrDefault();
                    //ViajeSeleccionado.ModeloRecomendacionId = ModeloRecomendacionActual.ModeloRecomendacionId;


                    //Datos para filtras según características del viaje
                    // Tipo Mercancía

                    var ListaVehiculos = Vehiculo.GetVehiculosCompleto(fEmpresaId).DefaultIfEmpty()
                        .Where(h => h.Estado == "AC").DefaultIfEmpty().AsEnumerable();
                    var ListaVehiculosFinal = new Collection<DT_Vehiculo>().DefaultIfEmpty().AsEnumerable();

                    if (ListaVehiculos.FirstOrDefault().VehiculoId > 0) // Verifica Si Hay Vehículos Activos
                    {
                        /////
                        /////////////////////// Vehículo
                        /////

                        var ListaCertificadosVehiculos = CertificadoVehiculo.GetCertificadoVehiculosCompleto(fEmpresaId).DefaultIfEmpty()
                                                        .Where(h => h.Estado == "AC");

                        // Naturaleza Carga == DT_Vehiculo
                        ListaVehiculos = ListaVehiculos.Where(h => h.NaturalezaCarga == ViajeSeleccionado.NaturalezaCarga);

                        if (ListaVehiculos.FirstOrDefault().VehiculoId > 0) // Vehiculos con la Natturaleza Deseada
                        {
                            // Peso, volumen
                            ListaVehiculos = ListaVehiculos.Where(h => h.Capacidad >= ViajeSeleccionado.Contenido && h.Capacidad >= ViajeSeleccionado.Volumen);
                            if (ListaVehiculos.FirstOrDefault().VehiculoId > 0) // Vehiculos con el Peso y Volumen Necesario
                            {

                                // Departamento
                                ListaVehiculos = ListaVehiculos.Where(h => h.CodDepartamentoBase == ViajeSeleccionado.CodDepartamentoOrigen);

                                if (ListaVehiculos.FirstOrDefault().VehiculoId > 0) // Vehiculos en el Departamento
                                {
                                    ////// Líquido, Comida, Inflamable, Normal(Resto)
                                    ////// 
                                    //////     7  ,    8   ,   9      ,     4   -> CertificadoId - Vehiculo
                                    ////// 009880 , 003105 , 009990   ,         -> CodMercancia
<<<<<<< Updated upstream
                                    ///
=======

>>>>>>> Stashed changes
                                    ListaCertificadosVehiculos = ViajeSeleccionado.CodMercancia switch
                                    {
                                        "009880" => ListaCertificadosVehiculos.Where(h => h.DT_Certificado.CertificadoId == 7).DefaultIfEmpty(),
                                        "003105" => ListaCertificadosVehiculos.Where(h => h.DT_Certificado.CertificadoId == 8).DefaultIfEmpty(),
                                        "009990" => ListaCertificadosVehiculos.Where(h => h.DT_Certificado.CertificadoId == 9).DefaultIfEmpty(),
                                        _ => ListaCertificadosVehiculos.Where(h => h.DT_Certificado.CertificadoId == 4).DefaultIfEmpty(),
                                    };
                                    ListaCertificadosVehiculos = ListaCertificadosVehiculos.Distinct().DefaultIfEmpty(); // Vehiculos Posibles

                                    if (ListaCertificadosVehiculos.FirstOrDefault().VehiculoId > 0) // Vehiculos con certificados necesarios
                                    {
                                        foreach (var item in ListaCertificadosVehiculos)
                                        {
                                            var Entontrado = ListaVehiculos.Where(h => h.VehiculoId == item.VehiculoId).DefaultIfEmpty().FirstOrDefault();

                                            if (Entontrado.VehiculoId > 0)
                                                ListaVehiculosFinal = ListaVehiculosFinal.Append(Entontrado);

                                        }


                                        if (ListaVehiculosFinal.FirstOrDefault().VehiculoId > 0) // Hay Vehículos Finales
                                        {
                                            //////// Fin Vehículo


                                            ///////////////////////////////////////////// Conductor [2 y 1 OBLIGATORIOS]

                                            var ListaCertificadosConductor = CertificadoConductor.GetCertificadoConductoresCompleto(fEmpresaId).DefaultIfEmpty()
                                                    .Where(h => h.Estado == "AC");


                                            var ListaConductores = Conductor.GetConductores(fEmpresaId).DefaultIfEmpty()
                                                .Where(h => h.Estado == "AC");

                                            // Departamento
                                            ListaConductores = ListaConductores.Where(h => h.CodDepartamentoBase == ViajeSeleccionado.CodDepartamentoOrigen);

                                            if (ListaConductores.FirstOrDefault().ConductorId > 0) // Conductores en el Departamento
                                            {
                                                ////// Líquido, Comida, Inflamable , Normal(Resto)
                                                ////// 
                                                //////     10  ,    11   ,   12    , 1 y 2   -> CertificadoId - Conductor
                                                ////// 009880 , 003105   , 009990  ,         -> CodMercancia
                                                ListaCertificadosConductor = ViajeSeleccionado.CodMercancia switch
                                                {
                                                    "009880" => ListaCertificadosConductor.Where(h => h.DT_Certificado.CertificadoId == 10).DefaultIfEmpty(),
                                                    "003105" => ListaCertificadosConductor.Where(h => h.DT_Certificado.CertificadoId == 11).DefaultIfEmpty(),
                                                    "009990" => ListaCertificadosConductor.Where(h => h.DT_Certificado.CertificadoId == 9).DefaultIfEmpty(),
                                                    _ => ListaCertificadosConductor
                                                    .Where(h => h.DT_Certificado.CertificadoId == 1 || h.DT_Certificado.CertificadoId == 2).DefaultIfEmpty(),
                                                };
                                                ListaCertificadosConductor = ListaCertificadosConductor.Distinct().DefaultIfEmpty(); // Conductores Posibles

                                                if (ListaCertificadosConductor.FirstOrDefault().ConductorId > 0) // Conductores con certificados necesarios
                                                {
                                                    var ListaConductoresFinal = new Collection<DT_Conductor>().DefaultIfEmpty().AsEnumerable();


                                                    foreach (var item in ListaCertificadosConductor)
                                                    {
                                                        var Entontrado = ListaConductores.Where(h => h.ConductorId == item.ConductorId).DefaultIfEmpty().FirstOrDefault();

                                                        if (Entontrado.ConductorId > 0)
                                                            ListaConductoresFinal = ListaConductoresFinal.Append(Entontrado);

                                                    }



                                                    if (ListaConductoresFinal.FirstOrDefault().ConductorId > 0) // Hay Conductores Finales
                                                    {

                                                        // Generación de Score con modelo de recomendación

                                                        //Final del proceso de Recomendación
                                                        ViewBag.lstVehiculosFinales = ListaVehiculosFinal.ToList().DefaultIfEmpty();
                                                        //.Select(h => new { id = h.VehiculoId, text = h.MarcaEmpresa + " / " + h.Modelo + " ( PLACA: " + h.CodPlacas + ")" }).ToList().DefaultIfEmpty();


                                                        ViewBag.lstConductoresFinales = ListaConductoresFinal.ToList().DefaultIfEmpty();
                                                        //.Select(h => new { id = h.ConductorId, text = h.NombreConductor + " / " + h.CodConductor + " ( CC: " + h.Documento + ")" }).ToList().DefaultIfEmpty();

                                                        return PartialView("~/Areas/Viaje/Views/Viaje/IniciarViaje.cshtml", ViajeSeleccionado);

                                                    }
                                                    else
                                                        ViewBag.ErrorRecomendacion = "No hay conductores activos y disponibles para  realizar el viaje respués de realizas las validaciones del viaje y conductor";
                                                }
                                                else
                                                    ViewBag.ErrorRecomendacion = "No hay conductores activos y disponibles con los certificado necesarios para el viaje";
                                            }

                                            else
                                                ViewBag.ErrorRecomendacion = "No hay conductores activos y disponibles en el Departamento " + ViajeSeleccionado.CodDepartamentoOrigen ?? "" + "para realizar el viaje";
                                        }
                                        else
                                            ViewBag.ErrorRecomendacion = "No hay vehículos activos y disponibles para realizar el viaje respués de realizas las validaciones del viaje y vehículos";
                                    }
                                    else
                                        ViewBag.ErrorRecomendacion = "No hay vehículos activos y disponibles con los certificado necesarios para el viaje";
                                }
                                else
                                    ViewBag.ErrorRecomendacion = "No hay vehículos activos y disponibles en el Departamento " + ViajeSeleccionado.CodDepartamentoOrigen ?? "" + "para realizar el viaje";
                            }
                            else
                                ViewBag.ErrorRecomendacion = "No hay vehículos activos y disponibles con el Peso: " + ViajeSeleccionado.Contenido + " y Volumen: " + ViajeSeleccionado.Volumen + "para realizar el viaje";
                        }
                        else
                            ViewBag.ErrorRecomendacion = "No hay vehículos activos y disponibles con la Naturaleza " + ViajeSeleccionado.NaturalezaCarga ?? "" + "para realizar el viaje";

                    }
                    else
                        ViewBag.ErrorRecomendacion = "No hay vehículos activos y disponibles para realizar el viaje";
                }

                catch (Exception ex)
                {

                    ViewBag.ErrorRecomendacion = "Error Inesperado en la Recomendación: " + ex.Message.ToString();



                    return View("~/Areas/Viaje/Views/Viaje/Index.cshtml", lstViaje);
                }
            }
            else
                ViewBag.ErrorRecomendacion = "No se puede iniciar la recomendación del viaje, no se ha encontrado el ViajeId";


            return View("~/Areas/Viaje/Views/Viaje/Index.cshtml", lstViaje);
        }

        public IActionResult IniciarViaje([Bind] AP_Viaje c)
        {
            ViewBag.Error = "";
            using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();
            ViewBag.ErrorFinalizacion = "";
            ViewBag.ErrorEditar = "";
            ViewBag.ErrorRecomendacion = "";

            IEnumerable lstViaje = CargarDatosIndex();

            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            try
            {
                IViaje Viaje = new IViaje(FsvrConn);


                IConductor Conductor = new IConductor(FsvrConn);
                IVehiculo Vehiculo = new IVehiculo(FsvrConn);

                var ConductorActualizar = Conductor.GetConductor(fEmpresaId, (int)(c.ConductorId ?? 0)).DefaultIfEmpty().FirstOrDefault();
                ConductorActualizar.Estado = "OC";
                Conductor.ActualizarConductor(ConductorActualizar);

                var VehiculoActualizar = Vehiculo.GetVehiculo(fEmpresaId, (int)(c.VehiculoId ?? 0)).DefaultIfEmpty().FirstOrDefault();
                VehiculoActualizar.Estado = "OC";
                Vehiculo.ActualizarVehiculo(VehiculoActualizar);

                c.Estado = "EP";
                Viaje.ActualizarViaje(c);
                dbTran.Commit();

                ViewBag.ErrorRecomendacion = "CREADO";

<<<<<<< Updated upstream

            }
            catch (Exception ex)
            {
                dbTran.Rollback();

                ViewBag.ErrorRecomendacion = "Error Inesperado en la Recomendación: " + ex.Message.ToString();


                return View("~/Areas/Viaje/Views/Viaje/Index.cshtml", lstViaje);


            }
=======

            }
            catch (Exception ex)
            {
                dbTran.Rollback();

                ViewBag.ErrorRecomendacion = "Error Inesperado en la Recomendación: " + ex.Message.ToString();


                return View("~/Areas/Viaje/Views/Viaje/Index.cshtml", lstViaje);


            }
>>>>>>> Stashed changes


            ViewBag.ErrorRecomendacion = "";


            return View("~/Areas/Viaje/Views/Viaje/Index.cshtml", lstViaje);
        }

        public IActionResult FinalizarViaje(int? ViajeId)
        {
            ViewBag.ErrorRecomendacion = "";
            ViewBag.ErrorFinalizacion = "";
            ViewBag.ErrorEditar = "";


            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            if (ViajeId != null && ViajeId > 0)
            {
                IViaje Viaje = new IViaje(FsvrConn);
                var ViajeFinalizar = Viaje.GetViaje(fEmpresaId, (int)ViajeId).DefaultIfEmpty().FirstOrDefault();

                return View("~/Areas/Viaje/Views/Viaje/FinalizarViaje.cshtml", ViajeFinalizar);
            }
            else
                ViewBag.ErrorFinalizacion = "No se puede finalizar el viaje, no se ha encontrado el ViajeId";

            IList lstViaje = CargarDatosIndex();

            return View("~/Areas/Viaje/Views/Viaje/Index.cshtml", lstViaje);
        }

        public IActionResult FinalizarViaje([Bind] AP_Viaje c)
        {
            ViewBag.Error = "";
            using IDbContextTransaction dbTran = FsvrConn.Database.BeginTransaction();
            ViewBag.ErrorRecomendacion = "";
            ViewBag.ErrorFinalizacion = "";
            ViewBag.ErrorEditar = "";
            IEnumerable lstViaje = CargarDatosIndex();
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            try
            {
                IViaje Viaje = new IViaje(FsvrConn);
                ISimulacion Simulacion = new ISimulacion(FsvrConn);
                c.FechaFinalizacion = DateTime.Now;
                c.TiempoTotalViaje = ((int?)(DateTime.Now - (DateTime)c.FechaInicioViaje).TotalMinutes) ?? 0;
                c.Estado = "FN";
                Viaje.ActualizarViaje(c);

                IConductor Conductor = new IConductor(FsvrConn);
                IVehiculo Vehiculo = new IVehiculo(FsvrConn);

                var ConductorActualizar = Conductor.GetConductor(fEmpresaId, (int)(c.ConductorId ?? 0)).DefaultIfEmpty().FirstOrDefault();
                ConductorActualizar.Estado = "AC";
                Conductor.ActualizarConductor(ConductorActualizar);

                var VehiculoActualizar = Vehiculo.GetVehiculo(fEmpresaId, (int)(c.VehiculoId ?? 0)).DefaultIfEmpty().FirstOrDefault();
                VehiculoActualizar.Estado = "AC";
                Vehiculo.ActualizarVehiculo(VehiculoActualizar);

                AgrgarDatosSimulacion(c);

                dbTran.Commit();
                ViewBag.ErrorFinalizacion = "CREADO";
            }
            catch (Exception ex)
            {
                dbTran.Rollback();

                ViewBag.ErrorFinalizacion = "Error Inesperado en la Finalización: " + ex.Message.ToString();
                return View("~/Areas/Viaje/Views/Viaje/Index.cshtml", lstViaje);
            }


            return View("~/Areas/Viaje/Views/Viaje/Index.cshtml", lstViaje);
        }
        public void AgrgarDatosSimulacion(AP_Viaje c)
        {
<<<<<<< Updated upstream

            try
            {
=======
>>>>>>> Stashed changes

            try
            {

                ISimulacion Simulacion = new ISimulacion(FsvrConn);
                int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
                var lstSimulaciones = Simulacion.GetSimulacions(fEmpresaId).DefaultIfEmpty().OrderByDescending(h => h.SimulacionId).FirstOrDefault();
                int SimulacionIdNuevo;

<<<<<<< Updated upstream
=======
                ISimulacion Simulacion = new ISimulacion(FsvrConn);
                int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
                var lstSimulaciones = Simulacion.GetSimulacions(fEmpresaId).DefaultIfEmpty().OrderByDescending(h => h.SimulacionId).FirstOrDefault();
                int SimulacionIdNuevo;

>>>>>>> Stashed changes
                if (lstSimulaciones != null)
                {
                    var SimulacionIdAnterior = lstSimulaciones.SimulacionId;
                    if (SimulacionIdAnterior > 0)
                        SimulacionIdNuevo = SimulacionIdAnterior + 1;
                    else
                        SimulacionIdNuevo = 1;
                }
                else
                    SimulacionIdNuevo = 1;


                var NuevaSimulacion = new AP_Simulacion
                {
                    EmpresaId = fEmpresaId,
                    Estado = "AC",
                    FechaIng = DateTime.Now,
                    FechaMod = DateTime.Now,
                    SimulacionId = SimulacionIdNuevo,
                    CalificacionRecomendacion = c.Satisfaccion,
                    CodDepartamentoDestino = c.CodDepartamentoDestino,
                    CodDepartamentoOrigen = c.CodDepartamentoOrigen,
                    CodMercancia = c.CodMercancia,
                    CodMunicipioDestino = c.CodMunicipioDestino,
                    CodMunicipioOrigen = c.CodMunicipioOrigen,
                    CodOperacionTransporte = c.CodOperacionTransporte,
                    AP_Recomendacion = null,
                    CodViaje = c.CodViaje,
                    ComentarioSimulacion = c.Descripcion,
                    Descripcion = c.Descripcion,
                    Contenido = c.Contenido,
                    CostoPeajes = c.CostoPeajes,
                    FechaServicio = c.FechaServicio,
                    Kilometros = c.Kilometros,
                    ModeloRecomendacionId = c.ModeloRecomendacionId,
                    NaturalezaCarga = c.NaturalezaCarga,
                    NumeroPeajes = c.NumeroPeajes,
                    PorcentajeRutaPrimaria = c.PorcentajeRutaPrimaria,
                    PorcentajeRutaSecundaria = c.PorcentajeRutaSecundaria,
                    PorcentajeRutaTerciaria = c.PorcentajeRutaTerciaria,
                    SimulacionElegida = "S",
                    TerceroId = c.TerceroId,
                    UsuarioId = c.UsuarioId,
                    ValorCotizado = c.ValorCotizado,
                    ViajeId = c.ViajeId,
                    Volumen = c.Volumen

                };

                IRecomendacion Recomendacion = new IRecomendacion(FsvrConn);
                var lstRecomendaciones = Recomendacion.GetRecomendacions(fEmpresaId).DefaultIfEmpty().OrderByDescending(h => h.RecomendacionId).FirstOrDefault();
                int RecomendacionNuevo;

                if (lstRecomendaciones != null)
                {
                    var RecomendacionIdAnterior = lstSimulaciones.SimulacionId;
                    if (RecomendacionIdAnterior > 0)
                        RecomendacionNuevo = RecomendacionIdAnterior + 1;
                    else
                        RecomendacionNuevo = 1;
                }
                else
                    RecomendacionNuevo = 1;


                var NuevaReco = new AP_Recomendacion
                {
                    ConductorId = c.ConductorId,
                    ViajeId = c.ViajeId,
                    UsuarioId = c.UsuarioId,
                    TerceroId = c.TerceroId,
                    EmpresaId = c.EmpresaId,
                    Estado = "AC",
                    FechaIng = DateTime.Now,
                    FechaMod = DateTime.Now,
                    ModeloRecomendacionId = c.ModeloRecomendacionId,
                    RecomendacionId = RecomendacionNuevo,
                    SimulacionId = SimulacionIdNuevo,
                    TipoRecomendacion = "A",
                    VehiculoId = c.VehiculoId,
                };

                Simulacion.AgregarSimulacion(NuevaSimulacion);
                Recomendacion.ActualizarRecomendacion(NuevaReco);


            }
            catch (Exception ex)
            {

                ViewBag.ErrorFinalizacion = "Error Inesperado en la Creaíón de la Simulación y Recomendaciones: " + ex.Message.ToString();
            }

        }

        public IActionResult EliminarViaje(int? ViajeId)
        {
            ViewBag.Error = "";
            IViaje Viaje = new IViaje(FsvrConn);
            ViewBag.ViajeCreado = false;
            ViewBag.ViajeEliminado = false;
            ViewBag.ViajeIniciado = false;
            ViewBag.ErrorRecomendacion = "";
            ViewBag.ErrorFinalizacion = "";
            ViewBag.ErrorEditar = "";


            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;

            var ViajeEliminar = Viaje.GetViaje(fEmpresaId, (int)ViajeId).DefaultIfEmpty().FirstOrDefault();
            if (ViajeEliminar != null)
            {
                Viaje.EliminarViaje(ViajeEliminar);
                ViewBag.ViajeEliminado = true;
            }
            else
            {
                ViewBag.Error = "No se puede eliminar el Viaje, ya se ha eliminado.";

            }

            var lstViajees = Viaje.GetViajes(fEmpresaId).DefaultIfEmpty();

            return View("~/Areas/Viaje/Views/Viaje/Index.cshtml", lstViajees.ToList());
        }

        public IList CargarDatosIndex()
        {
            int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
<<<<<<< Updated upstream

            IViaje Viaje = new IViaje(FsvrConn);
            var lstViaje = Viaje.GetViajes(fEmpresaId).DefaultIfEmpty().ToList().DefaultIfEmpty().ToList();
            ViewBag.Error = "";
            ViewBag.ViajeCreado = false;
            ViewBag.ViajeEliminado = false;
            ViewBag.ViajeIniciado = false;
            ViewBag.ErrorRecomendacion = "";

=======

            IViaje Viaje = new IViaje(FsvrConn);
            var lstViaje = Viaje.GetViajes(fEmpresaId).DefaultIfEmpty().ToList().DefaultIfEmpty().ToList();
            ViewBag.Error = "";
            ViewBag.ViajeCreado = false;
            ViewBag.ViajeEliminado = false;
            ViewBag.ErrorFinalizacion = "";

            ViewBag.ViajeIniciado = false;
            ViewBag.ErrorRecomendacion = "";

>>>>>>> Stashed changes
            return lstViaje;
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

        public ActionResult GetTercerosFind(string q)
        {
            var fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            string estadoPermitido = "AC";

            ITercero Tercero = new ITercero(FsvrConn);
            var results = Tercero.GetTerceroLike(fEmpresaId, q).Where(h => h.Estado.Equals(estadoPermitido)).
                          Select(h => new { id = h.TerceroId, text = h.NumeroNit + " - " + h.NombreTer }).ToList().Take(15);
            return Json(new { results });
        }

    }
}