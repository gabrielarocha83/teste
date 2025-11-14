using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryCustoHaRegiao : RepositoryBase<CustoHaRegiao>, IRepositoryCustoHaRegiao
    {
        public RepositoryCustoHaRegiao(DbContext context) : base(context)
        {
        }
    }
}
