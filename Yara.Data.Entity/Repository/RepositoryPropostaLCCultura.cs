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
    public class RepositoryPropostaLCCultura : RepositoryBase<PropostaLCCultura>, IRepositoryPropostaLCCultura
    {
        public RepositoryPropostaLCCultura(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaLCCultura obj)
        {
            _context.Set<PropostaLCCultura>().Attach(obj);
            _context.Entry<PropostaLCCultura>(obj).State = EntityState.Deleted;
        }
    }
}
