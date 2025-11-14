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
    [TestFixture]
    public class TipoGarantiaTest
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
        public async Task Insert_ShoulSaveTypeGuaranteeDatabase()
        {

            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var usuarios = new AppServiceUsuario(unitofWork);
                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));
              
                var tipogarantia = new AppServiceTipoGarantia(unitofWork);

                var codigocontacliente = new TipoGarantiaBuilder()
                    .WithId(Guid.NewGuid())
                    .WithNome("Tipo01")
                    .WithAtivo(true)
                    .WithUsuarioIDAlteracao(usuario.ID)
                    .Build();
                var booltipogarantia = tipogarantia.Insert(codigocontacliente.MapTo<TipoGarantiaDto>());


                //Assert
                Assert.IsTrue(booltipogarantia);
            }

        }

        [Test, Order(2)]
        public async Task GetAllFilterAsync_ShouldSearchTheTypeGuarantee()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceTipoGarantia = new AppServiceTipoGarantia(unitofWork);

                //Act
                var resultSearch = await appServiceTipoGarantia.GetAllFilterAsync(c => c.Nome.Equals("Tipo01"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(3)]
        public async Task GetAllAsync_ShouldListAllTheTypeGuarantee()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceTipoGarantia = new AppServiceTipoGarantia(unitofWork);

                //Act
                var resultSearch = await appServiceTipoGarantia.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(4)]
        public async Task GetAsync_ShouldSearchOneTypeGuarantee()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceTipoGarantia = new AppServiceTipoGarantia(unitofWork);

                //Act
                var resultSearch = await appServiceTipoGarantia.GetAsync(c => c.Nome.Equals("Tipo01"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.ID!=null);
            }
        }

        [Test, Order(5)]
        public async Task Update_ShouldSaveBasicDataOfTypeGuarantee()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appTipoGarantia = new AppServiceTipoGarantia(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var tipoGarantia = await appTipoGarantia.GetAsync(c => c.Nome.Equals("Tipo01"));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicDataTipoGarantia = new TipoGarantiaDto()
                {
                    ID = tipoGarantia.ID,
                    Nome = "Tipo01",
                    UsuarioIDAlteracao = user.ID,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await appTipoGarantia.Update(basicDataTipoGarantia);

                //Assert
                Assert.IsNotNull(tipoGarantia);
                Assert.IsTrue(resultUpdate);
            }
        }
    }
}