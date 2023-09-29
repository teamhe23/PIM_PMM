using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Log
    {
        public Int64 ID_TIPO { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string MENSAJE { get; set; }
        public string TRAMA { get; set; }
    }
}
