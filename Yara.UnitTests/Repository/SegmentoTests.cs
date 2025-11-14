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
    [Category("Segmento")]
    public class SegmentoTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void ShouldReturnOneSegmentoWithMoq()
        {
            var segmento = new Segmento();

            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(c => c.SegmentoRepository.GetAsync(u => u.ID.Equals(id))).Returns(Task.FromResult(segmento));

            var returns = _unitOfWorkMock.Object.SegmentoRepository.GetAsync(u => u.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void ShouldReturnListSegmentoWithMoq()
        {
            var segmento = new List<Segmento>
            {
                new Segmento
                {
                    ID = Guid.NewGuid(),
                    Descricao = "Segmento",
                    DataCriacao = DateTime.Now,
                    UsuarioIDCriacao = Guid.NewGuid()
                    
                }
            };
            _unitOfWorkMock.Setup(c => c.SegmentoRepository.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Segmento>>(segmento));

            var returns = _unitOfWorkMock.Object.SegmentoRepository.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void ShouldSaveSegmentoWithMoq()
        {
            var segmento = new Segmento();
            _unitOfWorkMock.Setup(c => c.SegmentoRepository.Insert(segmento));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            _unitOfWorkMock.Object.SegmentoRepository.Insert(segmento);

            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }

        [Test]
        public void ShouldUpdateSegmentoWithMoq()
        {
            var segmento = new Segmento();

            _unitOfWorkMock.Setup(c => c.SegmentoRepository.Update(segmento));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            _unitOfWorkMock.Object.SegmentoRepository.Update(segmento);

            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }
    }
}
