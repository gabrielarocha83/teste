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
    [RoutePrefix("flowanaliserelease")]
    [Authorize]
    public class FluxoAlcadaAnaliseController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceFluxoAlcadaAnalise _analise;
        private readonly FluxoAlcadaAnaliseValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="log"></param>
        /// <param name="analise"></param>
        public FluxoAlcadaAnaliseController(IAppServiceLog log, IAppServiceFluxoAlcadaAnalise analise)
        {
            _log = log;
            _analise = analise;
            _validator = new FluxoAlcadaAnaliseValidator();
        }

        /// <summary>
        /// Insere nivel para fluxo de alçada de aprovação
        /// </summary>
        /// <param name="fluxo">Objeto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertlimit")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoAlcadaAnalise_Inserir")]
        public async Task<GenericResult<FluxoAlcadaAnaliseDto>> Post(FluxoAlcadaAnaliseDto fluxo)
        {
            var result = new GenericResult<FluxoAlcadaAnaliseDto>();

            var validation = _validator.Validate(fluxo); // _validator.Validate(fluxo);
            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();

                    fluxo.ID = Guid.NewGuid();
                    fluxo.UsuarioIDCriacao = new Guid(userLogin);
                    fluxo.EmpresaID = empresa;
                    fluxo.Ativo = true;

                    result.Success = await _analise.InsertAsync(fluxo);

                    var descricao = $"Inseriu fluxo de alçada de análise novo com valor inicial de: {fluxo.ValorDe} até o valor final de: {fluxo.ValorAte} para o perfil {fluxo.PerfilID}.";
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
        /// Lista fluxo de alçada de aprovação trazendo o nome do perfil
        /// </summary>
        /// <param name="options">OData</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getlimit")]
        public async Task<GenericResult<IQueryable<FluxoAlcadaAnaliseDto>>> GetLimit(ODataQueryOptions<FluxoAlcadaAnaliseDto> options)
        {
            var empresa = Request.Properties["Empresa"].ToString();

            var result = new GenericResult<IQueryable<FluxoAlcadaAnaliseDto>>();

            try
            {
                var ret = await _analise.GetAllFilterAsync(c => c.EmpresaID.Equals(empresa));

                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(ret.AsQueryable(), new ODataQuerySettings()).Cast<FluxoAlcadaAnaliseDto>();
                    totalReg = filtro.Count();
                }

                result.Result = options.ApplyTo(ret.AsQueryable()).Cast<FluxoAlcadaAnaliseDto>();
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
        /// Busca fluxo de alçada de aprovação por ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodlimit/{id:guid}")]
        public async Task<GenericResult<FluxoAlcadaAnaliseDto>> Get(Guid id)
        {
            var result = new GenericResult<FluxoAlcadaAnaliseDto>();

            try
            {
                result.Result = await _analise.GetAsync(g => g.ID.Equals(id));
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
        /// Atuaizar fluxo de alçada de aprovação
        /// </summary>
        /// <param name="fluxo">Objeto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updatelimit")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoAlcadaAnalise_Inserir")]
        public async Task<GenericResult<FluxoAlcadaAnaliseDto>> Put(FluxoAlcadaAnaliseDto fluxo)
        {
            var result = new GenericResult<FluxoAlcadaAnaliseDto>();

            var validation = _validator.Validate(fluxo); // _validator.Validate(fluxo);
            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();

                    fluxo.EmpresaID = empresa;
                    fluxo.UsuarioIDAlteracao = new Guid(userLogin);
                    fluxo.DataAlteracao = DateTime.Now;

                    result.Success = await _analise.Update(fluxo);

                    var descricao = $"Atualizou fluxo de alçada de análise do nivel: {fluxo.Nivel} do Id: {fluxo.ID}";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, fluxo.ID);

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
        /// Metodo para inativar fluxo de alçada de aprovação
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactivelimit/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoAlcadaAnalise_Excluir")]
        public async Task<GenericResult<FluxoAlcadaAnaliseDto>> Delete(Guid id)
        {
            var result = new GenericResult<FluxoAlcadaAnaliseDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _analise.Inactive(id, new Guid(userLogin));

                var descricao = $" Usuario {userLogin} desativou o fluxo de alçada de análise: {id}";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, id);

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
