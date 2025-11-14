using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryTipoEndividamento : RepositoryBase<TipoEndividamento>,IRepositoryTipoEndividamento
    {
        public RepositoryTipoEndividamento(DbContext context) : base(context)
        {

        }
    }
}
