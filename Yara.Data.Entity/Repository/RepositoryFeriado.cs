using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryFeriado : RepositoryBase<Feriado>,IRepositoryFeriado
    {
        public RepositoryFeriado(DbContext context) : base(context)
        {

        }
    }
}
