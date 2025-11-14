using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaAlcadaComercial : IRepositoryBase<PropostaAlcadaComercial>
    {
        int GetMaxNumeroInterno();
        Task<IEnumerable<PropostaAlcadaComercialRestricoes>> BuscaRestricaoAlcada(Guid contaClienteId, string empresaId);
        Task<IEnumerable<BuscaCockpitPropostaAlcada>> BuscaPropostaCockpit(Guid usuarioId, string empresaId, bool acompanhar);
        Task<PropostaAlcadaComercial> GetMaxProposal();
        Task<PropostaAlcadaComercial> GetLatest(Expression<Func<PropostaAlcadaComercial, bool>> expression);
    }
}
