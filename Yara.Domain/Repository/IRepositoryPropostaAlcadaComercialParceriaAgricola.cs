using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaAlcadaComercialParceriaAgricola : IRepositoryBase<PropostaAlcadaComercialParceriaAgricola>
    {

        void Delete(PropostaAlcadaComercialParceriaAgricola obj);

    }
}
