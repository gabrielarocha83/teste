using System;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using Yara.AppService;
using Yara.AppService.Dtos;
using Yara.AppService.Mappings;
using Yara.Data.Entity.Context;
using Yara.Data.Entity.Repository;
using Yara.Domain.Repository;
using Yara.Service.Serasa.Concentre.Return;

namespace Yara.Data.Tests.AppService
{
    public class SerasaTest
    {
        private readonly IUnitOfWork _unitOfWork;

        public SerasaTest()
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
        //    solicitante.TipoSerasa = TipoSerasaDto.Relato;
        //    solicitante.ID = Guid.NewGuid();
        //    try
        //    {
        //        var retorno = await serasa.ConsultarSerasa(solicitante);
        //        //Assert.IsNaN(retorno);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        [Test]
        public void SerasaConcentre()
        {
            var serasa = new AppServiceSerasa(_unitOfWork);
            var solicitante = new SolicitanteSerasaDto();

            solicitante.ContaClienteID = new Guid("6D21BAC0-1624-4EBF-9810-0017BBBACDC8");
            solicitante.ContaCliente.Documento = "48655392049";
            solicitante.TipoClienteID = new Guid("B329A083-ECBF-4721-B7C1-84688D2EF739");
            solicitante.DataCriacao = DateTime.Now;
            solicitante.TipoSerasa = TipoSerasaDto.Concentre;
            solicitante.ID = Guid.NewGuid();

            try
            {
                //var retorno = await serasa.ConsultarSerasa(solicitante, "https://mqlinuxext.serasa.com.br/Homologa/consultahttps?p=", "61898304", "10203040");
                //var retorno = await serasa.ConsultarSerasa(solicitante, "https://sitenet43.serasa.com.br/Prod/consultahttps?p=", "86436704", "Y@raGo17");

                //var concentreRetorno = "B49C      000016291115034FC     FI0000200            S99SFIMAN                            SS N                                                                         000000000               00  2017122613564300000069    0070                                                                        0000                    3#                                                                             P002RSPU                                                                                                           I00100DS                           S                                                                               I10000BELMIRO CATELAN                                                       0408195320310201700N                   I10100ADELAIDE JOANNA BEDDIN CATELAN                                                                               I10501EXISTEM 2 VARIACOES DE GRAFIAS PARA O DOCUMENTO CONSULTADO                                                   I10502BELMIRO CATELAM                                                                                              I10503BELMIRO CATELAN                                                                                              I160002017033020170717000000000000000000000004B DO BRASIL     000000012062000                                      I220002015091720171101000000008000000000803219VGERDAU          000000044289126                                     I140002017090120171201000000002000000000030311B DO BRASIL     000000004738441                                      I120002015102920171011000000003000001404951597MARACANAU                     000001528620905                        I110002015011320171127000000015000000007000000BARREIRAS                     000000040444492                        I17001201707170012097000000002GMS RSB DO BRASIL         GUARANI DAS MISSOES             00000020170725070846       I170020125088050                                                                                                   I17001201707050410680000000002GMS RSBANRISUL            GUARANI DAS MISSOES             00000020171205061907       I170020126375263                                                                                                   I2200120171101NF 000000000803219                                    S0000000159751001         A0270139165V         I2200200000020171120000000VNOTA FISCAL                                                                             I2200307358761000000GERDAU                                                                                VP       I2200120171011OO 000000016668348SDR                                 S0201709251087598          0588683604V         I2200200000020171202000000VOUTRAS OPER                                                                             I2200315139629000194SPC-COELBA                                                                            VC       I2200120170712OO 000000000003878SDR                                 S0201707233828527          0576943613V         I2200200000020170901000000VOUTRAS OPER                                                                             I2200315139629000194SPC-COELBA                                                                            VC       I2200120160530DP 000000004975000                                    S000124636-1              A0248672896V         I2200200000020171003000000VDUPLICATA                                                                               I2200360398138000000PRODUQUIMIC                                                                           VP       I2200120160425OA 000000001075493                                    S3866-U/14                A0248672897V         I2200200000020171003000000VOPER AGRIC                                                                              I2200307667223000000UNICOT COMERCIAL                                                                      VP       I2200120160425OA 000000001891788                                    S4235-U15                 A0248672898V         I2200200000020171003000000VOPER AGRIC                                                                              I2200307667223000000UNICOT COMERCIAL                                                                      VP       I2200120151030DP 000000018250000                                    S30131124 GE19            A0248672899V         I2200200000020171003000000VDUPLICATA                                                                               I2200322266175000000HERINGER                                                                              VP       I2200120150917TD 000000000621400                                    SS1601930                 A0248672900V         I2200200000020171003000000VTIT DESCONTA                                                                            I2200304898488000000A N T T                                                                               VP       I1400120171201AD 000000000209740000000000030311      B DO BRASIL                                       SA0295489479I14002 000000201712190000000000000000000002ADIANT CONTA                                                            I1400120170901AG 060746948000112000000004708130      BANCO BRADESCO                                    SA0287344915I14002 00000020171218000000162911150000034AEMPRESTIMO                                                              I120012017101100010001EX 000001404951597MCW CEMARACANAU                     S 000000201711280813540021258236       I12002EXECUCAO                                                                                                     I120012017030800370000EX 000000113669308SPO SPSAO PAULO                     S 000000201703132056300020055445       I12002EXECUCAO                                                                                                     I120012015102900010001EX 000000010000000BES BABARREIRAS                     S 000000201703212022530020102313       I12002EXECUCAO                                                                                                     I11001201711270001   000000007000000BES BABARREIRAS                              000000201712011837370257954682    I11001201710100001   000000010000000SSR BASAO DESIDERIO                          000000201710131150360255819981    I11001201709210001   000000005000000SSR BASAO DESIDERIO                          000000201709221634330254978785    I11001201708230001   000000010000000SSR BASAO DESIDERIO                          000000201709081315510254255423    I11001201705050001   000000000106080SSR BASAO DESIDERIO                          000000201709081315490254255291    I11001201701310001   000000000229747BES BABARREIRAS                              000000201702071944110243899319    I11001201610210001   000000000854670BES BABARREIRAS                              000000201610242111370238239177    I11001201606270001   000000000060900SAN RSSANTO ANGELO                           000000201606281042070232697747    I11001201605300001   000000000060780SAN RSSANTO ANGELO                           000000201605311605440231277577    I11001201604130001   000000001961622BES BABARREIRAS                              000000201604191306360229347038    I11001201604080001   000000002176879BES BABARREIRAS                              000000201604121549120229025740    I11001201601060001   000000002032374BES BABARREIRAS                              000000201601111331470225165353    I11001201511190001   000000000052699BES BABARREIRAS                              000000201511241900380223162425    I11001201507100001   000000000071400BES BABARREIRAS                              000000201507131949590218064114    I11001201501130001   000000000837341BES BABARREIRAS                              000000201501151417300211659563    I52000CATELAN E CIA LTDA EPP                                                9045694800014905331977031020170813RSS  I52000AGROPECUARIA CATELAN LTDA ME                                          4060991900010002491991110620170212BAN  I52000AGROPECUARIA RANCHO GRANDE LTDA ME                                    6327986300016002091992050720170212BAN  T999000PROCESSO ENCERRADO NORMALMENTE                                                                             ";
                var concentreRetorno = "B49C      000043953042149FC FI0000200            S99SFIMAN SS N                                                                         000000000               00  2017122619521600000028    0029                                                                        0000                    3#                                                                             P002RSPU                                                                                                           I00100DS                           S                                                                               I10000JOVINIANO OLIVEIRA DOS REIS                                           1007196220208201700N                   I10501EXISTEM 2 VARIACOES DE GRAFIAS PARA O DOCUMENTO CONSULTADO                                                   I10502JOVINIANO OLIVEIRA REIS                                                                                      I10503JOVINIANO OLIVEIRA DOS REIS                                                                                  I220002013060620140210000000004000000001757227VSPC-BANCO DA AMA000000004985231                                     I140002014011020140210000000003000000001757227BANCO DA AMAZONI000000004980592                                      I2200120140210OO 000000001757227DIS                                 N1300378                   0317364756V         I2200200000020140327000000VOUTRAS OPER                                                                             I2200304902979005880SPC-BANCO DA AMAZONIA                                                                 VC       I2200120140110OO 000000001641469DIS                                 S1300360                   0317748311V         I2200200000020140331000000VOUTRAS OPER                                                                             I2200304902979005880SPC-BANCO DA AMAZONIA                                                                 VC       I2200120140110OO 000000001581896DIS                                 N1300386                   0317737164V         I2200200000020140331000000VOUTRAS OPER                                                                             I2200304902979005880SPC-BANCO DA AMAZONIA                                                                 VC       I2200120130606OO 000000000004639PMJ                                 S05.0020135031905          0427631664V         I2200200000020160913190025VOUTRAS OPER                                                                             I2200325086034000171SPC-ENERGISA TOCANTINS                                                                VC       I1400120140210AG 004902979005880000000001757227      BANCO DA AMAZONI                                  N 1473448590I14002 000000201403310000001300378         EMPRESTIMO                                                              I1400120140110AG 004902979005880000000001641469      BANCO DA AMAZONI                                  S 1475153227I14002 0000002014033100000013 00360        EMPRESTIMO                                                              I1400120140110AG 004902979005880000000001581896      BANCO DA AMAZONI                                  N 1475153102I14002 000000201403310000001300386         EMPRESTIMO                                                              I52000JOVINIANO OLIVEIRA DOS REIS 43953042149 ME                            1225023300012110002010072020171217TON  T999000PROCESSO ENCERRADO NORMALMENTE                                                                             ";

                var retorno = new ReturnConcentreNet().ReturnConcentre(concentreRetorno);

                // Assert.IsTrue(retorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //[Test]
        //public async Task SerasaCredNet()
        //{
        //    var serasa = new AppServiceSerasa(_unitOfWork);
        //    var solicitante = new SolicitanteSerasaDto();
        //    solicitante.ContaClienteID = new Guid("1e73e42e-b0cf-4a9e-97cc-000103114a19");
        //    try
        //    {
        //        await serasa.ConsultarSerasa(solicitante);
        //        Assert.IsTrue(true);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        [Test]
        public async Task SerasaGetExist()
        {
            var serasa = new AppServiceSerasa(_unitOfWork);
            var conta = new Guid("1e73e42e-b0cf-4a9e-97cc-000103114a19");
           var resposta = await serasa.ExistSerasa(conta, "G");
        }
    }
}