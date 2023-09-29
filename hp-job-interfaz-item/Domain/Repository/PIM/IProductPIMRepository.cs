using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.PIM
{
    public interface IProductPIMRepository
    {
        Boolean EjecutarProcedure(ref OracleDataReader oracleDataReader, ref OracleCommand OraCommand, ref string ErrorMessage, string SpName, bool conresultado);
    }
}
