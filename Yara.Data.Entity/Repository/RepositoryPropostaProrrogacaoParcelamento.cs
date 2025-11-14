using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaProrrogacaoParcelamento : RepositoryBase<PropostaProrrogacaoParcelamento>, IRepositoryPropostaProrrogacaoParcelamento
    {
        public RepositoryPropostaProrrogacaoParcelamento(DbContext context) : base(context)
        {

        }

        public void Delete(PropostaProrrogacaoParcelamento obj)
        {
            _context.Set<PropostaProrrogacaoParcelamento>().Attach(obj);
            _context.Entry<PropostaProrrogacaoParcelamento>(obj).State = EntityState.Deleted;
        }
    }
}
