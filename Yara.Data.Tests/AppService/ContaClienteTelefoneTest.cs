using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Yara.AppService;
using Yara.AppService.Dtos;
using Yara.AppService.Mappings;
using Yara.Data.Tests.Builder;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.AppService
{
    [TestFixture]
    public class ContaClienteTelefoneTest
    {
        private Container _container;
        
        [OneTimeSetUp]
        public void Initializer()
        {
            _container =  new Container();
            
            _container = IoC.SimpleInjectorContainer.RegisterServices();

            Mapper.Initialize(x =>
            {
                x.AddProfile(new MappingProfile());
            });
        }

        [Test]
        public async Task GetAllFilterAsync_ShouldGetListPhoneTheAccountClient()
        {
            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
             
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceContaCliente = new AppServiceContaCliente(unitofWork);
                var appServiceContaClienteTelefone = new AppServiceContaClienteTelefone(unitofWork);

                var contaCliente = await appServiceContaCliente.GetAsync(c=>c.Documento.Equals("31691301884"));
              
                //Act
                var returns = await appServiceContaClienteTelefone.GetAllFilterAsync(c => c.Ativo && c.ContaClienteID.Equals(contaCliente.ID));

                //Assert
                Assert.IsTrue(returns.Any());
            }

          
        }


        [Test]
        public async Task Insert_ShoulSavePhoneTheAccountClient()
        {
            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {

                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceContaCliente = new AppServiceContaCliente(unitofWork);
                var appServiceContaClienteTelefone = new AppServiceContaClienteTelefone(unitofWork);
                var appServiceContaClienteUsuario = new AppServiceUsuario(unitofWork);
                var contaCliente = await appServiceContaCliente.GetAsync(c => c.Documento.Equals("31691301884"));
                var usuarios = await appServiceContaClienteUsuario.GetAsync(c => c.Login.Equals("cpjunior"));


                //Act
                var codigocontacliente = new ContaClienteTelefoneBuilder()
                    .WithId(Guid.NewGuid())
                    .WithContaClienteId(contaCliente.ID)
                    .WithTelefone("11999999999")
                    .WithUsuarioIdCriacao(usuarios.ID);

                var boolcontaclientetelefone =  appServiceContaClienteTelefone.Insert(codigocontacliente);

                //Assert
                Assert.IsTrue(boolcontaclientetelefone);
            }


        }


        [Test]
        public async Task Inactive_ShoudInactivedPhoneTheAccountClient()
        {
            //Arrange

            using (AsyncScopedLifestyle.BeginScope(_container))
            {

                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceContaClienteTelefone = new AppServiceContaClienteTelefone(unitofWork);
                var contaclienteTelefone =await 
                    appServiceContaClienteTelefone.GetAsync(c => c.Telefone.Equals("19991041566"));

                //Act

                var codigocontacliente = new ContaClienteTelefoneBuilder()
                    .WithId(contaclienteTelefone.ID)
                    .WithContaClienteId(contaclienteTelefone.ContaClienteID);

                var boolcontaclientetelefone = await appServiceContaClienteTelefone.Inactive(codigocontacliente);

                //Assert
                Assert.IsTrue(boolcontaclientetelefone);
            }


        }

        [Test]
        public async Task InsertOrUpdateManyAsync_ShoulInsertOrUpdateAccountClientePhone()
        {
            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                 var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceContaCliente = new AppServiceContaCliente(unitofWork);
                var appServiceContaClienteTelefone = new AppServiceContaClienteTelefone(unitofWork);
                var appServiceContaClienteUsuario = new AppServiceUsuario(unitofWork);

                var contaCliente = await appServiceContaCliente.GetAsync(c => c.Documento.Equals("31691301884"));
                var usuario = await appServiceContaClienteUsuario.GetAsync(c => c.Login.Equals("cpjunior"));
                var contaclienteTelefones = new List<ContaClienteTelefoneDto>();
                var contaclienteTelefone = await
                    appServiceContaClienteTelefone.GetAsync(c => c.Telefone.Equals("19991041566"));

                contaclienteTelefone.Ativo = false;
                contaclienteTelefone.UsuarioIDAlteracao = usuario.ID;

                contaclienteTelefones.Add(new ContaClienteTelefoneDto
                {
                    ID = contaclienteTelefone.ID,
                    Ativo = false

                });
                contaclienteTelefones.Add(new ContaClienteTelefoneDto
                {
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    ContaClienteID =  contaCliente.ID,
                    Telefone = "19-3245-5568",
                    Tipo = TipoTelefoneDto.Fixo,
                    UsuarioIDCriacao = usuario.ID
                });

                //Act 
                var actionInsertOrUpdatePhone =  await appServiceContaClienteTelefone.InsertOrUpdateManyAsync(contaclienteTelefones);

                //Assert
                Assert.IsTrue(actionInsertOrUpdatePhone);

            }



           

          

        }

    }
}