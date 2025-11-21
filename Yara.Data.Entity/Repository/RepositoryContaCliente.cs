using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryContaCliente : RepositoryBase<ContaCliente>, IRepositoryContaCliente
    {
        public RepositoryContaCliente(DbContext context) : base(context)
        {
        }

        public async Task<ContaCliente> GetOneByIDAsync(Guid id)
        {
            return await _context.Set<ContaCliente>()
                .Include("ContaClienteEstruturaComercial.EstruturaComercial")
                .Include("ContaClienteEstruturaComercial.EstruturaComercial.Superior")
                .Include("ContaClienteEstruturaComercial.EstruturaComercial.Superior.Superior")
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ID.Equals(id));
        }

        public async Task<IEnumerable<BuscaContaCliente>> BuscaContaCliente(BuscaContaCliente contaCliente, Guid usuarioId)
        {
            try
            {
                IEnumerable<BuscaContaCliente> list = await _context.Database.SqlQuery<BuscaContaCliente>("EXEC spBuscaContaCliente @pNome, @pDocumento, @pCodigo, @pApelido, @pGrupo, @pEmpresaId, @pNumeroOrdem, @pUsuarioId",
                    new SqlParameter("pNome", string.IsNullOrEmpty(contaCliente.Nome) ? DBNull.Value : (object)contaCliente.Nome),
                    new SqlParameter("pDocumento", string.IsNullOrEmpty(contaCliente.Documento) ? DBNull.Value : (object)contaCliente.Documento),
                    new SqlParameter("pCodigo", string.IsNullOrEmpty(contaCliente.CodigoPrincipal) ? DBNull.Value : (object)contaCliente.CodigoPrincipal),
                    new SqlParameter("pApelido", string.IsNullOrEmpty(contaCliente.Apelido) ? DBNull.Value : (object)contaCliente.Apelido),
                    new SqlParameter("pGrupo", string.IsNullOrEmpty(contaCliente.GrupoEconomico) ? DBNull.Value : (object)contaCliente.GrupoEconomico),
                    new SqlParameter("pEmpresaId", string.IsNullOrEmpty(contaCliente.EmpresaId) ? DBNull.Value : (object)contaCliente.EmpresaId),
                    new SqlParameter("pNumeroOrdem", string.IsNullOrEmpty(contaCliente.NumeroOrdem) ? DBNull.Value : (object)contaCliente.NumeroOrdem),
                    new SqlParameter("pUsuarioId", usuarioId)
                ).ToListAsync();

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> ValidaAcessoContaCliente(Guid contaClienteId, Guid usuarioId)
        {
            bool check = await _context.Database.SqlQuery<bool>("EXEC spVerificaAcessoCliente @pContaClienteId, @pUsuarioId",
                    new SqlParameter("pContaClienteId", contaClienteId),
                    new SqlParameter("pUsuarioId", usuarioId)
                ).FirstOrDefaultAsync();

            return check;
        }

        public async Task<IEnumerable<BuscaContaClienteEstComl>> BuscaContaClienteEstComl(BuscaContaClienteEstComl contaCliente)
        {
            IEnumerable<BuscaContaClienteEstComl> list = await _context.Database.SqlQuery<BuscaContaClienteEstComl>("EXEC spBuscaContaClienteEstComl @pNome, @pDocumento, @pCodigo, @pApelido, @pRepresentante, @pCTC, @pGC, @pDC, @pEmpresaId, @pAtivo",
                    new SqlParameter("pNome", string.IsNullOrEmpty(contaCliente.Nome) ? DBNull.Value : (object)contaCliente.Nome),
                    new SqlParameter("pDocumento", string.IsNullOrEmpty(contaCliente.Documento) ? DBNull.Value : (object)contaCliente.Documento),
                    new SqlParameter("pCodigo", string.IsNullOrEmpty(contaCliente.CodigoPrincipal) ? DBNull.Value : (object)contaCliente.CodigoPrincipal),
                    new SqlParameter("pApelido", string.IsNullOrEmpty(contaCliente.Apelido) ? DBNull.Value : (object)contaCliente.Apelido),
                    new SqlParameter("pRepresentante", string.IsNullOrEmpty(contaCliente.Representante) ? DBNull.Value : (object)contaCliente.Representante),
                    new SqlParameter("pCTC", string.IsNullOrEmpty(contaCliente.CTC) ? DBNull.Value : (object)contaCliente.CTC),
                    new SqlParameter("pGC", string.IsNullOrEmpty(contaCliente.GC) ? DBNull.Value : (object)contaCliente.GC),
                    new SqlParameter("pDC", string.IsNullOrEmpty(contaCliente.DC) ? DBNull.Value : (object)contaCliente.DC),
                    new SqlParameter("pEmpresaId", string.IsNullOrEmpty(contaCliente.EmpresaId) ? DBNull.Value : (object)contaCliente.EmpresaId),
                    new SqlParameter("pAtivo", !contaCliente.Ativo.HasValue ? DBNull.Value : (object)contaCliente.Ativo)
                ).ToListAsync();

            return list;
        }

        public BuscaOrdemVendaSumarizado OrdemVendaSumarizado(Guid ContaClienteID, string Empresa)
        {
            var buscaOrdemCliente = new BuscaOrdemVendaSumarizado();


            var command = _context.Database.Connection.CreateCommand();
            command.CommandText = "[dbo].[spBuscaOrdensVendaPorClienteSumarizado]";
            command.CommandType = CommandType.StoredProcedure;


            var param1 = new SqlParameter { ParameterName = "@pClienteId", Value = ContaClienteID };
            var param2 = new SqlParameter { ParameterName = "@pOrgVendas", Value = Empresa };
            command.Parameters.Add(param1);
            command.Parameters.Add(param2);

            buscaOrdemCliente.ClienteId = ContaClienteID;
            buscaOrdemCliente.EmpresaId = Empresa;
            try
            {
                _context.Database.Connection.Open();
                var reader = command.ExecuteReader();

                var listOfVendaPrazoLiberadas = ((IObjectContextAdapter)_context).ObjectContext.Translate<OrdemVendaSumarizado>(reader).ToList();
                buscaOrdemCliente.BuscaOrdemVendaPrazosLiberadas = listOfVendaPrazoLiberadas.FirstOrDefault();

                reader.NextResult();
                var listOfVendaPrazoBloqueadas = ((IObjectContextAdapter)_context).ObjectContext.Translate<OrdemVendaSumarizado>(reader).ToList();
                buscaOrdemCliente.BuscaOrdemVendaPrazosBloqueadas = listOfVendaPrazoBloqueadas.FirstOrDefault();

                reader.NextResult();
                var listOfVendaAVista = ((IObjectContextAdapter)_context).ObjectContext.Translate<OrdemVendaSumarizado>(reader).ToList();
                buscaOrdemCliente.BuscaOrdemVendaAVista = listOfVendaAVista.FirstOrDefault();

                reader.NextResult();
                var listOfVendaPagaRetira = ((IObjectContextAdapter)_context).ObjectContext.Translate<OrdemVendaSumarizado>(reader).ToList();
                buscaOrdemCliente.BuscaOrdemPagaRetira = listOfVendaPagaRetira.FirstOrDefault();

            }
            catch (Exception e)
            {
                throw e;
            }

            return buscaOrdemCliente;
        }

        public async Task<IEnumerable<BuscaOrdemVendasPrazo>> OrdemVendaPorClientePrazo(BuscaOrdemVendasPrazo vendasPrazo)
        {
            IEnumerable<BuscaOrdemVendasPrazo> list = await _context.Database.SqlQuery<BuscaOrdemVendasPrazo>("EXEC spBuscaOrdensVendaPorClientePrazo @pClienteId, @pOrgVendas, @pOrdemVendaNumero, @pPagador, @pMaterial, @pCentro",
                new SqlParameter("pClienteId", string.IsNullOrEmpty(vendasPrazo.ClienteId.ToString()) ? DBNull.Value : (object)vendasPrazo.ClienteId),
                new SqlParameter("pOrgVendas", string.IsNullOrEmpty(vendasPrazo.EmpresaId) ? DBNull.Value : (object)vendasPrazo.EmpresaId),
                new SqlParameter("pOrdemVendaNumero", string.IsNullOrEmpty(vendasPrazo.OrdemVendaNumero) ? DBNull.Value : (object)vendasPrazo.OrdemVendaNumero),
                new SqlParameter("pPagador", string.IsNullOrEmpty(vendasPrazo.Pagador) ? DBNull.Value : (object)vendasPrazo.Pagador),
                new SqlParameter("pMaterial", string.IsNullOrEmpty(vendasPrazo.Material) ? DBNull.Value : (object)vendasPrazo.Material),
                new SqlParameter("pCentro", string.IsNullOrEmpty(vendasPrazo.Centro) ? DBNull.Value : (object)vendasPrazo.Centro)
            ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<BuscaOrdemVendasAVista>> OrdemVendaPorClienteVista(BuscaOrdemVendasAVista vendasAVista)
        {
            IEnumerable<BuscaOrdemVendasAVista> list = await _context.Database.SqlQuery<BuscaOrdemVendasAVista>("EXEC spBuscaOrdensVendaPorClienteAVista @pClienteId, @pOrgVendas, @pOrdemVendaNumero, @pPagador, @pMaterial, @pCentro",
                new SqlParameter("pClienteId", string.IsNullOrEmpty(vendasAVista.ClienteId.ToString()) ? DBNull.Value : (object)vendasAVista.ClienteId),
                new SqlParameter("pOrgVendas", string.IsNullOrEmpty(vendasAVista.EmpresaId) ? DBNull.Value : (object)vendasAVista.EmpresaId),
                new SqlParameter("pOrdemVendaNumero", string.IsNullOrEmpty(vendasAVista.OrdemVendaNumero) ? DBNull.Value : (object)vendasAVista.OrdemVendaNumero),
                new SqlParameter("pPagador", string.IsNullOrEmpty(vendasAVista.Pagador) ? DBNull.Value : (object)vendasAVista.Pagador),
                new SqlParameter("pMaterial", string.IsNullOrEmpty(vendasAVista.Material) ? DBNull.Value : (object)vendasAVista.Material),
                new SqlParameter("pCentro", string.IsNullOrEmpty(vendasAVista.Centro) ? DBNull.Value : (object)vendasAVista.Centro)
            ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<BuscaOrdemVendasPagaRetira>> OrdemVendaPorClienteRetira(BuscaOrdemVendasPagaRetira vendasPagaRetira)
        {
            IEnumerable<BuscaOrdemVendasPagaRetira> list = await _context.Database.SqlQuery<BuscaOrdemVendasPagaRetira>("EXEC spBuscaOrdensVendaPorRetira @pClienteId, @pOrgVendas, @pOrdemVendaNumero, @pPagador, @pMaterial, @pCentro",
                new SqlParameter("pClienteId", string.IsNullOrEmpty(vendasPagaRetira.ClienteId.ToString()) ? DBNull.Value : (object)vendasPagaRetira.ClienteId),
                new SqlParameter("pOrgVendas", string.IsNullOrEmpty(vendasPagaRetira.EmpresaId) ? DBNull.Value : (object)vendasPagaRetira.EmpresaId),
                new SqlParameter("pOrdemVendaNumero", string.IsNullOrEmpty(vendasPagaRetira.OrdemVendaNumero) ? DBNull.Value : (object)vendasPagaRetira.OrdemVendaNumero),
                new SqlParameter("pPagador", string.IsNullOrEmpty(vendasPagaRetira.Pagador) ? DBNull.Value : (object)vendasPagaRetira.Pagador),
                new SqlParameter("pMaterial", string.IsNullOrEmpty(vendasPagaRetira.Material) ? DBNull.Value : (object)vendasPagaRetira.Material),
                new SqlParameter("pCentro", string.IsNullOrEmpty(vendasPagaRetira.Centro) ? DBNull.Value : (object)vendasPagaRetira.Centro)
            ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<TitulosGrupoEconomicoMembros>> TitulosGrupoEconomicoMembroContaCliente(Guid contaClienteId, string empresa)
        {
            try
            {
                IEnumerable<TitulosGrupoEconomicoMembros> list = await _context.Database.SqlQuery<TitulosGrupoEconomicoMembros>("EXEC spTitulosGrupoEconomicoMembros @contaClienteId, @EmpresaID",
                    new SqlParameter("contaClienteId", contaClienteId),
                    new SqlParameter("EmpresaID", empresa)
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