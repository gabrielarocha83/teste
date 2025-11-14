using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryAreaIrrigada : RepositoryBase<AreaIrrigada>, IRepositoryAreaIrrigada
    {
        public RepositoryAreaIrrigada(DbContext context) : base(context)
        {
        }
    }
}
