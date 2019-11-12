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
		public ActionResult Login()
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
		public ActionResult Login(Usuario l)
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

                    // El usuario tiene que crear su perfil
                    if (String.IsNullOrEmpty(usuario.UserName)) 
                    {
                        return Redirect("/Usuario/CrearPerfil");
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
				return View(u);
			}
		}

        [HttpGet]
        public ActionResult MiPerfil()
        {
            Usuario usuario = SesionServicio.UsuarioSession;

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

            if (usuario == null || usuario.UserName != null)
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

            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0 && Request.Files[0].ContentType.Contains("image"))
            {
                string nombreSignificativo = p.UserName + DateTime.Now.ToString();
                string pathRelativoImagen = ImagenesUtility.Guardar(Request.Files[0], nombreSignificativo);
                p.Foto = pathRelativoImagen;
            }

            Usuarios.CrearPerfil(p);
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