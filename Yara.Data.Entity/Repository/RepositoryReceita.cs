using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryReceita : RepositoryBase<Receita>, IRepositoryReceita
    {
        public RepositoryReceita(DbContext context) : base(context)
        {
        }
    }
}
