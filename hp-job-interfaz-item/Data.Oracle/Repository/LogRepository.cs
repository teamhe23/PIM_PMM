using Domain.Model;
using Domain.Model.Properties;
using Domain.Repository.Oracle;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Oracle.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly OracleProperties _oracleProperties;
        public LogRepository(IOptions<OracleProperties> oracleProperties)
        {
            _oracleProperties = oracleProperties.Value;
            _oracleProperties.SetConnection();
        }

        public async Task Post(Log ologs)
        {
            using (var cn = new OracleConnection(_oracleProperties.stringConexion))
            {
                using (var cmd = cn.CreateCommand())
                {
                    await cn.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "EDSR.PKG_PIM_INTEGRACION.SP_INS_PIM_LOG_INTEGRACION";
                    cmd.CommandTimeout = 60000;
                    cmd.Parameters.Add("P_ID_TIPO", OracleDbType.Varchar2, ParameterDirection.Input).Value = ologs.ID_TIPO;
                    cmd.Parameters.Add("P_IDENTIFICADOR", OracleDbType.Varchar2, ParameterDirection.Input).Value = ologs.IDENTIFICADOR;
                    cmd.Parameters.Add("P_MENSAJE", OracleDbType.Varchar2, ParameterDirection.Input).Value = ologs.MENSAJE;
                    cmd.Parameters.Add("P_TRAMA", OracleDbType.Clob, ParameterDirection.Input).Value = ologs.TRAMA;

                    await cmd.ExecuteNonQueryAsync();
                    await cn.CloseAsync();
                    return;
                }
            }
        }
    }
}
