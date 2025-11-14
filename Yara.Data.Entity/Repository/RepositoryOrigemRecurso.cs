using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryOrigemRecurso : RepositoryBase<OrigemRecurso>, IRepositoryOrigemRecurso
    {
        public RepositoryOrigemRecurso(DbContext context) : base(context)
        {
        }
    }
}
