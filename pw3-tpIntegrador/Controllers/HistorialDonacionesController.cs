using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Servicios;
using Servicios.DTO.Donacion;

namespace pw3_tpIntegrador.Controllers
{
    public class HistorialDonacionesController : ApiController
    {
        DonacionServicio _donacionServicio = new DonacionServicio();

        // GET: api/HistorialDonaciones/5
        public List<DonacionDto> Get(int id)
        {
            return _donacionServicio.ObtenerDonacionesDelUsuario(id);
        }
    }
}
