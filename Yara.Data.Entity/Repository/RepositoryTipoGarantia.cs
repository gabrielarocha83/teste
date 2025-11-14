using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryTipoGarantia : RepositoryBase<TipoGarantia>, IRepositoryTipoGarantia
    {
        public RepositoryTipoGarantia(DbContext context) : base(context)
        {
        }
    }
}
