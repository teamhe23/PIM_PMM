using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PIM_PRODUCTO_ATRIBUTO
    {
        private String id_PIM;
        private String id_ATRIBUTO;
        private String clave;
        private String valor;
        private String und_MEDIDA;

        public String Id_PIM { get => id_PIM; set => id_PIM = value; }
        public String Id_ATRIBUTO { get => id_ATRIBUTO; set => id_ATRIBUTO = value; }
        public String Clave { get => clave; set => clave = value; }
        public String Valor { get => valor; set => valor = value; }
        public String Und_MEDIDA { get => und_MEDIDA; set => und_MEDIDA = value; }
        public PIM_PRODUCTO_ATRIBUTO() 
        {
        }
    }
}