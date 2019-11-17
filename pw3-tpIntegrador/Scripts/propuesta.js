function validarp(event) {
    event.preventDefault();
	var nombrep, descripcionp, fechafinp, telefonop, TipoDonacion, Imagen, refnombre1p, refnombre2p, reftelefono1p, reftelefono2p;

	nombrep = document.getElementById("nombrep").value;
	descripcionp = document.getElementById("descripcionp").value;
	fechafinp = document.getElementById("FechaFin").value;
	telefonop = document.getElementById("telefonop").value;
	TipoDonacion = document.getElementById("TipoDonacion").value;
	Imagen = document.getElementById("Imagen").value;
	refnombre1p = document.getElementById("refnombre1p").value;
	refnombre2p = document.getElementById("refnombre2p").value;
	reftelefono1p = document.getElementById("reftelefono1p").value;
	reftelefono2p = document.getElementById("reftelefono2p").value;

    if (nombrep === "" || descripcionp === "" || fechafinp === "" || telefonop === "" || TipoDonacion === "" || Imagen === "" || refnombre1p === "" || refnombre2p === "" || reftelefono1p === "" || reftelefono2p === "") {
		alert("Todos los campos son obligatorios");
		return false;
	}
	else if (nombrep.lenght > 50) {
		alert("El Nombre no puede superar los 50 caracteres");
		return false;
	}
	else if (descripcionp.lenght > 300) {
		alert("La descripción no puede superar los 300 caracteres");
		return false;
	}
	else if (fechafinp < Date.now()) {
		alert("La fecha de finalización no posterior al día de hoy");
		return false;
	}
	else if (telefonop.lenght > 30) {
		alert("El telefono no puede superar los 30 caracteres");
		return false;
	}
	else if (isNaN(telefonop)) {
		alert("El telefono ingresado no es un número");
		return false;
	}
	else if (TipoDonacion > 3 || TipoDonacion < 1) {
		alert("El tipo de donación no es correcta");
		return false;
	}
	else if (refnombre1p.lenght > 50) {
		alert("El Nombre del referido 1 no puede superar los 50 caracteres");
		return false;
	}
	else if (refnombre2p.lenght > 50) {
		alert("El Nombre del referido 2 no puede superar los 50 caracteres");
		return false;
	}
	else if (reftelefono1p.lenght > 50) {
		alert("El telefono del referido 1 no puede superar los 50 caracteres");
		return false;
	}
	else if (isNaN(reftelefono1p)) {
		alert("El telefono del referido 1 ingresado no es un número");
		return false;
	}
	else if (reftelefono2p.lenght > 50) {
		alert("El telefono del referido 2 no puede superar los 50 caracteres");
		return false;
	}
	else if (isNaN(reftelefono2p)) {
		alert("El telefono del referido 2 ingresado no es un número");
		return false;
	}

    event.target.submit();
}


function validarEdicion(event) {
    event.preventDefault();

    event.preventDefault();
    var nombrep, descripcionp, fechafinp, telefonop, TipoDonacion, Imagen, refnombre1p, refnombre2p, reftelefono1p, reftelefono2p;

    nombrep = document.getElementById("nombrep").value;
    descripcionp = document.getElementById("descripcionp").value;
    fechafinp = document.getElementById("FechaFin").value;
    telefonop = document.getElementById("telefonop").value;
    TipoDonacion = document.getElementById("TipoDonacion").value;
    refnombre1p = document.getElementById("refnombre1p").value;
    refnombre2p = document.getElementById("refnombre2p").value;
    reftelefono1p = document.getElementById("reftelefono1p").value;
    reftelefono2p = document.getElementById("reftelefono2p").value;

    if (nombrep === "" || descripcionp === "" || fechafinp === "" || telefonop === "" || TipoDonacion === "" || refnombre1p === "" || refnombre2p === "" || reftelefono1p === "" || reftelefono2p === "") {
        alert("Todos los campos son obligatorios");
        return false;
    }
    else if (nombrep.lenght > 50) {
        alert("El Nombre no puede superar los 50 caracteres");
        return false;
    }
    else if (descripcionp.lenght > 300) {
        alert("La descripción no puede superar los 300 caracteres");
        return false;
    }
    else if (fechafinp < Date.now()) {
        alert("La fecha de finalización no posterior al día de hoy");
        return false;
    }
    else if (telefonop.lenght > 30) {
        alert("El telefono no puede superar los 30 caracteres");
        return false;
    }
    else if (isNaN(telefonop)) {
        alert("El telefono ingresado no es un número");
        return false;
    }
    else if (TipoDonacion > 3 || TipoDonacion < 1) {
        alert("El tipo de donación no es correcta");
        return false;
    }
    else if (refnombre1p.lenght > 50) {
        alert("El Nombre del referido 1 no puede superar los 50 caracteres");
        return false;
    }
    else if (refnombre2p.lenght > 50) {
        alert("El Nombre del referido 2 no puede superar los 50 caracteres");
        return false;
    }
    else if (reftelefono1p.lenght > 50) {
        alert("El telefono del referido 1 no puede superar los 50 caracteres");
        return false;
    }
    else if (isNaN(reftelefono1p)) {
        alert("El telefono del referido 1 ingresado no es un número");
        return false;
    }
    else if (reftelefono2p.lenght > 50) {
        alert("El telefono del referido 2 no puede superar los 50 caracteres");
        return false;
    }
    else if (isNaN(reftelefono2p)) {
        alert("El telefono del referido 2 ingresado no es un número");
        return false;
    }



    if (TipoDonacion == 1) {
        let dinero = parseInt(document.getElementById('Dinero').value),
            cbu = document.getElementById('CBU').value;

        if (isNaN(dinero) || dinero == 0) {
            alert("El campo Cantidad de dinero debe ser un número mayor que 0");
            return false;
        } else if (cbu === "") {
            alert("El campo CBU es obligatorio");
            return false;
        }
    } else if (TipoDonacion == 2) {
        let cantidadInsumos = parseInt(document.getElementById('input-cantidad-insumos').value);

        for (i = 0; i < cantidadInsumos; i++) {
            let cantidad = parseInt(document.getElementById('Cantidad_' + i + "_").value),
                nombre = document.getElementById('Nombres_' + i + "_").value;

            if (isNaN(cantidad) || cantidad == 0) {
                alert("Todas las cantidades deben ser un mayores que 0");
                return false;
            } else if (nombre === "" || nombre.length < 5) {
                alert("Todas los nombres son obligatorios y deben tener más de 5 caracteres");
                return false;
            }
        }
    } else {
        let cantidadHoras = document.getElementById('CantidadHoras').value;

        if (isNaN(cantidadHoras) || cantidadHoras == 0) {
            alert("La cantidad de horas debe ser un número mayor que 0");
            return false;
        }
    }

    event.target.submit();
}