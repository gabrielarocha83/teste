using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryContaClienteComentario : RepositoryBase<ContaClienteComentario>, IRepositoryContaClienteComentario
    {
        public RepositoryContaClienteComentario(DbContext context) : base(context)
        {
        }
    }
}
