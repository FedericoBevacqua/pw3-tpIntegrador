using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pw3_tpIntegrador
{
	public class PropuestasDonacionesMonetariasMetadata
	{
		[Required(ErrorMessage = "El monto no puede estar vacio")]
		[Range(0, 9999999999999999.99, ErrorMessage = "El monto no es valido")]
		public decimal Dinero { get; set; }

		[Required(ErrorMessage = "El Cbu no puede estar vacio")]
		[MaxLength(80, ErrorMessage = "Maximo 80 numeros")]
		public string CBU { get; set; }
	}
}
