using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePropostaRenovacaoVigenciaLCComite : IAppServiceBase<PropostaRenovacaoVigenciaLCComiteDto>
    {
        Task<bool> SendCommittee(Guid propostaRenovacaoVigenciaLCID, Guid usuarioIDAlteracao, string URL);
        Task<IEnumerable<PropostaRenovacaoVigenciaLCComiteDto>> GetCommitteeByProposalID(Guid propostaRenovacaoVigenciaLCID);
        Task<bool> SendApproval(DecisaoComitePropostaRenovacaoVigenciaLCDto decisaoComitePropostaRenovacaoVigenciaLC, string URL);
    }
}
