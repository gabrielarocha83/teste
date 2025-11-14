using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryFluxoAlcadaAprovacao : RepositoryBase<FluxoAlcadaAprovacao>, IRepositoryFluxoAlcadaAprovacao
    {
        public RepositoryFluxoAlcadaAprovacao(DbContext context) : base(context)
        {
        }
    }
}
