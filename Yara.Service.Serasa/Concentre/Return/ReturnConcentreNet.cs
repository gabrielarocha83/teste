using System;
using System.IO;
using Yara.Service.Serasa.Common;
using Yara.Service.Serasa.Concentre.Entities;
using Yara.Service.Serasa.Relato.Entities;

namespace Yara.Service.Serasa.Concentre.Return
{
    public class ReturnConcentreNet
    {
        private DateTime? FormatDate(string date)
        {

            try
            {
                if (date.Length != 8) return null;

                var dia = int.Parse(date.Substring(0, 2));
                var mes = int.Parse(date.Substring(2, 2));
                var ano = int.Parse(date.Substring(4, 4));

                return new DateTime(ano, mes, dia);
            }
            catch (Exception)
            {
                return null;
            }

        }

        private DateTime? FormatDateAAAAMMDD(string date)
        {

            try
            {
                if (date.Length != 8) return null;

                var ano = int.Parse(date.Substring(0, 4));
                var mes = int.Parse(date.Substring(4, 2));
                var dia = int.Parse(date.Substring(6, 2));

                return new DateTime(ano, mes, dia);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public ReturnConcentre ReturnConcentre(string retorno)
        {

            if (retorno.Length < 515)
            {
                throw new SerasaException(string.Format("Retorno do Concentre Inválido: {0}", retorno));
            }

            try
            {

                var cabecalho01 = retorno.Substring(0, 400);
                var cabecalho02 = retorno.Substring(400, 115);

                //Pega documento do cliente que retorna no cabeçalho do arquivo.
                var documentoCliente = cabecalho01.Substring(25, 1) == "F" ? cabecalho01.Substring(14, 11).Trim() : cabecalho01.Substring(11, 14).Trim();

                retorno = retorno.Replace(cabecalho01, "");
                retorno = retorno.Replace(cabecalho02, "");

                //Quebra de linhas do arquivo de retorno
                retorno = retorno.Replace("I0", Environment.NewLine + "I0");
                retorno = retorno.Replace("I1", Environment.NewLine + "I1");
                retorno = retorno.Replace("I2", Environment.NewLine + "I2");
                retorno = retorno.Replace("I5", Environment.NewLine + "I5");
                retorno = retorno.Replace("F9", Environment.NewLine + "F9");
                retorno = retorno.Replace("T999", Environment.NewLine + "T999");

                var returnConcentre = new ReturnConcentre();
                var pefinResume = new PefinResume();
                var refinResume = new RefinResume();
                var acheiResume = new ChequeAcheiResume();
                var protestosResume = new ProtestosResume();
                var acaoJudicialResume = new AcaoJudicialResume();
                var falenciaResume = new FalenciaResume();
                var pefinDetail = new PefinDetail();
                var refinDetail = new RefinDetail();
                var societaria = new ParticipacaoSocietaria();

                using (var reader = new StringReader(retorno))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        line = line.PadRight(100, ' ');

                        switch (line.Substring(0, 4))
                        {
                            case "I100": //Cabeçalho do Retorno
                                returnConcentre.NomeCliente = line.Substring(6, 70).Trim();
                                returnConcentre.DataConfirmacao = FormatDate(line.Substring(76, 8));

                                try
                                {
                                    returnConcentre.Situacao = SituacaoCadastral.CPF[line.Substring(84, 1)];
                                }
                                catch (Exception)
                                {

                                }

                                returnConcentre.DataSituacao = FormatDate(line.Substring(85, 8));
                                returnConcentre.DataCriacao = DateTime.Now;
                                returnConcentre.DocumentoCliente = documentoCliente;
                                break;

                            case "I101": //Cabeçalho do Retorno
                                returnConcentre.NomeMae = line.Substring(6, 60).Trim();
                                break;

                            case "I220": //PEFIN
                                if (line.Substring(4, 2) == "00") //Resumo de PEFIN
                                {
                                    var dtInicio = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    var dtFim = FormatDateAAAAMMDD(line.Substring(14, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));

                                    returnConcentre.Resumos.Add(new ConcentreResumo()
                                    {
                                        Quantidade = Convert.ToInt32(line.Substring(22, 9)),
                                        Valor = Convert.ToDecimal(line.Substring(31, 15)) / 100,
                                        Discriminacao = "Pendências Comerciais (PEFIN)",
                                        Praca = "",
                                        Origem = line.Substring(47, 16),
                                        Periodo = $"{dtInicio} - {dtFim}"
                                    });
                                }
                                else if (line.Substring(4, 2) == "01") //Detalhe de PEFIN 01
                                {
                                    pefinDetail = new PefinDetail
                                    {
                                        DataOcorrencia = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01), //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2)),
                                        Natureza = NaturezaOperacaoAnexoIII.NaturezaAnexoIII[line.Substring(14, 03).Trim()],
                                        ValorOcorrencia = Convert.ToDecimal(line.Substring(17, 15).Trim()) / 100,
                                        CodigoPraca = line.Substring(32, 4),
                                        Principal = line.Substring(68, 1),
                                        Contrato = line.Substring(69, 16).Trim(),
                                        SubJudice = line.Substring(85, 1),
                                        SerieCadus = line.Substring(94, 1),
                                        ChaveCadus = line.Substring(95, 1),
                                        TipoOcorrencia = line.Substring(105, 1)
                                    };
                                }
                                else if (line.Substring(4, 2) == "03") //Detalhe de PEFIN 02
                                {
                                    pefinDetail.DocumentoCredor = line.Substring(6, 14).Trim();
                                    pefinDetail.NomeCredor = line.Substring(20, 70).Trim();
                                    returnConcentre.Pefin.Add(pefinDetail);
                                }
                                break;

                            case "I140": //REFIN
                                if (line.Substring(4, 2) == "00") //Resumo de REFIN
                                {
                                    var dtInicio = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    var dtFim = FormatDateAAAAMMDD(line.Substring(14, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));

                                    returnConcentre.Resumos.Add(new ConcentreResumo()
                                    {
                                        Quantidade = Convert.ToInt32(line.Substring(22, 9)),
                                        Valor = Convert.ToDecimal(line.Substring(31, 15)) / 100,
                                        Discriminacao = "Pendências Bancárias (REFIN)",
                                        Praca = "",
                                        Origem = line.Substring(46, 16).Trim(),
                                        Periodo = $"{dtInicio} - {dtFim}"
                                    });

                                    refinResume.DataInicialRefin = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    refinResume.DataFinalRefin = FormatDateAAAAMMDD(line.Substring(14, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                    refinResume.QtdTotal = Convert.ToInt32(line.Substring(22, 9));
                                    refinResume.ValorRefin = Convert.ToDecimal(line.Substring(31, 15)) / 100;
                                    refinResume.TipoOcorrencia = line.Substring(46, 1);
                                    refinResume.NomeCredor = line.Substring(46, 16).Trim();
                                }
                                else if (line.Substring(4, 2) == "01")
                                {
                                    refinDetail = new RefinDetail();
                                    refinDetail.DataOcorrencia = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    refinDetail.Natureza = NaturezaOperacaoAnexoII.NaturezaAnexoII[line.Substring(14, 03).Trim()];
                                    refinDetail.CnpjOrigem = line.Substring(17, 15).Trim();
                                    refinDetail.ValorOcorrencia = Convert.ToDecimal(line.Substring(32, 15).Trim()) / 100;
                                    refinDetail.CodigoPraca = line.Substring(47, 4).Trim();
                                    refinDetail.Uf = line.Substring(51, 2).Trim();
                                    refinDetail.NomeEmpresa = line.Substring(53, 20).Trim();
                                    refinDetail.Cidade = line.Substring(73, 30).Trim();
                                    refinDetail.Principal = line.Substring(103, 1);
                                    refinDetail.SerieCadus = line.Substring(104, 1);
                                    refinDetail.ChaveCadus = line.Substring(103, 1);
                                    returnConcentre.Refin.Add(refinDetail);
                                }
                                else if (line.Substring(4, 2) == "02")
                                {
                                    var refinDetail2 = new RefinDetail2();
                                    refinDetail2.SubJudice = line.Substring(6, 1);
                                    refinDetail2.CnpjFilial = line.Substring(7, 4);
                                    refinDetail2.DigitoDocumento = line.Substring(11, 2);
                                    refinDetail2.DataInclusao = FormatDateAAAAMMDD(line.Substring(13, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(13, 8).Substring(0, 4) + "/" + line.Substring(13, 8).Substring(4, 2) + "/" + line.Substring(13, 8).Substring(6, 2));
                                    if (line.Substring(21, 6) != "000000")
                                    {
                                        refinDetail2.HoraInclusao = line.Substring(21, 6).Substring(0, 2) + ":" + line.Substring(21, 6).Substring(2, 2) + ":" + line.Substring(21, 6).Substring(4, 2);
                                    }

                                    refinDetail2.Contrato = line.Substring(27, 16).Trim();
                                    refinDetail2.Modalidade = line.Substring(43, 30).Trim();
                                    refinResume.RefinDetail2.Add(refinDetail2);
                                    returnConcentre.RefinDetail2.Add(refinDetail2);
                                }
                                else if (line.Substring(4, 2) == "03")
                                {
                                    var detailSubJudice = new RefinDetailSubJudice();
                                    detailSubJudice.Praca = line.Substring(6, 4).Trim();
                                    detailSubJudice.Distribuidor = Convert.ToInt32(line.Substring(10, 2));
                                    detailSubJudice.Vara = Convert.ToInt32(line.Substring(12, 2));
                                    detailSubJudice.Data = FormatDateAAAAMMDD(line.Substring(14, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                    detailSubJudice.Processo = line.Substring(22, 16).Trim();
                                    detailSubJudice.Mensagem = line.Substring(38, 76).Trim();
                                    refinResume.RefinDetailSubJudice.Add(detailSubJudice);
                                }
                                break;

                            case "I110": //PROTESTOS
                                if (line.Substring(4, 2) == "00") //Resumo de PROTESTOS
                                {
                                    var dtInicio = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    var dtFim = FormatDateAAAAMMDD(line.Substring(14, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));

                                    returnConcentre.Resumos.Add(new ConcentreResumo()
                                    {
                                        Quantidade = Convert.ToInt32(line.Substring(22, 9)),
                                        Valor = Convert.ToDecimal(line.Substring(31, 15)) / 100,
                                        Discriminacao = "Protestos",
                                        Praca = "",
                                        Origem = line.Substring(46, 16),
                                        Periodo = $"{dtInicio} - {dtFim}"
                                    });
                                    protestosResume.DataInicial = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    protestosResume.DataFinal = FormatDateAAAAMMDD(line.Substring(14, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                    protestosResume.QtdeTotal = Convert.ToInt32(line.Substring(22, 9));
                                    protestosResume.Valor = Convert.ToDecimal(line.Substring(31, 15)) / 100;
                                    protestosResume.Origem = line.Substring(46, 30).Trim();
                                }
                                else if (line.Substring(4, 2) == "01")
                                {
                                    ProtestosDetail protestosDetail = new ProtestosDetail();
                                    protestosDetail.DataOcorrencia = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    protestosDetail.NumeroCartorio = line.Substring(14, 4);
                                    protestosDetail.Natureza = line.Substring(18, 3);
                                    protestosDetail.Valor = Convert.ToDecimal(line.Substring(21, 15)) / 100;
                                    protestosDetail.Praca = line.Substring(36, 4);
                                    protestosDetail.Uf = line.Substring(40, 2);
                                    protestosDetail.Cidade = line.Substring(42, 30).Trim();
                                    protestosDetail.SubJudice = line.Substring(72, 1).Trim();
                                    if (!string.IsNullOrEmpty(line.Substring(72, 1).Trim()))
                                    {
                                        protestosDetail.DataCarta = FormatDateAAAAMMDD(line.Substring(73, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(73, 8).Substring(0, 4) + "/" + line.Substring(73, 8).Substring(4, 2) + "/" + line.Substring(73, 8).Substring(6, 2));
                                    }
                                    protestosDetail.CnpjFilial = Convert.ToInt32(line.Substring(81, 4));
                                    protestosDetail.DigitoDocumento = Convert.ToInt32(line.Substring(85, 2));
                                    protestosDetail.DataInclusao = FormatDateAAAAMMDD(line.Substring(87, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(87, 8).Substring(0, 4) + "/" + line.Substring(87, 8).Substring(4, 2) + "/" + line.Substring(87, 8).Substring(6, 2));
                                    protestosDetail.HoraInclusao = line.Substring(95, 6).Substring(0, 2) + ":" + line.Substring(95, 6).Substring(2, 2) + ":" + line.Substring(95, 6).Substring(4, 2);
                                    protestosDetail.ChaveCadus = line.Substring(101, 10).Trim();
                                    returnConcentre.Protestos.Add(protestosDetail);
                                }
                                else if (line.Substring(4, 2) == "02")
                                {
                                    var protestosSubJudice = new ProtestosSubJudice();
                                    protestosSubJudice.Praca = line.Substring(6, 4);
                                    protestosSubJudice.Distribuidor = Convert.ToInt32(line.Substring(10, 2));
                                    protestosSubJudice.Vara = Convert.ToInt32(line.Substring(12, 2));
                                    protestosSubJudice.Data = FormatDateAAAAMMDD(line.Substring(14, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                    protestosSubJudice.Processo = line.Substring(22, 16).Trim();
                                    protestosSubJudice.Mensagem = line.Substring(38, 76).Trim();
                                    protestosResume.ProtestosSubJudice.Add(protestosSubJudice);
                                }
                                break;

                            case "I120": //Ação Judicial
                                if (line.Substring(4, 2) == "00") //Resumo Ação Judicial
                                {
                                    var dtInicio = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    var dtFim = FormatDateAAAAMMDD(line.Substring(14, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));

                                    returnConcentre.Resumos.Add(new ConcentreResumo()
                                    {
                                        Quantidade = Convert.ToInt32(line.Substring(22, 9)),
                                        Valor = Convert.ToDecimal(line.Substring(31, 15)) / 100,
                                        Discriminacao = "Ações Judiciais",
                                        Praca = "",
                                        Origem = line.Substring(46, 16),
                                        Periodo = $"{dtInicio} - {dtFim}"
                                    });
                                    acaoJudicialResume.DataInicial = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    acaoJudicialResume.DataFinal = FormatDateAAAAMMDD(line.Substring(14, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                    acaoJudicialResume.Valor = Convert.ToDecimal(line.Substring(22, 6)) / 100;
                                    acaoJudicialResume.QtdeTotal = Convert.ToInt32(line.Substring(31, 15));
                                    acaoJudicialResume.Origem = line.Substring(46, 30);
                                }
                                else if (line.Substring(4, 2) == "01") //Detalhe Ação Judicial
                                {
                                    var acaoJudicialDetail = new AcaoJudicialDetail();
                                    acaoJudicialDetail.DataOcorrencia = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    acaoJudicialDetail.VaraCivil = line.Substring(14, 4);
                                    acaoJudicialDetail.NumeroDistribuidor = line.Substring(18, 4);
                                    acaoJudicialDetail.Natureza = NaturezaOperacaoAnexoI.NaturezaAnexoI[line.Substring(22, 3).Trim()];
                                    acaoJudicialDetail.Valor = Convert.ToDecimal(line.Substring(25, 15)) / 100;
                                    acaoJudicialDetail.Praca = line.Substring(40, 4);
                                    acaoJudicialDetail.Uf = line.Substring(44, 2);
                                    acaoJudicialDetail.Cidade = line.Substring(46, 30);
                                    acaoJudicialDetail.Principal = line.Substring(76, 1);
                                    acaoJudicialDetail.SubJudice = line.Substring(77, 1);
                                    acaoJudicialDetail.CnpjFilial = Convert.ToInt32(line.Substring(78, 4));
                                    acaoJudicialDetail.DigitoDocumento = Convert.ToInt32(line.Substring(82, 2));
                                    acaoJudicialDetail.DataInclusao = FormatDateAAAAMMDD(line.Substring(84, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(84, 8).Substring(0, 4) + "/" + line.Substring(84, 8).Substring(4, 2) + "/" + line.Substring(84, 8).Substring(6, 2));
                                    acaoJudicialDetail.HoraInclusao = line.Substring(92, 6).Substring(0, 2) + ":" + line.Substring(92, 6).Substring(2, 2) + ":" + line.Substring(92, 6).Substring(4, 2);
                                    acaoJudicialDetail.ChaveCadus = line.Substring(98, 10);
                                    returnConcentre.Acoes.Add(acaoJudicialDetail);
                                }
                                else if (line.Substring(4, 2) == "02") //Detalhe Ação Judicial
                                {
                                    var acaoJudicialDetail2 = new AcaoJudicialDetail2();
                                    acaoJudicialDetail2.Descricao = line.Substring(6, 30).Trim();
                                    acaoJudicialResume.AcaoJudicialDetail2.Add(acaoJudicialDetail2);
                                }
                                else if (line.Substring(4, 2) == "03")
                                {
                                    var acaoJudicialSubJudice = new AcaoJudicialSubJudice();
                                    acaoJudicialSubJudice.Praca = line.Substring(6, 4);
                                    acaoJudicialSubJudice.Distribuidor = Convert.ToInt32(line.Substring(10, 2));
                                    acaoJudicialSubJudice.Vara = line.Substring(12, 2);
                                    acaoJudicialSubJudice.Data = FormatDateAAAAMMDD(line.Substring(14, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                    acaoJudicialSubJudice.Processo = line.Substring(22, 16).Trim();
                                    acaoJudicialSubJudice.Mensagem = line.Substring(38, 76).Trim();
                                    acaoJudicialResume.AcaoJudicialSubJudice.Add(acaoJudicialSubJudice);
                                }
                                break;

                            case "I160": //ACHEI + CCF
                                if (line.Substring(4, 2) == "00") //Resumo de ACHEI + CCF
                                {
                                    var dtInicio = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    var dtFim = FormatDateAAAAMMDD(line.Substring(14, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));

                                    returnConcentre.Resumos.Add(new ConcentreResumo()
                                    {
                                        Quantidade = Convert.ToInt32(line.Substring(37, 9)),
                                        Valor = Convert.ToDecimal(line.Substring(31, 15)) / 100,
                                        Discriminacao = "Cheque sem Fundos",
                                        Praca = "",
                                        Origem = line.Substring(46, 16),
                                        Periodo = $"{dtInicio} - {dtFim}"
                                    });
                                    acheiResume.DataInicialCheque = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    acheiResume.DataFinalCheque = FormatDateAAAAMMDD(line.Substring(14, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                    acheiResume.Valor = Convert.ToDecimal(line.Substring(22, 15)) / 100;
                                    acheiResume.QtdeTotal = Convert.ToInt32(line.Substring(37, 9));
                                    acheiResume.Origem = line.Substring(46, 16);
                                }
                                else if (line.Substring(4, 2) == "01")
                                {
                                    var acheiDetail = new ChequeAcheiDetail();
                                    acheiDetail.DataOcorrencia = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    acheiDetail.Banco = Convert.ToInt32(line.Substring(14, 3));
                                    acheiDetail.Agencia = Convert.ToInt32(line.Substring(17, 4));
                                    acheiDetail.ContaCorrente = Convert.ToInt32(line.Substring(21, 9));
                                    acheiDetail.Natureza = NaturezaOperacaoAnexoI.NaturezaAnexoI[line.Substring(30, 02).Trim()];
                                    acheiDetail.Valor = Convert.ToDecimal(line.Substring(32, 15)) / 100;
                                    acheiDetail.Praca = line.Substring(47, 4);
                                    acheiDetail.Uf = line.Substring(51, 2);
                                    acheiDetail.NomeBanco = line.Substring(53, 20).Trim();
                                    acheiDetail.NumeroCheque = line.Substring(73, 10).Trim();
                                    acheiDetail.Cidade = line.Substring(83, 30).Trim();

                                    returnConcentre.ChequesAchei.Add(acheiDetail);
                                }
                                else if (line.Substring(4, 2) == "02")
                                {
                                    var acheiDetail2 = new ChequeAcheiDetail2
                                    {
                                        CnpjFilial = Convert.ToInt32(line.Substring(6, 4)),
                                        DigitoDocumento = Convert.ToInt32(line.Substring(10, 2)),
                                        DataInclusao = FormatDateAAAAMMDD(line.Substring(12, 8)) ?? new DateTime(1901, 01, 01),
                                        //Convert.ToDateTime(line.Substring(12, 8).Substring(0, 4) + "/" +
                                        //                   line.Substring(12, 8).Substring(4, 2) + "/" + line
                                        //                       .Substring(12, 8).Substring(6, 2)),
                                        HoraInclusao =
                                            Convert.ToDateTime(line.Substring(20, 6).Substring(0, 2) + ":" +
                                                                line.Substring(20, 6).Substring(2, 2) + ":" + line
                                                                    .Substring(20, 6).Substring(4, 2)),
                                        ChaveCadus = line.Substring(26, 10).Trim()
                                    };

                                    acheiResume.ChequeAcheiDetail2.Add(acheiDetail2);
                                }
                                break;

                            case "I170": //ACHEI - Cheque Sem Fundos Detalhes
                                if (line.Substring(4, 2) == "01") //Detalhe de Cheque sem Fundos
                                {
                                    var chequeSemFundoDetail = new ChequeSemFundoDetail();
                                    chequeSemFundoDetail.DataOcorrencia = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); // Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    chequeSemFundoDetail.Banco = Convert.ToInt32(line.Substring(14, 3));
                                    chequeSemFundoDetail.Agencia = Convert.ToInt32(line.Substring(17, 4));
                                    chequeSemFundoDetail.QtdeCheque = Convert.ToInt32(line.Substring(21, 9));
                                    chequeSemFundoDetail.Praca = line.Substring(30, 4);
                                    chequeSemFundoDetail.Uf = line.Substring(34, 2);
                                    chequeSemFundoDetail.NomeBanco = line.Substring(36, 20);
                                    chequeSemFundoDetail.NomeCidade = line.Substring(56, 30);
                                    chequeSemFundoDetail.Natureza = line.Substring(86, 2);
                                    chequeSemFundoDetail.CnpjFilial = Convert.ToInt32(line.Substring(89, 4));
                                    chequeSemFundoDetail.DigitoDocumento = Convert.ToInt32(line.Substring(92, 2));
                                    chequeSemFundoDetail.DataInclusao = FormatDateAAAAMMDD(line.Substring(94, 8)) ?? new DateTime(1901, 01, 01); // Convert.ToDateTime(line.Substring(94, 8).Substring(0, 4) + "/" + line.Substring(94, 8).Substring(4, 2) + "/" + line.Substring(94, 8).Substring(6, 2));
                                    chequeSemFundoDetail.HoraInclusao = Convert.ToDateTime(line.Substring(102, 6).Substring(0, 2) + ":" + line.Substring(102, 6).Substring(2, 2) + ":" + line.Substring(102, 6).Substring(4, 2));
                                    returnConcentre.ChequesSemFundos.Add(chequeSemFundoDetail);
                                }
                                else if (line.Substring(4, 2) == "02")
                                {
                                    var chequeSemFundoDetail2 = new ChequeSemFundoDetail2();
                                    chequeSemFundoDetail2.ChaveCadus = line.Substring(6, 10).Trim();
                                    acheiResume.ChequeSemFundoDetail2.Add(chequeSemFundoDetail2);
                                }
                                break;

                            case "I130": //FALÊNCIAS/CONCORDATAS
                                if (line.Substring(4, 2) == "00") //Resumo de FALÊNCIAS/CONCORDATAS
                                {
                                    var dtInicio = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    var dtFim = FormatDateAAAAMMDD(line.Substring(14, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));

                                    returnConcentre.Resumos.Add(new ConcentreResumo()
                                    {
                                        Quantidade = Convert.ToInt32(line.Substring(22, 9)),
                                        Valor = Convert.ToDecimal(line.Substring(31, 15)) / 100,
                                        Discriminacao = "Falência/Concordata/Recuperação Judicial",
                                        Praca = "",
                                        Origem = line.Substring(46, 30),
                                        Periodo = $"{dtInicio} - {dtFim}"
                                    });

                                    falenciaResume.DataInicial = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    falenciaResume.DataFinal = FormatDateAAAAMMDD(line.Substring(14, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                    falenciaResume.QtdeTotal = Convert.ToInt32(line.Substring(22, 9));
                                    falenciaResume.Valor = Convert.ToDecimal(line.Substring(31, 15)) / 100;
                                    falenciaResume.Origem = line.Substring(46, 30);
                                }
                                else if (line.Substring(4, 2) == "01")
                                {
                                    var falenciaDetail = new FalenciaDetail();
                                    falenciaDetail.DataOcorrencia = FormatDateAAAAMMDD(line.Substring(6, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                    falenciaDetail.Natureza = NaturezaOperacaoAnexoI.NaturezaAnexoI[line.Substring(14, 2).Trim()];
                                    falenciaDetail.VaraCivil = line.Substring(16, 4);
                                    falenciaDetail.Praca = line.Substring(20, 4);
                                    falenciaDetail.Uf = line.Substring(24, 2);
                                    falenciaDetail.Cidade = line.Substring(26, 30);
                                    falenciaDetail.CnpjFilial = Convert.ToInt32(line.Substring(56, 4));
                                    falenciaDetail.DigitoDocumento = Convert.ToInt32(line.Substring(60, 2));
                                    falenciaDetail.DataInclusao = FormatDateAAAAMMDD(line.Substring(62, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(62, 8).Substring(0, 4) + "/" + line.Substring(62, 8).Substring(4, 2) + "/" + line.Substring(62, 8).Substring(6, 2));
                                    falenciaDetail.HoraInclusao = line.Substring(70, 6).Substring(0, 2) + ":" + line.Substring(70, 6).Substring(2, 2) + ":" + line.Substring(70, 6).Substring(4, 2);
                                    falenciaDetail.ChaveCadus = line.Substring(76, 10);
                                    falenciaDetail.DescricaoNatureza = line.Substring(86, 28);
                                    returnConcentre.Falencia.Add(falenciaDetail);
                                }
                                break;

                            case "I520": //PARTICIPAÇÃO SOCIETÁRIA
                                if (societaria == null) societaria = new ParticipacaoSocietaria();
                                if (line.Substring(4, 2) == "00") //Resumo de PARTICIPAÇÃO SOCIETÁRIA
                                {
                                    societaria.NomeEmpresa = line.Substring(6, 70).Trim();
                                    societaria.CnpjEmpresa = line.Substring(76, 14);
                                    societaria.NivelParticipacao = Convert.ToDecimal(line.Substring(90, 4)) / 10;
                                    societaria.DataDesde = FormatDateAAAAMMDD(line.Substring(94, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(94, 8).Substring(0, 4) + "/" + line.Substring(94, 8).Substring(4, 2) + "/" + line.Substring(94, 8).Substring(6, 2));
                                    societaria.DataAtual = FormatDateAAAAMMDD(line.Substring(102, 8)) ?? new DateTime(1901, 01, 01); //Convert.ToDateTime(line.Substring(102, 8).Substring(0, 4) + "/" + line.Substring(102, 8).Substring(4, 2) + "/" + line.Substring(102, 8).Substring(6, 2));
                                    societaria.Uf = line.Substring(110, 2);
                                    societaria.RestricaoParticipante = line.Substring(112, 1) == "S" ? true : false;
                                }
                                else if (line.Substring(4, 2) == "99")
                                {
                                    societaria.Mensagem = line.Substring(6, 80).Trim();
                                }
                                returnConcentre.ParticipacaoSocietarias.Add(societaria);
                                societaria = null;
                                break;
                        }
                    }

                    // Correção dos Totais de acordo com a tabela de Resumos
                    // -- PEFIN
                    if (returnConcentre.Pefin.Count > 0)
                        returnConcentre.Pefin.ForEach(c => c.TotalOcorrencia = returnConcentre.Resumos.Find(d => d.Discriminacao.Equals("Pendências Comerciais (PEFIN)")).Quantidade.ToString());
                    // -- REFIN
                    if (returnConcentre.Refin.Count > 0)
                        returnConcentre.Refin.ForEach(c => c.TotalOcorrencia = returnConcentre.Resumos.Find(d => d.Discriminacao.Equals("Pendências Bancárias (REFIN)")).Quantidade.ToString());
                    // -- Cheque sem fundos
                    if (returnConcentre.ChequesSemFundos.Count > 0)
                        returnConcentre.ChequesSemFundos.ForEach(c => c.TotalOcorrencia = returnConcentre.Resumos.Find(d => d.Discriminacao.Equals("Cheque sem Fundos")).Quantidade.ToString());
                    // -- Protestos
                    if (returnConcentre.Protestos.Count > 0)
                        returnConcentre.Protestos.ForEach(c => c.TotalOcorrencia = returnConcentre.Resumos.Find(d => d.Discriminacao.Equals("Protestos")).Quantidade.ToString());
                    // -- Ações Judiciais
                    if (returnConcentre.Acoes.Count > 0)
                        returnConcentre.Acoes.ForEach(c => c.TotalOcorrencia = returnConcentre.Resumos.Find(d => d.Discriminacao.Equals("Ações Judiciais")).Quantidade.ToString());
                    // -- Participação em Falências
                    if (returnConcentre.Falencia.Count > 0)
                        returnConcentre.Falencia.ForEach(c => c.TotalOcorrencia = returnConcentre.Resumos.Find(d => d.Discriminacao.Equals("Falência/Concordata/Recuperação Judicial")).Quantidade.ToString());
                }

                return returnConcentre;
            }
            catch (Exception sex)
            {
                throw new SerasaException(string.Format("Retorno do Concentre Inválido: {0}", sex.Message));
            }

        }
    }
}
