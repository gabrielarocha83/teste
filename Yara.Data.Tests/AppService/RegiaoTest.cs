using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Yara.AppService;
using Yara.AppService.Mappings;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.AppService
{
    public class RegiaoTest
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
        public async Task GetAllRegion_ShouldReturnListTheRegionDatabase()
        {
            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();


                var regiao = new AppServiceRegiao(unitofWork);


                var AllRegions = await regiao.GetAllRegion();

                //Assert
                Assert.IsTrue(AllRegions.Any());
            }

        }

    }
}
