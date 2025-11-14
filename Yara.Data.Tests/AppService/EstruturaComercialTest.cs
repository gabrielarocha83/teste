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
using Yara.Domain.Repository;

namespace Yara.Data.Tests.AppService
{
    public class EstruturaComercialTest
    {
        private Container _container;

        [OneTimeSetUp]
        public void Initializer()
        {
            _container = new Container();


            //DI of Class project
            _container = IoC.SimpleInjectorContainer.RegisterServices();

            //Auto Mapper Class AppService Initializer
            Mapper.Initialize(x =>
            {
                x.AddProfile(new MappingProfile());
            });
        }
        
        [Test, Order(1)]
        public async Task UpdateAsync_ShouldSaveBasicDataOfCommercialStructure()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange

                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var serviceEstruturaComercial = new AppServiceEstruturaComercial(unitofWork);
             
                var serviceUsuario = new AppServiceUsuario(unitofWork);
               
                var estruturaComercial = await serviceEstruturaComercial.GetAllFilterAsync(c => c.CodigoSap.Equals("0300"));
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

                var basicData = new EstruturaComercialDiretoriaDto()
                {
                    CodigoSap = "DDD",
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    UsuarioIDCriacao =  user.ID,
                    Nome = "DDD01",
                    Gerentes = new List<EstruturaComercialDto>
                    {
                        new EstruturaComercialDto()
                        {
                            CodigoSap = "0300",
                            Ativo = true
                        }
                    }
                   

                };

                //Act
                var resultUpdate= await serviceEstruturaComercial.Insert(basicData);

                //Assert
                Assert.IsTrue(resultUpdate);
            }
        }
        
        [Test, Order(2)]
        [TestCase("G")]
        [TestCase("D")]
        [TestCase("C")]
        public async Task GetListAccountClient_ShouldSearchStructurePaperListTheCommercialStructure(string papel)
        {

            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange

                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceContaCliente = new AppServiceEstruturaComercial(unitofWork);
               
                //Act
                var resultSearch = await appServiceContaCliente.GetEstruturaComercialByPaper(papel);

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }


        }


        [Test, Order(3)]
        public async Task GetAsync_ShouldGetListStructureWithCod()
        {

            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange

                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var serviceEstruturaComercial = new AppServiceEstruturaComercial(unitofWork);
              

                //Act
                var resultSearch = await serviceEstruturaComercial.GetAllFilterAsync(c => c.EstruturaComercialPapelID.Equals("D"));

                //Assert
                Assert.True(resultSearch.Any());
            }


        }

        [Test, Order(4)]
        public async Task Update_ShoulSaveAlterOfCommercialStructure()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange

                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var serviceEstruturaComercial = new AppServiceEstruturaComercial(unitofWork);
                var serviceUsuario = new AppServiceUsuario(unitofWork);
                var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

              

                var diretoriaDto = new EstruturaComercialDiretoriaDto( )
                {
                 CodigoSap =  "DDD",
                 UsuarioIDAlteracao = user.ID,
                 DataAlteracao =  DateTime.Now,
                 Ativo = true,
                 Gerentes = new List<EstruturaComercialDto>
                 {
                     new EstruturaComercialDto()
                     {
                         Ativo = false,
                         CodigoSap = "0300"
                     },
                     new EstruturaComercialDto()
                     {
                         Ativo = true,
                         CodigoSap = "0301"
                     }
                 }
                };

                //Act
                var resultUpdate = await serviceEstruturaComercial.Update(diretoriaDto);

                //Assert
                Assert.IsTrue(resultUpdate);
            }
        }

    }
}