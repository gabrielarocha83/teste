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
    [Category("Experiencia")]
    public class ExperienciaTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void ShouldReturnListExperienciaWithMoq()
        {
            var experiencias = new List<Experiencia>();
            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(c => c.ExperienciaRepository.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Experiencia>>(experiencias));

            var returns = _unitOfWorkMock.Object.ExperienciaRepository.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void ShouldReturnOneExperienciaWithMoq()
        {
            var experiencia = new Experiencia();

            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(c => c.ExperienciaRepository.GetAsync(u => u.ID.Equals(id))).Returns(Task.FromResult<Experiencia>(experiencia));

            var returns = _unitOfWorkMock.Object.ExperienciaRepository.GetAsync(u => u.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void ShouldUpdateExperienciaWithMoq()
        {
            var experiencia = new Experiencia();

            _unitOfWorkMock.Setup(c => c.ExperienciaRepository.Update(experiencia));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            _unitOfWorkMock.Object.ExperienciaRepository.Update(experiencia);

            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }

        [Test]
        public void ShouldSaveExperienciaWithMoq()
        {
            var experiencia = new Experiencia();

            _unitOfWorkMock.Setup(c => c.ExperienciaRepository.Insert(experiencia));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            _unitOfWorkMock.Object.ExperienciaRepository.Insert(experiencia);

            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }
    }
}
