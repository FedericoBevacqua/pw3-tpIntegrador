using Data;
using Servicios;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using pw3_tpIntegrador.Utils;
using System.Linq;

namespace pw3_tpIntegrador.Controllers
{
    public class PropuestasController : Controller
    {
        readonly PropuestaServicio Propuestas = new PropuestaServicio();

		[HttpGet]
		public ActionResult Crear()
        {
			if (SesionServicio.UsuarioSession == null) //Si no esta logueado
			{
				//TODO: Mensaje error no logueado
				return Redirect("/Home/Inicio");
            }
			else
			{
				if (SesionServicio.UsuarioSession.UserName == null) //Si no tiene username creado, no podra crear Propuesta. 
				{
					//TODO: Mensaje error : Debe crear su perfil primero
					return Redirect("/Usuario/CrearPerfil");
				}
			}
			//El Usuario solo puede crear una nueva propuesta si tiene menos de 3 propuestas activas. El admin puede crear mas de 3
			if (Propuestas.CantidadPropuestasActivasPorUsuario(SesionServicio.UsuarioSession.IdUsuario) < 3 || SesionServicio.IsAdmin)
            {
				return View();
            }
                // TODO: Mostrar mensaje de error
                return Redirect("/Home/Inicio");
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
            int tipoDonacion = int.Parse(form["TipoDonacion"]);

            Propuesta propuestaAModificar = Propuestas.ObtenerPorId(idPropuesta);

            //Logica Envio Imagen
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                //TODO: Agregar validacion para confirmar que el archivo es una imagen
                string nombreSignificativo = form["Nombre"] + DateTime.Now.ToString();
                string pathRelativoImagen = ImagenesUtility.Guardar(Request.Files[0], nombreSignificativo);
                propuestaAModificar.Foto = pathRelativoImagen;
            }

            switch (tipoDonacion) //Modifica segun tipo de donacion
            {
                case 1: //TipoMonetaria
                    PropuestasDonacionesMonetaria pam = propuestaAModificar.PropuestasDonacionesMonetarias.FirstOrDefault();

                    pam.Nombre = form["Nombre"];
                    pam.Descripcion = form["Descripcion"];
                    pam.FechaFin = System.DateTime.Parse(form["FechaFin"]);
                    pam.TelefonoContacto = form["TelefonoContacto"];
                    pam.TipoDonacion = int.Parse(form["TipoDonacion"]);
                    pam.Foto = propuestaAModificar.Foto;

                    pam.Dinero = decimal.Parse(form["Dinero"]);
                    pam.CBU = form["CBU"];

                    Propuestas.Modificar(idPropuesta, pam);
                    break;

                case 2: //TipoInsumos
                    PropuestasDonacionesInsumo pi = propuestaAModificar.PropuestasDonacionesInsumos.FirstOrDefault();

                    pi.Nombre = form["Nombre"];
                    pi.Descripcion = form["Descripcion"];
                    pi.FechaFin = System.DateTime.Parse(form["FechaFin"]);
                    pi.TelefonoContacto = form["TelefonoContacto"];
                    pi.TipoDonacion = int.Parse(form["TipoDonacion"]);
                    pi.Foto = propuestaAModificar.Foto;

                    //TODO: Copiar lista de insumos modificada
                    //List<PropuestasDonacionesInsumo> listaInsumos = ExtraerListaInsumos(form);
                    //((PropuestasDonacionesInsumo)propuestaAModificar).DonacionesInsumos = listaInsumos;
                    Propuestas.Modificar(idPropuesta, pi);
                    break;

                case 3: //TipoHorasTrabajo
                    PropuestasDonacionesHorasTrabajo pdt = propuestaAModificar.PropuestasDonacionesHorasTrabajoes.FirstOrDefault();

                    pdt.Nombre = form["Nombre"];
                    pdt.Descripcion = form["Descripcion"];
                    pdt.FechaFin = System.DateTime.Parse(form["FechaFin"]);
                    pdt.TelefonoContacto = form["TelefonoContacto"];
                    pdt.TipoDonacion = int.Parse(form["TipoDonacion"]);
                    pdt.Foto = propuestaAModificar.Foto;

                    pdt.CantidadHoras = int.Parse(form["CantidadHoras"]);
                    pdt.Profesion = form["Profesion"];

                    Propuestas.Modificar(idPropuesta, pdt);
                    break;
            }

            //TODO: Modificar Referencias 1 y 2
            return Redirect("/Home/Inicio");
        }

        [HttpPost]
        public ActionResult CrearPaso2(FormCollection form)
        {
            int TipoDonacion = Int32.Parse(form["TipoDonacion"]);
            Propuesta NuevaPropuesta;
            String ViewName;

            switch(TipoDonacion) //Recibe del formulario Crear el tipo de donacion que selecciono.
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
        //Creaciones segun el tipo Propuesta y guardado en Db
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
                //Logica si entra al detalle propuesta deslogueado. Te obliga a loguearte y luego te devuelve a la propuesta deseada
				//Guarda url donde se desea ingresar, y se envia a la vista Login como hidden
				string url = Url.Content(Request.Url.PathAndQuery);
				return RedirectToAction("Login", "Usuario", new { url, MensajeError = "Deb�s iniciar sesi�n para continuar" });
            } else
            {
                ViewBag.UsuarioActualId = Usuario.IdUsuario;
                Propuesta p = Propuestas.ObtenerPorId(Id);
                return View(p);
            }
        }
		
		public ActionResult Denunciar(int Id)
		{
			if (SesionServicio.UsuarioSession == null) //Si no esta logueado
			{
				//TODO: Mensaje error no logueado o sin perfil creado
				return Redirect("/Home/Inicio");
			}
			else
			{
				if (SesionServicio.UsuarioSession.UserName == null) //o no tiene creado su perfil.
				{
					//Mensaje de error: Primero debe crear su perfil
					return Redirect("/Usuario/CrearPerfil");
				}
			}
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
			if(Propuestas.VerificarDenunciaUna(d) == null)
			{
				Propuestas.CrearDenuncia(d);
				return Redirect("/Home/Inicio");
			}
			else
			{
				//TODO: Mensaje de error. Ya has hecho una denuncia para esta propuesta.
				return Redirect("/Home/Inicio");
			}


			
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

            //Logica Envio de Foto
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
        public ActionResult Buscar(string keyword) //Busquedad cuadro de texto en InicioUsuarioLogueado
        {
            var resultado = Propuestas.BuscarPorNombreYUsuario(keyword); //Trae propuestas por su nombre o el username del Usuario
            return View(resultado);
        }
        //Logica Valoraciones
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