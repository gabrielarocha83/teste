using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaProrrogacaoAcompanhamento : RepositoryBase<PropostaProrrogacaoAcompanhamento>, IRepositoryPropostaProrrogacaoAcompanhamento
    {
        public RepositoryPropostaProrrogacaoAcompanhamento(DbContext context) : base(context)
        {
        }

       
    }
}
