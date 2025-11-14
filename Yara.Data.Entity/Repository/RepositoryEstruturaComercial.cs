using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryEstruturaComercial : RepositoryBase<EstruturaComercial>, IRepositoryEstruturaComercial
    {
        public RepositoryEstruturaComercial(DbContext context) : base(context)
        {
        }
    }
}
