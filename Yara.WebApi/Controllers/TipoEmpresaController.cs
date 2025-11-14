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
    [RoutePrefix("company")]
    [Authorize]
    public class TipoEmpresaController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceTipoEmpresa _serviceTipoEmpresa;
        private readonly TipoEmpresaValidator _validator;

        /// <summary>
        /// Construtor da classe EmpresaController
        /// </summary>
        /// <param name="serviceTipoEmpresa"></param>
        /// <param name="appServiceLog"></param>
        public TipoEmpresaController(IAppServiceTipoEmpresa serviceTipoEmpresa, IAppServiceLog appServiceLog)
        {
            _log = appServiceLog;
            _serviceTipoEmpresa = serviceTipoEmpresa;
            _validator = new TipoEmpresaValidator();
        }

        /// <summary>
        /// Metodo que retorna a lista de tipos de empresas
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Tipo eq Sementes</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcompany")]
        public async Task<GenericResult<IQueryable<TipoEmpresaDto>>> Get(ODataQueryOptions<TipoEmpresaDto> options)
        {
            var result = new GenericResult<IQueryable<TipoEmpresaDto>>();
            try
            {
                var rettipo = await _serviceTipoEmpresa.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(rettipo.AsQueryable(), new ODataQuerySettings()).Cast<TipoEmpresaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(rettipo.AsQueryable()).Cast<TipoEmpresaDto>();
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
        /// Metodo que retorna os dados do tipo empresa de acordo com Guid
        /// </summary>
        /// <param name="id">Guid do tipo empresa</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodcompany/{id:guid}")]
        public async Task<GenericResult<TipoEmpresaDto>> Get(Guid id)
        {
            var result = new GenericResult<TipoEmpresaDto>();
            try
            {
                result.Result = await _serviceTipoEmpresa.GetAsync(g => g.ID.Equals(id));
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
        /// Metodo para inserir novo tipo de empresa
        /// </summary>
        /// <param name="tipoEmpresaDto">Object TipoEmpresaDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertculture")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoEmpresa_Inserir")]
        public async Task<GenericResult<TipoEmpresaDto>> Post(TipoEmpresaDto tipoEmpresaDto)
        {

            tipoEmpresaDto.ID = Guid.NewGuid();
            var objuserLogin = User.Identity as ClaimsIdentity;
            var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

            var result = new GenericResult<TipoEmpresaDto>();
            var validation = _validator.Validate(tipoEmpresaDto);
            LogDto logDto = null;

            if (validation.IsValid)
            {
                try
                {
                    tipoEmpresaDto.DataCriacao = DateTime.Now;
                    tipoEmpresaDto.UsuarioIDCriacao = new Guid(userLogin);
                    result.Success = await _serviceTipoEmpresa.InsertAsync(tipoEmpresaDto);
                    var descricao = $"Inseriu uma tipo com o tipo {tipoEmpresaDto.Tipo}";
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
        /// Metodo para alterar tipo de empresa
        /// </summary>
        /// <param name="tipoEmpresaDto">Object TipoEmpresaDto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updateculture")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoEmpresa_Editar")]
        public async Task<GenericResult<TipoEmpresaDto>> Put(TipoEmpresaDto tipoEmpresaDto)
        {
            var result = new GenericResult<TipoEmpresaDto>();
            var validation = _validator.Validate(tipoEmpresaDto);
            LogDto logDto = null;
            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    tipoEmpresaDto.UsuarioIDAlteracao = new Guid(userLogin);
                    tipoEmpresaDto.DataAlteracao = DateTime.Now;
                    result.Success = await _serviceTipoEmpresa.Update(tipoEmpresaDto);
                    var descricao = $"Atualizou tipo de empresa para o tipo {tipoEmpresaDto.Tipo}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, tipoEmpresaDto.ID);
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
        /// Metodo para inativar um tipo empresa
        /// </summary>
        /// <param name="id">Guid da tipo</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactivetipo/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoEmpresa_Excluir")]
        public async Task<GenericResult<TipoEmpresaDto>> Delete(Guid id)
        {
            var result = new GenericResult<TipoEmpresaDto>();
            LogDto logDto;
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _serviceTipoEmpresa.Inactive(id);

                var descricao = $" Usuario {userLogin} desativou o tipo empresa ID: {id}";
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
