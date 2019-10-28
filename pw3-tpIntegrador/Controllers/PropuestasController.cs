using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pw3_tpIntegrador.Controllers
{
    public class PropuestasController : Controller
    {
        public ActionResult Crear()
        {
            return View();
        }





        public ActionResult DetalleMonetaria()
        {
            //Action temporal para desarrollo, luego sera Detalle() quien decida si es DetalleMonetaria() o alguna otra de sus variantes
            return View();
        }
    }
}