using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository.Procedures;

namespace Yara.Data.Entity.Repository.Procedures
{
    public class RepositoryCobrancaResultado : RepositoryBase<CobrancaResultado>, IRepositoryCobrancaResultado
    {
        private new readonly DbContext _context;

        public RepositoryCobrancaResultado(DbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CobrancaResultado>> CobrancaGeralPorDiretoria(string empresaId, string diretoriaId = null)
        {
            IEnumerable<CobrancaResultado> list = await _context.Database.
                SqlQuery<CobrancaResultado>("EXEC spCobranca_GeralPorDiretoria @EmpresaId, @DiretoriaId",
                    new SqlParameter("EmpresaId", empresaId),
                    new SqlParameter("DiretoriaId", string.IsNullOrEmpty(diretoriaId) ? DBNull.Value : (object)diretoriaId)
                ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<CobrancaResultado>> CobrancaEfetivaPorDiretoria(string empresaId, string diretoriaId = null)
        {
            IEnumerable<CobrancaResultado> list = await _context.Database.
                SqlQuery<CobrancaResultado>("EXEC spCobranca_EfetivaPorDiretoria @EmpresaId, @DiretoriaId",
                    new SqlParameter("EmpresaId", empresaId),
                    new SqlParameter("DiretoriaId", string.IsNullOrEmpty(diretoriaId) ? DBNull.Value : (object)diretoriaId)
                ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<CobrancaResultado>> CobrancaEfetivaPorStatus(string empresaId, string diretoriaId = null)
        {
            IEnumerable<CobrancaResultado> list = await _context.Database.
                SqlQuery<CobrancaResultado>("EXEC spCobranca_EfetivaPorStatus @EmpresaId, @DiretoriaId",
                    new SqlParameter("EmpresaId", empresaId),
                    new SqlParameter("DiretoriaId", string.IsNullOrEmpty(diretoriaId) ? DBNull.Value : (object)diretoriaId)
                ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<CobrancaResultado>> CobrancaEfetivaPorEstado(string empresaId, string diretoriaId = null)
        {
            IEnumerable<CobrancaResultado> list = await _context.Database.
                SqlQuery<CobrancaResultado>("EXEC spCobranca_EfetivaPorEstado @EmpresaId, @DiretoriaId",
                    new SqlParameter("EmpresaId", empresaId),
                    new SqlParameter("DiretoriaId", string.IsNullOrEmpty(diretoriaId) ? DBNull.Value : (object)diretoriaId)
                ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<CobrancaVencidosResultado>> CobrancaVencidosMenosDias(string empresaId, string diretoriaId = null)
        {
            IEnumerable<CobrancaVencidosResultado> list = await _context.Database.
                SqlQuery<CobrancaVencidosResultado>("EXEC spCobranca_VencidosMenosDias @EmpresaId, @DiretoriaId",
                    new SqlParameter("EmpresaId", empresaId),
                    new SqlParameter("DiretoriaId", string.IsNullOrEmpty(diretoriaId) ? DBNull.Value : (object)diretoriaId)
                ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<CobrancaResultado>> CobrancaMaioresDevedores(string empresaId, string diretoriaId = null)
        {
            IEnumerable<CobrancaResultado> list = await _context.Database.
                SqlQuery<CobrancaResultado>("EXEC spCobranca_MaioresDevedores @EmpresaId, @DiretoriaId",
                    new SqlParameter("EmpresaId", empresaId),
                    new SqlParameter("DiretoriaId", string.IsNullOrEmpty(diretoriaId) ? DBNull.Value : (object)diretoriaId)
                ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<CobrancaResultado>> CobrancaEfetivaPorCultura(string empresaId, string diretoriaId = null)
        {
            IEnumerable<CobrancaResultado> list = await _context.Database.
                SqlQuery<CobrancaResultado>("EXEC spCobranca_EfetivaPorCultura @EmpresaId, @DiretoriaId",
                    new SqlParameter("EmpresaId", empresaId),
                    new SqlParameter("DiretoriaId", string.IsNullOrEmpty(diretoriaId) ? DBNull.Value : (object)diretoriaId)
                ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<CobrancaListaCliente>> CobrancaGeralPorDiretoria_Clientes(string empresaId, string chave, int dias, string diretoriaId = null)
        {
            IEnumerable<CobrancaListaCliente> list = await _context.Database.
                SqlQuery<CobrancaListaCliente>("EXEC spCobranca_GeralPorDiretoria_Clientes @EmpresaId, @Chave, @Dias, @DiretoriaId",
                    new SqlParameter("EmpresaId", empresaId),
                    new SqlParameter("Chave", chave ?? ""),
                    new SqlParameter("Dias", dias),
                    new SqlParameter("DiretoriaId", string.IsNullOrEmpty(diretoriaId) ? DBNull.Value : (object)diretoriaId)
                ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<CobrancaListaCliente>> CobrancaEfetivaPorDiretoria_Clientes(string empresaId, string chave, int dias, string diretoriaId = null)
        {
            IEnumerable<CobrancaListaCliente> list = await _context.Database.
                SqlQuery<CobrancaListaCliente>("EXEC spCobranca_EfetivaPorDiretoria_Clientes @EmpresaId, @Chave, @Dias, @DiretoriaId",
                    new SqlParameter("EmpresaId", empresaId),
                    new SqlParameter("Chave", chave ?? ""),
                    new SqlParameter("Dias", dias),
                    new SqlParameter("DiretoriaId", string.IsNullOrEmpty(diretoriaId) ? DBNull.Value : (object)diretoriaId)
                ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<CobrancaListaCliente>> CobrancaEfetivaPorStatus_Clientes(string empresaId, string chave, int dias, string diretoriaId = null)
        {
            IEnumerable<CobrancaListaCliente> list = await _context.Database.
                SqlQuery<CobrancaListaCliente>("EXEC spCobranca_EfetivaPorStatus_Clientes @EmpresaId, @Chave, @Dias, @DiretoriaId",
                    new SqlParameter("EmpresaId", empresaId),
                    new SqlParameter("Chave", chave ?? ""),
                    new SqlParameter("Dias", dias),
                    new SqlParameter("DiretoriaId", string.IsNullOrEmpty(diretoriaId) ? DBNull.Value : (object)diretoriaId)
                ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<CobrancaListaCliente>> CobrancaEfetivaPorEstado_Clientes(string empresaId, string chave, int dias, string diretoriaId = null)
        {
            IEnumerable<CobrancaListaCliente> list = await _context.Database.
                SqlQuery<CobrancaListaCliente>("EXEC spCobranca_EfetivaPorEstado_Clientes @EmpresaId, @Chave, @Dias, @DiretoriaId",
                    new SqlParameter("EmpresaId", empresaId),
                    new SqlParameter("Chave", chave ?? ""),
                    new SqlParameter("Dias", dias),
                    new SqlParameter("DiretoriaId", string.IsNullOrEmpty(diretoriaId) ? DBNull.Value : (object)diretoriaId)
                ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<CobrancaListaCliente>> CobrancaEfetivaPorCultura_Clientes(string empresaId, string chave, int dias, string diretoriaId = null)
        {
            IEnumerable<CobrancaListaCliente> list = await _context.Database.
                SqlQuery<CobrancaListaCliente>("EXEC spCobranca_EfetivaPorCultura_Clientes @EmpresaId, @Chave, @Dias, @DiretoriaId",
                    new SqlParameter("EmpresaId", empresaId),
                    new SqlParameter("Chave", chave ?? ""),
                    new SqlParameter("Dias", dias),
                    new SqlParameter("DiretoriaId", string.IsNullOrEmpty(diretoriaId) ? DBNull.Value : (object)diretoriaId)
                ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<CobrancaListaCliente>> Juridico_Clientes(string empresaId, int mes, int ano)
        {
            IEnumerable<CobrancaListaCliente> list = await _context.Database.
                SqlQuery<CobrancaListaCliente>("EXEC spCobranca_Juridico_Clientes @EmpresaId, @Mes, @Ano",
                    new SqlParameter("EmpresaId", empresaId),
                    new SqlParameter("Mes", mes == 0? DBNull.Value : (object)mes),
                    new SqlParameter("Ano", ano)
                ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<TituloContaCliente>> BuscaTitulosContaCliente(Guid contaClienteId, string empresaId)
        {
            IEnumerable<TituloContaCliente> list = await _context.Database.
                SqlQuery<TituloContaCliente>("EXEC spBuscaTitulosContaCliente @ContaClienteId, @EmpresaId",
                    new SqlParameter("ContaClienteId", contaClienteId),
                    new SqlParameter("EmpresaId", empresaId)
                ).ToListAsync();

            return list;
        }

    }
}
