using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryRegiao : RepositoryBase<Regiao>, IRepositoryRegiao
    {
        public RepositoryRegiao(DbContext context) : base(context)
        {
        }
    }
}
