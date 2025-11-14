using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Yara.AppService;
using Yara.AppService.Dtos;
using Yara.AppService.Mappings;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.AppService
{
    public class TipoEmpresaTest
    {
        private Container _container;

        [OneTimeSetUp]
        public void Initializer()
        {
            _container = new Container();
            //DI of Class project
            _container = IoC.SimpleInjectorContainer.RegisterServices();

            //Auto Mapper Class AppService Initializer
            Mapper.Initialize(x =>
            {
                x.AddProfile(new MappingProfile());
            });
        }

        [Test]
        public async Task GetAllFilterAsync_ShouldSearchTheCompanyType()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceTipoEmpresa = new AppServiceTipoEmpresa(unitofWork);

                //Act
                var resultSearch = await appServiceTipoEmpresa.GetAllFilterAsync(c => c.Tipo.Equals("Defensivos"));

                //Assert
                Assert.IsTrue(resultSearch.Count() == 1);
            }
        }

        [Test]
        public async Task GetAllAsync_ShouldSearchListTheCompanyType()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceTipoEmpresa = new AppServiceTipoEmpresa(unitofWork);

                //Act
                var resultSearch = await appServiceTipoEmpresa.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test]
        public async Task GetAsyncShouldUpdateBasicDataOfCompanyType()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appTipoEmpresa = new AppServiceTipoEmpresa(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var tipoEmpresa = await appTipoEmpresa.GetAsync(c => c.Tipo.Equals("Defensivos"));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicData = new TipoEmpresaDto
                {
                    ID = tipoEmpresa.ID,
                    Tipo = "Sementes de Plantio",
                    UsuarioIDAlteracao = user.ID,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await appTipoEmpresa.Update(basicData);

                //Assert
                Assert.IsTrue(resultUpdate);
            }
        }

        [Test]
        public async Task GetAsync_ShouldInsertBasicDataOfCompanyType()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appTipoEmpresa = new AppServiceTipoEmpresa(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicData = new TipoEmpresaDto
                {
                    ID = Guid.NewGuid(),
                    Tipo = "Insert of Tests",
                    UsuarioIDCriacao = user.ID,
                    DataCriacao = DateTime.Now
                };

                //Act
                var resultInsert = appTipoEmpresa.Insert(basicData);

                //Assert
                Assert.IsTrue(resultInsert);
            }
        }

        [Test]
        public async Task Inactive_ShouldDeleteBasicDataOfCompanyType()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appTipoEmpresa = new AppServiceTipoEmpresa(unitofWork);

                var tipoEmpresa = await appTipoEmpresa.GetAsync(c => c.Tipo.Contains("Defensivos"));

                //Act
                var resultInactive = await appTipoEmpresa.Inactive(tipoEmpresa.ID);

                //Assert
                Assert.IsTrue(resultInactive);
            }
        }
    }
}
