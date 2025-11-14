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
    [Category("AppServiceReceita")]
    public class AppServiceReceitaTests
    {
        private Mock<IAppServiceReceita> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceReceita>();
        }

        [Test]
        public void GetAllAsync_ShouldReturnListReceitaWithMoq()
        {
            var receita = new List<ReceitaDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<ReceitaDto>>(receita));
            var returns = _appServiceMock.Object.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAsync_ShouldReturnOneReceitaWithMoq()
        {
            var receita = new ReceitaDto();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(id)))
                .Returns(Task.FromResult<ReceitaDto>(receita));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldSaveReceitaWithMoq()
        {
            var receita = new ReceitaDto();

            _appServiceMock.Setup(c => c.Insert(receita)).Returns(true);
            var returns = _appServiceMock.Object.Insert(receita);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateReceitaWithMoq()
        {
            var receita = new ReceitaDto();

            _appServiceMock.Setup(c => c.Update(receita)).Returns(Task.FromResult<bool>(true));
            var returns = _appServiceMock.Object.Update(receita);

            Assert.IsTrue(returns.Result);
        }
    }

}
