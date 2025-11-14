using Moq;
using NUnit.Framework;
using Yara.Domain.Entities;
using Yara.Domain.Repository;
#pragma warning disable 618

namespace Yara.UnitTests.Repository
{
    [TestFixture]
    [Category("Log")]
    public class LogTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [TestFixtureSetUp]
        public void Initializer()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }
       [Test]
        public void ShouldSaveLogWithMoq()
        {
            //Arrange
          

            var objectLog = new Log()
            {
                ID = new System.Guid()
               ,IP = "IP",
                Navegador = "Navegador",
                Pagina = "Pagina",
                Idioma = "idioma"
            };

            _unitOfWorkMock.Setup(c => c.LogRepository.Insert(objectLog));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);
            //Act
            _unitOfWorkMock.Object.LogRepository.Insert(objectLog);

            //Assert
            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }

       


    }
}
