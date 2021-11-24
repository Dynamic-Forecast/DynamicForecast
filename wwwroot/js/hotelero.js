function CargarDatosTercero()
{
    var numeroNit = document.getElementById("NumeroNit").value;
    url = "/Hotelero/Reservas/DatosTerceroXDocumento?NumeroNit=" + numeroNit;

    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        async: false,
        success: function (result) {

            $.each(result, function (i, data) {
                document.getElementById("Nombre1").value = data.nombre1;
                document.getElementById("Nombre2").value = data.nombre2;
                document.getElementById("Apellido1").value = data.apellido1;
                document.getElementById("Apellido2").value = data.apellido2;
                document.getElementById("Direccion").value = data.direccion;
                document.getElementById("CiudadId").value = data.ciudadId;
                document.getElementById("Telefono").value = data.telefono;
                document.getElementById("Celular").value = data.celular;
                document.getElementById("EmailPersonal").value = data.emailpersonal;
            });
        },
        error: function (error) { alert('No fue posible cargar los datos del tercero'); }
    });
}

//---------------------------------------------------------------------------------------------