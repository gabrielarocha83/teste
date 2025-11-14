using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePropostaRenovacaoVigenciaLC : IAppServiceBase<PropostaRenovacaoVigenciaLCDto>
    {
        Task<IEnumerable<BuscaContaClientePropostaRenovacaoLCDto>> GetClientListByFilterAsync(FiltroContaClientePropostaRenovacaoVigenciaLCDto filter);
        Task<IEnumerable<BuscaContaClientePropostaRenovacaoLCDto>> GetClientListByFilterAsync(ListaClientePropostaRenovacaoVigenciaLCDto clientes, string empresaID);
        Task<byte[]> GetClientListExcelByFilterAsync(FiltroContaClientePropostaRenovacaoVigenciaLCDto filter);
        Task<bool> InsertPropostalAsync(ListaClientePropostaRenovacaoVigenciaLCDto clientes, Guid usuarioID, Guid propostaRenovacaoVigenciaLCID, string empresaID, string urlSerasa, string usuarioSerasa, string senhaSerasa);
        Task<byte[]> GetProposalClientListExcelAsync(Guid propostaRenovacaoVigenciaLCID);
        Task<bool> CancelProposalAsync(Guid propostaRenovacaoVigenciaLCID, Guid usuarioIDAlteracao);
    }
}
