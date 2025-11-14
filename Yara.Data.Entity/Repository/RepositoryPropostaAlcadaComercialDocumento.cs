using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaAlcadaComercialDocumento : RepositoryBase<PropostaAlcadaComercialDocumentos>, IRepositoryPropostaAlcadaComercialDocumento
    {
        public RepositoryPropostaAlcadaComercialDocumento(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaAlcadaComercialDocumentos obj)
        {
            _context.Set<PropostaAlcadaComercialDocumentos>().Attach(obj);
            _context.Entry<PropostaAlcadaComercialDocumentos>(obj).State = EntityState.Deleted;
        }
    }
}
