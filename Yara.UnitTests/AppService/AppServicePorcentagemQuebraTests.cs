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
    public class AppServicePorcentagemQuebraTests
    {
        private Mock<IAppServicePorcentagemQuebra> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServicePorcentagemQuebra>();
        }

        [Test]
        public void Insert_ShouldSavePorcentagemQuebraWithMoq()
        {
            //Arrange

            var PorcentagemQuebraFake = new Faker<PorcentagemQuebraDto>("pt-BR")
                .CustomInstantiator(f => new PorcentagemQuebraDto(
                    Guid.NewGuid(),
                    f.IndexVariable,
                    true,
                    Guid.NewGuid(),
                    null));

            var PorcentagemQuebra= PorcentagemQuebraFake.Generate();

            _appServiceMock.Setup(c => c.Insert(PorcentagemQuebra)).Returns(true);

            //Act
            var returns = _appServiceMock.Object.Insert(PorcentagemQuebra);

            //Assert
            Assert.IsNotNull(PorcentagemQuebra);
            Assert.IsTrue(returns);

        }

        [Test]
        public void Update_ShouldSavePorcentagemQuebraWithMoq()
        {
            //Arrange

            var PorcentagemQuebraFake = new Faker<PorcentagemQuebraDto>("pt-BR")
                .CustomInstantiator(f => new PorcentagemQuebraDto(
                    Guid.NewGuid(),
                  f.IndexVariable,
                    true,
                    Guid.NewGuid(),
                    null));

            var PorcentagemQuebra = PorcentagemQuebraFake.Generate();

            _appServiceMock.Setup(c => c.Update(PorcentagemQuebra)).Returns(Task.FromResult<bool>(true));

            //Act
            var returns = _appServiceMock.Object.Update(PorcentagemQuebra);

            //Assert
            Assert.IsTrue(returns.Result);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListPorcentagemQuebraWithMoq()
        {
            //Arrange
            var PorcentagemQuebraFake = new Faker<PorcentagemQuebraDto>("pt-BR")
                .CustomInstantiator(f => new PorcentagemQuebraDto(
                    Guid.NewGuid(),
                  f.IndexVariable,
                    true,
                    Guid.NewGuid(),
                    null));

            IEnumerable<PorcentagemQuebraDto> PorcentagemQuebra = PorcentagemQuebraFake.Generate(10).ToList();

            //Act
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult(PorcentagemQuebra));
            var returns = await _appServiceMock.Object.GetAllAsync();

            //Assert
            Assert.IsNotNull(returns);
            Assert.IsTrue(returns.Any());
        }

        [Test]
        public async Task GetAsync_ShouldReturnOnePorcentagemQuebraWithMoq()
        {
            //Arrange
            var porcentagemQuebraId = Guid.NewGuid();
            var PorcentagemQuebraFake = new Faker<PorcentagemQuebraDto>("pt-BR")
                .CustomInstantiator(f => new PorcentagemQuebraDto(
                    porcentagemQuebraId,
                   f.IndexVariable,
                    true,
                    Guid.NewGuid(),
                    null));

            var PorcentagemQuebraCultura = PorcentagemQuebraFake.Generate();


            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(porcentagemQuebraId))).Returns(Task.FromResult(PorcentagemQuebraCultura));

            //Act
            var returns = await _appServiceMock.Object.GetAsync(g => g.ID.Equals(porcentagemQuebraId));

            //Assert
            Assert.IsNotNull(returns);
            Assert.AreEqual(porcentagemQuebraId, returns.ID);
        }
    }
}