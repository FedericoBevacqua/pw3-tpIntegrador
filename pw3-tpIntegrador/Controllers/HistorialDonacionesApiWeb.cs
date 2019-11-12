using Data;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace pw3_tpIntegrador.Controllers
{
	public class HistorialDonacionesApiWeb : ApiController
	{
		PropuestaServicio servicio = new PropuestaServicio();

		// GET api/values
		public IEnumerable<Propuesta> Get(int id)
		{
			//TODO: 
			var Propuestas = servicio.ObtenerPorId(id);
			//TODO: Cambiarlo
			yield return Propuestas;
		}
	}
}