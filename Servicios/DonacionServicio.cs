using Data;
using Servicios.DTO.Donacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class DonacionServicio
    {
        readonly TpEntities ctx = new TpEntities();

        public List<DonacionDto> ObtenerDonacionesDelUsuario(int UsuarioId)
        {
            List<DonacionesMonetaria> monetarias = ctx.DonacionesMonetarias.Where(x => x.Usuario.IdUsuario == UsuarioId).ToList();
            List<DonacionesInsumo> insumos = ctx.DonacionesInsumos.Where(x => x.Usuario.IdUsuario == UsuarioId).ToList();
            List<DonacionesHorasTrabajo> horasTrabajo = ctx.DonacionesHorasTrabajoes.Where(x => x.Usuario.IdUsuario == UsuarioId).ToList();

            return TransformarADonacionesDto(monetarias, insumos, horasTrabajo);
        }

        private List<DonacionDto> TransformarADonacionesDto(List<DonacionesMonetaria> monetarias, List<DonacionesInsumo> insumos, List<DonacionesHorasTrabajo> horasTrabajo)
        {
            List<DonacionDto> Respuesta = new List<DonacionDto>();

            foreach(var m in monetarias)
            {
                Respuesta.Add(TransformarTipoMonetariaADonacionDto(m));
            }

            foreach(var i in insumos)
            {
                Respuesta.Add(TransformarTipoInsumoADonacionDto(i));
            }

            foreach(var h in horasTrabajo)
            {
                Respuesta.Add(TransformarTipoHorasTrabajoADonacionDto(h));
            }

            return Respuesta.OrderByDescending(x=>x.FechaDonacion).ToList();
        }

        private DonacionDto TransformarTipoMonetariaADonacionDto(DonacionesMonetaria d)
        {
            DonacionDto Respuesta = new DonacionDto();

            Respuesta.FechaDonacion = d.PropuestasDonacionesMonetaria.Propuesta.FechaCreacion;
            Respuesta.Foto = d.PropuestasDonacionesMonetaria.Propuesta.Foto;
            Respuesta.Nombre = d.PropuestasDonacionesMonetaria.Propuesta.Nombre;
            Respuesta.TipoDonacion = "Donación monetaria";
            Respuesta.Estado = d.PropuestasDonacionesMonetaria.Estado == 1 ? "activa" : "inactiva";
            Respuesta.TotalRecaudado = "$" + ctx.DonacionesMonetarias.Where(x=>x.IdPropuestaDonacionMonetaria == d.IdPropuestaDonacionMonetaria).Sum(x=>x.Dinero).ToString();
            Respuesta.MiDonacion = "$" + d.Dinero.ToString();
            Respuesta.LinkAPublicacion = "/Propuestas/Detalle/" + d.PropuestasDonacionesMonetaria.IdPropuesta;

            return Respuesta;
        }

        private DonacionDto TransformarTipoInsumoADonacionDto(DonacionesInsumo d)
        {
            DonacionDto Respuesta = new DonacionDto();

            Respuesta.FechaDonacion = d.PropuestasDonacionesInsumo.Propuesta.FechaCreacion;
            Respuesta.Foto = d.PropuestasDonacionesInsumo.Propuesta.Foto;
            Respuesta.Nombre = d.PropuestasDonacionesInsumo.Propuesta.Nombre;
            Respuesta.TipoDonacion = "Donación de insumos";
            Respuesta.Estado = d.PropuestasDonacionesInsumo.Estado == 1 ? "activa" : "inactiva";
            Respuesta.TotalRecaudado = ctx.DonacionesInsumos.Where(x=>x.IdPropuestaDonacionInsumo == d.IdPropuestaDonacionInsumo).Sum(x=>x.Cantidad).ToString() + " " + d.PropuestasDonacionesInsumo.Nombre;
            Respuesta.MiDonacion = d.PropuestasDonacionesInsumo.Cantidad + " " + d.PropuestasDonacionesInsumo.Nombre;
            Respuesta.LinkAPublicacion = "/Propuestas/Detalle/" + d.PropuestasDonacionesInsumo.IdPropuesta;

            return Respuesta;
        }

        private DonacionDto TransformarTipoHorasTrabajoADonacionDto(DonacionesHorasTrabajo d)
        {
            DonacionDto Respuesta = new DonacionDto();

            Respuesta.FechaDonacion = d.PropuestasDonacionesHorasTrabajo.Propuesta.FechaCreacion;
            Respuesta.Foto = d.PropuestasDonacionesHorasTrabajo.Propuesta.Foto;
            Respuesta.Nombre = d.PropuestasDonacionesHorasTrabajo.Propuesta.Nombre;
            Respuesta.TipoDonacion = "Donación de horas de trabajo";
            Respuesta.Estado = d.PropuestasDonacionesHorasTrabajo.Estado == 1 ? "activa" : "inactiva";
            Respuesta.TotalRecaudado = ctx.DonacionesHorasTrabajoes.Where(x=>x.IdPropuestaDonacionHorasTrabajo == d.IdPropuestaDonacionHorasTrabajo).Sum(x=>x.Cantidad).ToString() + " horas de " + d.PropuestasDonacionesHorasTrabajo.Profesion;
            Respuesta.MiDonacion = d.Cantidad.ToString() + " horas";
            Respuesta.LinkAPublicacion = "/Propuestas/Detalle/" + d.PropuestasDonacionesHorasTrabajo.IdPropuesta;

            return Respuesta;
        }

    }
}
