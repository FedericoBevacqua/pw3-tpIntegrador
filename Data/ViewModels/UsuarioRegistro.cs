using Servicios.CustomDataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public class UsuarioRegistro : Usuario
	{
		
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

		//Confirmacion Password
		[Required(ErrorMessage = "La confirmación del Password no puede estar vacia")]
		[DataType(DataType.Password, ErrorMessage = "Password no valido")]
		[RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password debe contener al menos un numero, una letra mayuscula y minuscula")]
		[MinLength(8, ErrorMessage = "Minimo 8 caracteres")]
		//[NotMapped] // Does not effect with your database
		[Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
		public string PasswordConfirmar { get; set; }

		[Required(ErrorMessage = "La Fecha no puede estar vacia")]
		[DataType(DataType.Date)]
		[MayorDeEdad(18,ErrorMessage = "Debe ser mayor a 18 años")] //Data Annotation Personalizada
		public System.DateTime FechaNacimiento { get; set; }
	}
}
