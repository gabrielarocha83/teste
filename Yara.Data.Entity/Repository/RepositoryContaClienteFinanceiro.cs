using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryContaClienteFinanceiro : RepositoryBase<ContaClienteFinanceiro>, IRepositoryContaClienteFinanceiro
    {
        public RepositoryContaClienteFinanceiro(DbContext context) : base(context){}

        public void UpdateConceito(ContaClienteFinanceiro obj)
        {
            var local = _context.Set<ContaClienteFinanceiro>().Local.FirstOrDefault(f => f.ContaClienteID.Equals(obj.ContaClienteID));
            _context.Entry(local).State = EntityState.Detached;
            _context.Entry(obj).State = EntityState.Modified;
        }

        public async Task<LimiteCreditoCliente> GetLimiteCreditoContaCliente(string codigoprincipal)
        {
            try
            {
                var list = await _context.Database.SqlQuery<LimiteCreditoCliente>("EXEC spBuscaLimiteLC @Numero",
                    new SqlParameter("Numero", codigoprincipal)
                    ).FirstAsync();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<DadosFinanceiroTitulo> GetDadosFinanceiroSomatoriaTitulos(Guid contaClienteId, string empresaId)
        {
            try
            {
                var objTitulo = await _context.Database.SqlQuery<DadosFinanceiroTitulo>("EXEC spBuscaSomatoriaTitulosFinanceiros @ContaClienteId, @EmpresaId",
                    new SqlParameter("@ContaClienteId", contaClienteId),
                    new SqlParameter("@EmpresaId", empresaId)
                    ).FirstAsync();
                return objTitulo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
