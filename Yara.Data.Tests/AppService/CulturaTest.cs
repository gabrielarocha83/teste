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
    public class CulturaTest
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
        public async Task ShoulInsertCultureDatabase()
        {
            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var usuarios = new AppServiceUsuario(unitofWork);
                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));

                var cultura = new AppServiceCultura(unitofWork);

                var exp = new CulturaBuilder()
                    .WithId(Guid.NewGuid())
                    .WithDescricao("Cultura01")
                    .WithAtivo(true)
                    .WithDataCriacao(DateTime.Now)
                    .WithDataUsuarioCriacao(usuario.ID)
                    .Build();
                var boolAreaIrrigada = cultura.Insert(exp.MapTo<CulturaDto>());


                //Assert
                Assert.IsTrue(boolAreaIrrigada);
            }

        }

        [Test, Order(2)]
        public async Task ShouldSearchTheCulture()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceCultura = new AppServiceCultura(unitofWork);

                //Act
                var resultSearch = await appServiceCultura.GetAllFilterAsync(c => c.Descricao.Contains("cultura"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(3)]
        public async Task ShouldListAllTheCulture()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceCultura = new AppServiceCultura(unitofWork);

                //Act
                var resultSearch = await appServiceCultura.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(4)]
        public async Task ShouldSearchOneCulture()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceCultura = new AppServiceCultura(unitofWork);

                //Act
                var resultSearch = await appServiceCultura.GetAsync(c => c.Descricao.Contains("cultura"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.ID != null);
            }
        }

        [Test, Order(5)]
        public async Task ShouldUpdateBasicDataOfCulture()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var cultura = new AppServiceCultura(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var appcultura = await cultura.GetAsync(c => c.Descricao.Contains("cultura"));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicDatacultura = new CulturaDto()
                {
                    ID = appcultura.ID,
                    Descricao = "culturaTeste01",
                    UnidadeMedida = "Unidade",
                    UsuarioIDAlteracao = user.ID,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await cultura.Update(basicDatacultura);

                //Assert
                Assert.IsNotNull(appcultura);
                Assert.IsTrue(resultUpdate);
            }
        }
    }
}
