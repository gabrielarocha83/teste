using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaLCComite : RepositoryBase<PropostaLCComite>, IRepositoryPropostaLCComite
    {

        public RepositoryPropostaLCComite(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PropostaLCComite>> InsertPropostaLCComite(Guid id, Guid segmentoId, string codigoSap, Guid usuarioId, string EmpresaID)
        {
            try
            {
                IEnumerable<PropostaLCComite> list = await _context.Database.SqlQuery<PropostaLCComite>("EXEC spInserirComiteProposta @CodigoSAP, @PropostaID, @SegmentoID, @UsuarioID, @EmpresaID",
                    new SqlParameter("CodigoSAP", codigoSap),
                    new SqlParameter("PropostaID", id),
                    new SqlParameter("SegmentoID", segmentoId),
                    new SqlParameter("UsuarioID", usuarioId),
                    new SqlParameter("EmpresaID", EmpresaID)).ToListAsync();

                return list;
            }
            catch
            {
                throw new ArgumentException("Por favor valide o cadastro do fluxo de aprovação dos usuarios para enviar ao comitê.");
            }
        }

        public async Task<PropostaLCComite> InsertFluxo(PropostaLCComite comite)
        {
            try
            {
                var fluxo = await _context.Database.SqlQuery<PropostaLCComite>("EXEC spFluxoComite @Comite, @Valor, @Status, @Descricao, @UsuarioID",
                    new SqlParameter("Comite", comite.ID),
                    new SqlParameter("Valor", comite.ValorEstipulado),
                    new SqlParameter("Status", comite.StatusComiteID),
                    new SqlParameter("Descricao", comite.Comentario),
                    new SqlParameter("UsuarioID", comite.UsuarioID)
                ).FirstOrDefaultAsync();

                return fluxo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> AbortaComite(Guid propostaId)
        {
            try
            {
                var comite = await _context.Database.SqlQuery<PropostaLCComite>("EXEC spAbortaFluxoComite @propostaLcId", new SqlParameter("propostaLcId", propostaId)).FirstOrDefaultAsync();

                return comite == null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
