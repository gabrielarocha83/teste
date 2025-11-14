using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaLCStatusComite : RepositoryBase<PropostaLCStatusComite>, IRepositoryPropostaLCStatusComite
    {
        public RepositoryPropostaLCStatusComite(DbContext context) : base(context)
        {
        }

       
    }
}
