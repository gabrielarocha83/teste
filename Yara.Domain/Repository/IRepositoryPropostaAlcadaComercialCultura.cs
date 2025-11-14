using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaAlcadaComercialCultura : IRepositoryBase<PropostaAlcadaComercialCultura>
    {

        void Delete(PropostaAlcadaComercialCultura obj);

    }
}
