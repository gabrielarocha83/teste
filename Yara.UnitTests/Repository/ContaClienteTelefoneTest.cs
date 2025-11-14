using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Yara.Domain.Entities;
using Yara.Domain.Repository;
#pragma warning disable 618

namespace Yara.UnitTests.Repository
{
    public class ContaClienteTelefoneTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [TestFixtureSetUp]
        public void Initializer()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public async  Task ShouldListPhonewithAccountClientId()
        {
            //Arrange
            var contaclienteId = Guid.NewGuid();

            var objContaClienteTelefone = new List<ContaClienteTelefone>
            {
                new ContaClienteTelefone
                {
                    ID = Guid.NewGuid(),
                    Ativo = true,
                    ContaClienteID = Guid.NewGuid(),
                    Telefone = "1991552255",
                    UsuarioIDCriacao = Guid.NewGuid(),
                    Tipo = TipoTelefone.Celular,
                    DataCriacao = DateTime.Now
                }
            };



            _unitOfWorkMock.Setup(
                c => c.ContaClienteTelefoneRepository.GetAllFilterAsync(d => d.ContaClienteID.Equals(contaclienteId))).Returns(Task.FromResult<IEnumerable<ContaClienteTelefone>>(objContaClienteTelefone));


            //Act
           var listcontaclientetelefone =  await _unitOfWorkMock.Object.ContaClienteTelefoneRepository.GetAllFilterAsync(
                c => c.ContaClienteID.Equals(contaclienteId));
            //Assert
            Assert.IsTrue(listcontaclientetelefone.Any());
        }

        [Test]
        public void ShouldInsertPhoneInAccountClient()
        {
            //Arrange
            var objContaClienteTelefone = new ContaClienteTelefone
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                ContaClienteID = Guid.NewGuid(),
                Telefone = "1991552255",
                UsuarioIDCriacao = Guid.NewGuid(),
                Tipo = TipoTelefone.Celular,
                DataCriacao = DateTime.Now
            };



            _unitOfWorkMock.Setup(c => c.ContaClienteTelefoneRepository.Update(objContaClienteTelefone));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            //Act
            _unitOfWorkMock.Object.ContaClienteTelefoneRepository.Update(objContaClienteTelefone);

            //Assert
            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }
    }
}