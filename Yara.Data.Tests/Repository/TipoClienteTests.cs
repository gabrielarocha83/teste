using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.Data.Entity.Context;

using Yara.Data.Entity.Repository;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.Repository
{
    public class TipoClienteTests
    {
        private IUnitOfWork _unitOfWork;

        [TestFixtureSetUp]
        public void Initializer()
        {
            var context = new YaraDataContext();

            _unitOfWork = new UnitOfWork(context);
        }

        [Test]
        public  void ShouldSaveTipoClienteDatabase()
        {

      
                //Arrange
                var TipoClienteTest = new TipoClienteBuilder()
                    .WithID(Guid.NewGuid())
                    .WithNome("Montadora (AIR 1)")
                    .WithDescricao("Montadora (AIR 1)")
                    .WithAtivo(true)
                    .WithDataCriacao(DateTime.Now)
                    .Build();
                //Act
                _unitOfWork.TipoClienteRepository.Insert(TipoClienteTest);
               
           
            var boolInsert = _unitOfWork.Commit();


            //Assert
            Assert.IsTrue(boolInsert);
        }

        [Test]
        public async Task ShouldUpdateTipoClienteDatabase()
        {
            //Arrange
            var TipoClienteTest = await _unitOfWork.TipoClienteRepository.GetAsync(c => c.Nome.Equals("Usina"));

            //Act 
            TipoClienteTest.DataAlteracao = DateTime.Now;
             _unitOfWork.TipoClienteRepository.Update(TipoClienteTest);

            var boolTransactionUpdate= _unitOfWork.Commit();

            //Assert 
            Assert.IsTrue(boolTransactionUpdate);
          


        }


        [Test]
        public async Task ShouldGetIdTipoClienteDatabase()
        {
            //Arrange
            var TipoClienteTest = new TipoCliente();
            var filtroNome = "Usina";

            //Act
            TipoClienteTest =await  _unitOfWork.TipoClienteRepository.GetAsync(c => c.Nome.Equals(filtroNome));

            //Assert 
            Assert.IsNotNull(TipoClienteTest);
            Assert.AreEqual(TipoClienteTest.Nome, filtroNome);
        }

        [Test]
        public async Task ShouldGetAllTipoClienteDatabase()
        {
            //Arrange
            IEnumerable<TipoCliente> TipoClienteTest = new List<TipoCliente>();

            //Act
            TipoClienteTest = await _unitOfWork.TipoClienteRepository.GetAllAsync();

            //Assert 
            Assert.IsNotNull(TipoClienteTest);
           Assert.IsTrue(TipoClienteTest.Any());
        }

    }
}