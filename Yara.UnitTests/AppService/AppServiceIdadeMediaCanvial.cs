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
    public class AppServiceIdadeMediaCanvial
    {
        private Mock<IAppServiceIdadeMediaCanavial> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceIdadeMediaCanavial>();
        }

        [Test]
        public void Insert_ShouldSaveIdadeMediaCanavialWithMoq()
        {
            //Arrange

            var idademediacanvialFake = new Faker<IdadeMediaCanavialDto>("pt-BR")
                .CustomInstantiator(f => new IdadeMediaCanavialDto(
                    Guid.NewGuid(),
                    f.Name.JobArea().ToString(),
                    true,
                    Guid.NewGuid(), 
                    null));

            var oneIdadeMediaCanavial = idademediacanvialFake.Generate();

            _appServiceMock.Setup(c => c.Insert(oneIdadeMediaCanavial)).Returns(true);

            //Act
            var returns = _appServiceMock.Object.Insert(oneIdadeMediaCanavial);

            //Assert
            Assert.IsNotNull(oneIdadeMediaCanavial);
            Assert.IsTrue(returns);

        }

        [Test]
        public void Update_ShouldSaveIdadeMediaCanavialWithMoq()
        {
            //Arrange

            var idademediacanvialFake = new Faker<IdadeMediaCanavialDto>("pt-BR")
                .CustomInstantiator(f => new IdadeMediaCanavialDto(
                    Guid.NewGuid(),
                    f.Name.JobArea().ToString(),
                    true,
                    Guid.NewGuid()
                    ,null));

            var idadeMediaCanavial = idademediacanvialFake.Generate();

            _appServiceMock.Setup(c => c.Update(idadeMediaCanavial)).Returns(Task.FromResult(true));

            //Act
            var returns = _appServiceMock.Object.Update(idadeMediaCanavial);

            //Assert
            Assert.IsTrue(returns.Result);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListIdadeMediaCanavialWithMoq()
        {
            //Arrange
            var idademediacanvialFake = new Faker<IdadeMediaCanavialDto>("pt-BR")
                .CustomInstantiator(f => new IdadeMediaCanavialDto(
                    Guid.NewGuid(),
                    f.Name.JobArea().ToString(),
                    true,
                    Guid.NewGuid()
                    , null));

            IEnumerable<IdadeMediaCanavialDto> cultList = idademediacanvialFake.Generate(10).ToList();

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
            var idadeMediaCanavialId = Guid.NewGuid();
            var idademediacanvialFake = new Faker<IdadeMediaCanavialDto>("pt-BR")
                .CustomInstantiator(f => new IdadeMediaCanavialDto(
                    idadeMediaCanavialId,
                    f.Name.JobArea().ToString(),
                    true,
                    Guid.NewGuid()
                    , null));

            var oneIdadeMediaCanavialFake = idademediacanvialFake.Generate();

           
            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(idadeMediaCanavialId))).Returns(Task.FromResult(oneIdadeMediaCanavialFake));

            //Act
            var returns =await _appServiceMock.Object.GetAsync(g => g.ID.Equals(idadeMediaCanavialId));

            //Assert
            Assert.IsNotNull(returns);
            Assert.AreEqual(idadeMediaCanavialId, returns.ID);
        }


    }
}