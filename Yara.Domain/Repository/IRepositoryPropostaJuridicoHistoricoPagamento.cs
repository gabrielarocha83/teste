using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaJuridicoHistoricoPagamento : IRepositoryBase<PropostaJuridicoHistoricoPagamento>
    {
        Task<IEnumerable<PropostaJuridicoHistoricoPagamento>> BuscaHistoricoPagamento(Guid propostaJuridicoId, Guid contaClienteId);
    }
}
