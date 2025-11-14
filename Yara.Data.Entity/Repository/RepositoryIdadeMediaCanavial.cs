using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryIdadeMediaCanavial: RepositoryBase<IdadeMediaCanavial>, IRepositoryIdadeMediaCanavial
    {
        public RepositoryIdadeMediaCanavial(DbContext context) : base(context)
        {
        }
    }
}