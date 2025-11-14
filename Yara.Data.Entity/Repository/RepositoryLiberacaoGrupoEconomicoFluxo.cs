using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    internal class RepositoryLiberacaoGrupoEconomicoFluxo : RepositoryBase<LiberacaoGrupoEconomicoFluxo>, IRepositoryLiberacaoGrupoEconomicoFluxo
    {

        public RepositoryLiberacaoGrupoEconomicoFluxo(DbContext context) : base(context)
        {
        }

        public async Task<FluxoGrupoEconomico> AprovaReprovaLiberacaoGrupoEconomico(bool aprovar, Guid GrupoEconomicoID, Guid UsuarioID, int ClassificacaoID, string EmpresaID)
        {
            try
            {
                var fluxo = await _context.Database.SqlQuery<FluxoGrupoEconomico>("EXEC spFluxoLiberacaoBloqueioGrupoEconomico @Aprovado, @GrupoID, @UsuarioID, @EmpresaID, @ClassificacaoGrupoEconomico",
                    new SqlParameter("Aprovado", aprovar),
                    new SqlParameter("GrupoID", GrupoEconomicoID),
                    new SqlParameter("UsuarioID", UsuarioID),
                    new SqlParameter("EmpresaID", EmpresaID),
                    new SqlParameter("ClassificacaoGrupoEconomico", ClassificacaoID)
                ).FirstOrDefaultAsync();

                return fluxo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}