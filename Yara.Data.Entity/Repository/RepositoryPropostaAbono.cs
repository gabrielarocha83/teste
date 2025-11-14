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
    public class RepositoryPropostaAbono : RepositoryBase<PropostaAbono>, IRepositoryPropostaAbono
    {

        public RepositoryPropostaAbono(DbContext context) : base(context)
        {
        }

        public int GetMaxNumeroInterno()
        {
            try
            {
                if (!_context.Set<PropostaAbono>().Any())
                    return 1;

                return (_context.Set<PropostaAbono>().Max(p => p.NumeroInternoProposta)) + 1;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<Decimal> Total(Guid PropostaID)
        {
            try
            {
                var list = await _context.Database.SqlQuery<decimal>("EXEC spBuscaTotalAbono @PropostaID ",
                    new SqlParameter("PropostaID", PropostaID)).FirstOrDefaultAsync();

                return list;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<PropostaAbonoComite> InsertPropostaAbonoComite(Guid id, Guid segmentoId, string codigoSap, Guid usuarioId, string EmpresaID)
        {
            try
            {
                var list = await _context.Database.SqlQuery<PropostaAbonoComite>("EXEC spInserirComitePropostaAbono @CodigoSAP, @PropostaID, @SegmentoID, @UsuarioID, @EmpresaID",
                    new SqlParameter("CodigoSAP", codigoSap),
                    new SqlParameter("PropostaID", id),
                    new SqlParameter("SegmentoID", segmentoId),
                    new SqlParameter("UsuarioID", usuarioId),
                    new SqlParameter("EmpresaID", EmpresaID)).FirstOrDefaultAsync();

                return list;
            }
            catch(Exception)
            {
                throw new ArgumentException("Por favor valide o cadastro do fluxo de aprovação dos usuarios para enviar ao comitê.");
            }
        }

        public async Task<IEnumerable<BuscaPropostaAbono>> BuscaPendenciasAbono(Guid usuarioID, string EmpresaID, bool Acompanhar)
        {
            try
            {
                IEnumerable<BuscaPropostaAbono> list = await _context.Database.SqlQuery<BuscaPropostaAbono>("EXEC spBuscaCockpitPropostaAbono @Usuario, @EmpresaID, @Acompanhar",
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

        public async Task<PropostaAbonoComite> AprovaReprovaAbono(Guid id, Guid Usuario, bool Conceito, bool Aprovado,string Descricao, string EmpresaID)
        {
            try
            {
                var list = await _context.Database.SqlQuery<PropostaAbonoComite>("EXEC spAprovaReprovaAbono @Comite, @UsuarioID, @Conceito, @Aprovado, @Descricao, @EmpresaID",
                    new SqlParameter("Comite", id),
                    new SqlParameter("UsuarioID", Usuario),
                    new SqlParameter("Conceito", Conceito),
                    new SqlParameter("Aprovado", Aprovado),
                    new SqlParameter("Descricao", Descricao),
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
