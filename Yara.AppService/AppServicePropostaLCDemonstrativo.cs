using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;
using System.Linq.Expressions;
using AutoMapper;

namespace Yara.AppService
{
    public class AppServicePropostaLCDemonstrativo : IAppServicePropostaLCDemonstrativo
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServicePropostaLCDemonstrativo(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Not Implemented

        public Task<IEnumerable<PropostaLCDemonstrativoDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PropostaLCDemonstrativoDto>> GetAllFilterAsync(Expression<Func<PropostaLCDemonstrativoDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PropostaLCDemonstrativoDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PropostaLCDemonstrativoDto obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        public async Task<PropostaLCDemonstrativoDto> GetAsync(Expression<Func<PropostaLCDemonstrativoDto, bool>> expression)
        {
            var demonstrativo = await _unitOfWork.PropostaLCDemonstrativoRepository.GetAsync(Mapper.Map<Expression<Func<PropostaLCDemonstrativo, bool>>>(expression));
            return demonstrativo.MapTo<PropostaLCDemonstrativoDto>();
        }

        public bool Insert(ref PropostaLCDemonstrativoDto obj)
        {
            try
            {
                if (obj.Tipo == "PJ")
                {
                    var potencial = this.CampoLinhaColuna(obj.Conteudo, "Balanço e DRE", 5, 19);
                    obj.Html = this.LeituraArquivoDRE(obj.Conteudo, "Tabela Auxiliar DFs");
                    obj.HtmlResumo = this.LeituraArquivoDRE(obj.Conteudo, "Tabela Auxiliar Resumo");
                    obj.HtmlRating = this.LeituraArquivoDRE(obj.Conteudo, "Tabela Auxiliar Rating");
                    obj.PotencialCredito = Convert.ToDecimal(potencial);
                    obj.Rating = this.CampoLinhaColuna(obj.Conteudo, "Tabela Auxiliar Rating", 3, 3);
                }
                else
                {
                    var conteudo = this.LeituraArquivoDRE(obj.Conteudo, "Tabela Auxiliar Rating");
                    var rating = this.CampoLinhaColuna(obj.Conteudo, "Tabela Auxiliar Rating", 3, 3);
                    obj.Html = conteudo;
                    obj.HtmlRating = conteudo;
                    obj.Rating = rating;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format("Erro ao ler o DRE {0}: {1}.", obj.Tipo, ex.Message));
            }

            var demonstrativo = obj.MapTo<PropostaLCDemonstrativo>();

            _unitOfWork.PropostaLCDemonstrativoRepository.Insert(demonstrativo);

            try
            {

                return _unitOfWork.Commit();

            }
            catch (Exception e)
            {
                throw e;
            }

        }



        private string LeituraArquivoDRE(byte[] conteudoArquivo, string nomeAba)
        {

            StringBuilder sbHtml = new StringBuilder("");


            try
            {

                MemoryStream ms = new MemoryStream(conteudoArquivo);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {

                    var xlWorksheet = xlPackage.Workbook.Worksheets[nomeAba];

                    sbHtml.Append(this.GerarHtmlResumoeDemonstrativo(xlWorksheet));

                }

                return sbHtml.ToString();

            }
            catch (Exception exDRE)
            {
                throw exDRE;
            }

        }


        private string CampoLinhaColuna(byte[] conteudoArquivo, string nomeAba, int linha, int coluna)
        {

            StringBuilder sbHtml = new StringBuilder("");


            try
            {

                MemoryStream ms = new MemoryStream(conteudoArquivo);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {

                    var xlWorksheet = xlPackage.Workbook.Worksheets[nomeAba];

                    sbHtml.Append(xlWorksheet.Cells[linha, coluna].Text);

                }

                return sbHtml.ToString();

            }
            catch (Exception exDRE)
            {
                throw exDRE;
            }

        }


        private string GerarHtmlResumoeDemonstrativo(ExcelWorksheet xlWorksheet)
        {
            StringBuilder sbHtml = new StringBuilder();
            StringBuilder sbLine = new StringBuilder();

            var colInicio = -1;
            var colFim = xlWorksheet.Dimension.End.Column;
            var iniRow = xlWorksheet.Dimension.Start.Row;
            var endRow = xlWorksheet.Dimension.End.Row;

            for (int i = xlWorksheet.Dimension.Start.Column; i <= xlWorksheet.Dimension.End.Column; i++)
            {

                if (colInicio == -1)
                {
                    if (!string.IsNullOrEmpty(xlWorksheet.Cells[iniRow, i].GetValue<string>()))
                    {
                        colInicio = i;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(xlWorksheet.Cells[iniRow, i].GetValue<string>()))
                    {
                        colFim = i - 1;
                        break;
                    }

                }

            }

            var colExibe = colFim;
            var colFormato = colFim - 1;
            colFim = colFim - 2;

            bool background = false;
            bool fontBold = false;
            bool rightAligned = false;
            bool emptyLine = true;
            bool size20 = false;
            bool removecol = false;
            sbHtml.Append("<table class=\"table table-bordered table-striped\" cellspacing=\"0\" cellpadding=\"0\"><tbody>");

            for (int row = iniRow; row <= endRow; row++)
            {

                if (row == iniRow || xlWorksheet.Cells[row, colExibe].GetValue<string>() == "X")
                {

                    sbLine.Clear();
                    emptyLine = true;

                    for (int col = colInicio; col <= colFim; col++)
                    {

                        background = !string.IsNullOrEmpty(xlWorksheet.Cells[row, col].Style.Fill.BackgroundColor.Rgb);
                        fontBold = xlWorksheet.Cells[row, col].Style.Font.Bold;
                        rightAligned = col != colInicio && !background;
                        size20 = xlWorksheet.Cells[row, col].Style.Font.Size == 20;
                        removecol = xlWorksheet.Cells[row, col].Style.Font.UnderLine;
                        
                        if (!string.IsNullOrEmpty(xlWorksheet.Cells[row, col].Text))
                            emptyLine = false;

                        if(!removecol)
                            sbLine.AppendFormat("<td class=\"{1} {2} {3} {4}\">{0}</td>", xlWorksheet.Cells[row, col].Text, background ? "blue-background" : "", fontBold ? "font-bold" : "", rightAligned ? "right-aligned" : "", size20?"rating-size":"");


                    }

                    if (!emptyLine)
                    {
                        sbHtml.Append("<tr>");
                        sbHtml.Append(sbLine.ToString());
                        sbHtml.AppendLine("</tr>");
                    }
                    else
                    {
                        sbHtml.AppendFormat("<tr><td class=\"empty-line\" colspan=\"{0}\"></td></tr>", (colFim - colInicio + 1));
                    }

                }

            }

            sbHtml.Append("</tbody></table>");

            return sbHtml.ToString();
        }


        public async Task<bool> InsertGrupoDemonstrativo(IEnumerable<PropostaLCGrupoEconomicoDto> grupos)
        {

            foreach (var grupo in grupos)
            {
               var exist = await  _unitOfWork.PropostaLcGrupoEconomico.GetAsync(c => c.PropostaLCID.Equals(grupo.PropostaLCID) &&
                                                                  c.Documento.Equals(grupo.Documento));
                if (exist!=null)
                {
                    if(grupo.PotencialCredito.HasValue)
                        exist.PotencialCredito = grupo.PotencialCredito;

                    if (grupo.PotencialPatrimonial.HasValue)
                        exist.PotencialPatrimonial = grupo.PotencialPatrimonial;

                    if (grupo.PotencialReceita.HasValue)
                        exist.PotencialReceita = grupo.PotencialReceita;

                    if (grupo.LimiteSugerido.HasValue)
                        exist.LimiteSugerido = grupo.LimiteSugerido;

                    if (grupo.VigenciaSugerida.HasValue)
                        exist.VigenciaSugerida = grupo.VigenciaSugerida;

                    if (grupo.VigenciaFimSugerida.HasValue)
                        exist.VigenciaFimSugerida = grupo.VigenciaFimSugerida;

                    if (grupo.DemonstrativoID.HasValue)
                        exist.DemonstrativoID = grupo.DemonstrativoID.Value;

                    if (!string.IsNullOrEmpty(grupo.Rating))
                        exist.Rating = grupo.Rating;

                    _unitOfWork.PropostaLcGrupoEconomico.Update(exist);
                }
                else
                {
                    var item = grupo.MapTo<PropostaLCGrupoEconomico>();

                    _unitOfWork.PropostaLcGrupoEconomico.Insert(item);
                }
               
            }

            return _unitOfWork.Commit();
        }


    }
}



