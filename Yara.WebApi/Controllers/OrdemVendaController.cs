using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.WebApi.Extensions;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591
#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("order")]
    [Authorize]
    public class OrdemVendaController : ApiController
    {
        private readonly IAppServiceOrdemVenda _venda;
        private readonly IAppServiceEnvioEmail _email;
        private readonly IAppServiceOrdemVendaFluxo _ordemVendaFluxo;
        private readonly IAppServiceLog _log;
        private readonly StatusOrdemVendaValidator _validator;
        private readonly SolicitanteFluxoValidatior _solicitanteFluxoValidatior;
        private readonly LiberacaoOrdemVendaFluxoValidator _liberacaoOrdemVendaFluxoValidator;
        private readonly CriaFluxoOrdemVendaValidator _criaFluxoOrdemVendaValidator;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="venda"></param>
        /// <param name="log"></param>
        /// <param name="email"></param>
        /// <param name="ordemVendaFluxo"></param>
        public OrdemVendaController(IAppServiceOrdemVenda venda, IAppServiceLog log, IAppServiceEnvioEmail email, IAppServiceOrdemVendaFluxo ordemVendaFluxo)
        {
            _venda = venda;
            _log = log;
            _email = email;
            _ordemVendaFluxo = ordemVendaFluxo;
            _validator = new StatusOrdemVendaValidator();
            _solicitanteFluxoValidatior = new SolicitanteFluxoValidatior();
            _liberacaoOrdemVendaFluxoValidator = new LiberacaoOrdemVendaFluxoValidator();
            _criaFluxoOrdemVendaValidator = new CriaFluxoOrdemVendaValidator();
        }

        /// <summary>
        /// Metodo que retorna os detalhes de uma ordem por número
        /// </summary>
        /// <param name="id">Numero da Ordem</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getbyid/{id}")]
        public async Task<GenericResult<OrdemVendaDto>> GetID(string id)
        {
            var result = new GenericResult<OrdemVendaDto>();

            try
            {
                result.Result = await _venda.GetOrdemAsync(id);
                result.Count = 1;
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
        /// Metodo que retorna os logs de integração com SAP
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getlogsap")]
        public async Task<GenericResult<IEnumerable<LogEnvioOrdensSAPDto>>> GetLogSAP(ODataQueryOptions<LogEnvioOrdensSAPDto> options)
        {
            var result = new GenericResult<IEnumerable<LogEnvioOrdensSAPDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                var retLogs = await _venda.GetLogEnvioOrdensSAP(empresa);

                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retLogs.AsQueryable(), new ODataQuerySettings()).Cast<LogEnvioOrdensSAPDto>();
                    totalReg = filtro.Count();
                }

                result.Result = options.ApplyTo(retLogs.AsQueryable()).Cast<LogEnvioOrdensSAPDto>();
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
        /// Metodo que retorna os detalhes de um Fluxo de Liberação de Ordem através do código do Solicitante
        /// </summary>
        /// <param name="SolicitanteID">Código do Solicitante</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getbysolicitante/{id}")]
        public async Task<GenericResult<SolicitanteFluxoDto>> GetSolicitanteID(Guid id)
        {
            var result = new GenericResult<SolicitanteFluxoDto>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();

                result.Result = await _venda.GetSolicitanteAsync(id, empresa);

                result.Count = 1;
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
        /// Metodo que retorna os detalhes de um Fluxo de Liberação de Ordem através do código do Solicitante
        /// </summary>
        /// <param name="SolicitanteID">Código do Solicitante</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getbyfluxosolicitante/{id}")]
        public async Task<GenericResult<IEnumerable<LiberacaoOrdemVendaFluxoDto>>> GetFluxo(Guid id)
        {
            var result = new GenericResult<IEnumerable<LiberacaoOrdemVendaFluxoDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();

                var fluxo = await _venda.GetFluxoSolicitanteAsync(id, empresa);

                result.Result = fluxo;
                result.Count = 1;
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
        /// Metodo que retorna a lista de Divisao de Remessa
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Nome eq irrigação</param>
        /// <param name="ordemVendaDto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getlist")]
        public async Task<GenericResult<IEnumerable<BuscaOrdemVendaDto>>> GetContaClienteConsulta(ODataQueryOptions<BuscaOrdemVendaDto> options)
        {
            var result = new GenericResult<IEnumerable<BuscaOrdemVendaDto>>();

            try
            {
                var ordem = await _venda.ConsultaOrdem();
                if (ordem != null)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(ordem.AsQueryable(), new ODataQuerySettings()).Cast<BuscaOrdemVendaDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(ordem.AsQueryable()).Cast<BuscaOrdemVendaDto>();
                    result.Count = totalReg > 0 ? totalReg : ordem.Count();
                    result.Success = true;
                }
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

        ///// <summary>
        ///// Metodo para bloquear toda a ordem de vendas
        ///// </summary>
        ///// <param name="vendaDto"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("v1/postblock")]
        //[ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "OrdemVenda_BloqueioManual")]
        //public async Task<GenericResult<StatusOrdemVendasDto>> PostBloqueioManual(StatusOrdemVendasDto vendaDto)
        //{
        //    var result = new GenericResult<StatusOrdemVendasDto>();
        //    var validationResult = _validator.Validate(vendaDto);
        //    var userClaims = User.Identity as ClaimsIdentity;

        //    if (validationResult.IsValid)
        //    {
        //        string userid = null;
        //        try
        //        {
        //            if (userClaims != null) userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
        //            result.Success = await _venda.UpdateAsync(vendaDto);
        //            result.Errors = new[] { Resources.Resources.OrderBlockSuccess };
        //            var descricao = $"Ordem de Venda: {vendaDto.Numero} - do Cliente : {vendaDto.Pagador}, foi bloqueada manualmente pelo usuário: {User.Identity.Name}.";
        //            var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.OrdemBloqueadoManual);
        //            _log.Create(logDto);
        //        }
        //        catch (Exception e)
        //        {
        //            result.Success = false;
        //            result.Errors = new[] { Resources.Resources.Error };
        //            var error = new ErrorsYara();
        //            error.ErrorYara(e);
        //        }
        //    }
        //    else
        //    {
        //        result.Errors = validationResult.GetErrors();
        //    }

        //    return result;
        //}

        /// <summary>
        /// Metodo de solicitação de liberação manual de ordens de vendas
        /// </summary>
        /// <param name="solicitanteFluxoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/postrequester")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "OrdemVenda_LiberacaoManual")]
        public async Task<GenericResult<SolicitanteFluxoDto>> PostSolicitaLiberacaoManualOrdemVenda(CriaFluxoOrdemVendaDto fluxo)
        {
            var result = new GenericResult<SolicitanteFluxoDto>();
            var validationResult = _criaFluxoOrdemVendaValidator.Validate(fluxo);

            if (validationResult.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();
                    var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                    fluxo.EmpresaID = empresa;
                    fluxo.UsuarioID = new Guid(userid);

                    result.Success = await _venda.GerarFluxo(fluxo, url);
                    result.Errors = new[] { Resources.Resources.OrderRequestSuccess };

                    var descricao = $"Solicitou a liberação manual de Ordem para o Fluxo de Aprovação.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.OrdemLiberacaoManual);
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
        /// Metodo para alterar o status do Fluxo de aprovação manual e adicionar o Id do Usuario
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/postalterstatus")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "OrdemVenda_AlteraStatusFluxo")]
        public async Task<GenericResult<OrdemVendaFluxoDto>> PostAlteraStatusFluxo(OrdemVendaFluxoDto dto)
        {
            var result = new GenericResult<OrdemVendaFluxoDto>();
            //var validationResult = _solicitanteFluxoValidatior.Validate(solicitanteFluxoDto);

            //if (validationResult.IsValid)
            //{
            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
               
                // dto.UsuarioID = new Guid(userid);

                result.Success = await _ordemVendaFluxo.UpdateStatusFluxo(dto);

                if (result.Success)
                    result.Errors = new[] { Resources.Resources.OrderAttr };

                //var descricao = $"Divisão de Remessa de numero: {dto.Divisao} - {dto.ItemOrdemVenda} - {dto.OrdemVendaNumero} atribuida no fluxo para o usuario {dto.UsuarioID} com status de Em Analise.";
                //var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.OrdemLiberacaoManual);
                //_log.Create(logDto);
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
            //}

            return result;
        }

        /// <summary>
        /// Metodo para inclusão e aprovação do fluxo de ordem de vendas
        /// </summary>
        /// <param name="fluxo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/postflowapproval")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "OrdemVenda_AprovacaoFluxo")]
        public async Task<GenericResult<LiberacaoOrdemVendaFluxoDto>> PostFluxoAprovacao(LiberacaoOrdemVendaFluxoDto fluxo)
        {
            var result = new GenericResult<LiberacaoOrdemVendaFluxoDto>();
            var validationResult = _liberacaoOrdemVendaFluxoValidator.Validate(fluxo);

            if (validationResult.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();
                    var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                    fluxo.UsuarioID = new Guid(userid);
                    fluxo.EmpresasId = empresa;
                    fluxo.StatusOrdemVendaNome = "LM";

                    result.Result = await _ordemVendaFluxo.FluxoAprovacaoReprovacaoOrdem(fluxo, url);
                    result.Success = true;
                    result.Errors = new[] { Resources.Resources.OrderSuccess };
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
        /// Metodo para bloqueio do fluxo de ordem de vendas
        /// </summary>
        /// <param name="fluxo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/postflowblock")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "OrdemVenda_BloqueioFluxo")]
        public async Task<GenericResult<LiberacaoOrdemVendaFluxoDto>> PostFluxoBloqueio(LiberacaoOrdemVendaFluxoDto fluxo)
        {
            var result = new GenericResult<LiberacaoOrdemVendaFluxoDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;
                
                fluxo.UsuarioID = new Guid(userid);
                fluxo.EmpresasId = empresa;
                fluxo.StatusOrdemVendaNome = "BM";

                result.Result = await _ordemVendaFluxo.FluxoAprovacaoReprovacaoOrdem(fluxo, url);
                result.Success = true;
                result.Errors = new[] { Resources.Resources.OrderSuccess };
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

        /// <summary>
        /// Metodo que recebe a lista para bloqueio do fluxo de ordem de vendas
        /// </summary>
        /// <param name="fluxo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/postlistflowblock")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "OrdemVenda_BloqueioFluxo")]
        public async Task<GenericResult<SolicitanteFluxoDto>> PostListaFluxoBloqueio(CriaFluxoOrdemVendaDto fluxo)
        {
            var result = new GenericResult<SolicitanteFluxoDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                fluxo.UsuarioID = new Guid(userid);
                fluxo.EmpresaID = empresa;

                result.Success = _venda.GerarBloqueioFluxo(fluxo);
                result.Errors = new[] { Resources.Resources.OrderSuccess };
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

        ///// <summary>
        ///// Api para encaminhar email.
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("v1/postsend")]
        //public async Task<Result> PostEmail(SolicitanteFluxoDto fluxo)
        //{
        //    var usuarioDtos = new List<UsuarioDto>();
        //    var dto = new UsuarioDto
        //    {
        //        Nome = "Luciano Carlos de Jesus",
        //        Email = "luciano.jesus@performait.com"
        //    };
        //    usuarioDtos.Add(dto);

        //    var dto2 = new UsuarioDto
        //    {
        //        Nome = "Claudemir P. Júnior",
        //        Email = "claudemir.junior@performait.com"
        //    };
        //    usuarioDtos.Add(dto2);

        //    var dto3 = new UsuarioDto
        //    {
        //        Nome = "Carlos R. Silvestre",
        //        Email = "carlos.silvestre@performait.com"
        //    };
        //    usuarioDtos.Add(dto3);

        //    var dto4 = new UsuarioDto
        //    {
        //        Nome = "André L. Paganuchi",
        //        Email = "andre.paganuchi@performait.com"
        //    };
        //    usuarioDtos.Add(dto4);

        //    var ret = await _email.SendMailLiberacaoManual(usuarioDtos, fluxo);
        //    var result = new Result();
        //    if (ret.Key)
        //    {
        //        result.Success = ret.Key;
        //    }
        //    else
        //    {
        //        result.Success = ret.Key;
        //        result.Errors = new[] { ret.Value };
        //    }
        //    return null;
        //}

        /// <summary>
        /// Metodo que recebe a lista para liberacao de carregamento de divisoes de remessa
        /// </summary>
        /// <param name="remessas">Lista das divisões de remessa</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/freeload")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "OrdemVenda_BloqueioCarregamento")]
        public async Task<Result> PostLiberarBloqueioCarregamento(List<SolicitacaoBloqueioRemessaDto> remessas)
        {
            var result = new Result();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userIdString = userClaims != null ? userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value : "";
                var userId = new Guid(userIdString);
                var empresa = Request.Properties["Empresa"].ToString();

                var ret = await _venda.LiberarBloqueioCarregamento(userId, empresa, remessas);

                result.Success = ret;
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

        /// <summary>
        /// Metodo que recebe a lista para bloqueio de carregamento de divisoes de remessa
        /// </summary>
        /// <param name="remessas">Lista das divisões de remessa</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/blockload")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "OrdemVenda_BloqueioCarregamento")]
        public async Task<Result> PostSolicitarBloqueioCarregamento(List<SolicitacaoBloqueioRemessaDto> remessas)
        {
            var result = new Result();

            try
            {

                var userClaims = User.Identity as ClaimsIdentity;
                var userIdString = userClaims != null ? userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value : "";
                var userId = new Guid(userIdString);
                var empresa = Request.Properties["Empresa"].ToString();

                var ret = await _venda.SolicitarBloqueioCarregamento(userId, empresa, remessas);

                result.Success = ret;
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

        /// <summary>
        /// Busca OVs de uma conta cliente para Aba Liberação de OVs
        /// </summary>
        /// <param name="options">OData</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getproposalOVbyaccountclientpage/{contaClienteID:guid}")]
        public async Task<GenericResult<IQueryable<DivisaoRemessaCockPitDto>>> GetOrdemVenda(ODataQueryOptions<DivisaoRemessaCockPitDto> options, Guid contaClienteID)
        {
            var result = new GenericResult<IQueryable<DivisaoRemessaCockPitDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();

                var retpropostal = await _venda.BuscaOrdemVenda(contaClienteID, empresa);

                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retpropostal.AsQueryable(), new ODataQuerySettings()).Cast<DivisaoRemessaCockPitDto>();
                    totalReg = filtro.Count();
                }

                result.Result = options.ApplyTo(retpropostal.AsQueryable()).Cast<DivisaoRemessaCockPitDto>();
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
        /// Busca OVs que estão somando remessa para o cliente por tipo de remessa.
        /// </summary>
        /// <param name="accountClientID"></param>
        /// <param name="empresaID"></param>
        /// <param name="tipoRemessa"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getclientdeliveries/{accountClientID:guid}/{empresaID}/{tipoRemessa}")]
        public async Task<GenericResult<IEnumerable<BuscaRemessasClienteDto>>> GetClientDeliveries(Guid accountClientID, string empresaID, string tipoRemessa)
        {
            var result = new GenericResult<IEnumerable<BuscaRemessasClienteDto>>();

            try
            {
                var remessas = await _venda.BuscaRemessasCliente(accountClientID, empresaID, tipoRemessa);
                result.Result = remessas;
                result.Success = true;
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
