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
    public class ExperienciaTests
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
        public async Task ShoulInsertExperienceDatabase()
        {
            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var usuarios = new AppServiceUsuario(unitofWork);
                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));

                var experiencia = new AppServiceExperiencia(unitofWork);

                var exp = new ExperienciaBuilder()
                    .WithId(Guid.NewGuid())
                    .WithDescricao("Teste01")
                    .WithAtivo(true)
                    .WithDataCriacao(DateTime.Now)
                    .WithUsuarioCriacao(usuario.ID)
                    .Build();
                var boolAreaIrrigada = experiencia.Insert(exp.MapTo<ExperienciaDto>());


                //Assert
                Assert.IsTrue(boolAreaIrrigada);
            }

        }

        [Test, Order(2)]
        public async Task ShouldSearchTheExperience()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceExperiencia = new AppServiceExperiencia(unitofWork);

                //Act
                var resultSearch = await appServiceExperiencia.GetAllFilterAsync(c => c.Descricao.Contains("experiência"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(3)]
        public async Task ShouldListAllTheExperience()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceExperiencia = new AppServiceExperiencia(unitofWork);

                //Act
                var resultSearch = await appServiceExperiencia.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(4)]
        public async Task ShouldSearchOneExperience()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceExperiencia = new AppServiceExperiencia(unitofWork);

                //Act
                var resultSearch = await appServiceExperiencia.GetAsync(c => c.Descricao.Contains("experiência"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.ID != null);
            }
        }

        [Test, Order(5)]
        public async Task ShouldUpdateBasicDataOfExperience()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var experiencia = new AppServiceExperiencia(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var appExperiencia = await experiencia.GetAsync(c => c.Descricao.Contains("experiência"));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicDataExperiencia = new ExperienciaDto()
                {
                    ID = appExperiencia.ID,
                    Descricao = "ExperienciaTeste01",
                    UsuarioIDAlteracao = user.ID,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await experiencia.Update(basicDataExperiencia);

                //Assert
                Assert.IsNotNull(appExperiencia);
                Assert.IsTrue(resultUpdate);
            }
        }
    }
}
