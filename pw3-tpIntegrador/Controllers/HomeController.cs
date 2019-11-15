using Data;
using Servicios;
using System.Collections.Generic;
using System.Web.Mvc;

namespace pw3_tpIntegrador.Controllers
{
    public class HomeController : Controller
	{
        private readonly PropuestaServicio Propuestas = new PropuestaServicio();

        [HttpGet]
        public ActionResult Inicio(bool mostrarPropuestasInactivas = false)
		{
			if (SesionServicio.UsuarioSession == null) //Si esta el user logueado
			{
				return View(Propuestas.ObtenerCincoMejoresActivas()); //Obtiene las 5 mejores propuestas por valoracion activas y vuelve al Inicio
			}
			else
			{
				Usuario usuario = SesionServicio.UsuarioSession;
				if (usuario.UserName == null)
				{
					//TODO: Complete su perfil para acceder al sitio normalmente.
					return Redirect("/Usuario/CrearPerfil");
				}
				//Si estas logueado te envia a la vista InicioUsuarioLogueado con la lista de todas las propuestas activas
                List<Propuesta> propuestas = mostrarPropuestasInactivas ? Propuestas.ObtenerTodas() : Propuestas.ObtenerActivas();
				return View("InicioUsuarioLogueado", propuestas);
			}
		}
        //Manejo de errores
        public ActionResult Error(int error = 0)
        {
            switch (error)
            {
                case 505:
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Esto es muy vergonzoso, esperemos que no vuelva a pasar.";
                    break;

                case 404:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "La URL que está intentando ingresar no existe";
                    break;

                default:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "Algo salio muy mal. Si el problema persiste por favor póngase en contacto con el administrador";
                    break;
            }

            return View();
        }
    }
}