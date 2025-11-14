using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MsgKit;
using MsgKit.Enums;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;
using MessageImportance = WebGrease.MessageImportance;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("juridical")]
    [Authorize]
    public class PropostaJuridicoController : ApiController
    {
        private readonly IAppServicePropostaJuridicoHistoricoPagamento _juridicoHistoricoPagamento;
        private readonly IAppServicePropostaJuridico _juridico;
        private readonly IAppServiceUsuario _usuario;
        private readonly IAppServiceEnvioEmail _email;
        private readonly IAppServiceAnexoArquivo _anexo;
        private readonly IAppServiceAnexoArquivoCobranca _anexoArquivoCobranca;
        private readonly IAppServiceLog _log;
        private readonly IAppServiceContaCliente _contaCliente;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="anexoArquivoCobranca"></param>
        /// <param name="anexo"></param>
        /// <param name="contaCliente"></param>
        /// <param name="juridicoHistoricoPagamento"></param>
        /// <param name="juridico"></param>
        /// <param name="usuario"></param>
        /// <param name="log"></param>
        /// <param name="email"></param>
        public PropostaJuridicoController(IAppServiceAnexoArquivoCobranca anexoArquivoCobranca, IAppServiceAnexoArquivo anexo, IAppServiceContaCliente contaCliente, IAppServicePropostaJuridicoHistoricoPagamento juridicoHistoricoPagamento, IAppServicePropostaJuridico juridico, IAppServiceUsuario usuario, IAppServiceLog log, IAppServiceEnvioEmail email)
        {
            _juridicoHistoricoPagamento = juridicoHistoricoPagamento;
            _juridico = juridico;
            _usuario = usuario;
            _log = log;
            _email = email;
            _anexo = anexo;
            _contaCliente = contaCliente;
            _anexoArquivoCobranca = anexoArquivoCobranca;
        }

        /// <summary>
        /// Metodo retorna historico de pagamento de titulos de uma proposta juridica em andamento
        /// </summary>
        /// <param name="propostaid"></param>
        /// <param name="clienteId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v2/getpayhistoric/{propostaid:guid}/{clienteId:guid}")]
        public async Task<GenericResult<IEnumerable<PropostaJuridicoHistoricoPagamentoDto>>> Get(Guid propostaid, Guid clienteId)
        {
            var result = new GenericResult<IEnumerable<PropostaJuridicoHistoricoPagamentoDto>>();
            try
            {
                var list = await _juridicoHistoricoPagamento.BuscaHistoricoPagamento(propostaid, clienteId);
                result.Result = list.OrderBy(c => c.DataPagamento);
                result.Count = result.Result.Count();

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
        /// Buscar Proposta Juridica por ID 
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getproposal/{id:guid}")]
        [ResponseType(typeof(PropostaJuridicoDto))]
        public async Task<GenericResult<PropostaJuridicoDto>> GetProposal(Guid id)
        {
            var result = new GenericResult<PropostaJuridicoDto>();

            try
            {
                var retorno = await _juridico.GetAsync(c => c.ID == id);

                if (retorno != null)
                {
                    result.Result = retorno;
                    result.Count = 1;
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

        /// <summary>
        /// Metodo para criar Proposta Juridico
        /// </summary>
        /// <param name="juridicoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertproposal")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaJuridico_Editar")]
        [ResponseType(typeof(PropostaJuridicoDto))]
        public async Task<GenericResult<PropostaJuridicoDto>> Post(PropostaJuridicoDto juridicoDto)
        {
            var result = new GenericResult<PropostaJuridicoDto>();
            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                juridicoDto.EmpresaID = Request.Properties["Empresa"].ToString();
                juridicoDto.UsuarioIDCriacao = new Guid(userid);


                var insert = await _juridico.CriaProposta((Guid)juridicoDto.ContaClienteID, juridicoDto.UsuarioIDCriacao, juridicoDto.EmpresaID);
                result.Success = !insert.ID.Equals(Guid.Empty);

                var user = await _usuario.GetAsync(c => c.ID.Equals(juridicoDto.UsuarioIDCriacao));
                var descricao = $"Foi criado nova proposta ao jurídico.";

                var level = EnumLogLevelDto.Proposta;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, insert.ID);
                _log.Create(logDto);

                result.Result = insert;
            }
            catch (ArgumentException)
            {
                result.Success = false;
                result.Errors = new[] { "Usuário não autorizado" };
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
        /// Metodo que salva uma proposta
        /// </summary>
        /// <param name="juridicoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/saveproposal")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaJuridico_Editar")]
        [ResponseType(typeof(PropostaJuridicoDto))]
        public async Task<GenericResult<PropostaJuridicoDto>> Save(PropostaJuridicoDto juridicoDto)
        {
            var result = new GenericResult<PropostaJuridicoDto>();
            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                juridicoDto.EmpresaID = Request.Properties["Empresa"].ToString();
                juridicoDto.UsuarioIDCriacao = new Guid(userid);

                result.Success = await _juridico.Update(juridicoDto);
                
                var user = await _usuario.GetAsync(c => c.ID.Equals(juridicoDto.UsuarioIDCriacao));
                var descricao = $"Usuario {user} salvou a proposta.";

                var level = EnumLogLevelDto.Proposta;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, juridicoDto.ID);
                _log.Create(logDto);
            }
            catch (ArgumentException)
            {
                result.Success = false;
                result.Errors = new[] { "Usuário não autorizado" };
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
        /// Api para encaminhar email.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/sendJuridico")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "PropostaJuridico_Editar")]
        public async Task<HttpResponseMessage> PostEnviaJuridico(PropostaJuridicoDto juridicoDto)
        {
            HttpResponseMessage result = null;

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                juridicoDto.EmpresaID = empresaId;
                juridicoDto.UsuarioIDCriacao = new Guid(userid);

                var cliente = await _contaCliente.GetAsync(c => c.ID == juridicoDto.ContaClienteID);
                var proposta = await _juridico.GetAsync(c => c.ID == juridicoDto.ID);
                var anexo = await _anexoArquivoCobranca.GetAllFilterAsync(c=>c.PropostaCobranca == proposta.ID && c.ContaClienteID == cliente.ID && c.Ativo);

                // Salvar a proposta com Historico de Pagamentos
                var save = await _juridico.UpdateWithHistoric(juridicoDto, cliente);
                
                // Configurações do e-mail
                var email = new Email(new Sender("portalcec@yarabrasil.com.br", "Portal C&C"), "Cliente Tranferido para o Jurídico"); // Isso deveria ser obtido lá nos parâmetros, assim como os demais e-mails, não?
                email.Subject = "Cliente Tranferido para o Jurídico";
                email.BodyHtml = await _email.SendMailPropostaJuridica(proposta, cliente, url);
                email.Importance = (MsgKit.Enums.MessageImportance)MessageImportance.High;
                email.IconIndex = MessageIconIndex.UnreadMail;

                // Anexando todos os arquivos da proposta
                if (anexo != null)
                {
                    foreach (var itemArquivo in anexo)
                    {
                        Stream streamCobranca = new MemoryStream(itemArquivo.Arquivo);
                        email.Attachments.Add(streamCobranca, itemArquivo.NomeArquivo);
                    }
                }

                // Anexando arquivo zipado contendo todos os arquivos da conta cliente
                var anexoZip = await _anexo.GetAccountZip(cliente.ID, proposta.EmpresaID);
                if (anexoZip != null)
                {
                    Stream streamContaCliente = new MemoryStream(anexoZip);
                    email.Attachments.Add(streamContaCliente, "DocumentosContaCliente.zip");
                }

                var ms = new MemoryStream();
                email.Save(ms);
                var bytes = ms.ToArray();

                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(bytes);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var descricao = $"Enviou Proposta para o Juridico e fez download do arquivo de email.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, juridicoDto.ID);
                _log.Create(logDto);
            }
            catch (ArgumentException e)
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

            return result;
        }

        /// <summary>
        /// Metodo retorna historico de titulos para o controle de cobrança
        /// </summary>
        /// <param name="diretoria"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcontrol/{diretoria}")]
        public async Task<GenericResult<IEnumerable<ControleCobrancaEnvioJuridicoDto>>> Get(string diretoria)
        {
            var result = new GenericResult<IEnumerable<ControleCobrancaEnvioJuridicoDto>>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();

                result.Result = await _juridico.BuscaControleCobranca(empresaId, diretoria);
                result.Count = result.Result.Count();
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
        /// Metodo para inativar uma proposta
        /// </summary>
        /// <param name="id">Guid da proposta</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactiveproposal/{id:guid}")]
        public async Task<GenericResult<PropostaJuridicoDto>> Delete(Guid id)
        {
            var result = new GenericResult<PropostaJuridicoDto>();
            LogDto logDto;
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _juridico.Inactive(id);

                var descricao = $" Usuario {userLogin} cancelou a proposta jurídica ID: {id}";
                var level = EnumLogLevelDto.Proposta;
                logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, id);
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
    }
}
