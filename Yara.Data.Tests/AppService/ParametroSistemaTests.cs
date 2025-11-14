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
    public class ParametroSistemaTests
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
        public async Task Insert_ShoulSaveParameterDatabase()
        {

            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var usuarios = new AppServiceUsuario(unitofWork);
                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));

                var parametroSistema = new AppServiceParametroSistema(unitofWork);

                var codigocontacliente = new ParametroSistemaBuilder()
                    .WithId(Guid.NewGuid())
                    .WithGrupo("Agrupamento de Email")
                    .WithTipo("email")
                    .WithChave("emailTO")
                    .WithValor("luciano.jesus@performait.com")
                    .WithAtivo(true)
                    .WithDataUsuarioCriacao(usuario.ID)
                    .Build();
                var boolParametroSistema = parametroSistema.Insert(codigocontacliente.MapTo<ParametroSistemaDto>());


                //Assert
                Assert.IsTrue(boolParametroSistema);
            }

        }

        [Test, Order(2)]
        public async Task GetAllFilterAsync_ShouldSearchTheParameter()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceParametroSistema = new AppServiceParametroSistema(unitofWork);

                //Act
                var resultSearch = await appServiceParametroSistema.GetAllFilterAsync(c => c.Tipo.Equals("email"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(3)]
        public async Task GetAllAsync_ShouldListAllTheParameter()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceParametroSistema = new AppServiceParametroSistema(unitofWork);
                var empresa = "Y";
                //Act
                var resultSearch = await appServiceParametroSistema.GetAllAsync(empresa);

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(4)]
        public async Task GetAsync_ShouldSearchOneParameter()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceParametroSistema = new AppServiceParametroSistema(unitofWork);

                //Act
                var resultSearch = await appServiceParametroSistema.GetAsync(c => c.Tipo.Equals("email"));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.ID != null);
            }
        }

        [Test, Order(5)]
        public async Task Update_ShouldSaveBasicDataOfParameter()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appParametroSistema = new AppServiceParametroSistema(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var parametroSistema = await appParametroSistema.GetAsync(c => c.Tipo.Equals("emailTO"));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicDataParametroSistema = new ParametroSistemaDto()
                {
                    ID = parametroSistema.ID,
                    Tipo = "email",
                    Valor = "luciano.carlos.jesus@performait.com",
                    UsuarioIDAlteracao = user.ID,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await appParametroSistema.Update(basicDataParametroSistema);

                //Assert
                Assert.IsNotNull(parametroSistema);
                Assert.IsTrue(resultUpdate);
            }
        }

    }
}
