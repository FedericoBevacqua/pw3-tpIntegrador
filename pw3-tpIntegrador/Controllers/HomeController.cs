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
    }
}