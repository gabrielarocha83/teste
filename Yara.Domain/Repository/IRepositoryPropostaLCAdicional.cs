using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCAdicional : IRepositoryBase<PropostaLCAdicional>
    {
        int GetMaxNumeroInterno();
        Task<PropostaLCAdicional> GetLatest(Expression<Func<PropostaLCAdicional, bool>> expression);
        Task<IEnumerable<BuscaPropostaLCAdicional>> BuscaPendenciasLCAdicional(Guid usuarioID, string EmpresaID, bool Acompanhar);
    }
}
