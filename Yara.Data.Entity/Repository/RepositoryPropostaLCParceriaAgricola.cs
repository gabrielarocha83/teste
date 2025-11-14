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
    public class RepositoryPropostaLCParceriaAgricola: RepositoryBase<PropostaLCParceriaAgricola>, IRepositoryPropostaLCParceriaAgricola
    {
        public RepositoryPropostaLCParceriaAgricola(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaLCParceriaAgricola obj)
        {
            _context.Set<PropostaLCParceriaAgricola>().Attach(obj);
            _context.Entry<PropostaLCParceriaAgricola>(obj).State = EntityState.Deleted;
        }
    }
}
