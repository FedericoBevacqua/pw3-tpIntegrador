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
				return Redirect("/Home/Inicio");
			}
        }

        [HttpGet]
        public ActionResult Aceptar(int id) //Aceptar Denuncia
        {
            if (SesionServicio.IsAdmin)
            {
                Denuncias.Aceptar(id);
                return Redirect("/Denuncias/Gestionar");
            }
            else
            {
                return Redirect("/Home/Inicio");
            }
        }

        [HttpGet]
        public ActionResult Desestimar(int id) //Desestimar Denuncia
        {
            if (SesionServicio.IsAdmin)
            {
                Denuncias.Desestimar(id);
                return Redirect("/Denuncias/Gestionar");
            }
            else
            {
                return Redirect("/Home/Inicio");
            }
        }

    }
}