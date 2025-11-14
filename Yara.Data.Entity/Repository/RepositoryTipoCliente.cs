using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryTipoCliente : RepositoryBase<TipoCliente>, IRepositoryTipoCliente
    {
        public RepositoryTipoCliente(DbContext context) : base(context)
        {
        }
    }
}