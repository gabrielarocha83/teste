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
    public class TipoEmpresaTests
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
        public void ShouldGetAllTipoEmpresaDatabase()
        {
            //Arrange
            var tipoEmpresaTest = new TipoEmpresaBuilder()
                .WithId(Guid.NewGuid())
                .WithTipo("TipoEmpresa  ShouldGetAllTipoEmpresaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.TipoEmpresaRepository.Insert(tipoEmpresaTest);
            var boolInsert = _unitOfWork.Commit();

            Assert.IsTrue(boolInsert);
        }

        [Test]
        public async Task ShouldGetIdTipoEmpresaDatabase()
        {
            //Arrange
            var tipoEmpresaTest = new TipoEmpresaBuilder()
                .WithId(Guid.NewGuid())
                .WithTipo("TipoEmpresa  ShouldGetIdTipoEmpresaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.TipoEmpresaRepository.Insert(tipoEmpresaTest);
            var boolInsert = _unitOfWork.Commit();

            var objGetId = await _unitOfWork.TipoEmpresaRepository.GetAsync(x => x.ID.Equals(tipoEmpresaTest.ID));

            Assert.IsTrue(boolInsert);
            Assert.IsNotNull(objGetId);
            Assert.AreEqual(objGetId.ID, tipoEmpresaTest.ID);
        }

        [Test]
        public void ShouldGetUpdateTipoEmpresaDatabase()
        {
            //Arrange
            var tipoEmpresaTest = new TipoEmpresaBuilder()
                .WithId(Guid.NewGuid())
                .WithTipo("TipoEmpresa  ShouldGetUpdateTipoEmpresaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.TipoEmpresaRepository.Insert(tipoEmpresaTest);
            var objtransactionInsert = _unitOfWork.Commit();


            tipoEmpresaTest.Tipo = "TipoEmpresa  ShouldGetUpdateTipoEmpresaDatabase2";
            tipoEmpresaTest.Ativo = false;
            tipoEmpresaTest.DataAlteracao = DateTime.Now;
            _unitOfWork.TipoEmpresaRepository.Update(tipoEmpresaTest);
            var objtransactionUpdate = _unitOfWork.Commit();

            //Assert 
            Assert.IsTrue(objtransactionInsert);
            Assert.IsTrue(objtransactionUpdate);
        }

        [Test]
        public void ShouldGetInsertTipoEmpresaDatabase()
        {
            //Arrange
            var tipoEmpresaTest = new TipoEmpresaBuilder()
                .WithId(Guid.NewGuid())
                .WithTipo("TipoEmpresa  ShouldGetUpdateTipoEmpresaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.TipoEmpresaRepository.Insert(tipoEmpresaTest);
            var objtransactionInsert = _unitOfWork.Commit();

            //Assert 
            Assert.IsTrue(objtransactionInsert);
        }
    }
}
