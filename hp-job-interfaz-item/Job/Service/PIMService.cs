using Domain.Helpers;
using Domain.Model;
using Domain.Models;
using Domain.Repository.Oracle;
using Domain.Repository.PIM;
using Domain.Service;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Job.Service
{
    public class PIMService : IPIMService
    {
        private readonly IProductPIMRepository _ProductPIMRepository;
        public PIMService(IProductPIMRepository productPIMRepository)
        {
            _ProductPIMRepository = productPIMRepository;
        }
        public List<PIM_TRAMA> GetTramaPendiente(Int32 IdTipo)
        {
            String e = "";
            List<PIM_TRAMA> retorna = new List<PIM_TRAMA>();
            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.Parameters.Add("in_Id_Tipo", IdTipo);
            oracleCommand.Parameters.Add("TRAMAS_C", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            OracleDataReader oracleDataReader = null;
            _ProductPIMRepository.EjecutarProcedure(ref oracleDataReader, ref oracleCommand, ref e, "SP_GET_TRAMAS_PENDIENTES", true);

            while (oracleDataReader.Read())
            {
                PIM_TRAMA obj = new PIM_TRAMA();
                obj.Id_Modelo = oracleDataReader.GetInt64(0);
                OracleClob datablob = oracleDataReader.GetOracleClob(1);
                obj.JSON_Data = JsonDocument.Parse(datablob.Value);
                retorna.Add(obj);
            }

            return retorna;
        }
        public Boolean InsertaProducto(PIM_PRODUCTO objProd, Int64 idModelo, ref string ErrorMessage)
        {
            OracleCommand oracleCommand = new OracleCommand();
            OracleDataReader oracleDataReader = null;
            oracleCommand.Parameters.Add("in_ID_PIM", OracleDbType.Varchar2).Value = objProd.Id_PIM;
            oracleCommand.Parameters.Add("in_ID_MODELO", OracleDbType.Int64).Value = idModelo;
            oracleCommand.Parameters.Add("in_DESCRIPCION", OracleDbType.Varchar2).Value = objProd.Nombre_PROD;
            //--id modelo, id pim, nombre producto

            return _ProductPIMRepository.EjecutarProcedure(ref oracleDataReader, ref oracleCommand, ref ErrorMessage, "SP_INS_PIM_PRODUCTO", false);
        }
        public Boolean InsertaAtribProducto(String idPim, PIM_PRODUCTO_ATRIBUTO objAtribProd, ref string ErrorMessage)
        {
            OracleCommand oracleCommand = new OracleCommand();
            OracleDataReader oracleDataReader = null;
            oracleCommand.Parameters.Add("in_ID_PIM", OracleDbType.Long).Value = idPim;
            oracleCommand.Parameters.Add("in_COD_ATRIBUTO", OracleDbType.Varchar2).Value = objAtribProd.Id_ATRIBUTO;
            oracleCommand.Parameters.Add("in_VALOR", OracleDbType.Varchar2).Value = objAtribProd.Valor;

            return _ProductPIMRepository.EjecutarProcedure(ref oracleDataReader, ref oracleCommand, ref ErrorMessage, "SP_INS_PIM_PRODUCTO_ATRIBUTO", false);
        }
        public void ActualizarEstadoTrama(PIM_TRAMA trama)
        {
            String ErrorMessage = "";
            OracleCommand oracleCommand = new OracleCommand();
            OracleDataReader oracleDataReader = null;
            oracleCommand.Parameters.Add("in_Id_Modelo", OracleDbType.Long).Value = trama.Id_Modelo;
            oracleCommand.Parameters.Add("in_FLG_ERROR", OracleDbType.Char).Value = trama.Flg_Error;
            oracleCommand.Parameters.Add("in_Mensaje", OracleDbType.Varchar2).Value = trama.Mensaje;

            _ProductPIMRepository.EjecutarProcedure(ref oracleDataReader, ref oracleCommand, ref ErrorMessage, "SP_ACTUALIZA_TRAMA", false);
        }
        public List<PIM_PRODUCTO> GetProductosPendientes()
        {
            String e = "";
            List<PIM_PRODUCTO> retorna = new List<PIM_PRODUCTO>();
            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.Parameters.Add("PIM_PRODUCTOS_C", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            OracleDataReader oracleDataReader = null;
            _ProductPIMRepository.EjecutarProcedure(ref oracleDataReader, ref oracleCommand, ref e, "SP_GET_PRODUCTOS_PENDIENTES", true);

            while (oracleDataReader.Read())
            {
                PIM_PRODUCTO obj = new PIM_PRODUCTO();
                obj.Id_PIM_PROD = oracleDataReader.GetInt64(0);
                obj.Nombre_PROD = oracleDataReader.GetString(1);
                retorna.Add(obj);
            }
            return retorna;
        }
        public Boolean CreaProductoPMM(Int64 idPIMProd, ref string ErrorMessage, ref Int64 pchild)
        {
            OracleCommand oracleCommand = new OracleCommand();
            OracleDataReader oracleDataReader = null;
            oracleCommand.Parameters.Add("P_ID_PIM_PROD", OracleDbType.Long).Value = idPIMProd;
            oracleCommand.Parameters.Add("P_PRD_LVL_CHILD", OracleDbType.Long, pchild, ParameterDirection.Output);

            return _ProductPIMRepository.EjecutarProcedure(ref oracleDataReader, ref oracleCommand, ref ErrorMessage, "SP_CREAR_PRODUCTO", false);
        }

        public void ActualizarEstadoPIMProducto(PIM_PRODUCTO objProd, Int64 pchild)
        {
            String ErrorMessage = "";
            OracleCommand oracleCommand = new OracleCommand();
            OracleDataReader oracleDataReader = null;
            oracleCommand.Parameters.Add("in_id_pim", OracleDbType.Varchar2).Value = objProd.Id_PIM_PROD;
            oracleCommand.Parameters.Add("in_PRD_LVL_CHILD", OracleDbType.Long).Value = pchild;
            oracleCommand.Parameters.Add("in_COD_ERROR", OracleDbType.Char).Value = objProd.COD_ERROR;
            oracleCommand.Parameters.Add("in_Mensaje", OracleDbType.Varchar2).Value = objProd.MENSAJE;

            _ProductPIMRepository.EjecutarProcedure(ref oracleDataReader, ref oracleCommand, ref ErrorMessage, "SP_ACTUALIZA_PIM_PRODUCTO", false);
        }







        public List<PIM_PRODUCTO_OUT> GetProductosPIM()
        {
            throw new NotImplementedException();
        }




    }
}
