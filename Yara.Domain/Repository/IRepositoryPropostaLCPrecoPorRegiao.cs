using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCPrecoPorRegiao : IRepositoryBase<PropostaLCPrecoPorRegiao>
    {

        void Delete(PropostaLCPrecoPorRegiao obj);

    }
}
