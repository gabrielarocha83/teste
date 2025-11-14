using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryMotivoProrrogacao : RepositoryBase<MotivoProrrogacao>, IRepositoryMotivoProrrogacao
    {
        public RepositoryMotivoProrrogacao(DbContext context) : base(context)
        {
        }
    }
}
