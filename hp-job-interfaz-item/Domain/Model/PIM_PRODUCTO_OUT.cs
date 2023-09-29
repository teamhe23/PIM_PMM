using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PIM_PRODUCTO_OUT
    {
        private String _tipoObjeto;
        private String _sku;
        private String _linea;
        private String _lineaO;
        private String _ean;
        private String _name;
        private String _codError;
        private String _marca;
        private String _procedencia;
        private String _tipoMarca;
        private String _tipoAlmacenaje;
        private String _tipoSku;
        private String _precio;
        private String _texturaTerminacion;
        private List<PIM_ATRIBUTO_OUT> _lstAtributos;
        private List<PIM_VARIACION_OUT> _lstVariaciones;

        public String TexturaTerminacion { get => _texturaTerminacion; set => _texturaTerminacion = value; }
        public String Precio { get => _precio; set => _precio = value; }
        public String TipoSku { get => _tipoSku; set => _tipoSku = value; }
        public String TipoAlmacenaje { get => _tipoAlmacenaje; set => _tipoAlmacenaje = value; }
        public String TipoMarca { get => _tipoMarca; set => _tipoMarca = value; }
        public String Procedencia { get => _procedencia; set => _procedencia = value; }
        public String Marca { get => _marca; set => _marca = value; }
        public String CodError { get => _codError; set => _codError = value; }
        public String Name { get => _name; set => _name = value; }
        public String Ean { get => _ean; set => _ean = value; }
        public String LineaO { get => _lineaO; set => _lineaO = value; }
        public String Linea { get => _linea; set => _linea = value; }
        public String Sku { get => _sku; set => _sku = value; }
        public String TipoObjeto { get => _tipoObjeto; set => _tipoObjeto = value; }
        public List<PIM_ATRIBUTO_OUT> LstAtributos { get => _lstAtributos; set => _lstAtributos = value; }
        public List<PIM_VARIACION_OUT> LstVariaciones { get => _lstVariaciones; set => _lstVariaciones = value; }
    }
}