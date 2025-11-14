using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCNecessidadeProduto : IRepositoryBase<PropostaLCNecessidadeProduto>
    {

        void Delete(PropostaLCNecessidadeProduto obj);

    }
}
