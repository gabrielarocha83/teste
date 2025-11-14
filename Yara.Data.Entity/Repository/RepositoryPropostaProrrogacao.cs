using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaProrrogacao : RepositoryBase<PropostaProrrogacao>, IRepositoryPropostaProrrogacao
    { 

        public RepositoryPropostaProrrogacao(DbContext context) : base(context)
        {
        }

        public int GetMaxNumeroInterno()
        {
            try
            {
                if (!_context.Set<PropostaProrrogacao>().Any())
                    return 1;

                return (_context.Set<PropostaProrrogacao>().Max(p => p.NumeroInternoProposta)) + 1;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BuscaPropostaProrrogacao>> BuscaPendenciasProrrogacao(Guid usuarioID, string EmpresaID, bool Acompanhar)
        {
            try
            {
                IEnumerable<BuscaPropostaProrrogacao> list = await _context.Database.SqlQuery<BuscaPropostaProrrogacao>("EXEC spBuscaCockpitPropostaProrrogacao @Usuario, @EmpresaID, @Acompanhar",
                    new SqlParameter("Usuario", usuarioID),
                    new SqlParameter("EmpresaID", EmpresaID),
                    new SqlParameter("Acompanhar", Acompanhar)
                ).ToListAsync();

                return list;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BuscaDetalhesPropostaProrrogacaoTitulo>> BuscaDetalhesTitulos(Guid propostaProrrogacaoId, string empresaId)
        {
            try
            {
                IEnumerable<BuscaDetalhesPropostaProrrogacaoTitulo> list = await _context.Database.SqlQuery<BuscaDetalhesPropostaProrrogacaoTitulo>("EXEC spBuscaDetalhesProrrogacaoTitulo @pEmpresaID, @pProposta",
                    new SqlParameter("@pEmpresaID", empresaId),
                    new SqlParameter("@pProposta", propostaProrrogacaoId)
                ).ToListAsync();

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PropostaProrrogacaoComite> AprovaReprovaProrrogacao(Guid id, Guid Usuario, bool NovasLiberacoes, bool Aprovado, float taxa, string Descricao, string EmpresaID)
        {
            try
            {
                var list = await _context.Database.SqlQuery<PropostaProrrogacaoComite>("EXEC spAprovaReprovaProrrogacao @Comite,@UsuarioID,@NovasLiberacoes,@Aprovado,@taxaJuros, @Descricao, @EmpresaID",
                    new SqlParameter("Comite", id),
                    new SqlParameter("UsuarioID", Usuario),
                    new SqlParameter("NovasLiberacoes", NovasLiberacoes),
                    new SqlParameter("Aprovado", Aprovado),
                    new SqlParameter("taxaJuros", taxa),
                    new SqlParameter("Descricao", Descricao),
                    new SqlParameter("EmpresaID", EmpresaID)).FirstOrDefaultAsync();
                return list;
            }
            catch (Exception)
            {
                throw new ArgumentException("Por favor valide o cadastro do fluxo de aprovação dos usuarios para enviar ao comitê.");
            }
        }

        public async Task<PropostaProrrogacaoComite> InsertPropostaProrrogacaoComite(Guid id, Guid segmentoId, string codigoSap, Guid usuarioId, string EmpresaID)
        {
            try
            {
                var list = await _context.Database.SqlQuery<PropostaProrrogacaoComite>("EXEC spInserirComitePropostaProrrogacao @CodigoSAP, @PropostaID, @SegmentoID, @UsuarioID, @EmpresaID",
                    new SqlParameter("CodigoSAP", codigoSap),
                    new SqlParameter("PropostaID", id),
                    new SqlParameter("SegmentoID", segmentoId),
                    new SqlParameter("UsuarioID", usuarioId),
                    new SqlParameter("EmpresaID", EmpresaID)).FirstOrDefaultAsync();
                return list;
            }
            catch (Exception)
            {
                throw new ArgumentException("Por favor valide o cadastro do fluxo de aprovação dos usuarios para enviar ao comitê.");
            }
        }
    }
}
