using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Yara.Domain.Entities;
using Yara.Domain.Repository;
#pragma warning disable 618

namespace Yara.UnitTests.Repository
{
    [TestFixture]
    [Category("ContaClienteFinaceiro")]
    public class ContaClienteFinanceiroTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [TestFixtureSetUp]
        public void Initializer()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void ShouldGetContaClienteFinanceiroWithContaClienteIdMoq()
        {
            //Arrange
            var contaClienteId = Guid.NewGuid();
            var contaClienteFinanceiroReturn = new ContaClienteFinanceiro{ContaClienteID = contaClienteId};
           

            _unitOfWorkMock.Setup(c => c.ContaClienteFinanceiroRepository.GetAsync(f=>f.ContaClienteID.Equals(contaClienteId))).Returns(Task.FromResult(contaClienteFinanceiroReturn));

            //Act
          var obj =  _unitOfWorkMock.Object.ContaClienteFinanceiroRepository.GetAsync(
                c => c.ContaClienteID.Equals(contaClienteId));

            //Assert
            Assert.AreEqual(contaClienteId, obj.Result.ContaClienteID);
            
        }
    }
}