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
using Yara.AppService.Extensions;
using Yara.AppService.Mappings;
using Yara.Data.Tests.Builder;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.AppService
{
    [TestFixture]
    public class ContaClienteComentarioTest
    {
        private Container _container;
        private AppServiceContaClienteComentario _contaClienteComentario;
        private Guid _contaClienteID;

        [OneTimeSetUp]
        public void Initializer()
        {
            _container = new Container();

            _container = IoC.SimpleInjectorContainer.RegisterServices();

            Mapper.Initialize(x =>
            {
                x.AddProfile(new MappingProfile());
            });


        }
        [Test, Order(1)]
        public async Task Insert_ShoulSaveCommentAccountClientDatabase()
        {

            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                Boolean boolcontaclientetelefone = false;


                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var contaCliente = new AppServiceContaCliente(unitofWork);
          
                var contaClienteID = await contaCliente.GetAsync(c => c.Documento.Equals("31691301884"));
               
                _contaClienteID = contaClienteID.ID;
                var contaClienteComentario = new AppServiceContaClienteComentario(unitofWork);

                var codigocontacliente = new ContaClienteComentarioBuilder()
                    .WithContaClienteId(contaClienteID.ID)
                    .WithDescricao("Inseriu comentário testes")
                    .WithUsuarioId(Guid.Empty)
                    .WithAtivo(true)
                    .Build();
                boolcontaclientetelefone =
                   contaClienteComentario.Insert(codigocontacliente.MapTo<ContaClienteComentarioDto>());


                //Assert
                Assert.IsTrue(boolcontaclientetelefone);
            }

        }

        [Test, Order(2)]
        public async Task GetAllFilterAsync_ShouldGetListAccountClientCommentDatabase()
        {
            //Arrange 
            IEnumerable<ContaClienteComentarioDto> contaclientecomentario;

            //Act
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                _contaClienteComentario = new AppServiceContaClienteComentario(unitofWork);
                contaclientecomentario = await _contaClienteComentario.GetAllFilterAsync(c => c.ContaClienteID.Equals(_contaClienteID));
            }

            //Assert
            Assert.IsTrue(contaclientecomentario.Any());
        }







    }
}


