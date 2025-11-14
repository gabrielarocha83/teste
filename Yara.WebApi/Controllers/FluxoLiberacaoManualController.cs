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
    [RoutePrefix("flow")]
    [Authorize]
    public class FluxoLiberacaoManualController : ApiController
    {
        private readonly IAppServiceFluxoLiberacaoManual _credito;
        private readonly IAppServiceLog _log;
        private readonly FluxoLiberacaoManualValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="credito"></param>
        /// <param name="log"></param>
        public FluxoLiberacaoManualController(IAppServiceFluxoLiberacaoManual credito, IAppServiceLog log)
        {
            _credito = credito;
            _log = log;
            _validator = new FluxoLiberacaoManualValidator();
        }

        /// <summary>
        /// Insere Nivel para fluxo de liberação Manual
        /// </summary>
        /// <param name="fluxoLimiteCreditoDto">Objeto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertlimit")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoLimite_Inserir")]
        public async Task<GenericResult<FluxoLiberacaoManualDto>> Post(FluxoLiberacaoManualDto fluxoLimiteCreditoDto)
        {

            fluxoLimiteCreditoDto.ID = Guid.NewGuid();
            var objuserLogin = User.Identity as ClaimsIdentity;
            var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

            var result = new GenericResult<FluxoLiberacaoManualDto>();
            var validation = _validator.Validate(fluxoLimiteCreditoDto);

            if (validation.IsValid)
            {
                try
                {
                    fluxoLimiteCreditoDto.DataCriacao = DateTime.Now;
                    fluxoLimiteCreditoDto.UsuarioIDCriacao = new Guid(userLogin);

                    result.Success = await _credito.InsertAsync(fluxoLimiteCreditoDto);

                    var descricao = $"Inseriu fluxo de limite de credito novo com valor inicial de: {fluxoLimiteCreditoDto.ValorDe} até o valor final de: {fluxoLimiteCreditoDto.ValorAte}";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
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
        /// Lista Fluxo de liberação Manual trazendo o nome do aprovador
        /// </summary>
        /// <param name="options">OData</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getlimit")]
        public async Task<GenericResult<IQueryable<FluxoLiberacaoManualDto>>> GetApprover(ODataQueryOptions<FluxoLiberacaoManualDto> options)
        {
            var result = new GenericResult<IQueryable<FluxoLiberacaoManualDto>>();
            try
            {
                var retarea = await _credito.GetAllListFluxoAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retarea.AsQueryable(), new ODataQuerySettings()).Cast<FluxoLiberacaoManualDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retarea.AsQueryable()).Cast<FluxoLiberacaoManualDto>();
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
        /// Busca fluxo de liberação manual por Guid
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodlimit/{id:guid}")]
        public async Task<GenericResult<FluxoLiberacaoManualDto>> Get(Guid id)
        {
            var result = new GenericResult<FluxoLiberacaoManualDto>();
            try
            {
                result.Result = await _credito.GetAsync(g => g.ID.Equals(id));
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
        /// Atuaizar um fluxo de liberação Manual
        /// </summary>
        /// <param name="fluxoLimiteCreditoDto">Objeto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updatelimit")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoLimite_Editar")]
        public async Task<GenericResult<FluxoLiberacaoManualDto>> Put(FluxoLiberacaoManualDto fluxoLimiteCreditoDto)
        {
            var result = new GenericResult<FluxoLiberacaoManualDto>();
            var validation = _validator.Validate(fluxoLimiteCreditoDto);
            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    fluxoLimiteCreditoDto.UsuarioIDAlteracao = new Guid(userLogin);
                    fluxoLimiteCreditoDto.DataAlteracao = DateTime.Now;
                    result.Success = await _credito.Update(fluxoLimiteCreditoDto);
                    var descricao = $"Atualizou Fluxo de Limite de Credito do nivel: {fluxoLimiteCreditoDto.Nivel} do Id: {fluxoLimiteCreditoDto.ID}";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, fluxoLimiteCreditoDto.ID);
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
        /// Metodo para inativar um fluxo de liberação Manual
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactivelimit/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoLimite_Excluir")]
        public async Task<GenericResult<FluxoLiberacaoManualDto>> Delete(Guid id)
        {
            var result = new GenericResult<FluxoLiberacaoManualDto>();
            LogDto logDto;
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _credito.Inactive(id);

                var descricao = $" Usuario {userLogin} desativou o fluxo de limite de credito: {id}";
                var level = EnumLogLevelDto.Info;
                logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, id);
                _log.Create(logDto);
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
