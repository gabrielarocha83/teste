using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePropostaLCAdicional : IAppServiceBase<PropostaLCAdicionalDto>
    {
        Task<bool> InsertPropostalAsync(PropostaLCAdicionalDto propostaLCAdicional);
        Task<bool> UpdatePropostalAsync(PropostaLCAdicionalDto propostaLCAdicional);
        Task<bool> CancelProposalAsync(Guid propostaLCAdicionalID, Guid usuarioIDAlteracao, string URL);
        Task<bool> FixLimitProposalAsync(ContaClienteFinanceiroDto clienteFinanceiroDto, string URL);
    }
}
