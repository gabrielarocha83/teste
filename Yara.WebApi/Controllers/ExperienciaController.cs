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
    [RoutePrefix("experiences")]
    [Authorize]
    public class ExperienciaController : ApiController
    {
        private readonly IAppServiceExperiencia _appServiceExperiencia;
        private readonly ExperienciaValidator _validator;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServiceExperiencia"></param>
        /// <param name="appServiceLog"></param>
        public ExperienciaController(IAppServiceExperiencia appServiceExperiencia, IAppServiceLog appServiceLog)
        {
            _appServiceExperiencia = appServiceExperiencia;
            _log = appServiceLog;
            _validator = new ExperienciaValidator();
        }

        /// <summary>
        /// Lista todas as experiencias cadastradas
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Descricao eq Sem experiencia</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getexperience")]
        public async Task<GenericResult<IQueryable<ExperienciaDto>>> Get(ODataQueryOptions<ExperienciaDto> options)
        {
            var result = new GenericResult<IQueryable<ExperienciaDto>>();
            try
            {
                var list = await _appServiceExperiencia.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(list.AsQueryable(), new ODataQuerySettings()).Cast<ExperienciaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(list.AsQueryable()).Cast<ExperienciaDto>();
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
        /// Metodo que retorna Entidade Experiencia de acordo com Guid
        /// </summary>
        /// <param name="id">Guid da Experiencia</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getexperience/{id:guid}")]
        public async Task<GenericResult<ExperienciaDto>> Get(Guid id)
        {
            var result = new GenericResult<ExperienciaDto>();
            try
            {
                result.Result = await _appServiceExperiencia.GetAsync(c => c.ID.Equals(id));
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
        /// Metodo que insere os dados de Experiencia
        /// </summary>
        /// <param name="experienciaDto">Objeto ExperienciaDto</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ExperienciaDto))]
        [Route("v1/insertexperience")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Experiencia_Inserir")]
        public async Task<GenericResult<ExperienciaDto>> Post(ExperienciaDto experienciaDto)
        {
            var user = User.Identity as ClaimsIdentity;
            var userId = user.Claims.First(c => c.Type.Equals("Usuario")).Value;

            var result = new GenericResult<ExperienciaDto>();

            var validationResult = _validator.Validate(experienciaDto);
            LogDto logDto = null;

            if (validationResult.IsValid)
            {
                try
                {
                    experienciaDto.ID = Guid.NewGuid();
                    experienciaDto.UsuarioIDCriacao = new Guid(userId);
                    result.Success = await _appServiceExperiencia.InsertAsync(experienciaDto);
                    var descricao = $"Inseriu uma nova Experiencia com a descrição {experienciaDto.Descricao}";
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
                result.Errors = validationResult.GetErrors();
            }
          
            return result;
        }

        /// <summary>
        /// Metodo que altera os dados de Experiencia
        /// </summary>
        /// <param name="experienciaDto">Objeto ExperienciaDto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updateexperience")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Experiencia_Editar")]
        public async Task<GenericResult<ExperienciaDto>> Put(ExperienciaDto experienciaDto)
        {
            var result = new GenericResult<ExperienciaDto>();
            var experienceValidation = _validator.Validate(experienciaDto);
            LogDto logDto = null;
            if (experienceValidation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    experienciaDto.UsuarioIDAlteracao = new Guid(userLogin);
                    experienciaDto.DataAlteracao = DateTime.Now;
                    result.Success = await _appServiceExperiencia.Update(experienciaDto);
                    var descricao = $"Atualizou o registro de Experiencia com a descrição {experienciaDto.Descricao} ";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, experienciaDto.ID);
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
                result.Errors = experienceValidation.GetErrors();
            }
           
            return result;
        }

        /// <summary>
        /// Metodo que inativa os dados de Experiencia
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactiveexperience/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Experiencia_Excluir")]
        public async Task<GenericResult<ExperienciaDto>> Delete(Guid id)
        {
            var result = new GenericResult<ExperienciaDto>();
            LogDto logDto;
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                result.Success = await _appServiceExperiencia.Inactive(id);

                var descricao = $"Usuario responsavel por inativar a Experiencia: {userLogin}";
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
