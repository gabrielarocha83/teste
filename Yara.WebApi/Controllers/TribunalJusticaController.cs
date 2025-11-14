using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("courtoflaw")]
    public class TribunalJusticaController : ApiController
    {

        private readonly IAppServiceTribunalJustica _appServiceTribunalJustica;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServiceTribunalJustica"></param>
        /// <param name="log"></param>
        public TribunalJusticaController(IAppServiceTribunalJustica appServiceTribunalJustica, IAppServiceLog log)
        {
            _appServiceTribunalJustica = appServiceTribunalJustica;
            _log = log;
        }

        /// <summary>
        /// Busca todos os tribunais de justica para serem consumidos pelo robô.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/courtoflaws")]
        public async Task<GenericResult<IQueryable<TribunalJusticaDto>>> Get(ODataQueryOptions<TribunalJusticaDto> options)
        {
            var result = new GenericResult<IQueryable<TribunalJusticaDto>>();

            try
            {
                var tjs = await _appServiceTribunalJustica.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(tjs.AsQueryable(), new ODataQuerySettings()).Cast<TribunalJusticaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(tjs.AsQueryable()).Cast<TribunalJusticaDto>();
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
        /// Busca um tribunal de justiça pelo documento.
        /// </summary>
        /// <param name="contaClienteID">ID da Conta Cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/checkaccount/{contaClienteID:guid}")]
        public async Task<Result> GetAccount(Guid contaClienteID)
        {
            var result = new Result();
            try
            {
                result.Success = await _appServiceTribunalJustica.CheckAsync(contaClienteID);
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
        /// Insere um novo usuário no tribunal de justiça.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertcourtoflaw")]
        [ResponseType(typeof(TribunalJusticaDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TJ_Consulta")]
        public async Task<Result> Post(TribunalJusticaDto value)
        {
            var result = new Result();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                if (await _appServiceTribunalJustica.CheckAsync(value.ContaClienteID))
                {
                    result.Success = false;
                }
                else
                {
                    result.Success = await _appServiceTribunalJustica.InsertAsync(value);

                    LogDto logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, $"Inseriu um novo documento no Tribunal de Justiça: {value.Documento}.", EnumLogLevelDto.Info);
                    _log.Create(logDto);
                }
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
