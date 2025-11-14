using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaAlcadaComercialCultura : RepositoryBase<PropostaAlcadaComercialCultura>, IRepositoryPropostaAlcadaComercialCultura
    {
        public RepositoryPropostaAlcadaComercialCultura(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaAlcadaComercialCultura obj)
        {
            _context.Set<PropostaAlcadaComercialCultura>().Attach(obj);
            _context.Entry<PropostaAlcadaComercialCultura>(obj).State = EntityState.Deleted;
        }
    }
}
