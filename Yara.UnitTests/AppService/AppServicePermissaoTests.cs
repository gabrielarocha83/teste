using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

namespace Yara.UnitTests.AppService
{
    
    [TestFixture]
    [Category("AppService_Permissao")]
    public class AppServicePermissaoTests
    {
        private Moq.Mock<IAppServicePermissao> moqPermissao;
        [OneTimeSetUpAttribute]
        public void Initializer()
        {
             moqPermissao = new Moq.Mock<IAppServicePermissao>();
        }

        [Test]
        public void Insert_ShouldSaveAppServicePermissaoWithMoq()
        {
            //Arrange

            var objPermissao = new PermissaoDto();

            moqPermissao.Setup(c => c.Insert(objPermissao)).Returns(true);

            //Act
            var returns = moqPermissao.Object.Insert(objPermissao);

            //Assert
            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldSaveGrupoWithMoq()
        {
            //Arrange

            var objPermissao = new PermissaoDto();

            moqPermissao.Setup(c => c.Update(objPermissao)).Returns(Task.FromResult<bool>(true));

            //Act
            var returns = moqPermissao.Object.Update(objPermissao);

            //Assert
            Assert.IsTrue(returns.Result);
        }

      

        //[Test]
        //public void GetAsync_ShouldReturnOneGrupoWithMoq()
        //{
        //    //Arrange
        //    var objPermissao = new PermissaoDto();
        //    var PermissaoID = Guid.NewGuid();
        //    moqPermissao.Setup(c => c.GetAsync(g=>g.ID.Equals(PermissaoID))).Returns(Task.FromResult<PermissaoDto>(objPermissao));

        //    //Act
        //    var returns = moqPermissao.Object.GetAsync(g => g.ID.Equals(PermissaoID));

        //    //Assert
        //    Assert.IsNotNull(returns);
        //}

        [Test]
        public void GetAllAsync_ShouldReturnListPermissaoWithMoq()
        {
            //Arrange
            var objPermissao = new List<PermissaoDto>();
            var ID = Guid.NewGuid();
            moqPermissao.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<PermissaoDto>>(objPermissao));

            //Act
            var returns = moqPermissao.Object.GetAllAsync();

            //Assert
            Assert.IsNotNull(returns);
        }
    }
}