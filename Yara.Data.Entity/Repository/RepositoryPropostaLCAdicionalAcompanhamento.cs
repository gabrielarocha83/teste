using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaLCAdicionalAcompanhamento : RepositoryBase<PropostaLCAdicionalAcompanhamento>, IRepositoryPropostaLCAdicionalAcompanhamento
    {
        public RepositoryPropostaLCAdicionalAcompanhamento(DbContext context) : base(context)
        {
        }
    }
}
