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
    [Category("Permissão")]
    public class PermissaoTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [TestFixtureSetUp]
        public void Initializer()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }
        [Test]
        public void ShouldSavePermissaoWithMoq()
        {
            //Arrange


            var objPermissao = new Permissao();

            _unitOfWorkMock.Setup(c => c.PermissaoRepository.Insert(objPermissao));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            //Act
            _unitOfWorkMock.Object.PermissaoRepository.Insert(objPermissao);

            //Assert
            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }
        [Test]
        public void ShouldUpdatePermissaoWithMoq()
        {
            //Arrange


            var objPermissao = new Permissao();

            _unitOfWorkMock.Setup(c => c.PermissaoRepository.Update(objPermissao));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            //Act
            _unitOfWorkMock.Object.PermissaoRepository.Update(objPermissao);

            //Assert
            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }


        [Test]
        public void ShouldReturnOnePermissaoWithMoq()
        {
            //Arrange


            var objPermissao = new Permissao();
            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(c => c.PermissaoRepository.GetAsync(p=>p.ID.Equals(id))).Returns(Task.FromResult(objPermissao));

            //Act
            var returns = _unitOfWorkMock.Object.PermissaoRepository.GetAsync(p => p.ID.Equals(id));

            //Assert
            Assert.IsNotNull(returns);
        }
        [Test]
        public void ShouldReturnListWithPaginationAndOrderPermissaoWithMoq()
        {
            //Arrange


            var objPermissao = new List<Permissao>();
            for (var i = 0; i < 50; i++)
            {
                objPermissao.Add(new Permissao()
                {
                    ID = Guid.NewGuid(),
                    Ativo = true,
                    Nome = "Permissao" + i.ToString(),
                    DataCriacao = DateTime.Now.AddMinutes(i)
                });
            }



            _unitOfWorkMock.Setup(c => c.PermissaoRepository.GetAllOrderAndPaginationAsync(
                f => f.Ativo,
                o => o.DataCriacao,
                15, 0, true)).Returns(Task.FromResult(objPermissao.Take(15).Skip(0)));

            //Act
            var returns = _unitOfWorkMock.Object.PermissaoRepository.GetAllOrderAndPaginationAsync(
                f => f.Ativo,
                o => o.DataCriacao,
                15, 0, true);

            //Assert
            Assert.IsNotNull(returns);
            Assert.AreEqual(returns.Result.Count(), 15);
        }
    }
}