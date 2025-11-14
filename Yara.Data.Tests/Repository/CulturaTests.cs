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
    public class CulturaTests
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
        public void ShouldGetAllCulturaDatabase()
        {
            //Arrange
            var culturaTest = new CulturaBuilder()
                .WithId(Guid.NewGuid())
                .WithTipo("cultura  ShouldGetAllculturaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.CulturaRepository.Insert(culturaTest);
            var boolInsert = _unitOfWork.Commit();

            Assert.IsTrue(boolInsert);
        }

        [Test]
        public async Task ShouldGetIdCulturaDatabase()
        {
            //Arrange
            var culturaTest = new CulturaBuilder()
                .WithId(Guid.NewGuid())
                .WithTipo("cultura  ShouldGetIdculturaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.CulturaRepository.Insert(culturaTest);
            var boolInsert = _unitOfWork.Commit();

            var objGetId = await _unitOfWork.CulturaRepository.GetAsync(x => x.ID.Equals(culturaTest.ID));

            Assert.IsTrue(boolInsert);
            Assert.IsNotNull(objGetId);
            Assert.AreEqual(objGetId.ID, culturaTest.ID);
        }

        [Test]
        public void ShouldGetUpdateCulturaDatabase()
        {
            //Arrange
            var culturaTest = new CulturaBuilder()
                .WithId(Guid.NewGuid())
                .WithTipo("cultura  ShouldGetUpdateculturaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.CulturaRepository.Insert(culturaTest);
            var objtransactionInsert = _unitOfWork.Commit();


            culturaTest.Tipo = "cultura  ShouldGetUpdateculturaDatabase2";
            culturaTest.Ativo = false;
            culturaTest.DataAlteracao = DateTime.Now;
            _unitOfWork.CulturaRepository.Update(culturaTest);
            var objtransactionUpdate = _unitOfWork.Commit();

            //Assert 
            Assert.IsTrue(objtransactionInsert);
            Assert.IsTrue(objtransactionUpdate);
        }

        [Test]
        public void ShouldGetInsertCulturaDatabase()
        {
            //Arrange
            var culturaTest = new CulturaBuilder()
                .WithId(Guid.NewGuid())
                .WithTipo("cultura  ShouldGetUpdateculturaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.CulturaRepository.Insert(culturaTest);
            var objtransactionInsert = _unitOfWork.Commit();

            //Assert 
            Assert.IsTrue(objtransactionInsert);
        }
    }
}
