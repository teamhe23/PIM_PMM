using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Properties
{
    public class OracleProperties
    {
        public string DataSource { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string stringConexion { get; set; }

        public void SetConnection()
        {
            stringConexion = $"data source={DataSource};user id={UserID};password={Password};";
        }
    }
}
