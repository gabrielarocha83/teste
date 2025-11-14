
using System;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Yara.Data.Entity.Context;
using Yara.Data.Entity.Repository;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.Repository
{
    [TestFixture]
    public class LogTests
    {

        private IUnitOfWork _unitOfWork;

        [TestFixtureSetUp]
        public void Initializer()
        {
            var context = new YaraDataContext();
            _unitOfWork = new UnitOfWork(context);
        }

        [Test, Order(1)]
        public async Task Insert_ShouldSaveLogDatabase()
        {
            //Arrange
            var userLog = await _unitOfWork.UsuarioRepository.GetAsync(c => c.Login.Equals("cpjunior"));

            var LogTest = new LogBuilder()
                .WithDescricao("Teste Integrado da conta cliente")
                .WithIP("10.172.16.10")
               .WithUsuarioID(userLog.ID)
               .WithUsuario("Junior Porfirio")
                .WithIdioma("pt-BR")
                .WithNavegador("Internet Explorer")
                .WithPagina("Home")
                .WithLevel(EnumLogLevel.ContaCliente)
                .Build();

            //Act
            _unitOfWork.LogRepository.Insert(LogTest);
            var boolInsert = _unitOfWork.Commit();

            //Assert
            Assert.IsTrue(boolInsert);
        }

        [Test, Order(2)]
        [TestCase("Junior", EnumLogLevel.ContaCliente, null, null, null)]
        [TestCase("Junior", EnumLogLevel.ContaCliente, null, null,null)]
        [TestCase("Junior", 0, null,"2017-07-01" , "2017-07-31")]
        public async Task BuscaLog_ShouldGetLogsFromDatabase(string user, int level, Guid? transacao, DateTime? dtinicio, DateTime? dtfim)
        {

            //Arrange
            var buscaLog = new BuscaLogs()
            {
                Usuario = user,
                LogLevel = level,
                IDTransacao = transacao,
                DataInicio = dtinicio,
                DataFim = dtfim
            };

            //Act
            var listLogs = await _unitOfWork.LogRepository.BuscaLog(buscaLog);


            //Assert
            Assert.IsTrue(listLogs.Any());
        }

    }

}

