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
    [RoutePrefix("flowextralimitrelease")]
    [Authorize]
    public class FluxoLiberacaoLCAdicionalController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceFluxoLiberacaoLCAdicional _creditoAdicional;
        private readonly FluxoLiberacaoLCAdicionalValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="log"></param>
        /// <param name="creditoAdicional"></param>
        public FluxoLiberacaoLCAdicionalController(IAppServiceLog log, IAppServiceFluxoLiberacaoLCAdicional creditoAdicional)
        {
            _log = log;
            _creditoAdicional = creditoAdicional;
            _validator = new FluxoLiberacaoLCAdicionalValidator();
        }

        /// <summary>
        /// Insere Nivel para fluxo de liberação de Limite de Crédito
        /// </summary>
        /// <param name="fluxo">Objeto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertlimit")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoLiberacaoLimiteCreditoAdicional_Inserir")]
        public async Task<GenericResult<FluxoLiberacaoLCAdicionalDto>> Post(FluxoLiberacaoLCAdicionalDto fluxo)
        {
            fluxo.ID = Guid.NewGuid();
            fluxo.Ativo = true;
            var objuserLogin = User.Identity as ClaimsIdentity;
            var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

            var result = new GenericResult<FluxoLiberacaoLCAdicionalDto>();
            var validation = _validator.Validate(fluxo);

            if (validation.IsValid)
            {
                try
                {
                    var empresa = Request.Properties["Empresa"].ToString();
                    fluxo.UsuarioIDCriacao = new Guid(userLogin);
                    fluxo.EmpresaID = empresa;
                    result.Success = await _creditoAdicional.InsertAsync(fluxo);
                    var descricao =
                        $"Inseriu fluxo de liberação de limite de credito adicional novo com valor inicial de: {fluxo.ValorDe} até o valor final de: {fluxo.ValorAte} para o perfil {fluxo.PrimeiroPerfilID}.";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
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
        /// Lista Fluxo de liberação de limite de credito trazendo o nome do perfil
        /// </summary>
        /// <param name="options">OData</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getlimit")]
        public async Task<GenericResult<IQueryable<FluxoLiberacaoLCAdicionalDto>>> GetLimit(ODataQueryOptions<FluxoLiberacaoLCAdicionalDto> options)
        {
            var result = new GenericResult<IQueryable<FluxoLiberacaoLCAdicionalDto>>();
            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                var ret = await _creditoAdicional.GetAllFilterAsync(c => c.EmpresaID.Equals(empresa));
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(ret.AsQueryable(), new ODataQuerySettings()).Cast<FluxoLiberacaoLCAdicionalDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(ret.AsQueryable()).Cast<FluxoLiberacaoLCAdicionalDto>();
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
        /// Busca fluxo de liberação de limite de credito por Guid
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodlimit/{id:guid}")]
        public async Task<GenericResult<FluxoLiberacaoLCAdicionalDto>> Get(Guid id)
        {
            var result = new GenericResult<FluxoLiberacaoLCAdicionalDto>();
            try
            {
                result.Result = await _creditoAdicional.GetAsync(g => g.ID.Equals(id));
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
        /// Atuaizar fluxo de liberação de limite de credito
        /// </summary>
        /// <param name="fluxo">Objeto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updatelimit")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoLiberacaoLimiteCreditoAdicional_Inserir")]
        public async Task<GenericResult<FluxoLiberacaoLCAdicionalDto>> Put(FluxoLiberacaoLCAdicionalDto fluxo)
        {
            var result = new GenericResult<FluxoLiberacaoLCAdicionalDto>();
            var validation = _validator.Validate(fluxo);
            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();
                    fluxo.EmpresaID = empresa;
                    fluxo.UsuarioIDAlteracao = new Guid(userLogin);
                    fluxo.DataAlteracao = DateTime.Now;
                    result.Success = await _creditoAdicional.Update(fluxo);
                    var descricao = $"Atualizou Fluxo de Limite de Credito Adicional do nivel: {fluxo.Nivel} do Id: {fluxo.ID}";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, fluxo.ID);
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
        /// Metodo para inativar fluxo de liberação de limite de credito
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactivelimit/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoLiberacaoLimiteCreditoAdicional_Excluir")]
        public async Task<GenericResult<FluxoLiberacaoLCAdicionalDto>> Delete(Guid id)
        {
            var result = new GenericResult<FluxoLiberacaoLCAdicionalDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _creditoAdicional.Inactive(id, new Guid(userLogin));

                var descricao = $" Usuario {userLogin} desativou o fluxo de limite de credito adicional: {id}";
                var level = EnumLogLevelDto.Info;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, id);
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
