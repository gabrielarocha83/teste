using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryTituloComentario : RepositoryBase<TituloComentario>, IRepositoryTituloComentario
    {
        public RepositoryTituloComentario(DbContext context) : base(context)
        {
        }
    }
}
