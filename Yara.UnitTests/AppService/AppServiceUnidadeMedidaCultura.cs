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
    public class AppServiceUnidadeMedidaCultura
    {
        private Mock<IAppServiceUnidadeMedidaCultura> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceUnidadeMedidaCultura>();
        }

        [Test]
        public void Insert_ShouldSaveUnidadeMedidaWithMoq()
        {
            //Arrange

            var unidademedidaculturaFake = new Faker<UnidadeMedidaCulturaDto>("pt-BR")
                .CustomInstantiator(f => new UnidadeMedidaCulturaDto(
                    Guid.NewGuid(),
                    f.Name.JobArea().ToString(),
                    f.Name.JobDescriptor(),
                    true,
                    Guid.NewGuid(),
                    null));

            var unidadeMedidaCultura = unidademedidaculturaFake.Generate();

            _appServiceMock.Setup(c => c.Insert(unidadeMedidaCultura)).Returns(true);

            //Act
            var returns = _appServiceMock.Object.Insert(unidadeMedidaCultura);

            //Assert
            Assert.IsNotNull(unidadeMedidaCultura);
            Assert.IsTrue(returns);

        }

        [Test]
        public void Update_ShouldSaveTipoGarantiaWithMoq()
        {
            //Arrange

            var unidademedidaculturaFake = new Faker<UnidadeMedidaCulturaDto>("pt-BR")
                .CustomInstantiator(f => new UnidadeMedidaCulturaDto(
                    Guid.NewGuid(),
                    f.Name.JobArea().ToString(),
                    f.Name.JobDescriptor(),
                    true,
                    Guid.NewGuid(),
                    null));

            var unidadeMedidaCultura = unidademedidaculturaFake.Generate();

            _appServiceMock.Setup(c => c.Update(unidadeMedidaCultura)).Returns(Task.FromResult<bool>(true));

            //Act
            var returns = _appServiceMock.Object.Update(unidadeMedidaCultura);

            //Assert
            Assert.IsTrue(returns.Result);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListTipoGarantiaWithMoq()
        {
            //Arrange
            var unidademedidaculturaFake = new Faker<UnidadeMedidaCulturaDto>("pt-BR")
                .CustomInstantiator(f => new UnidadeMedidaCulturaDto(
                    Guid.NewGuid(),
                    f.Name.JobArea().ToString(),
                    f.Name.JobDescriptor(),
                    true,
                    Guid.NewGuid(),
                    null));

            IEnumerable<UnidadeMedidaCulturaDto> unidadeMedidaCulturas = unidademedidaculturaFake.Generate(10).ToList();

            //Act
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult(unidadeMedidaCulturas));
            var returns = await _appServiceMock.Object.GetAllAsync();

            //Assert
            Assert.IsNotNull(returns);
            Assert.IsTrue(returns.Any());
        }

        [Test]
        public async Task GetAsync_ShouldReturnOneTipoGarantiaWithMoq()
        {
            //Arrange
            var UnidadeMedidaCulturaID = Guid.NewGuid();
            var unidademedidaculturaFake = new Faker<UnidadeMedidaCulturaDto>("pt-BR")
                .CustomInstantiator(f => new UnidadeMedidaCulturaDto(
                    UnidadeMedidaCulturaID,
                    f.Name.JobArea().ToString(),
                    f.Name.JobDescriptor(),
                    true,
                    Guid.NewGuid(),
                    null));

            var unidadeMedidaCultura = unidademedidaculturaFake.Generate();


            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(UnidadeMedidaCulturaID))).Returns(Task.FromResult(unidadeMedidaCultura));

            //Act
            var returns = await _appServiceMock.Object.GetAsync(g => g.ID.Equals(UnidadeMedidaCulturaID));

            //Assert
            Assert.IsNotNull(returns);
            Assert.AreEqual(UnidadeMedidaCulturaID, returns.ID);
        }
    }
}