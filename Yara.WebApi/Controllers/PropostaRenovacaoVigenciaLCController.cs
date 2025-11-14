using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("renewalLC")]
    [Authorize]
    public class PropostaRenovacaoVigenciaLCController : ApiController
    {
        private readonly IAppServicePropostaRenovacaoVigenciaLC _proposta;
        private readonly IAppServicePropostaRenovacaoVigenciaLCComite _comite;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="proposta"></param>
        public PropostaRenovacaoVigenciaLCController(IAppServicePropostaRenovacaoVigenciaLC proposta, IAppServicePropostaRenovacaoVigenciaLCComite comite, IAppServiceLog log)
        {
            _proposta = proposta;
            _comite = comite;
            _log = log;
        }

        /// <summary>
        /// Metodo que retorna uma tela de consulta de Contas Clientes com Filtros
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Tipo eq Bovino</param>
        /// <param name="filter">Filtros</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/client/search")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaRenovacaoLimite_Acesso")]
        public async Task<GenericResult<IQueryable<BuscaContaClientePropostaRenovacaoLCDto>>> PostClientFilter(ODataQueryOptions<BuscaContaClientePropostaRenovacaoLCDto> options, FiltroContaClientePropostaRenovacaoVigenciaLCDto filter)
        {
            var result = new GenericResult<IQueryable<BuscaContaClientePropostaRenovacaoLCDto>>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();
                filter.EmpresasID = empresaId;

                var ret = await _proposta.GetClientListByFilterAsync(filter);

                int totalReg = ret.Count();
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(ret.AsQueryable(), new ODataQuerySettings()).Cast<BuscaContaClientePropostaRenovacaoLCDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(ret.AsQueryable()).Cast<BuscaContaClientePropostaRenovacaoLCDto>();
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
        /// Metodo que retorna uma tela de consulta de Contas Clientes com Filtros
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Tipo eq Bovino</param>
        /// <param name="clientes">clientes</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/client/searchfiltered")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaRenovacaoLimite_Acesso")]
        public async Task<GenericResult<IQueryable<BuscaContaClientePropostaRenovacaoLCDto>>> PostClientFilterFiltered(ODataQueryOptions<BuscaContaClientePropostaRenovacaoLCDto> options, ListaClientePropostaRenovacaoVigenciaLCDto clientes)
        {
            var result = new GenericResult<IQueryable<BuscaContaClientePropostaRenovacaoLCDto>>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();

                var ret = await _proposta.GetClientListByFilterAsync(clientes, empresaId);

                int totalReg = ret.Count();
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(ret.AsQueryable(), new ODataQuerySettings()).Cast<BuscaContaClientePropostaRenovacaoLCDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(ret.AsQueryable()).Cast<BuscaContaClientePropostaRenovacaoLCDto>();
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
        /// Metodo que exporta uma tela de consulta de Contas Clientes com Filtros
        /// </summary>
        /// <param name="filter">Filtros para Busca de Propostas</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/client/export")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaRenovacaoLimite_Acesso")]
        public async Task<HttpResponseMessage> PostClientFilterExcel(FiltroContaClientePropostaRenovacaoVigenciaLCDto filter)
        {
            HttpResponseMessage result = null;

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();
                filter.EmpresasID = empresaId;

                var arquivo = await _proposta.GetClientListExcelByFilterAsync(filter);

                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(arquivo);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = $"Clientes_Renovacao_Filtro_{DateTime.Now:yyyy-MM-dd}.xlsx"
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            catch (Exception e)
            {
                var error = new ErrorsYara();
                error.ErrorYara(e);
                result = Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return result;
        }

        /// <summary>
        /// Metodo que cria uma proposta de renovação de vigência de LC
        /// </summary>
        /// <param name="clientes">Lista de IDs de contas clientes integrantes da proposta</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/proposal/insert")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaRenovacaoLimite_Inserir")]
        public async Task<GenericResult<Guid>> PostInsertProposal(ListaClientePropostaRenovacaoVigenciaLCDto clientes)
        {
            var result = new GenericResult<Guid>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();

                string urlSerasa;
                string usuarioSerasa;
                string senhaSerasa;

                if (empresaId == "G")
                {
                    urlSerasa = ConfigurationManager.AppSettings["URLSerasa_G"];
                    usuarioSerasa = ConfigurationManager.AppSettings["UsuarioSerasa_G"];
                    senhaSerasa = ConfigurationManager.AppSettings["SenhaSerasa_G"];
                }
                else
                {
                    urlSerasa = ConfigurationManager.AppSettings["URLSerasa"];
                    usuarioSerasa = ConfigurationManager.AppSettings["UsuarioSerasa"];
                    senhaSerasa = ConfigurationManager.AppSettings["SenhaSerasa"];
                }

                var propostaRenovacaoVigenciaLCID = Guid.NewGuid();

                result.Success = await _proposta.InsertPropostalAsync(clientes, new Guid(userLogin), propostaRenovacaoVigenciaLCID, empresaId, urlSerasa, usuarioSerasa, senhaSerasa);
                result.Result = propostaRenovacaoVigenciaLCID;

                var descricao = $"Proposta de Renovação de Vigência de LC criada pelo usuário: {User.Identity.Name}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, propostaRenovacaoVigenciaLCID);
                _log.Create(logDto);
            }
            catch (ArgumentException arg)
            {
                var error = new ErrorsYara();
                error.ErrorYara(arg as Exception);
                result.Success = false;
                result.Errors = new[] { arg.Message };
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
        /// Metodo que busca uma proposta de renovação de vigência de LC pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/proposal/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaRenovacaoLimite_Acesso")]
        public async Task<GenericResult<PropostaRenovacaoVigenciaLCDto>> GetProposal(Guid id)
        {
            var result = new GenericResult<PropostaRenovacaoVigenciaLCDto>();

            try
            {
                result.Result = await _proposta.GetAsync(c => c.ID.Equals(id));
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
        /// Metodo que exporta os clientes de uma proposta de renovação de vigência de LC pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/proposal/export/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaRenovacaoLimite_Acesso")]
        public async Task<HttpResponseMessage> PostClientProposalExcel(Guid id)
        {
            HttpResponseMessage result = null;

            try
            {
                var arquivo = await _proposta.GetProposalClientListExcelAsync(id);

                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(arquivo);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = $"Clientes_Renovacao_Proposta_{DateTime.Now:yyyy-MM-dd}.xlsx"
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var descricao = $"Proposta de Renovação de Vigência de LC exportada pelo usuário: {User.Identity.Name}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, id);
                _log.Create(logDto);
            }
            catch (Exception e)
            {
                var error = new ErrorsYara();
                error.ErrorYara(e);
                result = Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return result;
        }

        /// <summary>
        /// Metodo que envia para comitê uma proposta de renovação de vigência de LC
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/proposal/sendcommittee/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaRenovacaoLimite_Editar")]
        public async Task<GenericResult<bool>> PostSendCommittee(Guid id)
        {
            var result = new GenericResult<bool>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                result.Result = await _comite.SendCommittee(id, new Guid(userLogin), url);
                result.Success = true;

                var descricao = $"Proposta de Renovação de Vigência de LC enviada para comitê pelo usuário: {User.Identity.Name}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, id);
                _log.Create(logDto);
            }
            catch (ArgumentException arg)
            {
                var error = new ErrorsYara();
                error.ErrorYara(arg as Exception);
                result.Success = false;
                result.Errors = new[] { arg.Message };
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
        /// Metodo que retorna todos os membros do comitê de uma proposta pelo ID da proposta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/proposal/getcommittee/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaRenovacaoLimite_Acesso")]
        public async Task<GenericResult<IEnumerable<PropostaRenovacaoVigenciaLCComiteDto>>> GetProposalCommittee(Guid id)
        {
            var result = new GenericResult<IEnumerable<PropostaRenovacaoVigenciaLCComiteDto>>();

            try
            {
                result.Result = await _comite.GetCommitteeByProposalID(id);
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
        /// Metodo que insere uma aprovação / rejeição de um comitê pelo ID
        /// </summary>
        /// <param name="decisaoComitePropostaRenovacaoVigenciaLC"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/proposal/sendapproval/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaRenovacaoLimite_Aprovar")]
        public async Task<GenericResult<bool>> PostSendApproval(DecisaoComitePropostaRenovacaoVigenciaLCDto decisaoComitePropostaRenovacaoVigenciaLC)
        {
            var result = new GenericResult<bool>();

            try
            {
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;
                var empresaId = Request.Properties["Empresa"].ToString();
                decisaoComitePropostaRenovacaoVigenciaLC.EmpresasID = empresaId;

                result.Result = await _comite.SendApproval(decisaoComitePropostaRenovacaoVigenciaLC, url);
                result.Success = true;

                var descricao = $"Proposta de Renovação de Vigência de LC enviada para aprovação pelo usuário: {User.Identity.Name}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, decisaoComitePropostaRenovacaoVigenciaLC.PropostaRenovacaoVigenciaLCComiteID);
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

        /// <summary>
        /// Metodo que cancela uma proposta de renovação de vigência de LC pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/proposal/cancel/{id:guid}")]
        public async Task<GenericResult<bool>> PostCancelProposal(Guid id)
        {
            var result = new GenericResult<bool>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Result = await _proposta.CancelProposalAsync(id, new Guid(userLogin));
                result.Success = true;

                var descricao = $"Proposta de Renovação de Vigência de LC cancelada pelo usuário: {User.Identity.Name}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, id);
                _log.Create(logDto);
            }
            catch(Exception e)
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
