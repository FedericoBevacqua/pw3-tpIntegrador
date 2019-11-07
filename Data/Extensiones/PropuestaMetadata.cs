using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pw3_tpIntegrador
{
	public class PropuestaMetadata
	{
		[Required(ErrorMessage = "El Nombre no puede estar vacio")]
		[MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
		public string Nombre { get; set; }

		[Required(ErrorMessage = "La Descripcion no puede estar vacia")]
		[MaxLength(50, ErrorMessage = "Maximo 50 caracteres")] //No especifica
		public string Descripcion { get; set; }

		[Required(ErrorMessage = "La Fecha de Finalización no puede estar vacia")]
		[DataType(DataType.Date)]
		//TODO: [FechaMinimaHoyAttribute]
		public System.DateTime FechaFin { get; set; }

		[Required(ErrorMessage = "El Telefono no puede estar vacio")]
		[MaxLength(50, ErrorMessage = "Maximo 30 numeros")] //No especifica
		public string TelefonoContacto { get; set; }

		[Required(ErrorMessage = "Elija un tipo de donación")]
		[Range(1, 3, ErrorMessage = "Seleccione un tipo de donacion valida")]
		public int TipoDonacion { get; set; }
	}
}
