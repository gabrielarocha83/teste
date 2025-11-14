using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaJuridicoTitulo : RepositoryBase<PropostaJuridicoTitulo>, IRepositoryPropostaJuridicoTitulo
    {
        public RepositoryPropostaJuridicoTitulo(DbContext context) : base(context)
        {
        }
    }
}
