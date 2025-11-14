using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;

namespace Yara.UnitTests.AppService
{
    [TestFixture]
    [Category("AppServiceRegiao")]
    public class AppServiceRegiaoTests
    {
        private Mock<IAppServiceRegiao> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceRegiao>();
        }

        [Test]
        public void GetAllRegion_ShouldReturnLisRegionWithMoq()
        {
            var regiao = new List<RegiaoDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllRegion()).Returns(Task.FromResult<IEnumerable<RegiaoDto>>(regiao));
            var returns = _appServiceMock.Object.GetAllRegion();

            Assert.IsNotNull(returns);
        }

    }
}
