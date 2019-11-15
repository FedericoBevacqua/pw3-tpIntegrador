using Data;
using pw3_tpIntegrador.Utils;
using Servicios;
using System;
using System.Web.Mvc;

namespace pw3_tpIntegrador.Controllers
{
    public class UsuarioController : Controller
    {
        readonly UsuarioServicio Usuarios = new UsuarioServicio();

		[HttpGet]
		public ActionResult Login(String MensajeError, String Redirigir) //Si proviene de otra vista recibe la url y mensaje de error
        {

            if (SesionServicio.UsuarioSession == null)
			{
                if(MensajeError != null) ViewData["MensajeError"]= MensajeError;
                return View();
			}
			else
			{
                Redirigir = Redirigir == null ? "/Home/Inicio" : Redirigir;
                return Redirect(Redirigir); //Redirigue a la vista de donde provenia
            }

        }
		[HttpPost]
		public ActionResult Login(Usuario l, string url)
		{
			if (!ModelState.IsValid)
			{
				return View(l);
			}
			else
			{
				Usuario usuario = Usuarios.ValidarLogin(l);


				//Comprueba que exista el usuario con ese Email y Contraseña
				if (usuario != null)
				{

					//No permitiendo logearse a un user inactivo
					if (!usuario.Activo)
					{
						ViewBag.msg = "Su usuario está inactivo. Actívelo desde el email recibido.";

						return View();
					}

                    //Guardo usuario en Sesion
                    SesionServicio.UsuarioSession = usuario;

                    // El usuario tiene que crear su perfil //Al iniciar por primera vez y no tener perfil, te obliga a crearlo.
                    if (String.IsNullOrEmpty(usuario.UserName)) 
                    {
                        return Redirect("/Usuario/CrearPerfil");
                    }
					if (!string.IsNullOrEmpty(url))
					{
						//Si la url no es nula redirigue donde deseaba ir anteriormente.
						return Redirect(url);
					}

					return Redirect("/Home/Inicio");
				}
				else
				{
					ViewData["MensajeError"] = "Usuario o contraseña incorrecto.";

					return View(l);
				}
			}
		}
		[HttpGet]
		public ActionResult LogOut()
		{
			SesionServicio.UsuarioSession = null;   //Borra la sesion actual
			return RedirectToAction("Inicio", "Home");
		}
		[HttpGet]
		public ActionResult Registro()
        {
			if (SesionServicio.UsuarioSession == null)
			{
				return View();
			}
			else
			{
				return Redirect("/Home/Inicio");
            }
        }
		[HttpPost]
		public ActionResult Registro(Usuario u)
		{
			if (!ModelState.IsValid)
			{
				return View(u);
			}
			//Validacion que no exista el mismo Email en BD para luego crearlo
			var email = Usuarios.FindByEmail(u.Email);
			if(email == null) 
			{
				Usuarios.Alta(u);
				return Redirect("/Home/Inicio"); //TODO: Hacer vista, se ha registrado con exito, recibira el email de activacion
			}
			else
			{
				ViewData["MensajeErrorEmail"] = "El Email ya esta en uso.";
				return View(u);
			}
		}

        [HttpGet]
        public ActionResult MiPerfil()
        {
            Usuario usuario = SesionServicio.UsuarioSession;

			if(usuario.UserName == null)
			{
				return Redirect("/Usuario/CrearPerfil");
			}
            if (usuario != null)
            {
                return View(usuario);
            } else
            {
                return Redirect("/Usuario/Login");
            }
        }

        [HttpGet]
		public ActionResult CrearPerfil()
        {
            Usuario usuario = SesionServicio.UsuarioSession;

            if (usuario == null || usuario.UserName != null) //Si esta logueado y tiene username - vuelve al inicio
			{
				return Redirect("/Home/Inicio");
            }
            else
			{
				return View(usuario);
			}
        }
		[HttpPost]
		public ActionResult CrearPerfil(Usuario p)
		{
			if (!ModelState.IsValid)
			{
				return View(p);
			}
			Usuario usuario = SesionServicio.UsuarioSession;
			if (usuario.UserName == null) { 
				//Logica para guardar la Foto
				if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0 && Request.Files[0].ContentType.Contains("image"))
				{
					string nombreSignificativo = p.UserName + DateTime.Now.ToString();
					string pathRelativoImagen = ImagenesUtility.Guardar(Request.Files[0], nombreSignificativo);
					p.Foto = pathRelativoImagen;
				}

				Usuarios.CrearPerfil(p);

				//Actualizo sesion
				Usuario user = Usuarios.ObtenerPorId(p.IdUsuario);
				SesionServicio.UsuarioSession = user;

				return Redirect("/Home/Inicio");
			}
			//TODO: Mensaje error ya tiene username creado.
			return Redirect("/Home/Inicio");

		}

		[HttpGet]
        public ActionResult Activar(string token)
        {
            Usuario usuario = Usuarios.ActivarCuenta(token);
            if (usuario != null)
            {
                //TODO: Agregar mensaje al redirigir o pantalla intermedia
                ViewBag.msg = "Su usuario ha sido activado con éxito. Por favor, inicie sesión.";
                return Redirect("/Usuario/Login");
            }
            
            return View(); //Error
        }
		[HttpGet]
		public ActionResult AcercaDe()
		{
			if (SesionServicio.UsuarioSession != null)
			{
				return View();
			}
			else
			{
				return Redirect("/Home/Inicio");
			}
		}
	}
}