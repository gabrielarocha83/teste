using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaAbonoTitulo : RepositoryBase<PropostaAbonoTitulo>, IRepositoryPropostaAbonoTitulo
    {

        public RepositoryPropostaAbonoTitulo(DbContext context) : base(context)
        {
        }

        public void InsertRange(IEnumerable<PropostaAbonoTitulo> titulos)
        {
            _context.Set<PropostaAbonoTitulo>().AddRange(titulos);
        }
    }
}
