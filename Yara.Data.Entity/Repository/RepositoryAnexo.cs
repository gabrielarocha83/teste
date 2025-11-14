using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryAnexo : RepositoryBase<Anexo>, IRepositoryAnexo
    {
        public RepositoryAnexo(DbContext context) : base(context)
        {
        }
    }
}
