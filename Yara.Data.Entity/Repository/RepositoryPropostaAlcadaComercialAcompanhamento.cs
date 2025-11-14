using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaAlcadaComercialAcompanhamento : RepositoryBase<PropostaAlcadaComercialAcompanhamento> , IRepositoryPropostaAlcadaComercialAcompanhamento
    {
        public RepositoryPropostaAlcadaComercialAcompanhamento(DbContext context) : base(context)
        {
        }
    }
}
