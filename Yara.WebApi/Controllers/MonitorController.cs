using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("monitor")]
    public class MonitorController : ApiController
    {

        private readonly IAppServiceMonitor _monitor;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor do Serviço de Monitoramento
        /// </summary>
        /// <param name="monitor">Dependência: Instância do Serviço de Monitoramento</param>
        /// <param name="log">Dependência: Instância do Serviço de Log</param>
        public MonitorController(IAppServiceMonitor monitor, IAppServiceLog log)
        {
            _log = log;
            _monitor = monitor;
        }

        /// <summary>
        /// Metodo para Buscar Status dos Serviços para Exibição no Monitor
        /// </summary>
        /// <returns>Status dos Serviços e Comunicações do Portal</returns>
        [HttpGet]
        [Route("v1/statusservicos")]
        public async Task<GenericResult<MonitorStatusServicosDto>> GetStatusServicos()
        {

            var result = new GenericResult<MonitorStatusServicosDto>();

            try
            {

                result.Result = await _monitor.GetStatusServicos();
                result.Success = true;

            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { ex.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(ex.Message);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;

        }

        /// <summary>
        /// Metodo para Buscar Quantidades de Itens em FIla para Exibição no Monitor
        /// </summary>
        /// <returns>Quantidades de Itens Nas Filas do Portal</returns>
        [HttpGet]
        [Route("v1/quantidadesfilas")]
        public async Task<GenericResult<MonitorQuantidadesFilasDto>> GetQuantidadesFilas()
        {

            var result = new GenericResult<MonitorQuantidadesFilasDto>();

            try
            {

                result.Result = await _monitor.GetQuantidadesFilas();
                result.Success = true;

            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { ex.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(ex.Message);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;

        }

        /// <summary>
        /// Metodo para Buscar Totais Processados por Período para Exibição no Monitor
        /// </summary>
        /// <param name="input">Datas de Início e Fim para Filtro</param>
        /// <returns>Quantidades Totais de Carteiras e Divisões de Remessa Processadas no Portal, no Período Informado</returns>
        [HttpPost]
        [Route("v1/totalprocessado")]
        public async Task<GenericResult<MonitorTotalProcessadoDto>> GetTotalProcessado(MonitorDataInputDto input)
        {

            var result = new GenericResult<MonitorTotalProcessadoDto>();

            try
            {

                result.Result = await _monitor.GetTotalProcessado(input);
                result.Success = true;

            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { ex.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(ex.Message);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;

        }

        /// <summary>
        /// Metodo para Buscar Dados de Gráfico por Período para Exibição no Monitor
        /// </summary>
        /// <param name="input">Datas de Início e Fim para Filtro</param>
        /// <returns>Quantidades de Divisões de Remessa e Tempo Médio Processados no Portal, no Período Informado</returns>
        [HttpPost]
        [Route("v1/grafico")]
        public async Task<GenericResult<IEnumerable<MonitorInfoGraficoProcessamentoDto>>> GetGraficoProcessamento(MonitorDataInputDto input)
        {

            var result = new GenericResult<IEnumerable<MonitorInfoGraficoProcessamentoDto>>();

            try
            {

                result.Result = await _monitor.GetGraficoProcessamento(input);
                result.Count = result.Result.Count();
                result.Success = true;

            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { ex.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(ex.Message);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;

        }

        /// <summary>
        /// Metodo para Buscar Mensagens de Erro por Período para Exibição no Monitor
        /// </summary>
        /// <param name="input">Datas de Início e Fim para Filtro</param>
        /// <returns>Mensagens de Erro de Replicação de Carteira no Período Informado</returns>
        [HttpPost]
        [Route("v1/mensagenserro")]
        public async Task<GenericResult<IEnumerable<MonitorMensagemErroDto>>> GetMensagensErro(MonitorDataInputDto input)
        {

            var result = new GenericResult<IEnumerable<MonitorMensagemErroDto>>();

            try
            {

                result.Result = await _monitor.GetMensagensErro(input);
                result.Count = result.Result.Count();
                result.Success = true;

            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { ex.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(ex.Message);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;

        }

        /// <summary>
        /// Metodo para Buscar Notificações de OV para Exibição no Monitor
        /// </summary>
        /// <param name="input">Número de OV e Datas de Início e Fim para Filtro</param>
        /// <returns>Notificações de OV recebidas</returns>
        [HttpPost]
        [Route("v1/ovnotificacoes")]
        public async Task<GenericResult<IEnumerable<MonitorOVNotificacaoDto>>> GetOVNotificacoes(MonitorOVDataInputDto input)
        {

            var result = new GenericResult<IEnumerable<MonitorOVNotificacaoDto>>();

            try
            {

                result.Result = await _monitor.GetOVNotificacoes(input);
                result.Count = result.Result.Count();
                result.Success = true;

            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { ex.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(ex.Message);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;

        }

        /// <summary>
        /// Metodo para Buscar Envios de OV para Exibição no Monitor
        /// </summary>
        /// <param name="input">Número de OV e Datas de Início e Fim para Filtro</param>
        /// <returns>Envios de OV para o SAP</returns>
        [HttpPost]
        [Route("v1/ovenvios")]
        public async Task<GenericResult<IEnumerable<MonitorOVResultadoDto>>> GetOVResultados(MonitorOVDataInputDto input)
        {

            var result = new GenericResult<IEnumerable<MonitorOVResultadoDto>>();

            try
            {

                result.Result = await _monitor.GetOVResultados(input);
                result.Count = result.Result.Count();
                result.Success = true;

            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { ex.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(ex.Message);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;

        }

        /// <summary>
        /// Metodo para Buscar Mensagens de Erro por OV para Exibição no Monitor
        /// </summary>
        /// <param name="input">Número de OV e Datas de Início e Fim para Filtro</param>
        /// <returns>Mensagens de Erro de OV para o SAP</returns>
        [HttpPost]
        [Route("v1/ovmensagens")]
        public async Task<GenericResult<IEnumerable<MonitorOVMensagemErroDto>>> GetOVMensagensErro(MonitorOVDataInputDto input)
        {

            var result = new GenericResult<IEnumerable<MonitorOVMensagemErroDto>>();

            try
            {

                result.Result = await _monitor.GetOVMensagensErro(input);
                result.Count = result.Result.Count();
                result.Success = true;

            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { ex.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(ex.Message);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;

        }

        /// <summary>
        /// Método exportar Excel da Lista de Mensagens
        /// </summary>
        /// <param name="input">Datas de Início e Fim para Filtro</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/mensagensexcel")]
        public async Task<HttpResponseMessage> PostMensagensExcel(MonitorDataInputDto input)
        {
            HttpResponseMessage result = null;

            try
            {

                byte[] arquivo = await _monitor.GetMensagensErro_Excel(input);

                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(arquivo);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format("Monitor_Mensagens_{0:yyyyMMddHHmm}.xlsx", DateTime.Now)
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            catch (Exception e)
            {
                var error = new ErrorsYara();
                error.ErrorYara(e);
                result = Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return result;
        }

        /// <summary>
        /// Método exportar Excel do Detalhes da OV
        /// </summary>
        /// <param name="input">Número da OV e Datas de Início e Fim para Filtro</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/ovexcel")]
        public async Task<HttpResponseMessage> PostOVExcel(MonitorOVDataInputDto input)
        {
            HttpResponseMessage result = null;

            try
            {

                byte[] arquivo = await _monitor.GetOVDetalhes_Excel(input);

                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(arquivo);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format("Monitor_OV_{0:yyyyMMddHHmm}.xlsx", DateTime.Now)
                };

                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            }
            catch (Exception e)
            {
                var error = new ErrorsYara();
                error.ErrorYara(e);
                result = Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return result;
        }

    }
}
