function validarp() {
	var nombrep, descripcionp, fechafinp, telefonop, TipoDonacion, Imagen, refnombre1p, refnombre2p, reftelefono1p, reftelefono2p;

	nombrep = document.getElementById("nombrep").value;
	descripcionp = document.getElementById("descripcionp").value;
	fechafinp = document.getElementById("fechafinp").value;
	telefonop = document.getElementById("telefonop").value;
	TipoDonacion = document.getElementById("TipoDonacion").value;
	Imagen = document.getElementById("fotop").value;
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
}