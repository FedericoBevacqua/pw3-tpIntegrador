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
			if (SesionServicio.IsAdmin)
			{
                return View(Denuncias.ObtenerNuevasDenuncias());
			}
			else
			{
				return Redirect("/Home/Inicio");
			}
        }

        [HttpGet]
        public ActionResult Aceptar(int id)
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
        public ActionResult Desestimar(int id)
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