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
    [RoutePrefix("averageageofsugarcane")]
    [Authorize]
    public class IdadeMediaCanavialController : ApiController
    {
        private readonly IAppServiceIdadeMediaCanavial _idadeMediaCanavial;
        private readonly IdadeMediaCanavialValidator _validator;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="idadeMediaCanavial"></param>
        /// <param name="log"></param>
        public IdadeMediaCanavialController(IAppServiceIdadeMediaCanavial idadeMediaCanavial, IAppServiceLog log)
        {
            _validator = new IdadeMediaCanavialValidator();
            _idadeMediaCanavial = idadeMediaCanavial;
            _log = log;
        }

        /// <summary>
        /// Lista todos as Idades Médias de Canavial
        /// </summary>
        /// <param name="options">Lista Idade Média de Canavial</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getaverageageofsugarcane")]
        public async Task<GenericResult<IQueryable<IdadeMediaCanavialDto>>> Get(ODataQueryOptions<IdadeMediaCanavialDto> options)
        {
            var result = new GenericResult<IQueryable<IdadeMediaCanavialDto>>();

            try
            {
                var idadeMediaCanavial = await _idadeMediaCanavial.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(idadeMediaCanavial.AsQueryable(), new ODataQuerySettings()).Cast<IdadeMediaCanavialDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(idadeMediaCanavial.AsQueryable()).Cast<IdadeMediaCanavialDto>();
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
        /// Busca Idade Média Canavial por Código
        /// </summary>
        /// <param name="id">Código da Idade Média do Canavial</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodaverageageofsugarcane/{id:guid}")]
        public async Task<GenericResult<IdadeMediaCanavialDto>> Get(Guid id)
        {
            var result = new GenericResult<IdadeMediaCanavialDto>();

            try
            {
                result.Result = await _idadeMediaCanavial.GetAsync(g => g.ID.Equals(id));
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
        /// Adiciona um nova Idade Média de Canavial
        /// </summary>
        /// <param name="value">Idade Média de Canavial</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertaverageageofsugarcane")]
        [ResponseType(typeof(IdadeMediaCanavialDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "IdadeMediaCanavial_Inserir")]
        public GenericResult<IdadeMediaCanavialDto> Post(IdadeMediaCanavialDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<IdadeMediaCanavialDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    value.UsuarioIDCriacao = new Guid(userid);
                    result.Success = _idadeMediaCanavial.Insert(value);

                    var descricao = $"Inseriu uma nova Idade Média de Canavial: {value.Nome}";
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
        /// Atualiza uma idade média de canavial 
        /// </summary>
        /// <param name="value">Idade média de canavial</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updateaverageageofsugarcane")]
        [ResponseType(typeof(IdadeMediaCanavialDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "IdadeMediaCanavial_Editar")]
        public async Task<GenericResult<IdadeMediaCanavialDto>> Put(IdadeMediaCanavialDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<IdadeMediaCanavialDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;

            if (validationResult.IsValid)
            {
                try
                {
                    value.UsuarioIDAlteracao = new Guid(userid);
                    result.Success = await _idadeMediaCanavial.Update(value);

                    var descricao = $"Atualizou uma Idade Média de Canavial do nome {value.Nome}";
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
