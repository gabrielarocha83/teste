using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePropostaJuridicoHistoricoPagamento : IAppServiceBase<PropostaJuridicoHistoricoPagamentoDto>
    {
        Task<IEnumerable<PropostaJuridicoHistoricoPagamentoDto>> BuscaHistoricoPagamento(Guid propostaJuridicoId, Guid contaClienteId);
        
    }
}
