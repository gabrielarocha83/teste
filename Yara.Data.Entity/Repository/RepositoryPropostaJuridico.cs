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
    public class RepositoryPropostaJuridico : RepositoryBase<PropostaJuridico>, IRepositoryPropostaJuridico
    {

        public RepositoryPropostaJuridico(DbContext context) : base(context)
        {
        }

        public int GetMaxNumeroInterno()
        {
            try
            {
                if (!_context.Set<PropostaJuridico>().Any())
                    return 1;

                return (_context.Set<PropostaJuridico>().Max(p => p.NumeroInternoProposta)) + 1;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<PropostaJuridico> CriaPropostaJuridica(Guid contaClienteId, Guid usuarioId, string empresaId)
        {
            try
            {
                var obj = await _context.Database.SqlQuery<PropostaJuridico>("EXEC spCriaPropostaJuridico @pContaClienteId, @pUsuario, @pEmpresaId",
                    new SqlParameter("pContaClienteId", contaClienteId),
                    new SqlParameter("pUsuario", usuarioId),
                    new SqlParameter("pEmpresaId", empresaId)
                ).FirstAsync();

                return obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<ControleCobrancaEnvioJuridico>> BuscaControleCobranca(string empresaId, string diretoriaCodigo)
        {
            try
            {
                IEnumerable<ControleCobrancaEnvioJuridico> list = await _context.Database.SqlQuery<ControleCobrancaEnvioJuridico>("EXEC spBuscaControleCobrancaPropostaJuridico @pEmpresaId, @pDiretoria",
                    new SqlParameter("@pEmpresaId", empresaId),
                    new SqlParameter("@pDiretoria", diretoriaCodigo == "g" ? DBNull.Value : (object)diretoriaCodigo)
                ).ToListAsync();

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
