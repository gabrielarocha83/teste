using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    internal class RepositoryGrupo : RepositoryBase<Grupo>, IRepositoryGrupo
    {
        public RepositoryGrupo(DbContext context) : base(context)
        {
        }
    }
}