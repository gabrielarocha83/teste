using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryContaClienteCodigo : RepositoryBase<ContaClienteCodigo>, IRepositoryContaClienteCodigo
    {
        public RepositoryContaClienteCodigo(DbContext context) : base(context)
        {
        }
    }
}