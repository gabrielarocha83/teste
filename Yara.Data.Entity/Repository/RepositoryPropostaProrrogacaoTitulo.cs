using System.Collections.Generic;
using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaProrrogacaoTitulo : RepositoryBase<PropostaProrrogacaoTitulo>, IRepositoryPropostaProrrogacaoTitulo
    {
        public RepositoryPropostaProrrogacaoTitulo(DbContext context) : base(context)
        {
        }

        public void InsertRange(IEnumerable<PropostaProrrogacaoTitulo> titulos)
        {
            _context.Set<PropostaProrrogacaoTitulo>().AddRange(titulos);
        }
    }
}
