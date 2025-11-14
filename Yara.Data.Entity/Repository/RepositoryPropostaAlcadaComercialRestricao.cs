using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaAlcadaComercialRestricao : RepositoryBase<PropostaAlcadaComercialRestricoes>, IRepositoryPropostaAlcadaComercialRestricao
    {
        public RepositoryPropostaAlcadaComercialRestricao(DbContext context) : base(context)
        {
        }
    }
}
