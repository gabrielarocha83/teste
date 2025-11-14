using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryExperiencia : RepositoryBase<Experiencia>, IRepositoryExperiencia
    {
        public RepositoryExperiencia(DbContext context) : base(context)
        {
        }
    }
}
