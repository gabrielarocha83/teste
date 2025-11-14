using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    [RoutePrefix("holiday")]
    public class FeriadoController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceFeriado _feriado;
        private readonly FeriadoValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="feriado"></param>
        /// <param name="log"></param>
        public FeriadoController(IAppServiceFeriado feriado, IAppServiceLog log)
        {
            _log = log;
            _feriado = feriado;
            _validator = new FeriadoValidator();
        }
        
        /// <summary>
        /// Lista os feriados
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getholiday")]
        public async Task<GenericResult<IQueryable<FeriadoDto>>> Get(ODataQueryOptions<FeriadoDto> options)
        {

            var result = new GenericResult<IQueryable<FeriadoDto>>();
            try
            {
                var feriado = await _feriado.GetAllAsync();
                result.Result = options.ApplyTo(feriado.AsQueryable()).Cast<FeriadoDto>();
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
        /// Busca data feriado pelo Guid
        /// </summary>
        /// <param name="id">Código do tipo de cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodholiday/{id:guid}")]
        public async Task<GenericResult<FeriadoDto>> Get(Guid id)
        {
            var result = new GenericResult<FeriadoDto>();

            try
            {
                result.Result = await _feriado.GetAsync(g => g.ID.Equals(id));
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
        /// Adiciona uma data para feriado ou ativa uma data inativa.
        /// </summary>
        /// <param name="value">Tipo de cliente</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertholiday")]
        [ResponseType(typeof(FeriadoDto))]
        public async Task<GenericResult<FeriadoDto>> Post(FeriadoDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<FeriadoDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    value.UsuarioIDCriacao = new Guid(userid);

                    var b = await _feriado.InsertAsync(value);
                    result.Success = b;

                    var descricao = $"Inseriu um novo feriado ou ativo um existente com a descrição: {value.Descricao}";
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
        /// Atualiza uma data para feriado
        /// </summary>
        /// <param name="value">Tipo de cliente</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(FeriadoDto))]
        [Route("v1/updateholiday")]
        public async Task<GenericResult<FeriadoDto>> Put(FeriadoDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<FeriadoDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {

                    value.UsuarioIDAlteracao = new Guid(userid);
                    result.Success = await _feriado.Update(value);
                    var descricao = $"Atualizou o feriado {value.Descricao}";
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
        /// Inativa uma data cadastrada
        /// </summary>
        /// <param name="id">Código do tipo de cliente</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactiveholiday/{id:guid}")]
        public async Task<GenericResult<FeriadoDto>> Delete(Guid id)
        {
            var result = new GenericResult<FeriadoDto>();
            LogDto logDto;
            try
            {
                var feriado = await _feriado.GetAsync(c => c.ID.Equals(id));
                result.Success = await _feriado.Inactive(id);

                var descricao = $"Inativou o feriado: {feriado.Descricao}";
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
