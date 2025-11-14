using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryEmpresa : RepositoryBase<Empresas>, IRepositoryEmpresa
    {
        public RepositoryEmpresa(DbContext context) : base(context)
        {
        }
    }
}
