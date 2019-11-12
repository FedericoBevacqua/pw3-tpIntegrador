using Servicios;
using System.Web.Mvc;

namespace pw3_tpIntegrador.Controllers
{
    public class DenunciasController : Controller
    {
		[HttpGet]
		public ActionResult Gestionar()
        {
            // Si el usuario es Admin
			if (SesionServicio.UsuarioSession.TipoUsuario == 1)
			{
				return View();
			}
			else
			{
				return Redirect("/Home/Inicio");
			}
        }

    }
}