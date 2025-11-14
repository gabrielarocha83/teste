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
    public class RepositoryPropostaLCOutraReceita : RepositoryBase<PropostaLCOutraReceita>, IRepositoryPropostaLCOutraReceita
    {
        public RepositoryPropostaLCOutraReceita(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaLCOutraReceita obj)
        {
            _context.Set<PropostaLCOutraReceita>().Attach(obj);
            _context.Entry<PropostaLCOutraReceita>(obj).State = EntityState.Deleted;
        }
    }
}
