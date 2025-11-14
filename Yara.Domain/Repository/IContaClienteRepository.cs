using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IContaClienteRepository : IBaseRepository<ContaCliente>
    {
        Task<IEnumerable<BuscaContaCliente>> BuscaContaCliente(BuscaContaCliente contaCliente);
    }
    
}