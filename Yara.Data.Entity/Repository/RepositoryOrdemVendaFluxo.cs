using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Yara.Data.Entity.Context;
using Yara.Data.Entity.Migrations;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryOrdemVendaFluxo : RepositoryBase<OrdemVendaFluxo>, IRepositoryOrdemVendaFluxo
    {

        public RepositoryOrdemVendaFluxo(DbContext context) : base(context)
        {

        }

        public async Task<LiberacaoOrdemVendaFluxo> FluxoAprovacaoOrdem(LiberacaoOrdemVendaFluxo ordem)
        {
            try
            {
                var fluxo = await _context.Database.SqlQuery<LiberacaoOrdemVendaFluxo>("EXEC Spaprovareprovafluxoordemvenda @SolicitanteID, @UsuarioID, @Status, @EmpresaId, @Comentario",
                    new SqlParameter("SolicitanteID", ordem.SolicitanteFluxoID),
                    new SqlParameter("UsuarioID", ordem.UsuarioID.Value),
                    new SqlParameter("Status", ordem.StatusOrdemVendasID),
                    new SqlParameter("EmpresaId", ordem.EmpresasId),
                    new SqlParameter("Comentario", ordem.Comentario)
                ).FirstOrDefaultAsync();

                return fluxo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BuscaOrdemVendasAVista>> OrdensAprovacao(Guid Solicitante, String Empresa)
        {
            try
            {
                var fluxo = await _context.Database.SqlQuery<BuscaOrdemVendasAVista>("EXEC spBuscaOrdensAprovacao @SolicitanteID, @EmpresaID",
                    new SqlParameter("SolicitanteID", Solicitante),
                    new SqlParameter("EmpresaID", Empresa)
                ).ToListAsync();

                return fluxo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OrdemVendaFluxo BloqueioFluxoOrdem(Guid SolicitanteID, Guid UsuarioID, string EmpresaID)
        {
            try
            {
                var fluxo = _context.Database.SqlQuery<OrdemVendaFluxo>("EXEC spBloqueioOrdemVenda @SolicitanteID, @UsuarioID, @EmpresaID",
                    new SqlParameter("SolicitanteID", SolicitanteID),
                    new SqlParameter("UsuarioID", UsuarioID),
                    new SqlParameter("EmpresaID", EmpresaID)//,
                   // new SqlParameter("pUsuarioId", ordemVendaFluxo.UsuarioID)
                ).FirstOrDefault();

                return fluxo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void InsertRange(List<OrdemVendaFluxo> ordens)
        {
            _context.Set<OrdemVendaFluxo>().AddRange(ordens);
        }
    }
}
