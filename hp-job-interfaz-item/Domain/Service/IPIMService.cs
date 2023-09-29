using Domain.Model;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public interface IPIMService
    {
       
        List<PIM_TRAMA> GetTramaPendiente(Int32 IdTipo);
        List<PIM_PRODUCTO> GetProductosPendientes();
        List<PIM_PRODUCTO_OUT> GetProductosPIM();
        Boolean InsertaProducto(PIM_PRODUCTO objProd, Int64 idModelo, ref String ErrorMessage);
        Boolean InsertaAtribProducto(String idPim, PIM_PRODUCTO_ATRIBUTO objAtribProd, ref String ErrorMessage);
        void ActualizarEstadoTrama(PIM_TRAMA trama);
        Boolean CreaProductoPMM(Int64 idPIMProd, ref String ErrorMessage, ref Int64 pchild);
        void ActualizarEstadoPIMProducto(PIM_PRODUCTO objProd, Int64 pchild);
    }
}
