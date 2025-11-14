using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.Domain.Entities.Procedures;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePropostaJuridico : IAppServiceBase<PropostaJuridicoDto>
    {
        Task<bool> InsertAsync(PropostaJuridicoDto obj);
        Task<PropostaJuridicoDto> CriaProposta(Guid contaClienteId, Guid usuarioId, string empresaId);
        Task<bool> UpdateWithHistoric(PropostaJuridicoDto obj, ContaClienteDto clienteDto);
        Task<IEnumerable<ControleCobrancaEnvioJuridicoDto>> BuscaControleCobranca(string empresaId, string diretoriaCodigo);
        Task<bool> Inactive(Guid id);
    }
}
