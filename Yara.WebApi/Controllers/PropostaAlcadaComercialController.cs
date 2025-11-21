using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.Extensions;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591
#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("tradearea")]
    [Authorize]
    public class PropostaAlcadaComercialController : ApiController
    {
        private readonly IAppServicePropostaAlcadaComercial _alcada;
        private readonly IAppServiceLog _log;
        private readonly IAppServiceEstruturaPerfilUsuario _perfil;
        private readonly PropostaAlcadaComercialValidator _validator;
        private readonly IAppServiceProposta _proposta;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServiceEstruturaPerfilUsuario"></param>
        /// <param name="log"></param>
        /// <param name="alcada"></param>
        /// <param name="proposta"></param>
        public PropostaAlcadaComercialController(IAppServiceEstruturaPerfilUsuario appServiceEstruturaPerfilUsuario, IAppServiceLog log, IAppServicePropostaAlcadaComercial alcada, IAppServiceProposta proposta)
        {
            _alcada = alcada;
            _log = log;
            _perfil = appServiceEstruturaPerfilUsuario;
            _validator = new PropostaAlcadaComercialValidator();
            _proposta = proposta;
        }

        /// <summary>
        /// Método que retorna uma proposta de Alçada Comercial por Código
        /// </summary>
        /// <param name="PropostaID">Código da Proposta</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/get/{PropostaID:guid}")]
        public async Task<GenericResult<PropostaAlcadaComercialDto>> GetProposal(Guid PropostaID)
        {
            var result = new GenericResult<PropostaAlcadaComercialDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var user = new Guid(userLogin);

                result.Result = await _alcada.GetAsync(PropostaID, user);
                result.Count = 0;

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
        ///  Método que cancela uma proposta de Alçada Comercial
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/cancel/{PropostaID:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "AlcadaComl_Encerrar")]
        public async Task<GenericResult<PropostaAlcadaComercialDto>> Cancel(Guid PropostaID)
        {
            var result = new GenericResult<PropostaAlcadaComercialDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                result.Success = await _alcada.CancelAsync(PropostaID, new Guid(userLogin), url);

                var descricao = $"Proposta de Alçada Comercial cancelada pelo usuário: {User.Identity.Name}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, PropostaID);
                _log.Create(logDto);
            }
            catch (ArgumentException arg)
            {
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
        ///  Método que cancela uma proposta de Alçada Comercial
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/disapprove/{PropostaID:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "AlcadaComl_Rejeitar")]
        public async Task<GenericResult<PropostaAlcadaComercialDto>> Disapprove(Guid PropostaID)
        {
            var result = new GenericResult<PropostaAlcadaComercialDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                var proposta = new PropostaAlcadaComercialDto
                {
                    UsuarioIDAlteracao = new Guid(userLogin),
                    EmpresaID = empresa,
                    ID = PropostaID
                };

                result.Success = await _alcada.Disapprove(proposta, url);

                var descricao = $"Proposta de Alçada Comercial rejeitada pelo usuário: {User.Identity.Name}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                _log.Create(logDto);
            }
            catch (ArgumentException arg)
            {
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
        /// 
        /// </summary>
        /// <param name="restricoesDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/validateProposal")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "AlcadaComl_Criar")]
        public async Task<GenericResult<IEnumerable<PropostaAlcadaComercialRestricoesDto>>> ValidaSolicitacao(PropostaAlcadaComercialRestricoesDto restricoesDto)
        {
            var result = new GenericResult<IEnumerable<PropostaAlcadaComercialRestricoesDto>>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                var ret = await _alcada.BuscaRestricaoAlcada(restricoesDto.ContaClienteID, empresa);
                if (ret != null && ret.Any())
                {
                    var lstMensagem = new List<string>();
                    var descricao = "";

                    foreach (var item in ret)
                    {
                        descricao = descricao + item.Mensagem + "<br/>" + Environment.NewLine;
                        lstMensagem.Add(item.Mensagem);
                    }

                    var repre = await _perfil.GetActiveProfileAlcadaByCustomer(restricoesDto.ContaClienteID, new Guid(userLogin), empresa);
                    if (repre == "Representante" || repre == "Consultor Técnico Comercial")
                    {
                        result.Errors = new[] { "Cliente apresenta restrições que impossibilitam a criação da proposta. Em caso de dúvidas, contatar o Time de Crédito." };
                    }
                    else
                    {
                        result.Errors = lstMensagem.ToArray();
                    }

                    result.Success = false;
                }
                else
                {
                    result.Success = true;
                }
            }
            catch (Exception e)
            {
                result.Errors = new[] { Resources.Resources.Error };
                var error = new ErrorsYara();
                error.ErrorYara(e);
            }
            return result;
        }

        /// <summary>
        /// Método para editar uma proposta de alçada comercial
        /// </summary>
        /// <param name="proposta"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/edit")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "AlcadaComl_Editar")]
        public async Task<GenericResult<PropostaAlcadaComercialDto>> Edit(PropostaAlcadaComercialDto proposta)
        {
            var result = new GenericResult<PropostaAlcadaComercialDto>();
            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                proposta.EmpresaID = empresaId;
                proposta.ResponsavelID = new Guid(userLogin);
                proposta.DataAlteracao = DateTime.Now;
                proposta.UsuarioIDAlteracao = new Guid(userLogin);

                result.Success = await _alcada.UpdateOwner(proposta);

                var descricao = $"Proposta de Alçada Comercial editada pelo usuário: {User.Identity.Name}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
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
        /// Método que insere uma proposta de Alçada Comercial 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insert")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "AlcadaComl_Criar")]
        public async Task<GenericResult<PropostaAlcadaComercialDto>> Insert(PropostaAlcadaComercialDto proposta)
        {
            var result = new GenericResult<PropostaAlcadaComercialDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                proposta.ID = Guid.NewGuid();
                proposta.EmpresaID = empresa;
                proposta.UsuarioIDCriacao = new Guid(userLogin);

                var retorno = await _proposta.ExistePropostaEmAndamentoAsync(proposta.ContaClienteID, proposta.EmpresaID);
                if (!string.IsNullOrWhiteSpace(retorno))
                    throw new ArgumentException(retorno);

                result.Result = await _alcada.InsertAsync(proposta);
                result.Success = true;

                var descricao = $"Proposta de Alçada Comercial criada pelo usuário: {User.Identity.Name}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                _log.Create(logDto);
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

        /// <summary>
        /// Método que atualiza uma proposta de Alçada Comercial 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/save")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "AlcadaComl_Editar")]
        public async Task<GenericResult<PropostaAlcadaComercialDto>> Update(PropostaAlcadaComercialDto proposta)
        {
            var result = new GenericResult<PropostaAlcadaComercialDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                proposta.EmpresaID = empresa;
                proposta.UsuarioIDAlteracao = new Guid(userLogin);
                proposta.ResponsavelID = new Guid(userLogin);
                result.Success = await _alcada.UdpateAsync(proposta);

                var descricao = $"Proposta de Alçada Comercial atualizada pelo usuário: {User.Identity.Name}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                _log.Create(logDto);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { Resources.Resources.Error };
                var error = new ErrorsYara();
                error.ErrorYara(e);
                //}

            }
            return result;
        }

        /// <summary>
        /// Método que Envia para Cobranca
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/sendctc")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "AlcadaComl_EnviaCTC")]
        public async Task<GenericResult<PropostaAlcadaComercialDto>> SendCtc(PropostaAlcadaComercialDto titulos)
        {
            var result = new GenericResult<PropostaAlcadaComercialDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                titulos.EmpresaID = empresa;
                titulos.UsuarioIDAlteracao = new Guid(userLogin);
                var responsavel = await _perfil.GetUserPerfil(titulos.CodigoSap, "Consultor Técnico Comercial");

                if (responsavel.Equals(Guid.Empty))
                {
                    result.Success = false;
                    result.Errors = new[]
                    {
                        $"A estrutura {titulos.CodigoSap}, não possuí um Consultor Técnico Comercial, verifique suas configurações de Perfil x Usuário."
                    };
                    return result;
                }
                else
                {
                    titulos.ResponsavelID = responsavel;
                    result.Success = await _alcada.SendCtc(titulos, url);

                    var proposta = await _alcada.GetAsync(titulos.ID, titulos.UsuarioIDAlteracao.Value);

                    var descricao = $"A Proposta de Alçada Comercial {proposta.NumeroInternoProposta} foi enviada para o Consultor Técnico Comercial através do usuário: {User.Identity.Name}";

                    var level = EnumLogLevelDto.Proposta;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, titulos.ID);
                    _log.Create(logDto);
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

        /// <summary>
        /// Método que Envia para Comite
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/sendanalysis")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "AlcadaComl_SolicitarAprovacao")]
        public async Task<GenericResult<PropostaAlcadaComercialDto>> SendAnalysis(PropostaAlcadaComercialDto proposta)
        {
            var result = new GenericResult<PropostaAlcadaComercialDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                proposta.EmpresaID = empresa;
                proposta.UsuarioIDAlteracao = new Guid(userLogin);
                var responsavel = await _perfil.GetUserPerfil(proposta.CodigoSap, "Assistente de Crédito");

                if (responsavel.Equals(Guid.Empty))
                {
                    result.Success = false;
                    result.Errors = new[]
                    {
                        $"Esta estrutura {proposta.CodigoSap}, não possuí um Assistente de Crédito, verifique suas configurações de Perfil x Usuário."
                    };
                    return result;
                }
                else
                {
                    proposta.ResponsavelID = responsavel;
                    result.Success = await _alcada.SendAnalysis(proposta, url);
                    var alcada = await _alcada.GetAsync(proposta.ID, proposta.UsuarioIDCriacao);
                    var descricao = $"A Proposta de Alçada Comercial {alcada.NumeroProposta} foi enviada para o Crédito pelo usuário: {User.Identity.Name}";
                    var level = EnumLogLevelDto.Proposta;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, proposta.ID);
                    _log.Create(logDto);
                }

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

        /// <summary>
        /// Método que retorna o detalhe dos titulos de prorrogação
        /// </summary>
        /// <param name="proposal">Código da Proposta</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/details/{proposal:guid}")]
        public async Task<GenericResult<IEnumerable<BuscaDetalhesPropostaProrrogacaoTituloDto>>> GetDetalhes(Guid proposal)
        {
            var result = new GenericResult<IEnumerable<BuscaDetalhesPropostaProrrogacaoTituloDto>>();
            //try
            //{
            //    var empresa = Request.Properties["Empresa"].ToString();
            //    result.Result = await _prorrogacao.BuscaDetalhesTitulos(proposal, empresa);
            //    result.Count = 0;

            //    result.Success = true;
            //}

            //catch (Exception e)
            //{
            //    result.Success = false;
            //    result.Errors = new[] { Resources.Resources.Error };
            //    var error = new ErrorsYara();
            //    error.ErrorYara(e);
            //}
            return result;
        }

        /// <summary>
        /// Método que aprova a proposta de alçada comercial
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/aproveproposal")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "AlcadaComl_Fixar")]
        public async Task<GenericResult<PropostaAlcadaComercialDto>> AproveProposal(PropostaAlcadaComercialDto proposta)
        {
            var result = new GenericResult<PropostaAlcadaComercialDto>();
            try
            {
                var validation = _validator.Validate(proposta);
                if (validation.IsValid)
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();
                    var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                    proposta.EmpresaID = empresa;
                    proposta.ResponsavelID = new Guid(userLogin);
                    proposta.UsuarioIDCriacao = new Guid(userLogin);
                    result.Success = await _alcada.AprovaAlcadaComercial(proposta, url);

                    var descricao = $"Proposta de Alçada Comercial aprovada pelo Usuário: {User.Identity.Name}";

                    var level = EnumLogLevelDto.Proposta;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, proposta.ID);
                    _log.Create(logDto);
                }
                else
                {
                    result.Errors = validation.GetErrors();
                }
            }
            catch (ArgumentException arg)
            {
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
        /// Método de aprovação da Solicitação do CTC
        /// </summary>
        /// <param name="proposta">DadosProposta</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/aprovectc")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "AlcadaComl_SolicitarAprovacao")]
        public async Task<GenericResult<PropostaAlcadaComercialDto>> AproveProposalCTC(PropostaAlcadaComercialDto proposta)
        {
            var result = new GenericResult<PropostaAlcadaComercialDto>();
            try
            {
                var validation = _validator.Validate(proposta);
                if (validation.IsValid)
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();
                    var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                    proposta.EmpresaID = empresa;
                    proposta.ResponsavelID = new Guid(userLogin);
                    proposta.UsuarioIDCriacao = new Guid(userLogin);
                    result.Success = await _alcada.AprovaAlcadaComercial(proposta, url);

                    var descricao = $"Proposta de Alçada Comercial aprovada pelo Usuário: {User.Identity.Name}";

                    var level = EnumLogLevelDto.Proposta;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, proposta.ID);
                    _log.Create(logDto);
                }
                else
                {
                    result.Errors = validation.GetErrors();
                }
            }
            catch (ArgumentException arg)
            {
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
    }
}
