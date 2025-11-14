using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{

    public interface IAppServiceFluxoAlcadaAnalise : IAppServiceBase<FluxoAlcadaAnaliseDto>
    {
        Task<string> GetPerfilAtivoByValor(decimal? valorProposto, Guid? segmentoID);
        Task<bool> InsertAsync(FluxoAlcadaAnaliseDto obj);
        Task<bool> Inactive(Guid id, Guid userIdAlteracao);
    }
}
