using Domain.Model.Properties;
using Domain.Repository.PIM;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Data.PIM.Repository
{
    public class ProductPIMRepository : IProductPIMRepository
    {
        private readonly OracleProperties _oracleProperties;
        private OracleConnection ora_Connection;
        private OracleTransaction ora_Transaction;
        private struct stConnDB
        {
            public string CadenaConexion;
            public string ErrorDesc;
            public int ErrorNum;
        }
        private stConnDB info;
        public int timeout = 300;
        public ProductPIMRepository(IOptions<OracleProperties> oracleProperties)
        {
            _oracleProperties = oracleProperties.Value;
            _oracleProperties.SetConnection();
            info.CadenaConexion = _oracleProperties.stringConexion;
            ora_Connection = new OracleConnection();
        }
        public Boolean EjecutarProcedure(ref OracleDataReader oracleDataReader, ref OracleCommand OraCommand, ref string ErrorMessage, string SpName, bool conresultado)
        {
            bool ok = true;
            try
            {
                if (!IsConected())
                {
                    ok = Conectar();
                }
                if (ok)
                {
                    ora_Transaction = ora_Connection.BeginTransaction();
                    OraCommand.Connection = ora_Connection;
                    OraCommand.CommandText = "EDSR.PKG_PIM_INTEGRACION." + SpName;
                    OraCommand.CommandType = CommandType.StoredProcedure;
                    OraCommand.CommandTimeout = timeout;
                    if (conresultado) oracleDataReader = OraCommand.ExecuteReader();
                    else OraCommand.ExecuteNonQuery();
                    ora_Transaction.Commit();
                    ErrorMessage = "OK";
                }
                else
                {
                    ok = false;
                    ErrorMessage = "No se puede crear una conexion a Base de datos";
                }
            }
            catch (Exception ex)
            {
                ora_Transaction.Rollback();
                if (ex.Message.Contains("ORA-00001: unique constraint") || ex.Message.Contains("ORA-00001: restricción única")) ErrorMessage = "Clave unica duplicada";
                else ErrorMessage = ex.Message;
                AsignarError(ref ex);
                ok = false;
            }
            return ok;
        }
        public bool IsConected()
        {
            bool ok = false;
            try
            {
                if (ora_Connection != null)
                {
                    switch (ora_Connection.State)
                    {
                        case ConnectionState.Closed:
                        case ConnectionState.Broken:
                        case ConnectionState.Connecting:
                            ok = false;
                            break;
                        case ConnectionState.Open:
                        case ConnectionState.Fetching:
                        case ConnectionState.Executing:
                            ok = true;
                            break;
                    }
                }
                else
                {
                    ok = false;
                }
            }
            catch (Exception ex)
            {
                AsignarError(ref ex);
                ok = false;
            }
            return ok;
        }
        private void AsignarError(ref Exception ex)
        {
            // Si es una excepcion de Oracle.
            if (ex is OracleException)
            {
                info.ErrorNum = ((OracleException)ex).Number;
                info.ErrorDesc = ex.Message;
            }
            else
            {
                info.ErrorNum = 0;
                info.ErrorDesc = ex.Message;
            }
            // Grabamos Log de Error...
        }
        private bool Conectar()
        {
            bool ok = false;
            try
            {
                if (ora_Connection != null)
                {
                    ora_Connection.ConnectionString = info.CadenaConexion;
                    ora_Connection.Open();
                    if (ora_Connection.State == ConnectionState.Open)
                    {
                        ok = true;
                    }
                }
            }
            catch (Exception ex)
            {
                AsignarError(ref ex);
                ok = false;
            }
            return ok;
        }
    }
}
