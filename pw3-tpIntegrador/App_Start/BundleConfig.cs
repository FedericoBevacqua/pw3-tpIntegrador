using System.Web;
using System.Web.Optimization;

namespace pw3_tpIntegrador
{
	public class BundleConfig
	{
		// Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery/jquery.validate*"));

			// Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
			// para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/mainjs").Include(
                         "~/Scripts/crear-propuesta.js",
                         "~/Scripts/detalle.js",
                         "~/Scripts/donar.js",
                         "~/Scripts/historial-donaciones.js",
                         "~/Scripts/propuesta.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.js",
                      "~/Scripts/bootstrap/bootstrap.bundle.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/bootstrap-reboot.css",
                      "~/Content/bootstrap/bootstrap-grid.css",
                      "~/Content/bootstrap/bootstrap.css",
                      "~/Content/home.css",
                      "~/Content/main.css"));
		}
	}
}
