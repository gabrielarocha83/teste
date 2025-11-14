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
    [RoutePrefix("flowaprovacaorelease")]
    [Authorize]
    public class FluxoAlcadaAprovacaoController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceFluxoAlcadaAprovacao _aprovacao;
        private readonly FluxoAlcadaAprovacaoValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="log"></param>
        /// <param name="aprovacao"></param>
        public FluxoAlcadaAprovacaoController(IAppServiceLog log, IAppServiceFluxoAlcadaAprovacao aprovacao)
        {
            _log = log;
            _aprovacao = aprovacao;
            _validator = new FluxoAlcadaAprovacaoValidator();
        }

        /// <summary>
        /// Insere nivel para fluxo de alçada de aprovação
        /// </summary>
        /// <param name="fluxo">Objeto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertlimit")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoAlcadaAprovacao_Inserir")]
        public async Task<GenericResult<FluxoAlcadaAprovacaoDto>> Post(FluxoAlcadaAprovacaoDto fluxo)
        {
            var result = new GenericResult<FluxoAlcadaAprovacaoDto>();

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

                    result.Success = await _aprovacao.InsertAsync(fluxo);

                    var descricao = $"Inseriu fluxo de alçada de aprovação novo com valor inicial de: {fluxo.ValorDe} até o valor final de: {fluxo.ValorAte} para o perfil {fluxo.PerfilID}.";
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
        public async Task<GenericResult<IQueryable<FluxoAlcadaAprovacaoDto>>> GetLimit(ODataQueryOptions<FluxoAlcadaAprovacaoDto> options)
        {
            var empresa = Request.Properties["Empresa"].ToString();

            var result = new GenericResult<IQueryable<FluxoAlcadaAprovacaoDto>>();

            try
            {
                var ret = await _aprovacao.GetAllFilterAsync(c => c.EmpresaID.Equals(empresa));

                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(ret.AsQueryable(), new ODataQuerySettings()).Cast<FluxoAlcadaAprovacaoDto>();
                    totalReg = filtro.Count();
                }

                result.Result = options.ApplyTo(ret.AsQueryable()).Cast<FluxoAlcadaAprovacaoDto>();
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
        public async Task<GenericResult<FluxoAlcadaAprovacaoDto>> Get(Guid id)
        {
            var result = new GenericResult<FluxoAlcadaAprovacaoDto>();

            try
            {
                result.Result = await _aprovacao.GetAsync(g => g.ID.Equals(id));
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
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoAlcadaAprovacao_Inserir")]
        public async Task<GenericResult<FluxoAlcadaAprovacaoDto>> Put(FluxoAlcadaAprovacaoDto fluxo)
        {
            var result = new GenericResult<FluxoAlcadaAprovacaoDto>();

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

                    result.Success = await _aprovacao.Update(fluxo);
                    
                    var descricao = $"Atualizou fluxo de alçada de aprovação do nivel: {fluxo.Nivel} do Id: {fluxo.ID}";
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
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoAlcadaAprovacao_Excluir")]
        public async Task<GenericResult<FluxoAlcadaAprovacaoDto>> Delete(Guid id)
        {
            var result = new GenericResult<FluxoAlcadaAprovacaoDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _aprovacao.Inactive(id, new Guid(userLogin));

                var descricao = $" Usuario {userLogin} desativou o fluxo de alçada de aprovação: {id}";
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
