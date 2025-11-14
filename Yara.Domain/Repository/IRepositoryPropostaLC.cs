using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLC : IRepositoryBase<PropostaLC>
    {
        void RefreshObject(PropostaLC obj);
        int GetMaxNumeroInterno();
        Task<IEnumerable<BuscaCockpitPropostaLC>> BuscaPropostaLC(Guid usuarioID, string EmpresaID);
        Task<IEnumerable<BuscaPropostaLCPorStatus>> BuscaPropostaLCPorStatus(string empresaID, IEnumerable<string> propostaLCStatusIDs);
        Task<IEnumerable<BuscaCockpitPropostaLC>> BuscaPropostaLCAcompanhamento(Guid usuarioID, string EmpresaID);
        Task<PropostaLC> BuscaUltimaPropostaContaCliente(Guid contaClienteId, string empresaId);
        Task<IEnumerable<BuscaGrupoEconomicoPropostaLC>> BuscaGrupoEconomicoPropostaLc(Guid grupoEconomicoId);
        Task<PropostaLC> CriaPropostaCopiaAnterior(Guid usuarioCriacao, int numeroProposta, Guid responsavelPropostaId, Guid contaClienteId, string empresaid);
        Task<List<BuscaPropostaLCContaCliente>> GetPropostaLCContaCliente(Guid ContaCliente, string EmpresaID);
        PropostaLC GetAllFilterOrderBy(Expression<Func<PropostaLC, bool>> expression, Expression<Func<PropostaLC, DateTime>> expression2);
        Task<IEnumerable<PropostaLC>> GetAllFilterOrderByAsyncList(Expression<Func<PropostaLC, bool>> expression, Expression<Func<PropostaLC, DateTime>> expression2);
        Task<IEnumerable<PropostaLCCultura>> GetCulturasCliente(Guid contaClienteId, string empresaId);
        Task<PropostaLC> GetLatest(Expression<Func<PropostaLC, bool>> expression);
    }
}
