using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryFluxoLiberacaoManual : RepositoryBase<FluxoLiberacaoManual>, IRepositoryFluxoLiberacaoManual
    {
        public RepositoryFluxoLiberacaoManual(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FluxoLiberacaoManual>> GetAllListFluxoAsync()
        {
            try
            {
                IEnumerable<FluxoLiberacaoManual> fluxo = await _context.Database.SqlQuery<FluxoLiberacaoManual>("EXEC spBuscaFluxoLista").ToListAsync();
                return fluxo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
