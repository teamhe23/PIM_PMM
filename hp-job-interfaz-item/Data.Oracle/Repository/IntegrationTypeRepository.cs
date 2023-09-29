using Data.PIM;
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
    public class IntegrationTypeRepository : IIntegrationTypeRepository
    {
        private readonly OracleProperties _oracleProperties;
        public IntegrationTypeRepository(IOptions<OracleProperties> oracleProperties)
        {
            _oracleProperties = oracleProperties.Value;
            _oracleProperties.SetConnection();
        }

        public async Task<List<IntegrationType>> get(long? IdProceso)
        {
            List<IntegrationType> olist = null;
            using (var cn = new OracleConnection(_oracleProperties.stringConexion))
            {
                using (var cmd = cn.CreateCommand())
                {
                    await cn.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "EDSR.PIM_INTEGRACION_API.SP_GET_PIM_TIPO_INTEGRACION";
                    cmd.CommandTimeout = 60000;
                    cmd.Parameters.Add("P_ID_TIPO", OracleDbType.Int64, ParameterDirection.Input).Value = IdProceso;
                    cmd.Parameters.Add("TIPO_INTEGRACION", OracleDbType.RefCursor, ParameterDirection.Output);

                    await cmd.ExecuteNonQueryAsync();

                    olist = OracleConvert.CursorToModel<IntegrationType>(cmd.Parameters["TIPO_INTEGRACION"]);
                    await cn.CloseAsync();
                    return olist;

                }
            }
        }
    }
}
