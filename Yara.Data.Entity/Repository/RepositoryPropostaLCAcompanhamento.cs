using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaLCAcompanhamento : RepositoryBase<PropostaLCAcompanhamento>, IRepositoryPropostaLCAcompanhamento
    {
        public RepositoryPropostaLCAcompanhamento(DbContext context) : base(context)
        {
        }

       
    }
}
