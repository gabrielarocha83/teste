using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryContaClienteEstruturaComercial: RepositoryBase<ContaClienteEstruturaComercial>,IRepositoryContaClienteEstruturaComercial
    {
        public RepositoryContaClienteEstruturaComercial(DbContext context) : base(context)
        {
        }
    }
}
