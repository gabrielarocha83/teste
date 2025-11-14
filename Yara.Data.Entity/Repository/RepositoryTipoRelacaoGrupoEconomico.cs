using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryTipoRelacaoGrupoEconomico : RepositoryBase<TipoRelacaoGrupoEconomico>, IRepositoryTipoRelacaoGrupoEconomico
    {

        public RepositoryTipoRelacaoGrupoEconomico(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TipoRelacaoGrupoEconomico>> GetAllNoTracking()
        {
            return await _context.Set<TipoRelacaoGrupoEconomico>().AsNoTracking().ToListAsync();
        }
    }
}
