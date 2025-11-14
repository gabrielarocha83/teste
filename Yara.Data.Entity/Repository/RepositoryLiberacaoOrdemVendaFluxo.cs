using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryLiberacaoOrdemVendaFluxo : RepositoryBase<LiberacaoOrdemVendaFluxo>, IRepositoryLiberacaoOrdemVendaFluxo
    {
        public RepositoryLiberacaoOrdemVendaFluxo(DbContext context) : base(context)
        {
        }
    }
}
