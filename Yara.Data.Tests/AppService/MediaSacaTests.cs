using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class MediaSacaTests
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
        public async Task ShoulInsertAverageDatabase()
        {
            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var usuarios = new AppServiceUsuario(unitofWork);
                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));

                var mediaSaca = new AppServiceMediaSaca(unitofWork);

                var exp = new MediaSacaBuilder()
                    .WithId(Guid.NewGuid())
                    .WithNome("PreçoMedioSaca01")
                    .WithPeso(25)
                    .WithValor(Convert.ToDecimal(string.Format("{0:N}", "25,50")))
                    .WithAtivo(true)
                    .WithDataCriacao(DateTime.Now)
                    .WithDataUsuarioCriacao(usuario.ID)
                    .Build();
                var boolAreaIrrigada = mediaSaca.Insert(exp.MapTo<MediaSacaDto>());

                //Assert
                Assert.IsTrue(boolAreaIrrigada);
            }

        }

        [Test, Order(2)]
        public async Task ShouldSearchTheAverage()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceMediaSaca = new AppServiceMediaSaca(unitofWork);

                //Act
                var resultSearch = await appServiceMediaSaca.GetAllFilterAsync(c => c.Nome.Contains("PreçoMedioSaca01"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(3)]
        public async Task ShouldListAllTheAverage()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceMediaSaca = new AppServiceMediaSaca(unitofWork);

                //Act
                var resultSearch = await appServiceMediaSaca.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(4)]
        public async Task ShouldSearchOneAverage()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceMediaSaca = new AppServiceMediaSaca(unitofWork);

                //Act
                var resultSearch = await appServiceMediaSaca.GetAsync(c => c.Nome.Contains("PreçoMedioSaca01"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.ID != null);
            }
        }

        [Test, Order(5)]
        public async Task ShouldUpdateBasicDataOfAverage()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var mediaSaca = new AppServiceMediaSaca(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var appMediaSaca = await mediaSaca.GetAsync(c => c.Nome.Contains("PreçoMedioSaca01"));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicDataMediaSaca = new MediaSacaDto()
                {
                    ID = appMediaSaca.ID,
                    Nome = "PreçoMedioSaca02",
                    Peso = 30,
                    Valor = Convert.ToDecimal(string.Format("{0:N}", "13.50")),
                    UsuarioIDAlteracao = user.ID,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await mediaSaca.Update(basicDataMediaSaca);

                //Assert
                Assert.IsNotNull(appMediaSaca);
                Assert.IsTrue(resultUpdate);
            }
        }
    }
}
