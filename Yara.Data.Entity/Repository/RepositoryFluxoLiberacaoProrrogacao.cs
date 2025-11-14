using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryFluxoLiberacaoProrrogacao : RepositoryBase<FluxoLiberacaoProrrogacao>, IRepositoryFluxoLiberacaoProrrogacao
    {
        public RepositoryFluxoLiberacaoProrrogacao(DbContext context) : base(context)
        {

        }
    }
}
