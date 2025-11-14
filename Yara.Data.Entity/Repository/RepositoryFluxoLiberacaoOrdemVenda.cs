using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryFluxoLiberacaoOrdemVenda : RepositoryBase<FluxoLiberacaoOrdemVenda>, IRepositoryFluxoLiberacaoOrdemVenda
    {
        public RepositoryFluxoLiberacaoOrdemVenda(DbContext context) : base(context)
        {

        }
    }
}
