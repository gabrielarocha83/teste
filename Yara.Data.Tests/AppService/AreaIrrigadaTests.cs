using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
using Yara.Data.Entity.Context;
using Yara.Data.Tests.Builder;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.AppService
{
    public class AreaIrrigadaTests
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
        public async Task ShoulInsertIrrigatedAreaDatabase()
        {

            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var usuarios = new AppServiceUsuario(unitofWork);
                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));

                var areaIrrigada = new AppServiceAreaIrrigada(unitofWork);

                var codigocontacliente = new AreaIrrigadaBuilder()
                    .WithId(Guid.NewGuid())
                    .WithNome("Tipo01")
                    .WithAtivo(true)
                    .Build();
                var boolAreaIrrigada = areaIrrigada.Insert(codigocontacliente.MapTo<AreaIrrigadaDto>());


                //Assert
                Assert.IsTrue(boolAreaIrrigada);
            }

        }

        [Test, Order(2)]
        public async Task ShouldSearchTheIrrigatedArea()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceAreaIrrigada = new AppServiceAreaIrrigada(unitofWork);

                //Act
                var resultSearch = await appServiceAreaIrrigada.GetAllFilterAsync(c => c.Nome.Contains("Tipo01"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(3)]
        public async Task ShouldListAllTheIrrigatedArea()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceAreaIrrigada = new AppServiceAreaIrrigada(unitofWork);

                //Act
                var resultSearch = await appServiceAreaIrrigada.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(4)]
        public async Task ShouldSearchOneIrrigatedArea()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceAreaIrrigada = new AppServiceAreaIrrigada(unitofWork);

                //Act
                var resultSearch = await appServiceAreaIrrigada.GetAsync(c => c.Nome.Equals("Tipo01"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.ID != null);
            }
        }

        [Test, Order(5)]
        public async Task ShouldUpdateBasicDataOfIrrigatedArea()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appAreaIrrigada = new AppServiceAreaIrrigada(unitofWork);
                

                var areaIrrigada = await appAreaIrrigada.GetAsync(c => c.Nome.Equals("Tipo01"));


                var basicDataAreaIrrigada = new AreaIrrigadaDto()
                {
                    ID = areaIrrigada.ID,
                    Nome = "Tipo02",
                    UsuarioIDAlteracao = Guid.Empty,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await appAreaIrrigada.Update(basicDataAreaIrrigada);

                //Assert
                Assert.IsNotNull(areaIrrigada);
                Assert.IsTrue(resultUpdate);
            }
        }
    }
}
