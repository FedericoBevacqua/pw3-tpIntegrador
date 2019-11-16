using Servicios;
using System.Web.Mvc;

namespace pw3_tpIntegrador.Controllers
{
    public class DenunciasController : Controller
    {
        private readonly DenunciaServicio Denuncias = new DenunciaServicio();

		[HttpGet]
		public ActionResult Gestionar()
        {
			if (SesionServicio.IsAdmin) //Si es Admin puede entrar a Gestion de denuncias
			{
                return View(Denuncias.ObtenerNuevasDenuncias());
			}
			else
			{
				TempData["MensajeNoAdmin"] = "Usted no tiene permisos para acceder aquí.";
				return Redirect("/Home/Inicio");
			}
        }

        [HttpGet]
        public ActionResult Aceptar(int id) //Aceptar Denuncia
        {
            if (SesionServicio.IsAdmin)
            {
                Denuncias.Aceptar(id);
				TempData["MensajeDenunciaAceptada"] = "La denuncia ha sido aceptada con éxito.";
				return Redirect("/Denuncias/Gestionar");
            }
            else
            {
				TempData["MensajeNoAdmin"] = "Usted no tiene permisos para acceder aquí.";
				return Redirect("/Home/Inicio");
            }
        }

        [HttpGet]
        public ActionResult Desestimar(int id) //Desestimar Denuncia
        {
            if (SesionServicio.IsAdmin)
            {
                Denuncias.Desestimar(id);
				TempData["MensajeDenunciaDesestimada"] = "La denuncia ha sido desestimada.";
				return Redirect("/Denuncias/Gestionar");
            }
            else
            {
				TempData["MensajeNoAdmin"] = "Usted no tiene permisos para acceder aquí.";
				return Redirect("/Home/Inicio");
            }
        }

    }
}