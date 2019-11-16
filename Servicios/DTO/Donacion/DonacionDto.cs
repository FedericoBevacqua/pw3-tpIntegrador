using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.DTO.Donacion
{
    public class DonacionDto
    {
        public DateTime FechaDonacion { get; set; }
        public String Foto { get; set; }
        public string Nombre { get; set; }
        public string TipoDonacion { get; set; }
        public string Estado { get; set; }
        public string TotalRecaudado { get; set; }
        public string MiDonacion { get; set; }
        public string LinkAPublicacion { get; set; }

    }
}
