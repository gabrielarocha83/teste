using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceOrdemVendaFluxo : IAppServiceBase<OrdemVendaFluxoDto>
    {
        Task<bool> UpdateStatusFluxo(OrdemVendaFluxoDto obj);
        Task<LiberacaoOrdemVendaFluxoDto> FluxoAprovacaoReprovacaoOrdem(LiberacaoOrdemVendaFluxoDto solicitante, string URL);
    }
}
