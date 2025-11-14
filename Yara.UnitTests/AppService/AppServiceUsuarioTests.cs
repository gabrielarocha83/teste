using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

namespace Yara.UnitTests.AppService
{
    [TestFixture]
    [Category("AppServiceUsuario")]
    public class AppServiceUsuarioTests
    {
        private Mock<IAppServiceUsuario> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceUsuario>();
        }

        [Test]
        public void Insert_ShouldSaveUsuarioWithMoq()
        {
            var usuario = new UsuarioDto();
            _appServiceMock.Setup(c => c.Insert(usuario)).Returns(true);

            var returns = _appServiceMock.Object.Insert(usuario);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldSaveUsuarioWithMoq()
        {
            var usuario = new UsuarioDto();

            _appServiceMock.Setup(c => c.Update(usuario)).Returns(Task.FromResult<bool>(true));

            var returns = _appServiceMock.Object.Update(usuario);

            Assert.IsTrue(returns.Result);
        }

        [Test]
        public void GetAsync_ShouldReturnOneUsuarioWithMoq()
        {
            var usuario = new UsuarioDto();
            var id = Guid.NewGuid();

            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(id))).Returns(Task.FromResult<UsuarioDto>(usuario));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAllAsync_ShouldReturnListPermissaoWithMoq()
        {
            var usuario = new List<UsuarioDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<UsuarioDto>>(usuario));
            var returns = _appServiceMock.Object.GetAllAsync();

            Assert.IsNotNull(returns);
        }
    }
}
