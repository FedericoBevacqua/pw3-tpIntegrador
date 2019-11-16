using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
	public class DonarMonetaria : Propuesta
	{
		[Required(ErrorMessage = "El comprobante de pago es requerido")]
		public string ArchivoTransferencia { get; set; }

		[Required(ErrorMessage = "El monto no puede estar vacio")]
		[Range(0, 9999999999999999.99, ErrorMessage = "El monto no es valido")]
		public decimal Dinero { get; set; }
		public int IdUsuario { get; set; }
		public int IdPropuestaDonacionMonetaria { get; set; }
	}
}
