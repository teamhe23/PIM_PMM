using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Domain.Models
{
    public class PIM_TRAMA
    {
        private Int64 id_Modelo;
        private JsonDocument json_Data;
        private DateTime fec_Creacion;
        private DateTime fec_Proceso;
        private Char flg_Error;
        private String mensaje;

        public Int64 Id_Modelo { get => id_Modelo; set => id_Modelo = value; }
        public JsonDocument JSON_Data { get => json_Data; set => json_Data = value; }
        public DateTime Fec_Creacion { get => fec_Creacion; set => fec_Creacion = value; }
        public DateTime Fec_Proceso { get => fec_Proceso; set => fec_Proceso = value; }
        public Char Flg_Error { get => flg_Error; set => flg_Error = value; }
        public String Mensaje { get => mensaje; set => mensaje = value; }
        
        
    }
}