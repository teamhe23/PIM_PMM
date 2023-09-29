using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PIM_VARIACION_DIMENSION
    {
        private String attributeID;
        private String id_value;
        private Int32 orden;

        public String AttributeID { get => attributeID; set => attributeID = value; }
        public String Id_value { get => id_value; set => id_value = value; }
        public Int32 Orden { get => orden; set => orden = value; }
    }
}