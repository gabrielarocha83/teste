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
    public class FeriadoTests
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
        public async Task Insert_ShoulSaveHolidayDatabase()
        {

            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var usuarios = new AppServiceUsuario(unitofWork);

                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));

                if (string.IsNullOrEmpty(usuario.ToString()))
                {
                    //Add Usuario
                    var user01 = new UsuarioDto()
                    {
                        ID = Guid.NewGuid(),
                        Nome = "Junior Porfirio",
                        Login = "cpjunior",
                        Email = "claudemir.junior@performait.com",
                        Ativo = true,
                        TipoAcesso = (TipoAcessoDto)1,
                        DataCriacao = DateTime.Now
                    };
                    usuarios.Insert(user01);
                }
                

                var feriado = new AppServiceFeriado(unitofWork);

                var feriadoBuild = new FeriadoBuilder()
                    .WithId(Guid.NewGuid())
                    .WithDataFeriado(DateTime.Now)
                    .WithDescricao("Ano Novo")
                    .WithUsuarioIdCriacao(usuario.ID)
                    .WithDataCriacao(DateTime.Now)
                    .Build();
                var boolFeriado = feriado.Insert(feriadoBuild.MapTo<FeriadoDto>());


                //Assert
                Assert.IsTrue(boolFeriado);
            }

        }

        [Test, Order(2)]
        public async Task GetAllFilterAsync_ShouldSearchTheHoliday()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceFeriado = new AppServiceFeriado(unitofWork);

                //Act
                var resultSearch = await appServiceFeriado.GetAllFilterAsync(c => c.Descricao.Contains("Ano"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(3)]
        public async Task GetAllAsync_ShouldListAllTheHoliday()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceFeriado = new AppServiceFeriado(unitofWork);

                //Act
                var resultSearch = await appServiceFeriado.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(4)]
        public async Task GetAsync_ShouldSearchOneHoliday()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceFeriado = new AppServiceFeriado(unitofWork);

                //Act
                var resultSearch = await appServiceFeriado.GetAsync(c => c.Descricao.Contains("Ano"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.ID != null);
            }
        }

        [Test, Order(5)]
        public async Task Update_ShouldSaveBasicDataOfHoliday()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appFeriado = new AppServiceFeriado(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var feriado = await appFeriado.GetAsync(c => c.Descricao.Contains("Ano"));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicDataFeriado = new FeriadoDto()
                {
                    ID = feriado.ID,
                    Descricao = feriado.Descricao,
                    DataFeriado = Convert.ToDateTime("2018-01-01 00:00:00"),
                    UsuarioIDAlteracao = user.ID,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await appFeriado.Update(basicDataFeriado);

                //Assert
                Assert.IsNotNull(feriado);
                Assert.IsTrue(resultUpdate);
            }
        }
    }
}
