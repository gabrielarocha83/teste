using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCTipoEndividamento : IRepositoryBase<PropostaLCTipoEndividamento>
    {

        void Delete(PropostaLCTipoEndividamento obj);

    }
}
