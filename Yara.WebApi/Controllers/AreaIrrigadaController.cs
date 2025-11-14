using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.Extensions;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("area")]
    [Authorize]
    public class AreaIrrigadaController : ApiController
    {
        private readonly IAppServiceAreaIrrigada _appServicearea;
        private readonly IAppServiceLog _log;
        private readonly AreaIrrigadaValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServicearea"></param>
        /// <param name="appServiceLog"></param>
        public AreaIrrigadaController(IAppServiceAreaIrrigada appServicearea, IAppServiceLog appServiceLog)
        {
            _appServicearea = appServicearea;
            _log = appServiceLog;
            _validator = new AreaIrrigadaValidator();
        }

        /// <summary>
        /// Metodo que retorna a lista de areas
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Nome eq irrigação</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getarea")]
        public async Task<GenericResult<IQueryable<AreaIrrigadaDto>>> Get(ODataQueryOptions<AreaIrrigadaDto> options)
        {
            var result = new GenericResult<IQueryable<AreaIrrigadaDto>>();
            try
            {
                var retarea = await _appServicearea.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retarea.AsQueryable(), new ODataQuerySettings()).Cast<AreaIrrigadaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retarea.AsQueryable()).Cast<AreaIrrigadaDto>();
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
        /// Metodo que retorna os dados da area de acordo com Guid
        /// </summary>
        /// <param name="id">Guid da area</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodarea/{id:guid}")]
        public async Task<GenericResult<AreaIrrigadaDto>> Get(Guid id)
        {
            var result = new GenericResult<AreaIrrigadaDto>();
            try
            {
                result.Result = await _appServicearea.GetAsync(g => g.ID.Equals(id));
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
        /// Metodo para inserir nova area
        /// </summary>
        /// <param name="areaIrrigadaDto">Object AreaIrrigadaDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertarea")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "AreaIrrigada_Inserir")]
        public async Task<GenericResult<AreaIrrigadaDto>> Post(AreaIrrigadaDto areaIrrigadaDto)
        {

            areaIrrigadaDto.ID = Guid.NewGuid();
            var objuserLogin = User.Identity as ClaimsIdentity;
            var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

            var result = new GenericResult<AreaIrrigadaDto>();
            var validation = _validator.Validate(areaIrrigadaDto);
            LogDto logDto = null;

            if (validation.IsValid)
            {
                try
                {
                    areaIrrigadaDto.DataCriacao = DateTime.Now;
                    areaIrrigadaDto.UsuarioIDCriacao = new Guid(userLogin);
                    result.Success = await _appServicearea.InsertAsync(areaIrrigadaDto);
                    var descricao = $"Inseriu uma area com o tipo {areaIrrigadaDto.Nome}";
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
            {
                result.Errors = validation.GetErrors();
            }
            return result;
        }

        /// <summary>
        /// Metodo para alterar area
        /// </summary>
        /// <param name="areaIrrigadaDto">Object AreaIrrigadaDto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updatearea")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "AreaIrrigada_Editar")]
        public async Task<GenericResult<AreaIrrigadaDto>> Put(AreaIrrigadaDto areaIrrigadaDto)
        {
            var result = new GenericResult<AreaIrrigadaDto>();
            var validation = _validator.Validate(areaIrrigadaDto);
            LogDto logDto = null;
            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    areaIrrigadaDto.UsuarioIDAlteracao = new Guid(userLogin);
                    areaIrrigadaDto.DataAlteracao = DateTime.Now;
                    result.Success = await _appServicearea.Update(areaIrrigadaDto);
                    var descricao = $"Atualizou a area com o tipo {areaIrrigadaDto.Nome}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, areaIrrigadaDto.ID);
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
            {
                result.Errors = validation.GetErrors();
            }
            return result;
        }
    }
}
