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
    public class ProdutividadeMediaTests
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
        public async Task ShoulInsertProductivityDatabase()
        {
            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var usuarios = new AppServiceUsuario(unitofWork);
                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));

                var produtividadeMedia = new AppServiceProdutividadeMedia(unitofWork);

                var exp = new ProdutividadeMediaBuilder()
                    .WithId(Guid.NewGuid())
                    .WithNome("ProdutividadeMedia01")
                    .WithAtivo(true)
                    .WithDataCriacao(DateTime.Now)
                    .WithDataUsuarioCriacao(usuario.ID)
                    .Build();
                var boolAreaIrrigada = produtividadeMedia.Insert(exp.MapTo<ProdutividadeMediaDto>());


                //Assert
                Assert.IsTrue(boolAreaIrrigada);
            }

        }

        [Test, Order(2)]
        public async Task ShouldSearchTheProductivity()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceProdutividadeMedia = new AppServiceProdutividadeMedia(unitofWork);

                //Act
                var resultSearch = await appServiceProdutividadeMedia.GetAllFilterAsync(c => c.Nome.Contains("moagem"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(3)]
        public async Task ShouldListAllTheProductivity()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceProdutividadeMedia = new AppServiceProdutividadeMedia(unitofWork);

                //Act
                var resultSearch = await appServiceProdutividadeMedia.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(4)]
        public async Task ShouldSearchOneProductivity()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceProdutividadeMedia = new AppServiceProdutividadeMedia(unitofWork);

                //Act
                var resultSearch = await appServiceProdutividadeMedia.GetAsync(c => c.Nome.Contains("moagem"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.ID != null);
            }
        }

        [Test, Order(5)]
        public async Task ShouldUpdateBasicDataOfProductivity()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var produtividadeMedia = new AppServiceProdutividadeMedia(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var appProdutividadeMedia = await produtividadeMedia.GetAsync(c => c.Nome.Contains("moagem"));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicDataProdutividadeMedia = new ProdutividadeMediaDto()
                {
                    ID = appProdutividadeMedia.ID,
                    Nome = "Produtividade Media Teste01",
                    UsuarioIDAlteracao = user.ID,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await produtividadeMedia.Update(basicDataProdutividadeMedia);

                //Assert
                Assert.IsNotNull(appProdutividadeMedia);
                Assert.IsTrue(resultUpdate);
            }
        }
    }
}
