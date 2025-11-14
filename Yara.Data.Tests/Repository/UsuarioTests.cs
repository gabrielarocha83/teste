using System;
using System.Data.Entity;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.Data.Entity.Context;

using Yara.Data.Entity.Repository;
using Yara.Data.Tests.Builder;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.Repository
{
    public class UsuarioTests
    {
        private IUnitOfWork _unitOfWork;

        [OneTimeSetUp]
        public void Initializer()
        {
            var context = new YaraDataContext();
            Database.SetInitializer(new YaraDatabaseInitializer());
            _unitOfWork = new UnitOfWork(context);
        }

        [Test]
        public void ShouldSaveUsuarioDatabase()
        {
            //Arrange
            var usuarioTest = new UsuarioBuilder()
                .WithId(Guid.NewGuid())
                .WithNome("Usuario ShouldSaveUsuarioDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();
            //Act
            _unitOfWork.UsuarioRepository.Insert(usuarioTest);
            var boolInsert = _unitOfWork.Commit();
            //Assert
            Assert.IsTrue(boolInsert);
        }

        [Test]
        public void ShouldUpdateUsuarioDatabase()
        {
            //Arrange
            var usuarioTest = new UsuarioBuilder()
                .WithId(Guid.NewGuid())
                .WithNome("Usuario ShouldUpdateGrupoDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();
            //Act
            _unitOfWork.UsuarioRepository.Insert(usuarioTest);
            var objtransactionInsert = _unitOfWork.Commit();

            //Act Update
            usuarioTest.Nome = "Usuario ShouldUpdateGrupoDatabase2";
            usuarioTest.Ativo = false;
            usuarioTest.DataAlteracao = DateTime.Now;
            _unitOfWork.UsuarioRepository.Update(usuarioTest);
            var objtransactionUpdate = _unitOfWork.Commit();

            //Assert 
            Assert.IsTrue(objtransactionInsert);
            Assert.IsTrue(objtransactionUpdate);


        }

        [Test]
        public async Task ShouldGetIdUsuarioDatabase()
        {
            //Arrange
            var usuarioTest = new UsuarioBuilder()
                .WithId(Guid.NewGuid())
                .WithNome("Usuario  ShouldGetIdUsuarioDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.UsuarioRepository.Insert(usuarioTest);
            var boolInsert = _unitOfWork.Commit();

            var objGetId = await _unitOfWork.UsuarioRepository.GetAsync(x=>x.ID.Equals(usuarioTest.ID));

            Assert.IsTrue(boolInsert);
            Assert.IsNotNull(objGetId);
            Assert.AreEqual(objGetId.ID, usuarioTest.ID);
        }

        [Test]
        public async Task ShouldGetAllUsuarioDatabase()
        {
            //Arrange
            var usuarioTest = new UsuarioBuilder()
                .WithId(Guid.NewGuid())
                .WithNome("Usuario  ShouldGetIdUsuarioDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.UsuarioRepository.Insert(usuarioTest);
            var boolInsert = _unitOfWork.Commit();

            //var objAll = await _unitOfWork.UsuarioRepository.GetAllAsync();

            //Assert.IsTrue(boolInsert);
            //Assert.IsNotNull(objAll);

            //Assert.IsTrue(objAll.Any());
        }
    }
}
