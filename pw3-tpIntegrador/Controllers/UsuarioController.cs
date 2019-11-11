using Data;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pw3_tpIntegrador.Controllers
{
    public class UsuarioController : Controller
    {
		UsuarioServicio Usuarios = new UsuarioServicio();

		[HttpGet]
		public ActionResult Login()
        {
			if (SesionServicio.UsuarioSession == null)
			{
				return View();
			}
			else
			{
				return View("InicioUsuarioLogueado");
			}
        }
		[HttpPost]
		public ActionResult Login(Usuario l)
		{
			if (!ModelState.IsValid)
			{
				return View("Login", l);
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

						return View("Login");
					}

                    // El usuario tiene que crear su perfil
                    if (String.IsNullOrEmpty(usuario.UserName)) 
                    {
                        return View("CrearPerfil", usuario);
                    }

					//Guardo usuario en Sesion
					SesionServicio.UsuarioSession = usuario;

					return Redirect("/Home/InicioUsuarioLogueado");
				}
				else
				{
					ViewData["MensajeError"] = "Usuario o contraseña incorrecto.";

					return View("Login", l);
				}
			}
		}
		[HttpGet]
		public ActionResult LogOut()
		{
			SesionServicio.UsuarioSession = null;
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
				return View("InicioUsuarioLogueado");
			}
        }
		[HttpPost]
		public ActionResult Registro(Usuario u)
		{
			if (!ModelState.IsValid)
			{
				return View(u);
			}
			//Validacion que no exista el mismo Email en BD
			var email = Usuarios.FindByEmail(u.Email);
			if(email == null)
			{
				Usuarios.Alta(u);
				return Redirect("/Home/Inicio"); //TODO: Hacer vista, se ha registrado con exito, recibira el email de activacion
			}
			else
			{
				ViewData["MensajeErrorEmail"] = "El Email ya esta en uso.";
				return View("Registro", u);
			}
		}

        [HttpGet]
        public ActionResult MiPerfil()
        {
            Usuario usuario = SesionServicio.UsuarioSession;

            if (usuario != null)
            {
                return View("MiPerfil", SesionServicio.UsuarioSession);
            } else
            {
                return View("Login");
            }
        }

        [HttpGet]
		public ActionResult CrearPerfil()
        {
			if(SesionServicio.UsuarioSession.UserName != null)
			{
				return View("InicioUsuarioLogueado");
			}

			if (SesionServicio.UsuarioSession == null)
			{
				return View("Inicio");
			}
			else
			{
				return View();
			}
        }
		[HttpPost]
		public ActionResult CrearPerfil(Usuario p)
		{
			if (!ModelState.IsValid)
			{
				return View(p);
			}

			Usuarios.CrearPerfil(p);
			return View("InicioUsuarioLogueado");
		}

        [HttpGet]
        public ActionResult Activar(string token)
        {
            Usuario usuario = Usuarios.ActivarCuenta(token);
            if (usuario != null)
            {
                ViewBag.msg = "Su usuario ha sido activado con éxito. Por favor, inicie sesión.";
                return View("Login");
            }
            
            return View(); //Error
        }
    }
}