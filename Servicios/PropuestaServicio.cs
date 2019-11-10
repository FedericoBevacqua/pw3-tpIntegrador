﻿using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
	public class PropuestaServicio
	{
		TpEntities ctx = new TpEntities();
        UsuarioServicio Usuarios = new UsuarioServicio();

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

            foreach(PropuestasDonacionesInsumo ins in listaInsumos)
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

        public int GuardarDatosComunes(Propuesta p)
        {
            Propuesta Propuesta = new Propuesta();
            
            Propuesta.Usuario = ctx.Usuarios.Find(3); //TODO: Recibir IdUsuarioCreador
            Propuesta.Estado = 1; // Estados: 1-Activo | 0-Inactivo
            Propuesta.FechaCreacion = DateTime.Now;

            Propuesta.Nombre = p.Nombre;
            Propuesta.Descripcion = p.Descripcion;
            Propuesta.FechaFin = p.FechaFin;
            Propuesta.TelefonoContacto = p.TelefonoContacto;
            Propuesta.TipoDonacion = p.TipoDonacion;
            Propuesta.Foto = p.Foto;
            Propuesta.PropuestasReferencias = p.PropuestasReferencias;

            ctx.Propuestas.Add(Propuesta);
            ctx.SaveChanges();

            return Propuesta.IdPropuesta;
        }

        

		public Propuesta ObtenerPorId(int id)
		{
			return ctx.Propuestas.Find(id);
		}

		public void CrearDenuncia(Denuncia d)
		{
			//Denuncia denuncia = ObtenerPorId(d.IdPropuesta);

			//var iduser = ;
			//var idprop = ;

			//d.IdUsuario = iduser;	//TODO: Recibir IdUsuario de Usuario
			//d.IdPropuesta = idprop;	//TODO: Recibir IdPropuesta de Propuesta

			d.FechaCreacion = DateTime.Now;
			d.Estado = 0; //Tipos Estado: 0-Revision | 1-Aceptada
			ctx.Denuncias.Add(d);
			ctx.SaveChanges();
		}
	}
}
