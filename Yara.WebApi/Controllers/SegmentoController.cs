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
    [RoutePrefix("segments")]
    [Authorize]
    public class SegmentoController : ApiController
    {
        private readonly IAppServiceSegmento _appServiceSegmento;
        private readonly IAppServiceLog _appServiceLog;
        private readonly SegmentoValidator _segmentoValidator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServiceSegmento"></param>
        /// <param name="appServiceLog"></param>
        public SegmentoController(IAppServiceSegmento appServiceSegmento, IAppServiceLog appServiceLog)
        {
            _appServiceSegmento = appServiceSegmento;
            _appServiceLog = appServiceLog;
            _segmentoValidator = new SegmentoValidator();
        }

        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <param name="options">Filtros OData</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/segments")]
        public async Task<GenericResult<IQueryable<SegmentoDto>>> Get(ODataQueryOptions<SegmentoDto> options)
        {
            var result = new GenericResult<IQueryable<SegmentoDto>>();
            try
            {
                var listaSegmento = await _appServiceSegmento.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(listaSegmento.AsQueryable(), new ODataQuerySettings()).Cast<SegmentoDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(listaSegmento.AsQueryable()).Cast<SegmentoDto>();
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
        /// Busca segmento por Código
        /// </summary>
        /// <param name="id">Código do segmento</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getsegmentaccount/{id:guid}")]
        public async Task<GenericResult<SegmentoDto>> GetAccount(Guid id)
        {
            var result = new GenericResult<SegmentoDto>();
            try
            {
                result.Result = await _appServiceSegmento.GetAsync(g => g.ID.Equals(id));
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
        /// Busca segmento por Código
        /// </summary>
        /// <param name="id">Código do segmento</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getsegment/{id:guid}")]
        public async Task<GenericResult<SegmentoDto>> Get(Guid id)
        {
            var result = new GenericResult<SegmentoDto>();
            try
            {
                result.Result = await _appServiceSegmento.GetAsync(g => g.ID.Equals(id));
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
        /// Adiciona um novo usuário
        /// </summary>
        /// <param name="segmentoDto">Segmento</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertsegmento")]
        [ResponseType(typeof(SegmentoDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Segmento_Inserir")]
        public GenericResult<SegmentoDto> PostNovoSegmento(SegmentoDto segmentoDto)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var user = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<SegmentoDto>();
            var segmentoValidation = _segmentoValidator.Validate(segmentoDto);
            LogDto logDto = null;
            if (segmentoValidation.IsValid)
            {
                try
                {
                    segmentoDto.UsuarioIDCriacao = new Guid(user);
                    result.Success = _appServiceSegmento.Insert(segmentoDto);
                    var descricao = $"Inseriu um Segmento do nome {segmentoDto.Descricao}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
                    _appServiceLog.Create(logDto);


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
            {
                result.Errors = segmentoValidation.GetErrors();
            }
            return result;
        }

        /// <summary>
        /// Atualiza um segmento 
        /// </summary>
        /// <param name="segmentoDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updatesegment")]
        [ResponseType(typeof(SegmentoDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Segmento_Editar")]
        public async Task<GenericResult<SegmentoDto>> PutAlteraUsuario(SegmentoDto segmentoDto)
        {
            var result = new GenericResult<SegmentoDto>();
            var segmentoValidation = _segmentoValidator.Validate(segmentoDto);
            LogDto logDto = null;
            if (segmentoValidation.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var user = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;


                    segmentoDto.UsuarioIDAlteracao = new Guid(user);

                    result.Success = await _appServiceSegmento.Update(segmentoDto);
                    var descricao = $"Atualizou um Segmento do nome {segmentoDto.Descricao}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, segmentoDto.ID);
                    _appServiceLog.Create(logDto);
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
            {
                result.Errors = segmentoValidation.GetErrors();
            }
            return result;
        }

        /// <summary>
        ///  Inativa um segmento do sistema
        /// </summary>
        /// <param name="id">Código do segmeneto</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactivesegment/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Segmento_Excluir")]
        public async Task<GenericResult<UsuarioDto>> Delete(Guid id)
        {
            var result = new GenericResult<UsuarioDto>();
            LogDto logDto = null;
            try
            {
                var segmento = await _appServiceSegmento.GetAsync(c => c.ID.Equals(id));
                result.Success = await _appServiceSegmento.Inactive(id);

                var descricao = $"Inativou o Segmento {segmento.Descricao}";
                var level = EnumLogLevelDto.Info;
                logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, id);
                _appServiceLog.Create(logDto);
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
