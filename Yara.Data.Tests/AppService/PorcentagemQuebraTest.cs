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
    public class PorcentagemQuebraTest
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
        public async Task Insert_ShoulSavePorcentagemQuebraDatabase()
        {

            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var usuarios = new AppServiceUsuario(unitofWork);
                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));
              
                var PorcentagemQuebra = new AppServicePorcentagemQuebra(unitofWork);

                var codigocontacliente = new PorcentagemQuebraBuilder()
                    .WithId(Guid.NewGuid())
                    .WithPorcentagem(10)
                    .WithAtivo(true)
                    .WithUsuarioIDAlteracao(usuario.ID)
                    .Build();
                var boolPorcentagemQuebra = PorcentagemQuebra.Insert(codigocontacliente.MapTo<PorcentagemQuebraDto>());


                //Assert
                Assert.IsTrue(boolPorcentagemQuebra);
            }

        }

        [Test, Order(2)]
        public async Task GetAllFilterAsync_ShouldSearchThePorcentagemQuebra()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServicePorcentagemQuebra = new AppServicePorcentagemQuebra(unitofWork);

                //Act
                var resultSearch = await appServicePorcentagemQuebra.GetAllFilterAsync(c => c.Porcentagem.Equals(10));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(3)]
        public async Task GetAllAsync_ShouldListAllThePorcentagemQuebra()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServicePorcentagemQuebra = new AppServicePorcentagemQuebra(unitofWork);

                //Act
                var resultSearch = await appServicePorcentagemQuebra.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(4)]
        public async Task GetAsync_ShouldSearchOnePorcentagemQuebra()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServicePorcentagemQuebra = new AppServicePorcentagemQuebra(unitofWork);

                //Act
                var resultSearch = await appServicePorcentagemQuebra.GetAsync(c => c.Porcentagem.Equals(10));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.ID!=null);
            }
        }

        [Test, Order(5)]
        public async Task Update_ShouldSaveBasicDataOfPorcentagemQuebra()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appPorcentagemQuebra = new AppServicePorcentagemQuebra(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var PorcentagemQuebra = await appPorcentagemQuebra.GetAsync(c => c.Porcentagem.Equals(10));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicDataPorcentagemQuebra = new PorcentagemQuebraDto()
                {
                    ID = PorcentagemQuebra.ID,
                    Porcentagem = 15,
                    UsuarioIDAlteracao = user.ID,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await appPorcentagemQuebra.Update(basicDataPorcentagemQuebra);

                //Assert
                Assert.IsNotNull(PorcentagemQuebra);
                Assert.IsTrue(resultUpdate);
            }
        }
    }
}