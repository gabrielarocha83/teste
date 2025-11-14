using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.UnitTests.Repository
{
    [TestFixture]
    [Category("ContaClienteComentario")]
    public class ContaClienteComentarioTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void ShouldReturnOneContaClienteComentarioWithMoq()
        {
            var conta = new ContaClienteComentario();

            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(c => c.ContaClienteComentarioRepository.GetAsync(u => u.ID.Equals(id))).Returns(Task.FromResult(conta));

            var returns = _unitOfWorkMock.Object.ContaClienteComentarioRepository.GetAsync(u => u.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void ShouldReturnListContaClienteComentarioWithMoq()
        {
            var conta = new List<ContaClienteComentario>
            {
                new ContaClienteComentario
                {
                    ID = Guid.NewGuid(),
                    Ativo = true,
                    ContaClienteID = Guid.NewGuid(),
                    Descricao = "Comentário",
                    UsuarioIDCriacao = Guid.NewGuid(),
                    DataCriacao = DateTime.Now
                }
            };

            _unitOfWorkMock.Setup(c => c.ContaClienteComentarioRepository.GetAllAsync()).Returns(Task.FromResult<IEnumerable<ContaClienteComentario>>(conta));

            var returns = _unitOfWorkMock.Object.ContaClienteComentarioRepository.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void ShouldSaveContaClienteComentarioWithMoq()
        {
            var conta = new ContaClienteComentario();
            _unitOfWorkMock.Setup(c => c.ContaClienteComentarioRepository.Insert(conta));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            _unitOfWorkMock.Object.ContaClienteComentarioRepository.Insert(conta);

            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }

        [Test]
        public void ShouldUpdateContaClienteComentarioWithMoq()
        {
            var conta = new ContaClienteComentario();

            _unitOfWorkMock.Setup(c => c.ContaClienteComentarioRepository.Update(conta));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            _unitOfWorkMock.Object.ContaClienteComentarioRepository.Update(conta);

            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }
    }
}
