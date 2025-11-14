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
    [Category("AppServiceEstruturaComercial")]
    public class AppServiceEstruturaComercialTests
    {

        private Mock<IAppServiceEstruturaComercial> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceEstruturaComercial>();
        }

      

        [Test]
        public void GetAsync_ShouldReturnEstruturaComercialbyCodSapWithMoq()
        {
            var estruturaComercial = new List<EstruturaComercialDto>();

            var id ="0300";
            _appServiceMock.Setup(c => c.GetAllFilterAsync(g => g.CodigoSap.Equals(id))).Returns(Task.FromResult<IEnumerable<EstruturaComercialDto>>(estruturaComercial));
            var returns = _appServiceMock.Object.GetAllFilterAsync(g => g.CodigoSap.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAsync_ShouldReturnOEstruturaComercialbyPaperWithMoq()
        {
            var estruturaComercial = new List<EstruturaComercialDto>();

            var id = "0300";
            _appServiceMock.Setup(c => c.GetEstruturaComercialByPaper("D")).Returns(Task.FromResult<IEnumerable<EstruturaComercialDto>>(estruturaComercial));
            var returns = _appServiceMock.Object.GetAllFilterAsync(g => g.CodigoSap.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public async Task Insert_ShouldSaveEstruturaComercialWithMoq()
        {
            var estruturaComercial = new EstruturaComercialDiretoriaDto();

            _appServiceMock.Setup(c => c.Insert(estruturaComercial)).Returns(Task.FromResult(true));
            var returns = await _appServiceMock.Object.Insert(estruturaComercial);

            Assert.IsTrue(returns);
        }

        [Test]
        public async Task Update_ShoulSaveEstruturaComercialWithMoq()
        {
            var estruturaComercial = new EstruturaComercialDiretoriaDto();

            _appServiceMock.Setup(c => c.Update(estruturaComercial)).Returns(Task.FromResult<bool>(true));
            var returns = await _appServiceMock.Object.Update(estruturaComercial);

            Assert.IsTrue(returns);
        }

    }
}
