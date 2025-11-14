using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaLCNecessidadeProduto : RepositoryBase<PropostaLCNecessidadeProduto>, IRepositoryPropostaLCNecessidadeProduto
    {
        public RepositoryPropostaLCNecessidadeProduto(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaLCNecessidadeProduto obj)
        {
            _context.Set<PropostaLCNecessidadeProduto>().Attach(obj);
            _context.Entry<PropostaLCNecessidadeProduto>(obj).State = EntityState.Deleted;
        }
    }
}
