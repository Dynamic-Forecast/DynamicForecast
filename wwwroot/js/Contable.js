$(document).ready(function () {
    $("#SubTabCliente").hide();
    $("#formCliente").hide();
    $("#SubTabProveedor").hide();
    $("#formProveedor").hide();
    $("#SubTabVendedor").hide();
    $("#formVendedor").hide();
    $("#SubTabBancarios").hide();
    $(".formEmpleado").hide();
    $("#SubTabCompetencia").hide();
    $("#formCompetencia").hide();
    $(".datoProveedor").hide();
    $(".datoEmpleado").hide();

    $("#Tercero_TipoDoc").select2({
        allowClear: true,
        placeholder: 'Seleccione un Tipo de documento',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $('#Tercero_TipoDoc').on('select2:select', function (e) {
        var tipoDoc = $("#Tercero_TipoDoc").val();
        if ((tipoDoc == "N") || (tipoDoc == "X")) {
            $(".DatosPersonaNatural").addClass("d-none");
            $("#DatosPersonaJuridica").removeClass("d-none");
        } else {
            $(".DatosPersonaNatural").removeClass("d-none");
            $("#DatosPersonaJuridica").addClass("d-none");
        }
    });

    $("#Tercero_PrecioId").select2({
        allowClear: true,
        placeholder: 'Seleccione una Lista de Precios',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $("#Tercero_TarifaIca").select2({
        allowClear: true,
        placeholder: 'Seleccione una Tarifa',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $("#Tercero_TarifaFTE").select2({
        allowClear: true,
        placeholder: 'Seleccione una Tarifa',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $("#Tercero_CanalId").select2({
        allowClear: true,
        placeholder: 'Seleccione un Canal',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $("#canalVendedor").select2({
        allowClear: true,
        placeholder: 'Seleccione un Canal',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $("#Tercero_TarifaICAProv").select2({
        allowClear: true,
        placeholder: 'Seleccione una Tarifa',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $("#Tercero_TarifaFTEProv").select2({
        allowClear: true,
        placeholder: 'Seleccione una Tarifa',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    const today = new Date();
    let fechaIngreso = new Date();
    var year = today.getFullYear();
    var month = today.getMonth();
    var day = today.getDate();
    $('#FechaIngreso_').datetimepicker({
        format: 'YYYY-MM-DD',
        icons: {
            time: 'far fa-clock',
            date: 'far fa-calendar',
            close: 'far fa-times'
        },
        defaultDate: new Date(),
        maxDate: today.setDate(today.getDate() + 29),
        locale: 'es',
        onSelect: function (date) {
            var dateIngreso = $('#FechaIngreso_').datepicker('getDate');
            $("#FechaPrueba_").datepicker("setDate", new Date(date));
            $("#FechaPrueba_").datepicker("option", "minDate", new Date(date));
            $("#FechaFinal_").datepicker("option", "minDate", new Date(date));
            $("#FechaVacacion_").datepicker("option", "minDate", new Date(date.getFullYear() + 1, date.getMonth(), date.getDate()));
        },
    });
    $('#FechaPrueba_').datetimepicker({
        format: 'YYYY-MM-DD',
        icons: {
            time: 'far fa-clock',
            date: 'far fa-calendar',
            close: 'far fa-times'
        },
        defaultDate: new Date(),
        locale: 'es'
    });
    $('#FechaFinal_').datetimepicker({
        format: 'YYYY-MM-DD',
        icons: {
            time: 'far fa-clock',
            date: 'far fa-calendar',
            close: 'far fa-times'
        },
        //defaultDate: new Date(),
        locale: 'es'
    });
    $('#FechaVacacion_').datetimepicker({
        format: 'YYYY-MM-DD',
        icons: {
            time: 'far fa-clock',
            date: 'far fa-calendar',
            close: 'far fa-times'
        },
        defaultDate: new Date(),
        locale: 'es'
    });
    $('#FechaEPS_').datetimepicker({
        format: 'YYYY-MM-DD',
        icons: {
            time: 'far fa-clock',
            date: 'far fa-calendar',
            close: 'far fa-times'
        },
        defaultDate: new Date(),
        locale: 'es'
    });
    $('#FechaEFP_').datetimepicker({
        format: 'YYYY-MM-DD',
        icons: {
            time: 'far fa-clock',
            date: 'far fa-calendar',
            close: 'far fa-times'
        },
        defaultDate: new Date(),
        locale: 'es'
    });
    $("#Contrato_TipoNomId").select2({
        allowClear: true,
        placeholder: 'Seleccione un Tipo de Nomina',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $("#Contrato_TipoContrato").select2({
        allowClear: true,
        placeholder: 'Seleccione un Tipo de Contrato',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $("#Contrato_TipoTrabajadorId").select2({
        allowClear: true,
        placeholder: 'Seleccione un Tipo de Trabajador',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $("#Contrato_SubTipoTrabajadorId").select2({
        allowClear: true,
        placeholder: 'Seleccione un SubTipo de Trabajador',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $("#Contrato_TipoSalario").select2({
        allowClear: true,
        placeholder: 'Seleccione un Tipo de Salario',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $("#Contrato_FormaPagoId").select2({
        allowClear: true,
        placeholder: 'Seleccione una Forma de Pago',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $("#Contrato_MetodoPagoId").select2({
        allowClear: true,
        placeholder: 'Seleccione un Método de Pago',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $("#TipoContacto").select2({
        allowClear: true,
        placeholder: 'Seleccione un Tipo de Contacto',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });

    let countDotacion = 0;
    $('#AgregarDotacion').click(function (e) {
        e.preventDefault();

        let cTipoElemento = document.getElementById("DotacionTipoElemento");
        let tTipoElemento = cTipoElemento.options[cTipoElemento.selectedIndex].text;
        let vTipoElemento = $("#DotacionTipoElemento").val();
        let tTalla = $("#DotacionTalla").val();
        let tDescripcion = $("#DotacionDescripcion").val();
        var idRandom = Math.floor((Math.random() * (999 - 1)) + 1);
        let formRegistro = "<div class='dotacion" + idRandom + "'>\
                <input id='Dotaciones[" + countDotacion + "].TipoElemento'   name='Dotaciones[" + countDotacion + "].TipoElemento' type = 'text' value = '" + vTipoElemento + "' />\
                <input id='Dotaciones[" + countDotacion + "].Descripcion'   name='Dotaciones[" + countDotacion + "].Descripcion' type = 'text' value = '" + tDescripcion + "' />\
                <input id='Dotaciones[" + countDotacion + "].Talla' name='Dotaciones[" + countDotacion + "].Talla' type = 'text' value = '" + tTalla + "' />\
            </div >";
        let registro = "<tr class='gradeX'>\
                            <td class='text-center align-middle'>\
                                <a href='#' class='editar-dotacion'>\
                                    <i class='redondeado icon-note' title='Editar'></i>\
                                </a>\
                            </td>\
                            <td class='text-center'>\
                                <a href='#' id='dotacion" + idRandom + "' class='remover-dotacion'>\
                                    <i class='redondeado icon-trash' title='Eliminar'></i>\
                                </a>\
                            </td>\
                            <td>" + tTipoElemento + "</td>\
                            <td>" + tTalla + "</td>\
                            <td>" + tDescripcion + "</td>\
                        </tr>";
        $("#dotacionTable").append(registro);
        $("#dotacionForm").append(formRegistro);
        $("#DotacionTipoElemento").val(null).trigger('change');
        $("#DotacionTalla").val("");
        $("#DotacionDescripcion").val("");
        countDotacion++;
    });
    $('#dotacionTable').on("click", ".remover-dotacion", function (e) {
        e.preventDefault();
        var idReg = "." + $(this).attr('id');
        $(this).closest('tr').remove();
        $(idReg).remove();
    });
    let countContacto = 0;

    $('#AgregarContacto').click(function (e) {
        e.preventDefault();
        let cTipoContacto = document.getElementById("TipoContacto");
        let tTipoContacto = cTipoContacto.options[cTipoContacto.selectedIndex].text;
        let vTipoContacto = $("#TipoContacto").val();
        let tCargo = $("#ContactoCargo").val();
        let tWS = $("#ContactoWS").val();
        let tNombre = $("#ContactoNombre").val();
        let tTelefono = $("#ContactoTelefono").val();
        let tEmail = $("#ContactoEmail").val();
        var idRandom = Math.floor((Math.random() * (999 - 1)) + 1);
        let formRegistro = "<div class='contacto" + idRandom + "'>\
                <input id='[" + countContacto + "].TipoContacto'   name='Contactos[" + countContacto + "].TipoContacto' type = 'text' value = '" + vTipoContacto + "' />\
                <input id='[" + countContacto + "].Nombre'   name='Contactos[" + countContacto + "].Nombre' type = 'text' value = '" + tNombre + "' />\
                <input id='[" + countContacto + "].Telefono' name='Contactos[" + countContacto + "].Telefono' type = 'text' value = '" + tTelefono + "' />\
                <input id='[" + countContacto + "].Cargo' name='Contactos[" + countContacto + "].Cargo' type = 'text' value = '" + tCargo + "' />\
                <input id='[" + countContacto + "].EMail' name='Contactos[" + countContacto + "].EMail' type = 'text' value = '" + tEmail + "' />\
                <input id='[" + countContacto + "].Whatsapp' name='Contactos[" + countContacto + "].Whatsapp' type = 'text' value = '" + tWS + "' />\
            </div >";
        let registro = "<tr class='gradeX'>\
                            <td class='text-center align-middle'>\
                                <a href='#' class='editar-contacto'>\
                                    <i class='redondeado icon-note' title='Editar'></i>\
                                </a>\
                            </td>\
                            <td class='text-center'>\
                                <a href='#' id='contacto" + idRandom + "' class='remover-contacto'>\
                                    <i class='redondeado icon-trash' title='Eliminar'></i>\
                                </a>\
                            </td>\
                            <td>" + tTipoContacto + "</td>\
                            <td>" + tNombre + "</td>\
                            <td>" + tCargo + "</td>\
                            <td>" + tWS + "</td>\
                            <td>" + tTelefono + "</td>\
                            <td>" + tEmail + "</td>\
                        </tr>";
        $("#contactoTable").append(registro);
        $("#contactoForm").append(formRegistro);
        $("#TipoContacto").val(null).trigger('change');
        $("#ContactoCargo").val("");
        $("#ContactoWS").val("");
        $("#ContactoNombre").val("");
        $("#ContactoTelefono").val("");
        $("#ContactoEmail").val("");
        countContacto++;
    });
    $('#contactoTable').on("click", ".remover-contacto", function (e) {
        e.preventDefault();
        let idReg = "." + $(this).attr('id');
        $(this).closest('tr').remove();
        $(idReg).remove();
    });

    let countObligacion = 0;
    $("#AgregarObligacion").click(function (e) {
        e.preventDefault();
        if ($("#OtrosObligacion").val() == null) {
            Swal.fire('¡Upss no has espedicificado la Obligación!', '', 'error');
            return;
        }
        let cObligacion = document.getElementById("OtrosObligacion");
        let tObligacion = cObligacion.options[cObligacion.selectedIndex].text;
        let vObligacion = $("#OtrosObligacion").val();


        var idRandom = Math.floor((Math.random() * (999 - 1)) + 1);
        let formRegistro = "<div class='obliga" + idRandom + "'>\
                <input id='Obligaciones[" + countObligacion + "].ObligacionId'   name='Obligaciones[" + countObligacion + "].ObligacionId' type = 'text' value = '" + vObligacion + "' />\
                <input id='Obligaciones[" + countObligacion + "].Estado'   name='Obligaciones[" + countObligacion + "].Estado' type = 'text' value = 'AC' />\
            </div >";
        let registro = "<tr class='gradeX'>\
                            <td class='text-center align-middle'>\
                                <a href='#' class='editar-obliga'>\
                                    <i class='redondeado icon-note' title='Editar'></i>\
                                </a>\
                            </td>\
                            <td class='text-center'>\
                                <a href='#' id='obliga" + idRandom + "' class='remover-obliga'>\
                                    <i class='redondeado icon-trash' title='Eliminar'></i>\
                                </a>\
                            </td>\
                            <td class='text-center'>" + tObligacion + "</td>\
                        </tr>";
        $("#obligacionTable").append(registro);
        $("#obligacionForm").append(formRegistro);
        $("#OtrosObligacion").val(null).trigger('change');
        countObligacion++;
    });
    $('#obligacionTable').on("click", ".remover-obliga", function (e) {
        e.preventDefault();
        var idReg = "." + $(this).attr('id');
        $(this).closest('tr').remove();
        $(idReg).remove();
    });

    let countPunto = 0;
    $('#AgregarPunto').click(function (e) {
        e.preventDefault();
        let vCodigo = $("#CodigoPunto").val();
        let vNombre = $("#NombrePunto").val();
        let vDireccion = $("#DireccionPunto").val();

        if ((vCodigo == "") || (vCodigo == undefined)) {
            Swal.fire('¡Upss no has espedicificado el Código del Punto!', '', 'error');
            return;
        }
        if ((vNombre == "") || (vNombre == undefined)) {
            Swal.fire('¡Upss no has espedicificado el Nombre del Punto!', '', 'error');
            return;
        }
        if ((vDireccion == "") || (vDireccion == undefined)) {
            Swal.fire('¡Upss no has espedicificado la Dirección del Punto!', '', 'error');
            return;
        }

        let vDescripcion = $("#DescripcionPunto").val();
        let vLimite = $("#LimiteDespacho").val();
        let vInicio = $("#HoraIni").val();
        let vFin = $("#HoraFin").val();
        let vCiudad = $("#CiudadDespacho").val();
        let vContacto = $("#ContactoDespacho").val();
        var idRandom = Math.floor((Math.random() * (999 - 1)) + 1);
        let formRegistro = "<div class='puntos" + idRandom + "'>\
                <input id='Puntos[" + countPunto + "].CodigoPunto' name='Puntos[" + countPunto + "].CodigoPunto' type ='hidden' value ='" + vCodigo + "' />\
                <input id='Puntos[" + countPunto + "].NombrePunto' name='Puntos[" + countPunto + "].NombrePunto' type ='hidden' value ='" + vNombre + "' />\
                <input id='Puntos[" + countPunto + "].Descripcion'   name='Puntos[" + countPunto + "].Descripcion' type ='hidden' value ='" + vDescripcion + "' />\
                <input id='Puntos[" + countPunto + "].Direccion'   name='Puntos[" + countPunto + "].Direccion' type ='hidden' value ='" + vDireccion + "' />\
                <input id='Puntos[" + countPunto + "].LimiteDesp'   name='Puntos[" + countPunto + "].LimiteDesp' type ='hidden' value ='" + vLimite + "' />\
                <input id='Puntos[" + countPunto + "].HoraIni'   name='Puntos[" + countPunto + "].HoraIni' type ='hidden' value ='" + vInicio + "' />\
                <input id='Puntos[" + countPunto + "].HoraIniCitaEntrega'   name='Puntos[" + countPunto + "].HoraIniCitaEntrega' type ='hidden' value ='" + vInicio + "' />\
                <input id='Puntos[" + countPunto + "].HoraFin'   name='Puntos[" + countPunto + "].HoraFin' type ='hidden' value ='" + vFin + "' />\
                <input id='Puntos[" + countPunto + "].HoraFinCitaEntrega'   name='Puntos[" + countPunto + "].HoraFinCitaEntrega' type ='hidden' value ='" + vFin + "' />\
                <input id='Puntos[" + countPunto + "].CiudadId'   name='Puntos[" + countPunto + "].CiudadId' type ='hidden' value ='" + vCiudad + "' />\
                <input id='Puntos[" + countPunto + "].ContactoItem'   name='Puntos[" + countPunto + "].ContactoItem' type ='hidden' value ='" + vContacto + "' />\
            </div >";
        let registro = "<tr class='gradeX'>\
                            <td class='text-center'>\
                                <a href='#' class='editar-punto'>\
                                    <i class='redondeado icon-note' title='Editar'></i>\
                                </a>\
                            </td>\
                            <td class='text-center'>\
                                <a href='#' id='puntos" + idRandom + "' class='remover-punto'>\
                                    <i class='redondeado icon-trash' title='Eliminar'></i>\
                                </a>\
                            </td>\
                            <td>" + vCodigo + "</td>\
                            <td>" + vNombre + "</td>\
                            <td>" + vDireccion + "</td>\
                            <td>" + vDescripcion + "</td>\
                        </tr>";
        $("#puntoTable").append(registro);
        $("#puntoForm").append(formRegistro);
        $("#CodigoPunto").val("");
        $("#NombrePunto").val("");
        $("#DireccionPunto").val("");
        $("#DescripcionPunto").val("");
        $("#LimiteDespacho").val("0");
        $("#ContactoDespacho").val(null).trigger('change');
        countPunto++;
    });
    $('#puntoTable').on("click", ".remover-punto", function (e) {
        e.preventDefault();
        var idReg = "." + $(this).attr('id');
        $(this).closest('tr').remove();
        $(idReg).remove();
    });

    let hoursDespIni = today;
    hoursDespIni.setHours(8, 0, 0, 0);
    $('#HoraIni_').datetimepicker({
        format: 'LT',
        icons: {
            time: 'far fa-clock',
            date: 'far fa-calendar',
            close: 'far fa-times'
        },
        defaultDate: hoursDespIni,
        locale: 'es'
    });

    let hoursDespFin = today;
    hoursDespFin.setHours(17, 0, 0, 0);
    $('#HoraFin_').datetimepicker({
        format: 'LT',
        icons: {
            time: 'far fa-clock',
            date: 'far fa-calendar',
            close: 'far fa-times'
        },
        defaultDate: hoursDespFin,
        locale: 'es'
    });

    $("#ContactoDespacho").val(null).trigger('change');
    $("#ContactoDespacho").select2({
        allowClear: true,
        placeholder: 'Seleccione un Contacto',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });

    $('#FechaGraduacion_').datetimepicker({
        format: 'YYYY-MM-DD',
        icons: {
            time: 'far fa-clock',
            date: 'far fa-calendar',
            close: 'far fa-times'
        },
        defaultDate: new Date(),
        maxDate: today,
        locale: 'es',
    });
    let countEstudio = 0;
    $("#AgregarEstudio").click(function (e) {
        e.preventDefault();
        let vTitulo = $("#CEstudioTitulos").val();
        let vInstitucion = $("#CEstudioInstitucion").val();
        let vGrado = $("#FechaGraduacion").val();
        var idRandom = Math.floor((Math.random() * (999 - 1)) + 1);
        let formRegistro = "<div class='estudio" + idRandom + "'>\
                <input id='Estudios[" + countEstudio + "].Titulo'      name='Estudios[" + countEstudio + "].Titulo'      type = 'text' value = '" + vTitulo + "' />\
                <input id='Estudios[" + countEstudio + "].Institucion' name='Estudios[" + countEstudio + "].Institucion' type = 'text' value = '" + vInstitucion + "' />\
                <input id='Estudios[" + countEstudio + "].FechaGrado'  name='Estudios[" + countEstudio + "].FechaGrado'  type = 'text' value = '" + vGrado + "' />\
            </div >";
        let registro = "<tr class='gradeX'>\
                            <td class='text-center'>\
                                <a href='#' class='editar-estudio'>\
                                    <i class='redondeado icon-note' title='Editar'></i>\
                                </a>\
                            </td>\
                            <td class='text-center'>\
                                <a href='#' id='estudio" + idRandom + "' class='remover-estudio'>\
                                    <i class='redondeado icon-trash' title='Eliminar'></i>\
                                </a>\
                            </td>\
                            <td>" + vTitulo + "</td>\
                            <td>" + vInstitucion + "</td>\
                            <td>" + vGrado + "</td>\
                        </tr>";
        $("#estudioTable").append(registro);
        $("#estudioForm").append(formRegistro);
        $("#CEstudioTitulos").val("");
        $("#CEstudioInstitucion").val("");
        $("#FechaGraduacion_").datepicker("setDate", new Date());
        countEstudio++;
    });
    $('#estudioTable').on("click", ".remover-estudio", function (e) {
        e.preventDefault();
        var idReg = "." + $(this).attr('id');
        $(this).closest('tr').remove();
        $(idReg).remove();
    });

    $('#FechaNacimiento_').datetimepicker({
        format: 'YYYY-MM-DD',
        icons: {
            time: 'far fa-clock',
            date: 'far fa-calendar',
            close: 'far fa-times'
        },
        defaultDate: new Date(),
        maxDate: today,
        locale: 'es',
    });
    $('#FechaCompetencia_').datetimepicker({
        format: 'YYYY-MM-DD',
        icons: {
            time: 'far fa-clock',
            date: 'far fa-calendar',
            close: 'far fa-times'
        },
        defaultDate: new Date(),
        locale: 'es',
    });
    let countHijo = 0;
    $("#AgregarHijo").click(function (e) {
        e.preventDefault();
        let vNm1 = $("#CHijosNombre1").val();
        let vNm2 = $("#CHijosNombre2").val();
        let vAp1 = $("#CHijosApellido1").val();
        let vAp2 = $("#CHijosApellido2").val();
        let vGen = $("#generoHijo").val();
        let vNac = $("#FechaNacimiento").val();
        var idRandom = Math.floor((Math.random() * (999 - 1)) + 1);
        let formRegistro = "<div class='hijo" + idRandom + "'>\
                <input id='Hijos[" + countHijo + "].Nombre1'   name='Hijos[" + countHijo + "].Nombre1'   type = 'text' value = '" + vNm1 + "' />\
                <input id='Hijos[" + countHijo + "].Nombre2'   name='Hijos[" + countHijo + "].Nombre2'   type = 'text' value = '" + vNm2 + "' />\
                <input id='Hijos[" + countHijo + "].Apellido1' name='Hijos[" + countHijo + "].Apellido1' type = 'text' value = '" + vAp1 + "' />\
                <input id='Hijos[" + countHijo + "].Apellido2' name='Hijos[" + countHijo + "].Apellido2' type = 'text' value = '" + vAp2 + "' />\
                <input id='Hijos[" + countHijo + "].Genero'    name='Hijos[" + countHijo + "].Genero'    type = 'text' value = '" + vGen + "' />\
                <input id='Hijos[" + countHijo + "].FechaNac'  name='Hijos[" + countHijo + "].FechaNac'  type = 'text' value = '" + vNac + "' />\
            </div >";
        let registro = "<tr class='gradeX'>\
                            <td class='text-center'>\
                                <a href='#' class='editar-estudio'>\
                                    <i class='redondeado icon-note' title='Editar'></i>\
                                </a>\
                            </td>\
                            <td class='text-center'>\
                                <a href='#' id='hijo" + idRandom + "' class='remover-hijo'>\
                                    <i class='redondeado icon-trash' title='Eliminar'></i>\
                                </a>\
                            </td>\
                            <td>" + vNm1 + "</td>\
                            <td>" + vNm2 + "</td>\
                            <td>" + vAp1 + "</td>\
                            <td>" + vAp2 + "</td>\
                            <td>" + vNac + "</td>\
                        </tr>";
        $("#hijoTable").append(registro);
        $("#hijoForm").append(formRegistro);
        $("#CHijosNombre1").val("");
        $("#CHijosNombre2").val("");
        $("#CHijosApellido1").val("");
        $("#CHijosApellido2").val("");
        $("#FechaNacimiento_").datepicker("setDate", new Date());
        countHijo++;
    });
    $('#hijoTable').on("click", ".remover-hijo", function (e) {
        e.preventDefault();
        var idReg = "." + $(this).attr('id');
        $(this).closest('tr').remove();
        $(idReg).remove();
    });

    let countCosto = 0;
    $("#AgregarCCosto").click(function (e) {
        e.preventDefault();
        let cCosto = document.getElementById("ProyectoCC");
        let tCosto = cCosto.options[cCosto.selectedIndex].text;
        let vCosto = $("#ProyectoCC").val();
        let vParticipacion = $("#participacion").val();
        var idRandom = Math.floor((Math.random() * (999 - 1)) + 1);
        let formRegistro = "<div class='costo" + idRandom + "'>\
                <input id='Hijos[" + countCosto + "].Nombre1'   name='Hijos[" + countCosto + "].Participacion'   type = 'text' value = '" + vParticipacion + "' />\
                <input id='Hijos[" + countCosto + "].Nombre2'   name='Hijos[" + countCosto + "].ProyectoId'   type = 'text' value = '" + vCosto + "' />\
            </div >";
        let registro = "<tr class='gradeX'>\
                            <td class='text-center'>\
                                <a href='#' class='editar-estudio'>\
                                    <i class='redondeado icon-note' title='Editar'></i>\
                                </a>\
                            </td>\
                            <td class='text-center'>\
                                <a href='#' id='costo" + idRandom + "' class='remover-hijo'>\
                                    <i class='redondeado icon-trash' title='Eliminar'></i>\
                                </a>\
                            </td>\
                            <td>" + vCosto + "</td>\
                            <td>" + tCosto + "</td>\
                            <td>" + vParticipacion + "%</td>\
                        </tr>";
        $("#costoTable").append(registro);
        $("#costoForm").append(formRegistro);
        $("#ProyectoCC").val(null).trigger("change");
        $("#participacion").val(0);
        countCosto++;
    });
    $('#costoTable').on("click", ".remover-hijo", function (e) {
        e.preventDefault();
        var idReg = "." + $(this).attr('id');
        $(this).closest('tr').remove();
        $(idReg).remove();
    });

    $("#Tercero_TipoRegimen").select2({
        allowClear: true,
        placeholder: 'Seleccione un Tipo de regimen',
        width: '100%',
        closeOnSelect: true,
        language: {
            noResults: "No se encontraron resultados",
        },
        focus: true,
    });
    $('#Tercero_TipoRegimen').on('select2:select', function (e) {
        var tipoReg = $("#Tercero_TipoRegimen").val();
        if (tipoReg == "S") {
            $("#Tercero_IvaSimpli").val("S");
        } else {
            $("#Tercero_IvaSimpli").val("N");
        }
    });
});
$(".FormularioEnvioDatos").on('submit', function () {
    mostrarMensajes();
});

function onCheckRegimen(checkbox) {
    if (checkbox.checked) {
        $("#Tercero_GranContrib").attr("disabled", true);
        $("#Tercero_GranContrib").prop("checked", false);

        $("#Tercero_AutoRetFte").attr("disabled", true);
        $("#Tercero_AutoRetFte").prop("checked", false);
    }
    else {
        $("#Tercero_GranContrib").attr("disabled", false);
        $("#Tercero_AutoRetFte").attr("disabled", false);
    }
}
function onCheckCliente(checkbox) {
    if (checkbox.checked) {
        $("#TComercial").css("display", "block");
        $("#formCliente").show();
        $("#SubTabCliente").show();
    }
    else {
        $("#TComercial").css("display", "none");
        $("#formCliente").hide();
        $("#SubTabCliente").hide();
    }
}
function onCheckEmpleado(checkbox) {
    if (checkbox.checked) {
        $("#TDatosContrato").css("display", "block");
        $("#TContratoEstudios").css("display", "block");
        $("#TContratoHijos").css("display", "block");
        $("#TDotacion").css("display", "block");
        $(".formEmpleado").show();
        $("#SubTabBancarios").show();
        $(".formBancarios").show();
        $(".datoEmpleado").show();
        // Pone estos campos como obligatorios cuando es empleado
        $("#Contrato_CargoId").attr("required", true);
        $("#Contrato_ProyectoId").attr("required", true);
        $("#Contrato_TipoNomId").attr("required", true);
        $("#Contrato_FechaIngreso").attr("required", true);
        $("#Contrato_TipoContrato").attr("required", true);
        $("#Contrato_Salario").attr("required", true);
    }
    else {
        $("#TDatosContrato").css("display", "none");
        $("#TContratoEstudios").css("display", "none");
        $("#TContratoHijos").css("display", "none");
        $("#TDotacion").css("display", "none");
        $(".formEmpleado").hide();
        if (!$("#Tercero_Proveedor").is(":checked")) {
            $("#SubTabBancarios").hide();
            $(".formBancarios").hide();
        }
        $(".datoEmpleado").hide();

        // quita estos campos como obligatorios cuando no es empleado
        $("#Contrato_CargoId").attr("required", false);
        $("#Contrato_ProyectoId").attr("required", false);
        $("#Contrato_TipoNomId").attr("required", false);
        $("#Contrato_FechaIngreso").attr("required", false);
        $("#Contrato_TipoContrato").attr("required", false);
        $("#Contrato_Salario").attr("required", false);
    }
}
function onCheckProveedor(checkbox) {
    if (checkbox.checked) {
        $("#SubTabProveedor").show();
        $("#formProveedor").show();
        $("#SubTabBancarios").show();
        $(".formBancarios").show();
        $(".datoProveedor").show();
    }
    else {
        $("#SubTabProveedor").hide();
        $("#formProveedor").hide();
        if (!$("#Tercero_Empleado").is(":checked")) {
            $("#SubTabBancarios").hide();
            $(".formBancarios").hide();
        }
        $(".datoProveedor").hide();
    }
}
function onCheckVendedor(checkbox) {
    if (checkbox.checked) {
        $("#SubTabVendedor").show();
        $("#formVendedor").show();
    }
    else {
        $("#SubTabVendedor").hide();
        $("#formVendedor").hide();
    }
}
function onCheckCompetencia(checkbox) {
    if (checkbox.checked) {
        $("#SubTabCompetencia").show();
        $("#formCompetencia").show();
    }
    else {
        $("#SubTabCompetencia").hide();
        $("#formCompetencia").hide();
    }
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function mostrarMensajes() {
    Url = "@Url.Action('DatosTerceroCreado', 'Terceros', new {Area = 'Contable'})";
    $.get(Url).done(function (res) {
        //if (res.data.existe == true) {
        //    $("#mensajeYaExiste").css("display", "block");
        //    $("#mensajeError").css("display", "none");
        //    $("#mensajeAgregado").css("display", "none");

        //}
        //else {
        $("#mensajeAgregado").css("display", "block");
        $("#mensajeError").css("display", "none");
        $("#TerceroIdNuevo").val(res.data.terId);
        $("#NombreTerceroNuevo").val(res.data.nombre);
        $("#DocTerceroNuevo").val(res.data.documento);
        //$("#mensajeYaExiste").css("display", "none");

        //}
        if (res.data.error) {
            $("#mensajeError").css("display", "block");
            $("#mensajeAgregado").css("display", "none");
            // $("#mensajeYaExiste").css("display", "none");


        }
    });

}
//if ((document.getElementById("HistoriaId").value != '') && (document.getElementById("HistoriaId").value != null)) { cargardatosdelpaciente(); }
//cargarCentrosCostos();

//$('#HistoriaId').select2({
//    allowClear: true,
//    placeholder: 'Seleccione un Paciente',
//    //dropdownParent: $('#form-modal .modal-content'),
//    width: '100%',
//    closeOnSelect: true,
//    language: {
//        noResults: function () {
//            return 'No se encontraron resultados';
//        },
//        searching: function () {
//            return 'Buscando…';
//        },
//        inputTooShort: function (args) {
//            var remainingChars = args.minimum - args.input.length;
//            var message = 'Por favor, introduzca ' + remainingChars + ' car';

//            if (remainingChars == 1) {
//                message += 'ácter';
//            } else {
//                message += 'acteres';
//            }

//            return message;
//        },
//        errorLoading: function () {
//            return 'No se pudieron cargar los resultados';
//        },
//        loadingMore: function () {
//            return 'Cargando más resultados…';
//        },
//    },
//    focus: true,
//    minimumInputLength: 3,
//    ajax: {
//        dataType: 'json',
//        url: '@Url.Action("GetBeneficiariosFind", "Beneficiarios")',
//        data: function (params) {
//            return {
//                q: params.term
//            };
//        },
//    },
//});
