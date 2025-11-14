using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPorcentagemQuebra : RepositoryBase<PorcentagemQuebra>, IRepositoryPorcentagemQuebra
    {
        public RepositoryPorcentagemQuebra(DbContext context) : base(context)
        {
        }
    }
}

