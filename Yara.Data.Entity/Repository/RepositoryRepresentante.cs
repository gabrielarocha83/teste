using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryRepresentante : RepositoryBase<Representante>, IRepositoryRepresentante
    {
        public RepositoryRepresentante(DbContext context) : base(context)
        {
        }
    }
}
