using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPendenciaSerasa : RepositoryBase<PendenciasSerasa>, IRepositoryPendenciaSerasa
    { 
        public RepositoryPendenciaSerasa(DbContext context) : base(context)
        {
        }
    }
}
