using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryTitulo : RepositoryBase<Titulo>, IRepositoryTitulo
    {
        public RepositoryTitulo(DbContext context) : base(context)
        {
        }
    }
}
