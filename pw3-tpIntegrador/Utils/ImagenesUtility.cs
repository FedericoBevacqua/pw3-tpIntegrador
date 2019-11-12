using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;

namespace pw3_tpIntegrador.Utils
{
    public class ImagenesUtility
    {
        public static string Guardar(HttpPostedFileBase archivoSubido, string nombreSignificativo)
        {
            //Aclaracion: si queremos agrandar el tamaño máximo de archivo permitido modificar web.config (por defecto es 4MB -> 4096)
            //<httpRuntime maxRequestLength="4096" />

            //ejemplo: /Media/Imagenes/
            //la carpeta (con path relativo) donde se guardan las imagenes se obtiene del web.config
            string carpetaImagenes = System.Configuration.ConfigurationManager.AppSettings["CarpetaImagenes"];

            if (string.IsNullOrEmpty(carpetaImagenes))
            {
                throw new Exception("En el archivo web.config debe agregar dentro de <appSettings> el elemento <add key=\"CarpetaImagenes\" value=\"/Imagenes/\" />");
            }

            //garantizamos que no importa si el valor en el web.config empieza/termina con /, nosotros le ponemos que empiece y termine con /
            carpetaImagenes = string.Format("/{0}/", carpetaImagenes.TrimStart('/').TrimEnd('/'));

            //Server.MapPath antepone a un string la ruta fisica donde actualmente esta corriendo la aplicacion (ej. c:\inetpub\misitio\)
            string pathDestino = System.Web.Hosting.HostingEnvironment.MapPath("~") + carpetaImagenes;

            //si no exise la carpeta, la creamos
            if (!System.IO.Directory.Exists(pathDestino))
            {
                System.IO.Directory.CreateDirectory(pathDestino);
            }

            string nombreArchivoFinal = GenerarNombreUnico(nombreSignificativo);
            nombreArchivoFinal = string.Concat(nombreArchivoFinal, Path.GetExtension(archivoSubido.FileName));

            //para guardar en el disco rigido, se guarda con el path absoluto
            archivoSubido.SaveAs(string.Concat(pathDestino, nombreArchivoFinal));

            //retornamos el path relativo desde la raiz del sitio
            return string.Concat(carpetaImagenes, nombreArchivoFinal);
        }

        private static string GenerarNombreUnico(string nombreSignificativo)
        {
            //Genero un string random de 20 caracteres para asegurar un nombre unico y que no se pisen archivos inesperadamente
            //System.Web.Security.Membership.GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
            string randomString = System.Web.Security.Membership.GeneratePassword(20, 0);
            Random rnd = new Random();

            //removiendo espacios y caracteres raros del string random 
            randomString = Regex.Replace(randomString, @"[^a-zA-Z0-9]", m => "");

            //removiendo espacios y caracteres raros del nombre 
            nombreSignificativo = Regex.Replace(nombreSignificativo.Trim(), @"[^a-zA-Z0-9]", m => "").ToLower();

            //{Nombre,8 carac}-{Random,5 carac}
            return string.Format("{0}-{1}", StringUtility.Truncar(nombreSignificativo, 8), StringUtility.Truncar(randomString, 5));
        }
    }
}