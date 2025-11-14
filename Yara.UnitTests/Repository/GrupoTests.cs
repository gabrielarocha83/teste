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
    [TestFixture]
    [Category("Grupo")]
    public class GrupoTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [TestFixtureSetUp]
        public void Initializer()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void ShouldSaveGrupoWithMoq()
        {
            //Arrange
           

            var objGrupo = new Grupo();

            _unitOfWorkMock.Setup(c => c.GrupoRepository.Insert(objGrupo));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            //Act
            _unitOfWorkMock.Object.GrupoRepository.Insert(objGrupo);

            //Assert
            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }

        [Test]
        public void ShouldUpdateGrupoWithMoq()
        {
            //Arrange


            var objGrupo = new Grupo();

            _unitOfWorkMock.Setup(c => c.GrupoRepository.Update(objGrupo));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            //Act
            _unitOfWorkMock.Object.GrupoRepository.Update(objGrupo);

            //Assert
            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }



        [Test]
        public void ShouldReturnOneGrupoWithMoq()
        {
            //Arrange
          

            var objGrupo = new Grupo();
            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(c => c.GrupoRepository.GetAsync(g=>g.ID.Equals(id))).Returns(Task.FromResult(objGrupo));

            //Act
            var returns = _unitOfWorkMock.Object.GrupoRepository.GetAsync(g => g.ID.Equals(id));

            //Assert
            Assert.IsNotNull(returns);
        }

        [Test]
        public void ShouldReturnListGrupoWithMoq()
        {
            //Arrange


            
            _unitOfWorkMock.Setup(c => c.GrupoRepository.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Grupo>>(new List<Grupo>()));

            //Act
            var returns = _unitOfWorkMock.Object.GrupoRepository.GetAllAsync();

            //Assert
            Assert.IsNotNull(returns);
        }

        [Test]
        public void ShouldReturnListWithPaginationAndOrderGrupoWithMoq()
        {
            //Arrange


            var objGrupo = new List<Grupo>();
            for (var i = 0; i < 50; i++)
            {
                objGrupo.Add(new Grupo()
                {
                    ID = Guid.NewGuid(),
                    Ativo = true,
                    Nome = "Grupo"+i.ToString(),
                    DataCriacao = DateTime.Now.AddMinutes(i)
                });
            }



            _unitOfWorkMock.Setup(c => c.GrupoRepository.GetAllOrderAndPaginationAsync(
                f => f.Ativo,
                o => o.DataCriacao,
                15, 0, true)).Returns(Task.FromResult(objGrupo.Take(15).Skip(0)));

            //Act
            var returns = _unitOfWorkMock.Object.GrupoRepository.GetAllOrderAndPaginationAsync(
                f => f.Ativo,
                o => o.DataCriacao,
                15, 0, true);

            //Assert
            Assert.IsNotNull(returns);
            Assert.AreEqual(returns.Result.Count(), 15);
        }
    }
}