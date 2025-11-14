using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaAlcadaComercialDocumento : IRepositoryBase<PropostaAlcadaComercialDocumentos>
    {

        void Delete(PropostaAlcadaComercialDocumentos obj);

    }
}
