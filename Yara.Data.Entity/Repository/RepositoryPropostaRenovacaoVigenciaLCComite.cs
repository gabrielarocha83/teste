using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaRenovacaoVigenciaLCComite : RepositoryBase<PropostaRenovacaoVigenciaLCComite>, IRepositoryPropostaRenovacaoVigenciaLCComite
    {
        public RepositoryPropostaRenovacaoVigenciaLCComite(DbContext context) : base(context)
        {
        }
    }
}
