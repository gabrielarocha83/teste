using System.Data.Entity;
using System.Linq;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryContaClienteGarantia : RepositoryBase<ContaClienteGarantia>, IRepositoryContaClienteGarantia
    {
        public RepositoryContaClienteGarantia(DbContext context) : base(context)
        {
        }

        public int? GetMaxNumeroInterno()
        {

            if (_context.Set<ContaClienteGarantia>().Any())
            {
                var maxNumero = _context.Set<ContaClienteGarantia>().Max(p => p.Codigo);
                return maxNumero + 1;
            }
            else
            {
                return 1;
            }

        }
    }
}
