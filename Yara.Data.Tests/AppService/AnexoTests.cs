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
    [TestFixture]
    public class AnexoTests
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
        public void ShoulInsertAttachmentDatabase()
        {

            //Arrange
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Act
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                //var usuarios = new AppServiceUsuario(unitofWork);
                //var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));

                var anexo = new AppServiceAnexo(unitofWork);

                var codigocontacliente = new AnexoBuilder()
                    .WithId(Guid.NewGuid())
                    .WithDescricao("Registro Geral")
                    .WithDescricaoAbreviado("RG")
                    .WithForm(1)
                    .WithFormDesc("Formulario de Rascunho")
                    .WithAtivo(true)
                    .WithObrigatorio(true)
                    .Build();
                var boolAnexo = anexo.Insert(codigocontacliente.MapTo<AnexoDto>());


                //Assert
                Assert.IsTrue(boolAnexo);
            }

        }

        //[Test, Order(2)]
        //public async Task ShouldSearchTheAttachment()
        //{
        //    using (AsyncScopedLifestyle.BeginScope(_container))
        //    {
        //        //Arrange
        //        var unitofWork = _container.GetInstance<IUnitOfWork>();
        //        var appServiceAnexo = new AppServiceAnexo(unitofWork);

        //        //Act
        //        var resultSearch = await appServiceAnexo.GetAllFilterAsync(c => c.DescricaoAbreviado.Contains("RG"));

        //        //Assert
        //        Assert.IsNotNull(resultSearch);
        //        Assert.IsTrue(resultSearch.Any());
        //    }
        //}

        [Test, Order(3)]
        public async Task ShouldListAllTheAttachment()
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                //Arrange
                var unitofWork = _container.GetInstance<IUnitOfWork>();
                var appServiceAnexo = new AppServiceAnexo(unitofWork);

                //Act
                var resultSearch = await appServiceAnexo.GetAllAsync();

                //Assert
                Assert.IsTrue(resultSearch.Any());
            }
        }

        //[Test, Order(4)]
        //public async Task ShouldSearchOneAttachment()
        //{
        //    using (AsyncScopedLifestyle.BeginScope(_container))
        //    {
        //        //Arrange
        //        var unitofWork = _container.GetInstance<IUnitOfWork>();
        //        var appServiceAnexo = new AppServiceAnexo(unitofWork);

        //        //Act
        //        var resultSearch = await appServiceAnexo.GetAsync(c => c.DescricaoAbreviado.Equals("RG"));

        //        //Assert
        //        Assert.IsNotNull(resultSearch);
        //        Assert.IsTrue(resultSearch.ID != null);
        //    }
        //}

        //[Test, Order(5)]
        //public async Task ShouldUpdateBasicDataOfAttachment()
        //{
        //    using (AsyncScopedLifestyle.BeginScope(_container))
        //    {
        //        //Arrange
        //        var unitofWork = _container.GetInstance<IUnitOfWork>();
        //        var appAnexo = new AppServiceAnexo(unitofWork);
        //        var serviceUsuario = new AppServiceUsuario(unitofWork);

        //        var anexo = await appAnexo.GetAsync(c => c.DescricaoAbreviado.Equals("RG"));
        //        var user = await serviceUsuario.GetAsync(c => c.Login.Equals("cpjunior"));

        //        var basicDataAnexo = new AnexoDto()
        //        {
        //            ID = anexo.ID,
        //            Descricao = anexo.Descricao,
        //            Formulario = anexo.Formulario,
        //            DescricaoFormulario = anexo.DescricaoFormulario,
        //            Obrigatorio = false,
        //            UsuarioIDAlteracao = user.ID,
        //            DataAlteracao = DateTime.Now
        //        };

        //        //Act
        //        var resultUpdate = await appAnexo.Update(basicDataAnexo);

        //        //Assert
        //        Assert.IsNotNull(anexo);
        //        Assert.IsTrue(resultUpdate);
        //    }
        //}

    }
}
