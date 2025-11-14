using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryTribunalJustica : RepositoryBase<TribunalJustica>, IRepositoryTribunalJustica
    {
        public RepositoryTribunalJustica(DbContext context) : base(context)
        {
        }
    }
}
