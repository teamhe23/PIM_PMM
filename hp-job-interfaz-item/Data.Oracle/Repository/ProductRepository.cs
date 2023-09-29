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
    public class ProductRepository : IProductRepository
    {
        private readonly OracleProperties _oracleProperties;
        public ProductRepository(IOptions<OracleProperties> oracleProperties)
        {
            _oracleProperties = oracleProperties.Value;
            _oracleProperties.SetConnection();
        }

        public async Task<List<Product>> GetAll()
        {
            List<Product> obj = null;

            using (var cn = new OracleConnection(_oracleProperties.stringConexion))
            {
                using (var cmd = cn.CreateCommand())
                {
                    await cn.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "EDSR.PIM_INTEGRACION_API.SP_GET_PRODUCTS";
                    cmd.CommandTimeout = 60000;
                    cmd.Parameters.Add("PRODUCTS", OracleDbType.RefCursor, ParameterDirection.Output);
                    await cmd.ExecuteNonQueryAsync();
                    obj = OracleConvert.CursorToModel<Product>(cmd.Parameters["PRODUCTS"]);
                    await cn.CloseAsync();
                    return obj;
                }
            }
        }

        public Task<bool> PutId(long? vpc_tech_key, long? org_lvl_child)
        {
            throw new NotImplementedException();
        }
    }
}
