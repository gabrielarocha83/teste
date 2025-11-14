
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaProrrogacaoTitulo : IRepositoryBase<PropostaProrrogacaoTitulo>
    {
        void InsertRange(IEnumerable<PropostaProrrogacaoTitulo> titulos);
    }
}
