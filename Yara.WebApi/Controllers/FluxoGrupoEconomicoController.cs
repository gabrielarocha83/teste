using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.Extensions;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("flowgroup")]
    [Authorize]
    public class FluxoGrupoEconomicoController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceFluxoGrupoEconomico _fluxoGrupoEconomico;
        private readonly FluxoGrupoEconomicoValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="fluxoGrupoEconomico"></param>
        /// <param name="log"></param>
        public FluxoGrupoEconomicoController(IAppServiceFluxoGrupoEconomico fluxoGrupoEconomico, IAppServiceLog log)
        {
            _fluxoGrupoEconomico = fluxoGrupoEconomico;
            _log = log;
            _validator = new FluxoGrupoEconomicoValidator();
        }

        /// <summary>
        /// Lista os Fluxos de Liberação de Grupos Econômicos trazendo o nome do aprovador.
        /// </summary>
        /// <param name="options">OData</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getgroup")]
        public async Task<GenericResult<IQueryable<FluxoGrupoEconomicoDto>>> GetApprover(ODataQueryOptions<FluxoGrupoEconomicoDto> options)
        {
            var result = new GenericResult<IQueryable<FluxoGrupoEconomicoDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                var retarea = await _fluxoGrupoEconomico.GetAllFilterAsync(c => c.EmpresaID.Equals(empresa));

                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retarea.AsQueryable(), new ODataQuerySettings()).Cast<FluxoGrupoEconomicoDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retarea.AsQueryable()).Cast<FluxoGrupoEconomicoDto>();
                result.Count = totalReg > 0 ? totalReg : result.Result.Count();

                result.Success = true;
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

        /// <summary>
        /// Insere um nível no Fluxo de Liberação de Grupos Econômicos.
        /// </summary>
        /// <param name="grupoEconomicoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertgroup")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoGrupoEconomico_Inserir")]
        public async Task<GenericResult<FluxoGrupoEconomicoDto>> Post(FluxoGrupoEconomicoDto grupoEconomicoDto)
        {
            var result = new GenericResult<FluxoGrupoEconomicoDto>();
            var validation = _validator.Validate(grupoEconomicoDto);

            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var empresa = Request.Properties["Empresa"].ToString();
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    grupoEconomicoDto.ID = Guid.NewGuid();
                    grupoEconomicoDto.DataCriacao = DateTime.Now;
                    grupoEconomicoDto.UsuarioIDCriacao = new Guid(userLogin);
                    grupoEconomicoDto.EmpresaID = empresa;

                    result.Success = await _fluxoGrupoEconomico.InsertAsync(grupoEconomicoDto);

                    var descricao = $"O usuário {userLogin}, inseriu o perfil de {grupoEconomicoDto.PerfilId} no nível {grupoEconomicoDto.Nivel} como aprovador de fluxo de liberação de grupo econômico.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
                    _log.Create(logDto);
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
            }
            else
            {
                result.Errors = validation.GetErrors();
            }
            return result;
        }

        /// <summary>
        /// Atualiza um nível no Fluxo de Liberação de Grupos Econômicos.
        /// </summary>
        /// <param name="grupoEconomicoDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updategroup")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoGrupoEconomico_Editar")]
        public async Task<GenericResult<FluxoGrupoEconomicoDto>> Put(FluxoGrupoEconomicoDto grupoEconomicoDto)
        {
            var result = new GenericResult<FluxoGrupoEconomicoDto>();
            var validation = _validator.Validate(grupoEconomicoDto);

            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var empresa = Request.Properties["Empresa"].ToString();
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    grupoEconomicoDto.DataAlteracao = DateTime.Now;
                    grupoEconomicoDto.UsuarioIDAlteracao = new Guid(userLogin);
                    grupoEconomicoDto.EmpresaID = empresa;

                    result.Success = await _fluxoGrupoEconomico.Update(grupoEconomicoDto);

                    var descricao = $"O usuário {userLogin}, atualizou o nível {grupoEconomicoDto.Nivel} com o perfil de {grupoEconomicoDto.PerfilId} como aprovador de fluxo de liberação de grupo econômico.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, grupoEconomicoDto.ID);
                    _log.Create(logDto);
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Errors = new[] { Resources.Resources.Error };
                    var error = new ErrorsYara();
                    error.ErrorYara(e);
                }
            }
            else
            {
                result.Errors = validation.GetErrors();
            }

            return result;
        }

        /// <summary>
        /// Inativa um nível no Fluxo de Liberação de Grupos Econômicos.
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactivegroup/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoGrupoEconomico_Excluir")]
        public async Task<GenericResult<FluxoGrupoEconomicoDto>> Delete(Guid id)
        {
            var result = new GenericResult<FluxoGrupoEconomicoDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _fluxoGrupoEconomico.InactiveAsync(id, new Guid(userLogin));

                var descricao = $"O usuário {userLogin}, desativou o fluxo de liberação de grupo econômico {id}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, id);
                _log.Create(logDto);
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
