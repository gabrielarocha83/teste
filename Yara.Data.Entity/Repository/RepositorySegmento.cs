using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositorySegmento : RepositoryBase<Segmento>, IRepositorySegmento
    {
        public RepositorySegmento(DbContext context) : base(context)
        {
        }
    }
}
