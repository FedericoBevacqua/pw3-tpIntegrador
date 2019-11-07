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
			//TODO: p.IdUsuarioCreador
			//TODO: p.Foto =;
			p.Estado = 1; // Estados: 1-Activo | 0-Inactivo
			p.FechaCreacion = DateTime.Now;
			ctx.Propuestas.Add(p);
			ctx.SaveChanges();
		}

		public Propuesta ObtenerPorId(int id)
		{
			return ctx.Propuestas.Find(id);
		}
	}
}
