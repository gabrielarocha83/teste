using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.Extensions;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("economicgroup")]
    public class GrupoEconomicoController : ApiController
    {
        private readonly IAppServiceGrupoEconomico _serviceGrupoEconomico;
        private readonly IAppServiceUsuario _usuario;
        private readonly IAppServiceClassificacaoGrupoEconomico _classificacaoGrupoEconomico;
        private readonly IAppServiceLog _log;
        private readonly GrupoEconomicoValidator _validator;
        private readonly GrupoEconomicoDeleteValidator _validatorDelete;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="serviceGrupoEconomico"></param>
        /// <param name="log"></param>
        /// <param name="usuario"></param>
        /// <param name="classificacaoGrupoEconomico"></param>
        public GrupoEconomicoController(IAppServiceGrupoEconomico serviceGrupoEconomico, IAppServiceLog log, IAppServiceUsuario usuario, IAppServiceClassificacaoGrupoEconomico classificacaoGrupoEconomico)
        {
            _serviceGrupoEconomico = serviceGrupoEconomico;
            _log = log;
            _classificacaoGrupoEconomico = classificacaoGrupoEconomico;
            _usuario = usuario;
            _validator = new GrupoEconomicoValidator();
            _validatorDelete = new GrupoEconomicoDeleteValidator();
        }

        /// <summary>
        /// Validação de Grupo Econômico por nome
        /// </summary>
        /// <param name="name">Nome do Grupo Econômico</param>
        /// <returns>True or False</returns>
        [HttpGet]
        [Route("v1/getgroupbyname/{name}")]
        public async Task<GenericResult<bool>> Get(string name)
        {
            var result = new GenericResult<bool>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();

                var groups = await _serviceGrupoEconomico.GetAsync(c => c.Nome.Equals(name) && c.EmpresasID.Equals(empresa) && c.Ativo);

                result.Result = groups != null;
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
        /// Insere um grupo Econômico em uma contaCliente
        /// </summary>
        /// <param name="grupoDto">Dados do Grupo Econômico</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/inserteconomicgroup")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "GrupoEconomico_Inserir")]
        public async Task<GenericResult<BuscaGrupoEconomicoDetalheDto>> PostOrdemVendaClientePrazo(NovoGrupoEconomicoDto grupoDto)
        {
            var result = new GenericResult<BuscaGrupoEconomicoDetalheDto>();
            var validationResult = _validator.Validate(grupoDto);

            if (validationResult.IsValid)
            {
                try
                {
                    var empresa = Request.Properties["Empresa"].ToString();
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    grupoDto.ID = Guid.NewGuid();
                    grupoDto.EmpresaID = empresa;
                    grupoDto.UsuarioIDCriacao = new Guid(userid);

                    result.Success = await _serviceGrupoEconomico.InsertAsync(grupoDto);

                    var classificacao = await _classificacaoGrupoEconomico.GetbyID(grupoDto.ClassificacaoGrupoEconomicoID);

                    var descricao = $"O usuário {User.Identity.Name} criou um novo grupo com o nome {grupoDto.Nome} de {classificacao.Nome}.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.GrupoEconomico, grupoDto.ID);
                    _log.Create(logDto);
                }
                catch (ArgumentException a)
                {
                    result.Success = false;
                    result.Errors = new[] { a.Message };
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
        /// Busca Grupo Economico por código do cliente
        /// </summary>
        /// <param name="id">Código do Grupo</param>
        /// <returns>Lista de Grupo</returns>
        [HttpGet]
        [Route("v1/getcodclient/{id:guid}", Order = 1)]
        [Route("v1/getcodclient/{id:guid}/{empresaId}", Order = 2)]
        public async Task<GenericResult<IEnumerable<BuscaGrupoEconomicoDto>>> GetGrupoPorCliente(Guid id, string empresaId = null)
        {
            var result = new GenericResult<IEnumerable<BuscaGrupoEconomicoDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();

                result.Result = await _serviceGrupoEconomico.BuscaGrupoEconomico(id, String.IsNullOrEmpty(empresaId) ? empresa : empresaId);
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
        /// Busca Grupo Economico por código do grupo economico
        /// </summary>
        /// <param name="id">Código do Grupo</param>
        /// <returns>Lista de Grupo</returns>
        [HttpGet]
        [Route("v1/getcodgroup/{id:guid}")]
        public async Task<GenericResult<IEnumerable<BuscaGrupoEconomicoDto>>> GetGrupoPorGrupo(Guid id)
        {
            var result = new GenericResult<IEnumerable<BuscaGrupoEconomicoDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();

                result.Result = await _serviceGrupoEconomico.BuscaGrupoEconomicoPorGrupo(id, empresa);
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
        /// Método que solicita a exclusão de um grupo econômico
        /// </summary>
        /// <param name="group">Grupo Econômico</param>
        /// <param name="contacliente">Conta Cliente</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/requestdeleteeconomicgroup/{group:guid}/{contacliente:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "GrupoEconomico_Excluir")]
        public async Task<GenericResult<GrupoEconomicoDto>> DeleteGroup(Guid group, Guid contacliente)
        {
            var result = new GenericResult<GrupoEconomicoDto>();

            //var validationResult = _validatorDelete.Validate(grupo);
            //if (validationResult.IsValid)
            //{
            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims?.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                var groupObject = await _serviceGrupoEconomico.GetAsync(c => c.ID.Equals(group));

                var grupo = new GrupoEconomicoFluxoDto();
                grupo.UsuarioID = new Guid(userid);
                grupo.EmpresaID = empresa;
                grupo.GrupoID = group;
                grupo.ClassificacaoGrupoEconomicoID = groupObject.ClassificacaoGrupoEconomicoID;
                grupo.ContaClienteID = contacliente;
                grupo.Nome = groupObject.Nome;

                result.Success = await _serviceGrupoEconomico.DeleteAsync(grupo, url);

                var classificacao = await _classificacaoGrupoEconomico.GetbyID(grupo.ClassificacaoGrupoEconomicoID);

                var descricao = $"O usuário {User.Identity.Name} solicitou a exclusão do grupo com o nome {grupo.Nome} de {classificacao.Nome}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.GrupoEconomico, grupo.GrupoID);
                _log.Create(logDto);

            }
            catch (ArgumentException a)
            {
                result.Success = false;
                result.Errors = new[] { a.Message };
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { Resources.Resources.Error };
                var error = new ErrorsYara();
                error.ErrorYara(e);
            }
            //}
            //else
            //    result.Errors = validationResult.GetErrors();

            return result;
        }

        /// <summary>
        /// Busca Histórico do Grupo Economico por código do grupo economico
        /// </summary>
        /// <param name="id">Código do Grupo</param>
        /// <returns>Histórico do Grupo</returns>
        [HttpGet]
        [Route("v1/grouphistory/{id:guid}")]
        public async Task<GenericResult<IEnumerable<BuscaHistoricoGrupoDto>>> GetHistory(Guid id)
        {
            var result = new GenericResult<IEnumerable<BuscaHistoricoGrupoDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();

                result.Result = await _serviceGrupoEconomico.BuscaHistoricoPorGrupo(id, empresa);
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
    }
}
