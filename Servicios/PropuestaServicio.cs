using Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public class PropuestaServicio
    {
        readonly TpEntities ctx = new TpEntities();
        readonly UsuarioServicio Usuarios = new UsuarioServicio();

        public void Alta(PropuestasDonacionesMonetaria p)
        {
            int IdPropuesta = GuardarDatosComunes(p);

            p.IdPropuesta = IdPropuesta;
            ctx.PropuestasDonacionesMonetarias.Add(p);
            ctx.SaveChanges();
        }

        public void Alta(Propuesta p, List<PropuestasDonacionesInsumo> listaInsumos)
        {
            int IdPropuesta = GuardarDatosComunes(p);

            foreach (PropuestasDonacionesInsumo ins in listaInsumos)
            {
                ins.IdPropuesta = IdPropuesta;
                ctx.PropuestasDonacionesInsumos.Add(ins);
            }

            ctx.SaveChanges();
        }

        public void Alta(PropuestasDonacionesHorasTrabajo p)
        {
            int IdPropuesta = GuardarDatosComunes(p);

            p.IdPropuesta = IdPropuesta;
            ctx.PropuestasDonacionesHorasTrabajoes.Add(p);
            ctx.SaveChanges();
        }

        private int GuardarDatosComunes(Propuesta p)
        {
            int idUsuario = SesionServicio.UsuarioSession.IdUsuario;
            Propuesta Propuesta = new Propuesta
            {
                Usuario = ctx.Usuarios.Find(idUsuario),
                Estado = 1, // Estados: 1-Activo | 0-Inactivo
                FechaCreacion = DateTime.Now,

                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                FechaFin = p.FechaFin,
                TelefonoContacto = p.TelefonoContacto,
                TipoDonacion = p.TipoDonacion,
                Foto = p.Foto,
                PropuestasReferencias = p.PropuestasReferencias
            };

            ctx.Propuestas.Add(Propuesta);
            ctx.SaveChanges();

            return Propuesta.IdPropuesta;
        }

        public Propuesta ObtenerPorId(int id)
        {
            return ctx.Propuestas.Find(id);
        }

        public List<Propuesta> ObtenerTodas()
        {
            return ctx.Propuestas.ToList();
        }
		public List<Propuesta> ObtenerActivas()
		{
			return ctx.Propuestas.Where(x => x.Estado == 1).ToList();
		}
		public List<Propuesta> ObtenerCincoMejoresActivas()
		{
			return ctx.Propuestas.Where(x => x.Estado == 1).OrderByDescending(x => x.Valoracion).Take(5).ToList();
		}
		public void CrearDenuncia(Denuncia d)
        {
            d.FechaCreacion = DateTime.Now;
            d.Estado = 0; //Tipos Estado: 0-Revision | 1-Aceptada | 2-Desestimada
            ctx.Denuncias.Add(d);
            ctx.SaveChanges();

            if(ctx.Denuncias.Where(x=>x.IdPropuesta == d.IdPropuesta).Where(x=>x.Estado != 2).Count() >= 5)
            {
                //Si hay 5 denuncias sobre esta publicación en revisión o aceptadas se bloquea la publicación
                Propuesta p = ctx.Propuestas.Find(d.Propuesta.IdPropuesta);
                p.Estado = 0; //Estado inactivo
                ctx.SaveChanges();
            }
        }

        public void GuardarDonacion(DonacionesMonetaria d)
        {
            d.FechaCreacion = DateTime.Now;
            ctx.DonacionesMonetarias.Add(d);
            ctx.SaveChanges();
        }

        public void GuardarDonacion(List<DonacionesInsumo> donaciones)
        {
            foreach (var d in donaciones)
            {
                if (d.Cantidad > 0)
                {
                    ctx.DonacionesInsumos.Add(d);
                }
            }
            ctx.SaveChanges();
        }

        public void GuardarDonacion(DonacionesHorasTrabajo d)
        {
            ctx.DonacionesHorasTrabajoes.Add(d);
            ctx.SaveChanges();
        }

        public List<Propuesta> BuscarPorNombreYUsuario(string keyword)
        {
			int idUsuario = SesionServicio.UsuarioSession.IdUsuario;
			var resultados = ctx.Propuestas
                .Where(p => string.Equals(p.Usuario.UserName, keyword) || p.Nombre.Contains(keyword) && p.IdUsuarioCreador != idUsuario).OrderBy(x => x.FechaFin).ThenByDescending(x => x.Valoracion)
				.ToList();
            return resultados;
        }

        public void Bloquear(int id)
        {
            Propuesta propuesta = ctx.Propuestas.Find(id);
            propuesta.Estado = 0;
            ctx.SaveChanges();
        }
		public void ValorarMeGusta(int Id)
		{
			PropuestasValoracione Valoracion = new PropuestasValoracione();
			int idUsuario = SesionServicio.UsuarioSession.IdUsuario;
			var PropuestaActual = ObtenerPorId(Id);

			var consulta = ctx.PropuestasValoraciones.Where(x => x.IdPropuesta == PropuestaActual.IdPropuesta && idUsuario == x.IdUsuario).FirstOrDefault();
			if (consulta == null)
			{
				Valoracion.IdPropuesta = PropuestaActual.IdPropuesta;
				Valoracion.IdUsuario = idUsuario;
				Valoracion.Valoracion = true;
				ctx.PropuestasValoraciones.Add(Valoracion);
				ctx.SaveChanges();
			}
            else
            {
                consulta.Valoracion = true;
                ctx.SaveChanges();
            }
        }
		public void ValorarNoMeGusta(int Id)
		{
			PropuestasValoracione Valoracion = new PropuestasValoracione();
			int idUsuario = SesionServicio.UsuarioSession.IdUsuario;
			var PropuestaActual = ObtenerPorId(Id);

			var consulta = ctx.PropuestasValoraciones.Where(x => x.IdPropuesta == PropuestaActual.IdPropuesta && idUsuario == x.IdUsuario).FirstOrDefault();
			if (consulta == null)
			{
			Valoracion.IdPropuesta = PropuestaActual.IdPropuesta;
			Valoracion.IdUsuario = idUsuario;
			Valoracion.Valoracion = false;
			ctx.PropuestasValoraciones.Add(Valoracion);
			ctx.SaveChanges();
			}
            else
            {
                consulta.Valoracion = false;
                ctx.SaveChanges();
            }
		}
		public decimal CalcularValoracionTotal(int Id)
		{
			var PropuestaActual = ObtenerPorId(Id);
			var cantidadMeGusta = ctx.PropuestasValoraciones.Where(x => x.IdPropuesta == PropuestaActual.IdPropuesta && x.Valoracion == true).Count();
			var cantidadTotal = ctx.PropuestasValoraciones.Where(x => x.IdPropuesta == PropuestaActual.IdPropuesta).Count();

            decimal Valoracion = (decimal)cantidadMeGusta / cantidadTotal * 100;
            PropuestaActual.Valoracion = Valoracion;
			ctx.SaveChanges();

            return Valoracion;
		}

        public int CantidadPropuestasActivasPorUsuario(int id)
        {
            return ctx.Propuestas.Where(p => p.IdUsuarioCreador == id).Count();
        }

        public void Modificar(int idPropuesta, Propuesta propuesta)
        {
            Propuesta propuestaAModificar = ObtenerPorId(idPropuesta);
            propuestaAModificar.Nombre = propuesta.Nombre;
            propuestaAModificar.Descripcion = propuesta.Descripcion;
            propuestaAModificar.FechaFin = propuesta.FechaFin;
            propuestaAModificar.TelefonoContacto = propuesta.TelefonoContacto;
            propuestaAModificar.TipoDonacion = propuesta.TipoDonacion;
            propuestaAModificar.Foto = "FIXME";
            // TODO: Arreglar propuestaAModificar.Foto = propuesta.Foto;

            switch (propuestaAModificar.TipoDonacion)
            {
                case 1: //TipoMonetaria
                    propuestaAModificar.PropuestasDonacionesMonetarias.FirstOrDefault().Dinero = (propuesta as PropuestasDonacionesMonetaria).Dinero;
                    propuestaAModificar.PropuestasDonacionesMonetarias.FirstOrDefault().CBU = (propuesta as PropuestasDonacionesMonetaria).CBU;
                    break;
                case 2: //TipoInsumos
                    //TODO: Copiar lista de insumos modificada
                    //List<PropuestasDonacionesInsumo> listaInsumos = ExtraerListaInsumos(form);
                    //((PropuestasDonacionesInsumo)propuestaAModificar).DonacionesInsumos = listaInsumos;
                    break;
                case 3: //TipoHorasTrabajo
                    propuestaAModificar.PropuestasDonacionesHorasTrabajoes.FirstOrDefault().CantidadHoras = (propuesta as PropuestasDonacionesHorasTrabajo).CantidadHoras;
                    propuestaAModificar.PropuestasDonacionesHorasTrabajoes.FirstOrDefault().Profesion = (propuesta as PropuestasDonacionesHorasTrabajo).Profesion;
                    break;
            }

            ctx.SaveChanges();
        }

	}
}
