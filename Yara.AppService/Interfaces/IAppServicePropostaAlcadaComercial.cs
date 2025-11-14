using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePropostaAlcadaComercial
    {
        Task<PropostaAlcadaComercialDto> InsertAsync(PropostaAlcadaComercialDto obj);
        Task<PropostaAlcadaComercialDto> GetAsync(Guid proposal, Guid user);
        Task<bool> UdpateAsync(PropostaAlcadaComercialDto proposta);
        Task<IEnumerable<PropostaAlcadaComercialRestricoesDto>> BuscaRestricaoAlcada(Guid contaClienteId, string empresaId);
        Task<bool> CancelAsync(Guid propostaId, Guid guid, string URL);
        Task<bool> UpdateOwner(PropostaAlcadaComercialDto proposta);
        Task<bool> SendCtc(PropostaAlcadaComercialDto titulos, string URL);
        Task<bool> AprovaAlcadaComercial(PropostaAlcadaComercialDto alcadaComercial, string URL);
        Task<bool> SendAnalysis(PropostaAlcadaComercialDto proposta, string URL);
        Task<bool> Disapprove(PropostaAlcadaComercialDto proposta, string URL);
    }
}
