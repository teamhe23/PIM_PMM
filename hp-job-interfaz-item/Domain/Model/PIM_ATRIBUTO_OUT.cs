using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public  class PIM_ATRIBUTO_OUT
    {
        private String _idAtributo;
        private String _valueAtributo;

        public String ValueAtributo { get => _valueAtributo; set => _valueAtributo = value; }
        public String IdAtributo { get => _idAtributo; set => _idAtributo = value; }
    }
}