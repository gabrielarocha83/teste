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
    /// <summary>
    /// CRUD para Proposta de LC Adicional
    /// </summary>
    [RoutePrefix("extraLCProposal")]
    [Authorize]
    public class PropostaLCAdicionalController : ApiController
    {
        private readonly IAppServicePropostaLCAdicional _appServicePropostaLCAdicional;
        private readonly IAppServicePropostaLCAdicionalComite _appServicePropostaLCAdicionalComite;
        private readonly IAppServiceLog _appServiceLog;
        private readonly PropostaLCAdicionalInserirValidator _validator;
        private readonly IAppServiceProposta _proposta;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        public PropostaLCAdicionalController(IAppServicePropostaLCAdicional appServicePropostaLCAdicional, IAppServicePropostaLCAdicionalComite appServicePropostaLCAdicionalComite, IAppServiceLog appServiceLog, IAppServiceProposta proposta)
        {
            _appServicePropostaLCAdicional = appServicePropostaLCAdicional;
            _appServicePropostaLCAdicionalComite = appServicePropostaLCAdicionalComite;
            _appServiceLog = appServiceLog;
            _validator = new PropostaLCAdicionalInserirValidator();
            _proposta = proposta;
        }

        [HttpGet]
        [Route("v1/get/{PropostaID:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaLimiteCreditoAdicional_Acesso")]
        public async Task<GenericResult<PropostaLCAdicionalDto>> GetProposal(Guid PropostaID)
        {
            var result = new GenericResult<PropostaLCAdicionalDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var user = new Guid(userLogin);

                var ret = await _appServicePropostaLCAdicional.GetAsync(c => c.ID.Equals(PropostaID));
                if (ret.UsuarioIdAcompanhamento.Any() && ret.UsuarioIdAcompanhamento.Contains(user))
                {
                    ret.AcompanharProposta = true;
                }

                result.Success = true;
                result.Result = ret;
                result.Count = 0;
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

        [HttpPost]
        [Route("v1/insert")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaLimiteCreditoAdicional_Inserir")]
        public async Task<GenericResult<PropostaLCAdicionalDto>> PostInsertProposal(PropostaLCAdicionalDto propostaLCAdicional)
        {
            var result = new GenericResult<PropostaLCAdicionalDto>();
            var validation = _validator.Validate(propostaLCAdicional);

            if (validation.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresaID = Request.Properties["Empresa"].ToString();

                    propostaLCAdicional.EmpresaID = empresaID;
                    propostaLCAdicional.ID = Guid.NewGuid();
                    propostaLCAdicional.DataCriacao = DateTime.Now;
                    propostaLCAdicional.UsuarioIDCriacao = new Guid(userid);
                    propostaLCAdicional.ResponsavelID = new Guid(userid);

                    var retorno = await _proposta.ExistePropostaEmAndamentoAsync(propostaLCAdicional.ContaClienteID, propostaLCAdicional.EmpresaID);
                    if (!string.IsNullOrWhiteSpace(retorno))
                        throw new ArgumentException(retorno);

                    result.Success = await _appServicePropostaLCAdicional.InsertPropostalAsync(propostaLCAdicional);
                    result.Result = null;
                    result.Count = 0;

                    var descricao = $"Proposta de LC Adicional criada pelo usuário: {User.Identity.Name}.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, propostaLCAdicional.ID);
                    _appServiceLog.Create(logDto);
                }
                catch (ArgumentException arg)
                {
                    result.Success = false;
                    result.Errors = new[] { arg.Message };
                    var error = new ErrorsYara();
                    error.ErrorYara(arg as Exception);
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Errors = new[] { e.Message + $" | { Newtonsoft.Json.JsonConvert.SerializeObject(propostaLCAdicional) }" };
                    var error = new ErrorsYara();
                    error.ErrorYara(e);
                }

                return result;
            }
            else
                result.Errors = validation.GetErrors();

            return result;
        }

        [HttpPut]
        [Route("v1/update")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaLimiteCreditoAdicional_Inserir")]
        public async Task<GenericResult<PropostaLCAdicionalDto>> PutUpdateProposal(PropostaLCAdicionalDto propostaLCAdicional)
        {
            var result = new GenericResult<PropostaLCAdicionalDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    
                propostaLCAdicional.DataAlteracao = DateTime.Now;
                propostaLCAdicional.UsuarioIDAlteracao = new Guid(userid);
                propostaLCAdicional.ResponsavelID = new Guid(userid);

                result.Success = await _appServicePropostaLCAdicional.UpdatePropostalAsync(propostaLCAdicional);
                result.Result = null;
                result.Count = 0;

                var descricao = $"Proposta de LC Adicional atualizada pelo usuário: {User.Identity.Name}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, propostaLCAdicional.ID);
                _appServiceLog.Create(logDto);
            }
            catch (ArgumentException arg)
            {
                result.Success = false;
                result.Errors = new[] { arg.Message };
                var error = new ErrorsYara();
                error.ErrorYara(arg as Exception);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message + $" | { Newtonsoft.Json.JsonConvert.SerializeObject(propostaLCAdicional) }" };
                var error = new ErrorsYara();
                error.ErrorYara(e);
            }

            return result;
        }

        [HttpPut]
        [Route("v1/cancel")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaLimiteCreditoAdicional_Editar")]
        public async Task<GenericResult<PropostaLCAdicionalDto>> PutCancelProposal(PropostaLCAdicionalDto propostaLCAdicional)
        {
            var result = new GenericResult<PropostaLCAdicionalDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                result.Success = await _appServicePropostaLCAdicional.CancelProposalAsync(propostaLCAdicional.ID, new Guid(userLogin), url);
                result.Result = null;
                result.Count = 0;

                var descricao = $"Proposta de LC Adicional encerrada pelo usuário: {User.Identity.Name}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, propostaLCAdicional.ID);
                _appServiceLog.Create(logDto);
            }
            catch (ArgumentException arg)
            {
                result.Success = false;
                result.Errors = new[] { arg.Message };
                var error = new ErrorsYara();
                error.ErrorYara(arg as Exception);
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

        [HttpPost]
        [Route("v1/proposalstatus/sendComite")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaLimiteCreditoAdicional_Editar")]
        public async Task<GenericResult<PropostaLCComiteDto>> EnviadoComite(PropostaLCAdicionalComiteDto propostaLCAdicionalComite)
        {
            var result = new GenericResult<PropostaLCComiteDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                var proposta = await _appServicePropostaLCAdicional.GetAsync(c => c.ID.Equals(propostaLCAdicionalComite.PropostaLCAdicionalID));
                if (proposta == null)
                {
                    result.Success = false;
                    result.Errors = new[] { "Esta proposta não existe para esta empresa." };
                }

                propostaLCAdicionalComite.EmpresaID = empresaId;
                propostaLCAdicionalComite.UsuarioID = new Guid(userLogin);
                propostaLCAdicionalComite.DataCriacao = DateTime.Now;

                var retorno = await _appServicePropostaLCAdicionalComite.InsertAsync(propostaLCAdicionalComite, url);
                if (!retorno)
                {
                    result.Errors = new[] { "Ocorreu um erro ao tentar encaminhar o e-mail." };
                }

                result.Success = true;

                var descricao = $"Enviou a Proposta número {proposta.NumeroProposta} para o Status Em Aprovação com o CTC: {proposta.CodigoSap}";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                _appServiceLog.Create(logDto);
            }
            catch (ArgumentException arg)
            {
                result.Success = false;
                result.Errors = new[] { arg.Message };
                var error = new ErrorsYara();
                error.ErrorYara(arg as Exception);
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

        [HttpPost]
        [Route("v1/fixedLimitClientList")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "LimiteCreditoAdicional_FixarLimite")]
        public async Task<GenericResult<ContaClienteFinanceiroDto>> FixaLimiteCreditoList(List<ContaClienteFinanceiroDto> clienteFinanceiroDto)
        {
            var result = new GenericResult<ContaClienteFinanceiroDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                var idProposta = clienteFinanceiroDto.FirstOrDefault()?.PropostaLCAdicionalId;
                if (idProposta == null)
                {
                    result.Success = false;
                    result.Errors = new[] { "Nenhuma proposta foi enviada para fixação." };
                }

                var proposta = await _appServicePropostaLCAdicional.GetAsync(c => c.ID.Equals(idProposta.Value));
                if (proposta == null)
                {
                    result.Success = false;
                    result.Errors = new[] { "Esta proposta não existe para esta empresa." };
                }

                foreach (var financeiro in clienteFinanceiroDto)
                {
                    financeiro.EmpresasID = empresa;
                    financeiro.UsuarioIDCriacao = new Guid(userid);
                    financeiro.DataCriacao = DateTime.Now;

                    result.Success = await _appServicePropostaLCAdicional.FixLimitProposalAsync(financeiro, url);

                    var descricao = $"Fixou Limite de Credito para o Cliente de {financeiro.LC.Value.ToString("C")} para a proposta número: {proposta.NumeroProposta}.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, financeiro.PropostaLCId);
                    _appServiceLog.Create(logDto);
                }
            }
            catch (ArgumentException)
            {
                result.Success = false;
                result.Errors = new[] { "Usuário não autorizado." };
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

        #region Comitê

        [HttpGet]
        [Route("v1/comite/proposta/{propostaID:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaLimiteCreditoAdicional_Acesso")]
        public async Task<GenericResult<IEnumerable<PropostaLCAdicionalComiteDto>>> GetComiteProposta(Guid propostaID)
        {
            var result = new GenericResult<IEnumerable<PropostaLCAdicionalComiteDto>>();

            try
            {
                var comites = await _appServicePropostaLCAdicionalComite.GetAllFilterAsync(c => c.Ativo && c.PropostaLCAdicionalID.Equals(propostaID));

                if (comites.Any())
                {
                    foreach (var comite in comites)
                    {

                        switch (comite?.Perfil?.Descricao ?? "")
                        {
                            case "Analista de Crédito Jr": comite.Aprovadores = new List<string>() { "Analista de Crédito Pl", "Analista de Crédito Sr" }; break;
                            case "Analista de Crédito Pl": comite.Aprovadores = new List<string>() { "Analista de Crédito Sr" }; break;
                            case "Analista de Crédito Sr":
                            case "":
                            default: break;
                        }

                    }

                    result.Count = comites.Count();
                    result.Result = comites.OrderBy(c => c.Nivel).ThenBy(c => c.Round).ThenBy(c => c.DataCriacao);
                    result.Success = true;
                }
                else
                {
                    result.Count = 0;
                    result.Result = null;
                    result.Success = false;
                    result.Errors = new[] { "Nenhum Fluxo encontrado" };
                }
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

        [HttpGet]
        [Route("v1/comite/cockpit/propostas")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaLimiteCreditoAdicional_Acesso")]
        public async Task<GenericResult<IQueryable<PropostaLCAdicionalComiteDto>>> GetComiteProposta(ODataQueryOptions<PropostaLCAdicionalComiteDto> options)
        {
            var result = new GenericResult<IQueryable<PropostaLCAdicionalComiteDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var comites = await _appServicePropostaLCAdicionalComite.GetAllFilterAsync(c => c.Ativo && c.UsuarioID.Equals(new Guid(userid)) && c.PropostaLCAdicionalStatusComiteID.Equals("AP"));
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(comites.AsQueryable(), new ODataQuerySettings()).Cast<PropostaLCAdicionalComiteDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(comites.AsQueryable()).Cast<PropostaLCAdicionalComiteDto>();
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

        [HttpPost]
        [Route("v1/comite/adicionarNivel")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaLimiteCreditoAdicional_Aprovar")]
        public async Task<GenericResult<PropostaLCAdicionalComiteDto>> AdicionaNivel(PropostaLCAdicionalComiteDto comite)
        {
            var result = new GenericResult<PropostaLCAdicionalComiteDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaID = Request.Properties["Empresa"].ToString();

                comite.EmpresaID = empresaID;
                comite.UsuarioID = new Guid(userid);

                result.Success = await _appServicePropostaLCAdicionalComite.InsertNivel(comite);
                result.Count = 0;
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

        [HttpPost]
        [Route("v1/comite/atualizar")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaLimiteCreditoAdicional_Aprovar")]
        public async Task<GenericResult<PropostaLCAdicionalComiteDto>> Atualizar(PropostaLCAdicionalComiteDto comite)
        {
            var result = new GenericResult<PropostaLCAdicionalComiteDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaID = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                comite.EmpresaID = empresaID;
                comite.UsuarioID = new Guid(userid);

                var retorno = await _appServicePropostaLCAdicionalComite.UpdateValuePair(comite, url);
                if (!retorno.Key)
                {
                    result.Errors = new[] { retorno.Value };
                }
                result.Success = true;
                result.Count = 0;
            }
            catch (ArgumentException arg)
            {
                result.Success = false;
                result.Errors = new[] { arg.Message };
                var error = new ErrorsYara();
                error.ErrorYara(arg);
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

        #endregion
    }
}