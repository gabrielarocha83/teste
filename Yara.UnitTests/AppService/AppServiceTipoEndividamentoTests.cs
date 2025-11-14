using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

namespace Yara.UnitTests.AppService
{
    [TestFixture]
    [Category("AppServiceTipoEndividamento")]
    public class AppServiceTipoEndividamentoTests
    {
        private Moq.Mock<IAppServiceTipoEndividamento> moqMock;
        [OneTimeSetUpAttribute]
        public void Initializer()
        {
            moqMock = new Moq.Mock<IAppServiceTipoEndividamento>();
        }

        [Test]
        public void Insert_ShouldSaveAppServiceTipoEndividamentoWithMoq()
        {
            //Arrange

            var objTipoEndividamento = new TipoEndividamentoDto();

            moqMock.Setup(c => c.Insert(objTipoEndividamento)).Returns(true);

            //Act
            var returns = moqMock.Object.Insert(objTipoEndividamento);

            //Assert
            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateTipoEndividamentoWithMoq()
        {
            //Arrange

            var objTipoEndividamento = new TipoEndividamentoDto();

            moqMock.Setup(c => c.Update(objTipoEndividamento)).Returns(Task.FromResult<bool>(true));

            //Act
            var returns = moqMock.Object.Update(objTipoEndividamento);

            //Assert
            Assert.IsTrue(returns.Result);
        }

        [Test]
        public void GetAsync_ShouldReturnOneTipoEndividamentoWithMoq()
        {
            //Arrange


            var objTipoEndividamento = new TipoEndividamentoDto();
            var ID = Guid.NewGuid();
            moqMock.Setup(c => c.GetAsync(g => g.ID.Equals(ID))).Returns(Task.FromResult<TipoEndividamentoDto>(objTipoEndividamento));

            //Act
            var returns = moqMock.Object.GetAsync(g => g.ID.Equals(ID));

            //Assert
            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAllAsync_ShouldReturnListTipoEndividamentoWithMoq()
        {
            //Arrange


            var objTipoEndividamento = new List<TipoEndividamentoDto>();
            var ID = Guid.NewGuid();
            moqMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<TipoEndividamentoDto>>(objTipoEndividamento));

            //Act
            var returns = moqMock.Object.GetAllAsync();

            //Assert
            Assert.IsNotNull(returns);
        }
    }
}
