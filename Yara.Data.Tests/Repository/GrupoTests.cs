using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.Data.Entity.Context;

using Yara.Data.Entity.Repository;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.Repository
{
    public class GrupoTests
    {
        private IUnitOfWork _unitOfWork;

        [TestFixtureSetUp]
        public void Initializer()
        {
            var context = new YaraDataContext();

            Database.SetInitializer(new YaraDatabaseInitializer());

            _unitOfWork = new UnitOfWork(context);
        }

        [Test]
        public  void ShouldSaveGrupoDatabase()
        {

            for (int i = 0; i < 10; i++)
            {
                //Arrange
                var GrupoTest = new GrupoBuilder()
                    .WithID(Guid.NewGuid())
                    .WithNome(i.ToString()+ "Grupo")
                    .WithAtivo(true)
                    .WithDataCriacao(DateTime.Now)
                    .Build();
                //Act
                _unitOfWork.GrupoRepository.Insert(GrupoTest);
               
            }
            var boolInsert = _unitOfWork.Commit();


            //Assert
            Assert.IsTrue(boolInsert);
        }

        [Test]
        public  void ShouldUpdateGrupoDatabase()
        {
            //Arrange
            var GrupoTest = new GrupoBuilder()
                .WithID(Guid.NewGuid())
                .WithNome("Grupo Teste ShouldUpdateGrupoDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            //Act
            _unitOfWork.GrupoRepository.Insert(GrupoTest);
            var boolTransactionInsert = _unitOfWork.Commit();

            //Act 
            GrupoTest.DataAlteracao = DateTime.Now;
             _unitOfWork.GrupoRepository.Update(GrupoTest);

            var boolTransactionUpdate= _unitOfWork.Commit();

            //Assert 
            Assert.IsTrue(boolTransactionUpdate);
            Assert.IsTrue(boolTransactionInsert);


        }


        [Test]
        public async Task ShouldGetIdGrupoDatabase()
        {
            //Arrange
            var GrupoTest = new GrupoBuilder()
                .WithID(Guid.NewGuid())
                .WithNome("Grupo Teste ShouldGetIdGrupoDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();
            //Act
            _unitOfWork.GrupoRepository.Insert(GrupoTest);
            var boolInsert =  _unitOfWork.Commit();
            //Act GetId
            var objGetId = await _unitOfWork.GrupoRepository.GetAsync(c=>c.ID.Equals(GrupoTest.ID));

            //Assert Insert
            Assert.IsTrue(boolInsert);

            //Assert GetId
            Assert.IsNotNull(objGetId);
            Assert.AreEqual(objGetId.ID,GrupoTest.ID);
        }

        [Test]
        public async Task ShouldGetAllGrupoDatabase()
        {
            //Arrange
            var GrupoTest = new GrupoBuilder()
                .WithID(Guid.NewGuid())
                .WithNome("Grupo ShouldGetAllGrupoDatabase")
                .WithAtivo(true)
                .WithDataCriacao(DateTime.Now)
                .Build();

            //Act
            _unitOfWork.GrupoRepository.Insert(GrupoTest);
            var boolInsert =  _unitOfWork.Commit();

            var objAll = await _unitOfWork.GrupoRepository.GetAllAsync();


            //Assert Insert
            Assert.IsTrue(boolInsert);

            //Assert GetId
            Assert.IsNotNull(objAll);
           Assert.IsTrue(objAll.Any());
        }

    }
}