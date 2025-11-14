using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCMaquinaEquipamento : IRepositoryBase<PropostaLCMaquinaEquipamento>
    {

        void Delete(PropostaLCMaquinaEquipamento obj);

    }
}
