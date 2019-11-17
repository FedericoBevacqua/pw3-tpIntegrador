function validardi(event) {
	event.preventDefault();
	var nombredi, cantidaddi;

	nombredi = document.getElementById("nombredi").value;
	cantidaddi = document.getElementById("cantidaddi").value;

	if (nombredi === "" || cantidaddi === "") {
		alert("Todos los campos son obligatorios");
		return false;
	}
	else if (isNaN(cantidaddi)) {
		alert("El Monto ingresado no es un numero");
		return false;
	}
	else if (cantidaddi > 9999999999) {
		alert("El Monto ingresado es muy grande");
		return false;
	}
	else if (cantidaddi < 0) {
		alert("El Monto ingresado es muy pequeño");
		return false;
	}
	else if (nombredi.lenght > 50) {
		alert("El Nombre no puede superar los 50 caracteres");
		return false;
	}
	event.target.submit();
}