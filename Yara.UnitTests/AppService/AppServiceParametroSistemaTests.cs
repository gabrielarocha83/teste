using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

namespace Yara.UnitTests.AppService
{
    public class AppServiceParametroSistemaTests
    {
        private Mock<IAppServiceParametroSistema> _appServiceMock;

        [OneTimeSetUp]
        public void Initializer()
        {
            _appServiceMock = new Mock<IAppServiceParametroSistema>();
        }

        [Test]
        public void GetAllAsync_ShouldReturnListParametroWithMoq()
        {
            var parametro = new List<ParametroSistemaDto>();

            var id = Guid.NewGuid();
            var empresa = "Y";
            _appServiceMock.Setup(c => c.GetAllAsync(empresa)).Returns(Task.FromResult<IEnumerable<ParametroSistemaDto>>(parametro));
            var returns = _appServiceMock.Object.GetAllAsync(empresa);

            Assert.IsNotNull(returns);
        }

        [Test]
        public void GetAsync_ShouldReturnOneParametroWithMoq()
        {
            var parametro = new ParametroSistemaDto();

            var id = Guid.NewGuid();
            _appServiceMock.Setup(c => c.GetAsync(g => g.ID.Equals(id))).Returns(Task.FromResult<ParametroSistemaDto>(parametro));
            var returns = _appServiceMock.Object.GetAsync(g => g.ID.Equals(id));

            Assert.IsNotNull(returns);
        }

        [Test]
        public void Insert_ShouldSaveParametroWithMoq()
        {
            var parametro = new ParametroSistemaDto();

            _appServiceMock.Setup(c => c.Insert(parametro)).Returns(true);
            var returns = _appServiceMock.Object.Insert(parametro);

            Assert.IsTrue(returns);
        }

        [Test]
        public void Update_ShouldUpdateParametroWithMoq()
        {
            var parametro = new ParametroSistemaDto();

            _appServiceMock.Setup(c => c.Update(parametro)).Returns(Task.FromResult<bool>(true));
            var returns = _appServiceMock.Object.Update(parametro);

            Assert.IsTrue(returns.Result);
        }
    }
}
