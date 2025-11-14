using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryContaClienteBuscaBens : RepositoryBase<ContaClienteBuscaBens>, IRepositoryContaClienteBuscaBens
    {
        public RepositoryContaClienteBuscaBens(DbContext context) : base(context)
        {
        }
    }
}
