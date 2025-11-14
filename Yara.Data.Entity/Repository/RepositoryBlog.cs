using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryBlog : RepositoryBase<Blog>,IRepositoryBlog
    {
        public RepositoryBlog(DbContext context) : base(context)
        {

        }
    }
}
