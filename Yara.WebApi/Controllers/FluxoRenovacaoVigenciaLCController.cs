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
    [RoutePrefix("flowlimitrenewal")]
    [Authorize]
    public class FluxoRenovacaoVigenciaLCController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceFluxoRenovacaoVigenciaLC _fluxoRenovacaoVigenciaLC;
        private readonly FluxoRenovacaoVigenciaLCValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="fluxoRenovacaoVigenciaLC"></param>
        /// <param name="log"></param>
        public FluxoRenovacaoVigenciaLCController(IAppServiceFluxoRenovacaoVigenciaLC fluxoRenovacaoVigenciaLC, IAppServiceLog log)
        {
            _fluxoRenovacaoVigenciaLC = fluxoRenovacaoVigenciaLC;
            _log = log;
            _validator = new FluxoRenovacaoVigenciaLCValidator();
        }

        /// <summary>
        /// Lista os Fluxos de Renovação de Vigência de LC trazendo o nome do aprovador.
        /// </summary>
        /// <param name="options">OData</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getlist")]
        public async Task<GenericResult<IQueryable<FluxoRenovacaoVigenciaLCDto>>> GetApprover(ODataQueryOptions<FluxoRenovacaoVigenciaLCDto> options)
        {
            var result = new GenericResult<IQueryable<FluxoRenovacaoVigenciaLCDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                var retarea = await _fluxoRenovacaoVigenciaLC.GetAllFilterAsync(c => c.EmpresaID.Equals(empresa));

                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retarea.AsQueryable(), new ODataQuerySettings()).Cast<FluxoRenovacaoVigenciaLCDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retarea.AsQueryable()).Cast<FluxoRenovacaoVigenciaLCDto>();
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
        /// Busca um Fluxo de Renovação de Vigência de LC pelo ID.
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcod/{id:guid}")]
        public async Task<GenericResult<FluxoRenovacaoVigenciaLCDto>> Get(Guid id)
        {
            var result = new GenericResult<FluxoRenovacaoVigenciaLCDto>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                result.Result = await _fluxoRenovacaoVigenciaLC.GetAsync(f => f.ID.Equals(id) && f.EmpresaID.Equals(empresa));
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
        /// Insere um nível no Fluxo de Renovação de Vigência de LC.
        /// </summary>
        /// <param name="fluxoRenovacaoVigenciaLCDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insert")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoRenovacaoVigenciaLC_Inserir")]
        public async Task<GenericResult<FluxoRenovacaoVigenciaLCDto>> Post(FluxoRenovacaoVigenciaLCDto fluxoRenovacaoVigenciaLCDto)
        {
            var result = new GenericResult<FluxoRenovacaoVigenciaLCDto>();
            var validation = _validator.Validate(fluxoRenovacaoVigenciaLCDto);

            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var empresa = Request.Properties["Empresa"].ToString();
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    fluxoRenovacaoVigenciaLCDto.ID = Guid.NewGuid();
                    fluxoRenovacaoVigenciaLCDto.DataCriacao = DateTime.Now;
                    fluxoRenovacaoVigenciaLCDto.UsuarioIDCriacao = new Guid(userLogin);
                    //fluxoRenovacaoVigenciaLCDto.Nivel = Int32.MinValue; // Vem preenchido do front.
                    //fluxoRenovacaoVigenciaLCDto.Ativo = true; // Vem preenchido do front.
                    fluxoRenovacaoVigenciaLCDto.EmpresaID = empresa;
                    // fluxoRenovacaoVigenciaLCDto.UsuarioId = Guid.Empty; // Vem preenchido do front.

                    result.Success = await _fluxoRenovacaoVigenciaLC.InsertAsync(fluxoRenovacaoVigenciaLCDto);

                    var descricao = $"O usuário {userLogin}, inseriu o usuário {fluxoRenovacaoVigenciaLCDto.UsuarioId} no nível {fluxoRenovacaoVigenciaLCDto.Nivel} como aprovador de fluxo de renovação de vigência de LC.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
                    _log.Create(logDto);
                }
                catch (ArgumentException ex)
                {
                    result.Success = false;
                    result.Errors = new[] { ex.Message };
                    var error = new ErrorsYara();
                    error.ErrorYara(ex);
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
        /// Inativa um nível no Fluxo de Liberação de Grupos Econômicos.
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactive/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoRenovacaoVigenciaLC_Excluir")]
        public async Task<GenericResult<FluxoRenovacaoVigenciaLCDto>> Delete(Guid id)
        {
            var result = new GenericResult<FluxoRenovacaoVigenciaLCDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                var obj = new FluxoRenovacaoVigenciaLCDto() {
                    ID = id,
                    UsuarioIDAlteracao = new Guid(userLogin),
                    DataAlteracao = DateTime.Now
                };

                result.Success = await _fluxoRenovacaoVigenciaLC.RemoveAsync(obj);

                var descricao = $"O usuário {userLogin}, desativou o fluxo de renovação de vigência de LC {id}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, id);
                _log.Create(logDto);
            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { ex.Message };
                var error = new ErrorsYara();
                error.ErrorYara(ex);
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
