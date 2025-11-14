using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCGarantia : IRepositoryBase<PropostaLCGarantia>
    {

        Task<PropostaLCGarantia> GetLatest(Expression<Func<PropostaLCGarantia, bool>> expression);
        void Delete(PropostaLCGarantia obj);


    }
}
