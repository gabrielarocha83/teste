
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaAbonoTitulo : IRepositoryBase<PropostaAbonoTitulo>
    {
        void InsertRange(IEnumerable<PropostaAbonoTitulo> titulos);
    }
}
