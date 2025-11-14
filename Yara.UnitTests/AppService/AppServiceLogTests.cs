using Moq;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;


namespace Yara.UnitTests.AppService
{
    [TestFixture]
    [Category("AppService_Log")]
    public class AppServiceLogTests
    {
        private  Moq.Mock<IAppServiceLog> moqLog;

        [OneTimeSetUpAttribute]
        public void Initializer()
        {
            moqLog = new Mock<IAppServiceLog>();
        }
        [Test]
        public void Create_ShouldSaveAppServiceLogWithMoq()
        {
            //Arrange
           
            var objectLog = new LogDto()
            {
                ID = new System.Guid()
                ,
                IP = "IP",
                Navegador = "Navegador",
                Pagina = "Pagina",
                Idioma = "idioma"
            };

            moqLog.Setup(c => c.Create(objectLog)).Returns(true);

            //Act
            var returns = moqLog.Object.Create(objectLog);

            //Assert
            Assert.IsTrue(returns);
        }




    }
}
