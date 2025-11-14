using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using Yara.AppService;
using Yara.AppService.Dtos;
using Yara.AppService.Mappings;
using Yara.Domain.Repository;
using Yara.Service.Serasa.Common;
using Yara.Service.Serasa.Concentre.Entities;
using Yara.Data.Entity.Context;
using Yara.Data.Entity.Repository;

namespace Yara.UnitTests
{
    public class HeaderConcentreTest
    {
        //private readonly HeaderSerasaConcentre _concentre = new HeaderSerasaConcentre("00000111260");
        //[Test]
        //[TestCase("00000111260")]
        //public async Task HeaderConcentreNet(string codDocumento)
        //{

        //    //var ret = _concentre.HeaderConcentre(codDocumento);
        //    //var retorno = await GetServiceString(ret);
        //    //ReturnConcentreNet(retorno);
        //}

        private IUnitOfWork _unitOfWork;
        public HeaderConcentreTest()
        {
            var context = new YaraDataContext();
            _unitOfWork = new UnitOfWork(context);

            Mapper.Initialize(x =>
            {
                x.AddProfile(new MappingProfile());
            });
        }

        //[Test]
        //public async Task SerasaSearch()
        //{
        //    var serasa = new AppServiceSerasa(_unitOfWork);
        //    var solicitante = new SolicitanteSerasaDto();

        //    solicitante.ContaClienteID = new Guid("1e73e42e-b0cf-4a9e-97cc-000103114a19");
        //    solicitante.ContaCliente.Documento = "02673754000138";
        //    solicitante.TipoClienteID = new Guid("532a7df0-04ce-4799-89da-162fb4c14705");
        //    solicitante.DataCriacao = DateTime.Now;
        //    solicitante.TipoSerasa = TipoSerasaDto.Concentre;
        //    solicitante.ID = Guid.NewGuid();
        //    try
        //    {
        //        var retorno = await serasa.ConsultarSerasa(solicitante);
        //        Assert.IsTrue(retorno);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        public void ReturnConcentreNet(string retorno)
        {
            var cabecalho01 = retorno.Substring(0, 400);
            var cabecalho02 = retorno.Substring(400, 115);
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
            
            using (var reader = new StringReader(retorno))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    switch (line.Substring(0, 4))
                    {
                        case "I100": //Cabeçalho do Retorno
                            returnConcentre.NomeCliente = line.Substring(6, 70);
                            returnConcentre.DataConfirmacao = Convert.ToDateTime(line.Substring(76, 8));
                            returnConcentre.Situacao = line.Substring(84, 1);
                            returnConcentre.DataSituacao = Convert.ToDateTime(line.Substring(85, 8));
                            break;

                        case "I220": //PEFIN
                            if (line.Substring(4, 2) == "00") //Resumo de PEFIN
                            {
                                pefinResume.DataInicialPefin = Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                pefinResume.DataFinalPefin = Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                pefinResume.QtdTotal = Convert.ToInt32(line.Substring(22, 9));
                                pefinResume.ValorPefin = Convert.ToDecimal(line.Substring(31, 15));
                                pefinResume.TipoOcorrencia = line.Substring(46, 1);
                                pefinResume.NomeCredor = line.Substring(47, 16);
                            }
                            else if (line.Substring(4, 2) == "01") //Detalhe de PEFIN 01
                            {
                                var pefinDetail = new PefinDetail();
                                pefinDetail.DataOcorrencia = Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                pefinDetail.Natureza = NaturezaOperacaoAnexoIII.NaturezaAnexoIII[line.Substring(14, 03).Trim()];
                                pefinDetail.ValorOcorrencia = Convert.ToDecimal(line.Substring(17, 15).Trim());
                                pefinDetail.CodigoPraca = line.Substring(32, 4);
                                pefinDetail.Principal = line.Substring(68, 1);
                                pefinDetail.Contrato = line.Substring(69, 16).Trim();
                                pefinDetail.SubJudice = line.Substring(85, 1);
                                pefinDetail.SerieCadus = line.Substring(94, 1);
                                pefinDetail.ChaveCadus = line.Substring(95, 1);
                                pefinDetail.TipoOcorrencia = line.Substring(105, 1);
                                pefinResume.PefinDetail.Add(pefinDetail);
                            }
                            else if (line.Substring(4, 2) == "03") //Detalhe de PEFIN 02
                            {
                                var pefinDetalhe2 = new PefinDetail2();
                                pefinDetalhe2.DocumentoCredor = line.Substring(6, 14).Trim();
                                pefinDetalhe2.NomeCredor = line.Substring(20, 70).Trim();
                                pefinResume.PefinDetail2.Add(pefinDetalhe2);
                            }
                            break;

                        case "I140": //REFIN
                            if (line.Substring(4, 2) == "00") //Resumo de REFIN
                            {
                                refinResume.DataInicialRefin = Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                refinResume.DataFinalRefin = Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                refinResume.QtdTotal = Convert.ToInt32(line.Substring(22, 9));
                                refinResume.ValorRefin = Convert.ToDecimal(line.Substring(31, 15));
                                refinResume.TipoOcorrencia = line.Substring(46, 1);
                                refinResume.NomeCredor = line.Substring(47, 16);
                            }
                            else if (line.Substring(4, 2) == "01")
                            {
                                var refinDetail = new RefinDetail();
                                refinDetail.DataOcorrencia = Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                refinDetail.Natureza = NaturezaOperacaoAnexoII.NaturezaAnexoII[line.Substring(14, 03).Trim()];
                                refinDetail.CnpjOrigem = line.Substring(17, 15).Trim();
                                refinDetail.ValorOcorrencia = Convert.ToDecimal(line.Substring(32, 15).Trim());
                                refinDetail.CodigoPraca = line.Substring(47, 4).Trim();
                                refinDetail.Uf = line.Substring(51, 2).Trim();
                                refinDetail.NomeEmpresa = line.Substring(53, 20).Trim();
                                refinDetail.Cidade = line.Substring(73, 30).Trim();
                                refinDetail.Principal = line.Substring(103, 1);
                                refinDetail.SerieCadus = line.Substring(104, 1);
                                refinDetail.ChaveCadus = line.Substring(103, 1);
                                refinResume.RefinDetail.Add(refinDetail);
                            }
                            else if (line.Substring(4, 2) == "02")
                            {
                                var refinDetail2 = new RefinDetail2();
                                refinDetail2.SubJudice = line.Substring(6, 1);
                                refinDetail2.CnpjFilial = line.Substring(7, 4);
                                refinDetail2.DigitoDocumento = line.Substring(11, 2);
                                refinDetail2.DataInclusao = Convert.ToDateTime(line.Substring(13, 8).Substring(0, 4) + "/" + line.Substring(13, 8).Substring(4, 2) + "/" + line.Substring(13, 8).Substring(6, 2));
                                if (line.Substring(21, 6) != "000000")
                                {
                                    refinDetail2.HoraInclusao = line.Substring(21, 6).Substring(0, 2) + ":" + line.Substring(21, 6).Substring(2, 2) + ":" + line.Substring(21, 6).Substring(4, 2);
                                }

                                refinDetail2.Contrato = line.Substring(27, 16).Trim();
                                refinDetail2.Modalidade = line.Substring(43, 30).Trim();
                                refinResume.RefinDetail2.Add(refinDetail2);
                            }
                            else if (line.Substring(4, 2) == "03")
                            {
                                var detailSubJudice = new RefinDetailSubJudice();
                                detailSubJudice.Praca = line.Substring(6, 4).Trim();
                                detailSubJudice.Distribuidor = Convert.ToInt32(line.Substring(10, 2));
                                detailSubJudice.Vara = Convert.ToInt32(line.Substring(12, 2));
                                detailSubJudice.Data = Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                detailSubJudice.Processo = line.Substring(22, 16).Trim();
                                detailSubJudice.Mensagem = line.Substring(38, 76).Trim();
                                refinResume.RefinDetailSubJudice.Add(detailSubJudice);
                            }
                            break;

                        case "I110": //PROTESTOS
                            if (line.Substring(4, 2) == "00") //Resumo de PROTESTOS
                            {
                                protestosResume.DataInicial = Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                protestosResume.DataFinal = Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                protestosResume.QtdeTotal = Convert.ToInt32(line.Substring(22, 9));
                                protestosResume.Valor = Convert.ToDecimal(line.Substring(31, 15));
                                protestosResume.Origem = line.Substring(46, 30).Trim();
                            }
                            else if (line.Substring(4, 2) == "01")
                            {
                                ProtestosDetail protestosDetail = new ProtestosDetail();
                                protestosDetail.DataOcorrencia = Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                protestosDetail.NumeroCartorio = line.Substring(14, 4);
                                protestosDetail.Natureza = line.Substring(18, 3);
                                protestosDetail.Valor = Convert.ToDecimal(line.Substring(21, 15));
                                protestosDetail.Praca = line.Substring(36, 4);
                                protestosDetail.Uf = line.Substring(40, 2);
                                protestosDetail.Cidade = line.Substring(42, 30).Trim();
                                protestosDetail.SubJudice = line.Substring(72, 1).Trim();
                                if (!string.IsNullOrEmpty(line.Substring(72, 1).Trim()))
                                {
                                    protestosDetail.DataCarta = Convert.ToDateTime(line.Substring(73, 8).Substring(0, 4) + "/" + line.Substring(73, 8).Substring(4, 2) + "/" + line.Substring(73, 8).Substring(6, 2));
                                }
                                protestosDetail.CnpjFilial = Convert.ToInt32(line.Substring(81, 4));
                                protestosDetail.DigitoDocumento = Convert.ToInt32(line.Substring(85, 2));
                                protestosDetail.DataInclusao = Convert.ToDateTime(line.Substring(87, 8).Substring(0, 4) + "/" + line.Substring(87, 8).Substring(4, 2) + "/" + line.Substring(87, 8).Substring(6, 2));
                                protestosDetail.HoraInclusao = line.Substring(95, 6).Substring(0, 2) + ":" + line.Substring(95, 6).Substring(2, 2) + ":" + line.Substring(95, 6).Substring(4, 2);
                                protestosDetail.ChaveCadus = line.Substring(101, 10).Trim();
                                protestosResume.ProtestosDetail.Add(protestosDetail);
                            }
                            else if (line.Substring(4, 2) == "02")
                            {
                                ProtestosSubJudice protestosSubJudice = new ProtestosSubJudice();
                                protestosSubJudice.Praca = line.Substring(6, 4);
                                protestosSubJudice.Distribuidor = Convert.ToInt32(line.Substring(10, 2));
                                protestosSubJudice.Vara = Convert.ToInt32(line.Substring(12, 2));
                                protestosSubJudice.Data = Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                protestosSubJudice.Processo = line.Substring(22, 16).Trim();
                                protestosSubJudice.Mensagem = line.Substring(38, 76).Trim();
                                protestosResume.ProtestosSubJudice.Add(protestosSubJudice);
                            }
                            break;

                        case "I120": //Ação Judicial
                            if (line.Substring(4, 2) == "00") //Resumo Ação Judicial
                            {
                                acaoJudicialResume.DataInicial = Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                acaoJudicialResume.DataFinal = Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                acaoJudicialResume.Valor = Convert.ToDecimal(line.Substring(22, 6));
                                acaoJudicialResume.QtdeTotal = Convert.ToInt32(line.Substring(31, 15));
                                acaoJudicialResume.Origem = line.Substring(46, 30);
                            }
                            else if (line.Substring(4, 2) == "01") //Detalhe Ação Judicial
                            {
                                var acaoJudicialDetail = new AcaoJudicialDetail();
                                acaoJudicialDetail.DataOcorrencia = Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                acaoJudicialDetail.VaraCivil = line.Substring(14, 4);
                                acaoJudicialDetail.NumeroDistribuidor = line.Substring(18, 4);
                                acaoJudicialDetail.Natureza = NaturezaOperacaoAnexoI.NaturezaAnexoI[line.Substring(22, 3).Trim()];
                                acaoJudicialDetail.Valor = Convert.ToDecimal(line.Substring(25, 15));
                                acaoJudicialDetail.Praca = line.Substring(40, 4);
                                acaoJudicialDetail.Uf = line.Substring(44, 2);
                                acaoJudicialDetail.Cidade = line.Substring(46, 30);
                                acaoJudicialDetail.Principal = line.Substring(76, 1);
                                acaoJudicialDetail.SubJudice = line.Substring(77, 1);
                                acaoJudicialDetail.CnpjFilial = Convert.ToInt32(line.Substring(78, 4));
                                acaoJudicialDetail.DigitoDocumento = Convert.ToInt32(line.Substring(82, 2));
                                acaoJudicialDetail.DataInclusao = Convert.ToDateTime(line.Substring(84, 8).Substring(0, 4) + "/" + line.Substring(84, 8).Substring(4, 2) + "/" + line.Substring(84, 8).Substring(6, 2));
                                acaoJudicialDetail.HoraInclusao = line.Substring(92, 6).Substring(0, 2) + ":" + line.Substring(92, 6).Substring(2, 2) + ":" + line.Substring(92, 6).Substring(4, 2);
                                acaoJudicialDetail.ChaveCadus = line.Substring(98, 10);
                                acaoJudicialResume.AcaoJudicialDetail.Add(acaoJudicialDetail);
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
                                acaoJudicialSubJudice.Data = Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                acaoJudicialSubJudice.Processo = line.Substring(22, 16).Trim();
                                acaoJudicialSubJudice.Mensagem = line.Substring(38, 76).Trim();
                                acaoJudicialResume.AcaoJudicialSubJudice.Add(acaoJudicialSubJudice);
                            }
                            break;

                        case "I160": //ACHEI + CCF
                            if (line.Substring(4, 2) == "00") //Resumo de ACHEI + CCF
                            {
                                acheiResume.DataInicialCheque = Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                acheiResume.DataFinalCheque = Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                acheiResume.Valor = Convert.ToDecimal(line.Substring(22, 15));
                                acheiResume.QtdeTotal = Convert.ToInt32(line.Substring(37, 9));
                                acheiResume.Origem = line.Substring(46, 16);
                            }
                            else if (line.Substring(4, 2) == "01")
                            {
                                var acheiDetail = new ChequeAcheiDetail();
                                acheiDetail.DataOcorrencia = Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                acheiDetail.Banco = Convert.ToInt32(line.Substring(14, 3));
                                acheiDetail.Agencia = Convert.ToInt32(line.Substring(17, 4));
                                acheiDetail.ContaCorrente = Convert.ToInt32(line.Substring(21, 9));
                                acheiDetail.Natureza = NaturezaOperacaoAnexoI.NaturezaAnexoI[line.Substring(30, 02).Trim()];
                                acheiDetail.Valor = Convert.ToDecimal(line.Substring(32, 15));
                                acheiDetail.Praca = line.Substring(47, 4);
                                acheiDetail.Uf = line.Substring(51, 2);
                                acheiDetail.NomeBanco = line.Substring(53, 20).Trim();
                                acheiDetail.NumeroCheque = line.Substring(73, 10).Trim();
                                acheiDetail.Cidade = line.Substring(83, 30).Trim();
                                acheiResume.ChequeAcheiDetail.Add(acheiDetail);
                            }
                            else if (line.Substring(4, 2) == "02")
                            {
                                var acheiDetail2 = new ChequeAcheiDetail2();
                                acheiDetail2.CnpjFilial = Convert.ToInt32(line.Substring(6, 4));
                                acheiDetail2.DigitoDocumento = Convert.ToInt32(line.Substring(10, 2));
                                acheiDetail2.DataInclusao = Convert.ToDateTime(line.Substring(12, 8).Substring(0, 4) + "/" + line.Substring(12, 8).Substring(4, 2) + "/" + line.Substring(12, 8).Substring(6, 2));
                                acheiDetail2.HoraInclusao = Convert.ToDateTime(line.Substring(20, 6).Substring(0, 2) + ":" + line.Substring(20, 6).Substring(4, 2) + ":" + line.Substring(20, 6).Substring(6, 2));
                                acheiDetail2.ChaveCadus = line.Substring(26, 10).Trim();
                                acheiResume.ChequeAcheiDetail2.Add(acheiDetail2);
                            }
                            break;

                        case "I170": //ACHEI - Cheque Sem Fundos Detalhes
                            if (line.Substring(4, 2) == "01") //Detalhe de Cheque sem Fundos
                            {
                                var chequeSemFundoDetail = new ChequeSemFundoDetail();
                                chequeSemFundoDetail.DataOcorrencia = Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
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
                                chequeSemFundoDetail.DataInclusao = Convert.ToDateTime(line.Substring(94, 8).Substring(0, 4) + "/" + line.Substring(94, 8).Substring(4, 2) + "/" + line.Substring(94, 8).Substring(6, 2));
                                chequeSemFundoDetail.HoraInclusao = Convert.ToDateTime(line.Substring(102, 6).Substring(0, 2) + ":" + line.Substring(102, 6).Substring(2, 2) + ":" + line.Substring(102, 6).Substring(4, 2));
                                acheiResume.ChequeSemFundoDetail.Add(chequeSemFundoDetail);
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
                                falenciaResume.DataInicial = Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                falenciaResume.DataFinal = Convert.ToDateTime(line.Substring(14, 8).Substring(0, 4) + "/" + line.Substring(14, 8).Substring(4, 2) + "/" + line.Substring(14, 8).Substring(6, 2));
                                falenciaResume.QtdeTotal = Convert.ToInt32(line.Substring(22, 9));
                                falenciaResume.Valor = Convert.ToDecimal(line.Substring(31, 15));
                                falenciaResume.Origem = line.Substring(46, 30);
                            }
                            else if (line.Substring(4, 2) == "01")
                            {
                                var falenciaDetail = new FalenciaDetail();
                                falenciaDetail.DataOcorrencia = Convert.ToDateTime(line.Substring(6, 8).Substring(0, 4) + "/" + line.Substring(6, 8).Substring(4, 2) + "/" + line.Substring(6, 8).Substring(6, 2));
                                falenciaDetail.Natureza = NaturezaOperacaoAnexoI.NaturezaAnexoI[line.Substring(14, 2).Trim()];
                                falenciaDetail.VaraCivil = line.Substring(16, 4);
                                falenciaDetail.Praca = line.Substring(20, 4);
                                falenciaDetail.Uf = line.Substring(24, 2);
                                falenciaDetail.Cidade = line.Substring(26, 30);
                                falenciaDetail.CnpjFilial = Convert.ToInt32(line.Substring(56, 4));
                                falenciaDetail.DigitoDocumento = Convert.ToInt32(line.Substring(60, 2));
                                falenciaDetail.DataInclusao = Convert.ToDateTime(line.Substring(62, 8).Substring(0, 4) + "/" + line.Substring(62, 8).Substring(4, 2) + "/" + line.Substring(62, 8).Substring(6, 2));
                                falenciaDetail.HoraInclusao = line.Substring(70, 6).Substring(0, 2) + ":" + line.Substring(70, 6).Substring(2, 2) + ":" + line.Substring(70, 6).Substring(4, 2);
                                falenciaDetail.ChaveCadus = line.Substring(76, 10);
                                falenciaDetail.DescricaoNatureza = line.Substring(86, 28);
                                falenciaResume.FalenciaDetail.Add(falenciaDetail);
                            }
                            break;
                    }
                }
            }

            //returnConcentre.PerfinResume = pefinResume;
            //returnConcentre.RefinResume = refinResume;
            //returnConcentre.ProtestosResume = protestosResume;
            //returnConcentre.AcaoJudicialResume = acaoJudicialResume;
            //returnConcentre.ChequeAcheiResume = acheiResume;
            //returnConcentre.FalenciaResume = falenciaResume;
            //returnConcentre.ChequeAcheiResume = acheiResume;

            var teste = returnConcentre;
        }
    }
}
