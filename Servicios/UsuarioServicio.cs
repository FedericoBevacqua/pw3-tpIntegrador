using Data;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Servicios
{
    public class UsuarioServicio
	{
        readonly TpEntities ctx = new TpEntities();

		public void Alta(Usuario u)
		{
			u.Token = NuevoCodigoDeActivacion(); //Token autogenerado
			u.FechaCracion = DateTime.Now;
			u.Activo = false;
			u.TipoUsuario = 0; // 0 = Normal | 1 = Administrador 
			ctx.Usuarios.Add(u);
			ctx.SaveChanges();
            EnviarEmailToken(u);
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

			if (String.IsNullOrEmpty(p.UserName)) {
				var NombreUsuario = $"{p.Nombre}.{p.Apellido}";

				bool usuarioExiste = ctx.Usuarios.Where(x => x.UserName == NombreUsuario).FirstOrDefault() != null;

                // Si el nombre de usuario ya existe
				if (usuarioExiste)
				{
                    // Obtiene el ultimo usuario creado que tenga el mismo nombre y apellido
                    Usuario ultimoUsuarioRepetido = ctx.Usuarios.Where(x => x.UserName.StartsWith(NombreUsuario)).OrderByDescending(u => u.IdUsuario).FirstOrDefault();

                    int numeroUsuarioConcatenado;
                    try
                    {
                        // Obtiene el numero al final del nombre del usuario y le suma 1
                        numeroUsuarioConcatenado = int.Parse(Regex.Match(ultimoUsuarioRepetido.UserName, @"\d+").Value, NumberFormatInfo.InvariantInfo) + 1;
                    }
                    // Si el usuario esta repetido pero no tiene un numero al final, debo asignarle el numero 1
                    catch (FormatException)
                    {
                        numeroUsuarioConcatenado = 1;
                    }
                    NombreUsuario += numeroUsuarioConcatenado;
                }

                UsuarioActual.UserName = NombreUsuario;
            }

			UsuarioActual.Nombre = p.Nombre;
			UsuarioActual.Apellido = p.Apellido;
			UsuarioActual.Foto = p.Foto;
			ctx.SaveChanges();
		}

		private string NuevoCodigoDeActivacion() //Generacion automatica del Token para posterior validacion
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

		public Usuario FindByEmail(String email)
		{
            var usuario = ctx.Usuarios.Where(x => x.Email == email).FirstOrDefault();
            return usuario;
		}

        public Usuario FindByToken(String token)
        {
            var usuario = ctx.Usuarios.Where(x => x.Token == token).FirstOrDefault();
            return usuario;
        }
        //Logica Envio de Email con su Token para activacion
        public void EnviarEmailToken(Usuario usuario)
		{
			MailMessage email = new MailMessage();

			email.To.Add(new MailAddress(usuario.Email));
			email.From = new MailAddress("pw3tpayudaprojimo@gmail.com");
			email.Subject = "Ayudando al prójimo - Activar Cuenta";
            email.Body = $"Usuario: {usuario.Nombre} Código de activación: {usuario.Token} Active su cuenta aquí: http://localhost:52413/Usuario/activar?token={usuario.Token}";
            email.IsBodyHtml = true;
			email.Priority = MailPriority.High;

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email.From.Address, "pw3tpfededani")
            };

            smtp.Send(email);
			email.Dispose();
		}

		public Usuario ActivarCuenta(string token)
		{
			Usuario usuario = FindByToken(token);
            if (usuario != null)
            {
                usuario.Activo = true;
                ctx.SaveChanges();
            }

            return usuario;
		}
	}
}
