using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaJuridicoGarantia: RepositoryBase<PropostaJuridicoGarantia>, IRepositoryPropostaJuridicoGarantia
    {
        public RepositoryPropostaJuridicoGarantia(DbContext context) : base(context)
        {
        }
    }
}
