using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryFluxoLiberacaoLimiteCredito : RepositoryBase<FluxoLiberacaoLimiteCredito>, IRepositoryFluxoLiberacaoLimiteCredito
    {
        public RepositoryFluxoLiberacaoLimiteCredito(DbContext context) : base(context)
        {

        }
    }
}
