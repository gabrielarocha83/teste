using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaLCAdicionalHistorico : RepositoryBase<PropostaLCAdicionalHistorico>, IRepositoryPropostaLCAdicionalHistorico
    {
        public RepositoryPropostaLCAdicionalHistorico(DbContext context) : base(context)
        {
        }
    }
}
