using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCMercado : IRepositoryBase<PropostaLCMercado>
    {

        void Delete(PropostaLCMercado obj);

    }
}
