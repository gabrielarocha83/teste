using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryRelatorios : IRepositoryRelatorios
    {
        private readonly DbContext _context;

        public RepositoryRelatorios(DbContext context)
        {
            _context = context;
        }

        public  async Task<IEnumerable<BuscaPropostas>> GetConsultaProposta(BuscaPropostasSearch filter)
        {
            try
            {
                ((IObjectContextAdapter)_context).ObjectContext.CommandTimeout = 180; // This query needs to be longer than normal timeout...

                IEnumerable<BuscaPropostas> list = await _context.Database.SqlQuery<BuscaPropostas>("EXEC spConsultaPropostas " +
                    "@TipoProposta," +
                    "@CodigoProposta," +
                    "@Cliente," +
                    "@Status," +
                    "@ValorProposta," +
                    "@Vigencia," +
                    "@VigenciaFim," +
                    "@Rating," +
                    "@TipoCliente," +
                    "@Segmento," +
                    "@Diretoria," +
                    "@GC," +
                    "@CTC," +
                    "@DataCriacao," +
                    "@DataCriacaoFim," +
                    "@DataConclusao," +
                    "@DataConclusaoFim," +
                    "@DataEntregue," +
                    "@DataEntregueFim," +
                    "@Analista," +
                    "@Documento," +
                    "@EmpresaID," +
                    "@LeadTime," +
                    "@LeadTimeFim",
                    new SqlParameter("TipoProposta", (string.IsNullOrEmpty(filter.TipoProposta) ? Convert.DBNull : filter.TipoProposta)),
                    new SqlParameter("CodigoProposta", (string.IsNullOrEmpty(filter.CodigoProposta) ? Convert.DBNull : filter.CodigoProposta)),
                    new SqlParameter("Cliente", (string.IsNullOrEmpty(filter.NomeCliente) ? Convert.DBNull : filter.NomeCliente)),
                    new SqlParameter("Status", (string.IsNullOrEmpty(filter.Status) ? Convert.DBNull : filter.Status)),
                    new SqlParameter("ValorProposta", (filter.ValorProposta.HasValue ? filter.ValorProposta.Value : Convert.DBNull)),
                    new SqlParameter("Vigencia", (filter.Vigencia.HasValue ? filter.Vigencia.Value : Convert.DBNull)),
                    new SqlParameter("VigenciaFim", (filter.VigenciaFim.HasValue ? filter.VigenciaFim.Value : Convert.DBNull)),
                    new SqlParameter("Rating", (string.IsNullOrEmpty(filter.Rating) ? Convert.DBNull : filter.Rating)),
                    new SqlParameter("TipoCliente", (string.IsNullOrEmpty(filter.TipoCliente) ? Convert.DBNull : filter.TipoCliente)),
                    new SqlParameter("Segmento", (string.IsNullOrEmpty(filter.Segmento) ? Convert.DBNull : filter.Segmento)),
                    new SqlParameter("Diretoria", (string.IsNullOrEmpty(filter.Diretoria) ? Convert.DBNull : filter.Diretoria)),
                    new SqlParameter("GC", (string.IsNullOrEmpty(filter.GC) ? Convert.DBNull : filter.GC)),
                    new SqlParameter("CTC", (string.IsNullOrEmpty(filter.CTC) ? Convert.DBNull : filter.CTC)),
                    new SqlParameter("DataCriacao", (filter.DataCriacao.HasValue ? filter.DataCriacao.Value : Convert.DBNull)),
                    new SqlParameter("DataCriacaoFim", (filter.DataCriacaoFim.HasValue ? filter.DataCriacaoFim.Value : Convert.DBNull)),
                    new SqlParameter("DataConclusao", (filter.DataConclusao.HasValue ? filter.DataConclusao.Value : Convert.DBNull)),
                    new SqlParameter("DataConclusaoFim", (filter.DataConclusaoFim.HasValue ? filter.DataConclusaoFim.Value : Convert.DBNull)),
                    new SqlParameter("DataEntregue", (filter.DataEntregue.HasValue ? filter.DataEntregue.Value : Convert.DBNull)),
                    new SqlParameter("DataEntregueFim", (filter.DataEntregueFim.HasValue ? filter.DataEntregueFim.Value : Convert.DBNull)),
                    new SqlParameter("Analista", (string.IsNullOrEmpty(filter.Analista) ? Convert.DBNull : filter.Analista)),
                    new SqlParameter("Documento", (string.IsNullOrEmpty(filter.Documento) ? Convert.DBNull : filter.Documento)),
                    new SqlParameter("EmpresaID", filter.EmpresaID),
                    new SqlParameter("LeadTime", filter.LeadTime.HasValue ? filter.LeadTime.Value : Convert.DBNull),
                    new SqlParameter("LeadTimeFim", filter.LeadTimeFim.HasValue ? filter.LeadTimeFim.Value : Convert.DBNull)
                    ).ToListAsync();

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<string>> GetStatus()
        {
            try
            {
                IEnumerable<string> list = await _context.Database.SqlQuery<string>("SELECT Nome FROM PropostaLCStatus WHERE Ativo = 1 UNION SELECT Status FROM PropostaCobrancaStatus").ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
