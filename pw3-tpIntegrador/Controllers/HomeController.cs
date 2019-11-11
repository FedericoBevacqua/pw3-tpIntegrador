using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;

namespace pw3_tpIntegrador.Controllers
{
	public class HomeController : Controller
	{
        private PropuestaServicio Propuestas = new PropuestaServicio();
        public ActionResult Inicio()
		{
			if (SesionServicio.UsuarioSession == null)
			{
				return View(Propuestas.ObtenerCincoMejoresActivas());
			}
			else
			{
				return View("InicioUsuarioLogueado");
			}
		}

        //Eliminar este action cuando se agregue la logica que hace el redirect desde el action Inicio()
        public ActionResult InicioUsuarioLogueado()
        {
			if (SesionServicio.UsuarioSession == null)
			{
				return View("Inicio");
			}
			else
			{
				return View();
			}
        }

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