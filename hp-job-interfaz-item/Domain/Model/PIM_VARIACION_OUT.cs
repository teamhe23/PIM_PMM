using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PIM_VARIACION_OUT
    {
        private String _sku;
        private String _estilo;
        private String _name;
        private String _tipoObjeto;
        private String _ean;
        private String _color;
        private String _diseno;
        private String _tallaInfantil;
        private String _tallaCalzH;
        private String _tallaCalzInf;
        private String _tallaCalzAmer;
        private String _tallaTextReg;
        private String _tallaCalzMuj;
        private String _tallaS2Xl;
        private String _tallaCuellCam;
        private String _tallaSasteria;

        public String TallaSasteria { get => _tallaSasteria; set => _tallaSasteria = value; }
        public String TallaCuellCam { get => _tallaCuellCam; set => _tallaCuellCam = value; }
        public String TallaS2Xl { get => _tallaS2Xl; set => _tallaS2Xl = value; }
        public String TallaCalzMuj { get => _tallaCalzMuj; set => _tallaCalzMuj = value; }
        public String TallaTextReg { get => _tallaTextReg; set => _tallaTextReg = value; }
        public String TallaCalzAmer { get => _tallaCalzAmer; set => _tallaCalzAmer = value; }
        public String TallaCalzInf { get => _tallaCalzInf; set => _tallaCalzInf = value; }
        public String TallaCalzH { get => _tallaCalzH; set => _tallaCalzH = value; }
        public String TallaInfantil { get => _tallaInfantil; set => _tallaInfantil = value; }
        public String Diseno { get => _diseno; set => _diseno = value; }
        public String Color { get => _color; set => _color = value; }
        public String Ean { get => _ean; set => _ean = value; }
        public String TipoObjeto { get => _tipoObjeto; set => _tipoObjeto = value; }
        public String Name { get => _name; set => _name = value; }
        public String Estilo { get => _estilo; set => _estilo = value; }
        public String Sku { get => _sku; set => _sku = value; }
    }
}