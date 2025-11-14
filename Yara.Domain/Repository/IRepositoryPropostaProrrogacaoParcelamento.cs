using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaProrrogacaoParcelamento : IRepositoryBase<PropostaProrrogacaoParcelamento>
    {
        void Delete(PropostaProrrogacaoParcelamento obj);
    }
}
