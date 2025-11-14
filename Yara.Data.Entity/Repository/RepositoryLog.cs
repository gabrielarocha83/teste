using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    internal class RepositoryLog : RepositoryBase<Log>, IRepositoryLog
    {
        public RepositoryLog(DbContext context) : base(context) { }

        public async Task<IEnumerable<LogRetorno>> BuscaLog(BuscaLogs log)
        {
            IEnumerable<LogRetorno> list = await _context.Database.SqlQuery<LogRetorno>("EXEC spBuscaLogs @user,@idtransacao, @begindate, @enddate, @logLevel",
                    new SqlParameter("user", string.IsNullOrEmpty(log.Usuario) ? DBNull.Value : (object)log.Usuario),
                    new SqlParameter("idtransacao", log.IDTransacao.HasValue ? (object)log.IDTransacao : DBNull.Value),
                    new SqlParameter("logLevel", log.LogLevel == 0 ? DBNull.Value : (object)log.LogLevel),
                    new SqlParameter("begindate", log.DataInicio.HasValue ? (object)log.DataInicio : DBNull.Value),
                    new SqlParameter("enddate", log.DataFim.HasValue ? (object)log.DataFim : DBNull.Value)
                ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<LogwithUser>> BuscaLogGrupoEconomico(Guid ContaClienteID, string empresaId)
        {
            IEnumerable<LogwithUser> list = await _context.Database.SqlQuery<LogwithUser>("EXEC spBuscaLogGrupoEconomico @ContaClienteID, @EmpresaID",
                new SqlParameter("ContaClienteID", ContaClienteID),
                new SqlParameter("EmpresaID", empresaId)
            ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<LogwithUser>> BuscaLogProposta(Guid propostaId)
        {
            IEnumerable<LogwithUser> list = await _context.Database.SqlQuery<LogwithUser>("EXEC spBuscaLogProposta @propostaId",
                new SqlParameter("propostaId", propostaId)
            ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<BuscaLogFluxoAutomatico>> BuscaLogFluxoAutomatico(BuscaLogFluxoAutomatico log)
        {
            ((IObjectContextAdapter)_context).ObjectContext.CommandTimeout = 180; // This query needs to be longer than normal timeout...

            IEnumerable<BuscaLogFluxoAutomatico> list = await _context.Database.SqlQuery<BuscaLogFluxoAutomatico>("EXEC spBuscaLogFluxoAutomatico @pNomeCliente, @pIdCliente, @pDivisao, @pItem, @pNumero, @pMotivo, @pDataInicial, @pDataFinal",
                new SqlParameter("pNomeCliente", string.IsNullOrEmpty(log.NomeCliente) ? DBNull.Value : (object)log.NomeCliente),
                new SqlParameter("pIdCliente", log.ClienteId.HasValue ? (object)log.ClienteId : DBNull.Value),
                new SqlParameter("pDivisao", log.OrdemDivisao == 0 ? DBNull.Value : (object)log.OrdemDivisao),
                new SqlParameter("pItem", log.OrdemVendaItem == 0 ? DBNull.Value : (object)log.OrdemVendaItem),
                new SqlParameter("pNumero", string.IsNullOrEmpty(log.OrdemVendaNumero) ? DBNull.Value : (object)log.OrdemVendaNumero),
                new SqlParameter("pMotivo", string.IsNullOrEmpty(log.Motivo) ? DBNull.Value : (object)log.Motivo),
                new SqlParameter("pDataInicial", log.DataInicial.HasValue ? (object)log.DataInicial : DBNull.Value),
                new SqlParameter("pDataFinal", log.DataFinal.HasValue ? (object)log.DataFinal : DBNull.Value)
            ).ToListAsync();

            return list;
        }
    }
}
