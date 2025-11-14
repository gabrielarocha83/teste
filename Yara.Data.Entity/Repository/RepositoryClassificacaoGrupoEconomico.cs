using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryClassificacaoGrupoEconomico : RepositoryBase<ClassificacaoGrupoEconomico>, IRepositoryClassificacaoGrupoEconomico
    {
        public RepositoryClassificacaoGrupoEconomico(DbContext context) : base(context)
        {
        }
    }
}
