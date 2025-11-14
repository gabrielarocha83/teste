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
    public class UnidadeMedidaCulturaTests
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
        public async Task Insert_ShoulSaveUnidadeMedidaCulturaDatabase()
        {

            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceUsuario = new AppServiceUsuario(unitofWork);
                var usuario = await appServiceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var unidadeMedidaCultura = new AppServiceUnidadeMedidaCultura(unitofWork);

                var medidaCultura = new UnidadeMedidaCulturaBuilder()
                    .WithId(Guid.NewGuid())
                    .WithNome("Tipo01")
                    .WithSigla("TP01")
                    .WithUsuarioIDCriacao(usuario.ID)
                    .WithAtivo(true)
                    .Build();
                var boolUnidadeMedidaCultura = unidadeMedidaCultura.Insert(medidaCultura.MapTo<UnidadeMedidaCulturaDto>());


                //Assert
                Assert.IsTrue(boolUnidadeMedidaCultura);
            }

        }

        [Test, Order(2)]
        public async Task ShouldSearchTheIrrigatedArea()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var unidadeMedidaCultura = new AppServiceUnidadeMedidaCultura(unitofWork);

                //Act
                var unidadeMedidaCulturas = await unidadeMedidaCultura.GetAllFilterAsync(c => c.Nome.Contains("Tipo01"));

                //Assert
                Assert.IsNotNull(unidadeMedidaCulturas);
                Assert.IsTrue(unidadeMedidaCulturas.Any());
            }
        }

        [Test, Order(3)]
        public async Task ShouldListAllTheIrrigatedArea()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var unidadeMedidaCultura = new AppServiceUnidadeMedidaCultura(unitofWork);

                //Act
                var unidadeMedidaCulturas = await unidadeMedidaCultura.GetAllAsync();

                //Assert
                Assert.IsTrue(unidadeMedidaCulturas.Any());
            }
        }

        [Test, Order(4)]
        public async Task ShouldSearchOneIrrigatedArea()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var unidadeMedidaCultura = new AppServiceUnidadeMedidaCultura(unitofWork);

                //Act
                var unidademedidaCultura = await unidadeMedidaCultura.GetAsync(c => c.Nome.Equals("Tipo01"));

                //Assert
                Assert.IsNotNull(unidademedidaCultura);
                Assert.IsTrue(unidademedidaCultura.ID != null);
            }
        }

        [Test, Order(5)]
        public async Task ShouldUpdateBasicDataOfIrrigatedArea()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var unidadeMedidaCultura = new AppServiceUnidadeMedidaCultura(unitofWork);
                var usuario = new AppServiceUsuario(unitofWork);

                var UnidadeMedidaCultura = await unidadeMedidaCultura.GetAsync(c => c.Nome.Contains("Tipo01"));
                var user = await usuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicDataUnidadeMedidaCultura = new UnidadeMedidaCulturaDto()
                {
                    ID = UnidadeMedidaCultura.ID,
                    Nome = "Tipo01",
                    Sigla = "TP01",
                    UsuarioIDAlteracao = user.ID,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await unidadeMedidaCultura.Update(basicDataUnidadeMedidaCultura);

                //Assert
                Assert.IsNotNull(UnidadeMedidaCultura);
                Assert.IsTrue(resultUpdate);
            }
        }
    }
}
