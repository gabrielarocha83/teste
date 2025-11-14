using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryContaClienteParticipanteGarantia : IRepositoryBase<ContaClienteParticipanteGarantia>
    {
        void Delete(ContaClienteParticipanteGarantia obj);
    }


}
