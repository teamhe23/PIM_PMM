using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PIM_PRODUCTO
    {
        private String id_PIM;
        private Int64 id_PIM_PROD;
        private String tipo_PRODUCTO;
        private String cod_LINEA;
        private String clasificacion_ID;
        private String nombre_PROD;
        private DateTime fec_CREACION;
        private DateTime fec_PROCESO;
        private String prd_LVL_NUMBER;
        private String cod_ERROR;
        private Char flg_RESP_PIM;
        private String mensaje;
        private List<PIM_PRODUCTO_ATRIBUTO> lstAtributos;
        private List<PIM_VARIACION_TMP> lstVariaciones;

        public String Id_PIM { get => id_PIM; set => id_PIM = value; }
        public Int64 Id_PIM_PROD { get => id_PIM_PROD; set => id_PIM_PROD = value; }
        public String Tipo_PRODUCTO { get => tipo_PRODUCTO; set => tipo_PRODUCTO = value; }
        public String Cod_LINEA { get => cod_LINEA; set => cod_LINEA = value; }
        public String Clasificacion_ID { get => clasificacion_ID; set => clasificacion_ID = value; }
        public String Nombre_PROD { get => nombre_PROD; set => nombre_PROD = value; }
        public DateTime Fec_CREACION { get => fec_CREACION; set => fec_CREACION = value; }
        public DateTime Fec_PROCESO { get => fec_PROCESO; set => fec_PROCESO = value; }
        public String PRD_LVL_NUMBER { get => prd_LVL_NUMBER; set => prd_LVL_NUMBER = value; }
        public String COD_ERROR { get => cod_ERROR; set => cod_ERROR = value; }
        public Char FLG_RESP_PIM { get => flg_RESP_PIM; set => flg_RESP_PIM = value; }
        public String MENSAJE { get => mensaje; set => mensaje = value; }
        public List<PIM_PRODUCTO_ATRIBUTO> LstAtributos { get => lstAtributos; set => lstAtributos = value; }
        public List<PIM_VARIACION_TMP> LstVariaciones { get => lstVariaciones; set => lstVariaciones = value; }
    }
}