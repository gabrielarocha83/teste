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
    [Category("AppServiceContaClienteComentario")]
    public class AppServiceContaClienteComentarioTests
    {
        private Mock<IAppServiceContaClienteComentario> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceContaClienteComentario>();
        }

        [Test]
        public void GetAsync_ShouldReturnOneContaClienteComentarioWithMoq()
        {
            var conta = new ContaClienteComentarioDto();
            var id = Guid.NewGuid();

            _appServiceMock.Setup(c => c.GetAsync(g=>g.ID.Equals(id))).Returns(Task.FromResult<ContaClienteComentarioDto>(conta));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAllAsync_ShouldReturnListContaClienteComentarioWithMoq()
        {
            var conta = new List<ContaClienteComentarioDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<ContaClienteComentarioDto>>(conta));
            var returns = _appServiceMock.Object.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldSaveContaClienteComentarioWithMoq()
        {
            var conta = new ContaClienteComentarioDto();
            _appServiceMock.Setup(c => c.Insert(conta)).Returns(true);

            var returns = _appServiceMock.Object.Insert(conta);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateContaClienteComentarioWithMoq()
        {
            var conta = new ContaClienteComentarioDto();

            _appServiceMock.Setup(c => c.Update(conta)).Returns(Task.FromResult<bool>(true));

            var returns = _appServiceMock.Object.Update(conta);

            Assert.IsTrue(returns.Result);
        }
    }
}
