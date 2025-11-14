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
    public class RepositoryPropostaLCMercado : RepositoryBase<PropostaLCMercado>, IRepositoryPropostaLCMercado
    {
        public RepositoryPropostaLCMercado(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaLCMercado obj)
        {
            _context.Set<PropostaLCMercado>().Attach(obj);
            _context.Entry<PropostaLCMercado>(obj).State = EntityState.Deleted;
        }
    }
}
