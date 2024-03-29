﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pw3_tpIntegrador
{
	public class UsuarioMetadata
	{
		/*
		[Required(ErrorMessage = "El Email no puede estar vacio")]
		[DataType(DataType.EmailAddress, ErrorMessage = "Email no valido")]
		[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email no valido")]
		[MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
		public string Email { get; set; }

		[Required(ErrorMessage = "El Password no puede estar vacio")]
		[DataType(DataType.Password, ErrorMessage = "Password no valido")]
		[RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password debe contener al menos un numero, una letra mayuscula y minuscula")]
		[MinLength(8, ErrorMessage = "Minimo 8 caracteres")]
		public string Password { get; set; }


		[Required(ErrorMessage = "La Fecha no puede estar vacia")]
		[DataType(DataType.Date)]
		//TODO: [MayorDeEdad(ErrorMessage = "Debe ser mayor a 18 años")] //Data Annotation Personalizada
		public System.DateTime FechaNacimiento { get; set; }
		*/

		//Confirmacion Password
		/*[Required(ErrorMessage = "La confirmación del Password no puede estar vacia")]
		[DataType(DataType.Password, ErrorMessage = "Password no valido")]
		[RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password debe contener al menos un numero, una letra mayuscula y minuscula")]
		[MinLength(8, ErrorMessage = "Minimo 8 caracteres")]
		[NotMapped] // Does not effect with your database
		[Compare("Password")]
		public string PasswordConfirmar { get; set; }
		*/


		/*TODO: Necesario para Validar CrearPerfil
		[Required(ErrorMessage = "El Nombre no puede estar vacio")]
		[MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
		public string Nombre { get; set; }

		[Required(ErrorMessage = "El Apellido no puede estar vacio")]
		[MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
		public string Apellido { get; set; }

		[Required(ErrorMessage = "La Foto es requerida")]
		public string Foto { get; set; }
		*/
	}
}
