using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.Data.Entity.Context;
using Yara.Data.Entity.Repository;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.Repository
{
    public class ContaClienteTests
    {
        private IUnitOfWork _unitOfWork;

        [OneTimeSetUp]
        public void Initializer()
        {
            var context = new YaraDataContext();
            //Database.SetInitializer(new YaraDBInitializerTest());
            _unitOfWork = new UnitOfWork(context);
        }

        [Test]
        [TestCase("Junior", "", "", "", "")]
        [TestCase("", "123", "", "", "")]
        [TestCase("", "", "316", "", "")]
        [TestCase("", "", "", "Claudemir", "")]
        [TestCase("", "", "", "", "Tipo")]
        public async Task BuscaContaCliente_ShouldSearchContaClienteFromDatabase(string apelido, string codigoprincipal, string documento, string nome, string grupoeconomico)
        {

            var busca = new BuscaContaCliente()
            {
                Apelido = apelido,
                CodigoPrincipal = codigoprincipal,
                Documento = documento,
                Nome = nome,
                GrupoEconomico = grupoeconomico
            };

            var contaClienteSearch =
                await _unitOfWork.ContaClienteRepository.BuscaContaCliente(busca);

            Assert.IsNotNull(contaClienteSearch);
            Assert.IsTrue(contaClienteSearch.Any());

        }




    }
}