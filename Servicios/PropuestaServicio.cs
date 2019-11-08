using Data;
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

		public void Alta(Propuesta p)
		{
			//p.IdUsuarioCreador	 //TODO: Recibir IdUsuarioCreador

			p.Estado = 1; // Estados: 1-Activo | 0-Inactivo
			p.FechaCreacion = DateTime.Now;
			ctx.Propuestas.Add(p);
			ctx.SaveChanges();
		}

		public Propuesta ObtenerPorId(int id)
		{
			return ctx.Propuestas.Find(id);
		}

		public void CrearDenuncia(Denuncia d)
		{
			//d.IdUsuario =  ;		//TODO: Recibir IdUsuario
			//d.IdPropuesta	=  ;	//TODO: Recibir IdPropuesta

			d.FechaCreacion = DateTime.Now;
			d.Estado = 0; //Tipos Estado: 0-Revision | 1-Aceptada
			ctx.Denuncias.Add(d);
			ctx.SaveChanges();
		}
	}
}
