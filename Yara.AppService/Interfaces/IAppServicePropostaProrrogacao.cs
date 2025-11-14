using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePropostaProrrogacao
    {
        Task<bool> InsertAsync(PropostaProrrogacaoInserirDto obj);
        Task<bool> InsertPaymentAsync(PropostaProrrogacaoInserirDto obj);
        Task<bool> UdpateAsync(PropostaProrrogacaoDto obj);
        Task<bool> SendCollection(PropostaProrrogacaoDto obj, string URL);
        Task<bool> SendCommittee(PropostaProrrogacaoDto obj, string URL);
        Task<PropostaProrrogacaoDto> GetAsync(Guid ID, Guid usuarioID);
        Task<IEnumerable<PropostaProrrogacaoComiteDto>> GetCommitteeAsync(Guid ID);
        Task<bool> CancelAsync(Guid PropostaID, string EmpresaID, Guid Usuario, string URL);
        Task<bool> AprovaReprovaProrrogacao(AprovaReprovaProrrogacaoDto prorrogacao, string URL);
        Task<bool> EfetivaProrrogacao(PropostaProrrogacaoDto prorrogacao, string URL);
        Task<IEnumerable<BuscaDetalhesPropostaProrrogacaoTituloDto>> BuscaDetalhesTitulos(Guid propostaProrrogacaoId, string empresaId);
    }
}
