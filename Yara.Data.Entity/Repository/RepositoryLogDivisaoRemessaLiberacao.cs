using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryLogDivisaoRemessaLiberacao : RepositoryBase<LogDivisaoRemessaLiberacao>, IRepositoryLogDivisaoRemessaLiberacao
    {
        public RepositoryLogDivisaoRemessaLiberacao(DbContext context) : base(context)
        {
        }
    }
}
