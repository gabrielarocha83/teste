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
    public class ReceitaTest
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
        public async Task GetAllFilterAsync_ShouldSearchTheRevenue()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceReceita = new AppServiceReceita(unitofWork);

                //Act
                var resultSearch = await appServiceReceita.GetAllFilterAsync(c => c.Descricao.Equals("Fazenda"));

                //Assert
                Assert.IsTrue(resultSearch.Count() == 1);
            }
        }

        [Test]
        public async Task GetAllAsync_ShouldSearchListTheRevenue()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceReceita = new AppServiceReceita(unitofWork);

                //Act
                var resultSearch = await appServiceReceita.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test]
        public async Task Update_ShouldSaveBasicDataOfRevenue()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appReceita = new AppServiceReceita(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var receita = await appReceita.GetAsync(c => c.Descricao.Equals("Fazenda"));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicData = new ReceitaDto
                {
                    ID = receita.ID,
                    Descricao = "Estoque de Plantio",
                    UsuarioIDAlteracao = user.ID,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await appReceita.Update(basicData);

                //Assert
                Assert.IsTrue(resultUpdate);
            }
        }

        [Test]
        public async Task Insert_ShouldSaveBasicDataOfRevenue()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appReceita = new AppServiceReceita(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicData = new ReceitaDto
                {
                    ID = Guid.NewGuid(),
                    Descricao = "Insert of Tests",
                    UsuarioIDCriacao = user.ID,
                    DataCriacao = DateTime.Now
                };

                //Act
                var resultInsert = appReceita.Insert(basicData);

                //Assert
                Assert.IsTrue(resultInsert);
            }
        }

        [Test]
        public async Task GetAsync_ShouldDeleteBasicDataOfRevenue()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appReceita = new AppServiceReceita(unitofWork);
                
                var receita = await appReceita.GetAsync(c => c.Descricao.Equals("Fazenda"));
                
                //Act
                var resultInactive = await appReceita.Inactive(receita.ID);

                //Assert
                Assert.IsTrue(resultInactive);
            }
        }
    }
}
