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
		public ActionResult Crear(FormCollection form)
		{
			/*if (!ModelState.IsValid)
			{
				return View(form);
			}*/

			return CrearPaso2(form);
		}

        [HttpPost]
        public ActionResult CrearPaso2(FormCollection form)
        {
            int TipoDonacion = Int32.Parse(form["TipoDonacion"]);
            Propuesta NuevaPropuesta;
            String ViewName;

            if (TipoDonacion == 1)
            {
                NuevaPropuesta = new PropuestasDonacionesMonetaria();
                ViewName = "CrearTipoMonetaria";
            } 
            else if (TipoDonacion == 2)
            {
                NuevaPropuesta = new Propuesta();
                ViewName = "CrearTipoInsumos";
            } 
            else
            {
                NuevaPropuesta = new PropuestasDonacionesHorasTrabajo();
                ViewName = "CrearTipoHorasTrabajo";
            }

            NuevaPropuesta.Nombre = form["Nombre"];
            NuevaPropuesta.Descripcion = form["Descripcion"];
            NuevaPropuesta.FechaFin = System.DateTime.Parse(form["FechaFin"]);
            NuevaPropuesta.TelefonoContacto = form["TelefonoContacto"];
            NuevaPropuesta.TipoDonacion = Int32.Parse(form["TipoDonacion"]);
            NuevaPropuesta.Foto = form["Foto"];

            ViewBag.Referencia1Nombre = form["Referencia1Nombre"];
            ViewBag.Referencia1Telefono = form["Referencia1Telefono"];

            ViewBag.Referencia2Nombre = form["Referencia2Nombre"];
            ViewBag.Referencia2Telefono = form["Referencia2Telefono"];

            return View(ViewName, NuevaPropuesta);
        }

        [HttpPost]
        public ActionResult ProcesarTipoMonetaria(FormCollection form)
        {
            PropuestasDonacionesMonetaria p = new PropuestasDonacionesMonetaria();

            p.Nombre = form["Nombre"];
            p.Descripcion = form["Descripcion"];
            p.FechaFin = System.DateTime.Parse(form["FechaFin"]);
            p.TelefonoContacto = form["TelefonoContacto"];
            p.TipoDonacion = Int32.Parse(form["TipoDonacion"]);
            p.Foto = form["Foto"];

            p.Dinero = Decimal.Parse(form["Dinero"]);
            p.CBU = form["CBU"];

            PropuestasReferencia referencia1 = new PropuestasReferencia();
            referencia1.Nombre = form["Referencia1Nombre"];
            referencia1.Telefono = form["Referencia1Telefono"];

            PropuestasReferencia referencia2 = new PropuestasReferencia();
            referencia2.Nombre = form["Referencia2Nombre"];
            referencia2.Telefono = form["Referencia2Telefono"];

            p.PropuestasReferencias.Add(referencia1);
            p.PropuestasReferencias.Add(referencia2);

            Propuestas.Alta(p);
            return Redirect("/Home/Inicio");
        }

        public ActionResult ProcesarTipoInsumos(FormCollection form)
        {
            Propuesta p = new Propuesta();

            p.Nombre = form["Nombre"];
            p.Descripcion = form["Descripcion"];
            p.FechaFin = System.DateTime.Parse(form["FechaFin"]);
            p.TelefonoContacto = form["TelefonoContacto"];
            p.TipoDonacion = Int32.Parse(form["TipoDonacion"]);
            p.Foto = form["Foto"];

            int cantidadCompras = Int32.Parse(form["CantidadInsumos"]);
            List<PropuestasDonacionesInsumo> listaDonaciones = new List<PropuestasDonacionesInsumo>();
            PropuestasDonacionesInsumo donacion;

            for(int i = 0; i<cantidadCompras; i++)
            {
                donacion = new PropuestasDonacionesInsumo();
                donacion.Nombre = form["Nombres[" + i + "]"];
                donacion.Cantidad = Int32.Parse(form["Cantidad[" + i + "]"]);
                listaDonaciones.Add(donacion);
            }

            PropuestasReferencia referencia1 = new PropuestasReferencia();
            referencia1.Nombre = form["Referencia1Nombre"];
            referencia1.Telefono = form["Referencia1Telefono"];

            PropuestasReferencia referencia2 = new PropuestasReferencia();
            referencia2.Nombre = form["Referencia2Nombre"];
            referencia2.Telefono = form["Referencia2Telefono"];

            p.PropuestasReferencias.Add(referencia1);
            p.PropuestasReferencias.Add(referencia2);

            Propuestas.Alta(p, listaDonaciones);
            return Redirect("/Home/Inicio");
        }

        public ActionResult ProcesarTipoHorasTrabajo(FormCollection form)
        {
            PropuestasDonacionesHorasTrabajo p = new PropuestasDonacionesHorasTrabajo();

            p.Nombre = form["Nombre"];
            p.Descripcion = form["Descripcion"];
            p.FechaFin = System.DateTime.Parse(form["FechaFin"]);
            p.TelefonoContacto = form["TelefonoContacto"];
            p.TipoDonacion = Int32.Parse(form["TipoDonacion"]);
            p.Foto = form["Foto"];

            p.CantidadHoras = Int32.Parse(form["CantidadHoras"]);
            p.Profesion = form["Profesion"];

            PropuestasReferencia referencia1 = new PropuestasReferencia();
            referencia1.Nombre = form["Referencia1Nombre"];
            referencia1.Telefono = form["Referencia1Telefono"];

            PropuestasReferencia referencia2 = new PropuestasReferencia();
            referencia2.Nombre = form["Referencia2Nombre"];
            referencia2.Telefono = form["Referencia2Telefono"];

            p.PropuestasReferencias.Add(referencia1);
            p.PropuestasReferencias.Add(referencia2);

            Propuestas.Alta(p);
            return Redirect("/Home/Inicio");
        }

		public ActionResult Detalle()
        {
            return View();
        }
		[HttpGet]
		public ActionResult Denunciar()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Denunciar(Denuncia d)
		{
			if (!ModelState.IsValid)
			{
				return View(d);
			}

			Propuestas.CrearDenuncia(d);
			return Redirect("/Home/Inicio");
		}


		public ActionResult Donar()
        {
            return View();
        }
    }
}