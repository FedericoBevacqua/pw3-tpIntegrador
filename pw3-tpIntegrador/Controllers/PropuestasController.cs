using Data;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pw3_tpIntegrador.Controllers
{
    public class PropuestasController : Controller
    {
		PropuestaServicio Propuestas = new PropuestaServicio();

		[HttpGet]
		public ActionResult Crear()
        {
            return View();
        }
        [HttpPost]
		public ActionResult Crear(Propuesta p)
		{
			if (!ModelState.IsValid)
			{
				return View(p);
			}

			Propuestas.Alta(p);
			return Redirect("/Home/Inicio");
		}

		public ActionResult Detalle()
        {
            return View();
        }

        public ActionResult Denunciar()
        {
            return View();
        }

        public ActionResult Donar()
        {
            return View();
        }
    }
}