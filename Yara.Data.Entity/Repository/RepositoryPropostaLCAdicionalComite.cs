using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaLCAdicionalComite : RepositoryBase<PropostaLCAdicionalComite>, IRepositoryPropostaLCAdicionalComite
    {
        public RepositoryPropostaLCAdicionalComite(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PropostaLCAdicionalComite>> InsertPropostaLCAdicionalComite(Guid id, Guid SegmentoID, string CodigoSAP, Guid usuarioID, string EmpresaID)
        {
            try
            {
                IEnumerable<PropostaLCAdicionalComite> list = await _context.Database.SqlQuery<PropostaLCAdicionalComite>("EXEC spInserirComitePropostaLCAdicional @CodigoSAP, @PropostaID, @SegmentoID, @UsuarioID, @EmpresaID",
                    new SqlParameter("CodigoSAP", CodigoSAP),
                    new SqlParameter("PropostaID", id),
                    new SqlParameter("SegmentoID", SegmentoID),
                    new SqlParameter("UsuarioID", usuarioID),
                    new SqlParameter("EmpresaID", EmpresaID)
                ).ToListAsync();

                return list;
            }
            catch
            {
                throw new ArgumentException("Por favor valide o cadastro do fluxo de aprovação dos usuarios para enviar ao comitê.");
            }
        }

        public async Task<PropostaLCAdicionalComite> InsertFluxo(PropostaLCAdicionalComite comite)
        {
            try
            {
                var fluxo = await _context.Database.SqlQuery<PropostaLCAdicionalComite>("EXEC spFluxoComiteAdicional @Comite, @Valor, @Status, @Descricao, @UsuarioID",
                    new SqlParameter("Comite", comite.ID),
                    new SqlParameter("Valor", comite.ValorEstipulado),
                    new SqlParameter("Status", comite.PropostaLCAdicionalStatusComiteID),
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
                var comite = await _context.Database.SqlQuery<PropostaLCAdicionalComite>("EXEC spAbortaFluxoComiteAdicional @PropostaLCAdicionalId",
                    new SqlParameter("PropostaLCAdicionalId", propostaId)
                ).FirstOrDefaultAsync();

                return comite == null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
