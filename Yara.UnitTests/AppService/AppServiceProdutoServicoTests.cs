using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

namespace Yara.UnitTests.AppService
{
    public class AppServiceProdutoServicoTests
    {
        private Mock<IAppServiceProdutoServico> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceProdutoServico>();
        }

        [Test]
        public void GetAllAsync_ShouldReturnListProdutoServicoWithMoq()
        {
            var produtoServico = new List<ProdutoServicoDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<ProdutoServicoDto>>(produtoServico));
            var returns = _appServiceMock.Object.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAsync_ShouldReturnOneProdutoServicoWithMoq()
        {
            var produtoServico = new ProdutoServicoDto();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(id))).Returns(Task.FromResult<ProdutoServicoDto>(produtoServico));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldSaveProdutoServicoWithMoq()
        {
            var produtoServico = new ProdutoServicoDto();

            _appServiceMock.Setup(c => c.Insert(produtoServico)).Returns(true);
            var returns = _appServiceMock.Object.Insert(produtoServico);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateProdutoServicoWithMoq()
        {
            var produtoServico = new ProdutoServicoDto();

            _appServiceMock.Setup(c => c.Update(produtoServico)).Returns(Task.FromResult<bool>(true));
            var returns = _appServiceMock.Object.Update(produtoServico);

            Assert.IsTrue(returns.Result);
        }
    }
}
