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
    [Category("AppServiceCultura")]
    public class AppServiceCulturaTests
    {
        private Mock<IAppServiceCultura> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceCultura>();
        }

        [Test]
        public void GetAllAsync_ShouldReturnListCulturaWithMoq()
        {
            var cultura = new List<CulturaDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<CulturaDto>>(cultura));
            var returns = _appServiceMock.Object.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAsync_ShouldReturnOneCulturaWithMoq()
        {
            var cultura = new CulturaDto();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(id))).Returns(Task.FromResult<CulturaDto>(cultura));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldSaveCulturaWithMoq()
        {
            var cultura = new CulturaDto();

            _appServiceMock.Setup(c => c.Insert(cultura)).Returns(true);
            var returns = _appServiceMock.Object.Insert(cultura);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateCulturaWithMoq()
        {
            var cultura = new CulturaDto();

            _appServiceMock.Setup(c => c.Update(cultura)).Returns(Task.FromResult<bool>(true));
            var returns = _appServiceMock.Object.Update(cultura);

            Assert.IsTrue(returns.Result);
        }
    }
}
