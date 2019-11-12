using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Servicios
{
	public static class SesionServicio
	{
		internal static readonly object UsuarioLogin;

		public static Usuario UsuarioSession
		{
			get
			{
				return HttpContext.Current.Session["UserSession"] as Usuario;
			}
			set
			{
				HttpContext.Current.Session["UserSession"] = value;
			}
		}

        public static bool IsAdmin => UsuarioSession != null && UsuarioSession.TipoUsuario == 1;
    }
}
