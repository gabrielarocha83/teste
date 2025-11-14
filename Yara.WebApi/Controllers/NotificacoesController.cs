using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("notifications")]
    [Authorize]
    public class NotificacoesController : ApiController
    {

        private readonly IAppServiceNotificacoes _serviceNotificacoes;
        private readonly IAppServiceLog _log;

        public NotificacoesController(IAppServiceNotificacoes serviceNotificacoes, IAppServiceLog appServiceLog)
        {
            _serviceNotificacoes = serviceNotificacoes;
            _log = appServiceLog;
        }

        /// <summary>
        /// Envia email com as pendencias do Cockpit dos usuários pos empresa
        /// </summary>
        /// <param name="empresa"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getnotifications/{empresa}")]
        public async Task<GenericResult<IEnumerable<KeyValuePair<bool, Guid>>>> Get(string empresa)
        {
            var result = new GenericResult<IEnumerable<KeyValuePair<bool, Guid>>>();

            try
            {
                result.Result = await _serviceNotificacoes.NotificacoesCockpitUsuarios(empresa, ConfigurationManager.AppSettings["URL_cockpit_portal"], ConfigurationManager.AppSettings["URL_cadastroCliente_portal"]);
                result.Success = true;
            }
            catch(Exception e)
            {
                result.Success = false;
                result.Errors = new[] { Resources.Resources.Error };
                var error = new ErrorsYara();
                error.ErrorYara(e);
            }

            return result;
        }

    }
}
