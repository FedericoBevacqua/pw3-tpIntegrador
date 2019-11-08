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

		public ActionResult Login()
        {
            return View();
        }
		[HttpGet]
		public ActionResult Registro()
        {
            return View();
        }
		[HttpPost]
		public ActionResult Registro(Usuario u)
		{
			if (!ModelState.IsValid)
			{
				return View(u);
			}

			Usuarios.Alta(u);
			return Redirect("/Home/Inicio");

		}
		[HttpGet]
		public ActionResult CrearPerfil()
        {
            return View();
        }
		[HttpPost]
		public ActionResult CrearPerfil(Usuario p)
		{
			if (!ModelState.IsValid)
			{
				return View(p);
			}

			Usuarios.CrearPerfil(p);
			return Redirect("/Home/Inicio");
		}
	}
}