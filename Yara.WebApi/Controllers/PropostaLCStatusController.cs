using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("proposalstatus")]
    [Authorize]
    public class PropostaLCStatusController : ApiController
    {
        private readonly IAppServiceLog _appServiceLog;
        private readonly IAppServicePropostaLC _propostaLc;
        private readonly IAppServicePropostaLCComite _comite;
        private readonly IAppServiceUsuario _usuario;
        private readonly IAppServiceEstruturaPerfilUsuario _appServiceEstruturaPerfilUsuario;
        private readonly IAppServiceFluxoAlcadaAnalise _appServiceFluxoAlcadaAnalise;
        private readonly IAppServiceContaCliente _appServiceContaCliente;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServiceLog"></param>
        /// <param name="propostaLc"></param>
        /// <param name="comite"></param>
        /// <param name="usuario"></param>
        /// <param name="appServiceEstruturaPerfilUsuario"></param>
        /// <param name="appServiceFluxoAlcadaAnalise"></param>
        /// <param name="appServiceContaCliente"></param>
        public PropostaLCStatusController(IAppServiceLog appServiceLog, IAppServicePropostaLC propostaLc, IAppServicePropostaLCComite comite, IAppServiceUsuario usuario, IAppServiceEstruturaPerfilUsuario appServiceEstruturaPerfilUsuario, IAppServiceFluxoAlcadaAnalise appServiceFluxoAlcadaAnalise, IAppServiceContaCliente appServiceContaCliente)
        {
            _appServiceLog = appServiceLog;
            _propostaLc = propostaLc;
            _comite = comite;
            _usuario = usuario;
            _appServiceEstruturaPerfilUsuario = appServiceEstruturaPerfilUsuario;
            _appServiceFluxoAlcadaAnalise = appServiceFluxoAlcadaAnalise;
            _appServiceContaCliente = appServiceContaCliente;
        }

        /// <summary>
        /// Método que envia uma proposta para o CTC
        /// </summary>
        /// <param name="propostaLC"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/sendCTC")]
        public async Task<GenericResult<PropostaLCStatusDto>> EnviadoCtc(PropostaLCDto propostaLC)
        {
            var result = new GenericResult<PropostaLCStatusDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                var proposta = await _propostaLc.GetProposalByID(propostaLC.ID, empresaId);
                if (proposta == null)
                {
                    result.Success = false;
                    result.Errors = new[] { "Não existe está proposta para está empresa" };
                }
                else if (proposta.PropostaLCStatusID.Equals("XC") || proposta.PropostaLCStatusID.Equals("RP"))
                {
                    var responsavel = await _appServiceEstruturaPerfilUsuario.GetUserPerfil(propostaLC.CodigoSap, "Consultor Técnico Comercial");
                    if (responsavel.Equals(Guid.Empty))
                    {
                        result.Success = false;
                        result.Errors = new[] { "Este CTC não possuí um responsável, verifique as configurações." };
                    }
                    else
                    {
                        proposta.EmpresaID = empresaId;
                        proposta.PropostaLCStatusID = "CA";
                        proposta.UsuarioIDAlteracao = new Guid(userLogin);
                        proposta.CodigoSap = propostaLC.CodigoSap;
                        proposta.DataAlteracao = DateTime.Now;
                        proposta.ResponsavelID = responsavel;

                        result.Success = await _propostaLc.SaveStatus(proposta, "", url);

                        var descricao = $"Alterou a Proposta número {proposta.NumeroProposta} do Status Em Criação para Enviado ao CTC";
                        var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                        _appServiceLog.Create(logDto);
                    }
                }
                else
                {
                    result.Success = false;
                    result.Errors = new[] { "Está proposta não pode ser enviada ao CTC,pois, não se encontra no Status correto" };
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
        /// Método que envia uma proposta para Pré Analise
        /// </summary>
        /// <param name="propostaLC"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/sendpreAnalise")]
        public async Task<GenericResult<PropostaLCStatusDto>> EnviadoPreAnalise(PropostaLCDto propostaLC)
        {
            var result = new GenericResult<PropostaLCStatusDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();

                var proposta = await _propostaLc.GetProposalByID(propostaLC.ID, empresaId);
                if (proposta == null)
                {
                    result.Success = false;
                    result.Errors = new[]
                    {
                        "Não existe está proposta para está empresa"
                    };
                }
                else if (proposta.PropostaLCStatusID.Equals("RP") || proposta.PropostaLCStatusID.Equals("CA") || proposta.PropostaLCStatusID.Equals("CP") || proposta.PropostaLCStatusID.Equals("XC") || proposta.PropostaLCStatusID.Equals("FF"))
                {
                    var responsavel = await _appServiceEstruturaPerfilUsuario.GetUserPerfil(propostaLC.CodigoSap, "Assistente de Crédito");
                    if (responsavel.Equals(Guid.Empty))
                    {
                        result.Success = false;
                        result.Errors = new[]
                        {
                            $"O Assistente de Crédito para o código {proposta.CodigoSap} não possuí um responsável, verifique as configurações."
                        };
                    }
                    else
                    {
                        proposta.EmpresaID = empresaId;
                        proposta.PropostaLCStatusID = "FA";
                        proposta.UsuarioIDAlteracao = new Guid(userLogin);
                        proposta.DataAlteracao = DateTime.Now;
                        proposta.ResponsavelID = responsavel;
                        proposta.CodigoSap = propostaLC.CodigoSap;

                        result.Success = await _propostaLc.SaveStatusWithAnexo(proposta);

                        var descricao = $"Proposta número {proposta.NumeroProposta} enviado para Pré-Analise";
                        var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                        _appServiceLog.Create(logDto);
                    }
                }
                else
                {
                    result.Success = false;
                    result.Errors = new[]
                    {
                        "Está proposta não pode ser enviada a Pré Análise, pois, não se encontra no Status correto."
                    };
                }
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

            return result;
        }

        /// <summary>
        /// Método que envia uma proposta para Analise
        /// </summary>
        /// <param name="propostaLC"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/sendAnalise")]
        public async Task<GenericResult<PropostaLCStatusDto>> EnviadoAnalise(PropostaLCDto propostaLC)
        {
            var result = new GenericResult<PropostaLCStatusDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();

                var proposta = await _propostaLc.GetProposalByID(propostaLC.ID, empresaId);
                if (proposta == null)
                {
                    result.Success = false;
                    result.Errors = new[] { "Não existe está proposta para esta empresa" };
                }
                else if (proposta.PropostaLCStatusID.Equals("FP") || proposta.PropostaLCStatusID.Equals("FE") || proposta.PropostaLCStatusID.Equals("FC"))
                {
                    var cc = await _appServiceContaCliente.GetAsync(c => c.ID == proposta.ContaClienteID);

                    var perfilResponsavel = await _appServiceFluxoAlcadaAnalise.GetPerfilAtivoByValor(proposta.LCProposto, cc.SegmentoID);

                    var responsavel = await _appServiceEstruturaPerfilUsuario.GetUserPerfil(proposta.CodigoSap, perfilResponsavel);

                    if (string.IsNullOrEmpty(perfilResponsavel) || responsavel.Equals(Guid.Empty))
                    {
                        result.Success = false;
                        result.Errors = new[] { $"O segmento {cc.Segmento.Descricao} para o Fluxo de Alçada de Análise não possuí um responsável, verifique as configurações." };
                    }
                    else
                    {
                        proposta.EmpresaID = empresaId;
                        proposta.UsuarioIDAlteracao = new Guid(userLogin);
                        proposta.DataAlteracao = DateTime.Now;
                        proposta.ResponsavelID = responsavel;

                        string descricao;
                        if (proposta.PropostaLCStatusID.Equals("FC"))
                        {
                            result.Success = await _propostaLc.SaveStatusAbortaComite(proposta);
                            descricao = $"Alterou a Proposta número {proposta.NumeroProposta} e abortou o fluxo de aprovação do comitê.";
                        }
                        else
                        {
                            proposta.PropostaLCStatusID = "FE";
                            result.Success = await _propostaLc.SaveStatus(proposta, null, null);
                            descricao = $"Alterou a Proposta número {proposta.NumeroProposta} do Em Pré Analise para Enviado para Analise";
                        }
                        var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                        _appServiceLog.Create(logDto);
                    }
                }
                else
                {
                    result.Success = false;
                    result.Errors = new[] { "Está proposta não pode ser enviada para 'Enviado para Analise', pois não se encontra no Status de 'Pré Analise'." };
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
        /// Método que encerra uma proposta
        /// </summary>
        /// <param name="propostaLC"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/sendEncerrar")]
        public async Task<GenericResult<PropostaLCStatusDto>> EnviadoEncerrar(PropostaLCDto propostaLC)
        {
            var result = new GenericResult<PropostaLCStatusDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                var proposta = await _propostaLc.GetProposalByID(propostaLC.ID, empresaId);
                if (proposta == null)
                {
                    result.Success = false;
                    result.Errors = new[] { "Não existe está proposta para esta empresa" };
                }

                proposta.EmpresaID = empresaId;
                proposta.PropostaLCStatusID = "XE";
                proposta.UsuarioIDAlteracao = new Guid(userLogin);
                proposta.DataAlteracao = DateTime.Now;
                proposta.ResponsavelID = null;
                proposta.CodigoSap = null;

                result.Success = await _propostaLc.SaveStatus(proposta, "", url);

                var descricao = $"Alterou a Proposta número {proposta.NumeroProposta} para o Status Encerrado.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                _appServiceLog.Create(logDto);
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

            return result;
        }

        /// <summary>
        /// Método que envia uma proposta ao comite
        /// </summary>
        /// <param name="propostaLC"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/sendComite")]
        public async Task<GenericResult<PropostaLCComiteDto>> EnviadoComite(PropostaLCComiteDto propostaLC)
        {
            var result = new GenericResult<PropostaLCComiteDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                var proposta = await _propostaLc.GetProposalByID(propostaLC.PropostaLCID, empresaId);
                if (proposta == null)
                {
                    result.Success = false;
                    result.Errors = new[] { "Esta proposta não existe para esta empresa." };
                }

                propostaLC.UsuarioID = new Guid(userLogin);
                propostaLC.DataCriacao = DateTime.Now;
                propostaLC.EmpresaID = empresaId;

                var retorno = await _comite.InsertAsync(propostaLC, url);
                if (retorno)
                {
                    result.Errors = new[] {"Ocorreu um erro ao tentar encaminhar o e-mail."};
                }

                result.Success = true;

                var descricao = $"Enviou a Proposta número {proposta.NumeroProposta} para o Status Em Aprovação com o CTC: {propostaLC.CodigoSAP}";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                _appServiceLog.Create(logDto);
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

            return result;
        }

        /// <summary>
        /// Método para editar uma proposta mudando o Status
        /// </summary>
        /// <param name="propostaLC"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/edit")]
        public async Task<GenericResult<PropostaLCStatusDto>> Edit(PropostaLCDto propostaLC)
        {
            var result = new GenericResult<PropostaLCStatusDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();

                var proposta = await _propostaLc.GetProposalByID(propostaLC.ID, empresaId);
                proposta.PropostaLCStatusID = ChangeStatus(proposta.PropostaLCStatusID);
                proposta.ResponsavelID = new Guid(userLogin);

                result.Success = await _propostaLc.SaveStatus(proposta, null, null);

                var descricao = $"Editou a Proposta número {proposta.NumeroProposta}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                _appServiceLog.Create(logDto);
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
        /// Método que envia uma pendência ao CTC
        /// </summary>
        /// <param name="pendente"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/pendenciactc")]
        public async Task<GenericResult<PropostaLCStatusDto>> PendenciaCTC(SendPendencia pendente)
        {
            var result = new GenericResult<PropostaLCStatusDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                var proposta = await _propostaLc.GetProposalByID(pendente.ID, empresaId);
                if (proposta == null)
                {
                    result.Success = false;
                    result.Errors = new[] { "Não existe está proposta para esta empresa" };
                }

                proposta.PropostaLCStatusID = "CP";
                proposta.UsuarioIDAlteracao = new Guid(userLogin);

                // Regra antiga...
                //if (!string.IsNullOrEmpty(proposta.CodigoSap))
                //    pendente.CodigoSAP = proposta.CodigoSap;
                // Regra nova
                if (!string.IsNullOrEmpty(pendente.CodigoSAP))
                    proposta.CodigoSap = pendente.CodigoSAP;

                var responsavel = await _appServiceEstruturaPerfilUsuario.GetUserPerfil(pendente.CodigoSAP, "Consultor Técnico Comercial");
                if (responsavel.Equals(Guid.Empty))
                {
                    result.Success = false;
                    result.Errors = new[] { "Este CTC não possuí um responsável, verifique as configurações." };
                }
                else
                    proposta.ResponsavelID = responsavel;

                result.Success = await _propostaLc.SaveStatusWithPending(proposta, pendente.Mensagem, url);

                var descricao = $"Enviou pendência para o CTC: {proposta.CodigoSap}";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                _appServiceLog.Create(logDto);
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
        /// Método que encerra a proposta e fixa um limite
        /// </summary>
        /// <param name="pendente"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/encerraefixalimite")]
        public async Task<GenericResult<PropostaLCStatusDto>> EncerraeFixaLimite(SendPendencia pendente)
        {
            var result = new GenericResult<PropostaLCStatusDto>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                var proposta = await _propostaLc.GetProposalByID(pendente.ID, empresaId);
                if (proposta == null)
                {
                    result.Success = false;
                    result.Errors = new[] { "Não existe está proposta para esta empresa" };
                }

                proposta.PropostaLCStatusID = "CP";

                // Regra antiga...
                //if (!string.IsNullOrEmpty(proposta.CodigoSap))
                //    pendente.CodigoSAP = proposta.CodigoSap;
                // Regra nova
                if (!string.IsNullOrEmpty(pendente.CodigoSAP))
                    proposta.CodigoSap = pendente.CodigoSAP;

                var responsavel = await _appServiceEstruturaPerfilUsuario.GetUserPerfil(pendente.CodigoSAP, "Consultor Técnico Comercial");
                if (responsavel.Equals(Guid.Empty))
                {
                    result.Success = false;
                    result.Errors = new[] { "Este CTC não possuí um responsável, verifique as configurações." };
                }

                proposta.ParecerCTC = pendente.Mensagem;
                proposta.ResponsavelID = responsavel;
                proposta.UsuarioIDAlteracao = new Guid(userLogin);

                result.Success = await _propostaLc.SaveStatusWithPending(proposta, pendente.Mensagem, url);

                var descricao = $"Fixou Limite de Credito e Encerrou a proposta.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                _appServiceLog.Create(logDto);
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
        /// Método que envia uma pendência ao Representante
        /// </summary>
        /// <param name="pendente"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/pendenciarepresentante")]
        public async Task<GenericResult<PropostaLCStatusDto>> PendenciaRepresentante(SendPendencia pendente)
        {
            var result = new GenericResult<PropostaLCStatusDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                var proposta = await _propostaLc.GetProposalByID(pendente.ID, empresaId);
                if (proposta == null)
                {
                    result.Success = false;
                    result.Errors = new[] { "Não existe está proposta para esta empresa" };
                }

                proposta.PropostaLCStatusID = "RP";
                proposta.UsuarioIDAlteracao = new Guid(userLogin);

                // Regra antiga...
                //if (!string.IsNullOrEmpty(proposta.CodigoSap))
                //    pendente.CodigoSAP = proposta.CodigoSap;
                // Regra nova
                if (!string.IsNullOrEmpty(pendente.CodigoSAP))
                    proposta.CodigoSap = pendente.CodigoSAP;

                if (proposta.RepresentanteID.HasValue)
                    pendente.RepresentanteID = proposta.RepresentanteID;
                else
                    proposta.RepresentanteID = pendente.RepresentanteID;

                result.Success = await _propostaLc.SaveStatusWithRepresentante(proposta, pendente.Mensagem, url);

                var descricao = $"Enviou pendência para o Representante: {pendente.RepresentanteID}";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                _appServiceLog.Create(logDto);
            }
            catch (ArgumentException a)
            {
                result.Success = false;
                result.Errors = new[] { a.Message };
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
                var error = new ErrorsYara();
                error.ErrorYara(e);
            }

            return result;
        }

        /// <summary>
        /// Método que aprova a proposta com Outras Pendencias
        /// </summary>
        /// <param name="pendente"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/sendAprovarPendente")]
        public async Task<GenericResult<PropostaLCStatusDto>> AprovadoPendencias(SendPendencia pendente)
        {
            var result = new GenericResult<PropostaLCStatusDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                var proposta = await _propostaLc.GetProposalByID(pendente.ID, empresaId);
                if (proposta == null)
                {
                    result.Success = false;
                    result.Errors = new[] { "Não existe está proposta para esta empresa" };
                }

                proposta.PropostaLCStatusID = "AP";
                proposta.UsuarioIDAlteracao = new Guid(userLogin);

                if (!string.IsNullOrEmpty(proposta.CodigoSap))
                    pendente.CodigoSAP = proposta.CodigoSap;

                var responsavel = await _appServiceEstruturaPerfilUsuario.GetUserPerfil(pendente.CodigoSAP, "Consultor Técnico Comercial");
                if (responsavel.Equals(Guid.Empty))
                {
                    result.Success = false;
                    result.Errors = new[] { "Este CTC não possuí um responsável, verifique as configurações." };
                }
                else
                    proposta.ResponsavelID = responsavel;

                result.Success = await _propostaLc.SaveStatusWithPending(proposta, pendente.Mensagem, url);

                var descricao = $"Alterou o status da proposta: {proposta.NumeroProposta} para Aprovado com Pendência para o Consultor Técnico Comercial.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                _appServiceLog.Create(logDto);
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

            return result;
        }

        /// <summary>
        /// Método que aprova a proposta com Pendencia por Garantia
        /// </summary>
        /// <param name="pendente"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/sendAprovarGarantia")]
        public async Task<GenericResult<PropostaLCStatusDto>> AprovadoGarantias(SendPendencia pendente)
        {
            var result = new GenericResult<PropostaLCStatusDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                var proposta = await _propostaLc.GetProposalByID(pendente.ID, empresaId);
                if (proposta == null)
                {
                    result.Success = false;
                    result.Errors = new[] { "Não existe está proposta para esta empresa" };
                }

                proposta.PropostaLCStatusID = "AG";
                proposta.UsuarioIDAlteracao = new Guid(userLogin);

                if (!string.IsNullOrEmpty(proposta.CodigoSap))
                    pendente.CodigoSAP = proposta.CodigoSap;

                var responsavel = await _appServiceEstruturaPerfilUsuario.GetUserPerfil(pendente.CodigoSAP, "Consultor Técnico Comercial");
                if (responsavel.Equals(Guid.Empty))
                {
                    result.Success = false;
                    result.Errors = new[] { "Este CTC não possuí um responsável, verifique as configurações." };
                }
                else
                    proposta.ResponsavelID = responsavel;

                result.Success = await _propostaLc.SaveStatusWithPending(proposta, pendente.Mensagem, url);

                var descricao = $"Alterou o status da proposta: {proposta.NumeroProposta} para Aprovado aguardando garantia para o Consultor Técnico Comercial.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, proposta.ID);
                _appServiceLog.Create(logDto);
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

            return result;
        }

        #region Helpers

        /// <summary>
        /// Método que Troca o Status da proposta
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private static string ChangeStatus(string status)
        {
            var change = status;

            switch (status)
            {
                case "EC": // Enviado CTC
                    change = "CA"; // PARACER CTC
                    break;
                case "FA": // ENVIADO PRE ANALISE
                    change = "FP"; // EM PRE ANALISE
                    break;
                case "FE": // ENVIADO ANALISE
                    change = "FF"; // EM ANALISE
                    break;
            }

            return change;
        }

        #endregion
    }
}
