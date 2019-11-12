using Data;
using Servicios;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using pw3_tpIntegrador.Utils;

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
				return Redirect("/Home/Inicio");
            }
			else
			{
                //El Usuario solo puede crear una nueva propuesta si tiene menos de 3 propuestas activas
                if (Propuestas.CantidadPropuestasActivasPorUsuario(SesionServicio.UsuarioSession.IdUsuario) < 3 || SesionServicio.IsAdmin)
                {
                    return View();
                }
                // TODO: Mostrar mensaje de error
                return Redirect("/Home/Inicio");
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
            Usuario Usuario = SesionServicio.UsuarioSession;
            if (Usuario == null)
            {
                return RedirectToAction("Login", "Usuario", new
                {
                    Redirigir = "/Propuestas/Detalle/" + Id.ToString(),
                    MensajeError = "Debés iniciar sesión para continuar"
                }); ;
            } else
            {
                ViewBag.UsuarioActualId = Usuario.IdUsuario;
                Propuesta p = Propuestas.ObtenerPorId(Id);
                return View(p);
            }
                

            
        }
		
		public ActionResult Denunciar(int Id)
		{
            Propuesta PropuestaDenunciada = Propuestas.ObtenerPorId(Id);

            ViewBag.Img = PropuestaDenunciada.Foto;
            ViewBag.NombrePropuesta = PropuestaDenunciada.Nombre;
            ViewBag.UsuarioDenunciado = PropuestaDenunciada.Usuario.UserName;
            ViewBag.IdUsuario = PropuestaDenunciada.Usuario.IdUsuario;
            ViewBag.FechaCreacion = DateTime.Now;
            ViewBag.IdPropuesta = Id;

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

		public ActionResult Donar(int Id)
        {
            Propuesta p = Propuestas.ObtenerPorId(Id);
            Usuario usuario = SesionServicio.UsuarioSession;

            // Si el usuario no es el mismo que creo la propuesta o es Admin puede donar
            if (usuario != null && usuario.IdUsuario != p.IdUsuarioCreador || SesionServicio.IsAdmin)
            {
                switch(p.TipoDonacion)
                {
                    case 1:
                        return View("DonarTipoMonetaria", p);

                    case 2:
                        return View("DonarTipoInsumos", p);

                    case 3:
                        return View("DonarTipoHorasTrabajo", p);
                }

            }

            return Redirect("/Home/Inicio");
        }

        [HttpPost]
        public ActionResult DonarMonetaria(DonacionesMonetaria d)
        {

            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0 && Request.Files[0].ContentType.Contains("image"))
            {
                string nombreSignificativo = d.ArchivoTransferencia + DateTime.Now.ToString();
                string pathRelativoImagen = ImagenesUtility.Guardar(Request.Files[0], nombreSignificativo);
                d.ArchivoTransferencia = pathRelativoImagen;
            }

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

            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                //TODO: Agregar validacion para confirmar que el archivo es una imagen
                string nombreSignificativo = form["Nombre"] + DateTime.Now.ToString();
                string pathRelativoImagen = ImagenesUtility.Guardar(Request.Files[0], nombreSignificativo);
                p.Foto = pathRelativoImagen;
            }

            

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
        [HttpPost]
        public ActionResult Buscar(string keyword)
        {
            var resultado = Propuestas.BuscarPorNombreYUsuario(keyword);
            return View(resultado);
        }

        [HttpPost]
        public decimal MeGusta(int Id)
        {
            Propuestas.ValorarMeGusta(Id);
            return Propuestas.CalcularValoracionTotal(Id);
        }

        [HttpPost]
		public decimal NoMeGusta(int Id)
		{
			Propuestas.ValorarNoMeGusta(Id);
			return Propuestas.CalcularValoracionTotal(Id);
		}
	}
}