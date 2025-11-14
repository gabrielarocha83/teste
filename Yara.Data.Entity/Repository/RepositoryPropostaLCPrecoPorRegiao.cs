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
    public class RepositoryPropostaLCPrecoPorRegiao : RepositoryBase<PropostaLCPrecoPorRegiao>, IRepositoryPropostaLCPrecoPorRegiao
    {
        public RepositoryPropostaLCPrecoPorRegiao(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaLCPrecoPorRegiao obj)
        {
            _context.Set<PropostaLCPrecoPorRegiao>().Attach(obj);
            _context.Entry<PropostaLCPrecoPorRegiao>(obj).State = EntityState.Deleted;
        }
    }
}
