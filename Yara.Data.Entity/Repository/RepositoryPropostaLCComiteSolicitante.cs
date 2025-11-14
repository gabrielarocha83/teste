using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaLCComiteSolicitante : RepositoryBase<PropostaLCComiteSolicitante>, IRepositoryPropostaLCComiteSolicitante
    {
        public RepositoryPropostaLCComiteSolicitante(DbContext context) : base(context)
        {
        }
    }
}
