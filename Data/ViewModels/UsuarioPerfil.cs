using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public class UsuarioPerfil : Usuario
	{
		[Required(ErrorMessage = "El Nombre no puede estar vacio")]
		[MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
		public string Nombre { get; set; }

		[Required(ErrorMessage = "El Apellido no puede estar vacio")]
		[MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
		public string Apellido { get; set; }

		[Required(ErrorMessage = "La Foto es requerida")]
		public string Foto { get; set; }
	}
}
