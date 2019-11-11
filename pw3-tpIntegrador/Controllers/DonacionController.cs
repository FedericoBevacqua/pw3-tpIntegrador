using Servicios;
using System.Web.Mvc;

namespace pw3_tpIntegrador.Controllers
{
    public class DonacionController : Controller
    {
		[HttpGet]
		public ActionResult Historial()
        {
			if (SesionServicio.UsuarioSession == null)
			{
				return Redirect("/Home/Inicio");
            }
			else
			{
				return View();
			}
        }

    }
}