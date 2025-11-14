using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryOrdemVenda : IRepositoryBase<OrdemVenda>
    {
        int GetMaxNumeroInterno();
        bool InsertSolicitante(SolicitanteFluxo fluxo);
        Task<OrdemVenda> GetOrdemAsync(string Numero);
        Task<DivisaoRemessa> GetAsyncDivisaoRemessa(Expression<Func<DivisaoRemessa, bool>> expression);
        Task<IEnumerable<BuscaOrdemVenda>> ConsultaOrdem();
        Task<IEnumerable<BuscaRemessasCliente>> GetClientDeliveries(Guid accountClientID, string empresaID, string tipoRemessa);
        Task<IEnumerable<LogEnvioOrdensSAP>> GetLogEnvioOrdensSAP(string empresaId);
    }
}
