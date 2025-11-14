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
    [RoutePrefix("stateculture")]
    [Authorize]
    public class CulturaEstadoController : ApiController
    {
        private readonly IAppServiceCulturaEstado _appServiceCulturaEstado;
        private readonly IAppServiceEstado _appServiceEstado;
        private readonly IAppServiceCultura _appServiceCultura;
        private readonly CulturaEstadoValidator _validator;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServiceCulturaEstado"></param>
        /// <param name="appServiceLog"></param>
        public CulturaEstadoController(IAppServiceCulturaEstado appServiceCulturaEstado, IAppServiceLog appServiceLog, IAppServiceEstado appServiceEstado, IAppServiceCultura appServiceCultura)
        {
            _appServiceEstado = appServiceEstado;
            _appServiceCultura = appServiceCultura;
            _appServiceCulturaEstado = appServiceCulturaEstado;
            _log = appServiceLog;
            _validator = new CulturaEstadoValidator();
        }

        /// <summary>
        /// Método que retorna as configurações de cultura de um estado
        /// </summary>
        /// <param name="options">ODATA</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getculturestate")]
        public async Task<GenericResult<IQueryable<CulturaEstadoDto>>> Get(ODataQueryOptions<CulturaEstadoDto> options)
        {
            var result = new GenericResult<IQueryable<CulturaEstadoDto>>();
            try
            {
                var retCultura = await _appServiceCulturaEstado.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retCultura.AsQueryable(), new ODataQuerySettings()).Cast<CulturaEstadoDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retCultura.AsQueryable()).Cast<CulturaEstadoDto>();
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
        /// Metodo que retorna os dados da cultura de um Estado de acordo com Guid
        /// </summary>
        /// <param name="id">Guid da CulturaEstado</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodculture/{id:guid}")]
        public async Task<GenericResult<CulturaEstadoDto>> Get(Guid id)
        {
            var result = new GenericResult<CulturaEstadoDto>();
            try
            {
                result.Result = await _appServiceCulturaEstado.GetAsync(g => g.ID.Equals(id));
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
        ///  Metodo que retorna os dados da cultura de um Estado de acordo com o Estado e Cultura ID
        /// </summary>
        /// <param name="estado">Código Estado</param>
        /// <param name="cultura">Código Cultura</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodculturestate/{estado:guid}/{cultura:guid}")]
        public async Task<GenericResult<CulturaEstadoDto>> GetEstadoCultura(Guid estado, Guid cultura)
        {
            var result = new GenericResult<CulturaEstadoDto>();
            try
            {
                result.Result = await _appServiceCulturaEstado.GetAsync(g => g.Ativo && g.EstadoID.Equals(estado) && g.CulturaID.Equals(cultura));
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
        /// Metodo para inserir nova cultura para um Estado
        /// </summary>
        /// <param name="culturaDto">CulturaEstadoDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertculture")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "CulturaEstado_Inserir")]
        public async Task<GenericResult<CulturaEstadoDto>> Post(CulturaEstadoDto culturaDto)
        {
            culturaDto.ID = Guid.NewGuid();
            var objuserLogin = User.Identity as ClaimsIdentity;
            var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

            var result = new GenericResult<CulturaEstadoDto>();
            var validationResult = _validator.Validate(culturaDto);

            if (validationResult.IsValid)
            {
                try
                {
                    var cultura = await _appServiceCulturaEstado.GetAsync(g => g.EstadoID.Equals(culturaDto.EstadoID) && g.CulturaID.Equals(culturaDto.CulturaID));

                    if (cultura != null)
                    {
                        result.Success = false;
                        result.Errors = new[] { "Essa Cultura por Estado já está cadastrada." };
                        return result;
                    }
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Errors = new[] { Resources.Resources.Error };
                    var error = new ErrorsYara();
                    error.ErrorYara(e);
                    return result;
                }

                try
                {
                    culturaDto.DataCriacao = DateTime.Now;
                    culturaDto.UsuarioIDCriacao = new Guid(userLogin);

                    result.Success = await _appServiceCulturaEstado.InsertAsync(culturaDto);

                    var cultura = await _appServiceCultura.GetAsync(c => c.ID.Equals(culturaDto.CulturaID));
                    var estado = await _appServiceEstado.GetById(culturaDto.EstadoID);
                    var descricao = $"Inseriu a configuração da cultura{cultura.Descricao} do estado { estado.Nome}";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
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
        /// Metodo para alterar cultura para o Estado
        /// </summary>
        /// <param name="culturaDto">Object CulturaDto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updateculture")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "CulturaEstado_Editar")]
        public async Task<GenericResult<CulturaEstadoDto>> Put(CulturaEstadoDto culturaDto)
        {
            var result = new GenericResult<CulturaEstadoDto>();
            var validationResult = _validator.Validate(culturaDto);

            if (validationResult.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    culturaDto.UsuarioIDAlteracao = new Guid(userLogin);
                    culturaDto.DataAlteracao = DateTime.Now;
                    result.Success = await _appServiceCulturaEstado.Update(culturaDto);

                    var cultura = await _appServiceCultura.GetAsync(c => c.ID.Equals(culturaDto.CulturaID));
                    var estado = await _appServiceEstado.GetById(culturaDto.EstadoID);
                    var descricao = $"Atualizou a configuração da cultura {cultura.Descricao} do estado { estado.Nome}";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, culturaDto.ID);
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
