using pw3_tpIntegrador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	[MetadataType(typeof(PropuestaMetadata))]
	public partial class Propuesta
	{
        public String TipoDonacionString()
        {
            if (this.TipoDonacion == 1)
            {
                return "dinero";
            }
            else if (this.TipoDonacion == 2)
            {
                return "insumos";
            }
            else
            {
                return "horas de trabajo";
            }
        }
    }
}
