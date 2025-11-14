using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryFluxoLiberacaoAbono : RepositoryBase<FluxoLiberacaoAbono>, IRepositoryFluxoLiberacaoAbono
    {
        public RepositoryFluxoLiberacaoAbono(DbContext context) : base(context)
        {

        }
    }
}
