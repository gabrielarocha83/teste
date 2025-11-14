using Moq;
using NUnit.Framework;
using Yara.Domain.Entities;
using Yara.Domain.Repository;
#pragma warning disable 618

namespace Yara.UnitTests.Repository
{
    [TestFixture]
    [Category("ContaCliente")]
    public class ContaClienteTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [TestFixtureSetUp]
        public void Initializer()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void ShouldUpdateBasicDataWithMoq()
        {
            //Arrange
            var objContaCliente = new ContaCliente
            {
                Nome = "Claudemir",
                Apelido = "Junior",
                Endereco = "Rua Maria Mercedes 222, BL C AP 201",
                CEP = "13044-555",
                Cidade = "Campinas",
                UF = "SP"
            };

            _unitOfWorkMock.Setup(c => c.ContaClienteRepository.Update(objContaCliente));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            //Act
            _unitOfWorkMock.Object.ContaClienteRepository.Update(objContaCliente);

            //Assert
            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
            
        }
    }
}