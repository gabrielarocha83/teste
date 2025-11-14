using System;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositorySolicitanteFluxo : IRepositoryBase<SolicitanteFluxo>
    {
        Task<BuscaInformacaoClienteLiberacaoOrdemVenda> BuscaInformacaoCliente(Guid solicitanteFluxoId, string empresaId);
    }
}
