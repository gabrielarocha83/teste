using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceOrdemVenda : IAppServiceBase<OrdemVendaDto>
    {
        // Task<bool> Inactive(Guid id);
        Task<IEnumerable<BuscaOrdemVendaDto>> ConsultaOrdem();
        // Task<bool> UpdateAsync(StatusOrdemVendaDto obj);
        Task<bool> GerarFluxo(CriaFluxoOrdemVendaDto fluxo, string URL);
        bool GerarBloqueioFluxo(CriaFluxoOrdemVendaDto fluxo);
        Task<OrdemVendaDto> GetOrdemAsync(string Documento);
        Task<SolicitanteFluxoDto> GetSolicitanteAsync(Guid SolicitanteID, string EmpresaID);
        Task<IEnumerable<LiberacaoOrdemVendaFluxoDto>> GetFluxoSolicitanteAsync(Guid SolicitanteID, string EmpresaID);
        Task<bool> SolicitarBloqueioCarregamento(Guid usuarioId, string empresaId, List<SolicitacaoBloqueioRemessaDto> remessas);
        Task<bool> LiberarBloqueioCarregamento(Guid usuarioId, string empresaId, List<SolicitacaoBloqueioRemessaDto> remessas);
        Task<IEnumerable<LogEnvioOrdensSAPDto>> GetLogEnvioOrdensSAP(string empresaId);
        Task<IEnumerable<DivisaoRemessaCockPitDto>> BuscaOrdemVenda(Guid ContaClienteID, string EmpresaID);
        Task<IEnumerable<BuscaRemessasClienteDto>> BuscaRemessasCliente(Guid accountClientID, string empresaID, string tipoRemessa);
    }
}
