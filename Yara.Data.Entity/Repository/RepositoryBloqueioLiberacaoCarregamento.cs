using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryBloqueioLiberacaoCarregamento : RepositoryBase<BloqueioLiberacaoCarregamento>, IRepositoryBloqueioLiberacaoCarregamento
    {
        public RepositoryBloqueioLiberacaoCarregamento(DbContext context) : base(context)
        {
        }
    }
}
