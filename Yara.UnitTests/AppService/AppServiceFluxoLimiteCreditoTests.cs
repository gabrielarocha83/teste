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
    [Category("AppServiceFluxoLiberacaoManual")]
    public class AppServiceFluxoLiberacaoManualTests
    {
        private Mock<IAppServiceFluxoLiberacaoManual> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceFluxoLiberacaoManual>();
        }

        [Test]
        public void GetAllAsync_ShouldReturnListFluxoLimiteCreditoWithMoq()
        {
            var fluxoLimiteCredito = new List<FluxoLiberacaoManualDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<FluxoLiberacaoManualDto>>(fluxoLimiteCredito));
            var returns = _appServiceMock.Object.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAsync_ShouldReturnOneFluxoLimiteCreditoWithMoq()
        {
            var fluxoLimiteCredito = new FluxoLiberacaoManualDto();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(id))).Returns(Task.FromResult<FluxoLiberacaoManualDto>(fluxoLimiteCredito));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldSaveFluxoLimiteCreditoWithMoq()
        {
            var fluxoLimiteCredito = new FluxoLiberacaoManualDto();

            _appServiceMock.Setup(c => c.Insert(fluxoLimiteCredito)).Returns(true);
            var returns = _appServiceMock.Object.Insert(fluxoLimiteCredito);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateFluxoLimiteCreditoWithMoq()
        {
            var fluxoLimiteCredito = new FluxoLiberacaoManualDto();

            _appServiceMock.Setup(c => c.Update(fluxoLimiteCredito)).Returns(Task.FromResult<bool>(true));
            var returns = _appServiceMock.Object.Update(fluxoLimiteCredito);

            Assert.IsTrue(returns.Result);
        }
    }
}
