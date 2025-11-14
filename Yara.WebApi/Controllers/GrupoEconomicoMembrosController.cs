using System;
using System.Collections.Generic;
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
    [Authorize]
    [RoutePrefix("groupsmembers")]
    public class GrupoEconomicoMembrosController : ApiController
    {
        private readonly IAppServiceGrupoEconomicoMembros _serviceGrupoEconomicoMembros;
        private readonly IAppServiceContaCliente _serviceContaCliente;
        private readonly IAppServiceLog _log;
        private readonly IAppServiceUsuario _usuario;
        private readonly GrupoEconomicoMembrosValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="serviceGrupoEconomicoMembros"></param>
        /// <param name="log"></param>
        /// <param name="usuario"></param>
        /// 
        public GrupoEconomicoMembrosController(IAppServiceGrupoEconomicoMembros serviceGrupoEconomicoMembros, IAppServiceLog log, IAppServiceUsuario usuario, IAppServiceContaCliente serviceContaCliente)
        {
            _serviceGrupoEconomicoMembros = serviceGrupoEconomicoMembros;
            _serviceContaCliente = serviceContaCliente;
            _log = log;
            _usuario = usuario;
            _validator = new GrupoEconomicoMembrosValidator();
        }

        /// <summary>
        /// Retorna todos os grupos, possibilitando utilizar filtros OData
        /// </summary>
        /// <param name="options">Filtros OData</param>
        /// <returns>Lista de Grupos</returns>
        [HttpGet]
        [Route("v1/getgroups")]
        public async Task<GenericResult<IQueryable<GrupoEconomicoMembrosDto>>> Get(ODataQueryOptions<GrupoEconomicoMembrosDto> options)
        {
            var result = new GenericResult<IQueryable<GrupoEconomicoMembrosDto>>();

            try
            {
                var grupo = await _serviceGrupoEconomicoMembros.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(grupo.AsQueryable(), new ODataQuerySettings()).Cast<GrupoEconomicoMembrosDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(grupo.AsQueryable()).Cast<GrupoEconomicoMembrosDto>();
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
        /// Busca Grupo Economico e Membros pelo por código do grupo
        /// </summary>
        /// <param name="id">Código do Grupo</param>
        /// <returns>Unico Grupo</returns>
        [HttpGet]
        [Route("v1/getcodgroups/{id:guid}")]
        public async Task<GenericResult<GrupoEconomicoMembrosDto>> Get(Guid id)
        {
            var result = new GenericResult<GrupoEconomicoMembrosDto>();

            try
            {
                result.Result = await _serviceGrupoEconomicoMembros.GetAsync(c => c.GrupoEconomicoID.Equals(id));
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
        /// Busca Grupo Economico e Membros por código do cliente
        /// </summary>
        /// <param name="id">Código do Grupo</param>
        /// <returns>Unico Grupo</returns>
        [HttpGet]
        [Route("v1/getcodclient/{id:guid}")]
        public async Task<GenericResult<IEnumerable<GrupoEconomicoMembrosDto>>> GetGrupoPorCliente(Guid id)
        {
            var result = new GenericResult<IEnumerable<GrupoEconomicoMembrosDto>>();

            try
            {
                result.Result = await _serviceGrupoEconomicoMembros.GetAllFilterAsync(c => c.ContaClienteID.Equals(id));
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
        /// Método que retorna Lista Grupo Economico e Membros
        /// </summary>
        /// <param name="options"></param>
        /// <param name="detalheDto"></param>
        /// <returns>Objeto com os dados da Conta Cliente</returns>
        [HttpPost]
        [Route("v1/postgroupsdetails")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "GrupoEconomico_Acesso")]
        public async Task<GenericResult<BuscaGrupoEconomicoDetalheDto>> PostOrdemVendaClientePrazo(BuscaGrupoEconomicoDetalheDto detalheDto)
        {
            var result = new GenericResult<BuscaGrupoEconomicoDetalheDto>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();

                // ESTE MÉTODO TEM EXCEPTION NO ARQUIVO DE LOG! TIMEOUT:
                // 2018-04-05 08:23:12,607 [40] ERROR YaraLog [(null)] – Execution Timeout Expired.  The timeout period elapsed prior to completion of the operation or the server is not responding.
                result.Result = await _serviceGrupoEconomicoMembros.BuscaContaCliente(detalheDto.GrupoId, empresa);
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
        /// Metodo para inserir membros no grupo economico
        /// </summary>
        /// <param name="economicoDetalheDtos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertmembers")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "GrupoEconomico_Inserir")]
        public async Task<GenericResult<GrupoEconomicoMembrosDto>> Post(List<GrupoEconomicoMembrosDto> economicoDetalheDtos)
        {
            var result = new GenericResult<GrupoEconomicoMembrosDto>();
            var validationResult = _validator.Validate(economicoDetalheDtos.FirstOrDefault());

            if (validationResult.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();
                    var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                    var serviceResult = await _serviceGrupoEconomicoMembros.InsertAsyncList(economicoDetalheDtos, new Guid(userLogin), empresa, url);

                    result.Success = serviceResult.Value;

                    ICollection<string> insertedUsers = new List<string>();
                    foreach (var ccID in economicoDetalheDtos)
                    {
                        var membro = await _serviceContaCliente.GetAsync(c => c.ID.Equals(ccID.ContaClienteID));
                        insertedUsers.Add(membro.Nome);
                    }

                    var integrantes = await _serviceGrupoEconomicoMembros.GetAllFilterAsync(c => c.GrupoEconomicoID.Equals(serviceResult.Key.Value));
                    var nomeGrupo = integrantes.FirstOrDefault()?.GrupoEconomico?.Nome;
                    var grupoId = economicoDetalheDtos.Select(c => c.GrupoEconomicoID).FirstOrDefault();

                    var descricao = $"Usuario {User.Identity.Name}, solicitou a inclusão do(s) cliente(s) {string.Join(", ", insertedUsers)} para o grupo economico {nomeGrupo}.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.GrupoEconomico, grupoId);
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
                result.Errors = validationResult.GetErrors();

            return result;
        }

        /// <summary>
        /// Metodo para inativar membros no grupo economico
        /// </summary>
        /// <param name="economicoDetalheDtos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/inactivemembers")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "GrupoEconomico_Inserir")]
        public async Task<GenericResult<GrupoEconomicoMembrosDto>> PostInativaClienteGrupoEconomico(List<GrupoEconomicoMembrosDto> economicoDetalheDtos)
        {
            var result = new GenericResult<GrupoEconomicoMembrosDto>();
            var validationResult = _validator.Validate(economicoDetalheDtos.FirstOrDefault());

            if (validationResult.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();
                    var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                    var serviceResult = await _serviceGrupoEconomicoMembros.InactiveAsyncList(economicoDetalheDtos, new Guid(userLogin), empresa, url);

                    result.Success = serviceResult.Value;

                    ICollection<string> removedUsers = new List<string>();
                    foreach(var ccID in economicoDetalheDtos)
                    {
                        var membro = await _serviceContaCliente.GetAsync(c => c.ID.Equals(ccID.ContaClienteID));
                        removedUsers.Add(membro.Nome);
                    }

                    var integrantes = await _serviceGrupoEconomicoMembros.GetAllFilterAsync(c => c.GrupoEconomicoID.Equals(serviceResult.Key.Value));
                    var nomeGrupo = integrantes.FirstOrDefault()?.GrupoEconomico?.Nome;
                    var grupoId = economicoDetalheDtos.Select(c => c.GrupoEconomicoID).FirstOrDefault();

                    var descricao = $"Usuario {User.Identity.Name}, solicitou a exclusão do(s) cliente(s) {string.Join(", ", removedUsers)} para o grupo economico {nomeGrupo}.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.GrupoEconomico, grupoId);
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
                result.Errors = validationResult.GetErrors();

            return result;
        }
    }
}
