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
    [Category("AppServiceAreaIrrigada")]
    public class AppServiceAreaIrrigadaTests
    {
        private Mock<IAppServiceAreaIrrigada> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceAreaIrrigada>();
        }

        [Test]
        public void GetAllAsync_ShouldReturnListAreaIrrigadaWithMoq()
        {
            var area = new List<AreaIrrigadaDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<AreaIrrigadaDto>>(area));
            var returns = _appServiceMock.Object.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAsync_ShouldReturnOneAreaIrrigadaWithMoq()
        {
            var area = new AreaIrrigadaDto();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(id))).Returns(Task.FromResult<AreaIrrigadaDto>(area));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldSaveAreaIrrigadaWithMoq()
        {
            var area = new AreaIrrigadaDto();

            _appServiceMock.Setup(c => c.Insert(area)).Returns(true);
            var returns = _appServiceMock.Object.Insert(area);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateAreaIrrigadaWithMoq()
        {
            var area = new AreaIrrigadaDto();

            _appServiceMock.Setup(c => c.Update(area)).Returns(Task.FromResult<bool>(true));
            var returns = _appServiceMock.Object.Update(area);

            Assert.IsTrue(returns.Result);
        }
    }
}
