using System;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using Yara.AppService;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Mappings;
using Yara.Data.Entity.Context;
using Yara.Data.Entity.Repository;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Tests.AppService
{
    public class SendEmailTest
    {
        private readonly IUnitOfWork _unitOfWork;

        public SendEmailTest()
        {
            var context = new YaraDataContext();
            _unitOfWork = new UnitOfWork(context);
            Mapper.Initialize(x =>
            {
                x.AddProfile(new MappingProfile());
            });
        }

        [Test]
        public async Task SendEmailsPropostaLimiteCredito()
        {
            try
            {
                //usuario
                var usuarios = new AppServiceUsuario(_unitOfWork);
                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));

                var propostaId = new Guid("C1A3C01F-90CA-41A7-A88A-04FA925096A8");
                //var proposta = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.EmpresaID.Equals("Y") && c.ID.Equals(propostaId));

                var fluxo = new PropostaLCComiteDto();
                fluxo.EmpresaID = "Y";
                fluxo.PropostaLCID = propostaId;
                fluxo.UsuarioID = usuario.ID;

                var email = new AppServiceEnvioEmail(_unitOfWork);
                var sendemail = fluxo.MapTo<PropostaLCComiteDto>();

                await email.SendMailComiteLC(sendemail, "");

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Test]
        public async Task SendEmailsPropostaAbono()
        {
            try
            {
                //usuario
                var usuarios = new AppServiceUsuario(_unitOfWork);
                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));

                var propostaId = new Guid("9A90BCFD-DDD5-44A9-B31D-7E3FC7B9DAF8");
                //var proposta = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.EmpresaID.Equals("Y") && c.ID.Equals(propostaId));

                var fluxo = new PropostaAbonoComiteDto();
                fluxo.EmpresaID = "Y";
                fluxo.PropostaAbonoID = propostaId;
                fluxo.UsuarioID = usuario.ID;

                var email = new AppServiceEnvioEmail(_unitOfWork);
                var sendemail = fluxo.MapTo<PropostaAbonoComiteDto>();

                await email.SendMailComiteAbono(sendemail, "");

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Test]
        public async Task SendEmailsPropostaProrrogacao()
        {
            try
            {
                //usuario
                var usuarios = new AppServiceUsuario(_unitOfWork);
                var usuario = await usuarios.GetAsync(c => c.Login.Equals("cpjunior"));

                var propostaId = new Guid("9FF02708-A741-484A-B0C0-4DA123F3726A");
                //var proposta = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.EmpresaID.Equals("Y") && c.ID.Equals(propostaId));

                var fluxo = new PropostaProrrogacaoComiteDto();
                fluxo.EmpresaID = "Y";
                fluxo.PropostaProrrogacaoID = propostaId;
                fluxo.UsuarioID = usuario.ID;

                var email = new AppServiceEnvioEmail(_unitOfWork);
                var sendemail = fluxo.MapTo<PropostaProrrogacaoComiteDto>();

                await email.SendMailComiteProrrogacao(sendemail, "");

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
    }
}
