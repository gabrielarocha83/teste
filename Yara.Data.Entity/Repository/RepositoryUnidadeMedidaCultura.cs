using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryUnidadeMedidaCultura : RepositoryBase<UnidadeMedidaCultura>, IRepositoryUnidadeMedidaCultura
    {
        public RepositoryUnidadeMedidaCultura(DbContext context) : base(context)
        {
        }
    }
}
