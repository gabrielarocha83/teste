using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePropostaAbono
    {
        Task<bool> InsertAutoAsync(PropostaAbonoInserirDto obj);
        Task<bool> InsertAsync(PropostaAbonoInserirDto obj);
        Task<bool> InsertPaymentAsync(PropostaAbonoInserirDto obj);
        Task<bool> UdpateAsync(PropostaAbonoDto obj);
        Task<bool> SendCollection(PropostaAbonoDto obj, string URL);
        Task<bool> SendCommittee(PropostaAbonoDto obj, string URL);
        Task<PropostaAbonoDto> GetAsync(Guid ID, Guid usuarioID);
        Task<IEnumerable<PropostaAbonoComiteDto>> GetCommitteeAsync(Guid ID);
        Task<bool> CancelAsync(Guid PropostaID, string EmpresaID, Guid Usuario, string URL);
        Task<bool> AprovaReprovaAbono(AprovaReprovaAbonoDto aprovacao, string URL);
    }
}
