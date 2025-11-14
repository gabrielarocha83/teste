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
    [RoutePrefix("product")]
    [Authorize]
    public class ProdutoServicoController : ApiController
    {
        private readonly IAppServiceProdutoServico _appServiceservices;
        private readonly IAppServiceLog _log;
        private readonly ProdutoServicoValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServiceservices"></param>
        /// <param name="appServiceLog"></param>
        public ProdutoServicoController(IAppServiceProdutoServico appServiceservices, IAppServiceLog appServiceLog)
        {
            _appServiceservices = appServiceservices;
            _log = appServiceLog;
            _validator = new ProdutoServicoValidator();
        }

        /// <summary>
        /// Metodo que retorna a lista de Produtos e Serviços
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Tipo eq Bovino</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getservices")]
        public async Task<GenericResult<IQueryable<ProdutoServicoDto>>> Get(ODataQueryOptions<ProdutoServicoDto> options)
        {
            var result = new GenericResult<IQueryable<ProdutoServicoDto>>();
            try
            {
                var retservices = await _appServiceservices.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retservices.AsQueryable(), new ODataQuerySettings()).Cast<ProdutoServicoDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retservices.AsQueryable()).Cast<ProdutoServicoDto>();
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
        /// Metodo que retorna os dados de Produtos e Serviços de acordo com Guid
        /// </summary>
        /// <param name="id">Guid da services</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodservices/{id:guid}")]
        public async Task<GenericResult<ProdutoServicoDto>> Get(Guid id)
        {
            var result = new GenericResult<ProdutoServicoDto>();
            try
            {
                result.Result = await _appServiceservices.GetAsync(g => g.ID.Equals(id));
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
        /// Metodo para inserir novo Produto ou Serviço
        /// </summary>
        /// <param name="produtoServicoDto">Object ProdutoServicoDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertservices")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "OpcoesProdutoServico_Inserir")]
        public async Task<GenericResult<ProdutoServicoDto>> Post(ProdutoServicoDto produtoServicoDto)
        {

            produtoServicoDto.ID = Guid.NewGuid();
            var objuserLogin = User.Identity as ClaimsIdentity;
            var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

            var result = new GenericResult<ProdutoServicoDto>();
            var validation = _validator.Validate(produtoServicoDto);
            LogDto logDto = null;

            if (validation.IsValid)
            {
                try
                {
                    produtoServicoDto.DataCriacao = DateTime.Now;
                    produtoServicoDto.UsuarioIDCriacao = new Guid(userLogin);
                    result.Success = await _appServiceservices.InsertAsync(produtoServicoDto);
                    var descricao = $"Inseriu um produto ou serviço com o nome {produtoServicoDto.Nome}";
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
        /// Metodo para alterar Produtos e Serviços
        /// </summary>
        /// <param name="produtoServicoDto">Object ProdutoServicoDto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updateservices")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "OpcoesProdutoServico_Editar")]
        public async Task<GenericResult<ProdutoServicoDto>> Put(ProdutoServicoDto produtoServicoDto)
        {
            var result = new GenericResult<ProdutoServicoDto>();
            var validation = _validator.Validate(produtoServicoDto);
            LogDto logDto = null;
            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    produtoServicoDto.UsuarioIDAlteracao = new Guid(userLogin);
                    produtoServicoDto.DataAlteracao = DateTime.Now;
                    result.Success = await _appServiceservices.Update(produtoServicoDto);
                    var descricao = $"Atualizou um produto ou serviço com o nome {produtoServicoDto.Nome}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, produtoServicoDto.ID);
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
        /// Metodo para inativar um Produto e Serviço
        /// </summary>
        /// <param name="id">Guid da services</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactiveservices/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "OpcoesProdutoServico_Excluir")]
        public async Task<GenericResult<ProdutoServicoDto>> Delete(Guid id)
        {
            var result = new GenericResult<ProdutoServicoDto>();
            LogDto logDto;
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _appServiceservices.Inactive(id);

                var descricao = $" Usuario {userLogin} desativou um produto ou serviço com o nome ID: {id}";
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
