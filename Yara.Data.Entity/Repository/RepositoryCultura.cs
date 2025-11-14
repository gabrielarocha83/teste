using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryCultura : RepositoryBase<Cultura>, IRepositoryCultura
    {
        public RepositoryCultura(DbContext context) : base(context)
        {
        }
    }
}
