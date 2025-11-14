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
    [RoutePrefix("motivoprorrogacao")]
    [Authorize]
    public class MotivoProrrogacaoController : ApiController
    {
        private readonly IAppServiceMotivoProrrogacao _motivoprorrogacao;
        private readonly MotivoProrrogacaoValidator _validator;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="motivoprorrogacao"></param>
        /// <param name="log"></param>
        public MotivoProrrogacaoController(IAppServiceMotivoProrrogacao motivoprorrogacao, IAppServiceLog log)
        {
            _validator = new MotivoProrrogacaoValidator();
            _motivoprorrogacao = motivoprorrogacao;
            _log = log;
        }

        /// <summary>
        /// Retorna todos os motivos de prorrogacaos, possibilitando utilizar filtros OData.
        /// </summary>
        /// <param name="options">Filtros OData</param>
        /// <returns>Lista de MotivoProrrogacaoDto</returns>
        [HttpGet]
        [Route("v1/motivosprorrogacoes")]
        public async Task<GenericResult<IQueryable<MotivoProrrogacaoDto>>> Get(ODataQueryOptions<MotivoProrrogacaoDto> options)
        {
            var result = new GenericResult<IQueryable<MotivoProrrogacaoDto>>();

            try
            {
                var motivo = await _motivoprorrogacao.GetAllAsync();
                int totalReg = 0;
          
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(motivo.AsQueryable(), new ODataQuerySettings()).Cast<MotivoProrrogacaoDto>();
                    totalReg = filtro.Count();
                }

                result.Result = options.ApplyTo(motivo.AsQueryable()).Cast<MotivoProrrogacaoDto>();
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
        /// Busca motivo do prorrogacao pelo código.
        /// </summary>
        /// <param name="id">Código do Motivo do Prorrogacao</param>
        /// <returns>MotivoProrrogacaoDto</returns>
        [HttpGet]
        [Route("v1/consultarmotivoprorrogacao/{id:guid}")]
        public async Task<GenericResult<MotivoProrrogacaoDto>> Get(Guid id)
        {
            var result = new GenericResult<MotivoProrrogacaoDto>();

            try
            {
                result.Result = await _motivoprorrogacao.GetAsync(c => c.ID.Equals(id));
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
        /// Insere um novo motivo do prorrogacao.
        /// </summary>
        /// <param name="value">MotivoProrrogacaoDto</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(MotivoProrrogacaoDto))]
        [Route("v1/inserirmotivoprorrogacao")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "MotivoProrrogacao_Inserir")]
        public GenericResult<MotivoProrrogacaoDto> Post(MotivoProrrogacaoDto value)
        {
            var result = new GenericResult<MotivoProrrogacaoDto>();
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
                    result.Success = _motivoprorrogacao.Insert(value);

                    var descricao = $"Inseriu um Motivo do Prorrogacao com o nome {value.Nome}";
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
        /// Atualiza um Motivo do Prorrogacao
        /// </summary>
        /// <param name="value">MotivoProrrogacaoDto</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(GrupoDto))]
        [Route("v1/atualizarmotivoprorrogacao")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "MotivoProrrogacao_Editar")]
        public async Task<GenericResult<MotivoProrrogacaoDto>> Put(MotivoProrrogacaoDto value)
        {
            var result = new GenericResult<MotivoProrrogacaoDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;

            if (validationResult.IsValid)
            {
                try
                {
                    var user = User.Identity as ClaimsIdentity;
                    var userID = user.Claims.First(c => c.Type.Equals("Usuario")).Value;

                    value.UsuarioIDAlteracao = new Guid(userID);
                    result.Success = await _motivoprorrogacao.Update(value);

                    var descricao = $"Atualizou um Motivo do Prorrogacao com o nome {value.Nome}";
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
        /// Inativa um Motivo de Prorrogacao.
        /// </summary>
        /// <param name="id">Código do Motivo do Prorrogacao</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inativarmotivoprorrogacao/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "MotivoProrrogacao_Excluir")]
        public async Task<GenericResult<MotivoProrrogacaoDto>> Delete(Guid id)
        {
            var result = new GenericResult<MotivoProrrogacaoDto>();
            LogDto logDto = null;

            try
            {
                var grupo = await _motivoprorrogacao.GetAsync(c => c.ID.Equals(id));
                result.Success = await _motivoprorrogacao.Inactive(id);

                var descricao = $"Inativou um Motivo do Prorrogacao com o nome {grupo.Nome}";
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
