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
    [RoutePrefix("comments")]
    [Authorize]
    public class ContaClienteComentarioController : ApiController
    {
        private readonly IAppServiceContaClienteComentario _appServiceContaClienteComentario;
        private readonly IAppServiceLog _log;
        private readonly ContaClienteComentarioValidator _contaClienteComentarioValidator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServiceContaClienteComentario"></param>
        /// <param name="log"></param>
        public ContaClienteComentarioController(IAppServiceContaClienteComentario appServiceContaClienteComentario, IAppServiceLog log)
        {
            _appServiceContaClienteComentario = appServiceContaClienteComentario;
            _log = log;
            _contaClienteComentarioValidator = new ContaClienteComentarioValidator();
        }

        /// <summary>
        /// Método para buscar informações dos comentarios da conta cliente.
        /// </summary>
        /// <param name="options">Objeto de busca dos comentarios.</param>
        /// <returns>Listagem com os dados de comentario da conta cliente.</returns>
        [HttpGet]
        [Route("v1/listaccountscomments")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteComentario_Visualizar")]
        public async Task<GenericResult<IQueryable<ContaClienteComentarioDto>>> Get(ODataQueryOptions<ContaClienteComentarioDto> options)
        {
            var result = new GenericResult<IQueryable<ContaClienteComentarioDto>>();
            try
            {
                var listaContaClienteComentario = await _appServiceContaClienteComentario.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(listaContaClienteComentario.AsQueryable(), new ODataQuerySettings()).Cast<ContaClienteComentarioDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(listaContaClienteComentario.AsQueryable()).Cast<ContaClienteComentarioDto>();
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
        /// Método para buscar informações dos comentarios de um cliente de acordo com ID.
        /// </summary>
        /// <param name="options">Objeto de busca dos comentarios.</param>
        /// <returns>Listagem com os dados de comentario da conta cliente.</returns>
        [HttpGet]
        [Route("v1/listaccountscommentsid/{conta:Guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteComentario_Visualizar")]
        public async Task<GenericResult<IQueryable<ContaClienteComentarioDto>>> GetContaCliente(Guid conta)
        {
            var result = new GenericResult<IQueryable<ContaClienteComentarioDto>>();
            try
            {
                var listaContaClienteComentario = await _appServiceContaClienteComentario.GetAllFilterAsync(c=>c.ContaClienteID.Equals(conta));
                result.Result = listaContaClienteComentario.OrderByDescending(C=>C.DataCriacao).AsQueryable();
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
        /// Método para buscar informações dos comentarios de um cliente de acordo com ID.
        /// </summary>
        /// <param name="options">Objeto de busca dos comentarios.</param>
        /// <returns>Objeto com os dados de comentario da conta cliente.</returns>
        [HttpGet]
        [Route("v1/accountscomments/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteComentario_Visualizar")]
        public async Task<GenericResult<ContaClienteComentarioDto>> Get(Guid id)
        {
            var result = new GenericResult<ContaClienteComentarioDto>();
            try
            {
                result.Result = await _appServiceContaClienteComentario.GetAsync(g => g.ID.Equals(id));
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
        /// Método para inserir comentarios.
        /// </summary>
        /// <param name="clienteComentarioDto">Objeto com as informações para inserir dados</param>
        /// <returns>Booleano com sucesso ou erro da insersão.</returns>
        [HttpPost]
        [Route("v1/insertaccountscomments")]
        [ResponseType(typeof(ContaClienteComentarioDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteComentario_Inserir")]
        public GenericResult<ContaClienteComentarioDto> PostNovoComentario(ContaClienteComentarioDto clienteComentarioDto)
        {
            var result = new GenericResult<ContaClienteComentarioDto>();
            var contaClienteComentarioValidation = _contaClienteComentarioValidator.Validate(clienteComentarioDto);
            if (contaClienteComentarioValidation.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    clienteComentarioDto.UsuarioID = new Guid(userid);
                    clienteComentarioDto.Ativo = true;
             
                    result.Success = _appServiceContaClienteComentario.Insert(clienteComentarioDto);

                    var descricao = $"Inseriu um comentário.";
                    var level = EnumLogLevelDto.AccountClient;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, clienteComentarioDto.ContaClienteID);
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
                result.Errors = contaClienteComentarioValidation.GetErrors();

            return result;
        }

        /// <summary>
        /// Método para alteração de comentarios.
        /// </summary>
        /// <param name="clienteComentarioDto">Objeto com as informações para alteração dos dados</param>
        /// <returns>Booleano com sucesso ou erro da alteração.</returns>
        [HttpPut]
        [Route("v1/updateaccountscomments")]
        [ResponseType(typeof(ContaClienteComentarioDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteComentario_Inserir")]
        public async Task<GenericResult<ContaClienteComentarioDto>> PutAlteraComentario(ContaClienteComentarioDto clienteComentarioDto)
        {
            var result = new GenericResult<ContaClienteComentarioDto>();
            var contaClienteComentarioValidation = _contaClienteComentarioValidator.Validate(clienteComentarioDto);
            if (contaClienteComentarioValidation.IsValid)
            {
                try
                {
                    result.Success = await _appServiceContaClienteComentario.Update(clienteComentarioDto);
                    var descricao = $"Atualizou um comentário com a descrição {clienteComentarioDto.Descricao}";
                    var level = EnumLogLevelDto.AccountClient;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, clienteComentarioDto.ContaClienteID);
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
                result.Errors = contaClienteComentarioValidation.GetErrors();
            }
            return result;
        }
    }
}
