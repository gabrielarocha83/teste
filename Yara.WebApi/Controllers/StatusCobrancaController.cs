using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.Extensions;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("collectstatus")]
    [Authorize]
    public class StatusCobrancaController : ApiController
    {
        private readonly IAppServiceStatusCobranca _statusCobranca;
        private readonly IAppServiceLog _log;
        private readonly StatusCobrancaValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="statusCobranca"></param>
        /// <param name="log"></param>
        public StatusCobrancaController(IAppServiceStatusCobranca statusCobranca, IAppServiceLog log)
        {
            _statusCobranca = statusCobranca;
            _log = log;
            _validator = new StatusCobrancaValidator();
        }

        /// <summary>
        /// Lista os status de cobrança utilizando os filtros enviados
        /// </summary>
        /// <param name="options">Filtros de Status de Cobrança</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/list")]
        public async Task<GenericResult<IQueryable<StatusCobrancaDto>>> Get(ODataQueryOptions<StatusCobrancaDto> options)
        {
            var result = new GenericResult<IQueryable<StatusCobrancaDto>>();

            try
            {
                var statusCobranca = await _statusCobranca.GetAllAsync();

                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(statusCobranca.AsQueryable(), new ODataQuerySettings()).Cast<StatusCobrancaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(statusCobranca.AsQueryable()).Cast<StatusCobrancaDto>();
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
        /// Busca status de cobrança por ID
        /// </summary>
        /// <param name="id">ID do Status de Cobrança</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/get/{id:guid}")]
        public async Task<GenericResult<StatusCobrancaDto>> Get(Guid id)
        {
            var result = new GenericResult<StatusCobrancaDto>();

            try
            {
                result.Result = await _statusCobranca.GetAsync(g => g.ID.Equals(id));
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
        /// Adiciona um novo status de cobrança
        /// </summary>
        /// <param name="value">Status de Cobrança</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insert")]
        [ResponseType(typeof(StatusCobrancaDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "StatusCobranca_Inserir")]
        public GenericResult<StatusCobrancaDto> Post(StatusCobrancaDto value)
        {
            var result = new GenericResult<StatusCobrancaDto>();
            var validationResult = _validator.Validate(value);

            if (validationResult.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    value.UsuarioIDCriacao = new Guid(userid);
                    result.Success = _statusCobranca.Insert(value);

                    var descricao = $"Inseriu um Status de Cobrança {value.Descricao}";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
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
                result.Errors = validationResult.GetErrors();
           
            return result;
        }

        /// <summary>
        /// Atualiza um status de cobrança
        /// </summary>
        /// <param name="value">Status de Cobrança</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(StatusCobrancaDto))]
        [Route("v1/update")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "StatusCobranca_Editar")]
        public async Task<GenericResult<StatusCobrancaDto>> Put(StatusCobrancaDto value)
        {
            var result = new GenericResult<StatusCobrancaDto>();
            var validationResult = _validator.Validate(value);

            if (validationResult.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    value.UsuarioIDAlteracao = new Guid(userid);
                    result.Success = await _statusCobranca.Update(value);

                    var descricao = $"Atualizou um Status de Cobrança {value.Descricao}";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
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
                result.Errors = validationResult.GetErrors();
           
            return result;
        }

        /// <summary>
        /// Inativa um status de cobrança
        /// </summary>
        /// <param name="id">Status de Cobrança</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactive/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "StatusCobranca_Editar")]
        public async Task<GenericResult<StatusCobrancaDto>> Delete(Guid id)
        {
            var result = new GenericResult<StatusCobrancaDto>();

            try
            {
                var value = await _statusCobranca.GetAsync(c => c.ID.Equals(id));
                result.Success = await _statusCobranca.Inactive(id);

                var descricao = $"Inativou um Status de Cobrança {value.Descricao}";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, id);
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
