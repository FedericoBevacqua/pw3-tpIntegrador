//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonacionesInsumo
    {
        public int IdDonacionInsumo { get; set; }
        public int IdPropuestaDonacionInsumo { get; set; }
        public int IdUsuario { get; set; }
        public int Cantidad { get; set; }
    
        public virtual PropuestasDonacionesInsumo PropuestasDonacionesInsumo { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
