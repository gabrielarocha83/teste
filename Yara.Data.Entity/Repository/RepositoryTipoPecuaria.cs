using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryTipoPecuaria : RepositoryBase<TipoPecuaria>, IRepositoryTipoPecuaria
    {
        public RepositoryTipoPecuaria(DbContext context) : base(context)
        {
        }
    }
}
