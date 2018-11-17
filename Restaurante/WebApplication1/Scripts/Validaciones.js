
function recarga() {
    document.getElementById("provincia").value = "";
    document.getElementById("terminos").checked = false;
    document.getElementById("apellido").value = "";
    document.getElementById("nombre").value = "";
    document.getElementById("email").value = "";
    document.getElementById("dni").value = "";
}

function validarLocalidades() {
    var provincia = document.getElementById("provincia").value;
    if (provincia == "bsas") {
        document.getElementById("localidades").style.display = "block";
    } else {
        document.getElementById("localidades").style.display = "none";
    }
}

$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})

function validarEnvio() {
    var provincia = document.getElementById("provincia").value;
    var terminos = document.getElementById("terminos").checked;
    var apellido = document.getElementById("apellido").value;
    var nombre = document.getElementById("nombre").value;
    var email = document.getElementById("email").value;
    var dni = document.getElementById("dni").value;
    if (terminos || provincia == null || apellido == null || nombre == null || email == null || dni == null) {
        alert('[ERROR] Ingrese correctamente los campos a completar');
    }
}