function validardm(event) {
	event.preventDefault();
	var montodm, ArchivoTransferencia;

	montodm = document.getElementById("montodm").value;
	ArchivoTransferencia = document.getElementById("ArchivoTransferencia").value;

	if (montodm === "" || ArchivoTransferencia === "") {
		alert("Todos los campos son obligatorios");
		return false;
	}
	else if (isNaN(montodm)) {
		alert("El Monto ingresado no es un numero");
		return false;
	}
	else if (montodm > 999999999999999999) {
		alert("El Monto ingresado es muy grande");
		return false;
	}
	else if (montodm < 0) {
		alert("El Monto ingresado es muy pequeño");
		return false;
	}
	event.target.submit();
}