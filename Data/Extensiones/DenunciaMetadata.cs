using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pw3_tpIntegrador
{
	public class DenunciaMetadata
	{
		[Required(ErrorMessage = "El Motivo no puede estar vacio")]
		[Range(1, 4, ErrorMessage = "El Motivo no es valido")]
		public int IdMotivo { get; set; }

		[Required(ErrorMessage = "El Comentario no puede estar vacio")]
		[MaxLength(300, ErrorMessage = "Maximo 300 caracteres")]
		public string Comentarios { get; set; }
	}
}
