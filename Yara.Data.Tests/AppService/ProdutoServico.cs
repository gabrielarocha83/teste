using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Yara.AppService;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Mappings;
using Yara.Data.Tests.Builder;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.AppService
{
    public class ServiceProduct
    {
        private Container _container;

        [OneTimeSetUp]
        public void Initializer()
        {
            _container = new Container();

            _container = IoC.SimpleInjectorContainer.RegisterServices();

            Mapper.Initialize(x =>
            {
                x.AddProfile(new MappingProfile());
            });
        }

        [Test, Order(1)]
        public async Task ShoulInsertServiceProductDatabase()
        {
            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var usuarios = new AppServiceUsuario(unitofWork);
                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));

                var prd = new AppServiceProdutoServico(unitofWork);

                var exp = new ProdutoServicoBuilder()
                    .WithId(Guid.NewGuid())
                    .WithNome("Produto_Serviço_01")
                    .WithAtivo(true)
                    .WithDataCriacao(DateTime.Now)
                    .WithUsuarioCriacao(usuario.ID)
                    .Build();
                var boolAreaIrrigada = prd.Insert(exp.MapTo<ProdutoServicoDto>());


                //Assert
                Assert.IsTrue(boolAreaIrrigada);
            }

        }

        [Test, Order(2)]
        public async Task ShouldSearchTheServiceProduct()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appService = new AppServiceProdutoServico(unitofWork);

                //Act
                var resultSearch = await appService.GetAllFilterAsync(c => c.Nome.Contains("Sementes"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(3)]
        public async Task ShouldListAllTheServiceProduct()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appService = new AppServiceProdutoServico(unitofWork);

                //Act
                var resultSearch = await appService.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(4)]
        public async Task ShouldSearchOneServiceProduct()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appService = new AppServiceProdutoServico(unitofWork);

                //Act
                var resultSearch = await appService.GetAsync(c => c.Nome.Contains("Sementes"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.ID != null);
            }
        }

        [Test, Order(5)]
        public async Task ShouldUpdateBasicDataOfServiceProduct()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var produto = new AppServiceProdutoServico(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var app = await produto.GetAsync(c => c.Nome.Contains("Sementes"));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicDatacultura = new ProdutoServicoDto()
                {
                    ID = app.ID,
                    Nome = "SementesTeste01",
                    UsuarioIDAlteracao = user.ID,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await produto.Update(basicDatacultura);

                //Assert
                Assert.IsNotNull(app);
                Assert.IsTrue(resultUpdate);
            }
        }
    }
}
