using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryOrdemVendaFluxo : IRepositoryBase<OrdemVendaFluxo>
    {
        Task<LiberacaoOrdemVendaFluxo> FluxoAprovacaoOrdem(LiberacaoOrdemVendaFluxo ordem);
        OrdemVendaFluxo BloqueioFluxoOrdem(Guid SolicitanteID, Guid UsuarioID, string EmpresaID);
        Task<IEnumerable<BuscaOrdemVendasAVista>> OrdensAprovacao(Guid Solicitante, String Empresa);
        void InsertRange(List<OrdemVendaFluxo> ordens);
    }
}
