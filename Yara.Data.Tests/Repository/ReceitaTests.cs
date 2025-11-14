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
    public class ReceitaTests
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
        public void ShouldGetAllReceitaDatabase()
        {
            //Arrange
            var receitaTest = new ReceitaBuilder()
                .WithId(Guid.NewGuid())
                .WithDescricao("receita  ShouldGetAllreceitaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.ReceitaRepository.Insert(receitaTest);
            var boolInsert = _unitOfWork.Commit();

            Assert.IsTrue(boolInsert);
        }

        [Test]
        public async Task ShouldGetIdReceitaDatabase()
        {
            //Arrange
            var receitaTest = new ReceitaBuilder()
                .WithId(Guid.NewGuid())
                .WithDescricao("receita  ShouldGetIdreceitaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.ReceitaRepository.Insert(receitaTest);
            var boolInsert = _unitOfWork.Commit();

            var objGetId = await _unitOfWork.ReceitaRepository.GetAsync(x => x.ID.Equals(receitaTest.ID));

            Assert.IsTrue(boolInsert);
            Assert.IsNotNull(objGetId);
            Assert.AreEqual(objGetId.ID, receitaTest.ID);
        }

        [Test]
        public void ShouldGetUpdateReceitaDatabase()
        {
            //Arrange
            var receitaTest = new ReceitaBuilder()
                .WithId(Guid.NewGuid())
                .WithDescricao("receita  ShouldGetUpdatereceitaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.ReceitaRepository.Insert(receitaTest);
            var objtransactionInsert = _unitOfWork.Commit();


            receitaTest.Descricao = "receita  ShouldGetUpdatereceitaDatabase2";
            receitaTest.Ativo = false;
            receitaTest.DataAlteracao = DateTime.Now;
            _unitOfWork.ReceitaRepository.Update(receitaTest);
            var objtransactionUpdate = _unitOfWork.Commit();

            //Assert 
            Assert.IsTrue(objtransactionInsert);
            Assert.IsTrue(objtransactionUpdate);
        }

        [Test]
        public void ShouldGetInsertReceitaDatabase()
        {
            //Arrange
            var receitaTest = new ReceitaBuilder()
                .WithId(Guid.NewGuid())
                .WithDescricao("receita  ShouldGetUpdatereceitaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.ReceitaRepository.Insert(receitaTest);
            var objtransactionInsert = _unitOfWork.Commit();

            //Assert 
            Assert.IsTrue(objtransactionInsert);
        }

        [Test]
        public void ShouldGetDeleteReceitaDatabase()
        {
            //Arrange
            var receitaTest = new ReceitaBuilder()
                .WithId(Guid.NewGuid())
                .WithDescricao("receita  ShouldGetDeletereceitaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.ReceitaRepository.Insert(receitaTest);
            var objtransactionInsert = _unitOfWork.Commit();
            
            receitaTest.Ativo = false;
            receitaTest.DataAlteracao = DateTime.Now;
            _unitOfWork.ReceitaRepository.Update(receitaTest);
            var objtransactionUpdate = _unitOfWork.Commit();

            //Assert 
            Assert.IsTrue(objtransactionInsert);
            Assert.IsTrue(objtransactionUpdate);
        }

    }
}
