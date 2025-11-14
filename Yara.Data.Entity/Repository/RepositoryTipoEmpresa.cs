using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryTipoEmpresa : RepositoryBase<TipoEmpresa>, IRepositoryTipoEmpresa
    {
        public RepositoryTipoEmpresa(DbContext context) : base(context)
        {
        }
    }
}
