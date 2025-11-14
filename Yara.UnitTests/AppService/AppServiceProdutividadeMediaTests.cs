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
    [Category("AppServiceProdutividadeMedia")]
    public class AppServiceProdutividadeMediaTests
    {
        private Mock<IAppServiceProdutividadeMedia> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceProdutividadeMedia>();
        }

        [Test]
        public void GetAllAsync_ShouldReturnListProdutividadeWithMoq()
        {
            var produtividade = new List<ProdutividadeMediaDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<ProdutividadeMediaDto>>(produtividade));
            var returns = _appServiceMock.Object.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAsync_ShouldReturnOneProdutividadeWithMoq()
        {
            var produtividade = new ProdutividadeMediaDto();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(id))).Returns(Task.FromResult<ProdutividadeMediaDto>(produtividade));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldSaveProdutividadeWithMoq()
        {
            var produtividade = new ProdutividadeMediaDto();

            _appServiceMock.Setup(c => c.Insert(produtividade)).Returns(true);
            var returns = _appServiceMock.Object.Insert(produtividade);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateProdutividadeWithMoq()
        {
            var produtividade = new ProdutividadeMediaDto();

            _appServiceMock.Setup(c => c.Update(produtividade)).Returns(Task.FromResult<bool>(true));
            var returns = _appServiceMock.Object.Update(produtividade);

            Assert.IsTrue(returns.Result);
        }
    }
}
