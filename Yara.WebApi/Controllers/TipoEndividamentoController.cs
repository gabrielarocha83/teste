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
    [RoutePrefix("typeofdebt")]
    [Authorize]
    public class TipoEndividamentoController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceTipoEndividamento _tipoEndividamento;
        private readonly TipoEndividamentoValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="tipoEndividamento"></param>
        /// <param name="log"></param>
        public TipoEndividamentoController(IAppServiceTipoEndividamento tipoEndividamento, IAppServiceLog log)
        {
            _log = log;
            _tipoEndividamento = tipoEndividamento;
            _validator = new TipoEndividamentoValidator();
        }

        /// <summary>
        /// Busca todos os tipo de endividamentos
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/typeofdebt")]
        public async Task<GenericResult<IQueryable<TipoEndividamentoDto>>> Get(ODataQueryOptions<TipoEndividamentoDto> options)
        {
            var result = new GenericResult<IQueryable<TipoEndividamentoDto>>();
            try
            {
                var feriado = await _tipoEndividamento.GetAllAsync();
                result.Result = options.ApplyTo(feriado.AsQueryable()).Cast<TipoEndividamentoDto>();
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
        /// Busca tipo de endividamento por ID
        /// </summary>
        /// <param name="id">Código do tipo de endividamento</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/gettypeofdebt/{id:guid}")]
        public async Task<GenericResult<TipoEndividamentoDto>> Get(Guid id)
        {
            var result = new GenericResult<TipoEndividamentoDto>();

            try
            {
                result.Result = await _tipoEndividamento.GetAsync(g => g.ID.Equals(id));
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
        /// Adiciona um tipo de endividamento
        /// </summary>
        /// <param name="value">Tipo de endividamento</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/inserttypeofdebt")]
        [ResponseType(typeof(TipoEndividamentoDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoEndividamento_Inserir")]
        public async Task<GenericResult<TipoEndividamentoDto>> Post(TipoEndividamentoDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<TipoEndividamentoDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    value.UsuarioIDCriacao = new Guid(userid);
                    result.Success = await _tipoEndividamento.InsertAsync(value);

                    var descricao = $"Inseriu um novo Tipo de Endividamento: {value.Tipo}";
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
        /// Atualiza um tipo de endividamento
        /// </summary>
        /// <param name="value">Tipo de endividamento</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updatetypeofdebt")]
        [ResponseType(typeof(TipoEndividamentoDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoEndividamento_Editar")]
        public async Task<GenericResult<TipoEndividamentoDto>> Put(TipoEndividamentoDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<TipoEndividamentoDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    value.UsuarioIDAlteracao = new Guid(userid);
                    result.Success = await _tipoEndividamento.Update(value);
                    var descricao = $"Atualizou o Tipo de Endividamento {value.Tipo}";
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
    }
}
