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
    [Category("AppServiceMediaSaca")]
    public class AppServiceMediaSacaTests
    {
        private Mock<IAppServiceMediaSaca> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceMediaSaca>();
        }

        [Test]
        public void GetAsync_ShouldReturnOneMediaSacaWithMoq()
        {
            var mediaSaca = new MediaSacaDto();
            var id = Guid.NewGuid();

            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(id))).Returns(Task.FromResult<MediaSacaDto>(mediaSaca));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAllAsync_ShouldReturnListMediaSacaWithMoq()
        {
            var mediaSaca = new List<MediaSacaDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<MediaSacaDto>>(mediaSaca));
            var returns = _appServiceMock.Object.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldSaveMediaSacaWithMoq()
        {
            var mediaSaca = new MediaSacaDto();
            _appServiceMock.Setup(c => c.Insert(mediaSaca)).Returns(true);

            var returns = _appServiceMock.Object.Insert(mediaSaca);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldSaveMediaSacaWithMoq()
        {
            var mediaSaca = new MediaSacaDto();

            _appServiceMock.Setup(c => c.Update(mediaSaca)).Returns(Task.FromResult<bool>(true));

            var returns = _appServiceMock.Object.Update(mediaSaca);

            Assert.IsTrue(returns.Result);
        }
    }
}
