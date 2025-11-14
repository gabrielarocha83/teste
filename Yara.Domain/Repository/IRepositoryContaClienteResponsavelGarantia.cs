using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryContaClienteResponsavelGarantia : IRepositoryBase<ContaClienteResponsavelGarantia>
    {
        void Delete(ContaClienteResponsavelGarantia obj);
    }
}
