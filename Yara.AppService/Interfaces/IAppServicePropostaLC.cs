using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePropostaLC
    {
        Task<PropostaLCValidacaoDto> ValidaProposta(Guid ContaClienteID, Guid Usuario, string empresaID);
        Task<PropostaLCDto> Save(PropostaLCDto propostaLc);
        Task<bool> SaveStatusWithAnexo(PropostaLCDto proposta);
        Task<bool> SaveStatus(PropostaLCDto proposta, string Mensagem, string URL);
        Task<bool> SaveStatusAbortaComite(PropostaLCDto proposta);
        Task<bool> SaveStatusWithRepresentante(PropostaLCDto proposta, string Mensagem, string URL);
        Task<bool> SaveStatusWithPending(PropostaLCDto proposta, string Mensagem, string URL);
        Task<bool> LimitFixed(ContaClienteFinanceiroDto clienteFinanceiroDto, string URL);
        Task<bool> LimitFixedPartial(ContaClienteFinanceiroDto clienteFinanceiroDto);
        Task<List<BuscaPropostaLCContaClienteDto>> GetPropostaLCContaCliente(Guid ContaCliente, string EmpresaID);
        Task<PropostaLCDto> GetProposalByAccountID(Guid id, string empresaId, [Optional] Guid propostaId);
        Task<PropostaLCDto> GetProposalByID(Guid ID, string empresaID);
        Task<PropostaLCPatrimoniosDto> GetPatrimonio(string documento);
        Task<PropostaLcTodasReceitasDto> GetTodasReceitas(string documento);
        Task<IEnumerable<PropostaLCDto>> GetPropostalList(Guid id);
        Task<IEnumerable<BuscaGrupoEconomicoPropostaLCDto>> BuscaGrupoEconomicoPropostaLc(Guid grupoEconomicoId);
    }
}
