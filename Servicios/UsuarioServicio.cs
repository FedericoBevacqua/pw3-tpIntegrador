using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
	public class UsuarioServicio
	{
		TpEntities ctx = new TpEntities();

		public void Alta(Usuario u)
		{
			u.Token = "12345"; //TODO: Token Autogenerado
			u.FechaCracion = DateTime.Now;
			u.Activo = false;
			u.TipoUsuario = 0; // 0 = Normal | 1 = Administrador 
			ctx.Usuarios.Add(u);
			ctx.SaveChanges();
		}

		public Usuario ObtenerPorId(int id)
		{
			return ctx.Usuarios.Find(id);
		}
	}
}
