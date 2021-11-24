//Función que crea la prescripción del medicamento
//Función que crea la prescripción del medicamento
function onchangeMedicamento() {

    cargardatosproducto();

    var Cantidad = document.getElementById("Cantidad").value     // Cantidad del producto, si por jornada, se multiplica
                                                                 // por la sumatoria de las jornadas se multiplica por 1
                                                                 // 1 si DuranteTiempo es Dias, 7 si es semana y 30 si es mes,
                                                                 // si no es por jornada se multiplica tratamientohoras y tiempo tratamieno
    var DuranteTiempo = document.getElementById("DuranteTiempo").value;   // Tienpo en el que se mide el tratamiento (Días, Semanas, Meses)
    var TiempoTratamiento = document.getElementById("TratamientoDias").value;    
    var ViaAdministracion = document.getElementById("ViaAdministracion").value; 
    var TratamientoTexto = "", TextoManana = "", TextoTarde = "", TextoNoche = "", DuranteTiempo_ = "";
    var TratamientoHoras = document.getElementById("TratamientoHoras").value;
    var VecesAlDia = 0;
    var CantidadManana = 0, CantidadTarde = 0, CantidadNoche = 0;
    var Frecuencia = document.getElementById("Frecuencia").value;

    if (TiempoTratamiento == 1) {
        if (DuranteTiempo == "Días") { DuranteTiempo_ = "Día"; }
        if (DuranteTiempo == "Semanas") { DuranteTiempo_ = "Semana"; }
        if (DuranteTiempo == "Meses") { DuranteTiempo_ = "Mes"; }
    }
    else {
        DuranteTiempo_ = DuranteTiempo;
    }

    var TotalMedicamento = 0;
    var Verbo = "APLICAR ";
    if (ViaAdministracion == "Inhalatoria") { var Verbo = "INHALAR "; }
    if (ViaAdministracion == "VO: Vía oral") { var Verbo = "TOMAR "; }


    if (document.getElementById("DosisporJornada").checked == true)    // Se valida si es dosis por jornada
    {
        Cantidad = 0;    // Cantidad es la sumatoria de jornadas   (Mañana, Tardes, Noche)
        TratamientoHoras = 1;
        if (document.getElementById("CantManana").value > 0) {
            CantidadManana = document.getElementById("CantManana").value;
            Cantidad = Cantidad + +CantidadManana;
            VecesAlDia = VecesAlDia + 1;
            TextoManana = " " + CantidadManana + " en la mañana, ";
        }

        if (document.getElementById("CantTarde").value > 0) {
            CantidadTarde = document.getElementById("CantTarde").value;
            Cantidad = Cantidad + +CantidadTarde;
            VecesAlDia = VecesAlDia + 1;
            TextoTarde = " " + CantidadTarde + " en la tarde, ";
        }

        if (document.getElementById("CantNoche").value > 0) {
            CantidadNoche = document.getElementById("CantNoche").value;
            Cantidad = Cantidad + +CantidadNoche;
            VecesAlDia = VecesAlDia + 1;
            TextoNoche = " " + CantidadNoche + " en la noche, ";
        }

        document.getElementById("Cantidad").value = Cantidad;

        // La variable Cantidad esta guardando el total del producto enviado por día

        if (DuranteTiempo == "Días") { TotalMedicamento = Cantidad * (TiempoTratamiento); }
        if (DuranteTiempo == "Semanas") { TotalMedicamento = Cantidad * (TiempoTratamiento * 7); }
        if (DuranteTiempo == "Meses") { TotalMedicamento = Cantidad * (TiempoTratamiento * 30); }

        TratamientoTexto = Verbo + "," + TextoManana + TextoTarde + TextoNoche + " por " + TiempoTratamiento + " " + DuranteTiempo_ +
                           "         Cantidad Total = " + TotalMedicamento;
    }
    else {    
        TotalMedicamento = 0;

        if (Frecuencia == 'Horas')
        {            
            if (DuranteTiempo == "Días") { TotalMedicamento = Cantidad * (Math.round(24 / TratamientoHoras)) * (TiempoTratamiento); }
            if (DuranteTiempo == "Semanas") { TotalMedicamento = Cantidad * (Math.round(24 / TratamientoHoras)) * (TiempoTratamiento * 7); }
            if (DuranteTiempo == "Meses") { TotalMedicamento = Cantidad * (Math.round(24 / TratamientoHoras)) * (TiempoTratamiento * 30); }
        }
        if (Frecuencia == 'Dias') {
            if (DuranteTiempo == "Días") { TotalMedicamento = Cantidad * ((TiempoTratamiento) / TratamientoHoras); }
            if (DuranteTiempo == "Semanas") { TotalMedicamento = Cantidad * ((TiempoTratamiento * 7) / TratamientoHoras); }
            if (DuranteTiempo == "Meses") { TotalMedicamento = Cantidad * ((TiempoTratamiento * 30) / TratamientoHoras); }
        }

        TratamientoTexto = Verbo + Cantidad + " CADA " + TratamientoHoras + " " + Frecuencia + ", DURANTE " + TiempoTratamiento + " " + DuranteTiempo_.toUpperCase()
            + "         Cantidad Total = " + Math.ceil(TotalMedicamento);


    }

    document.getElementById("CantidadTotal").value = TotalMedicamento;

    //Si maneja cantidad por unidad de empque - ejemplo : Insulina
    if (document.getElementById("CantidadUniEmpaque").value > 0)
    {
        var Cantidaduniempaque = document.getElementById("CantidadUniEmpaque").value;
        var CantTotalMedicamento = TotalMedicamento;   /*Se crea para guardar el valor original del total del medicamento*/

        TotalMedicamento = Math.trunc(TotalMedicamento / Cantidaduniempaque);

        if ((CantTotalMedicamento % Cantidaduniempaque) > 0) { TotalMedicamento = TotalMedicamento + 1; }

        TratamientoTexto = Verbo + Cantidad + " CADA " + TratamientoHoras + " HORA(S), DURANTE " + TiempoTratamiento + " " + DuranteTiempo.toUpperCase() +
                           "         Cantidad Total = " + TotalMedicamento;
        document.getElementById("CantidadTotal").value = Math.ceil(TotalMedicamento);
    }

    document.getElementById("Descripcion").value = TratamientoTexto;

};

//------------------------------------------------------------------------------------------------------------------------------------

function CalculoIMC() {
    var Peso = document.getElementById("Peso").value;
    var Talla = document.getElementById("Talla").value;

    if (Talla > 0) {
        Talla = Talla / 100;
        document.getElementById("IMC").value = (Peso / (Talla * Talla)).toFixed(2);
    }

};

//------------------------------------------------------------------------------------------------------------------------------------
function OnChangeTipoDiabetes() {

    if (document.getElementById("TipoDiabetes").value == 'Otras') {
        $("#OtroTipoDiabetes").prop("disabled", false);
        document.getElementById('OtroTipoDiabetes').required = true;
    }
    else {
        $("#OtroTipoDiabetes").prop("disabled", true);
        document.getElementById('OtroTipoDiabetes').required = false;
    }
}

//------------------------------------------------------------------------------------------------------------------------------------
function OnChangeHospitalizacion() {
    if (document.getElementById("Hospitalizaciones").checked == true) { $("#FechaHospitalizacion").prop("disabled", false); $("#MotivoHospitalizacion").prop("disabled", false); }
    else { $("#FechaHospitalizacion").prop("disabled", true); $("#MotivoHospitalizacion").prop("disabled", true); }
}

//------------------------------------------------------------------------------------------------------------------------------------

function CargarDiagnosticos(url) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        async: false,
        success: function (result) {
            if (document.getElementById("DiagPrincipal").value == "") {
                $('#DiagPrincipal').html('');
                $("#DiagPrincipal").append('<option value="">Seleccione un Diagnóstico</option>');
                $.each(result, function (i, data) {
                    $("#DiagPrincipal").append('<option value="' + data.diagnostico + '">' + data.nombreDiag + '</option>');
                });
            };

            if (document.getElementById("DiagRelacionado1").value == "") {
                $('#DiagRelacionado1').html('');
                $("#DiagRelacionado1").append('<option value="">Seleccione un Diagnóstico</option>');
                $.each(result, function (i, data) {
                    $("#DiagRelacionado1").append('<option value="' + data.diagnostico + '">' + data.nombreDiag + '</option>');
                });

            };

            if (document.getElementById("DiagRelacionado2").value == "") {
                $('#DiagRelacionado2').html('');
                $("#DiagRelacionado2").append('<option value="">Seleccione un Diagnóstico</option>');
                $.each(result, function (i, data) {
                    $("#DiagRelacionado2").append('<option value="' + data.diagnostico + '">' + data.nombreDiag + '</option>');
                });
            };

            if (document.getElementById("DiagRelacionado3").value == "") {
                $('#DiagRelacionado3').html('');
                $("#DiagRelacionado3").append('<option value="">Seleccione un Diagnóstico</option>');
                $.each(result, function (i, data) {
                    $("#DiagRelacionado3").append('<option value="' + data.diagnostico + '">' + data.nombreDiag + '</option>');
                });
            };
        },
        error: function (error) { alert('No fue posible cargar los datos de diagnósticos'); }
    });
}

//-------------------------------------------------------------------------------------------------------------------
function onchangeDosisporJornada() {
    
    if (document.getElementById("DosisporJornada").checked == true) {         
        document.getElementById("TratamientoNoJornada").style.display = "none";
        document.getElementById("TratamientoCantidad").style.display = "none";

        document.getElementById("MediCantidadManana").style.display = "inline";
        document.getElementById("MediCantidadTarde").style.display = "inline";
        document.getElementById("MediCantidadNoche").style.display = "inline"; 

        document.getElementById("FrecuenciaNoJornada").style.display = "none";
    }
    else {
        document.getElementById("TratamientoNoJornada").style.display = "inline";
        document.getElementById("TratamientoCantidad").style.display = "inline";

        document.getElementById("MediCantidadManana").style.display = "none";
        document.getElementById("MediCantidadTarde").style.display = "none";
        document.getElementById("MediCantidadNoche").style.display = "none";       

        document.getElementById("FrecuenciaNoJornada").style.display = "none";

        document.getElementById("FrecuenciaNoJornada").style.display = "inline";

        if (document.getElementById("IndUnidosis").checked == true) {
            $('#Frecuencia').html('');
            $("#Frecuencia").append('<option value="Horas">' + "Horas" + '</option>');
        }
        else {
            $('#Frecuencia').html('');
            $("#Frecuencia").append('<option value="Horas">' + "Horas" + '</option>');
            $("#Frecuencia").append('<option value="Dias">' + "Días" + '</option>');
        }

    }

    document.getElementById("Cantidad").value = 0;
    document.getElementById("CantManana").value = 0;
    document.getElementById("CantTarde").value = 0;
    document.getElementById("CantNoche").value = 0;
    document.getElementById("TratamientoDias").value = 1;
    document.getElementById("DuranteTiempo").value = 'Días';

    document.getElementById("CantidadTotal").value = 0;
    document.getElementById("Descripcion").value = '';

}

//-------------------------------------------------------------------------------------------------------------------
function onchangeTipoIngId() {

    if (document.getElementById("TipoIngId").value == 'H') {
        document.getElementById('lbHospitalizacion').style.visibility = 'visible';
        document.getElementById('Hospitalizacion').style.visibility = 'visible';
        //document.getElementById('Hospitalizacion').required = true;
    }
    else {
        document.getElementById('lbHospitalizacion').style.visibility = 'hidden';
        document.getElementById('Hospitalizacion').style.visibility = 'hidden';
        //document.getElementById('Hospitalizacion').required = false;
    }
}

//-------------------------------------------------------------------------------------------------------------------

function cargardatosdelpaciente() {

    var HistoriaId = document.getElementById("HistoriaId").value;
    url = "/Salud/Citas/CargarDatosdelPacienteJson?HistoriaId=" + HistoriaId;

    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        async: false,
        success: function (result) {

            $.each(result, function (i, data) {
                document.getElementById("_DatosPaciente").value = data.tipodocumento + " - " + data.numeronit + " - " + data.nombreben + " - " +
                    "Edad : " + data.edad + "   " + "Entidad : " + data.nombrefondo + "   " + "Regimen : " + data.nombreregimen +
                    "   " + "Tipo : " + data.tipo + "   " + "Categoria : " + data.categoria + " - " + "Teléfono : " + data.telefono + " - " +
                    "Celular : " + data.celular + " - " + "Acompañante : " + data.acompanante;
            });
        }
    });
}

//-------------------------------------------------------------------------------------------------------------------

function onchangeConfirmado() {
    if (document.getElementById("Estado").value == 'CO') {
        document.getElementById('lbQuienConfirma').style.visibility = 'visible';
        document.getElementById('QuienConfirma').style.visibility = 'visible';
        document.getElementById('QuienConfirma').required = true;
    }
    else {
        document.getElementById('lbQuienConfirma').style.visibility = 'hidden';
        document.getElementById('QuienConfirma').style.visibility = 'hidden';
        document.getElementById('QuienConfirma').required = false;
    }
}

//-------------------------------------------------------------------------------------------------------------------

function onchagedatoshistoria() {

    var HistoriaId = document.getElementById("HistoriaId").value;
    url = "/Salud/Citas/CargarDatosdelPacienteJson?HistoriaId=" + HistoriaId;

    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        async: false,
        success: function (result) {

            $.each(result, function (i, data) {

                document.getElementById("Regimen").value = data.regimen;
                document.getElementById("UbicacionId").value = data.ubicacion;
                document.getElementById("EntidadId").value = data.entidadid;
                document.getElementById("Acompanante").value = data.acompanante;
                document.getElementById("Direccion").value = data.direccion;
                document.getElementById("Telefono").value = data.telefono;
            });
        },
        error: function (error) { alert('No fue posible cargar los datos del paciente'); }
    });
}

//-------------------------------------------------------------------------------------------------------------------

function cargardatosproducto() {

    var ProcedimId = document.getElementById("ProcedimId").value;
    url = "/Salud/Ingresos/CargarDatosProductoJson?ProcedimId=" + ProcedimId;

    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        async: false,
        success: function (result) {

            $.each(result, function (i, data) {
                document.getElementById("Recomendacion").value = data.recomendacion;
                document.getElementById("CantidadUniEmpaque").value = data.cantidaduniempaque;
            });
        },
        error: function (error) { alert('No fue posible cargar datos del producto'); }
    });
}

//-------------------------------------------------------------------------------------------------------------------
function DatosGestionTelefonica() {

    var IngresoId = document.getElementById("IngresoId").value;
    url = "/Salud/Ingresos/DatosTelefonicosJson?IngresoId=" + IngresoId;

    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        async: false,
        success: function (result) {

            $.each(result, function (i, data) {
                document.getElementById("_DatosTelefonicos").value = data.telefono;
            });
        },
        error: function (error) { alert('No fue posible cargar los datos teléfonicos'); }
    });
}


//-------------------------------------------------------------------------------------------------------------------

// Temporizador par Consultas
var hours = minutes = seconds = milliseconds = 0;
var prev_hours = prev_minutes = prev_seconds = prev_milliseconds = undefined;
var timeUpdate;

function cronometro_consulta() {
    updateTime(0, 0, 0);
};

function updateTime(prev_hours, prev_minutes, prev_seconds) {
    var startTime = new Date();    // fetch current time

    timeUpdate = setInterval(function () {
        var timeElapsed = new Date().getTime() - startTime.getTime();    // calculate the time elapsed in milliseconds

        // calculate hours                
        hours = parseInt(timeElapsed / 1000 / 60 / 60) + prev_hours;

        // calculate minutes
        minutes = parseInt(timeElapsed / 1000 / 60) + prev_minutes;
        if (minutes > 60) minutes %= 60;

        // calculate seconds
        seconds = parseInt(timeElapsed / 1000) + prev_seconds;
        if (seconds > 60) seconds %= 60;

        // set the stopwatch
        setStopwatch(hours, minutes, seconds);

    }, 25); // update time in stopwatch after every 25ms

}

// Set the time in stopwatch
function setStopwatch(hours, minutes, seconds) {
    $("#hours").html(prependZero(hours, 2));
    $("#minutes").html(prependZero(minutes, 2));
    $("#seconds").html(prependZero(seconds, 2));
    document.getElementById("TiempoConsulta").value = hours + ":" + minutes + ":" + seconds;
}

// Prepend zeros to the digits in stopwatch
function prependZero(time, length) {
    time = new String(time);    // stringify time
    return new Array(Math.max(length - time.length + 1, 0)).join("0") + time;
}

//------------------------------------------------------------------------------------------

function disableform_ordenes() {

    var f = document.getElementById("Form_EliminarRemision").getElementsByTagName("select");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    var f = document.getElementById("Form_EliminarRemision").getElementsByTagName("input");
    for (var i = 0; i < f.length; i++) {
        if ((f[i].name != 'IngresoId') && (f[i].name != 'NovedadId') && (f[i].name != 'ItemId')) {
            f[i].disabled = true;
        };
    }

    var f = document.getElementById("Form_EliminarRemision").getElementsByTagName("textarea");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    var f = document.getElementById("Form_EliminarRemision").getElementsByTagName("checkbox");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

};

//------------------------------------------------------------------------------------------
function disableform_ingresos() {

    var f = document.getElementById("taprincipalconsultaactiva").getElementsByTagName("select");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    var f = document.getElementById("taprincipalconsultaactiva").getElementsByTagName("input");
    for (var i = 0; i < f.length; i++) {
        f[i].disabled = true;
    }

    var f = document.getElementById("taprincipalconsultaactiva").getElementsByTagName("textarea");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    var f = document.getElementById("taprincipalconsultaactiva").getElementsByTagName("button");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    var f = document.getElementById("taprincipalconsultaactiva").getElementsByTagName("checkbox");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true
};


//------------------------------------------------------------------------------------------

function disableform_Consulta()
{
    //var TiempoBloqueoConsulta = document.getElementById("TiempoBloqueoConsulta").value;
    //var FechaAtn = document.getElementById("FechaAtn").value;

    //if (TiempoBloqueoConsulta > 0) {
    //    var diff = Math.abs(new Date(FechaAtn) - new Date());
    //    var minutes = Math.floor((diff / 1000) / 60);

    //    

/*    if (minutes >= TiempoBloqueoConsulta) {*/

    var f = document.getElementById("formConsulta").getElementsByTagName("select");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    var f = document.getElementById("formConsulta").getElementsByTagName("input");
    for (var i = 0; i < f.length; i++) {
        f[i].disabled = true;
    }

    var f = document.getElementById("formConsulta").getElementsByTagName("textarea");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    var f = document.getElementById("formConsulta").getElementsByTagName("button");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    var f = document.getElementById("formConsulta").getElementsByTagName("checkbox");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    var f = document.getElementById("ordenes").getElementsByTagName("button");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    var f = document.getElementById("ordenes").getElementsByTagName("a:link");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    //    }
    //}
};

//-------------------------------------------------------------------------------------------------------------------

function cargarDepartamentos() {

    url = "/Salud/Beneficiarios/CargarDepartamentosJson";

    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        async: false,
        success: function (result) {
            //$('#Departamento').html('');
            $("#Departamento").append('<option value="">Seleccione un Departamento</option>');
            $.each(result, function (i, data) {
                $("#Departamento").append('<option value="' + data.ciudadid + '">' + data.region + '</option>');
            });
        }
    });
}

//-------------------------------------------------------------------------------------------------------------------

function cargarCiudadesBenef() {
    var departamento = document.getElementById("Departamento").value;
    var url = "/Salud/Beneficiarios/CargarCiudadesJson?departamento=" + departamento;

    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        async: false,
        success: function (result) {
            $('#CiudadId').html('');
            $("#CiudadId").append('<option value="">Seleccione una Ciudad</option>');
            $.each(result, function (i, data) {
                $("#CiudadId").append('<option value="' + data.ciudadid + '">' + data.nombreciudad + '</option>');
            });
        }
    });
}

//------------------------------------------------------------------------------------------
function disableform_enfermeria() {

    var f = document.getElementById("tabpanelnotasenfermeria").getElementsByTagName("input");
    for (var i = 0; i < f.length; i++) {
        f[i].disabled = true;
    }

    var f = document.getElementById("tabpanelnotasenfermeria").getElementsByTagName("button");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    var f = document.getElementById("tabpanelnotasenfermeria").getElementsByTagName("button");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

};

//------------------------------------------------------------------------------------------
function disableform_editaringreso() {

    var f = document.getElementById("Editar_Ingreso").getElementsByTagName("select");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    var f = document.getElementById("Editar_Ingreso").getElementsByTagName("input");
    for (var i = 0; i < f.length; i++) {
        f[i].disabled = true;
    }

    var f = document.getElementById("Editar_Ingreso").getElementsByTagName("textarea");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    var f = document.getElementById("Editar_Ingreso").getElementsByTagName("button");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true

    var f = document.getElementById("Editar_Ingreso").getElementsByTagName("checkbox");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = true
};

//-------------------------------------------------------------------------------------------------------------------

function CargarCiudades() {
    var url = "/Salud/Sedes/CargarCiudadesJson";

    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        async: false,
        success: function (result) {
            $("#CiudadId").append('<option value="">Seleccione una Ciudad</option>');
            $.each(result, function (i, data) {
                $("#CiudadId").append('<option value="' + data.ciudadid + '">' + data.nombreciudad + '</option>');
            });
        }
    });
}

//------------------------------------------------------------------------------------------


function PacienteAsistio(ingresoId)
{
    Asistido = document.getElementById("IndAsistio").checked;

    var url = "/Salud/Ingresos/PacienteAsistio?ingresoId=" + ingresoId + "&Asistido=" + Asistido;
    $.ajax({
        url: url,
        type: "GET",
        dataType: "TEXT",
        async: false,
        success: function (result) {
            
        }
    });
};

//------------------------------------------------------------------------------------------

function enable_notasaclaratorias() {

    var f = document.getElementById("notasaclaratorias").getElementsByTagName("button");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = false

};

//------------------------------------------------------------------------------------------

function cargardatosdelpacientetrabajosocial(HistoriaId)
{
    url = "/Salud/Citas/CargarDatosdelPacienteJson?HistoriaId=" + HistoriaId;

    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        async: false,
        success: function (result) {

            $.each(result, function (i, data)
            {
                document.getElementById("Barrio").value = data.barrio;
                document.getElementById("Direccion").value = data.direccion;
                document.getElementById("Telefono").value = data.telefono;
                document.getElementById("FechaNac").value = data.fechanac.toString().split('T')[0];
                document.getElementById("Estrato").value = data.estrato;
                document.getElementById("Acompanante").value = data.acompanante;

            });
        }        
    });
}

//------------------------------------------------------------------------------------------

function cargarDatosHistoriaId() {

    if (document.getElementById("HistoriaIdDatosPaciente") != null) {
        var historiaId = document.getElementById("HistoriaIdDatosPaciente").value;
        if (historiaId > 0) {
            var url = "/Salud/Citas/CargarDatosdelPacienteJson?HistoriaId=" + historiaId;

            $.ajax({
                url: url,
                type: "GET",
                dataType: "JSON",
                async: false,
                success: function (result) {

                    $.each(result, function (i, data) {
                        document.getElementById("datosPaciente").innerHTML = data.tipodocumento + " - " + data.numeronit + " - " + data.nombreben + " - " +
                            "Edad : " + data.edad + " - " + "Entidad : " + data.nombrefondo + " - " + "Regimen : " + data.nombreregimen +
                            " - " + "Tipo : " + data.tipo + " - " + "Categoria : " + data.categoria + " - " + "Teléfono : " + data.telefono + " - " +
                            "Celular : " + data.celular + " - " + "Acompañante : " + data.acompanante;
                    });
                }
            });

        }
    }
}

//------------------------------------------------------------------------------------------

function cargarDatosHistoriaId_2() {
    var historiaId = document.getElementById("HistoriaId").value;
    if (historiaId > 0) {
        var url = "/Salud/Citas/CargarDatosdelPacienteJson?HistoriaId=" + historiaId;

        $.ajax({
            url: url,
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (result) {

                $.each(result, function (i, data) {
                    document.getElementById("datosPaciente").innerHTML = data.tipodocumento + " - " + data.numeronit + " - " + data.nombreben + " - " +
                        "Edad : " + data.edad + " - " + "Entidad : " + data.nombrefondo + " - " + "Regimen : " + data.nombreregimen +
                        " - " + "Tipo : " + data.tipo + " - " + "Categoria : " + data.categoria + " - " + "Teléfono : " + data.telefono +
                        " - " + "Acompañante : " + data.acompanante;
                });
            }
        });

    }
}



function ActualizarCantidadesAplicadasDia()
{
    var url = "/Salud/Ingresos/ActualizarCantidadesDespachadasDia";
    $.ajax({
        url: url,
        type: "GET",
        dataType: "TEXT",
        async: false,
        success: function (result) {

        }
    });
}



function enableform_ingresos_gestores_gestor() {

    var f = document.getElementById("taprincipalconsultaactiva").getElementsByTagName("select");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = false

    var f = document.getElementById("taprincipalconsultaactiva").getElementsByTagName("input");
    for (var i = 0; i < f.length; i++) {
        f[i].disabled = false;
    }

    var f = document.getElementById("taprincipalconsultaactiva").getElementsByTagName("textarea");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = false

    var f = document.getElementById("taprincipalconsultaactiva").getElementsByTagName("button");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = false

    var f = document.getElementById("taprincipalconsultaactiva").getElementsByTagName("checkbox");
    for (var i = 0; i < f.length; i++)
        f[i].disabled = false
};


