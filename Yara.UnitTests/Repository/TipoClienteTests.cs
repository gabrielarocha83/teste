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
    public class TipoClienteTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [TestFixtureSetUp]
        public void Initializer()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void ShouldSaveTipoClienteWithMoq()
        {
            //Arrange


            var objTipoCliente = new TipoCliente();

            _unitOfWorkMock.Setup(c => c.TipoClienteRepository.Insert(objTipoCliente));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            //Act
            _unitOfWorkMock.Object.TipoClienteRepository.Insert(objTipoCliente);

            //Assert
            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }

        [Test]
        public void ShouldUpdateTipoClienteWithMoq()
        {
            //Arrange


            var objTipoCliente = new TipoCliente();

            _unitOfWorkMock.Setup(c => c.TipoClienteRepository.Update(objTipoCliente));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            //Act
            _unitOfWorkMock.Object.TipoClienteRepository.Update(objTipoCliente);

            //Assert
            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }



        [Test]
        public void ShouldReturnOneTipoClienteWithMoq()
        {
            //Arrange

            var id = Guid.NewGuid();
            var objTipoCliente = new TipoCliente()
            {
                ID = id
            };
            

            _unitOfWorkMock.Setup(c => c.TipoClienteRepository.GetAsync(g => g.ID.Equals(id))).Returns(Task.FromResult(objTipoCliente));

            //Act
            var returns = _unitOfWorkMock.Object.TipoClienteRepository.GetAsync(g => g.ID.Equals(id));

            //Assert
            Assert.AreEqual(returns.Result.ID, objTipoCliente.ID);
        }

        [Test]
        public void ShouldReturnListTipoClienteWithMoq()
        {
            //Arrange


            var objTipoCliente = new List<TipoCliente>
            {
                new TipoCliente
                {
                    ID = Guid.NewGuid(),
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    Nome = "Tipo 01",
                    UsuarioIDCriacao = Guid.NewGuid()
                }
            };
            _unitOfWorkMock.Setup(c => c.TipoClienteRepository.GetAllAsync()).Returns(Task.FromResult<IEnumerable<TipoCliente>>(objTipoCliente));

            //Act
            var returns = _unitOfWorkMock.Object.TipoClienteRepository.GetAllAsync();

            //Assert
            Assert.IsNotNull(returns);
        }

        [Test]
        public void ShouldReturnListWithPaginationAndOrderTipoClienteWithMoq()
        {
            //Arrange


            var objTipoCLientes = new List<TipoCliente>();
            for (var i = 0; i < 10; i++)
            {
                objTipoCLientes.Add(new TipoCliente()
                {
                    ID = Guid.NewGuid(),
                    Ativo = true,
                    Nome = "Grupo" + i.ToString(),
                    Descricao= i.ToString(),
                    DataCriacao = DateTime.Now.AddMinutes(i)
                });
            }



            _unitOfWorkMock.Setup(c => c.TipoClienteRepository.GetAllOrderAndPaginationAsync(
                f => f.Ativo,
                o => o.DataCriacao,
                15, 0, true)).Returns(Task.FromResult(objTipoCLientes.Take(15).Skip(0)));

            //Act
            var returns = _unitOfWorkMock.Object.TipoClienteRepository.GetAllOrderAndPaginationAsync(
                f => f.Ativo,
                o => o.DataCriacao,
                15, 0, true);

            //Assert
            Assert.IsNotNull(returns);
            Assert.AreEqual(returns.Result.Count(), 10);
        }
    }
}