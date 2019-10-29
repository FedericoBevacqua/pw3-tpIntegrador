using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pw3_tpIntegrador.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Inicio()
		{
			return View();
		}

        //Eliminar este action cuando se agregue la logica que hace el redirect desde el action Inicio()
        public ActionResult InicioUsuarioLogueado()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Buscar(String Texto)
        {
            return View();
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