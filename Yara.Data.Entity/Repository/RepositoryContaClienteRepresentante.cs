using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryContaClienteRepresentante : RepositoryBase<ContaClienteRepresentante>, IRepositoryContaClienteRepresentante
    {
        public RepositoryContaClienteRepresentante(DbContext context) : base(context)
        {
        }
    }
}
