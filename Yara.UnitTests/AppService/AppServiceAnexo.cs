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
    public class AppServiceAnexo
    {
        private Mock<IAppServiceAnexo> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceAnexo>();
        }

        [Test]
        public void GetAllAsync_ShouldReturnListAnexoWithMoq()
        {
            var anexo = new List<AnexoDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<AnexoDto>>(anexo));
            var returns = _appServiceMock.Object.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAsync_ShouldReturnOneAnexoWithMoq()
        {
            var anexo = new AnexoDto();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(id))).Returns(Task.FromResult<AnexoDto>(anexo));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldSaveAnexoWithMoq()
        {
            var anexo = new AnexoDto();

            _appServiceMock.Setup(c => c.Insert(anexo)).Returns(true);
            var returns = _appServiceMock.Object.Insert(anexo);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateAnexoWithMoq()
        {
            var anexo = new AnexoDto();

            _appServiceMock.Setup(c => c.Update(anexo)).Returns(Task.FromResult<bool>(true));
            var returns = _appServiceMock.Object.Update(anexo);

            Assert.IsTrue(returns.Result);
        }
    }
}
