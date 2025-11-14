using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCParceriaAgricola : IRepositoryBase<PropostaLCParceriaAgricola>
    {

        void Delete(PropostaLCParceriaAgricola obj);

    }
}
