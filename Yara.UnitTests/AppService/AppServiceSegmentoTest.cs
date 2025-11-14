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
    [Category("AppServiceSegmento")]
    public class AppServiceSegmentoTest
    {
        private Mock<IAppServiceSegmento> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceSegmento>();
        }

        [Test]
        public void GetAsync_ShouldReturnOneSegmentoWithMoq()
        {
            var segmento = new SegmentoDto();
            var id = Guid.NewGuid();

            _appServiceMock.Setup(c => c.GetAsync(g=>g.ID.Equals(id))).Returns(Task.FromResult<SegmentoDto>(segmento));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAllAsync_ShouldReturnListSegmentoWithMoq()
        {
            var segmento = new List<SegmentoDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<SegmentoDto>>(segmento));
            var returns = _appServiceMock.Object.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldSaveSegmentoWithMoq()
        {
            var segmento = new SegmentoDto();
            _appServiceMock.Setup(c => c.Insert(segmento)).Returns(true);

            var returns = _appServiceMock.Object.Insert(segmento);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldSaveSegmentoWithMoq()
        {
            var segmento = new SegmentoDto();

            _appServiceMock.Setup(c => c.Update(segmento)).Returns(Task.FromResult<bool>(true));

            var returns = _appServiceMock.Object.Update(segmento);

            Assert.IsTrue(returns.Result);
        }
    }
}
