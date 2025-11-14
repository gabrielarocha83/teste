using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class FluxoLimiteCreditoTests
    {
        private Container _container;

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
        public async Task ShoulInsertFlowDatabase()
        {
            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();

                //usuario
                var usuarios = new AppServiceUsuario(unitofWork);
                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));

                //grupo
                var grupos = new AppServiceGrupo(unitofWork);
                var grupo = await grupos.GetAsync(c => c.Nome.Equals("CTC"));

                //estrutura
                var estruturas = new AppServiceEstruturaComercial(unitofWork);
                var estrutura = await estruturas.GetEstruturaComercialByPaper("D");

                var fluxoLimiteCredito = new AppServiceFluxoLiberacaoManual(unitofWork);

                var exp = new FluxoLimiteCreditoBuilder()
                    .WithId(Guid.NewGuid())
                    .WithNivel(1)
                    .WithValorDe(1)
                    .WithValorAte(100)
                    .WithUsuario(usuario.ID)
                    //.WithGrupo(grupo.ID)
                    //.WithEstrutura(estrutura.)
                    .WithDataCriacao(DateTime.Now)
                    .WithDataUsuarioCriacao(usuario.ID)
                    .Build();
                var boolAreaIrrigada = fluxoLimiteCredito.Insert(exp.MapTo<FluxoLiberacaoManualDto>());


                //Assert
                Assert.IsTrue(boolAreaIrrigada);
            }

        }

        [Test, Order(2)]
        public async Task ShouldSearchTheFlow()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceFluxoLimiteCredito = new AppServiceFluxoLiberacaoManual(unitofWork);

                //Act
                var resultSearch = await appServiceFluxoLimiteCredito.GetAllFilterAsync(c => c.Nivel.Equals(1));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(3)]
        public async Task ShouldListAllTheFlow()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceFluxoLimiteCredito = new AppServiceFluxoLiberacaoManual(unitofWork);

                //Act
                var resultSearch = await appServiceFluxoLimiteCredito.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        [Test, Order(4)]
        public async Task ShouldSearchOneFlow()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceFluxoLimiteCredito = new AppServiceFluxoLiberacaoManual(unitofWork);

                //Act
                var resultSearch = await appServiceFluxoLimiteCredito.GetAsync(c => c.Nivel.Equals(1));

                //Assert
                Assert.IsNotNull(resultSearch);
                Assert.IsTrue(resultSearch.ID != null);
            }
        }

        [Test, Order(5)]
        public async Task ShouldUpdateBasicDataOfFlow()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var fluxoLimiteCredito = new AppServiceFluxoLiberacaoManual(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);

                var appFluxoLimiteCredito = await fluxoLimiteCredito.GetAsync(c => c.Nivel.Equals(1));

                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicDataFluxoLimiteCredito = new FluxoLiberacaoManualDto()
                {
                    ID = appFluxoLimiteCredito.ID,
                    ValorDe = 10,
                    UsuarioIDAlteracao = user.ID,
                    DataAlteracao = DateTime.Now
                };

                //Act
                var resultUpdate = await fluxoLimiteCredito.Update(basicDataFluxoLimiteCredito);

                //Assert
                Assert.IsNotNull(appFluxoLimiteCredito);
                Assert.IsTrue(resultUpdate);
            }
        }
    }
}
