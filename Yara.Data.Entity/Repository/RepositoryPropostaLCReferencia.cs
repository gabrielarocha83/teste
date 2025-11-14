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
    public class RepositoryPropostaLCReferencia : RepositoryBase<PropostaLCReferencia>, IRepositoryPropostaLCReferencia
    {
        public RepositoryPropostaLCReferencia(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaLCReferencia obj)
        {
            _context.Set<PropostaLCReferencia>().Attach(obj);
            _context.Entry<PropostaLCReferencia>(obj).State = EntityState.Deleted;
        }
    }
}
