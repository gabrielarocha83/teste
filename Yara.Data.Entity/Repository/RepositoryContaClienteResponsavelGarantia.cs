using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryContaClienteResponsavelGarantia : RepositoryBase<ContaClienteResponsavelGarantia>, IRepositoryContaClienteResponsavelGarantia
    {
        public RepositoryContaClienteResponsavelGarantia(DbContext context) : base(context)
        {
        }

        public void Delete(ContaClienteResponsavelGarantia obj)
        {
            _context.Set<ContaClienteResponsavelGarantia>().Attach(obj);
            _context.Entry<ContaClienteResponsavelGarantia>(obj).State = EntityState.Deleted;
        }
    }
}
