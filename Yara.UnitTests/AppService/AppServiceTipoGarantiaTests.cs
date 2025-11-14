using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Moq;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

namespace Yara.UnitTests.AppService
{
    public class AppServiceTipoGarantiaTests
    {
        private Mock<IAppServiceTipoGarantia> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceTipoGarantia>();
        }

        [Test]
        public void Insert_ShouldSaveTipoGarantiaWithMoq()
        {
            //Arrange

            var culturaFake = new Faker<TipoGarantiaDto>("pt-BR")
                .CustomInstantiator(f => new TipoGarantiaDto(
                    Guid.NewGuid(),
                    f.Name.JobArea().ToString(),
                    true,
                    DateTime.Now,
                    Guid.NewGuid()));

            var tipogarantia = culturaFake.Generate();

            _appServiceMock.Setup(c => c.Insert(tipogarantia)).Returns(true);

            //Act
            var returns = _appServiceMock.Object.Insert(tipogarantia);

            //Assert
            Assert.IsNotNull(tipogarantia);
            Assert.IsTrue(returns);

        }

        [Test]
        public void Update_ShouldSaveTipoGarantiaWithMoq()
        {
            //Arrange

            var tipoGarantia = new Faker<TipoGarantiaDto>("pt-BR")
                .CustomInstantiator(f => new TipoGarantiaDto(
                    Guid.NewGuid(),
                    f.Name.JobArea().ToString(),
                    true,
                    DateTime.Now,
                    Guid.NewGuid()));

            var garantiaupdate = tipoGarantia.Generate();

            _appServiceMock.Setup(c => c.Update(garantiaupdate)).Returns(Task.FromResult<bool>(true));

            //Act
            var returns = _appServiceMock.Object.Update(garantiaupdate);

            //Assert
            Assert.IsTrue(returns.Result);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListTipoGarantiaWithMoq()
        {
            //Arrange
            var culturaFake = new Faker<TipoGarantiaDto>("pt-BR")
                .CustomInstantiator(f => new TipoGarantiaDto(
                    Guid.NewGuid(),
                    f.Name.JobArea().ToString(),
                    true,
                    DateTime.Now, 
                    Guid.NewGuid()));

            IEnumerable<TipoGarantiaDto> cultList = culturaFake.Generate(10).ToList();

            //Act
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult(cultList));
            var returns = await _appServiceMock.Object.GetAllAsync();

            //Assert
            Assert.IsNotNull(returns);
            Assert.IsTrue(returns.Any());
        }

        [Test]
        public async  Task GetAsync_ShouldReturnOneTipoGarantiaWithMoq()
        {
            //Arrange
            var TipoGarantiaID = Guid.NewGuid();
            var garantiaFake = new Faker<TipoGarantiaDto>("pt-BR")
                .CustomInstantiator(f => new TipoGarantiaDto(
                    TipoGarantiaID,
                    f.Name.JobArea().ToString(),
                    true,
                    DateTime.Now,
                    Guid.NewGuid()));

            var oneGarantiaFake = garantiaFake.Generate();

           
            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(TipoGarantiaID))).Returns(Task.FromResult<TipoGarantiaDto>(oneGarantiaFake));

            //Act
            var returns =await _appServiceMock.Object.GetAsync(g => g.ID.Equals(TipoGarantiaID));

            //Assert
            Assert.IsNotNull(returns);
            Assert.AreEqual(TipoGarantiaID, returns.ID);
        }


    }
}