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
    [RoutePrefix("moneysource")]
    [Authorize]
    public class OrigemRecursoController : ApiController
    {
        private readonly IAppServiceOrigemRecurso _origemRecurso;
        private readonly OrigemRecursoValidator _validator;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="origemRecurso"></param>
        /// <param name="log"></param>
        public OrigemRecursoController(IAppServiceOrigemRecurso origemRecurso, IAppServiceLog log)
        {
            _validator = new OrigemRecursoValidator();
            _origemRecurso = origemRecurso;
            _log = log;
        }

        /// <summary>
        /// Retorna todas as origens de recurso, possibilitando utilizar filtros OData.
        /// </summary>
        /// <param name="options">Filtros OData</param>
        /// <returns>Lista de OrigemRecurso</returns>
        [HttpGet]
        [Route("v1/list")]
        public async Task<GenericResult<IQueryable<OrigemRecursoDto>>> Get(ODataQueryOptions<OrigemRecursoDto> options)
        {
            var result = new GenericResult<IQueryable<OrigemRecursoDto>>();

            try
            {
                var origens = await _origemRecurso.GetAllAsync();
                int totalReg = 0;
          
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(origens.AsQueryable(), new ODataQuerySettings()).Cast<OrigemRecursoDto>();
                    totalReg = filtro.Count();
                }

                result.Result = options.ApplyTo(origens.AsQueryable()).Cast<OrigemRecursoDto>();
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
        /// Busca origem de recurso pelo ID.
        /// </summary>
        /// <param name="id">ID da Origem de Recurso</param>
        /// <returns>OrigemRecursoDto</returns>
        [HttpGet]
        [Route("v1/get/{id:guid}")]
        public async Task<GenericResult<OrigemRecursoDto>> Get(Guid id)
        {
            var result = new GenericResult<OrigemRecursoDto>();

            try
            {
                result.Result = await _origemRecurso.GetAsync(c => c.ID.Equals(id));
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
        /// Insere uma nova origem de recurso.
        /// </summary>
        /// <param name="value">OrigemRecursoDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insert")]
        [ResponseType(typeof(OrigemRecursoDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "OrigemRecurso_Inserir")]
        public GenericResult<OrigemRecursoDto> Post(OrigemRecursoDto value)
        {
            var result = new GenericResult<OrigemRecursoDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;

            if (validationResult.IsValid)
            {
                try
                {
                    var user = User.Identity as ClaimsIdentity;
                    var userID = user.Claims.First(c => c.Type.Equals("Usuario")).Value;

                    value.ID = Guid.NewGuid();
                    value.UsuarioIDCriacao = new Guid(userID);
                    result.Success = _origemRecurso.Insert(value);

                    var descricao = $"Inseriu uma nova Origem de Recurso com o nome {value.Nome}";
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
        /// Atualiza uma Origem de Recurso
        /// </summary>
        /// <param name="value">OrigemRecursoDto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/update")]
        [ResponseType(typeof(OrigemRecursoDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "OrigemRecurso_Editar")]
        public async Task<GenericResult<OrigemRecursoDto>> Put(OrigemRecursoDto value)
        {
            var result = new GenericResult<OrigemRecursoDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;

            if (validationResult.IsValid)
            {
                try
                {
                    var user = User.Identity as ClaimsIdentity;
                    var userID = user.Claims.First(c => c.Type.Equals("Usuario")).Value;

                    value.UsuarioIDAlteracao = new Guid(userID);
                    result.Success = await _origemRecurso.Update(value);

                    var descricao = $"Atualizou a Origem de Recurso com o nome {value.Nome}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, value.ID);
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
        /// Inativa uma Origem de Recurso.
        /// </summary>
        /// <param name="id">Código do Motivo do Abono</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/disable/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "OrigemRecurso_Editar")]
        public async Task<GenericResult<OrigemRecursoDto>> Delete(Guid id)
        {
            var result = new GenericResult<OrigemRecursoDto>();
            LogDto logDto = null;

            try
            {
                var grupo = await _origemRecurso.GetAsync(c => c.ID.Equals(id));
                result.Success = await _origemRecurso.Inactive(id);

                var descricao = $"Inativou a Origem de Recurso com o nome {grupo.Nome}";
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
