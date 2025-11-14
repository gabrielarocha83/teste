using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaAlcadaComercialProdutoServico : IRepositoryBase<PropostaAlcadaComercialProdutoServico>
    {

        void Delete(PropostaAlcadaComercialProdutoServico obj);

    }
}
