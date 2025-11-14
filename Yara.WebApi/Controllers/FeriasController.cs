using System;
using System.Collections.Generic;
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
    [Authorize]
    [RoutePrefix("vacation")]
    public class FeriasController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceFerias _ferias;
        private readonly FeriasValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="ferias"></param>
        /// <param name="log"></param>
        public FeriasController(IAppServiceFerias ferias, IAppServiceLog log)
        {
            _log = log;
            _ferias = ferias;
            _validator = new FeriasValidator();
        }

        /// <summary>
        /// Busca todas as férias dos usuários
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getvacations")]
        public async Task<GenericResult<IQueryable<FeriasDto>>> Get(ODataQueryOptions<FeriasDto> options)
        {
            var result = new GenericResult<IQueryable<FeriasDto>>();

            try
            {
                var feriado = await _ferias.GetAllAsync();
                result.Result = options.ApplyTo(feriado.AsQueryable()).Cast<FeriasDto>();
                result.Count = result.Result.Count();

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
        /// Busca  feriaS pelo id
        /// </summary>
        /// <param name="id">Código do tipo de cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodvacation/{id:guid}")]
        public async Task<GenericResult<FeriasDto>> Get(Guid id)
        {
            var result = new GenericResult<FeriasDto>();

            try
            {
                result.Result = await _ferias.GetAsync(g => g.ID.Equals(id));
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
        /// Retorna as ferias do usuário
        /// </summary>
        /// <param name="id">Código do usuario</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodvacationuser/{id:guid}")]
        public async Task<GenericResult<IEnumerable<FeriasDto>>> GetUsuario(ODataQueryOptions<FeriasDto> options, Guid id)
        {
            var result = new GenericResult<IEnumerable<FeriasDto>>();

            try
            {
                var ferias = await _ferias.GetFeriasByIDUser(id);
                result.Result = options.ApplyTo(ferias.AsQueryable()).Cast<FeriasDto>();
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
        /// Adiciona  ferias.
        /// </summary>
        /// <param name="value">Férias</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertvacation")]
        [ResponseType(typeof(FeriasDto))]
        public async Task<GenericResult<FeriasDto>> Post(FeriasDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<FeriasDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    value.UsuarioIDCriacao = new Guid(userid);

                    var b = await _ferias.InsertAsync(value);
                    result.Success = b;

                    var descricao = $"Inseriu uma férias para o usuário do código: {value.UsuarioID}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
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
                result.Errors = validationResult.GetErrors();


            return result;
        }

        /// <summary>
        /// Atualiza ferias
        /// </summary>
        /// <param name="value">Férias</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(FeriasDto))]
        [Route("v1/updatevacation")]
        public async Task<GenericResult<FeriasDto>> Put(FeriasDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<FeriasDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {

                    value.UsuarioIDAlteracao = new Guid(userid);
                    result.Success = await _ferias.Update(value);
                    var descricao = $"Atualizou as férias do usuário com código {value.UsuarioID}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
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
        /// Inativa uma Férias
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactivevacation/{id:guid}")]
        public async Task<GenericResult<FeriasDto>> Delete(Guid id)
        {
            var result = new GenericResult<FeriasDto>();
            LogDto logDto;
            try
            {
                var feriado = await _ferias.GetAsync(c => c.ID.Equals(id));
                result.Success = await _ferias.Inactive(id);

                var descricao = $"Inativou a férias do usuário do código: {feriado.UsuarioID}";
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
