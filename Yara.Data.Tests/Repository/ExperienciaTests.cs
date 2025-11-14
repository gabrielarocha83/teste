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
    public class ExperienciaTests
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
        public void ShouldGetAllExperienciaDatabase()
        {
            //Arrange
            var experienciaTest = new ExperienciaBuilder()
                .WithId(Guid.NewGuid())
                .WithDescricao("Experiencia  ShouldGetAllExperienciaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.ExperienciaRepository.Insert(experienciaTest);
            var boolInsert = _unitOfWork.Commit();

            Assert.IsTrue(boolInsert);
        }

        [Test]
        public async Task ShouldGetIdExperienciaDatabase()
        {
            //Arrange
            var experienciaTest = new ExperienciaBuilder()
                .WithId(Guid.NewGuid())
                .WithDescricao("Experiencia  ShouldGetIdExperienciaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.ExperienciaRepository.Insert(experienciaTest);
            var boolInsert = _unitOfWork.Commit();
            
            var objGetId = await _unitOfWork.ExperienciaRepository.GetAsync(x => x.ID.Equals(experienciaTest.ID));

            Assert.IsTrue(boolInsert);
            Assert.IsNotNull(objGetId);
            Assert.AreEqual(objGetId.ID, experienciaTest.ID);
        }

        [Test]
        public void ShouldGetUpdateExperienciaDatabase()
        {
            //Arrange
            var experienciaTest = new ExperienciaBuilder()
                .WithId(Guid.NewGuid())
                .WithDescricao("Experiencia  ShouldGetUpdateExperienciaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.ExperienciaRepository.Insert(experienciaTest);
            var objtransactionInsert = _unitOfWork.Commit();


            experienciaTest.Descricao = "Experiencia  ShouldGetUpdateExperienciaDatabase2";
            experienciaTest.Ativo = false;
            experienciaTest.DataAlteracao = DateTime.Now;
            _unitOfWork.ExperienciaRepository.Update(experienciaTest);
            var objtransactionUpdate = _unitOfWork.Commit();

            //Assert 
            Assert.IsTrue(objtransactionInsert);
            Assert.IsTrue(objtransactionUpdate);
        }

        [Test]
        public void ShouldGetInsertExperienciaDatabase()
        {
            //Arrange
            var experienciaTest = new ExperienciaBuilder()
                .WithId(Guid.NewGuid())
                .WithDescricao("Experiencia  ShouldGetUpdateExperienciaDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.ExperienciaRepository.Insert(experienciaTest);
            var objtransactionInsert = _unitOfWork.Commit();
            
            //Assert 
            Assert.IsTrue(objtransactionInsert);
        }
    }
}
