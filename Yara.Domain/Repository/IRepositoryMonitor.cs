using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryMonitor
    {

        Task<MonitorQuantidadesFilas> BuscarQuantidadesFilas();
        Task<MonitorTotalProcessado> BuscarQuantidadesTotais(DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<MonitorInfoGraficoProcessamento>> BuscarDadosGrafico(DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<MonitorMensagemErro>> BuscarMensagensErro(DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<MonitorOVNotificacao>> BuscarOVNotificacao(string ordemVenda, DateTime? dataInicio, DateTime? dataFim);
        Task<IEnumerable<MonitorOVResultado>> BuscarOVResultados(string ordemVenda, DateTime? dataInicio, DateTime? dataFim);
        Task<IEnumerable<MonitorOVMensagemErro>> BuscarOVMensagensErro(string ordemVenda, DateTime? dataInicio, DateTime? dataFim);

    }
}
