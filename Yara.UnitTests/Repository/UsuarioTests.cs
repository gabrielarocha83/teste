using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.UnitTests.Repository
{
    [TestFixture]
    [Category("Usuario")]
    public class UsuarioTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public void ShouldSaveUsuarioWithMoq()
        {
            var usuario = new Usuario();
            _unitOfWorkMock.Setup(c => c.UsuarioRepository.Insert(usuario));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            _unitOfWorkMock.Object.UsuarioRepository.Insert(usuario);

            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }

        [Test]
        public void ShouldUpdateUsuarioWithMoq()
        {
            var usuario = new Usuario();

            _unitOfWorkMock.Setup(c => c.UsuarioRepository.Update(usuario));
            _unitOfWorkMock.Setup(c => c.Commit()).Returns(true);

            _unitOfWorkMock.Object.UsuarioRepository.Update(usuario);

            Assert.IsTrue(_unitOfWorkMock.Object.Commit());
        }

        [Test]
        public void ShouldReturnOneUsuarioWithMoq()
        {
            var usuario = new Usuario();

            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(c => c.UsuarioRepository.GetAsync(u => u.ID.Equals(id))).Returns(Task.FromResult(usuario));

            var returns = _unitOfWorkMock.Object.UsuarioRepository.GetAsync(u => u.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void ShouldReturnListPermissaoWithMoq()
        {
            var usuario = new List<Usuario>
            {
                new Usuario
                {
                    ID = Guid.NewGuid(),
                    Ativo = true,
                    Nome = "Junior",
                    DataCriacao = DateTime.Now,
                    UsuarioIDCriacao = Guid.NewGuid(),
                    Email = "claudemir.junior@performait.com",
                    Login = "cpjunior",
                    TipoAcesso =  TipoAcesso.AD
                }
            };

            
            _unitOfWorkMock.Setup(c => c.UsuarioRepository.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Usuario>>(usuario));

            var returns = _unitOfWorkMock.Object.UsuarioRepository.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void ShouldReturnListWithPaginationAndOrderGrupoWithMoq()
        {
            //Arrange


            var objUsuario = new List<Usuario>();
            for (var i = 0; i < 50; i++)
            {
                objUsuario.Add(new Usuario()
                {
                    ID = Guid.NewGuid(),
                    Ativo = true,
                    Login = "Login",
                    Email = "login@login.com",
                    Nome = "Grupo" + i.ToString(),
                    DataCriacao = DateTime.Now.AddMinutes(i)
                });
            }



            _unitOfWorkMock.Setup(c => c.UsuarioRepository.GetAllOrderAndPaginationAsync(
                f => f.Ativo,
                o => o.DataCriacao,
                15, 0, true)).Returns(Task.FromResult(objUsuario.Take(15).Skip(0)));

            //Act
            var returns = _unitOfWorkMock.Object.UsuarioRepository.GetAllOrderAndPaginationAsync(
                f => f.Ativo,
                o => o.DataCriacao,
                15, 0, true);

            //Assert
            Assert.IsNotNull(returns);
            Assert.AreEqual(returns.Result.Count(), 15);
        }

    }
}
