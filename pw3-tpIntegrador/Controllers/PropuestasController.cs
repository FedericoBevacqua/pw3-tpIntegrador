using Data;
using Servicios;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace pw3_tpIntegrador.Controllers
{
    public class PropuestasController : Controller
    {
        readonly PropuestaServicio Propuestas = new PropuestaServicio();

		[HttpGet]
		public ActionResult Crear()
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

            switch(TipoDonacion)
            {
                case 1:
                    NuevaPropuesta = new PropuestasDonacionesMonetaria();
                    ViewName = "CrearTipoMonetaria";
                    break;
                case 2:
                    NuevaPropuesta = new Propuesta();
                    ViewName = "CrearTipoInsumos";
                    break;
                default:
                    NuevaPropuesta = new PropuestasDonacionesHorasTrabajo();
                    ViewName = "CrearTipoHorasTrabajo";
                    break;
            }

            NuevaPropuesta = ExtraerInformacionComun(form, NuevaPropuesta);

            ViewBag.Referencia1Nombre = form["Referencia1Nombre"];
            ViewBag.Referencia1Telefono = form["Referencia1Telefono"];

            ViewBag.Referencia2Nombre = form["Referencia2Nombre"];
            ViewBag.Referencia2Telefono = form["Referencia2Telefono"];

            return View(ViewName, NuevaPropuesta);
        }

        [HttpPost]
        public ActionResult ProcesarTipoMonetaria(FormCollection form)
        {
            PropuestasDonacionesMonetaria p = (PropuestasDonacionesMonetaria) ExtraerInformacionComun(form, new PropuestasDonacionesMonetaria());

            p.Dinero = Decimal.Parse(form["Dinero"]);
            p.CBU = form["CBU"];

            Propuestas.Alta(p);
            return Redirect("/Home/Inicio");
        }

        public ActionResult ProcesarTipoInsumos(FormCollection form)
        {
            Propuesta p = ExtraerInformacionComun(form, new Propuesta());
            List<PropuestasDonacionesInsumo> listaInsumos = ExtraerListaInsumos(form);

            Propuestas.Alta(p, listaInsumos);
            return Redirect("/Home/Inicio");
        }

        public ActionResult ProcesarTipoHorasTrabajo(FormCollection form)
        {
            PropuestasDonacionesHorasTrabajo p = (PropuestasDonacionesHorasTrabajo) ExtraerInformacionComun(form, new PropuestasDonacionesHorasTrabajo());

            p.CantidadHoras = Int32.Parse(form["CantidadHoras"]);
            p.Profesion = form["Profesion"];

            Propuestas.Alta(p);
            return Redirect("/Home/Inicio");
        }

		public ActionResult Detalle(int Id)
        {
            Propuesta p = Propuestas.ObtenerPorId(Id);

            return View(p);
        }
		[HttpGet]
		public ActionResult Denunciar(int Id)
		{
			return View(Propuestas.ObtenerPorId(Id));
		}
		[HttpPost]
		public ActionResult Denunciar(Denuncia d)
		{
			if (!ModelState.IsValid)
			{
				return View(d);
			}

			Propuestas.CrearDenuncia(d);
			return Redirect("/Home/InicioUsuarioLogueado");
		}


		public ActionResult Donar(int Id)
        {
            Propuesta p = Propuestas.ObtenerPorId(Id);

            if(p.TipoDonacion == 1)
            {
                return View("DonarTipoMonetaria", p);
            }
            else if (p.TipoDonacion == 2)
            {
                return View("DonarTipoInsumos", p);
            }
            else
            {
                return View("DonarTipoHorasTrabajo", p);
            }
        }

        [HttpPost]
        public ActionResult DonarMonetaria(DonacionesMonetaria d)
        {
            Propuestas.GuardarDonacion(d);
            return Redirect("/Home/Inicio");
        }

        [HttpPost]
        public ActionResult DonarInsumos(FormCollection form)
        {
            List<DonacionesInsumo> donaciones = new List<DonacionesInsumo>();
            DonacionesInsumo d;

            for(int i=0; i< Int32.Parse(form["CantidadInsumos"]); i++)
            {
                d = new DonacionesInsumo();
                d.Cantidad = Int32.Parse(form["Cantidad[" + i + "]"]);           
                d.IdUsuario = Int32.Parse(form["IdUsuario"]);
                d.IdPropuestaDonacionInsumo = Int32.Parse(form["IdPropuestaDonacionInsumo[" + i + "]"]);

                donaciones.Add(d);
            }

            Propuestas.GuardarDonacion(donaciones);
            return Redirect("/Home/Inicio");
        }

        [HttpPost]
        public ActionResult DonarHorasTrabajo(DonacionesHorasTrabajo d)
        {
            Propuestas.GuardarDonacion(d);
            return Redirect("/Home/Inicio");
        }








        //Helpers privados
        private Propuesta ExtraerInformacionComun(FormCollection form, Propuesta p)
        {
            p.Nombre = form["Nombre"];
            p.Descripcion = form["Descripcion"];
            p.FechaFin = System.DateTime.Parse(form["FechaFin"]);
            p.TelefonoContacto = form["TelefonoContacto"];
            p.TipoDonacion = Int32.Parse(form["TipoDonacion"]);
            p.Foto = form["Foto"];

            PropuestasReferencia referencia1 = new PropuestasReferencia();
            referencia1.Nombre = form["Referencia1Nombre"];
            referencia1.Telefono = form["Referencia1Telefono"];

            PropuestasReferencia referencia2 = new PropuestasReferencia();
            referencia2.Nombre = form["Referencia2Nombre"];
            referencia2.Telefono = form["Referencia2Telefono"];

            p.PropuestasReferencias.Add(referencia1);
            p.PropuestasReferencias.Add(referencia2);

            return p;
        }

        private List<PropuestasDonacionesInsumo> ExtraerListaInsumos(FormCollection form)
        {
            List<PropuestasDonacionesInsumo> listaInsumos = new List<PropuestasDonacionesInsumo>();

            int cantidadCompras = Int32.Parse(form["CantidadInsumos"]);
            PropuestasDonacionesInsumo donacion;

            for (int i = 0; i < cantidadCompras; i++)
            {
                donacion = new PropuestasDonacionesInsumo();
                donacion.Nombre = form["Nombres[" + i + "]"];
                donacion.Cantidad = Int32.Parse(form["Cantidad[" + i + "]"]);
                listaInsumos.Add(donacion);
            }

            return listaInsumos;
        }
    }
}