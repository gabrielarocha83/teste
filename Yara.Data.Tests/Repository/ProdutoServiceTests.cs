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
    public class ProdutoServiceTests
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
        public void ShouldGetAllProdutoServicoDatabase()
        {
            //Arrange
            var produtoServicoTest = new ProdutoServicoBuilder()
                .WithId(Guid.NewGuid())
                .WithNome("ProdutoServico  ShouldGetAllProdutoServicoDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.ProdutoServicoRepository.Insert(produtoServicoTest);
            var boolInsert = _unitOfWork.Commit();

            Assert.IsTrue(boolInsert);
        }

        [Test]
        public async Task ShouldGetIdProdutoServicoDatabase()
        {
            //Arrange
            var ProdutoServicoTest = new ProdutoServicoBuilder()
                .WithId(Guid.NewGuid())
                .WithNome("ProdutoServico  ShouldGetIdProdutoServicoDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.ProdutoServicoRepository.Insert(ProdutoServicoTest);
            var boolInsert = _unitOfWork.Commit();

            var objGetId = await _unitOfWork.ProdutoServicoRepository.GetAsync(x => x.ID.Equals(ProdutoServicoTest.ID));

            Assert.IsTrue(boolInsert);
            Assert.IsNotNull(objGetId);
            Assert.AreEqual(objGetId.ID, ProdutoServicoTest.ID);
        }

        [Test]
        public void ShouldGetUpdateProdutoServicoDatabase()
        {
            //Arrange
            var produtoServicoTest = new ProdutoServicoBuilder()
                .WithId(Guid.NewGuid())
                .WithNome("ProdutoServico  ShouldGetUpdateProdutoServicoDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.ProdutoServicoRepository.Insert(produtoServicoTest);
            var objtransactionInsert = _unitOfWork.Commit();


            produtoServicoTest.Nome = "ProdutoServico  ShouldGetUpdateProdutoServicoDatabase2";
            produtoServicoTest.Ativo = false;
            produtoServicoTest.DataAlteracao = DateTime.Now;
            _unitOfWork.ProdutoServicoRepository.Update(produtoServicoTest);
            var objtransactionUpdate = _unitOfWork.Commit();

            //Assert 
            Assert.IsTrue(objtransactionInsert);
            Assert.IsTrue(objtransactionUpdate);
        }

        [Test]
        public void ShouldGetInsertProdutoServicoDatabase()
        {
            //Arrange
            var produtoServicoTest = new ProdutoServicoBuilder()
                .WithId(Guid.NewGuid())
                .WithNome("ProdutoServico  ShouldGetUpdateProdutoServicoDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.ProdutoServicoRepository.Insert(produtoServicoTest);
            var objtransactionInsert = _unitOfWork.Commit();

            //Assert 
            Assert.IsTrue(objtransactionInsert);
        }
    }
}
