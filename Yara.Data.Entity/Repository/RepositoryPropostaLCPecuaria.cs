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
    public class RepositoryPropostaLCPecuaria : RepositoryBase<PropostaLCPecuaria>, IRepositoryPropostaLCPecuaria
    {
        public RepositoryPropostaLCPecuaria(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaLCPecuaria obj)
        {
            _context.Set<PropostaLCPecuaria>().Attach(obj);
            _context.Entry<PropostaLCPecuaria>(obj).State = EntityState.Deleted;
        }
    }
}
