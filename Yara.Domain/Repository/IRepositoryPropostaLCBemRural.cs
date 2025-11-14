using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCBemRural : IRepositoryBase<PropostaLCBemRural>
    {

        void Delete(PropostaLCBemRural obj);

    }
}
