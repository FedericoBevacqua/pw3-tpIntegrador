function validardh(event) {
	event.preventDefault();
	var cantidadh;

	cantidadh = document.getElementById("cantidadh").value;

	if (cantidadh === "") {
		alert("La cantidad de horas es obligatoria");
		return false;
	}
	else if (isNaN(cantidadh)) {
		alert("La cantidad de horas no es un numero");
		return false;
	}
	else if (cantidadh > 999999) {
		alert("La cantidad de horas ingresadas es muy grande");
		return false;
	}
	else if (cantidadh < 0) {
		alert("La cantidad de horas ingresadas es muy pequeña");
		return false;
	}
	event.target.submit();
}