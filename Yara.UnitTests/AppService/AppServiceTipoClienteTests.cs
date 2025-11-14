using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

namespace Yara.UnitTests.AppService
{
    
    [TestFixture]
    [Category("AppService_Log")]
    public class AppServiceTipoClienteTests
    {
        private Moq.Mock<IAppServiceTipoCliente> moqTipoCliente;
        [OneTimeSetUpAttribute]
        public void Initializer()
        {
             moqTipoCliente = new Moq.Mock<IAppServiceTipoCliente>();
        }

        [Test]
        public void Insert_ShouldSaveAppServiceTipoClienteWithMoq()
        {
            //Arrange

            var objTipoCliente = new TipoClienteDto();

            moqTipoCliente.Setup(c => c.Insert(objTipoCliente)).Returns(true);

            //Act
            var returns =  moqTipoCliente.Object.Insert(objTipoCliente);

            //Assert
            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateTipoClienteWithMoq()
        {
            //Arrange

            var objTipoCLiente = new TipoClienteDto();

            moqTipoCliente.Setup(c => c.Update(objTipoCLiente)).Returns(Task.FromResult<bool>(true));

            //Act
            var returns =  moqTipoCliente.Object.Update(objTipoCLiente);

            //Assert
            Assert.IsTrue(returns.Result);
        }

        [Test]
        public void GetAsync_ShouldReturnOneTipoClienteWithMoq()
        {
            //Arrange
          

            var objTipoCliente = new TipoClienteDto();
            var ID = Guid.NewGuid();
            moqTipoCliente.Setup(c => c.GetAsync(g=>g.ID.Equals(ID))).Returns(Task.FromResult<TipoClienteDto>(objTipoCliente));

            //Act
            var returns = moqTipoCliente.Object.GetAsync(g => g.ID.Equals(ID));

            //Assert
            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAllAsync_ShouldReturnListTipoClienteWithMoq()
        {
            //Arrange
           

            var objTipoCliente = new List<TipoClienteDto>();
            var ID = Guid.NewGuid();
            moqTipoCliente.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<TipoClienteDto>>(objTipoCliente));

            //Act
            var returns = moqTipoCliente.Object.GetAllAsync();

            //Assert
            Assert.IsNotNull(returns);
        }

    }
}