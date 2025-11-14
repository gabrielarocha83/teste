using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.WebApi.Extensions;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("percentageofbreak")]
    [Authorize]
    public class PorcentagemQuebraController : ApiController
    {
        private readonly IAppServicePorcentagemQuebra _porcentagemQuebra;
        private readonly PorcentagemQuebraValidator _validator;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="porcentagemquebra"></param>
        /// <param name="log"></param>
        public PorcentagemQuebraController(IAppServicePorcentagemQuebra porcentagemquebra, IAppServiceLog log)
        {
            _validator = new PorcentagemQuebraValidator();
            _porcentagemQuebra = porcentagemquebra;
            _log = log;
        }

        /// <summary>
        /// Lista todos as porcentagens de quebra
        /// </summary>
        /// <param name="options">Lista de porcentagens de quebra</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getpercentageofbreak")]
        public async Task<GenericResult<IQueryable<PorcentagemQuebraDto>>> Get(ODataQueryOptions<PorcentagemQuebraDto> options)
        {

            var result = new GenericResult<IQueryable<PorcentagemQuebraDto>>();
            try
            {
                var porcentagemQuebra = await _porcentagemQuebra.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(porcentagemQuebra.AsQueryable(), new ODataQuerySettings()).Cast<PorcentagemQuebraDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(porcentagemQuebra.AsQueryable()).Cast<PorcentagemQuebraDto>();
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
        /// Busca porcentagens de quebra por Código
        /// </summary>
        /// <param name="id">Código da porcentagens de quebra</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodpercentageofbreak/{id:guid}")]
        public async Task<GenericResult<PorcentagemQuebraDto>> Get(Guid id)
        {
            var result = new GenericResult<PorcentagemQuebraDto>();

            try
            {
                result.Result = await _porcentagemQuebra.GetAsync(g => g.ID.Equals(id));
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
        /// Adiciona um nova porcentagens de quebra
        /// </summary>
        /// <param name="value">porcentagens de quebra</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertpercentageofbreak")]
        [ResponseType(typeof(PorcentagemQuebraDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PorcentagemQuebra_Inserir")]
        public GenericResult<PorcentagemQuebraDto> Post(PorcentagemQuebraDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<PorcentagemQuebraDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    value.ID = Guid.NewGuid();
                    value.UsuarioIDCriacao = new Guid(userid);
                    result.Success = _porcentagemQuebra.Insert(value);

                    var descricao = $"Inseriu uma Porcentagem de quebra de {value.Porcentagem}%";
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
        /// Atualiza uma porcentagens de quebra
        /// </summary>
        /// <param name="value">porcentagens de quebra</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updatepercentageofbreak")]
        [ResponseType(typeof(PorcentagemQuebraDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PorcentagemQuebra_Editar")]
        public async Task<GenericResult<PorcentagemQuebraDto>> Put(PorcentagemQuebraDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<PorcentagemQuebraDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                   
                    value.UsuarioIDAlteracao = new Guid(userid);
                    result.Success = await _porcentagemQuebra.Update(value);
                    var descricao = $"Atualizou uma porcentagem de quebra com {value.Porcentagem}%";
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