using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

			string clave = new string(Enumerable.Repeat(chars, 4)
				.Select(s => s[random.Next(s.Length)]).ToArray()) +
				 new string(Enumerable.Repeat(chars, 4)
				.Select(s => s[random.Next(s.Length)]).ToArray()) +
				 new string(Enumerable.Repeat(chars, 4)
				.Select(s => s[random.Next(s.Length)]).ToArray()) +
				 new string(Enumerable.Repeat(chars, 4)
				.Select(s => s[random.Next(s.Length)]).ToArray());

			return clave;
		}

		public Usuario FindByEmail(Usuario email)
		{
			var ConsultaEmail = ctx.Usuarios.Where(x => x.Email == email.Email).First();
			return ConsultaEmail;
		}

		public void EnviarEmailToken(Usuario usuario)
		{
			MailMessage email = new MailMessage();

			email.To.Add(new MailAddress(usuario.Email));
			email.From = new MailAddress("pw3tpayudaprojimo@gmail.com");
			email.Subject = "Asunto (Validar Cuenta)";
			email.Body = "Usuario: " + usuario.Nombre + "  Codigo de activacion: " + usuario.Token + "   active su cuenta aquí: http://localhost:49525/Usuario/activar?token=" + usuario.Token;
			email.IsBodyHtml = true;
			email.Priority = MailPriority.Normal;

			SmtpClient smtp = new SmtpClient();


			smtp.Host = "smtp.gmail.com";
			smtp.Port = 587;

			smtp.EnableSsl = true;
			smtp.UseDefaultCredentials = false;

			smtp.Credentials = new NetworkCredential(email.From.Address, "pw3tpfededani");

			smtp.Send(email);
			email.Dispose();
		}
	}
}
