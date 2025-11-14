using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaAbonoAcompanhamento : RepositoryBase<PropostaAbonoAcompanhamento>, IRepositoryPropostaAbonoAcompanhamento
    {
        public RepositoryPropostaAbonoAcompanhamento(DbContext context) : base(context)
        {
        }

       
    }
}
