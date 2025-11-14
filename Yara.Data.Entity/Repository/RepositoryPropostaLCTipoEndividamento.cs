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
    public class RepositoryPropostaLCTipoEndividamento : RepositoryBase<PropostaLCTipoEndividamento>, IRepositoryPropostaLCTipoEndividamento
    {
        public RepositoryPropostaLCTipoEndividamento(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaLCTipoEndividamento obj)
        {
            _context.Set<PropostaLCTipoEndividamento>().Attach(obj);
            _context.Entry<PropostaLCTipoEndividamento>(obj).State = EntityState.Deleted;
        }
    }
}
