using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaAlcadaComercialParceriaAgricola : RepositoryBase<PropostaAlcadaComercialParceriaAgricola>, IRepositoryPropostaAlcadaComercialParceriaAgricola
    {
        public RepositoryPropostaAlcadaComercialParceriaAgricola(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaAlcadaComercialParceriaAgricola obj)
        {
            _context.Set<PropostaAlcadaComercialParceriaAgricola>().Attach(obj);
            _context.Entry<PropostaAlcadaComercialParceriaAgricola>(obj).State = EntityState.Deleted;
        }
    }
}
