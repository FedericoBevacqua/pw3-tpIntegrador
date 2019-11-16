using Servicios.CustomDataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public class PropuestaCrear
	{
		[Required(ErrorMessage = "El Nombre no puede estar vacio")]
		[MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
		public string Nombre { get; set; }

		[Required(ErrorMessage = "La Descripcion no puede estar vacia")]
		[MaxLength(100, ErrorMessage = "Maximo 50 caracteres")] //No especifica
		public string Descripcion { get; set; }

		[Required(ErrorMessage = "La Fecha de Finalización no puede estar vacia")]
		[DataType(DataType.Date)]
		[FechaMinimaHoy(ErrorMessage = "La fecha no puede ser menor al dia de hoy")]
		public System.DateTime FechaFin { get; set; }

		[Required(ErrorMessage = "El Telefono no puede estar vacio")]
		[MaxLength(30, ErrorMessage = "Maximo 30 numeros")]
		public string TelefonoContacto { get; set; }

		[Required(ErrorMessage = "Elija un tipo de donación")]
		[Range(1, 3, ErrorMessage = "Seleccione un tipo de donacion valida")]
		public int TipoDonacion { get; set; }

		[Required(ErrorMessage = "La Foto es requerida")]
		public string Foto { get; set; }

		//
		[Required(ErrorMessage = "La Referencia Nombre 1 es requerida")]
		[MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
		public string Referencia1Nombre { get; set; }

		[Required(ErrorMessage = "La Referencia Nombre 2 es requerida")]
		[MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
		public string Referencia2Nombre { get; set; }

		[Required(ErrorMessage = "La Referencia Telefono 1 es requerida")]
		[MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
		public string Referencia1Telefono { get; set; }

		[Required(ErrorMessage = "La Referencia Telefono 2 es requerida")]
		[MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
		public string Referencia2Telefono { get; set; }
		
	}
}
