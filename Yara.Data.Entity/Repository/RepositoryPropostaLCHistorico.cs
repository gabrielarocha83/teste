using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaLCHistorico : RepositoryBase<PropostaLCHistorico>, IRepositoryPropostaLCHistorico
    {
        public RepositoryPropostaLCHistorico(DbContext context) : base(context)
        {
        }

       
    }
}
