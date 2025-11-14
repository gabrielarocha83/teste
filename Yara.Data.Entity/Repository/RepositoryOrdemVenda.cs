using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryOrdemVenda : RepositoryBase<OrdemVenda>, IRepositoryOrdemVenda
    {
        public RepositoryOrdemVenda(DbContext context): base(context)
        {
        
        }

        public int GetMaxNumeroInterno()
        {
            if (_context.Set<SolicitanteFluxo>().Any())
            {
                var maxNumero = _context.Set<SolicitanteFluxo>().Max(p => p.NumeroInternoProposta);
                return maxNumero + 1;
            }
            else
                return 1;
        }

        public bool InsertSolicitante(SolicitanteFluxo fluxo)
        {
            try
            {
                _context.Set<SolicitanteFluxo>().Add(fluxo);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<OrdemVenda> GetOrdemAsync(string Numero)
        {
            return _context.Set<OrdemVenda>().Include(c => c.Itens).Include(c => c.DivisaoRemessas).FirstOrDefaultAsync(c => c.Numero.Equals(Numero));
        }

        public Task<DivisaoRemessa> GetAsyncDivisaoRemessa(Expression<Func<DivisaoRemessa, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BuscaOrdemVenda>> ConsultaOrdem()
        {
            try
            {
                IEnumerable<BuscaOrdemVenda> list = await _context.Database.SqlQuery<BuscaOrdemVenda>("EXEC spBuscaOrdemVenda").ToListAsync();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BuscaRemessasCliente>> GetClientDeliveries(Guid accountClientID, string empresaID, string tipoRemessa)
        {
            try
            {
                IEnumerable<BuscaRemessasCliente> list = await _context.Database.SqlQuery<BuscaRemessasCliente>("EXEC spBuscaRemessasCliente @ccid, @empresa, @tipoRemessa",
                    new SqlParameter("ccid", accountClientID),
                    new SqlParameter("empresa", empresaID),
                    new SqlParameter("tipoRemessa", tipoRemessa)
                ).ToListAsync();

                return list;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<LogEnvioOrdensSAP>> GetLogEnvioOrdensSAP(string empresaId)
        {
            try
            {
                ((IObjectContextAdapter)_context).ObjectContext.CommandTimeout = 180; // This query needs to be longer than normal timeout...

                IEnumerable<LogEnvioOrdensSAP> list = await _context.Database.SqlQuery<LogEnvioOrdensSAP>("EXEC spLogEnvioOrdensSAP @EmpresaId",
                    new SqlParameter("EmpresaId", empresaId)
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
