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
    public class TipoEndividamentoTest
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

        [Test, Order(1)]
        public void GetAsync_ShouldInsertBasicDataOfCompanyType()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var apptipoendividamento = new AppServiceTipoEndividamento(unitofWork);
               

             

                var basicData = new TipoEndividamentoDto
                {
                    ID = Guid.NewGuid(),
                    Tipo = "Tipo01",
                    UsuarioIDCriacao =Guid.Empty,
                    DataCriacao = DateTime.Now
                };

                //Act
                var resultInsert = apptipoendividamento.Insert(basicData);

                //Assert
                Assert.IsTrue(resultInsert);
            }
        }
        [Test,Order(2)]
        public async Task GetAllFilterAsync_ShouldSearchTheCompanyType()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var AppServiceTipoEndividamento = new AppServiceTipoEndividamento(unitofWork);

                //Act
                var resultSearch = await AppServiceTipoEndividamento.GetAllFilterAsync(c => c.Tipo.Equals("Tipo01"));

                //Assert
                Assert.IsTrue(resultSearch.Count() == 1);
            }
        }

        [Test, Order(3)]
        public async Task GetAllAsync_ShouldSearchListTheCompanyType()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var AppServiceTipoEndividamento = new AppServiceTipoEndividamento(unitofWork);

                //Act
                var resultSearch = await AppServiceTipoEndividamento.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(4)]
        public async Task GetAsyncShouldUpdateBasicDataOfCompanyType()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var apptipoendividamento = new AppServiceTipoEndividamento(unitofWork);
              

                var tipoendividamento = await apptipoendividamento.GetAsync(c => c.Tipo.Equals("Tipo01"));

                var basicData = new TipoEndividamentoDto
                {
                    ID = tipoendividamento.ID,
                    Tipo = "Sementes de Plantio",
                    UsuarioIDAlteracao = Guid.Empty,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await apptipoendividamento.Update(basicData);

                //Assert
                Assert.IsTrue(resultUpdate);
            }
        }

       

      
    }
}
