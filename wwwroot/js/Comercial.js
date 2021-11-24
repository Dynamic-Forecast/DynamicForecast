
//
///// X'BPM
//
// PrincipalProceso
//

//
// Info Evento
//
function mostrarDatosFase(fase) {

    var estado = $("#estadoSeccion_" + fase).val();
    if (estado == "false") {
        $("#seccionInfoFase_" + fase).css("display", "flex");
        $("#estadoSeccion_" + fase).val(true);
    } else {
        $("#estadoSeccion_" + fase).val(false);
        $("#seccionInfoFase_" + fase).css("display", "none");

    }
}

//