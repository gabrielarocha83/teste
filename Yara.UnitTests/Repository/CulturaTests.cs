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
    [Category("Cultura")]
    public class CulturaTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void ShouldSaveCulturaWithMoq()
        {
            var cultura = new Cultura();

            _unitOfWorkMock.Setup(c => c.CulturaRepository.Insert(cultura));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            _unitOfWorkMock.Object.CulturaRepository.Insert(cultura);

            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }

        [Test]
        public void ShouldUpdateCulturaWithMoq()
        {
            var cultura = new Cultura();

            _unitOfWorkMock.Setup(c => c.CulturaRepository.Update(cultura));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            _unitOfWorkMock.Object.CulturaRepository.Update(cultura);

            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }

        [Test]
        public void ShouldReturnOneCulturaWithMoq()
        {
            var cultura = new Cultura();

            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(c => c.CulturaRepository.GetAsync(u => u.ID.Equals(id))).Returns(Task.FromResult(cultura));

            var returns = _unitOfWorkMock.Object.CulturaRepository.GetAsync(u => u.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void ShouldReturnListCulturaWithMoq()
        {
            var  lstCultura = new List<Cultura>()
            {
                new Cultura()
                {
                    ID = Guid.NewGuid(),
                    Tipo = "avicultura",
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    UsuarioIDCriacao = Guid.NewGuid()
                },
                new Cultura()
                {
                    ID = Guid.NewGuid(),
                    Tipo = "suinocultura",
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    UsuarioIDCriacao = Guid.NewGuid()
                },
                new Cultura()
                {
                    ID = Guid.NewGuid(),
                    Tipo = "sericultura",
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    UsuarioIDCriacao = Guid.NewGuid()
                }
            };
            _unitOfWorkMock.Setup(c => c.CulturaRepository.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Cultura>>(lstCultura));
            var returns = _unitOfWorkMock.Object.CulturaRepository.GetAllAsync();
            Assert.IsNotNull(returns);
        }
    }
}
