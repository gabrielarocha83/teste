using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryEstruturaComercialPapel : RepositoryBase<EstruturaComercialPapel>, IRepositoryEstruturaComercialPapel
    {
        public RepositoryEstruturaComercialPapel(DbContext context) : base(context)
        {
        }
    }
}
