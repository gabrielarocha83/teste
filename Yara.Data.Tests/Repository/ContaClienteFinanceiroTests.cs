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
    public class ContaClienteFinanceiroTests
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
        public async Task ShouldGetContaClienteForContaClienteIDDatabase()
        {
            //Arrange
            var contaClienteTest =
                await _unitOfWork.ContaClienteRepository.GetAsync(c => c.Documento.Equals("31691301884"));
           


            //Act
           var objContaClienteFinanceiro =  await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c=>c.ContaClienteID.Equals(contaClienteTest.ID));
           
            //Assert
            Assert.IsNotNull(objContaClienteFinanceiro);

            Assert.AreEqual(objContaClienteFinanceiro.ContaClienteID, contaClienteTest.ID);
        }

        [Test]
        public async Task ShouldGetAllContaClienteDataBase()
        {
            var usuarioId = Guid.NewGuid();
            var segmentoID = Guid.NewGuid();
            var tipoclienteID = Guid.NewGuid();

            var contaClienteTest = new ContaClienteBuilder();
            var segmentoTest = new SegmentoBuilder();
            var tipoClienteTest = new TipoClienteBuilder();

            ////Segmento
            //segmentoTest.WithId(segmentoID);
            //segmentoTest.WithDescricao("Segmento de Teste");
            //segmentoTest.WithDataCriacao(DateTime.Now);
            //segmentoTest.WithUsuarioIdCriacao(Guid.NewGuid());
            //segmentoTest.WithAtivo(true);
            //_unitOfWork.SegmentoRepository.Insert(segmentoTest);
            //var segmento = _unitOfWork.SegmentoRepository.GetAsync(c => c.ID.Equals(segmentoID));

            ////Tipo Cliente
            //tipoClienteTest.WithID(tipoclienteID);
            //tipoClienteTest.WithNome("Teste Nome");
            //tipoClienteTest.WithDescricao("Teste Descrição");
            //tipoClienteTest.WithDataCriacao(DateTime.Now);
            //tipoClienteTest.WithAtivo(true);
            //_unitOfWork.TipoClienteRepository.Insert(tipoClienteTest);
            //var tipocliente = _unitOfWork.TipoClienteRepository.GetAsync(a=>a.ID.Equals(tipoclienteID));

            //contaClienteTest.WithId(usuarioId);
            //contaClienteTest.WithDocumento("281.566.498-44");
            //contaClienteTest.WithCodigoPrincipal("1547896534");
            //contaClienteTest.WithNome("Luciano Carlos de Jesus");
            //contaClienteTest.WithApelido("Luciano Carlos");
            //contaClienteTest.WithTipoClienteId(Guid.NewGuid());
            //contaClienteTest.WithSegmentoId(segmentoID);
            //contaClienteTest.WithDataNascimentoFundacao(DateTime.Now);
            //contaClienteTest.WithContato("Contato Cadastrado Conta Cliente");
            //contaClienteTest.WithCEP("13.184.567-23");
            //contaClienteTest.WithEndereco("Rua blablabla");
            //contaClienteTest.WithCidade("Campinas");
            //contaClienteTest.WithUF("SP");
            //contaClienteTest.WithTelefone("19982066655");
            //contaClienteTest.WithEmail("luciano.jesus@performait.com");
            //contaClienteTest.Build();

            //_unitOfWork.ContaClienteRepository.Insert(contaClienteTest);
            //try
            //{
            //    var retInsert = _unitOfWork.Commit();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}
            var clienteExibeDados = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.Documento.Equals("31691301884"));

            //Assert.IsTrue(retInsert);
            Assert.IsNotNull(clienteExibeDados);
            
        }
    }
}