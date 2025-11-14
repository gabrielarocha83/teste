using AutoMapper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.Domain.Repository;


namespace Yara.AppService
{
    public class AppServiceMonitor : IAppServiceMonitor
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceMonitor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MonitorStatusServicosDto> GetStatusServicos()
        {
            var ret = new MonitorStatusServicosDto { ConexaoPI = false, ConexaoSAP = false, ServicoPortal = false };

            RFCSap.SapCommClient client = new RFCSap.SapCommClient();
            client.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 3);

            // Testar Serviço Portal e RFC
            try
            {
                var test = await client.QuantidadeOVPendenteAsync();

                ret.ServicoPortal = true;

                if (test >= 0)
                    ret.ConexaoSAP = true;
            }
            catch (Exception) { /* Propositalmente vazio */ }

            // Testar PI
            try
            {
                var test = await client.TesteConexaoPIAsync();

                if (test)
                    ret.ConexaoPI = true;
            }
            catch (Exception) { /* Propositalmente vazio */ }

            return ret;
        }

        public async Task<MonitorQuantidadesFilasDto> GetQuantidadesFilas()
        {
            var ret = await _unitOfWork.MonitorRepository.BuscarQuantidadesFilas();

            try
            {
                RFCSap.SapCommClient client = new RFCSap.SapCommClient();
                client.InnerChannel.OperationTimeout = new TimeSpan(0, 0, 3);

                var qtdSAP = await client.QuantidadeOVPendenteAsync();
                ret.OrdensAguardandoEnvioPortal = qtdSAP;
            }
            catch (Exception)
            {
                /* Propositalmente vazio */
                ret.OrdensAguardandoEnvioPortal = -1;
            }

            return Mapper.Map<MonitorQuantidadesFilasDto>(ret); ;
        }

        public async Task<MonitorTotalProcessadoDto> GetTotalProcessado(MonitorDataInputDto input)
        {
            this.ValidarInputPeriodo(ref input);

            var ret = await _unitOfWork.MonitorRepository.BuscarQuantidadesTotais(input.DataInicio, input.DataFim.Value);
            return Mapper.Map<MonitorTotalProcessadoDto>(ret); ;
        }

        public async Task<IEnumerable<MonitorInfoGraficoProcessamentoDto>> GetGraficoProcessamento(MonitorDataInputDto input)
        {
            this.ValidarInputPeriodo(ref input);

            var ret = await _unitOfWork.MonitorRepository.BuscarDadosGrafico(input.DataInicio, input.DataFim.Value);
            return Mapper.Map<IEnumerable<MonitorInfoGraficoProcessamentoDto>>(ret);
        }

        public async Task<IEnumerable<MonitorMensagemErroDto>> GetMensagensErro(MonitorDataInputDto input)
        {
            this.ValidarInputPeriodo(ref input);

            var ret = await _unitOfWork.MonitorRepository.BuscarMensagensErro(input.DataInicio, input.DataFim.Value);
            return Mapper.Map<IEnumerable<MonitorMensagemErroDto>>(ret);
        }

        public async Task<IEnumerable<MonitorOVNotificacaoDto>> GetOVNotificacoes(MonitorOVDataInputDto input)
        {
            var ret = await _unitOfWork.MonitorRepository.BuscarOVNotificacao(input.OrdemVenda, input.DataInicio, input.DataFim);
            return Mapper.Map<IEnumerable<MonitorOVNotificacaoDto>>(ret);
        }

        public async Task<IEnumerable<MonitorOVResultadoDto>> GetOVResultados(MonitorOVDataInputDto input)
        {
            var ret = await _unitOfWork.MonitorRepository.BuscarOVResultados(input.OrdemVenda, input.DataInicio, input.DataFim);
            return Mapper.Map<IEnumerable<MonitorOVResultadoDto>>(ret);
        }

        public async Task<IEnumerable<MonitorOVMensagemErroDto>> GetOVMensagensErro(MonitorOVDataInputDto input)
        {
            var ret = await _unitOfWork.MonitorRepository.BuscarOVMensagensErro(input.OrdemVenda, input.DataInicio, input.DataFim);
            return Mapper.Map<IEnumerable<MonitorOVMensagemErroDto>>(ret);
        }

        public async Task<byte[]> GetMensagensErro_Excel(MonitorDataInputDto input)
        {
            this.ValidarInputPeriodo(ref input);

            var dados = await _unitOfWork.MonitorRepository.BuscarMensagensErro(input.DataInicio, input.DataFim.Value);

            using (ExcelPackage excel = new ExcelPackage())
            {
                var sheet = excel.Workbook.Worksheets.Add("Mensagens");

                sheet.Cells[1, 1].Value = "Período: ";
                sheet.Cells[1, 2].Value = string.Format("{0:dd/MM/yyyy HH:mm:ss} a {1:dd/MM/yyyy HH:mm:sss}", input.DataInicio, input.DataFim.Value);

                // Linha de Titulo
                sheet.Cells[3, 1].Value = "Data / Hora";
                sheet.Cells[3, 2].Value = "Mensagem";

                // Linha de Titulo - Negrito
                sheet.Cells[3, 1, 3, 2].Style.Font.Bold = true;
                sheet.Cells[3, 1, 3, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                var startRow = 3;

                if (dados != null)
                {
                    foreach (var dado in dados.OrderByDescending(d => d.DataHora))
                    {
                        startRow++;

                        sheet.Cells[startRow, 1].Value = dado.DataHora.ToString("dd/MM/yyyy HH:mm:ss");
                        sheet.Cells[startRow, 2].Value = dado.Mensagem;
                    }
                }

                // Auto Ajustar Colunas
                sheet.Cells.AutoFitColumns();

                var excelFileContent = excel.GetAsByteArray();

                return excelFileContent;
            }
        }

        public async Task<byte[]> GetOVDetalhes_Excel(MonitorOVDataInputDto input)
        {
            var notificacoes = await _unitOfWork.MonitorRepository.BuscarOVNotificacao(input.OrdemVenda, input.DataInicio, input.DataFim);
            var envios = await _unitOfWork.MonitorRepository.BuscarOVResultados(input.OrdemVenda, input.DataInicio, input.DataFim);
            var mensagens = await _unitOfWork.MonitorRepository.BuscarOVMensagensErro(input.OrdemVenda, input.DataInicio, input.DataFim);

            using (ExcelPackage excel = new ExcelPackage())
            {
                if (notificacoes != null)
                {
                    var sheet = excel.Workbook.Worksheets.Add("Notificacoes");

                    sheet.Cells[1, 1].Value = "Período: ";

                    string mensagemPeriodo = "Completo";

                    if (input.DataInicio.HasValue && input.DataFim.HasValue)
                        mensagemPeriodo = string.Format("{0:dd/MM/yyyy HH:mm:ss} a {1:dd/MM/yyyy HH:mm:sss}", input.DataInicio.Value, input.DataFim.Value);
                    else if (input.DataInicio.HasValue)
                        mensagemPeriodo = string.Format("A partir de {0:dd/MM/yyyy HH:mm:ss}", input.DataInicio.Value);
                    else if (input.DataFim.HasValue)
                        mensagemPeriodo = string.Format("Até {0:dd/MM/yyyy HH:mm:ss}", input.DataFim.Value);

                    sheet.Cells[1, 2].Value = mensagemPeriodo;

                    sheet.Cells[1, 2, 1, 6].Merge = true;

                    // Linha de Titulo
                    sheet.Cells[3, 1].Value = "Data / Hora";
                    sheet.Cells[3, 2].Value = "Ordem Venda";
                    sheet.Cells[3, 3].Value = "Status";
                    sheet.Cells[3, 4].Value = "Download";
                    sheet.Cells[3, 5].Value = "Avaliação";
                    sheet.Cells[3, 6].Value = "Envio";

                    // Linha de Titulo - Negrito
                    sheet.Cells[3, 1, 3, 6].Style.Font.Bold = true;
                    sheet.Cells[3, 1, 3, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    var startRow = 3;

                    foreach (var notif in notificacoes.OrderByDescending(d => d.DataHora))
                    {
                        startRow++;

                        sheet.Cells[startRow, 1].Value = notif.DataHora.ToString("dd/MM/yyyy HH:mm:ss");
                        sheet.Cells[startRow, 2].Value = notif.OrdemVenda;
                        sheet.Cells[startRow, 3].Value = notif.Status;

                        if (notif.DataHoraDownload.HasValue)
                            sheet.Cells[startRow, 4].Value = notif.DataHoraDownload.Value.ToString("dd/MM/yyyy HH:mm:ss");

                        if (notif.DataHoraProcessamento.HasValue)
                            sheet.Cells[startRow, 5].Value = notif.DataHoraProcessamento.Value.ToString("dd/MM/yyyy HH:mm:ss");

                        if (notif.DataHoraUltimoEnvio.HasValue)
                            sheet.Cells[startRow, 6].Value = notif.DataHoraUltimoEnvio.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    }

                    // Auto Ajustar Colunas
                    sheet.Cells.AutoFitColumns();
                }

                if (envios != null)
                {
                    var sheet = excel.Workbook.Worksheets.Add("Resultados");

                    sheet.Cells[1, 1].Value = "Período: ";

                    string mensagemPeriodo = "Completo";

                    if (input.DataInicio.HasValue && input.DataFim.HasValue)
                        mensagemPeriodo = string.Format("{0:dd/MM/yyyy HH:mm:ss} a {1:dd/MM/yyyy HH:mm:sss}", input.DataInicio.Value, input.DataFim.Value);
                    else if (input.DataInicio.HasValue)
                        mensagemPeriodo = string.Format("A partir de {0:dd/MM/yyyy HH:mm:ss}", input.DataInicio.Value);
                    else if (input.DataFim.HasValue)
                        mensagemPeriodo = string.Format("Até {0:dd/MM/yyyy HH:mm:ss}", input.DataFim.Value);

                    sheet.Cells[1, 2].Value = mensagemPeriodo;

                    sheet.Cells[1, 2, 1, 6].Merge = true;

                    // Linha de Titulo
                    sheet.Cells[3, 1].Value = "Data / Hora";
                    sheet.Cells[3, 2].Value = "Ordem Venda";
                    sheet.Cells[3, 3].Value = "Item";
                    sheet.Cells[3, 4].Value = "Divisão";
                    sheet.Cells[3, 5].Value = "Ação";
                    sheet.Cells[3, 6].Value = "Data Envio";

                    // Linha de Titulo - Negrito
                    sheet.Cells[3, 1, 3, 6].Style.Font.Bold = true;
                    sheet.Cells[3, 1, 3, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    var startRow = 3;

                    foreach (var envio in envios.OrderByDescending(d => d.DataHora))
                    {
                        startRow++;

                        sheet.Cells[startRow, 1].Value = envio.DataHora.ToString("dd/MM/yyyy HH:mm:ss");
                        sheet.Cells[startRow, 2].Value = envio.OrdemVenda;
                        sheet.Cells[startRow, 3].Value = envio.Item;
                        sheet.Cells[startRow, 4].Value = envio.Divisao;

                        if (envio.Liberar)
                            sheet.Cells[startRow, 5].Value = "Liberar";
                        else
                            sheet.Cells[startRow, 5].Value = "Bloquear";

                        if (envio.DataHoraEnvio.HasValue)
                            sheet.Cells[startRow, 6].Value = envio.DataHoraEnvio.Value.ToString("dd/MM/yyyy");
                    }

                    // Auto Ajustar Colunas
                    sheet.Cells.AutoFitColumns();
                }

                if (mensagens != null)
                {
                    var sheet = excel.Workbook.Worksheets.Add("Mensagens");

                    sheet.Cells[1, 1].Value = "Período: ";

                    string mensagemPeriodo = "Completo";

                    if (input.DataInicio.HasValue && input.DataFim.HasValue)
                        mensagemPeriodo = string.Format("{0:dd/MM/yyyy HH:mm:ss} a {1:dd/MM/yyyy HH:mm:sss}", input.DataInicio.Value, input.DataFim.Value);
                    else if (input.DataInicio.HasValue)
                        mensagemPeriodo = string.Format("A partir de {0:dd/MM/yyyy HH:mm:ss}", input.DataInicio.Value);
                    else if (input.DataFim.HasValue)
                        mensagemPeriodo = string.Format("Até {0:dd/MM/yyyy HH:mm:ss}", input.DataFim.Value);

                    sheet.Cells[1, 2].Value = mensagemPeriodo;

                    // Linha de Titulo
                    sheet.Cells[3, 1].Value = "Data / Hora";
                    sheet.Cells[3, 2].Value = "Mensagem";

                    // Linha de Titulo - Negrito
                    sheet.Cells[3, 1, 3, 2].Style.Font.Bold = true;
                    sheet.Cells[3, 1, 3, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    var startRow = 3;

                    foreach (var msg in mensagens.OrderByDescending(d => d.DataHora))
                    {
                        startRow++;

                        sheet.Cells[startRow, 1].Value = msg.DataHora.ToString("dd/MM/yyyy HH:mm:ss");
                        sheet.Cells[startRow, 2].Value = msg.Mensagem;
                    }

                    // Auto Ajustar Colunas
                    sheet.Cells.AutoFitColumns();
                }

                var excelFileContent = excel.GetAsByteArray();

                return excelFileContent;
            }
        }

        private void ValidarInputPeriodo(ref MonitorDataInputDto input)
        {
            if (!input.DataFim.HasValue)
                input.DataFim = DateTime.Now;

            if ((input.DataFim.Value - input.DataInicio).Days > 31)
                throw new ArgumentException("Período Máximo de Análise é de 30 dias.");
        }
    }
}
