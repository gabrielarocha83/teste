using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryFluxoRenovacaoVigenciaLC : IRepositoryBase<FluxoRenovacaoVigenciaLC>
    {
        Task<IEnumerable<FluxoRenovacaoVigenciaLC>> GetAllFilterAsyncOrdered(Expression<Func<FluxoRenovacaoVigenciaLC, bool>> expression, Expression<Func<FluxoRenovacaoVigenciaLC, int>> expression2);
    }
}
