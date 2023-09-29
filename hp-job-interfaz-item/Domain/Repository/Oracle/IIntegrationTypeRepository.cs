using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Oracle
{
    public interface IIntegrationTypeRepository
    {
        Task<List<IntegrationType>> get(Int64? IdProceso);
    }
}
