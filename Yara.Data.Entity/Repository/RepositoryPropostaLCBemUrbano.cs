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
    public class RepositoryPropostaLCBemUrbano : RepositoryBase<PropostaLCBemUrbano>, IRepositoryPropostaLCBemUrbano
    {
        public RepositoryPropostaLCBemUrbano(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaLCBemUrbano obj)
        {
            _context.Set<PropostaLCBemUrbano>().Attach(obj);
            _context.Entry<PropostaLCBemUrbano>(obj).State = EntityState.Deleted;
        }
    }
}
