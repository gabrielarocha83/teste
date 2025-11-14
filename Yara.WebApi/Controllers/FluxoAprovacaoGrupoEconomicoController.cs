using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("flowgroupaproval")]
    [Authorize]
    public class FluxoAprovacaoGrupoEconomicoController : ApiController
    {
        private readonly IAppServiceLiberacaoGrupoEconomicoFluxo _fluxo;
        private readonly IAppServiceUsuario _usuario;
        private readonly IAppServiceLog _log;
        private readonly IAppServiceEnvioEmail _email;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="credito"></param>
        /// <param name="log"></param>
        /// <param name="usuario"></param>
        /// <param name="email"></param>
        public FluxoAprovacaoGrupoEconomicoController(IAppServiceLiberacaoGrupoEconomicoFluxo credito, IAppServiceLog log, IAppServiceUsuario usuario, IAppServiceEnvioEmail email)
        {
            _fluxo = credito;
            _log = log;
            _usuario = usuario;
            _email = email;
        }

        /// <summary>
        /// Aprova ou Reprova um Grupo Econômico
        /// </summary>
        /// <param name="GrupoID">Código do Grupo</param>
        /// <param name="aprovar">Aprovar ou Reprovar</param>
        /// <param name="ClassificacaoID">Tipo de Classificação</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/approvalgroup/{GrupoID}/{aprovar}/{ClassificacaoID}")]
        public async Task<GenericResult<FluxoGrupoEconomicoDto>> Post(Guid GrupoID, bool aprovar, int ClassificacaoID)
        {
            var result = new GenericResult<FluxoGrupoEconomicoDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;
                var username = new Guid(userLogin);

                var fluxo = await _fluxo.AprovaReprovaLiberacaoGrupoEconomicoValue(aprovar, GrupoID, username, ClassificacaoID, empresa, url);
                var user = await _usuario.GetAsync(c => c.ID.Equals(username));

                result.Errors = new[] { fluxo.Value };
                result.Success = true;
                result.Count = 1;
            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { ex.Message };
                var error = new ErrorsYara();
                error.ErrorYara(ex);
            }
            catch (Exception e)
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
