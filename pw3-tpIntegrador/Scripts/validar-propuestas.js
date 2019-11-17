let validarCamposTipoMonetaria = (event) => {
    event.preventDefault();

    let dinero = parseInt(document.getElementById('Dinero').value),
        cbu = document.getElementById('CBU').value;

    if (isNaN(dinero) || dinero == 0) {
        alert("El campo Cantidad de dinero debe ser un número mayor que 0");
        return false;
    } else if (cbu === "") {
        alert("El campo CBU es obligatorio");
        return false;
    }

    event.target.submit();
}



let validarCamposTipoInsumos = (event) => {
    event.preventDefault();

    let cantidadInsumos = parseInt(document.getElementById('input-cantidad-insumos').value);

    for (i = 0; i < cantidadInsumos; i++) {
        let cantidad = parseInt(document.getElementById('Cantidad_' + i + "_").value),
            nombre = document.getElementById('Nombres_' + i + "_").value;

        if (isNaN(cantidad) || cantidad == 0) {
            alert("Todas las cantidades deben ser un mayores que 0");
            return false;
        } else if (nombre === "" || nombre.length < 5){
            alert("Todas los nombres son obligatorios y deben tener más de 5 caracteres");
            return false;
        }
    }

    event.target.submit();
}




let validarCamposTipoHorasTrabajo = (event) => {
    event.preventDefault();

    let cantidadHoras = document.getElementById('CantidadHoras').value;

    if (isNaN(cantidadHoras) || cantidadHoras == 0) {
        alert("La cantidad de horas debe ser un número mayor que 0");
        return false;
    }

    event.target.submit();
}