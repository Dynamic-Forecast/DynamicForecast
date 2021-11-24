showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            if (res.isValid == false) {
                window.location.href = res.url;
            } else {
                $('#form-modal .modal-body').html(res);
                $('#form-modal .modal-title').html(title);
                var div = document.getElementById('labelmessage');
                div.style.display = 'none';
                $('#form-modal').modal('show');
            }
        }
    })
}

showInPopup2 = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal2 .modal-body').html(res);
            $('#form-modal2 .modal-title').html(title);
            $('#form-modal2').modal('show');
        }
    })
}

showInPopupSize = (url, title, sizeModal = "modal-xl") => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal .modal-dialog').removeClass("modal-xl");
            $('#form-modal .modal-dialog').addClass(sizeModal);
            $('#form-modal').modal('show');
        }
    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res)
            {
                if (res.isValid) {
                    //$('#view-all').html(res.html)
                    //$('#form-modal .modal-body').html('');
                    //$('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    window.location.href = res.url;
                }
                else {
                    $('#form-modal .modal-body').html(res.html);
                    
                    if (res.mensaje != 'undefined' && res.mensaje != '')
                    {
                        $('#form-modal .modal-message').html(res.mensaje);
                        var div = document.getElementById('labelmessage');
                        div.style.display = '';
                    }
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxPost2 = form => {
    try {
        $.ajax({
            type: 'GET',
            url: form.action,
            data: new FormData(form),
            success: function (res) {
                $('#form-modal .modal-body').html(res.html);
                $('#form-modal .modal-title').html(res.title);
                $('#form-modal').modal('show');
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxPostSimple = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    //$('#view-all').html(res.html)
                    //$('#form-modal .modal-body').html('');
                    //$('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    //window.location.href = res.url;
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}
