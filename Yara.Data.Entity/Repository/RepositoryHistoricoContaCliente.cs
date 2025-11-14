using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    internal class RepositoryHistoricoContaCliente : RepositoryBase<HistoricoContaCliente>, IRepositoryHistoricoContaCliente
    {
        public RepositoryHistoricoContaCliente(DbContext context) : base(context)
        {
        }
    }
}