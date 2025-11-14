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
    public class TipoPecuariaTest
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
        public async Task Insert_ShoulSaveLivestocktypeDatabase()
        {

            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var usuarios = new AppServiceUsuario(unitofWork);
                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));
              
                var tipopecuaria = new AppServiceTipoPecuaria(unitofWork);

                var codigocontacliente = new TipoPecuariaBuilder()
                    .WithId(Guid.NewGuid())
                    .WithTipo("Carne")
                    .WithUnidadeMedida("Kilo")
                    .WithAtivo(true)
                    .WithUsuarioIDCriacao(usuario.ID)
                    .Build();
                var booltipopecuaria = tipopecuaria.Insert(codigocontacliente.MapTo<TipoPecuariaDto>());


                //Assert
                Assert.IsTrue(booltipopecuaria);
            }

        }

        [Test, Order(2)]
        public async Task GetAllFilterAsync_ShouldSearchTheLivestocktype()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceTipoPecuaria = new AppServiceTipoPecuaria(unitofWork);

                //Act
                var resultSearch = await appServiceTipoPecuaria.GetAllFilterAsync(c => c.Tipo.Equals("Carne"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(3)]
        public async Task GetAllAsync_ShouldListAllTheLivestocktype()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var AppServiceTipoPecuaria = new AppServiceTipoPecuaria(unitofWork);

                //Act
                var resultSearch = await AppServiceTipoPecuaria.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(4)]
        public async Task GetAsync_ShouldSearchOneLivestocktype()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var AppServiceTipoPecuaria = new AppServiceTipoPecuaria(unitofWork);

                //Act
                var resultSearch = await AppServiceTipoPecuaria.GetAsync(c => c.Tipo.Equals("Carne"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.ID!=null);
            }
        }

        [Test, Order(5)]
        public async Task Update_ShouldSaveBasicDataOfLivestocktype()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var apptipopecuaria = new AppServiceTipoPecuaria(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var tipopecuaria = await apptipopecuaria.GetAsync(c => c.Tipo.Equals("Carne"));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicDatatipopecuaria = new TipoPecuariaDto()
                {
                    ID = tipopecuaria.ID,
                    Tipo = "Carne",
                    UsuarioIDAlteracao = user.ID,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await apptipopecuaria.Update(basicDatatipopecuaria);

                //Assert
                Assert.IsNotNull(tipopecuaria);
                Assert.IsTrue(resultUpdate);
            }
        }
    }
}