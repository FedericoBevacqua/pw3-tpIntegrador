using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pw3_tpIntegrador.Controllers
{
    public class DenunciasController : Controller
    {
		[HttpGet]
		public ActionResult Gestionar()
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

    }
}