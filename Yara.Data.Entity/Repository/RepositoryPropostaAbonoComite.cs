using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaAbonoComite : RepositoryBase<PropostaAbonoComite>, IRepositoryPropostaAbonoComite
    {
        public RepositoryPropostaAbonoComite(DbContext context) : base(context)
        {
        }
    }
}
