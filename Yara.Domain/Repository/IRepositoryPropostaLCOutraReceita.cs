using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCOutraReceita : IRepositoryBase<PropostaLCOutraReceita>
    {

        void Delete(PropostaLCOutraReceita obj);

    }
}
