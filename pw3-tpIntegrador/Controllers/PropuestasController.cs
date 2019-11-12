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

        [HttpGet]
        public ActionResult Modificar(int id)
        {
            if (SesionServicio.UsuarioSession == null)
            {
                return Redirect("/Home/Inicio");
            }
            else
            {
                // TODO: Solo puedo editar mis propuestas o ser Admin
                return View(Propuestas.ObtenerPorId(id));
            }
        }

        [HttpPost]
        public ActionResult Modificar(FormCollection form)
        {
            int idPropuesta = int.Parse(form["idPropuesta"]);
            Propuesta propuestaAModificar = Propuestas.ObtenerPorId(idPropuesta);



            propuestaAModificar.Nombre = form["Nombre"];
            propuestaAModificar.Descripcion = form["Descripcion"];
            propuestaAModificar.FechaFin = System.DateTime.Parse(form["FechaFin"]);
            propuestaAModificar.TelefonoContacto = form["TelefonoContacto"];
            propuestaAModificar.TipoDonacion = int.Parse(form["TipoDonacion"]);
            propuestaAModificar.Foto = form["Foto"];

            //TODO: Modificar Referencias 1 y 2

            //TODO: ctx.SaveChanges();

            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                //TODO: Agregar validacion para confirmar que el archivo es una imagen
                string nombreSignificativo = form["Nombre"] + DateTime.Now.ToString();
                string pathRelativoImagen = ImagenesUtility.Guardar(Request.Files[0], nombreSignificativo);
                propuestaAModificar.Foto = pathRelativoImagen;
            }

            switch (propuestaAModificar.TipoDonacion)
            {
                case 1: //TipoMonetaria
                    ((PropuestasDonacionesMonetaria)propuestaAModificar).Dinero = decimal.Parse(form["Dinero"]);
                    ((PropuestasDonacionesMonetaria)propuestaAModificar).CBU = form["CBU"];
                    break;
                case 2: //TipoInsumos
                    
                    //List<PropuestasDonacionesInsumo> listaInsumos = ExtraerListaInsumos(form);
                    //((PropuestasDonacionesInsumo)propuestaAModificar).DonacionesInsumos = listaInsumos;
                    break;
                case 3: //TipoHorasTrabajo
                    ((PropuestasDonacionesHorasTrabajo)propuestaAModificar).CantidadHoras = int.Parse(form["CantidadHoras"]);
                    ((PropuestasDonacionesHorasTrabajo)propuestaAModificar).Profesion = form["Profesion"];
                    break;
            }

            Propuestas.Modificar(idPropuesta, propuestaAModificar);

            //TODO: Actualizar Propuesta con sus modificaciones...

            //return CrearPaso2(form);
            return Redirect("/Home/Inicio");
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

        [HttpPost]
        public ActionResult ModificarTipoMonetaria(FormCollection form)
        {
            PropuestasDonacionesMonetaria p = (PropuestasDonacionesMonetaria)ExtraerInformacionComun(form, new PropuestasDonacionesMonetaria());

            p.Dinero = Decimal.Parse(form["Dinero"]);
            p.CBU = form["CBU"];

            //TODO: Editar
            //Propuestas.Alta(p);
            return Redirect("/Home/Inicio");
        }

        public ActionResult ModificarTipoInsumos(FormCollection form)
        {
            Propuesta p = ExtraerInformacionComun(form, new Propuesta());
            List<PropuestasDonacionesInsumo> listaInsumos = ExtraerListaInsumos(form);

            //TODO: Editar
            // Propuestas.Alta(p, listaInsumos);
            return Redirect("/Home/Inicio");
        }

        public ActionResult ModificarTipoHorasTrabajo(FormCollection form)
        {
            PropuestasDonacionesHorasTrabajo p = (PropuestasDonacionesHorasTrabajo)ExtraerInformacionComun(form, new PropuestasDonacionesHorasTrabajo());

            p.CantidadHoras = Int32.Parse(form["CantidadHoras"]);
            p.Profesion = form["Profesion"];
            
            //TODO: Editar
            //Propuestas.Alta(p);
            return Redirect("/Home/Inicio");
        }

        public ActionResult Detalle(int Id)
        {
            if (SesionServicio.UsuarioSession == null)
            {
                return RedirectToAction("Login", "Usuario", new
                {
                    Redirigir = "/Propuestas/Detalle/" + Id.ToString(),
                    MensajeError = "Debés iniciar sesión para continuar"
                }); ;
            } else
            {
                Propuesta p = Propuestas.ObtenerPorId(Id);
                return View(p);
            }
                

            
        }
		
		public ActionResult Denunciar(int Id)
		{
            Propuesta PropuestaDenunciada = Propuestas.ObtenerPorId(Id);

            ViewBag.Img = PropuestaDenunciada.Foto;
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
		[HttpGet]
		public ActionResult MeGusta(int Id)
		{
			Propuestas.ValorarMeGusta(Id);
			Propuestas.CalcularValoracionTotal(Id);

			return Redirect("/Home/Inicio");
		}

		[HttpGet]
		public ActionResult NoMeGusta(int Id)
		{
			Propuestas.ValorarNoMeGusta(Id);
			Propuestas.CalcularValoracionTotal(Id);

			return Redirect("/Home/Inicio");
		}
	}
}