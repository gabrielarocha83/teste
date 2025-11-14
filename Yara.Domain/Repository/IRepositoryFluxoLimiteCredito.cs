using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryFluxoLiberacaoManual : IRepositoryBase<FluxoLiberacaoManual>
    {
        Task<IEnumerable<FluxoLiberacaoManual>> GetAllListFluxoAsync();

        
    }
}
