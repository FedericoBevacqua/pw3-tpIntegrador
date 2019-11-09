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

			return CrearPaso2(p);
		}

        [HttpPost]
        public ActionResult CrearPaso2(Propuesta p)
        {
            Propuesta NuevaPropuesta;
            String ViewName;

            if (p.TipoDonacion == 1)
            {
                NuevaPropuesta = new PropuestasDonacionesMonetaria();
                ViewName = "CrearTipoMonetaria";
            } 
            else if (p.TipoDonacion == 2)
            {
                NuevaPropuesta = new Propuesta();
                ViewName = "CrearTipoInsumos";
            } 
            else
            {
                NuevaPropuesta = new PropuestasDonacionesHorasTrabajo();
                ViewName = "CrearTipoHorasTrabajo";
            }

            NuevaPropuesta.Nombre = p.Nombre;
            NuevaPropuesta.Descripcion = p.Descripcion;
            NuevaPropuesta.FechaFin = p.FechaFin;
            NuevaPropuesta.TelefonoContacto = p.TelefonoContacto;
            NuevaPropuesta.TipoDonacion = p.TipoDonacion;
            NuevaPropuesta.Foto = p.Foto;

            /*
            NuevaPropuesta.NombreReferencia1 = p.NombreReferencia1;
            NuevaPropuesta.TelefonoReferencia1 = p.TelefonoReferencia1;
            NuevaPropuesta.NombreReferencia2 = p.NombreReferencia2;
            NuevaPropuesta.TelefonoReferencia2 = p.TelefonoReferencia2;
            */

            return View(ViewName, NuevaPropuesta);
        }

        [HttpPost]
        public ActionResult ProcesarTipoMonetaria(PropuestasDonacionesMonetaria  p)
        {
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

            Propuestas.Alta(p, listaDonaciones);
            return Redirect("/Home/Inicio");
        }

        public ActionResult ProcesarTipoHorasTrabajo(PropuestasDonacionesHorasTrabajo p)
        {
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