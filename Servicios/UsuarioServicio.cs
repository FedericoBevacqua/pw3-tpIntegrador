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
			u.Token = NuevoCodigoDeActivacion(); //Token autogenerado
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

		public Usuario ValidarLogin(Usuario usuario)
		{
			var ConsultaUsuario = ctx.Usuarios.Where(x => x.Email == usuario.Email && x.Password == usuario.Password).FirstOrDefault();
			return ConsultaUsuario;
		}

		public void CrearPerfil(Usuario p)
		{
			Usuario UsuarioActual = ObtenerPorId(p.IdUsuario);

			if (p.UserName == null) { 
				var NombreUsuario = p.Nombre + "." + p.Apellido;

				var Consulta = ctx.Usuarios.Where(x => x.UserName == NombreUsuario).First();

				if (Consulta == null)
				{
					UsuarioActual.UserName = NombreUsuario;
				}
				else
				{
					//TODO: Si ya existe NombreUsuario identico, se le agrega 1 numero delante. Validar que no se repita tmb infinitas veces. For?
				}
			}
			UsuarioActual.Nombre = p.Nombre;
			UsuarioActual.Apellido = p.Apellido;
			UsuarioActual.Foto = p.Foto;
			ctx.SaveChanges();
		}

		private string NuevoCodigoDeActivacion()
		{
			Random random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

			string clave = new string(Enumerable.Repeat(chars, 8)
				.Select(s => s[random.Next(s.Length)]).ToArray()) + "-" +
				 new string(Enumerable.Repeat(chars, 4)
				.Select(s => s[random.Next(s.Length)]).ToArray()) + "-" +
				 new string(Enumerable.Repeat(chars, 4)
				.Select(s => s[random.Next(s.Length)]).ToArray()) + "-" +
				 new string(Enumerable.Repeat(chars, 4)
				.Select(s => s[random.Next(s.Length)]).ToArray()) + "-" +
				 new string(Enumerable.Repeat(chars, 12)
				.Select(s => s[random.Next(s.Length)]).ToArray());

			return clave;
		}
	}
}
