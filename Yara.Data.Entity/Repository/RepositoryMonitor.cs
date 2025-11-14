using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Repository;
using Yara.Domain.Entities;
using System.Data.SqlClient;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryMonitor : IRepositoryMonitor
    {

        private readonly DbContext _context;

        public RepositoryMonitor(DbContext context)
        {
            _context = context;
        }

        public async Task<MonitorQuantidadesFilas> BuscarQuantidadesFilas()
        {

            var ret = await _context.Database.SqlQuery<MonitorQuantidadesFilas>("EXEC spMonitorFilas").ToListAsync();
            return ret.First();

        }

        public async Task<MonitorTotalProcessado> BuscarQuantidadesTotais(DateTime dataInicio, DateTime dataFim)
        {

            var ret = await _context.Database.SqlQuery<MonitorTotalProcessado>("EXEC spMonitorQuantidadesTotais @dataInicio, @dataFim",
                new SqlParameter("dataInicio", dataInicio),
                new SqlParameter("dataFim", dataFim)
                ).ToListAsync();

            return ret.First();

        }

        public async Task<IEnumerable<MonitorInfoGraficoProcessamento>> BuscarDadosGrafico(DateTime dataInicio, DateTime dataFim)
        {

            var ret = await _context.Database.SqlQuery<MonitorInfoGraficoProcessamento>("EXEC spMonitorGrafico @dataInicio, @dataFim",
                new SqlParameter("dataInicio", dataInicio),
                new SqlParameter("dataFim", dataFim)
                ).ToListAsync();

            return ret;

        }

        public async Task<IEnumerable<MonitorMensagemErro>> BuscarMensagensErro(DateTime dataInicio, DateTime dataFim)
        {

            var ret = await _context.Database.SqlQuery<MonitorMensagemErro>("EXEC spMonitorMensagensErro @dataInicio, @dataFim",
                new SqlParameter("dataInicio", dataInicio),
                new SqlParameter("dataFim", dataFim)
                ).ToListAsync();

            return ret;

        }

        public async Task<IEnumerable<MonitorOVNotificacao>> BuscarOVNotificacao(string ordemVenda, DateTime? dataInicio, DateTime? dataFim)
        {

            var parameterDataInicio = new SqlParameter("dataInicio", System.Data.SqlDbType.DateTime);
            var parameterDataFim = new SqlParameter("dataFim", System.Data.SqlDbType.DateTime);

            if (dataInicio.HasValue)
                parameterDataInicio.Value = dataInicio.Value;
            else
                parameterDataInicio.Value = DBNull.Value;

            if (dataFim.HasValue)
                parameterDataFim.Value = dataFim.Value;
            else
                parameterDataFim.Value = DBNull.Value;

            var ret = await _context.Database.SqlQuery<MonitorOVNotificacao>("EXEC spMonitorOVNotificacoes @ordemVenda, @dataInicio, @dataFim",
                new SqlParameter("ordemVenda", ordemVenda),
                parameterDataInicio,
                parameterDataFim
                ).ToListAsync();

            return ret;

        }

        public async Task<IEnumerable<MonitorOVResultado>> BuscarOVResultados(string ordemVenda, DateTime? dataInicio, DateTime? dataFim)
        {

            var parameterDataInicio = new SqlParameter("dataInicio", System.Data.SqlDbType.DateTime);
            var parameterDataFim = new SqlParameter("dataFim", System.Data.SqlDbType.DateTime);

            if (dataInicio.HasValue)
                parameterDataInicio.Value = dataInicio.Value;
            else
                parameterDataInicio.Value = DBNull.Value;

            if (dataFim.HasValue)
                parameterDataFim.Value = dataFim.Value;
            else
                parameterDataFim.Value = DBNull.Value;

            var ret = await _context.Database.SqlQuery<MonitorOVResultado>("EXEC spMonitorOVEnvios @ordemVenda, @dataInicio, @dataFim",
                new SqlParameter("ordemVenda", ordemVenda),
                parameterDataInicio,
                parameterDataFim
                ).ToListAsync();

            return ret;

        }

        public async Task<IEnumerable<MonitorOVMensagemErro>> BuscarOVMensagensErro(string ordemVenda, DateTime? dataInicio, DateTime? dataFim)
        {

            var parameterDataInicio = new SqlParameter("dataInicio", System.Data.SqlDbType.DateTime);
            var parameterDataFim = new SqlParameter("dataFim", System.Data.SqlDbType.DateTime);

            if (dataInicio.HasValue)
                parameterDataInicio.Value = dataInicio.Value;
            else
                parameterDataInicio.Value = DBNull.Value;

            if (dataFim.HasValue)
                parameterDataFim.Value = dataFim.Value;
            else
                parameterDataFim.Value = DBNull.Value;

            var ret = await _context.Database.SqlQuery<MonitorOVMensagemErro>("EXEC spMonitorOVMensagensErro @ordemVenda, @dataInicio, @dataFim",
                new SqlParameter("ordemVenda", ordemVenda),
                parameterDataInicio,
                parameterDataFim
                ).ToListAsync();

            return ret;

        }


    }

}
