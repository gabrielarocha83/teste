using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositorySolicitanteFluxo : RepositoryBase<SolicitanteFluxo>, IRepositorySolicitanteFluxo
    {

        public RepositorySolicitanteFluxo(DbContext context) : base(context)
        {
        }

        public async Task<BuscaInformacaoClienteLiberacaoOrdemVenda> BuscaInformacaoCliente(Guid solicitanteFluxoId, string empresaId)
        {
            try
            {
                var obj = await _context.Database.SqlQuery<BuscaInformacaoClienteLiberacaoOrdemVenda>("EXEC spBuscaInformacaoCliente @solicitanteFluxoId, @empresaID",
                    new SqlParameter("solicitanteFluxoId", solicitanteFluxoId),
                    new SqlParameter("empresaID", empresaId)
                ).FirstAsync();

                return obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
