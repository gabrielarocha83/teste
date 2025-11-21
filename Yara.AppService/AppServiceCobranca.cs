using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceCobranca : IAppServiceCobranca
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceCobranca(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CobrancaResultadoDto>> CobrancaGeralPorDiretoria(string empresaId, string diretoriaId = null)
        {
            var resultado = await _unitOfWork.CobrancaResultadoRepository.CobrancaGeralPorDiretoria(empresaId, diretoriaId);

            var clientesDto = resultado.MapTo<IEnumerable<CobrancaResultadoDto>>().ToList();
            clientesDto.ForEach(c => { c.Dias30 = Math.Round(c.Dias30, 2); c.Dias60 = Math.Round(c.Dias60, 2); c.Dias90 = Math.Round(c.Dias90, 2); c.Dias180 = Math.Round(c.Dias180, 2); c.DiasMais = Math.Round(c.DiasMais, 2); c.Total = Math.Round(c.Total, 2); });

            return clientesDto;
        }

        public async Task<IEnumerable<CobrancaResultadoDto>> CobrancaEfetivaPorDiretoria(string empresaId, string diretoriaId = null)
        {
            var resultado = await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorDiretoria(empresaId, diretoriaId);

            var clientesDto = resultado.MapTo<IEnumerable<CobrancaResultadoDto>>().ToList();
            clientesDto.ForEach(c => { c.Dias30 = Math.Round(c.Dias30, 2); c.Dias60 = Math.Round(c.Dias60, 2); c.Dias90 = Math.Round(c.Dias90, 2); c.Dias180 = Math.Round(c.Dias180, 2); c.DiasMais = Math.Round(c.DiasMais, 2); c.Total = Math.Round(c.Total, 2); });

            return clientesDto;
        }

        public async Task<IEnumerable<CobrancaResultadoDto>> CobrancaEfetivaPorStatus(string empresaId, string diretoriaId = null)
        {
            var resultado = await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorStatus(empresaId, diretoriaId);

            var clientesDto = resultado.MapTo<IEnumerable<CobrancaResultadoDto>>().ToList();
            clientesDto.ForEach(c => { c.Dias30 = Math.Round(c.Dias30, 2); c.Dias60 = Math.Round(c.Dias60, 2); c.Dias90 = Math.Round(c.Dias90, 2); c.Dias180 = Math.Round(c.Dias180, 2); c.DiasMais = Math.Round(c.DiasMais, 2); c.Total = Math.Round(c.Total, 2); });

            return clientesDto;
        }

        public async Task<IEnumerable<CobrancaResultadoDto>> CobrancaEfetivaPorEstado(string empresaId, string diretoriaId = null)
        {
            var resultado = await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorEstado(empresaId, diretoriaId);

            var clientesDto = resultado.MapTo<IEnumerable<CobrancaResultadoDto>>().ToList();
            clientesDto.ForEach(c => { c.Dias30 = Math.Round(c.Dias30, 2); c.Dias60 = Math.Round(c.Dias60, 2); c.Dias90 = Math.Round(c.Dias90, 2); c.Dias180 = Math.Round(c.Dias180, 2); c.DiasMais = Math.Round(c.DiasMais, 2); c.Total = Math.Round(c.Total, 2); });

            return clientesDto;
        }

        public async Task<IEnumerable<CobrancaVencidosResultadoDto>> CobrancaVencidosMenosDias(string empresaId, string diretoriaId = null)
        {
            var resultado = await _unitOfWork.CobrancaResultadoRepository.CobrancaVencidosMenosDias(empresaId, diretoriaId);

            var clientesDto = resultado.MapTo<IEnumerable<CobrancaVencidosResultadoDto>>().ToList();
            clientesDto.ForEach(c => { c.Valor = Math.Round(c.Valor, 2); });

            return clientesDto;
        }

        public async Task<IEnumerable<CobrancaResultadoDto>> CobrancaMaioresDevedores(string empresaId, string diretoriaId = null)
        {
            var resultado = await _unitOfWork.CobrancaResultadoRepository.CobrancaMaioresDevedores(empresaId, diretoriaId);

            var clientesDto = resultado.MapTo<IEnumerable<CobrancaResultadoDto>>().ToList();
            clientesDto.ForEach(c => { c.Dias30 = Math.Round(c.Dias30, 2); c.Dias60 = Math.Round(c.Dias60, 2); c.Dias90 = Math.Round(c.Dias90, 2); c.Dias180 = Math.Round(c.Dias180, 2); c.DiasMais = Math.Round(c.DiasMais, 2); c.Total = Math.Round(c.Total, 2); });

            return clientesDto;
        }

        public async Task<IEnumerable<CobrancaResultadoDto>> CobrancaEfetivaPorCultura(string empresaId, string diretoriaId = null)
        {
            var resultado = await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorCultura(empresaId, diretoriaId);

            var clientesDto = resultado.MapTo<IEnumerable<CobrancaResultadoDto>>().ToList();
            clientesDto.ForEach(c => { c.Dias30 = Math.Round(c.Dias30, 2); c.Dias60 = Math.Round(c.Dias60, 2); c.Dias90 = Math.Round(c.Dias90, 2); c.Dias180 = Math.Round(c.Dias180, 2); c.DiasMais = Math.Round(c.DiasMais, 2); c.Total = Math.Round(c.Total, 2); });

            return clientesDto;
        }

        public async Task<CobrancaListaClienteResultadoDto> JuridicoClientes(string empresaId, int mes, int ano)
        {
            CobrancaListaClienteResultadoDto resultado = new CobrancaListaClienteResultadoDto();

            IEnumerable<CobrancaListaCliente> clientes = await _unitOfWork.CobrancaResultadoRepository.Juridico_Clientes(empresaId, mes, ano);

            var clientesDto = clientes.MapTo<IEnumerable<CobrancaListaClienteDto>>().ToList();
            resultado.ValorTotal = Math.Round(clientesDto.Sum(c => c.Valor), 2);

            clientesDto.ForEach(c => { c.Valor = Math.Round(c.Valor, 2); });
            resultado.Clientes = clientesDto;

            resultado.NomeDiretoria = "Jurídico";

            resultado.QuantidadeTotal = resultado.Clientes.Count();
            resultado.QuantidadeHoje = resultado.Clientes.Where(c => c.UltimaAtualizacao.HasValue && c.UltimaAtualizacao.Value.Date == DateTime.Now.Date).Count();
            resultado.ValorHoje = resultado.Clientes.Where(c => c.UltimaAtualizacao.HasValue && c.UltimaAtualizacao.Value.Date == DateTime.Now.Date).Sum(c => c.Valor);

            return resultado;
        }

        public async Task<CobrancaListaClienteResultadoDto> CobrancaClientes(string empresaId, string tipo, string chave, int dias, string diretoriaId = null)
        {
            CobrancaListaClienteResultadoDto resultado = new CobrancaListaClienteResultadoDto();

            IEnumerable<CobrancaListaCliente> clientes = null;

            switch (tipo)
            {
                case "GD":
                    clientes = await _unitOfWork.CobrancaResultadoRepository.CobrancaGeralPorDiretoria_Clientes(empresaId, chave, dias, diretoriaId);
                    break;

                case "ED":
                    clientes = await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorDiretoria_Clientes(empresaId, chave, dias, diretoriaId);
                    break;

                case "ES":
                    clientes = await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorStatus_Clientes(empresaId, chave, dias, diretoriaId);
                    break;

                case "EE":
                    clientes = await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorEstado_Clientes(empresaId, chave, dias, diretoriaId);
                    break;

                case "EC":
                    clientes = await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorCultura_Clientes(empresaId, chave, dias, diretoriaId);
                    break;

                default:
                    clientes = new List<CobrancaListaCliente>();
                    break;
            }

            var clientesDto = clientes.MapTo<IEnumerable<CobrancaListaClienteDto>>().ToList();
            resultado.ValorTotal = Math.Round(clientesDto.Sum(c => c.Valor), 2);

            clientesDto.ForEach(c => { c.Valor = Math.Round(c.Valor, 2); });
            resultado.Clientes = clientesDto;

            var estrutura = await _unitOfWork.EstruturaComercialRepository.GetAsync(c => c.CodigoSap.Equals(diretoriaId ?? chave));
            resultado.NomeDiretoria = estrutura != null ? estrutura.Nome : "[Sem diretoria]";

            resultado.QuantidadeTotal = resultado.Clientes.Count();
            resultado.QuantidadeHoje = resultado.Clientes.Where(c => c.UltimaAtualizacao.HasValue && c.UltimaAtualizacao.Value.Date == DateTime.Now.Date).Count();
            resultado.ValorHoje = resultado.Clientes.Where(c => c.UltimaAtualizacao.HasValue && c.UltimaAtualizacao.Value.Date == DateTime.Now.Date).Sum(c => c.Valor);

            return resultado;
        }

        public async Task<CobrancaListaClienteResultadoDto> CobrancaClientesGrandTotal(string empresaId, string tipo, string chave, string diretoriaId = null)
        {
            CobrancaListaClienteResultadoDto resultado = new CobrancaListaClienteResultadoDto();

            List<CobrancaListaCliente> clientes = new List<CobrancaListaCliente>();

            int[] diasForLoop = { 30, 60, 90, 180, 0 };

            foreach (var dias in diasForLoop) {
                switch (tipo)
                {
                    case "GD":
                        clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaGeralPorDiretoria_Clientes(empresaId, chave, dias, diretoriaId));
                        break;

                    case "ED":
                        clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorDiretoria_Clientes(empresaId, chave, dias, diretoriaId));
                        break;

                    case "ES":
                        clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorStatus_Clientes(empresaId, chave, dias, diretoriaId));
                        break;

                    case "EE":
                        clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorEstado_Clientes(empresaId, chave, dias, diretoriaId));
                        break;

                    case "EC":
                        clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorCultura_Clientes(empresaId, chave, dias, diretoriaId));
                        break;

                    default:
                        clientes = null;
                        break;
                }
            }

            var clientesDto = clientes.MapTo<IEnumerable<CobrancaListaClienteDto>>().ToList();
            resultado.ValorTotal = Math.Round(clientesDto.Sum(c => c.Valor), 2);

            clientesDto.ForEach(c => { c.Valor = Math.Round(c.Valor, 2); });
            resultado.Clientes = clientesDto;

            var estrutura = await _unitOfWork.EstruturaComercialRepository.GetAsync(c => c.CodigoSap.Equals(diretoriaId ?? chave));
            resultado.NomeDiretoria = estrutura != null ? estrutura.Nome : "[Sem diretoria]";

            resultado.QuantidadeTotal = resultado.Clientes.Count();
            resultado.QuantidadeHoje = resultado.Clientes.Where(c => c.UltimaAtualizacao.HasValue && c.UltimaAtualizacao.Value.Date == DateTime.Now.Date).Count();
            resultado.ValorHoje = resultado.Clientes.Where(c => c.UltimaAtualizacao.HasValue && c.UltimaAtualizacao.Value.Date == DateTime.Now.Date).Sum(c => c.Valor);

            return resultado;
        }

        public async Task<CobrancaListaClienteResultadoDto> CobrancaClientesGrandTotalSum(string empresaId, string tipo)
        {
            CobrancaListaClienteResultadoDto resultado = new CobrancaListaClienteResultadoDto();

            List<CobrancaResultadoDto> cobrancas = new List<CobrancaResultadoDto>();

            List<CobrancaListaCliente> clientes = new List<CobrancaListaCliente>();

            switch (tipo)
            {
                case "GD":
                    cobrancas.AddRange(await CobrancaGeralPorDiretoria(empresaId, null));
                    break;

                case "ED":
                    cobrancas.AddRange(await CobrancaEfetivaPorDiretoria(empresaId, null));
                    break;

                case "ES":
                    cobrancas.AddRange(await CobrancaEfetivaPorStatus(empresaId, null));
                    break;

                case "EE":
                    cobrancas.AddRange(await CobrancaEfetivaPorEstado(empresaId, null));
                    break;

                case "EC":
                    cobrancas.AddRange(await CobrancaEfetivaPorCultura(empresaId, null));
                    break;

                default:
                    clientes = null;
                    break;
            }

            int[] diasForLoop = { 30, 60, 90, 180, 0 };

            foreach (var cobranca in cobrancas)
            {
                foreach (var dias in diasForLoop)
                {
                    switch (tipo)
                    {
                        case "GD":
                            clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaGeralPorDiretoria_Clientes(empresaId, cobranca.Chave, dias, null));
                            break;

                        case "ED":
                            clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorDiretoria_Clientes(empresaId, cobranca.Chave, dias, null));
                            break;

                        case "ES":
                            clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorStatus_Clientes(empresaId, cobranca.Chave, dias, null));
                            break;

                        case "EE":
                            clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorEstado_Clientes(empresaId, cobranca.Chave, dias, null));
                            break;

                        case "EC":
                            clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorCultura_Clientes(empresaId, cobranca.Chave, dias, null));
                            break;

                        default:
                            clientes = new List<CobrancaListaCliente>();
                            break;
                    }
                }
            }

            var clientesDto = clientes.MapTo<IEnumerable<CobrancaListaClienteDto>>().ToList();
            resultado.ValorTotal = Math.Round(clientesDto.Sum(c => c.Valor), 2);

            clientesDto.ForEach(c => { c.Valor = Math.Round(c.Valor, 2); });
            resultado.Clientes = clientesDto;

            resultado.NomeDiretoria = "Total";

            resultado.QuantidadeTotal = resultado.Clientes.Count();
            resultado.QuantidadeHoje = resultado.Clientes.Where(c => c.UltimaAtualizacao.HasValue && c.UltimaAtualizacao.Value.Date == DateTime.Now.Date).Count();
            resultado.ValorHoje = resultado.Clientes.Where(c => c.UltimaAtualizacao.HasValue && c.UltimaAtualizacao.Value.Date == DateTime.Now.Date).Sum(c => c.Valor);

            return resultado;
        }

        public async Task<byte[]> CobrancaClientesExcel(string empresaId, string tipo, string chave, int dias, string diretoriaId = null)
        {
            IEnumerable<CobrancaListaCliente> clientes = null;

            switch (tipo)
            {
                case "JD":
                    int mes = 0;
                    int ano = DateTime.Now.Year;

                    if (chave != null && chave.Length == 4)
                        ano = int.Parse(chave);
                    else
                        mes = int.Parse(chave);

                    clientes = await _unitOfWork.CobrancaResultadoRepository.Juridico_Clientes(empresaId, mes, ano);
                    break;

                case "GD":
                    clientes = await _unitOfWork.CobrancaResultadoRepository.CobrancaGeralPorDiretoria_Clientes(empresaId, chave, dias, diretoriaId);
                    break;

                case "ED":
                    clientes = await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorDiretoria_Clientes(empresaId, chave, dias, diretoriaId);
                    break;

                case "ES":
                    clientes = await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorStatus_Clientes(empresaId, chave, dias, diretoriaId);
                    break;

                case "EE":
                    clientes = await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorEstado_Clientes(empresaId, chave, dias, diretoriaId);
                    break;

                case "EC":
                    clientes = await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorCultura_Clientes(empresaId, chave, dias, diretoriaId);
                    break;

                default:
                    clientes = new List<CobrancaListaCliente>();
                    break;
            }

            try
            {
                // Regex para remover pontos, dígitos e barras do número do documento.
                var rgx = new Regex("[\\.\\-\\/]+");

                // Criar Excel
                using (ExcelPackage excel = new ExcelPackage())
                {
                    var sheet = excel.Workbook.Worksheets.Add("Clientes");

                    // Linha de Titulo
                    sheet.Cells[1, 1].Value = "Nome Cliente";
                    sheet.Cells[1, 2].Value = "CPF / CNPJ";
                    sheet.Cells[1, 3].Value = "Aging";
                    sheet.Cells[1, 4].Value = "Última Atualização";
                    sheet.Cells[1, 5].Value = "Valor (MBRL)";
                    sheet.Cells[1, 6].Value = "% Total";

                    // Linha de Titulo - Negrito
                    sheet.Cells[1, 1, 1, 6].Style.Font.Bold = true;
                    sheet.Cells[1, 1, 1, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    var startRow = 1;

                    var clientesDto = clientes.MapTo<IEnumerable<CobrancaListaClienteDto>>().ToList();

                    foreach (var cliente in clientesDto)
                    {
                        // Parse das informações do documento
                        string documentoFormatado = rgx.Replace(cliente.Documento.Trim(), "");
                        bool converted = UInt64.TryParse(documentoFormatado, out ulong documentoOut);

                        startRow++;

                        sheet.Cells[startRow, 1].Value = cliente.Nome;
                        sheet.Cells[startRow, 2].Value = converted ? (documentoFormatado.Length > 11 ? documentoOut.ToString(@"00\.000\.000\/0000\-00") : documentoOut.ToString(@"000\.000\.000\-00")) : "";
                        sheet.Cells[startRow, 3].Value = cliente.Aging;

                        sheet.Cells[startRow, 4].Value = cliente.UltimaAtualizacao;
                        sheet.Cells[startRow, 4].Style.Numberformat.Format = "dd/MM/yyyy";

                        sheet.Cells[startRow, 5].Value = Math.Round(cliente.Valor, 2);
                        sheet.Cells[startRow, 5].Style.Numberformat.Format = "#,##0.00";

                        sheet.Cells[startRow, 6].Value = cliente.Percentual / 100;
                        sheet.Cells[startRow, 6].Style.Numberformat.Format = "#,##0.0%";
                    }

                    // Linha de Totais
                    sheet.Cells[startRow + 1, 1].Value = "TOTAL";
                    sheet.Cells[startRow + 1, 5].Formula = string.Format("=SUM(E2:{0})", sheet.Cells[startRow, 5].Address);
                    sheet.Cells[startRow + 1, 5].Style.Numberformat.Format = "#,##0.00";
                    sheet.Cells[startRow + 1, 6].Formula = string.Format("=SUM(F2:{0})", sheet.Cells[startRow, 6].Address);
                    sheet.Cells[startRow + 1, 6].Style.Numberformat.Format = "#,##0.0%";

                    // Linha de Totais - Negrito
                    sheet.Cells[startRow + 1, 1, startRow + 1, 6].Style.Font.Bold = true;

                    // Auto Ajustar Colunas
                    sheet.Cells.AutoFitColumns();

                    var excelFileContent = excel.GetAsByteArray();

                    return excelFileContent;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<byte[]> CobrancaClientesExcelGrandTotal(string empresaId, string tipo, string chave, string diretoriaId = null)
        {
            CobrancaListaClienteResultadoDto resultado = new CobrancaListaClienteResultadoDto();

            List<CobrancaListaCliente> clientes = new List<CobrancaListaCliente>();

            int[] diasForLoop = { 30, 60, 90, 180, 0 };

            foreach (var dias in diasForLoop)
            {
                switch (tipo)
                {
                    case "GD":
                        clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaGeralPorDiretoria_Clientes(empresaId, chave, dias, diretoriaId));
                        break;

                    case "ED":
                        clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorDiretoria_Clientes(empresaId, chave, dias, diretoriaId));
                        break;

                    case "ES":
                        clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorStatus_Clientes(empresaId, chave, dias, diretoriaId));
                        break;

                    case "EE":
                        clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorEstado_Clientes(empresaId, chave, dias, diretoriaId));
                        break;

                    case "EC":
                        clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorCultura_Clientes(empresaId, chave, dias, diretoriaId));
                        break;

                    default:
                        clientes = null;
                        break;
                }
            }

            try
            {
                // Regex para remover pontos, dígitos e barras do número do documento.
                var rgx = new Regex("[\\.\\-\\/]+");

                // Criar Excel
                using (ExcelPackage excel = new ExcelPackage())
                {
                    var sheet = excel.Workbook.Worksheets.Add("Clientes");

                    // Linha de Titulo
                    sheet.Cells[1, 1].Value = "Nome Cliente";
                    sheet.Cells[1, 2].Value = "CPF / CNPJ";
                    sheet.Cells[1, 3].Value = "Aging";
                    sheet.Cells[1, 4].Value = "Última Atualização";
                    sheet.Cells[1, 5].Value = "Valor (MBRL)";
                    sheet.Cells[1, 6].Value = "% Total";

                    // Linha de Titulo - Negrito
                    sheet.Cells[1, 1, 1, 6].Style.Font.Bold = true;
                    sheet.Cells[1, 1, 1, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    var startRow = 1;

                    var clientesDto = clientes.MapTo<IEnumerable<CobrancaListaClienteDto>>().ToList();

                    foreach (var cliente in clientesDto)
                    {
                        // Parse das informações do documento
                        string documentoFormatado = rgx.Replace(cliente.Documento.Trim(), "");
                        bool converted = UInt64.TryParse(documentoFormatado, out ulong documentoOut);

                        startRow++;

                        sheet.Cells[startRow, 1].Value = cliente.Nome;
                        sheet.Cells[startRow, 2].Value = converted ? (documentoFormatado.Length > 11 ? documentoOut.ToString(@"00\.000\.000\/0000\-00") : documentoOut.ToString(@"000\.000\.000\-00")) : "";
                        sheet.Cells[startRow, 3].Value = cliente.Aging;

                        sheet.Cells[startRow, 4].Value = cliente.UltimaAtualizacao;
                        sheet.Cells[startRow, 4].Style.Numberformat.Format = "dd/MM/yyyy";

                        sheet.Cells[startRow, 5].Value = Math.Round(cliente.Valor, 2);
                        sheet.Cells[startRow, 5].Style.Numberformat.Format = "#,##0.00";

                        sheet.Cells[startRow, 6].Value = cliente.Percentual / 100;
                        sheet.Cells[startRow, 6].Style.Numberformat.Format = "#,##0.0%";
                    }

                    // Linha de Totais
                    sheet.Cells[startRow + 1, 1].Value = "TOTAL";
                    sheet.Cells[startRow + 1, 5].Formula = string.Format("=SUM(E2:{0})", sheet.Cells[startRow, 5].Address);
                    sheet.Cells[startRow + 1, 5].Style.Numberformat.Format = "#,##0.00";
                    sheet.Cells[startRow + 1, 6].Formula = string.Format("=SUM(F2:{0})", sheet.Cells[startRow, 6].Address);
                    sheet.Cells[startRow + 1, 6].Style.Numberformat.Format = "#,##0.0%";

                    // Linha de Totais - Negrito
                    sheet.Cells[startRow + 1, 1, startRow + 1, 6].Style.Font.Bold = true;

                    // Auto Ajustar Colunas
                    sheet.Cells.AutoFitColumns();

                    var excelFileContent = excel.GetAsByteArray();

                    return excelFileContent;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<byte[]> CobrancaClientesExcelGrandTotalSum(string empresaId, string tipo)
        {
            CobrancaListaClienteResultadoDto resultado = new CobrancaListaClienteResultadoDto();

            List<CobrancaResultadoDto> cobrancas = new List<CobrancaResultadoDto>();

            List<CobrancaListaCliente> clientes = new List<CobrancaListaCliente>();

            switch (tipo)
            {
                case "GD":
                    cobrancas.AddRange(await CobrancaGeralPorDiretoria(empresaId, null));
                    break;

                case "ED":
                    cobrancas.AddRange(await CobrancaEfetivaPorDiretoria(empresaId, null));
                    break;

                case "ES":
                    cobrancas.AddRange(await CobrancaEfetivaPorStatus(empresaId, null));
                    break;

                case "EE":
                    cobrancas.AddRange(await CobrancaEfetivaPorEstado(empresaId, null));
                    break;

                case "EC":
                    cobrancas.AddRange(await CobrancaEfetivaPorCultura(empresaId, null));
                    break;

                default:
                    cobrancas = null;
                    break;
            }

            int[] diasForLoop = { 30, 60, 90, 180, 0 };

            foreach (var cobranca in cobrancas)
            {
                foreach (var dias in diasForLoop)
                {
                    switch (tipo)
                    {
                        case "GD":
                            clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaGeralPorDiretoria_Clientes(empresaId, cobranca.Chave, dias, null));
                            break;

                        case "ED":
                            clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorDiretoria_Clientes(empresaId, cobranca.Chave, dias, null));
                            break;

                        case "ES":
                            clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorStatus_Clientes(empresaId, cobranca.Chave, dias, null));
                            break;

                        case "EE":
                            clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorEstado_Clientes(empresaId, cobranca.Chave, dias, null));
                            break;

                        case "EC":
                            clientes.AddRange(await _unitOfWork.CobrancaResultadoRepository.CobrancaEfetivaPorCultura_Clientes(empresaId, cobranca.Chave, dias, null));
                            break;

                        default:
                            clientes = new List<CobrancaListaCliente>();
                            break;
                    }
                }
            }

            try
            {
                // Regex para remover pontos, dígitos e barras do número do documento.
                var rgx = new Regex("[\\.\\-\\/]+");

                // Criar Excel
                using (ExcelPackage excel = new ExcelPackage())
                {
                    var sheet = excel.Workbook.Worksheets.Add("Clientes");

                    // Linha de Titulo
                    sheet.Cells[1, 1].Value = "Nome Cliente";
                    sheet.Cells[1, 2].Value = "CPF / CNPJ";
                    sheet.Cells[1, 3].Value = "Aging";
                    sheet.Cells[1, 4].Value = "Última Atualização";
                    sheet.Cells[1, 5].Value = "Valor (MBRL)";
                    sheet.Cells[1, 6].Value = "% Total";

                    // Linha de Titulo - Negrito
                    sheet.Cells[1, 1, 1, 6].Style.Font.Bold = true;
                    sheet.Cells[1, 1, 1, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    var startRow = 1;

                    var clientesDto = clientes.MapTo<IEnumerable<CobrancaListaClienteDto>>().ToList();

                    foreach (var cliente in clientesDto)
                    {
                        // Parse das informações do documento
                        string documentoFormatado = rgx.Replace(cliente.Documento.Trim(), "");
                        bool converted = UInt64.TryParse(documentoFormatado, out ulong documentoOut);

                        startRow++;

                        sheet.Cells[startRow, 1].Value = cliente.Nome;
                        sheet.Cells[startRow, 2].Value = converted ? (documentoFormatado.Length > 11 ? documentoOut.ToString(@"00\.000\.000\/0000\-00") : documentoOut.ToString(@"000\.000\.000\-00")) : "";
                        sheet.Cells[startRow, 3].Value = cliente.Aging;

                        sheet.Cells[startRow, 4].Value = cliente.UltimaAtualizacao;
                        sheet.Cells[startRow, 4].Style.Numberformat.Format = "dd/MM/yyyy";

                        sheet.Cells[startRow, 5].Value = Math.Round(cliente.Valor, 2);
                        sheet.Cells[startRow, 5].Style.Numberformat.Format = "#,##0.00";

                        sheet.Cells[startRow, 6].Value = cliente.Percentual / 100;
                        sheet.Cells[startRow, 6].Style.Numberformat.Format = "#,##0.0%";
                    }

                    // Linha de Totais
                    sheet.Cells[startRow + 1, 1].Value = "TOTAL";
                    sheet.Cells[startRow + 1, 5].Formula = string.Format("=SUM(E2:{0})", sheet.Cells[startRow, 5].Address);
                    sheet.Cells[startRow + 1, 5].Style.Numberformat.Format = "#,##0.00";
                    sheet.Cells[startRow + 1, 6].Formula = string.Format("=SUM(F2:{0})", sheet.Cells[startRow, 6].Address);
                    sheet.Cells[startRow + 1, 6].Style.Numberformat.Format = "#,##0.0%";

                    // Linha de Totais - Negrito
                    sheet.Cells[startRow + 1, 1, startRow + 1, 6].Style.Font.Bold = true;

                    // Auto Ajustar Colunas
                    sheet.Cells.AutoFitColumns();

                    var excelFileContent = excel.GetAsByteArray();

                    return excelFileContent;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<TituloContaClienteDto>> BuscaTitulosContaCliente(Guid contaClienteId, string empresaId)
        {
            var resultado = await _unitOfWork.CobrancaResultadoRepository.BuscaTitulosContaCliente(contaClienteId, empresaId);
            return resultado.MapTo<IEnumerable<TituloContaClienteDto>>();
        }

        public async Task<byte[]> BuscaTitulosContaClienteExcel(Guid contaClienteId, string empresaId, bool acessoCompleto)
        {
            var contaCliente = await _unitOfWork.ContaClienteRepository.GetOneByIDAsync(contaClienteId);

            string nomeCliente = contaCliente != null ? contaCliente.Nome : "";
            string documentoCliente = contaCliente != null ? contaCliente.Documento : "";

            var titulos = await _unitOfWork.CobrancaResultadoRepository.BuscaTitulosContaCliente(contaClienteId, empresaId);

            // Criar Excel
            using (ExcelPackage excel = new ExcelPackage())
            {
                var sheet = excel.Workbook.Worksheets.Add("Titulos");

                int c = 0;

                if (!acessoCompleto) c = 3;

                // Linha de Titulo
                sheet.Cells[1, 1].Value = "CTC";
                sheet.Cells[1, 2].Value = "Nota Fiscal";
                sheet.Cells[1, 3].Value = "Pedido";
                sheet.Cells[1, 4].Value = "Emissão";
                sheet.Cells[1, 5].Value = "Vencimento original";
                sheet.Cells[1, 6].Value = "Vencimento prorrogado";
                sheet.Cells[1, 7].Value = "PayT";
                sheet.Cells[1, 8].Value = "Valor(R$)";
                sheet.Cells[1, 9].Value = "Taxa cambial";
                if (acessoCompleto)
                {
                    sheet.Cells[1, 10].Value = "Taxa de juros";
                    sheet.Cells[1, 11].Value = "Valor de juros";
                    sheet.Cells[1, 12].Value = "Valor atual";
                }
                sheet.Cells[1, 13 - c].Value = "Previsão de pagamento";
                sheet.Cells[1, 14 - c].Value = "Comentário histórico";
                sheet.Cells[1, 15 - c].Value = "Status CBR";
                sheet.Cells[1, 16 - c].Value = "Faixa de vencimento";
                sheet.Cells[1, 17 - c].Value = "Dias";
                sheet.Cells[1, 18 - c].Value = "PR / REPR";
                sheet.Cells[1, 19 - c].Value = "Tipo";
                sheet.Cells[1, 20 - c].Value = "Emissão DP";
                sheet.Cells[1, 21 - c].Value = "Emissão TR";
                sheet.Cells[1, 22 - c].Value = "Aceite";
                sheet.Cells[1, 23 - c].Value = "Inclusão Pefin";
                sheet.Cells[1, 24 - c].Value = "Exclusão Pefin";
                sheet.Cells[1, 25 - c].Value = "Protesto solicitado";
                sheet.Cells[1, 26 - c].Value = "Protesto realizado";
                sheet.Cells[1, 27 - c].Value = "Quantidade Entregue";
                sheet.Cells[1, 28 - c].Value = "Quantidade Pendente";
                sheet.Cells[1, 29 - c].Value = "Nome Cliente";
                sheet.Cells[1, 30 - c].Value = "CPF / CNPJ";

                // Linha de Titulo - Negrito
                sheet.Cells[1, 1, 1, 30].Style.Font.Bold = true;
                sheet.Cells[1, 1, 1, 30].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                var startRow = 1;

                foreach (var titulo in titulos)
                {
                    startRow++;

                    sheet.Cells[startRow, 1].Value = titulo.NomeCTC;
                    sheet.Cells[startRow, 2].Value = titulo.NotaFiscal;
                    sheet.Cells[startRow, 3].Value = titulo.OrdemVendaNumero;
                    sheet.Cells[startRow, 4].Value = titulo.DataEmissaoDocumento;
                    sheet.Cells[startRow, 4].Style.Numberformat.Format = "dd/MM/yyyy";
                    sheet.Cells[startRow, 5].Value = titulo.VencimentoOriginal;
                    sheet.Cells[startRow, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                    sheet.Cells[startRow, 6].Value = titulo.DataVencimento;
                    sheet.Cells[startRow, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                    sheet.Cells[startRow, 7].Value = titulo.CondPagto;
                    sheet.Cells[startRow, 8].Value = titulo.ValorInterno;
                    sheet.Cells[startRow, 8].Style.Numberformat.Format = "#,##0.00";
                    sheet.Cells[startRow, 9].Value = titulo.TaxaCambio;
                    sheet.Cells[startRow, 9].Style.Numberformat.Format = "#0.0000";
                    if (acessoCompleto)
                    {
                        sheet.Cells[startRow, 10].Value = titulo.TaxaJuros;
                        sheet.Cells[startRow, 10].Style.Numberformat.Format = "#0.0###";
                        sheet.Cells[startRow, 11].Value = titulo.ValorJuros;
                        sheet.Cells[startRow, 11].Style.Numberformat.Format = "#,##0.00";
                        sheet.Cells[startRow, 12].Value = titulo.ValorAtual;
                        sheet.Cells[startRow, 12].Style.Numberformat.Format = "#,##0.00";
                    }
                    sheet.Cells[startRow, 13-c].Value = titulo.PrevisaoPagamento;
                    sheet.Cells[startRow, 13 - c].Style.Numberformat.Format = "dd/MM/yyyy";
                    sheet.Cells[startRow, 14 - c].Value = titulo.UltimoComentario;
                    sheet.Cells[startRow, 15 - c].Value = titulo.DescricaoStatusCobranca;
                    sheet.Cells[startRow, 16 - c].Value = titulo.FaixaVencimento;
                    sheet.Cells[startRow, 17 - c].Value = titulo.Dias;
                    sheet.Cells[startRow, 17 - c].Style.Numberformat.Format = "#,##0";
                    sheet.Cells[startRow, 18 - c].Value = titulo.TipoPR;
                    sheet.Cells[startRow, 19 - c].Value = titulo.TipoVencimento;
                    sheet.Cells[startRow, 20 - c].Value = titulo.DataDuplicata;
                    sheet.Cells[startRow, 20 - c].Style.Numberformat.Format = "dd/MM/yyyy";
                    sheet.Cells[startRow, 21 - c].Value = titulo.DataTriplicata;
                    sheet.Cells[startRow, 21 - c].Style.Numberformat.Format = "dd/MM/yyyy";
                    sheet.Cells[startRow, 22 - c].Value = titulo.DataAceite.HasValue ? "Sim" : "Não";
                    sheet.Cells[startRow, 23 - c].Value = titulo.DataPefinInclusao;
                    sheet.Cells[startRow, 23 - c].Style.Numberformat.Format = "dd/MM/yyyy";
                    sheet.Cells[startRow, 24 - c].Value = titulo.DataPefinExclusao;
                    sheet.Cells[startRow, 24 - c].Style.Numberformat.Format = "dd/MM/yyyy";
                    sheet.Cells[startRow, 25 - c].Value = titulo.DataProtesto;
                    sheet.Cells[startRow, 25 - c].Style.Numberformat.Format = "dd/MM/yyyy";
                    sheet.Cells[startRow, 26 - c].Value = titulo.DataProtestoRealizado;
                    sheet.Cells[startRow, 26 - c].Style.Numberformat.Format = "dd/MM/yyyy";
                    sheet.Cells[startRow, 27 - c].Value = titulo.QtdEntregue;
                    sheet.Cells[startRow, 27 - c].Style.Numberformat.Format = "#,##0";
                    sheet.Cells[startRow, 28 - c].Value = titulo.QtdPendente;
                    sheet.Cells[startRow, 28 - c].Style.Numberformat.Format = "#,##0";
                    sheet.Cells[startRow, 29 - c].Value = nomeCliente;
                    sheet.Cells[startRow, 30 - c].Value = (documentoCliente.Length > 11 ? Convert.ToUInt64(documentoCliente).ToString(@"00\.000\.000\/0000\-00") : Convert.ToUInt64(documentoCliente).ToString(@"000\.000\.000\-00"));
                }

                // Linha de Totais
                sheet.Cells[startRow + 1, 1].Value = "TOTAL";
                sheet.Cells[startRow + 1, 8].Formula = string.Format("=SUM(H2:{0})", sheet.Cells[startRow, 8].Address);
                sheet.Cells[startRow + 1, 8].Style.Numberformat.Format = "#,##0.00";

                if (acessoCompleto)
                {
                    sheet.Cells[startRow + 1, 11 - c].Formula = string.Format("=SUM(K2:{0})", sheet.Cells[startRow, 11].Address);
                    sheet.Cells[startRow + 1, 11].Style.Numberformat.Format = "#,##0.00";
                    sheet.Cells[startRow + 1, 12].Formula = string.Format("=SUM(L2:{0})", sheet.Cells[startRow, 12].Address);
                    sheet.Cells[startRow + 1, 12].Style.Numberformat.Format = "#,##0.00";
                }

                // Linha de Totais - Negrito
                sheet.Cells[startRow + 1, 1, startRow + 1, 12].Style.Font.Bold = true;

                // Auto Ajustar Colunas
                sheet.Cells.AutoFitColumns();

                var excelFileContent = excel.GetAsByteArray();

                return excelFileContent;
            }
        }

        public async Task<CobrancaContaDto> ExistPropostas(Guid ID, string EmpresaID)
        {
            var abono = await _unitOfWork.PropostaAbonoRepository.GetAllFilterAsync(c => c.ContaClienteID == ID && c.EmpresaID == EmpresaID &&
                                                                                   (c.PropostaCobrancaStatusID == "EC" || c.PropostaCobrancaStatusID == "EA" || c.PropostaCobrancaStatusID == "CC" || c.PropostaCobrancaStatusID == "ET" || c.PropostaCobrancaStatusID == "AA" || c.PropostaCobrancaStatusID == "AC"));
            var ultimoabono = abono.OrderByDescending(c => c.DataCriacao).FirstOrDefault();
            var idabono = ultimoabono?.ID ?? Guid.Empty;
            var statusabono = ultimoabono == null ? "" : ultimoabono.PropostaCobrancaStatusID;

            var juridico = await _unitOfWork.PropostaJuridicoRepository.GetAllFilterAsync(c => c.ContaClienteID == ID && c.EmpresaID == EmpresaID && (c.PropostaJuridicoStatus == "EA"));
            var ultimaJuridico = juridico.OrderByDescending(c => c.DataCriacao).FirstOrDefault();
            var idjuridico = ultimaJuridico?.ID ?? Guid.Empty;
            var statusjuridico = ultimaJuridico == null ? "" : ultimaJuridico.PropostaJuridicoStatus == "EA" ? "IC" : "AP";

            var prorrogacao = await _unitOfWork.PropostaProrrogacao.GetAllFilterAsync(c => c.ContaClienteID == ID && c.EmpresaID == EmpresaID &&
                                                                                     (c.PropostaCobrancaStatusID == "EC" || c.PropostaCobrancaStatusID == "EA" || c.PropostaCobrancaStatusID == "CC" || c.PropostaCobrancaStatusID == "ET" || c.PropostaCobrancaStatusID == "AA" || c.PropostaCobrancaStatusID == "AC"));
            var ultimoprorrogacao = prorrogacao.OrderByDescending(c => c.DataCriacao).FirstOrDefault();
            var idprorrogacao = ultimoprorrogacao?.ID ?? Guid.Empty;
            var statusprorrogacao = ultimoprorrogacao == null ? "" : ultimoprorrogacao.PropostaCobrancaStatusID;

            var cobranca = new CobrancaContaDto()
            {
                PropostaAbonoID = ultimoabono?.ID ?? null,
                PropostaAbonoStatus = ultimoabono == null ? "" : ultimoabono.PropostaCobrancaStatusID,

                PropostaJuridicoID = ultimaJuridico?.ID ?? null,
                PropostaJuridicoStatus = ultimaJuridico == null ? "" : ultimaJuridico.PropostaJuridicoStatus,

                PropostaProrrogacaoID = ultimoprorrogacao?.ID ?? null,
                PropostaProrrogacaoStatus = ultimoprorrogacao == null ? "" : ultimoprorrogacao.PropostaCobrancaStatusID
            };

            return cobranca;
        }
    }
}