using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCPecuaria : IRepositoryBase<PropostaLCPecuaria>
    {

        void Delete(PropostaLCPecuaria obj);

    }
}
