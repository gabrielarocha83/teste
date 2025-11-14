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
    [RoutePrefix("culture")]
    [Authorize]
    public class CulturaController : ApiController
    {
        private readonly IAppServiceCultura _appServiceCultura;
        private readonly CulturaValidator _validator;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServiceCultura"></param>
        /// <param name="appServiceLog"></param>
        public CulturaController(IAppServiceCultura appServiceCultura, IAppServiceLog appServiceLog)
        {
            _appServiceCultura = appServiceCultura;
            _log = appServiceLog;
            _validator = new CulturaValidator();
        }

        /// <summary>
        /// Metodo que retorna a lista de culturas
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Tipo eq Bovino</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getculture")]
        public async Task<GenericResult<IQueryable<CulturaDto>>> Get(ODataQueryOptions<CulturaDto> options)
        {
            var result = new GenericResult<IQueryable<CulturaDto>>();
            try
            {
                var retCultura = await _appServiceCultura.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retCultura.AsQueryable(), new ODataQuerySettings()).Cast<CulturaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retCultura.AsQueryable()).Cast<CulturaDto>();
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
        /// Metodo que retorna os dados da cultura de acordo com Guid
        /// </summary>
        /// <param name="id">Guid da Cultura</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodculture/{id:guid}")]
        public async Task<GenericResult<CulturaDto>> Get(Guid id)
        {
            var result = new GenericResult<CulturaDto>();
            try
            {
                result.Result = await _appServiceCultura.GetAsync(g => g.ID.Equals(id));
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
        /// Metodo para inserir nova cultura
        /// </summary>
        /// <param name="culturaDto">Object CulturaDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertculture")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Cultura_Inserir")]
        public async Task<GenericResult<CulturaDto>> Post(CulturaDto culturaDto)
        {

            culturaDto.ID = Guid.NewGuid();
            var objuserLogin = User.Identity as ClaimsIdentity;
            var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

            var result = new GenericResult<CulturaDto>();
            var validationResult = _validator.Validate(culturaDto);
            LogDto logDto = null;

            if (validationResult.IsValid)
            {
                try
                {
                    culturaDto.DataCriacao = DateTime.Now;
                    culturaDto.UsuarioIDCriacao = new Guid(userLogin);
                    result.Success = await _appServiceCultura.InsertAsync(culturaDto);
                    var descricao = $"Inseriu a cultura '{culturaDto.Descricao}'";
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
        /// Metodo para alterar cultura
        /// </summary>
        /// <param name="culturaDto">Object CulturaDto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updateculture")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Cultura_Editar")]
        public async Task<GenericResult<CulturaDto>> Put(CulturaDto culturaDto)
        {
            var result = new GenericResult<CulturaDto>();
            var validationResult = _validator.Validate(culturaDto);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    culturaDto.UsuarioIDAlteracao = new Guid(userLogin);
                    culturaDto.DataAlteracao = DateTime.Now;
                    result.Success = await _appServiceCultura.Update(culturaDto);
                    var descricao = $"Atualizou a cultura '{culturaDto.Descricao}'";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, culturaDto.ID);
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
        /// Metodo para inativar uma cultura
        /// </summary>
        /// <param name="id">Guid da cultura</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactivecultura/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Cultura_Excluir")]
        public async Task<GenericResult<CulturaDto>> Delete(Guid id)
        {
            var result = new GenericResult<CulturaDto>();
            LogDto logDto;
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _appServiceCultura.Inactive(id);

                var descricao = $" Usuario {userLogin} desativou a cultura ID: {id}";
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
