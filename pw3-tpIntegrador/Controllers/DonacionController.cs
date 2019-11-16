using Servicios;
using Servicios.DTO.Donacion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace pw3_tpIntegrador.Controllers
{
    public class DonacionController : Controller
    {
		[HttpGet]
		public ActionResult Historial()
        {
			if (SesionServicio.UsuarioSession == null)
			{
				return Redirect("/Home/Inicio");
            }
			else
			{
                ViewBag.IdUsuario = SesionServicio.UsuarioSession.IdUsuario;
				return View();
			}
        }

    }
}