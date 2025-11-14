using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.Data.Entity.Context;

using Yara.Data.Entity.Repository;
using Yara.Domain.Repository;
#pragma warning disable 618

namespace Yara.Data.Tests.Repository
{
    public class PermissaoTests
    {
        private IUnitOfWork  _unitOfWork;

        [TestFixtureSetUp]
        public void Initializer()
        {
            var context = new YaraDataContext();

            Database.SetInitializer(new YaraDatabaseInitializer());

            _unitOfWork = new UnitOfWork(context);
        }

     
        [Test]
        public async Task PermissaoRepository_ShouldGetPermissionsDatabase()
        {
            var user = await _unitOfWork.UsuarioRepository.GetAsync(c => c.Login.Equals("lcjesus"));
            var resturnTask = _unitOfWork.PermissaoRepository.ListaPermissoes(user.ID);

            Assert.IsTrue(resturnTask.Result.Any());
        }

    }
}