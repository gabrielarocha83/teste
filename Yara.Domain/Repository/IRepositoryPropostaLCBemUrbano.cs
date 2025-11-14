using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCBemUrbano : IRepositoryBase<PropostaLCBemUrbano>
    {

        void Delete(PropostaLCBemUrbano obj);

    }
}
