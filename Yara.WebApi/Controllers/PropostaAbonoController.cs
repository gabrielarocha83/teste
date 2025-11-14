using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("allowance")]
    [Authorize]
    public class PropostaAbonoController : ApiController
    {
        private readonly IAppServicePropostaAbono _abono;
        private readonly IAppServiceLog _log;
        private readonly IAppServiceEstruturaPerfilUsuario _perfil;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="log"></param>
        /// <param name="abono"></param>
        /// <param name="perfil"></param>
        public PropostaAbonoController(IAppServiceLog log, IAppServicePropostaAbono abono, IAppServiceEstruturaPerfilUsuario perfil)
        {
            _abono = abono;
            _log = log;
            _perfil = perfil;
        }

        /// <summary>
        /// Método que retorna uma proposta de Abono por Código
        /// </summary>
        /// <param name="proposal">Código da Proposta</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/get/{proposal:guid}")]
        public async Task<GenericResult<PropostaAbonoDto>> GetProposal(Guid proposal)
        {
            var result = new GenericResult<PropostaAbonoDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var user = new Guid(userLogin);

                result.Result = await _abono.GetAsync(proposal, user);
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
        public async Task<GenericResult<IEnumerable<PropostaAbonoComiteDto>>> Get(Guid proposal)
        {
            var result = new GenericResult<IEnumerable<PropostaAbonoComiteDto>>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var user = new Guid(userLogin);

                IEnumerable<PropostaAbonoComiteDto> comite = new List<PropostaAbonoComiteDto>();

                comite = await _abono.GetCommitteeAsync(proposal);
                foreach (var item in comite)
                {
                    if (item.DataAcao == null && item.UsuarioID == user && item.PropostaAbono.ResponsavelID == user)
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
        /// Método que insere uma proposta de Abono Automática
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertauto")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Abono_Automatico")]
        public async Task<GenericResult<PropostaAbonoInserirDto>> InsertAuto(PropostaAbonoInserirDto proposta)
        {
            var result = new GenericResult<PropostaAbonoInserirDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                proposta.ID = Guid.NewGuid();
                proposta.EmpresaID = empresa;
                proposta.UsuarioIDCriacao = new Guid(userLogin);

                result.Success = await _abono.InsertAutoAsync(proposta);

                var descricao = $"Títulos abonados automaticamente pelo usuário: {User.Identity.Name}.";
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
        /// Método que cancela uma proposta de Abono Automática
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/cancel/{PropostaID:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Abono_Cancelar")]
        public async Task<GenericResult<PropostaAbonoInserirDto>> CancelAuto(Guid PropostaID)
        {
            var result = new GenericResult<PropostaAbonoInserirDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                result.Success = await _abono.CancelAsync(PropostaID,empresa, new Guid(userLogin), url);

                var descricao = $"Proposta de Abono cancelada pelo usuário: {User.Identity.Name}.";
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
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Abono_Aprovador")]
        public async Task<GenericResult<AprovaReprovaAbonoDto>> InsertAuto(AprovaReprovaAbonoDto proposta)
        {
            var result = new GenericResult<AprovaReprovaAbonoDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                proposta.EmpresaID = empresa;
                proposta.UsuarioID = new Guid(userLogin);

                result.Success = await _abono.AprovaReprovaAbono(proposta, url);

                var descricao = $"Ação do Aprovador de Proposta de Abono com o Usuário: {User.Identity.Name}";

                var level = EnumLogLevelDto.Proposta;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, proposta.PropostaAbonoID);
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
        /// Método que insere uma proposta de Abono 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insert")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Abono_Inserir")]
        public async Task<GenericResult<PropostaAbonoInserirDto>> Insert(PropostaAbonoInserirDto proposta)
        {
            var result = new GenericResult<PropostaAbonoInserirDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                proposta.EmpresaID = empresa;
                proposta.UsuarioIDCriacao = new Guid(userLogin);
                proposta.ID = Guid.NewGuid();
                result.Success = await _abono.InsertAsync(proposta);

                var descricao = $"Proposta de Abono criada pelo usuário: {User.Identity.Name}.";
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
        /// Método que atualiza uma proposta de Abono 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/save")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Abono_Inserir")]
        public async Task<GenericResult<PropostaAbonoDto>> Insert(PropostaAbonoDto proposta)
        {
            var result = new GenericResult<PropostaAbonoDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                proposta.EmpresaID = empresa;
                proposta.UsuarioIDAlteracao = new Guid(userLogin);

                result.Success = await _abono.UdpateAsync(proposta);

                var descricao = $"Proposta de Abono atualizada pelo usuário: {User.Identity.Name}.";
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
        /// Método que adicionar titulos a uma proposta de Abono 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/savepayments")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Abono_Inserir")]
        public async Task<GenericResult<PropostaAbonoDto>> InsertPayMents(PropostaAbonoInserirDto titulos)
        {
            var result = new GenericResult<PropostaAbonoDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                titulos.EmpresaID = empresa;
                titulos.UsuarioIDCriacao = new Guid(userLogin);

                result.Success = await _abono.InsertPaymentAsync(titulos);

                var descricao = $"Proposta de Abono adicionou novos titulos com o usuário: {User.Identity.Name}";

                var level = EnumLogLevelDto.Proposta;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, titulos.ID);
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
        /// Método que Envia para Cobranca
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/sendcollection")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Abono_Cobranca")]
        public async Task<GenericResult<PropostaAbonoDto>> SendCollection(PropostaAbonoDto titulos)
        {
            var result = new GenericResult<PropostaAbonoDto>();
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
                    result.Errors = new[]
                    {
                        $"Esta estrutura {titulos.CodigoSap}, não possuí um Analista de Cobrança, verifique suas configurações de Perfil x Usuário."
                    };
                    return result;
                }
                else
                {
                    titulos.ResponsavelID = responsavel;
                     result.Success = await _abono.SendCollection(titulos, url);

                    var abono = await _abono.GetAsync(titulos.ID, titulos.UsuarioIDAlteracao.Value);

                    var descricao = $"A Proposta de Abono {abono.NumeroProposta} foi enviada para o Analista de Cobrança através do usuário: {User.Identity.Name}";

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
        [Route("v1/sendcommittee")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Abono_CobrancaComite")]
        public async Task<GenericResult<PropostaAbonoDto>> SendCommittee(PropostaAbonoDto titulos)
        {
            var result = new GenericResult<PropostaAbonoDto>();
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
                    result.Errors = new[]
                    {
                        $"Esta estrutura {titulos.CodigoSap}, não possuí um Analista de Cobrança, verifique suas configurações de Perfil x Usuário."
                    };
                    return result;
                }
                else
                {
                    titulos.ResponsavelID = responsavel;
                    result.Success = await _abono.SendCommittee(titulos, url);

                    var abono = await _abono.GetAsync(titulos.ID, titulos.UsuarioIDCriacao);

                    var descricao = $"A Proposta de Abono {abono.NumeroProposta} foi enviada para aprovação do Comite através do usuário: {User.Identity.Name}";
                    var level = EnumLogLevelDto.Proposta;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, titulos.ID);
                    _log.Create(logDto);
                }
            }
            catch (ArgumentException arg)
            {
                result.Success = false;
                result.Errors = new[] { arg.Message};
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
    }
}
