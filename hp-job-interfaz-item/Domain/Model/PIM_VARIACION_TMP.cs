using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PIM_VARIACION_TMP
    {
        private Int64 id_VARIACION;
        private String nombre;
        private List<PIM_VARIACION_DIMENSION> lstDimensiones;

        public Int64 Id_VARIACION { get => id_VARIACION; set => id_VARIACION = value; }
        public String Nombre { get => nombre; set => nombre = value; }
        public List<PIM_VARIACION_DIMENSION> LstDimensiones { get => lstDimensiones; set => lstDimensiones = value; }
    }
}
