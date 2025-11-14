using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaRenovacaoVigenciaLC : IRepositoryBase<PropostaRenovacaoVigenciaLC>
    {
        int GetMaxNumeroInterno();
        Task<IEnumerable<BuscaContaClientePropostaRenovacaoLC>> GetClientListByFilterAsync(FiltroContaClientePropostaRenovacaoVigenciaLC filter);
        Task<Guid?> GetProposalByAccountClient(Guid contaClienteID);
        Task<IEnumerable<BuscaCockpitPropostaRenovacaoVigenciaLC>> GetCockpit(Guid usuarioID, string empresaID);
    }
}
