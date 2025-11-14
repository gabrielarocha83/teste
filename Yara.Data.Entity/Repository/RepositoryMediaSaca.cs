using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryMediaSaca : RepositoryBase<MediaSaca>, IRepositoryMediaSaca
    {
        public RepositoryMediaSaca(DbContext context) : base(context)
        {
        }
    }
}
