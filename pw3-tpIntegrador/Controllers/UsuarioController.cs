﻿using Data;
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
            return View();
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
					if (usuario.Activo == false)
					{
						ViewBag.msg = "Necesitas activar tu cuenta primero.";

						return View("Login");
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