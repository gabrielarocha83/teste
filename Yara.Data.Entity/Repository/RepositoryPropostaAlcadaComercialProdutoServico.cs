using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaAlcadaComercialProdutoServico : RepositoryBase<PropostaAlcadaComercialProdutoServico>, IRepositoryPropostaAlcadaComercialProdutoServico
    {
        public RepositoryPropostaAlcadaComercialProdutoServico(DbContext context) : base(context)
        {
        }

        public void Delete(PropostaAlcadaComercialProdutoServico obj)
        {
            _context.Set<PropostaAlcadaComercialProdutoServico>().Attach(obj);
            _context.Entry<PropostaAlcadaComercialProdutoServico>(obj).State = EntityState.Deleted;
        }
    }
}
