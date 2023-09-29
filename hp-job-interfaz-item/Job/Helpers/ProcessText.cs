using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Helpers
{
    public static class ProcessText
    {
        public static string TrimString(this string cadena, int longitud)
        {
            if (!string.IsNullOrEmpty(cadena))
            {
                cadena = cadena.Substring(0, cadena.Length > longitud ? longitud : cadena.Length);
            }

            return cadena;
        }
    }
}
