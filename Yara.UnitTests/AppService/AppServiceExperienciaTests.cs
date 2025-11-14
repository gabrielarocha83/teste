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
    [Category("AppServiceExperiencia")]
    public class AppServiceExperienciaTests
    {

        private Mock<IAppServiceExperiencia> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceExperiencia>();
        }

        [Test]
        public void GetAllAsync_ShouldReturnListExperienciaWithMoq()
        {
            var experiencia = new List<ExperienciaDto>();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAllAsync()).Returns(Task.FromResult<IEnumerable<ExperienciaDto>>(experiencia));
            var returns = _appServiceMock.Object.GetAllAsync();

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAsync_ShouldReturnOneExperienciaWithMoq()
        {
            var experiencia = new ExperienciaDto();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(id))).Returns(Task.FromResult<ExperienciaDto>(experiencia));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldSaveExperienciaWithMoq()
        {
            var experiencia = new ExperienciaDto();

            _appServiceMock.Setup(c => c.Insert(experiencia)).Returns(true);
            var returns = _appServiceMock.Object.Insert(experiencia);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShoulSaveExperienciaWithMoq()
        {
            var experiencia = new ExperienciaDto();

            _appServiceMock.Setup(c => c.Update(experiencia)).Returns(Task.FromResult<bool>(true));
            var returns = _appServiceMock.Object.Update(experiencia);

            Assert.IsTrue(returns.Result);
        }

    }
}
