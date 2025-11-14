using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryFluxoRenovacaoVigenciaLC : RepositoryBase<FluxoRenovacaoVigenciaLC>, IRepositoryFluxoRenovacaoVigenciaLC
    {
        public RepositoryFluxoRenovacaoVigenciaLC(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FluxoRenovacaoVigenciaLC>> GetAllFilterAsyncOrdered(Expression<Func<FluxoRenovacaoVigenciaLC, bool>> expression, Expression<Func<FluxoRenovacaoVigenciaLC, int>> expression2)
        {
            try
            {
                return await _context.Set<FluxoRenovacaoVigenciaLC>().AsNoTracking().Where(expression).OrderBy(expression2).ToListAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
