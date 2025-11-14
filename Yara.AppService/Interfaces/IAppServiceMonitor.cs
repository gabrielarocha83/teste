using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceMonitor
    {

        Task<MonitorStatusServicosDto> GetStatusServicos();
        Task<MonitorQuantidadesFilasDto> GetQuantidadesFilas();
        Task<MonitorTotalProcessadoDto> GetTotalProcessado(MonitorDataInputDto input);
        Task<IEnumerable<MonitorInfoGraficoProcessamentoDto>> GetGraficoProcessamento(MonitorDataInputDto input);
        Task<IEnumerable<MonitorMensagemErroDto>> GetMensagensErro(MonitorDataInputDto input);
        Task<IEnumerable<MonitorOVNotificacaoDto>> GetOVNotificacoes(MonitorOVDataInputDto input);
        Task<IEnumerable<MonitorOVResultadoDto>> GetOVResultados(MonitorOVDataInputDto input);
        Task<IEnumerable<MonitorOVMensagemErroDto>> GetOVMensagensErro(MonitorOVDataInputDto input);
        Task<byte[]> GetMensagensErro_Excel(MonitorDataInputDto input);
        Task<byte[]> GetOVDetalhes_Excel(MonitorOVDataInputDto input);

    }
}
