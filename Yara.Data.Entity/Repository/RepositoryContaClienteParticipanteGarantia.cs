using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryContaClienteParticipanteGarantia : RepositoryBase<ContaClienteParticipanteGarantia>, IRepositoryContaClienteParticipanteGarantia
    {
        public RepositoryContaClienteParticipanteGarantia(DbContext context) : base(context)
        {
        }

        public void Delete(ContaClienteParticipanteGarantia obj)
        {
            _context.Set<ContaClienteParticipanteGarantia>().Attach(obj);
            _context.Entry<ContaClienteParticipanteGarantia>(obj).State = EntityState.Deleted;
        }
    }
}
