using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

namespace Yara.UnitTests.AppService
{
    public class AppServiceContaClienteTests
    {
        private Mock<IAppServiceContaCliente> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceContaCliente>();
        }

        [Test]
        public async Task Update_ShouldSaveEnderecoTelefoneNomeContaClienteWithMoq()
        {
            //Arrange
            var objContaCliente = new ContaClienteDto
            {
                Nome = "Claudemir",
                Apelido = "Junior",
                Endereco = "Rua Maria Mercedes 222, BL C AP 201",
                CEP = "13044-555",
                Cidade = "Campinas",
                UF = "SP"
            };

            _appServiceMock.Setup(c => c.Update(objContaCliente)).Returns(Task.FromResult<bool>(true));

            //Act
            var boolUpdate = await _appServiceMock.Object.Update(objContaCliente);

            //Assert
            Assert.IsTrue(boolUpdate);

        }
    }
}