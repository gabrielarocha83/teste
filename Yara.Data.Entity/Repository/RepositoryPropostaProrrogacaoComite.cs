using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaProrrogacaoComite : RepositoryBase<PropostaProrrogacaoComite>, IRepositoryPropostaProrrogacaoComite
    {
        public RepositoryPropostaProrrogacaoComite(DbContext context) : base(context)
        {
        }
    }
}
