using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

namespace Yara.UnitTests.AppService
{
    [TestFixture]
    [Category("AppServiceFeriado")]
    public class AppServiceFeriadoTests
    {
        private Moq.Mock<IAppServiceFeriado> moqMock;
        [OneTimeSetUpAttribute]
        public void Initializer()
        {
            moqMock = new Moq.Mock<IAppServiceFeriado>();
        }

        [Test]
        public void Insert_ShouldSaveAppServiceFeriadoWithMoq()
        {
            //Arrange

            var objFeriado = new FeriadoDto();

            moqMock.Setup(c => c.Insert(objFeriado)).Returns(true);

            //Act
            var returns = moqMock.Object.Insert(objFeriado);

            //Assert
            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateFeriadoWithMoq()
        {
            //Arrange

            var objFeriado = new FeriadoDto();

            moqMock.Setup(c => c.Update(objFeriado)).Returns(Task.FromResult<bool>(true));

            //Act
            var returns = moqMock.Object.Update(objFeriado);

            //Assert
            Assert.IsTrue(returns.Result);
        }

        [Test]
        public void GetAsync_ShouldReturnOneFeriadoWithMoq()
        {
            //Arrange


            var objFeriado = new FeriadoDto();
            var ID = Guid.NewGuid();
            moqMock.Setup(c => c.GetAsync(g => g.ID.Equals(ID))).Returns(Task.FromResult<FeriadoDto>(objFeriado));

            //Act
            var returns = moqMock.Object.GetAsync(g => g.ID.Equals(ID));

            //Assert
            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAllAsync_ShouldReturnListFeriadoWithMoq()
        {
            //Arrange


            var objFeriado = new List<FeriadoDto>();
            var ID = Guid.NewGuid();
            moqMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<FeriadoDto>>(objFeriado));

            //Act
            var returns = moqMock.Object.GetAllAsync();

            //Assert
            Assert.IsNotNull(returns);
        }
    }
}
