using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryContaClienteVisita : RepositoryBase<ContaClienteVisita>, IRepositoryContaClienteVisita
    {
        public RepositoryContaClienteVisita(DbContext context) : base(context)
        {
        }
    }
}
