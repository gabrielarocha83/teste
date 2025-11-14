using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

namespace Yara.UnitTests.AppService
{
    [TestFixture]
    [Category("AppServiceFerias")]
    public class AppServiceFeriasTests
    {
        private Moq.Mock<IAppServiceFerias> moqMock;
        [OneTimeSetUpAttribute]
        public void Initializer()
        {
            moqMock = new Moq.Mock<IAppServiceFerias>();
        }

        [Test]
        public void Insert_ShouldSaveAppServiceFeriasWithMoq()
        {
            //Arrange

            var objFerias = new FeriasDto();

            moqMock.Setup(c => c.Insert(objFerias)).Returns(true);

            //Act
            var returns = moqMock.Object.Insert(objFerias);

            //Assert
            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateFeriasWithMoq()
        {
            //Arrange

            var objFerias = new FeriasDto();

            moqMock.Setup(c => c.Update(objFerias)).Returns(Task.FromResult<bool>(true));

            //Act
            var returns = moqMock.Object.Update(objFerias);

            //Assert
            Assert.IsTrue(returns.Result);
        }

        [Test]
        public void GetAsync_ShouldReturnOneFeriasWithMoq()
        {
            //Arrange


            var objFerias = new FeriasDto();
            var ID = Guid.NewGuid();
            moqMock.Setup(c => c.GetAsync(g => g.ID.Equals(ID))).Returns(Task.FromResult<FeriasDto>(objFerias));

            //Act
            var returns = moqMock.Object.GetAsync(g => g.ID.Equals(ID));

            //Assert
            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAllAsync_ShouldReturnListFeriasWithMoq()
        {
            //Arrange


            var objFerias = new List<FeriasDto>();
            var ID = Guid.NewGuid();
            moqMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<FeriasDto>>(objFerias));

            //Act
            var returns = moqMock.Object.GetAllAsync();

            //Assert
            Assert.IsNotNull(returns);
        }
    }
}
