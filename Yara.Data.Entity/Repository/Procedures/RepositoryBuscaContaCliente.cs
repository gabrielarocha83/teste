using System.Data.Entity;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository.Procedures;

namespace Yara.Data.Entity.Repository.Procedures
{
    public class RepositoryBuscaContaCliente : RepositoryBase<BuscaContaCliente>, IRepositoryBuscaContaCliente
    {
        public RepositoryBuscaContaCliente(DbContext context) : base(context)
        {
        }
    }
}
