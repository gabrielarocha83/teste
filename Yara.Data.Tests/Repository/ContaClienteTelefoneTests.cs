using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using Yara.AppService.Extensions;
using Yara.AppService.Mappings;
using Yara.Data.Entity.Context;
using Yara.Data.Entity.Repository;
using Yara.Data.Tests.Builder;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Repository
{
    public class ContaClienteTelefoneTests
    {
        private UnitOfWork _unitOfWork;

        [OneTimeSetUp]
        public void Initializer()
        {
            var context = new YaraDataContext();
            Database.SetInitializer(new YaraDatabaseInitializer());
            _unitOfWork = new UnitOfWork(context);

            Mapper.Initialize(x =>
            {
                x.AddProfile(new MappingProfile());
            });
        }

        [Test]
        public async Task ShouldGetListPhoneTheAccountClient()
        {
            //Arrange
            var contacliente =
                await _unitOfWork.ContaClienteRepository.GetAsync(c => c.Documento.Equals("31691301884"));


            //Act
            var contaclientetelefones = await _unitOfWork.ContaClienteComentarioRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(contacliente.ID));


            //Assert
            Assert.IsNotNull(contacliente);
            Assert.IsNotNull(contaclientetelefones);
            Assert.IsTrue(contaclientetelefones.Any());
        }

        [Test]
        public async Task ShoulInsertPhoneTheAccountClient()
        {
            //Arrange

            var usuarios = await _unitOfWork.UsuarioRepository.GetAsync(c => c.Login.Equals("cpjunior"));
            var contaCliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.Documento.Equals("31691301884"));

            //Act
            var contaclientetelefone = new ContaClienteTelefoneBuilder()
                    .WithId(Guid.NewGuid())
                    .WithContaClienteId(contaCliente.ID)
                    .WithTelefone("11999999999")
                    .WithUsuarioIdCriacao(usuarios.ID).Build();

            _unitOfWork.ContaClienteTelefoneRepository.Insert(contaclientetelefone.MapTo<ContaClienteTelefone>());
            var boolcontaclientetelefone = _unitOfWork.Commit();

            //Assert
            Assert.IsTrue(boolcontaclientetelefone);
        }


        [Test]
        public async Task ShoudInactivePhoneTheAccountClient()
        {
            //Arrange
            var contaclienteTelefone = await
                _unitOfWork.ContaClienteTelefoneRepository.GetAsync(c => c.Telefone.Equals("19991041566"));

            //Act
            contaclienteTelefone.Ativo = false;

            _unitOfWork.ContaClienteTelefoneRepository.Update(contaclienteTelefone);

            var boolcontaclientetelefone = _unitOfWork.Commit();

            //Assert
            Assert.IsTrue(boolcontaclientetelefone);
        }


    }
}

