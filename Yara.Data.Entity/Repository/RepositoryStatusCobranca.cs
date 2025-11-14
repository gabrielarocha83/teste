using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryStatusCobranca : RepositoryBase<StatusCobranca>, IRepositoryStatusCobranca
    {
        public RepositoryStatusCobranca(DbContext context) : base(context)
        {
        }
    }
}
