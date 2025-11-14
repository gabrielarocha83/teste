using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.Data.Entity.Context;

using Yara.Data.Entity.Repository;
using Yara.Data.Tests.Builder;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.Repository
{
    public class SegmentoTests
    {
        private IUnitOfWork _unitOfWork;
        private readonly Guid _userCriacao = Guid.NewGuid();
        private readonly Guid _userAlteracao = Guid.NewGuid();

        [OneTimeSetUp]
        public void Initializer()
        {
            var context = new YaraDataContext();
            Database.SetInitializer(new YaraDatabaseInitializer());
            _unitOfWork = new UnitOfWork(context);
        }

        [Test]
        public async Task ShouldGetIdSegmentoDatabase()
        {
            //Arrange
            var segmentoTest = new SegmentoBuilder()
                .WithId(Guid.NewGuid())
                .WithDescricao("Segmento ShouldGetIdSegmentoDatabase")
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.SegmentoRepository.Insert(segmentoTest);
            var boolInsert = _unitOfWork.Commit();

            var objGetId = await _unitOfWork.SegmentoRepository.GetAsync(x=>x.ID.Equals(segmentoTest.ID));

            Assert.IsTrue(boolInsert);
            Assert.IsNotNull(objGetId);
            Assert.AreEqual(objGetId.ID, segmentoTest.ID);
        }

        [Test]
        public async Task ShouldGetAllSegmentoDatabase()
        {
            //Arrange
            var segmentoTest = new SegmentoBuilder()
                .WithId(Guid.NewGuid())
                .WithDescricao("Segmento ShouldGetAllSegmentoDatabase")
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.SegmentoRepository.Insert(segmentoTest);
            var boolInsert = _unitOfWork.Commit();

            var objAll = await _unitOfWork.SegmentoRepository.GetAllAsync();

            Assert.IsTrue(boolInsert);
            Assert.IsNotNull(objAll);
            Assert.IsTrue(objAll.Any());
        }

        [Test]
        public void ShouldSaveSegmentoDatabase()
        {
            //Arrange
            var segmentoTest = new SegmentoBuilder()
                .WithId(Guid.NewGuid())
                .WithDescricao("Segmento ShouldSaveSegmentoDatabase")
                .WithAtivo(true)
                .WithUsuarioIdCriacao(_userCriacao)
                .WithDataCriacao(DateTime.Now)
                .Build();
            //Act
            _unitOfWork.SegmentoRepository.Insert(segmentoTest);
            var boolInsert = _unitOfWork.Commit();
            //Assert
            Assert.IsTrue(boolInsert);
        }

        [Test]
        public void ShouldUpdateSegmentoDatabase()
        {
            //Arrange
            var segmentoTest = new SegmentoBuilder()
                .WithId(Guid.NewGuid())
                .WithDescricao("Segmento ShouldUpdateSegmentoDatabase")
                .WithAtivo(true)
                .WithUsuarioIdCriacao(_userCriacao)
                .WithDataCriacao(DateTime.Now)
                .Build();

            //Act
            _unitOfWork.SegmentoRepository.Insert(segmentoTest);
            var objtransactionInsert = _unitOfWork.Commit();

            //Act Update
            segmentoTest.Descricao = "Segmento ShouldUpdateSegmentoDatabase2";
            segmentoTest.Ativo = false;
            segmentoTest.UsuarioIDAlteracao = _userAlteracao;
            segmentoTest.DataAlteracao = DateTime.Now;
            _unitOfWork.SegmentoRepository.Update(segmentoTest);
            var objtransactionUpdate = _unitOfWork.Commit();

            //Assert 
            Assert.IsTrue(objtransactionInsert);
            Assert.IsTrue(objtransactionUpdate);


        }
    }
}
