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
    public class RepositoryPropostaLCBemRural : RepositoryBase<PropostaLCBemRural>, IRepositoryPropostaLCBemRural
    {
        public RepositoryPropostaLCBemRural(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaLCBemRural obj)
        {
            _context.Set<PropostaLCBemRural>().Attach(obj);
            _context.Entry<PropostaLCBemRural>(obj).State = EntityState.Deleted;
        }
    }
}
