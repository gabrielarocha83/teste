using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OfficeOpenXml;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceRelatorios : IAppServiceRelatorios
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceRelatorios(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BuscaPropostasDto>> GetConsultaProposta(BuscaPropostasSearchDto filter)
        {
            try
            {
                var filtros = filter.MapTo<BuscaPropostasSearch>();
                var consulta = await _unitOfWork.Relatorios.GetConsultaProposta(filtros);

                return consulta.MapTo<IEnumerable<BuscaPropostasDto>>();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<byte[]> GetConsultaPropostaExcel(BuscaPropostasSearchDto filter)
        {
            try
            {
                var filters = filter.MapTo<BuscaPropostasSearch>();
                var resultados = await _unitOfWork.Relatorios.GetConsultaProposta(filters);

                using (ExcelPackage excel = new ExcelPackage())
                {
                    var sheet = excel.Workbook.Worksheets.Add("Clientes");

                    // Linha de Titulo
                    sheet.Cells[1, 1].Value = "Tipo de Proposta";
                    sheet.Cells[1, 2].Value = "Código Proposta";
                    sheet.Cells[1, 3].Value = "Nome";
                    sheet.Cells[1, 4].Value = "Código";
                    sheet.Cells[1, 5].Value = "Documento";
                    sheet.Cells[1, 6].Value = "LC Disponível";
                    sheet.Cells[1, 7].Value = "Data Entrega";
                    sheet.Cells[1, 8].Value = "Tipo Cliente";
                    sheet.Cells[1, 9].Value = "Status";
                    sheet.Cells[1, 10].Value = "Responsável";
                    sheet.Cells[1, 11].Value = "Analista";
                    sheet.Cells[1, 12].Value = "CTC";
                    sheet.Cells[1, 13].Value = "GC";
                    sheet.Cells[1, 14].Value = "Diretoria";
                    sheet.Cells[1, 15].Value = "Segmento";
                    sheet.Cells[1, 16].Value = "Data Criacao";
                    sheet.Cells[1, 17].Value = "Data Entrada Crédito";
                    sheet.Cells[1, 18].Value = "Data Conclusão";
                    sheet.Cells[1, 19].Value = "Valor Proposta";
                    sheet.Cells[1, 20].Value = "Rating";
                    sheet.Cells[1, 21].Value = "Vigência LC Aprovado";
                    sheet.Cells[1, 22].Value = "Fim Vigência LC Aprovado";
                    sheet.Cells[1, 23].Value = "LC Aprovado";
                    sheet.Cells[1, 24].Value = "LeadTime Cliente";
                    sheet.Cells[1, 25].Value = "LeadTime Comercial";

                    var startRow = 1;

                    foreach (var resultado in resultados)
                    {
                        startRow++;

                        sheet.Cells[startRow, 1].Value = resultado.TipoProposta;
                        sheet.Cells[startRow, 2].Value = resultado.CodigoProposta;
                        sheet.Cells[startRow, 3].Value = resultado.NomeCliente;
                        sheet.Cells[startRow, 4].Value = resultado.CodigoCliente;
                        sheet.Cells[startRow, 5].Value = resultado.Documento;

                        sheet.Cells[startRow, 6].Value = resultado.LCDisponivel ?? 0;
                        sheet.Cells[startRow, 6].Style.Numberformat.Format = "#,##0.00";

                        sheet.Cells[startRow, 7].Value = resultado.DataEntrega;
                        sheet.Cells[startRow, 7].Style.Numberformat.Format = "dd/MM/yyyy";

                        sheet.Cells[startRow, 8].Value = resultado.TipoCliente;
                        sheet.Cells[startRow, 9].Value = resultado.Status;
                        sheet.Cells[startRow, 10].Value = resultado.Responsavel;
                        sheet.Cells[startRow, 11].Value = resultado.Analista;
                        sheet.Cells[startRow, 12].Value = resultado.CTC;
                        sheet.Cells[startRow, 13].Value = resultado.GC;
                        sheet.Cells[startRow, 14].Value = resultado.Diretoria;
                        sheet.Cells[startRow, 15].Value = resultado.Segmento;

                        sheet.Cells[startRow, 16].Value = resultado.DataCriacao;
                        sheet.Cells[startRow, 16].Style.Numberformat.Format = "dd/MM/yyyy";

                        sheet.Cells[startRow, 17].Value = resultado.DataEntradaCredito;
                        sheet.Cells[startRow, 17].Style.Numberformat.Format = "dd/MM/yyyy";

                        sheet.Cells[startRow, 18].Value = resultado.DataConclusao;
                        sheet.Cells[startRow, 18].Style.Numberformat.Format = "dd/MM/yyyy";

                        sheet.Cells[startRow, 19].Value = resultado.ValorProposta ?? 0;
                        sheet.Cells[startRow, 19].Style.Numberformat.Format = "#,##0.00";

                        sheet.Cells[startRow, 20].Value = resultado.Rating;

                        sheet.Cells[startRow, 21].Value = resultado.Vigencia;
                        sheet.Cells[startRow, 21].Style.Numberformat.Format = "dd/MM/yyyy";

                        sheet.Cells[startRow, 22].Value = resultado.VigenciaFim;
                        sheet.Cells[startRow, 22].Style.Numberformat.Format = "dd/MM/yyyy";

                        sheet.Cells[startRow, 23].Value = resultado.LCAprovado ?? 0;
                        sheet.Cells[startRow, 23].Style.Numberformat.Format = "#,##0.00";

                        sheet.Cells[startRow, 24].Value = resultado.LeadTime;

                        sheet.Cells[startRow, 25].Value = resultado.LeadTimeComercial;
                    }

                    sheet.Cells.AutoFitColumns();

                    return excel.GetAsByteArray();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<string>> GetStatusProposal()
        {
            try
            {
                return await _unitOfWork.Relatorios.GetStatus();
            }
            catch (Exception e)
            {     
                throw e;
            }
        }
    }
}

