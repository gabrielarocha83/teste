using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

namespace Yara.UnitTests.AppService
{
    
    [TestFixture]
    [Category("AppServiceTipoPecuaria")]
    public class AppServiceTipoPecuariaTests
    {
        private Moq.Mock<IAppServiceTipoPecuaria> moqMock;
        [OneTimeSetUpAttribute]
        public void Initializer()
        {
             moqMock = new Moq.Mock<IAppServiceTipoPecuaria>();
        }

        [Test]
        public void Insert_ShouldSaveAppServiceTipoPecuariaWithMoq()
        {
            //Arrange

            var objTipoPecuaria = new TipoPecuariaDto();

            moqMock.Setup(c => c.Insert(objTipoPecuaria)).Returns(true);

            //Act
            var returns =  moqMock.Object.Insert(objTipoPecuaria);

            //Assert
            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateTipoPecuariaWithMoq()
        {
            //Arrange

            var objTipoPecuaria = new TipoPecuariaDto();

            moqMock.Setup(c => c.Update(objTipoPecuaria)).Returns(Task.FromResult<bool>(true));

            //Act
            var returns =  moqMock.Object.Update(objTipoPecuaria);

            //Assert
            Assert.IsTrue(returns.Result);
        }

        [Test]
        public void GetAsync_ShouldReturnOneTipoPecuariaWithMoq()
        {
            //Arrange
          

            var objTipoPecuaria = new TipoPecuariaDto();
            var ID = Guid.NewGuid();
            moqMock.Setup(c => c.GetAsync(g=>g.ID.Equals(ID))).Returns(Task.FromResult<TipoPecuariaDto>(objTipoPecuaria));

            //Act
            var returns = moqMock.Object.GetAsync(g => g.ID.Equals(ID));

            //Assert
            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAllAsync_ShouldReturnListTipoPecuariaWithMoq()
        {
            //Arrange
           

            var objTipoPecuaria = new List<TipoPecuariaDto>();
            var ID = Guid.NewGuid();
            moqMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<TipoPecuariaDto>>(objTipoPecuaria));

            //Act
            var returns = moqMock.Object.GetAllAsync();

            //Assert
            Assert.IsNotNull(returns);
        }

    }
}