using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryParametroSistema : RepositoryBase<ParametroSistema>,IRepositoryParametroSistema
    {
        public RepositoryParametroSistema(DbContext context) : base(context)
        {
        }
    }
}
