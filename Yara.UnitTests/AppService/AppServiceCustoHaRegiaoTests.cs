using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
// ReSharper disable CollectionNeverUpdated.Local

namespace Yara.UnitTests.AppService
{
    [TestFixture]
    [Category("AppServiceCustoHaRegiao")]
    public class AppServiceCustoHaRegiaoTests
    {
        private Mock<IAppServiceCustoHaRegiao> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceCustoHaRegiao>();
        }

        [Test]
        public void GetAllAsync_ShouldReturnListcustoHaRegiaoWithMoq()
        {
            var custoHaRegiao = new List<CustoHaRegiaoDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<CustoHaRegiaoDto>>(custoHaRegiao));
            var returns = _appServiceMock.Object.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAsync_ShouldReturnOnecustoHaRegiaoWithMoq()
        {
            var custoHaRegiao = new CustoHaRegiaoDto();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(id))).Returns(Task.FromResult(custoHaRegiao));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldSavecustoHaRegiaoWithMoq()
        {
            var custoHaRegiao = new CustoHaRegiaoDto();

            _appServiceMock.Setup(c => c.Insert(custoHaRegiao)).Returns(true);
            var returns = _appServiceMock.Object.Insert(custoHaRegiao);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdatecustoHaRegiaoWithMoq()
        {
            var custoHaRegiao = new CustoHaRegiaoDto();

            _appServiceMock.Setup(c => c.Update(custoHaRegiao)).Returns(Task.FromResult(true));
            var returns = _appServiceMock.Object.Update(custoHaRegiao);

            Assert.IsTrue(returns.Result);
        }
    }
}
