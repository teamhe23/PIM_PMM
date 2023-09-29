using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Oracle
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Boolean> PutId(Int64? vpc_tech_key, Int64? org_lvl_child);
    }
}
