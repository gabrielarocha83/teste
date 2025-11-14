using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using Moq;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
// ReSharper disable InconsistentNaming

namespace Yara.UnitTests.AppService
{
    [TestFixture]
    [Category("AppServiceContaClienteTelefone")]
    public class AppServiceContaClienteTelefoneTest
    {
        private Mock<IAppServiceContaClienteTelefone> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceContaClienteTelefone>();
        }

        [Test]
        public void GetAllFilterAsync_ShouldListPhonewithAccountClientId()
        {
            //Arrange
            var contaclienteId = Guid.NewGuid();
            var contaClienteTelefone = new List<ContaClienteTelefoneDto>
            {
                new ContaClienteTelefoneDto()
                {
                    ID = Guid.NewGuid(),
                    Ativo = true,
                    ContaClienteID =contaclienteId,
                    Telefone = "1991552255",
                    UsuarioIDCriacao = Guid.NewGuid(),
                    Tipo = TipoTelefoneDto.Celular,
                    DataCriacao = DateTime.Now
                }
            };
            //Act
           

            _appServiceMock.Setup(c => c.GetAllFilterAsync(g => g.ContaClienteID.Equals(contaclienteId))).Returns(Task.FromResult<IEnumerable<ContaClienteTelefoneDto>>(contaClienteTelefone));

            var returns = _appServiceMock.Object.GetAllFilterAsync(g => g.ContaClienteID.Equals(contaclienteId));

            //Assert
            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldInsertPhoneInAccountClient()
        {
            //Arrange
            var objContaClienteTelefone = new ContaClienteTelefoneDto()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                ContaClienteID = Guid.NewGuid(),
                Telefone = "1991552255",
                UsuarioIDCriacao = Guid.NewGuid(),
                Tipo = TipoTelefoneDto.Celular,
                DataCriacao = DateTime.Now
            };



            _appServiceMock.Setup(c => c.Insert(objContaClienteTelefone)).Returns(true);
          

            //Act
           var update =   _appServiceMock.Object.Insert(objContaClienteTelefone);

            //Assert
            Assert.IsTrue(update);
        }

        [Test]
        public async Task Inactive_ShouldInactiveOnePhoneInAccountClientPhone()
        {
            //Arrange
            var objContaClienteTelefone = new ContaClienteTelefoneDto()
            {
                ID = Guid.NewGuid(),
                ContaClienteID = Guid.NewGuid(),
                Telefone = "1991552255"
            };

            //Act
            _appServiceMock.Setup(c => c.Inactive(objContaClienteTelefone)).Returns(Task.FromResult(true));
            var inactive = await _appServiceMock.Object.Inactive(objContaClienteTelefone);

            //Assert
            Assert.IsTrue(inactive);
        }

        [Test]
        public async Task InsertOrUpdateManyAsync_ShoulInsertOrUpdateAccountClientePhone()
        {
            //Arrange
            Guid contaclienteID = Guid.NewGuid();

            var contaclienteTelefoneFake = new Faker<ContaClienteTelefoneDto>()
                .RuleFor(c => c.Ativo, f => Convert.ToBoolean(f.Random.Number(1, 0)))
                .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber("(###)####-####"))
                .RuleFor(c => c.ContaClienteID, contaclienteID)
                .RuleFor(c => c.Tipo, TipoTelefoneDto.Fixo)
                .RuleFor(c => c.DataCriacao, DateTime.Now)
                .RuleFor(c => c.UsuarioIDCriacao, Guid.NewGuid);


            var contaClienteTelefones = contaclienteTelefoneFake.Generate(10);
            //Act
            _appServiceMock.Setup(c => c.InsertOrUpdateManyAsync(contaClienteTelefones)).Returns(Task.FromResult(true));
            var actionInsertOrUpdatePhone = await  _appServiceMock.Object.InsertOrUpdateManyAsync(contaClienteTelefones);

            //Assert
            Assert.IsTrue(actionInsertOrUpdatePhone);
        

        }
    }
}