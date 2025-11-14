using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Yara.AppService;
using Yara.AppService.Dtos;
using Yara.AppService.Mappings;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.AppService
{
    public class ContaClienteTest
    {
        private Container _container;

        [OneTimeSetUp]
        public void Initializer()
        {
            _container = new Container();


            //DI of Class project
            _container = IoC.SimpleInjectorContainer.RegisterServices();

            //Auto Mapper Class AppService Initializer
            Mapper.Initialize(x =>
            {
                x.AddProfile(new MappingProfile());
            });
        }
        
        [Test, Order(1)]
        public async Task UpdateAsync_ShouldSaveBasicDataOfAccountClient()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange

                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceContaCliente = new AppServiceContaCliente(unitofWork);
                var serviceSegmento = new AppServiceSegmento(unitofWork);
                var serviceTipoCliente = new AppServiceTipoCliente(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);
                var tipocliente = await serviceTipoCliente.GetAsync(c=>c.Nome.Equals("Usina"));
                var segmento = await serviceSegmento.GetAsync(c => c.Descricao.Equals("Industrial"));
                var contacliente = await appServiceContaCliente.GetAsync(c => c.Documento.Equals("31691301884"));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicData = new ContaClienteAlteracaoDadosPessoaisDto()
                {
                    ID = contacliente.ID,
                    Telefone = "1932244034",
                    CEP = "13044555",
                    Nome = "Claudemir P. Júnior",
                    Apelido = "Junior P.",
                    Cidade = "Campinas",
                    DataNascimentoFundacao = DateTime.Now,
                    Endereco = "Rua Maria Dulce, 200",
                    UF = "SP",
                    UsuarioIDAlteracao = user.ID,
                    SegmentoID = segmento.ID,
                    TipoClienteID = tipocliente.ID


                };

                //Act
                var resultUpdate= await appServiceContaCliente.UpdateAsync(basicData);

                //Assert
                // Assert.IsTrue(resultUpdate);
            }
        }
        
        [Test, Order(2)]
        [TestCase("Claudemir", "", "", "", "")]
        [TestCase("", "Juni", "", "", "")]
        [TestCase("", "", "123", "", "")]
        [TestCase("", "", "", "316", "")]
        [TestCase("", "", "", "", "Tipo")]
        public async Task GetListAccountClient_ShouldSearchDocumentListTheAccountClient(string nome, string apelido, string codigoprincipal, string documento, string grupoeconomico)
        {

            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange

                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceContaCliente = new AppServiceContaCliente(unitofWork);
                var search = new BuscaContaClienteDto()
                {
                    Nome = nome,
                    Apelido = apelido,
                    CodigoPrincipal = codigoprincipal,
                    Documento = documento,
                    GrupoEconomico = grupoeconomico
                };

                //Act
                var resultSearch = await appServiceContaCliente.GetListAccountClient(search, Guid.Empty);

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }


        }


        [Test, Order(3)]
        public async Task GetAsync_ShouldGetTheAccountClientWithID()
        {

            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange

                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceContaCliente = new AppServiceContaCliente(unitofWork);
                var contacliente = await appServiceContaCliente.GetAsync(c => c.Documento.Equals("31691301884"));

                //Act
                var resultSearch = await appServiceContaCliente.GetAsync(c => c.ID.Equals(contacliente.ID));

                //Assert
                Assert.AreEqual(resultSearch.ID, contacliente.ID);
            }


        }

        public async Task UpdateAsync_ShoulSaveBloquedManualOfAccountClient()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange

                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceContaCliente = new AppServiceContaCliente(unitofWork);
                var serviceSegmento = new AppServiceSegmento(unitofWork);
                var serviceTipoCliente = new AppServiceTipoCliente(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);
                var tipocliente = await serviceTipoCliente.GetAsync(c => c.Nome.Equals("Usina"));
                var segmento = await serviceSegmento.GetAsync(c => c.Descricao.Equals("Industrial"));
                var contacliente = await appServiceContaCliente.GetAsync(c => c.Documento.Equals("31691301884"));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                //var basicData = new ContaClienteAlteracaoDadosPessoaisDto()
                //{
                //    ID = contacliente.ID,
                //    Telefone = "1932244034",
                //    CEP = "13044555",
                //    Nome = "Claudemir P. Júnior",
                //    Apelido = "Junior P.",
                //    Cidade = "Campinas",
                //    DataNascimentoFundacao = DateTime.Now,
                //    Endereco = "Rua Maria Dulce, 200",
                //    UF = "SP",
                //    UsuarioIDAlteracao = user.ID,
                //    SegmentoID = segmento.ID,
                //    TipoClienteID = tipocliente.ID


                //};
                
                var bloqueio = new BloqueioManualContaClienteDto()
                {
                    ID = contacliente.ID,
                    BloqueioManual = true
                };


                //Act
                var resultUpdate = await appServiceContaCliente.UpdateAsyncManualLock(bloqueio);

                //Assert
                Assert.IsTrue(resultUpdate);
            }
        }

    }
}