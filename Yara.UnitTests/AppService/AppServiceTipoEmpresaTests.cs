using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

namespace Yara.UnitTests.AppService
{
    [TestFixture]
    [Category("AppServiceTipoEmpresa")]
    public class AppServiceTipoEmpresaTests
    {
        private Mock<IAppServiceTipoEmpresa> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceTipoEmpresa>();
        }

        [Test]
        public void GetAllAsync_ShouldReturnListTipoEmpresaWithMoq()
        {
            var tipoEmpresa = new List<TipoEmpresaDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<TipoEmpresaDto>>(tipoEmpresa));
            var returns = _appServiceMock.Object.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAsync_ShouldReturnOneTipoEmpresaWithMoq()
        {
            var tipoEmpresa = new TipoEmpresaDto();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(id))).Returns(Task.FromResult<TipoEmpresaDto>(tipoEmpresa));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldSaveTipoEmpresaWithMoq()
        {
            var tipoEmpresa = new TipoEmpresaDto();

            _appServiceMock.Setup(c => c.Insert(tipoEmpresa)).Returns(true);
            var returns = _appServiceMock.Object.Insert(tipoEmpresa);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateTipoEmpresaWithMoq()
        {
            var tipoEmpresa = new TipoEmpresaDto();

            _appServiceMock.Setup(c => c.Update(tipoEmpresa)).Returns(Task.FromResult<bool>(true));
            var returns = _appServiceMock.Object.Update(tipoEmpresa);

            Assert.IsTrue(returns.Result);
        }
    }
}
