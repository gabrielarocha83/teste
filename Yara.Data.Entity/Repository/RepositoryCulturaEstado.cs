using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryCulturaEstado : RepositoryBase<CulturaEstado>, IRepositoryCulturaEstado
    {
        public RepositoryCulturaEstado(DbContext context) : base(context)
        {
        }
    }
}
