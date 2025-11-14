using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaLC : RepositoryBase<PropostaLC>, IRepositoryPropostaLC
    {

        public RepositoryPropostaLC(DbContext context) : base(context)
        {
        }

        public void RefreshObject(PropostaLC obj)
        {
            this._context.Entry(obj).GetDatabaseValues();
        }

        public int GetMaxNumeroInterno()
        {
            try
            {
                if (!_context.Set<PropostaLC>().Any())
                    return 1;

                return (_context.Set<PropostaLC>().Max(p => p.NumeroInternoProposta)) + 1;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BuscaCockpitPropostaLC>> BuscaPropostaLC(Guid usuarioID, string EmpresaID)
        {
            try
            {
                IEnumerable<BuscaCockpitPropostaLC> list = await _context.Database.SqlQuery<BuscaCockpitPropostaLC>("EXEC spBuscaCockpitPropostaLC @Usuario, @EmpresaID",
                    new SqlParameter("Usuario", usuarioID),
                    new SqlParameter("EmpresaID", EmpresaID)
                ).ToListAsync();

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BuscaPropostaLCPorStatus>> BuscaPropostaLCPorStatus(string empresaID, IEnumerable<string> propostaLCStatusIDs)
        {
            try
            {
                var list = await _context.Database.SqlQuery<BuscaPropostaLCPorStatus>("EXEC spBuscaPropostaLCPorStatus @EmpresaID, @PropostaLCStatusIDs",
                    new SqlParameter("EmpresaID", empresaID),
                    new SqlParameter("PropostaLCStatusIDs", propostaLCStatusIDs.Aggregate((a, b) => $"{a},{b}"))
                ).ToListAsync();

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BuscaCockpitPropostaLC>> BuscaPropostaLCAcompanhamento(Guid usuarioID, string EmpresaID)
        {
            try
            {
                IEnumerable<BuscaCockpitPropostaLC> list = await _context.Database.SqlQuery<BuscaCockpitPropostaLC>("EXEC spBuscaCockpitPropostaLCAcompanhamento @Usuario, @EmpresaID",
                    new SqlParameter("Usuario", usuarioID),
                    new SqlParameter("EmpresaID", EmpresaID)
                ).ToListAsync();

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PropostaLC> BuscaUltimaPropostaContaCliente(Guid contaClienteId, string empresaId)
        {
            try
            {
                var proposta = await _context.Set<PropostaLC>().Where(p => p.ContaClienteID == contaClienteId && p.EmpresaID == empresaId).OrderByDescending(p => p.DataCriacao).FirstOrDefaultAsync();
                return proposta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BuscaGrupoEconomicoPropostaLC>> BuscaGrupoEconomicoPropostaLc(Guid grupoEconomicoId)
        {
            try
            {
                IEnumerable<BuscaGrupoEconomicoPropostaLC> list = await _context.Database.SqlQuery<BuscaGrupoEconomicoPropostaLC>("EXEC spBuscaGrupoEconomicoPropostaLC @GrupoEconomicoId",
                    new SqlParameter("GrupoEconomicoId", grupoEconomicoId)).ToListAsync();

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PropostaLC> CriaPropostaCopiaAnterior(Guid usuarioCriacao, int numeroProposta, Guid responsavelPropostaId, Guid contaClienteId, string empresaId)
        {
            try
            {
                var obj = await _context.Database.SqlQuery<PropostaLC>("EXEC spCriaPropostaLCCopiaAnterior @UsuarioCriacao, @NumeroInterno, @ResponsavelPropostaId, @ContaClienteId, @EmpresaId",
                    new SqlParameter("UsuarioCriacao", usuarioCriacao),
                    new SqlParameter("NumeroInterno", numeroProposta),
                    new SqlParameter("ResponsavelPropostaId", responsavelPropostaId),
                    new SqlParameter("ContaClienteId", contaClienteId),
                    new SqlParameter("EmpresaId", empresaId)
                ).FirstAsync();

                return obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public async Task<List<BuscaPropostaLCContaCliente>> GetPropostaLCContaCliente(Guid ContaCliente, string EmpresaID)
        {
            try
            { 
                var obj = await _context.Database.SqlQuery<BuscaPropostaLCContaCliente>("EXEC [spBuscaPropostaLCContaCliente] @ContaClienteID, @EmpresaID",
                    new SqlParameter("ContaClienteID", ContaCliente),
                    new SqlParameter("EmpresaID", EmpresaID)
                ).ToListAsync();

                return obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PropostaLC GetAllFilterOrderBy(Expression<Func<PropostaLC, bool>> expression, Expression<Func<PropostaLC, DateTime>> expression2)
        {
            try
            {
                return _context.Set<PropostaLC>().AsNoTracking().Where(expression).OrderByDescending(expression2).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<PropostaLC>> GetAllFilterOrderByAsyncList(Expression<Func<PropostaLC, bool>> expression, Expression<Func<PropostaLC, DateTime>> expression2)
        {
            try
            {
                return await _context.Set<PropostaLC>().AsNoTracking().Where(expression).OrderByDescending(expression2).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<PropostaLCCultura>> GetCulturasCliente(Guid contaClienteId, string empresaId)
        {
            IEnumerable<PropostaLCCultura> retorno = null;

            try
            {
                var ultimaProposta = await _context.Set<PropostaLC>().Where(plc => plc.ContaClienteID == contaClienteId && plc.EmpresaID == empresaId).OrderByDescending(plc => plc.DataAlteracao).Select(plc => plc.ID).FirstOrDefaultAsync();

                if (ultimaProposta != null && ultimaProposta != Guid.Empty)
                    retorno = await _context.Set<PropostaLC>().Where(plc => plc.ID == ultimaProposta).SelectMany(plc => plc.Culturas).ToListAsync();

                return retorno;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PropostaLC> GetLatest(Expression<Func<PropostaLC, bool>> expression)
        {
            try
            {
                return await _context.Set<PropostaLC>().Where(expression).OrderByDescending(p => p.DataCriacao).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
