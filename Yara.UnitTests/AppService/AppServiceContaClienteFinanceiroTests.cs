using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

namespace Yara.UnitTests.AppService
{
    public class AppServiceContaClienteFinanceiroTests
    {
        private Mock<IAppServiceContaClienteFinanceiro> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceContaClienteFinanceiro>();
        }

        [Test]
        public void GetAsync_ShouldGetContaClienteFinanceiroWithContaClienteIDMoq()
        {
            //Arrange
            var contaClienteId = Guid.NewGuid();
            var contaClienteFinanceiroReturn = new ContaClienteFinanceiroDto { ContaClienteID = contaClienteId };


            _appServiceMock.Setup(c => c.GetAsync(f => f.ContaClienteID.Equals(contaClienteId))).Returns(Task.FromResult<ContaClienteFinanceiroDto>(contaClienteFinanceiroReturn));

            //Act
            var Obj = _appServiceMock.Object.GetAsync(
                c => c.ContaClienteID.Equals(contaClienteId));

            //Assert
            Assert.AreEqual(contaClienteId, Obj.Result.ContaClienteID);

        }

        [Test]
        public async Task Update_ShouldSaveClienteFinanceiroConceitoWithMoq()
        {
            //Arrange
            var objContaCliente = new ContaClienteFinanceiroDto
            {
                Conceito = true
            };

            _appServiceMock.Setup(c => c.Update(objContaCliente)).Returns(Task.FromResult<bool>(true));

            //Act
            var boolUpdate = await _appServiceMock.Object.Update(objContaCliente);

            //Assert
            Assert.IsTrue(boolUpdate);

        }
    }
}