using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.Data.Entity.Context;

using Yara.Data.Entity.Repository;
using Yara.Data.Tests.Builder;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.Repository
{
    public class ContaClienteComentarioTests
    {
        private IUnitOfWork _unitOfWork;
        private Usuario _user;
        private ContaCliente _contaCliente;
        private ContaClienteComentario _contaClienteComentario;

        [OneTimeSetUp]
        public async Task Initializer()
        {
            var context = new YaraDataContext();
            Database.SetInitializer(new YaraDatabaseInitializer());
            _unitOfWork = new UnitOfWork(context);

            _user = await _unitOfWork.UsuarioRepository.GetAsync(c => c.Login.Equals("lcjesus"));
            _contaCliente =
                await _unitOfWork.ContaClienteRepository.GetAsync(c => c.Documento.Equals("31691301884"));

           
        }

        private void InsertComment(string comentarios)
        {
            _contaClienteComentario = new ContaClienteComentarioBuilder()
                .WithId(Guid.NewGuid())
                .WithContaClienteId(_contaCliente.ID)
                .WithDescricao(comentarios)
                .WithUsuarioId((Guid)_user.ID)
                .WithDataCriacao(DateTime.Now)
                .Build();

            _unitOfWork.ContaClienteComentarioRepository.Insert(_contaClienteComentario);
            _unitOfWork.Commit();
        }

        [Test]
        public async Task ShouldGetIdContaClienteComentarioDatabase()
        {
            //Arrange 
            InsertComment("Inserir Coment");


            //Act
            var objGetId = await _unitOfWork.ContaClienteComentarioRepository.GetAsync(x => x.ID.Equals(_contaClienteComentario.ID));

            //Assert
            Assert.IsNotNull(objGetId);
            Assert.AreEqual(objGetId.ID, _contaClienteComentario.ID);
        }

        [Test]
        public async Task ShouldGetAllContaClienteComentarioDatabase()
        {
            //Arrange 
            InsertComment("Inserir Coment2");

            //Act
            var objAll = await _unitOfWork.ContaClienteComentarioRepository.GetAllAsync();

            //Assert
            Assert.IsNotNull(objAll);
            Assert.IsTrue(objAll.Any());
        }

        [Test]
        public async Task ShouldSaveContaClienteComentarioDatabase()
        {
           
            //Arrange
            var contaTest = new ContaClienteComentarioBuilder()
                .WithId(Guid.NewGuid())
                .WithContaClienteId(_contaCliente.ID)
                .WithDescricao("ContaClienteComentario ShouldSaveContaClienteComentarioDatabase")
                .WithAtivo(true)
                .WithUsuarioId((Guid)_user.ID)
                .WithDataCriacao(DateTime.Now)
                .Build();

            //Act
            _unitOfWork.ContaClienteComentarioRepository.Insert(contaTest);
            var boolInsert = _unitOfWork.Commit();


            //Assert
            Assert.IsTrue(boolInsert);
        }

       
    }
}
