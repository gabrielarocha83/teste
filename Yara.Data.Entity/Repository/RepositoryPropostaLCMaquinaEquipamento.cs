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
    public class RepositoryPropostaLCMaquinaEquipamento : RepositoryBase<PropostaLCMaquinaEquipamento>, IRepositoryPropostaLCMaquinaEquipamento
    {
        public RepositoryPropostaLCMaquinaEquipamento(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaLCMaquinaEquipamento obj)
        {
            _context.Set<PropostaLCMaquinaEquipamento>().Attach(obj);
            _context.Entry<PropostaLCMaquinaEquipamento>(obj).State = EntityState.Deleted;
        }
    }
}
