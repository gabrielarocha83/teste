using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryCidade : RepositoryBase<Cidade>, IRepositoryCidade
    {
        public RepositoryCidade(DbContext context) : base(context)
        {
        }
    }
}
