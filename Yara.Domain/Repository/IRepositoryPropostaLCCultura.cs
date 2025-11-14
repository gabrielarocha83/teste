using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCCultura : IRepositoryBase<PropostaLCCultura>
    {

        void Delete(PropostaLCCultura obj);

    }
}
