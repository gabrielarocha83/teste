using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Repository;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceNotificacoes : IAppServiceNotificacoes
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceNotificacoes(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<KeyValuePair<bool, Guid>>> NotificacoesCockpitUsuarios(string empresa, string urlCockpit, string urlContaClient)
        {
            var retornoEnvio = new List<KeyValuePair<bool, Guid>>();

            try
            {
                var resultado = await _unitOfWork.NotificacoesRepository.BuscaUsuariosCockpit(empresa);
                var listaNotificacoesDto = resultado.MapTo<IEnumerable<NotificacaoUsuarioDto>>().ToList();

                List<Guid> listaIdUsuario = listaNotificacoesDto.Select(s => s.ResponsavelId).Distinct().ToList();
                if (listaIdUsuario != null && listaIdUsuario.Count > 0)
                {

                    var email = new AppServiceEnvioEmail(_unitOfWork);

                    List<NotificacaoUsuarioDto> listaCockpit = null;
                    foreach (var usuarioId in listaIdUsuario)
                    {
                        listaCockpit = listaNotificacoesDto.Where(n => n.ResponsavelId == usuarioId).ToList();

                        KeyValuePair<bool, string> retornoEnvioEmail = await email.SendMailCockpitNotificacaoUsuario(listaCockpit, new UsuarioDto() { ID = listaCockpit[0].ResponsavelId, Email = listaCockpit[0].EmailResponsavel, Nome = listaCockpit[0].Responsavel }, empresa, urlCockpit, urlContaClient);

                        retornoEnvio.Add(new KeyValuePair<bool, Guid>(retornoEnvioEmail.Key, usuarioId));
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return retornoEnvio;
        }

    }
}
