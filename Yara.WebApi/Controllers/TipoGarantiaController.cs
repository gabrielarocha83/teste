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
    [RoutePrefix("typeofguarantee")]
    [Authorize]
    public class TipoGarantiaController : ApiController
    {
        private readonly IAppServiceTipoGarantia _tipoGarantia;
        private readonly TipoGarantiaValidator _validator;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="tipoGarantia"></param>
        /// <param name="log"></param>
        public TipoGarantiaController(IAppServiceTipoGarantia tipoGarantia, IAppServiceLog log)
        {
            _validator = new TipoGarantiaValidator();
            _tipoGarantia = tipoGarantia;
            _log = log;
        }

        /// <summary>
        /// Lista todos os tipos de garantia
        /// </summary>
        /// <param name="options">Lista de Tipos de Garantia</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/gettypeofguarantees")]
        public async Task<GenericResult<IQueryable<TipoGarantiaDto>>> Get(ODataQueryOptions<TipoGarantiaDto> options)
        {

            var result = new GenericResult<IQueryable<TipoGarantiaDto>>();
            try
            {
                var tipogarantia = await _tipoGarantia.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(tipogarantia.AsQueryable(), new ODataQuerySettings()).Cast<TipoGarantiaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(tipogarantia.AsQueryable()).Cast<TipoGarantiaDto>();
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
        /// Busca tipo de garantia por Código
        /// </summary>
        /// <param name="id">Código do tipo de garantia</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodtypeofguarantee/{id:guid}")]
        public async Task<GenericResult<TipoGarantiaDto>> Get(Guid id)
        {
            var result = new GenericResult<TipoGarantiaDto>();

            try
            {
                result.Result = await _tipoGarantia.GetAsync(g => g.ID.Equals(id));
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
        /// Adiciona um novo tipo de garantia
        /// </summary>
        /// <param name="value">Tipo de garantia</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/inserttypeofguarantee")]
        [ResponseType(typeof(TipoGarantiaDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoGarantia_Inserir")]
        public GenericResult<TipoGarantiaDto> Post(TipoGarantiaDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<TipoGarantiaDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    value.UsuarioIDCriacao = new Guid(userid);
                    result.Success = _tipoGarantia.Insert(value);

                    var descricao = $"Inseriu um novo Tipo de Garantia: {value.Nome}";
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
        /// Atualiza um tipo de garantia 
        /// </summary>
        /// <param name="value">Tipo de cliente</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(TipoGarantiaDto))]
        [Route("v1/updatetypeofguarantee")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoGarantia_Editar")]
        public async Task<GenericResult<TipoGarantiaDto>> Put(TipoGarantiaDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<TipoGarantiaDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {

                    value.UsuarioIDAlteracao = new Guid(userid);
                    result.Success = await _tipoGarantia.Update(value);
                    var descricao = $"Atualizou um Tipo de Garantia do nome {value.Nome}";
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
