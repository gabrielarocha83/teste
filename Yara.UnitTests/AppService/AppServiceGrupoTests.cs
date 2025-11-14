using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

namespace Yara.UnitTests.AppService
{
    
    [TestFixture]
    [Category("AppService_Log")]
    public class AppServiceGrupoTests
    {
        private Moq.Mock<IAppServiceGrupo> moqGrupo;
        [OneTimeSetUpAttribute]
        public void Initializer()
        {
             moqGrupo = new Moq.Mock<IAppServiceGrupo>();
        }

        [Test]
        public  void Insert_ShouldSaveAppServiceGrupoWithMoq()
        {
            //Arrange

            var objGrupo = new GrupoDto();

            moqGrupo.Setup(c => c.Insert(objGrupo)).Returns(true);

            //Act
            var returns =  moqGrupo.Object.Insert(objGrupo);

            //Assert
            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldSaveGrupoWithMoq()
        {
            //Arrange

            var objGrupo = new GrupoDto();

            moqGrupo.Setup(c => c.Update(objGrupo)).Returns(Task.FromResult<bool>(true));

            //Act
            var returns =  moqGrupo.Object.Update(objGrupo);

            //Assert
            Assert.IsTrue(returns.Result);
        }

       

        [Test]
        public void GetAsync_ShouldReturnOneGrupoWithMoq()
        {
            //Arrange
          

            var objGrupo = new GrupoDto();
            var ID = Guid.NewGuid();
            moqGrupo.Setup(c => c.GetAsync(g=>g.ID.Equals(ID))).Returns(Task.FromResult<GrupoDto>(objGrupo));

            //Act
            var returns = moqGrupo.Object.GetAsync(g => g.ID.Equals(ID));

            //Assert
            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAllAsync_ShouldReturnListGrupoWithMoq()
        {
            //Arrange
           

            var objGrupo = new List<GrupoDto>();
            var ID = Guid.NewGuid();
            moqGrupo.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<GrupoDto>>(objGrupo));

            //Act
            var returns = moqGrupo.Object.GetAllAsync();

            //Assert
            Assert.IsNotNull(returns);
        }

       
    }
}