using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaLCGarantia: RepositoryBase<PropostaLCGarantia>, IRepositoryPropostaLCGarantia
    {
        public RepositoryPropostaLCGarantia(DbContext context) : base(context)
        {
        }

        public async Task<PropostaLCGarantia> GetLatest(Expression<Func<PropostaLCGarantia, bool>> expression)
        {
            return await _context.Set<PropostaLCGarantia>().Where(expression).OrderByDescending(p => p.DataCriacao).FirstOrDefaultAsync();
        }

        public void Delete(PropostaLCGarantia obj)
        {
            _context.Set<PropostaLCGarantia>().Attach(obj);
            _context.Entry<PropostaLCGarantia>(obj).State = EntityState.Deleted;
        }
    }
}
