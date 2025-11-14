using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryConceitoCobranca : RepositoryBase<ConceitoCobranca>, IRepositoryConceitoCobranca
    {
        public RepositoryConceitoCobranca(DbContext context) : base(context)
        {
        }
    }
}
