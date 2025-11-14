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
    [RoutePrefix("prolongation")]
    [Authorize]
    public class PropostaProrrogacaoController : ApiController
    {
        private readonly IAppServicePropostaProrrogacao _prorrogacao;
        private readonly IAppServiceEstruturaPerfilUsuario _perfil;
        private readonly IAppServiceLog _log;
        private readonly PropostaProrrogacaoInserirValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="log"></param>
        /// <param name="prorrogacao"></param>
        /// <param name="perfil"></param>
        public PropostaProrrogacaoController(IAppServiceLog log, IAppServicePropostaProrrogacao prorrogacao, IAppServiceEstruturaPerfilUsuario perfil)
        {
            _prorrogacao = prorrogacao;
            _log = log;
            _perfil = perfil;
            _validator = new PropostaProrrogacaoInserirValidator();
        }

        /// <summary>
        /// Método que retorna uma proposta de Abono por Código
        /// </summary>
        /// <param name="proposal">Código da Proposta</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/get/{proposal:guid}")]
        public async Task<GenericResult<PropostaProrrogacaoDto>> GetProposal(Guid proposal)
        {
            var result = new GenericResult<PropostaProrrogacaoDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var user = new Guid(userLogin);

                result.Result = await _prorrogacao.GetAsync(proposal, user);
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
        /// Método que retorna o comite da proposta de abono
        /// </summary>
        /// <param name="proposal">Código da Proposta</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/committee/{proposal:guid}")]
        public async Task<GenericResult<IEnumerable<PropostaProrrogacaoComiteDto>>> Get(Guid proposal)
        {
            var result = new GenericResult<IEnumerable<PropostaProrrogacaoComiteDto>>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var user = new Guid(userLogin);

                IEnumerable<PropostaProrrogacaoComiteDto> comite = new List<PropostaProrrogacaoComiteDto>();

                comite = await _prorrogacao.GetCommitteeAsync(proposal);
                foreach (var item in comite)
                {
                    if (item.DataAcao == null && item.UsuarioID == user && item.PropostaProrrogacao.ResponsavelID == user)
                    {
                        item.AprovadorLogado = true;
                        break;
                    }
                }

                result.Result = comite;
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
        /// Método que cancela uma proposta de Prorrogação
        /// </summary>
        /// <param name="PropostaID">Guid da proposta</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/cancel/{PropostaID:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Prorrogacao_Cancelar")]
        public async Task<GenericResult<PropostaProrrogacaoDto>> CancelAuto(Guid PropostaID)
        {
            var result = new GenericResult<PropostaProrrogacaoDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                result.Success = await _prorrogacao.CancelAsync(PropostaID, empresa, new Guid(userLogin), url);

                var descricao = $"Proposta de Prorrogação cancelada pelo usuário: {User.Identity.Name}.";
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
        /// Método que aprova ou reprova comite
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/aprovedisapprove")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Prorrogacao_Comite")]
        public async Task<GenericResult<AprovaReprovaProrrogacaoDto>> InsertAuto(AprovaReprovaProrrogacaoDto proposta)
        {
            var result = new GenericResult<AprovaReprovaProrrogacaoDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                proposta.EmpresaID = empresa;
                proposta.UsuarioID = new Guid(userLogin);

                result.Success = await _prorrogacao.AprovaReprovaProrrogacao(proposta, url);

                var descricao = $"Ação do Aprovador de Proposta de Prorrogação com o usuário: {User.Identity.Name}";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.PropostaProrrogacaoID);
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
        /// Método que insere uma proposta de Prorrogação 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insert")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Prorrogacao_Inserir")]
        public async Task<GenericResult<PropostaProrrogacaoInserirDto>> Insert(PropostaProrrogacaoInserirDto proposta)
        {
            var result = new GenericResult<PropostaProrrogacaoInserirDto>();
            var validation = _validator.Validate(proposta);

            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();

                    proposta.EmpresaID = empresa;
                    proposta.UsuarioIDCriacao = new Guid(userLogin);
                    proposta.ID = Guid.NewGuid();
                    result.Success = await _prorrogacao.InsertAsync(proposta);

                    var descricao = $"Proposta de Prorrogação criada pelo usuário: {User.Identity.Name}.";
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
            }
            else
                result.Errors = validation.GetErrors();

            return result;
        }

        /// <summary>
        /// Método que atualiza uma proposta de Abono 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/save")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Prorrogacao_Editar")]
        public async Task<GenericResult<PropostaProrrogacaoDto>> Insert(PropostaProrrogacaoDto proposta)
        {
            var result = new GenericResult<PropostaProrrogacaoDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                proposta.EmpresaID = empresa;
                proposta.UsuarioIDAlteracao = new Guid(userLogin);

                result.Success = await _prorrogacao.UdpateAsync(proposta);

                var descricao = $"Proposta de Prorrogação atualizada pelo usuário: {User.Identity.Name}.";
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
        /// Método que adicionar titulos a uma proposta de Prorrogação 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/savepayments")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Prorrogacao_Editar")]
        public async Task<GenericResult<PropostaProrrogacaoDto>> InsertPayMents(PropostaProrrogacaoInserirDto titulos)
        {
            var result = new GenericResult<PropostaProrrogacaoDto>();
            var validation = _validator.Validate(titulos);

            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();

                    titulos.EmpresaID = empresa;
                    titulos.UsuarioIDCriacao = new Guid(userLogin);

                    result.Success = await _prorrogacao.InsertPaymentAsync(titulos);

                    var descricao = $"Proposta de Prorrogação novos títulos adicionados pelo usuário: {User.Identity.Name}.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, titulos.ID);
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
                result.Errors = validation.GetErrors();

            return result;
        }

        /// <summary>
        /// Método que Envia para Cobranca
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/sendcollection")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Prorrogacao_Cobranca")]
        public async Task<GenericResult<PropostaProrrogacaoDto>> SendCollection(PropostaProrrogacaoDto titulos)
        {
            var result = new GenericResult<PropostaProrrogacaoDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                titulos.EmpresaID = empresa;
                titulos.UsuarioIDAlteracao = new Guid(userLogin);

                var responsavel = await _perfil.GetUserPerfil(titulos.CodigoSap, "Analista de Cobrança");

                if (responsavel.Equals(Guid.Empty))
                {
                    result.Success = false;
                    result.Errors = new[] { $"Esta estrutura {titulos.CodigoSap}, não possuí um Analista de Cobrança, verifique suas configurações de Perfil x Usuário." };
                    return result;
                }
                else
                {
                    titulos.ResponsavelID = responsavel;
                    result.Success = await _prorrogacao.SendCollection(titulos, url);

                    var proposta = await _prorrogacao.GetAsync(titulos.ID, titulos.UsuarioIDAlteracao.Value);

                    var descricao = $"A Proposta de Prorrogação {proposta.NumeroInternoProposta} foi enviada para o Analista de Cobrança através do usuário: {User.Identity.Name}.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, titulos.ID);
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
        [Route("v1/sendcommittee")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Prorrogacao_CobrancaComite")]
        public async Task<GenericResult<PropostaProrrogacaoDto>> SendCommittee(PropostaProrrogacaoDto titulos)
        {
            var result = new GenericResult<PropostaProrrogacaoDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                titulos.EmpresaID = empresa;
                titulos.UsuarioIDCriacao = new Guid(userLogin);

                var responsavel = await _perfil.GetUserPerfil(titulos.CodigoSap, "Analista de Cobrança");

                if (responsavel.Equals(Guid.Empty))
                {
                    result.Success = false;
                    result.Errors = new[] { $"Esta estrutura {titulos.CodigoSap}, não possuí um Analista de Cobrança, verifique suas configurações de Perfil x Usuário." };
                    return result;
                }
                else
                {
                    titulos.ResponsavelID = responsavel;
                    result.Success = await _prorrogacao.SendCommittee(titulos, url);

                    var prorrogacao = await _prorrogacao.GetAsync(titulos.ID, titulos.UsuarioIDCriacao);

                    var descricao = $"A Proposta de Prorrogação {prorrogacao.NumeroProposta} foi enviada para aprovação do Comite através do usuário: {User.Identity.Name}";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, titulos.ID);
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

            try
            {
                //var objuserLogin = User.Identity as ClaimsIdentity;
                //var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                result.Result = await _prorrogacao.BuscaDetalhesTitulos(proposal, empresa);

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
        /// Método que aprova a proposta prorrogação
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/aproveproposal")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Prorrogacao_Efetivar")]
        public async Task<GenericResult<PropostaProrrogacaoDto>> AproveProposal(PropostaProrrogacaoDto proposta)
        {
            var result = new GenericResult<PropostaProrrogacaoDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                proposta.EmpresaID = empresa;
                proposta.UsuarioIDCriacao = new Guid(userLogin);

                result.Success = await _prorrogacao.EfetivaProrrogacao(proposta, url);

                var descricao = $"Proposta de Prorrogação aprovada pelo usuário: {User.Identity.Name}.";
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
    }
}