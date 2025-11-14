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
    [RoutePrefix("flowlimitsaleorder")]
    [Authorize]
    public class FluxoLiberacaoOrdemVendaController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceFluxoLiberacaoOrdemVenda _credito;
        private readonly FluxoLiberacaoOrdemVendaValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="log"></param>
        /// <param name="credito"></param>
        public FluxoLiberacaoOrdemVendaController(IAppServiceLog log, IAppServiceFluxoLiberacaoOrdemVenda credito)
        {
            _log = log;
            _credito = credito;
            _validator = new FluxoLiberacaoOrdemVendaValidator();
        }

        /// <summary>
        /// Insere Nivel para fluxo de liberação de Ordem de Venda
        /// </summary>
        /// <param name="fluxo">Objeto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insert")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoLiberacaoOrdemVenda_Inserir")]
        public async Task<GenericResult<FluxoLiberacaoOrdemVendaDto>> Post(FluxoLiberacaoOrdemVendaDto fluxo)
        {
            fluxo.ID = Guid.NewGuid();
            fluxo.Ativo = true;
            var objuserLogin = User.Identity as ClaimsIdentity;
            var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var empresa = Request.Properties["Empresa"].ToString();

            

            var result = new GenericResult<FluxoLiberacaoOrdemVendaDto>();
            var validation = _validator.Validate(fluxo);

            if (validation.IsValid)
            {
                try
                {
                    fluxo.UsuarioIDCriacao = new Guid(userLogin);
                    fluxo.EmpresaID = empresa;
                    result.Success = await _credito.InsertAsync(fluxo);
                    var descricao =
                        $"Inseriu fluxo de liberação de ordem de venda novo com valor inicial de: {fluxo.ValorDe} até o valor final de: {fluxo.ValorAte} para o perfil {fluxo.PerfilID}.";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
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
        /// Lista Fluxo de liberação de ordem de venda trazendo o nome do perfil
        /// </summary>
        /// <param name="options">OData</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/all")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoLiberacaoOrdemVenda_Visualizar")]
        public async Task<GenericResult<IQueryable<FluxoLiberacaoOrdemVendaDto>>> GetLimit(ODataQueryOptions<FluxoLiberacaoOrdemVendaDto> options)
        {
            var result = new GenericResult<IQueryable<FluxoLiberacaoOrdemVendaDto>>();
            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                var ret = await _credito.GetAllFilterAsync(c=>c.EmpresaID==empresa);
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(ret.AsQueryable(), new ODataQuerySettings()).Cast<FluxoLiberacaoOrdemVendaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(ret.AsQueryable()).Cast<FluxoLiberacaoOrdemVendaDto>();
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
        /// Busca fluxo de liberação de ordem de venda por Guid
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcod/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoLiberacaoOrdemVenda_Editar")]
        public async Task<GenericResult<FluxoLiberacaoOrdemVendaDto>> Get(Guid id)
        {
            var result = new GenericResult<FluxoLiberacaoOrdemVendaDto>();
            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                result.Result = await _credito.GetAsync(g => g.ID.Equals(id) && g.EmpresaID.Equals(empresa));
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
        /// Atuaizar fluxo de liberação de ordem de venda
        /// </summary>
        /// <param name="fluxo">Objeto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/update")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoLiberacaoOrdemVenda_Inserir")]
        public async Task<GenericResult<FluxoLiberacaoOrdemVendaDto>> Put(FluxoLiberacaoOrdemVendaDto fluxo)
        {
            var result = new GenericResult<FluxoLiberacaoOrdemVendaDto>();
            var validation = _validator.Validate(fluxo);
            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();
                    fluxo.UsuarioIDAlteracao = new Guid(userLogin);
                    fluxo.DataAlteracao = DateTime.Now;
                    fluxo.EmpresaID = empresa;
                    result.Success = await _credito.Update(fluxo);
                    var descricao = $"Atualizou Fluxo de ordem de venda do nivel: {fluxo.Nivel} do Id: {fluxo.ID}";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, fluxo.ID);
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
        /// Metodo para inativar fluxo de liberação de ordem de venda
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactive/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "FluxoLiberacaoOrdemVenda_Excluir")]
        public async Task<GenericResult<FluxoLiberacaoOrdemVendaDto>> Delete(Guid id)
        {
            var result = new GenericResult<FluxoLiberacaoOrdemVendaDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _credito.Inactive(id, new Guid(userLogin));

                var descricao = $" Usuario {userLogin} desativou o fluxo de ordem de venda: {id}";
                var level = EnumLogLevelDto.Info;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, id);
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
