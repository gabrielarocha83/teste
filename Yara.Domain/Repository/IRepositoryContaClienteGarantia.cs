using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryContaClienteGarantia : IRepositoryBase<ContaClienteGarantia>
    {
        int? GetMaxNumeroInterno();
    }
}
