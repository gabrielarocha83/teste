using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    /// <summary>
    /// CRUD para Proposta de LC
    /// </summary>
    [RoutePrefix("proposalLC")]
    [Authorize]
    public class PropostaLCController : ApiController
    {
        private readonly IAppServicePropostaLC _propostaLc;
        private readonly IAppServicePropostaLCDemonstrativo _propostaLcDemonstrativo;
        private readonly IAppServiceUsuario _usuario;
        private readonly IAppServiceLog _appServiceLog;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="propostaLc"></param>
        /// <param name="propostaLcDemonstrativo"></param>
        /// <param name="appServiceLog"></param>
        /// <param name="usuario"></param>
        public PropostaLCController(IAppServicePropostaLC propostaLc, IAppServicePropostaLCDemonstrativo propostaLcDemonstrativo, IAppServiceLog appServiceLog, IAppServiceUsuario usuario)
        {
            _propostaLc = propostaLc;
            _propostaLcDemonstrativo = propostaLcDemonstrativo;
            _appServiceLog = appServiceLog;
            _usuario = usuario;
        }

        /// <summary>
        /// Metodo de validação da Proposta de LC
        /// </summary>
        /// <param name="accountclienteID">Código da Conta Cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getproposalLCbyaccountclientvalidation/{accountclienteID:guid}")]
        public async Task<GenericResult<PropostaLCValidacaoDto>> GetAccountClientID(Guid accountclienteID)
        {
            var result = new GenericResult<PropostaLCValidacaoDto>();
            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaID = Request.Properties["Empresa"].ToString();

                result.Result = await _propostaLc.ValidaProposta(accountclienteID, new Guid(userid), empresaID);
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
        /// Metodo que salva proposta de LC
        /// </summary>
        /// <param name="propostaLC">Proposta de LC</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/saveproposalLC")]
        [ResponseType(typeof(PropostaLCDto))]
        public async Task<GenericResult<PropostaLCDto>> Post(PropostaLCDto propostaLC)
        {
            var result = new GenericResult<PropostaLCDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaID = Request.Properties["Empresa"].ToString();
                var criaProposta = false;

                propostaLC.EmpresaID = empresaID;

                if (propostaLC.ID == Guid.Empty)
                {
                    propostaLC.DataCriacao = DateTime.Now;
                    propostaLC.UsuarioIDCriacao = new Guid(userid);
                    propostaLC.ResponsavelID = new Guid(userid);
                    criaProposta = true;
                }
                else
                {
                    propostaLC.DataAlteracao = DateTime.Now;
                    propostaLC.UsuarioIDAlteracao = new Guid(userid);
                    propostaLC.ResponsavelID = new Guid(userid);
                }

                var insert = await _propostaLc.Save(propostaLC);

                result.Success = !insert.ID.Equals(Guid.Empty);

                string descricao;
                LogDto logDto;

                if (criaProposta)
                {
                    descricao = $"Criou nova Proposta de Limite de Crédito.";
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Proposta, propostaLC.ID);
                }
                else
                {
                    descricao = $"Salvou uma proposta de LC.";
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, propostaLC.ID);
                }

                _appServiceLog.Create(logDto);

                result.Result = insert;
            }
            catch (ArgumentException e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
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
        /// Buscar Proposta de Limite de Crédito pelo ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getproposalLC/{id:guid}")]
        [ResponseType(typeof(PropostaLCDto))]
        public async Task<GenericResult<PropostaLCDto>> Get(Guid id)
        {
            var result = new GenericResult<PropostaLCDto>();

            try
            {
                var empresaID = Request.Properties["Empresa"].ToString();
                result.Result = await _propostaLc.GetProposalByID(id, empresaID);
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
        /// Buscar Lista das Propostas de Limite de Crédito pelo ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getproposalLCList/{id:guid}")]
        //[ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "SolicitacaoPropostaLC_Acesso")]
        [ResponseType(typeof(PropostaLCDto))]
        public async Task<GenericResult<IEnumerable<PropostaLCDto>>> GetLista(Guid id)
        {
            var result = new GenericResult<IEnumerable<PropostaLCDto>>();

            try
            {
                result.Result = await _propostaLc.GetPropostalList(id);
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
        /// Buscar Proposta de Limite de Crédito pela Conta do Cliente
        /// </summary>
        /// <param name="accountclienteID">Código da contaCliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getproposalLCbyaccountclient/{accountclienteID:guid}")]
        public async Task<GenericResult<PropostaLCDto>> GetAccountClientIDAndUser(Guid accountclienteID)
        {
            var result = new GenericResult<PropostaLCDto>();

            try
            {
                var empresaID = Request.Properties["Empresa"].ToString();

                result.Result = await _propostaLc.GetProposalByAccountID(accountclienteID, empresaID);
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
        /// Busca propostas de uma conta cliente para Aba Propostas
        /// </summary>
        /// <param name="accountclienteID">Código da Conta Cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getproposalLCbyaccountclientpage/{accountclienteID:guid}")]
        public async Task<GenericResult<IQueryable<BuscaPropostaLCContaClienteDto>>> GetAccountClientIDPage(ODataQueryOptions<BuscaPropostaLCContaClienteDto> options, Guid accountclienteID)
        {
            var result = new GenericResult<IQueryable<BuscaPropostaLCContaClienteDto>>();

            try
            {
                var empresaID = Request.Properties["Empresa"].ToString();

                var propostas = await _propostaLc.GetPropostaLCContaCliente(accountclienteID, empresaID);

                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(propostas.AsQueryable(), new ODataQuerySettings()).Cast<BuscaPropostaLCContaClienteDto>();
                    totalReg = filtro.Count();
                }

                result.Result = options.ApplyTo(propostas.AsQueryable()).Cast<BuscaPropostaLCContaClienteDto>();
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
        /// Fixa limite de credito parcial aprovado pelo comitê ainda não finalizado.
        /// </summary>
        /// <param name="clienteFinanceiroDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "LimiteCredito_FixarLimite")]
        [Route("v1/fixedLimitClientPartial")]
        public async Task<GenericResult<ContaClienteFinanceiroDto>> FixaLimiteCreditoParcial(ContaClienteFinanceiroDto clienteFinanceiroDto)
        {
            var result = new GenericResult<ContaClienteFinanceiroDto>();
            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                clienteFinanceiroDto.EmpresasID = Request.Properties["Empresa"].ToString();
                clienteFinanceiroDto.UsuarioIDCriacao = new Guid(userid);
                clienteFinanceiroDto.DataCriacao = DateTime.Now;

                result.Success = await _propostaLc.LimitFixedPartial(clienteFinanceiroDto);
                var user =await _usuario.GetAsync(c => c.ID.Equals(clienteFinanceiroDto.UsuarioIDCriacao));
                var proposta = await _propostaLc.GetProposalByID(clienteFinanceiroDto.PropostaLCId, clienteFinanceiroDto.EmpresasID);

                var descricao = $"Usuario {user.Nome} fixou Limite de Credito para o Cliente Parcial de {clienteFinanceiroDto.LC.Value.ToString("C")} para a proposta número:{string.Format("LC{0:00000}/{1:yyyy}", proposta.NumeroProposta, proposta.DataCriacao)}.";
                var level = EnumLogLevelDto.Proposta;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, clienteFinanceiroDto.PropostaLCId);
                _appServiceLog.Create(logDto);
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
        /// Fixa uma lista de limite de credito parcial aprovado pelo comitê ainda não finalizado.
        /// </summary>
        /// <param name="clienteFinanceiroDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "LimiteCredito_FixarLimite")]
        [Route("v1/fixedLimitClientPartialList")]
        public async Task<GenericResult<ContaClienteFinanceiroDto>> FixaLimiteCreditoParcialList(List<ContaClienteFinanceiroDto> clienteFinanceiroDto)
        {
            var result = new GenericResult<ContaClienteFinanceiroDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                foreach (var financeiro in clienteFinanceiroDto)
                {
                    financeiro.EmpresasID = Request.Properties["Empresa"].ToString();
                    financeiro.UsuarioIDCriacao = new Guid(userid);
                    financeiro.DataCriacao = DateTime.Now;

                    result.Success = await _propostaLc.LimitFixedPartial(financeiro);
                    var user = await _usuario.GetAsync(c => c.ID.Equals(financeiro.UsuarioIDCriacao));
                    var proposta = await _propostaLc.GetProposalByID(financeiro.PropostaLCId, financeiro.EmpresasID);
                    var descricao = $"Usuario {user.Nome} fixou Limite de Credito para o Cliente Parcial de {financeiro.LC.Value.ToString("C")} para a proposta número:{string.Format("LC{0:00000}/{1:yyyy}", proposta.NumeroProposta, proposta.DataCriacao)}.";
                    var level = EnumLogLevelDto.Proposta;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, proposta.ID);
                    _appServiceLog.Create(logDto);
                }
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
        /// Fixa limite de credito Final aprovado pelo comitê finalizado.
        /// </summary>
        /// <param name="clienteFinanceiroDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/fixedLimitClient")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "LimiteCredito_FixarLimite")]
        public async Task<GenericResult<ContaClienteFinanceiroDto>> FixaLimiteCredito(ContaClienteFinanceiroDto clienteFinanceiroDto)
        {
            var result = new GenericResult<ContaClienteFinanceiroDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                clienteFinanceiroDto.EmpresasID = Request.Properties["Empresa"].ToString();
                clienteFinanceiroDto.UsuarioIDCriacao = new Guid(userid);
                clienteFinanceiroDto.DataCriacao = DateTime.Now;

                result.Success = await _propostaLc.LimitFixed(clienteFinanceiroDto, url);
                var user = await _usuario.GetAsync(c => c.ID.Equals(clienteFinanceiroDto.UsuarioIDCriacao));
                var proposta = await _propostaLc.GetProposalByID(clienteFinanceiroDto.PropostaLCId, clienteFinanceiroDto.EmpresasID);
                var descricao = $"Usuario {user.Nome} fixou Limite de Credito para o Cliente de {clienteFinanceiroDto.LC.Value.ToString("C")} para a proposta número: {proposta.NumeroProposta}.";
             
                var level = EnumLogLevelDto.Proposta;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, clienteFinanceiroDto.PropostaLCId);
                _appServiceLog.Create(logDto);
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
        /// Fixa uma lista de limite de credito Final aprovado pelo comitê finalizado.
        /// </summary>
        /// <param name="clienteFinanceiroDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/fixedLimitClientList")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "LimiteCredito_FixarLimite")]
        public async Task<GenericResult<ContaClienteFinanceiroDto>> FixaLimiteCreditoList(List<ContaClienteFinanceiroDto> clienteFinanceiroDto)
        {
            var result = new GenericResult<ContaClienteFinanceiroDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                foreach (var financeiro in clienteFinanceiroDto)
                {
                    financeiro.EmpresasID = Request.Properties["Empresa"].ToString();
                    financeiro.UsuarioIDCriacao = new Guid(userid);
                    financeiro.DataCriacao = DateTime.Now;

                    result.Success = await _propostaLc.LimitFixed(financeiro, url);
                    var user = await _usuario.GetAsync(c => c.ID.Equals(financeiro.UsuarioIDCriacao));
                    var proposta = await _propostaLc.GetProposalByID(financeiro.PropostaLCId, financeiro.EmpresasID);
                    var descricao = $"Usuario {user.Nome} fixou Limite de Credito para o Cliente de {financeiro.LC.Value.ToString("C")} para a proposta número: {proposta.NumeroProposta}.";
                    var level = EnumLogLevelDto.Proposta;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, financeiro.PropostaLCId);
                    _appServiceLog.Create(logDto);
                }
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
        /// Método que registra os demonstrativos financeiros PJ dos Grupos Economicos
        /// </summary>
        /// <param name="grupos">Lista de Grupos Economicos</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/savedatagroup")]
        public async Task<GenericResult<PropostaLCGrupoEconomicoDto>> SalvaGrupoDemonstrativo(List<PropostaLCGrupoEconomicoDto> grupos) { 

            var result = new GenericResult<PropostaLCGrupoEconomicoDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success =  await _propostaLcDemonstrativo.InsertGrupoDemonstrativo(grupos);
                var documentos = grupos.Select(c => c.Documento).ToArray();
                var propostaId = grupos.Select(c => c.PropostaLCID).FirstOrDefault();
                var descricao = $"Demonstrativo Financeiro anexado aos Documentos {string.Join(", ",documentos)}.";
                var level = EnumLogLevelDto.Proposta;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, propostaId);
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
        /// Metodo para Upload do arquivo de DRE PJ
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/uploaddrepj")]
        public GenericResult<PropostaLCDemonstrativoDto> Upload()
        {
            var result = new GenericResult<PropostaLCDemonstrativoDto>();

            try
            {

                var request = HttpContext.Current.Request;

                if (request.Files.Count <= 0)
                    throw new ArgumentException("Arquivo é obrigatório");

                var anexo = new PropostaLCDemonstrativoDto();

                var arquivo = request.Files[0];

                anexo.ID = Guid.NewGuid();
                anexo.Tipo = request.Form["Tipo"];
                anexo.NomeArquivo = arquivo.FileName;
                anexo.DataUpload = DateTime.Now;

                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(request.Files[0].InputStream))
                {
                    fileData = binaryReader.ReadBytes(request.Files[0].ContentLength);
                }
                anexo.Conteudo = fileData;

                // ESTE MÉTODO TEM EXCEPTION NO ARQUIVO DE LOG! ALGUM SUBSTRING ESTÁ GERANDO O SEGUINTE ERRO:
                // 2018-04-05 07:37:10,936 [36] ERROR YaraLog [(null)] – Erro ao ler o DRE PJ: Input string was not in a correct format..
                // 2018-04-05 07:37:23,477 [32] ERROR YaraLog [(null)] – Erro ao ler o DRE PJ: Input string was not in a correct format..
                // 2018-04-05 07:38:35,932 [30] ERROR YaraLog [(null)] – Erro ao ler o DRE PJ: Input string was not in a correct format..
                result.Success = _propostaLcDemonstrativo.Insert(ref anexo);

                if (result.Success)
                {
                    result.Result = anexo;
                    result.Result.Conteudo = null;
                }

                var descricao = $"Inseriu um novo DRE {request.Form["Tipo"]} na Proposat LC {anexo.NomeArquivo}";
                var level = EnumLogLevelDto.Proposta;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);

                _appServiceLog.Create(logDto);

            }
            catch (ArgumentException a)
            {
                result.Success = false;
                result.Errors = new[] { a.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(a.Message);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { Resources.Resources.Error };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;

        }
        
        /// <summary>
        /// Metodo para Upload do arquivo de DRE PF
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/uploaddrepf")]
        public GenericResult<PropostaLCDemonstrativoDto> UploadPF()
        {

            var result = new GenericResult<PropostaLCDemonstrativoDto>();

            try
            {

                var request = HttpContext.Current.Request;

                if (request.Files.Count <= 0)
                    throw new ArgumentException("Arquivo é obrigatório");

                var anexo = new PropostaLCDemonstrativoDto();

                var arquivo = request.Files[0];

                anexo.ID = Guid.NewGuid();
                anexo.Tipo = request.Form["Tipo"];
                anexo.NomeArquivo = arquivo.FileName;
                anexo.DataUpload = DateTime.Now;

                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(request.Files[0].InputStream))
                {
                    fileData = binaryReader.ReadBytes(request.Files[0].ContentLength);
                }
                anexo.Conteudo = fileData;

                result.Success = _propostaLcDemonstrativo.Insert(ref anexo);

                if (result.Success)
                {
                    result.Result = anexo;
                    result.Result.Conteudo = null;
                }

                var descricao = $"Inseriu um novo DRE {request.Form["Tipo"]} na Proposat LC {anexo.NomeArquivo}";
                var level = EnumLogLevelDto.Proposta;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);

                _appServiceLog.Create(logDto);

            }
            catch (ArgumentException a)
            {
                result.Success = false;
                result.Errors = new[] { a.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(a.Message);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { Resources.Resources.Error };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;

        }

        /// <summary>
        /// Método utilizado para buscar o Demonstrativo de Resultado por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getdre/{id:guid}")]
        public async Task<GenericResult<PropostaLCDemonstrativoDto>> GetDre(Guid id)
        {
            var result = new GenericResult<PropostaLCDemonstrativoDto>();
            try
            {

                result.Result = await _propostaLcDemonstrativo.GetAsync(d => d.ID.Equals(id));

                if (result.Result != null)
                    result.Count = 1;
                else
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
        /// Download DRE
        /// </summary>
        /// <param name="id">Código do Anexo</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/downloaddre/{id:guid}")]
        public async Task<HttpResponseMessage> DownDre(Guid id)
        {
            HttpResponseMessage result = null;
            try
            {
                var dre = await _propostaLcDemonstrativo.GetAsync(d => d.ID.Equals(id));
                result = Request.CreateResponse(HttpStatusCode.OK);

                result.Content = new ByteArrayContent(dre.Conteudo);

                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("proposalLC")
                {
                    FileName = dre.NomeArquivo
                };

                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var descricao = $"Baixou um documento {dre.NomeArquivo} da PropostaLC";
                var level = EnumLogLevelDto.Proposta;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
                _appServiceLog.Create(logDto);

            }
            catch (Exception e)
            {
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }
            return result;
        }

        /// <summary>
        /// Busca Grupo Economico do Cliente da Proposta de Limite de Credito
        /// </summary>
        /// <param name="grupoEconomicoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getGrupoPropostaLc/{grupoEconomicoId:guid}")]
        public async Task<GenericResult<IEnumerable<BuscaGrupoEconomicoPropostaLCDto>>> GetGrupoProposta(Guid grupoEconomicoId)
        {
            var result = new GenericResult<IEnumerable<BuscaGrupoEconomicoPropostaLCDto>>();
            try
            {
                result.Result = await _propostaLc.BuscaGrupoEconomicoPropostaLc(grupoEconomicoId);
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
        /// Método que retorna todos os patrimonios de uma garantia por documento
        /// </summary>
        /// <param name="document">Documento</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getpatrimoniodocument/{document}")]
        public async Task<GenericResult<PropostaLCPatrimoniosDto>> GetPatrimonios(string document)
        {
            var result = new GenericResult<PropostaLCPatrimoniosDto>();
            try
            {
                result.Result = await _propostaLc.GetPatrimonio(document);
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
        /// Método que retorna todas as receitas do cliente de acordo com documento
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getreceitadocument/{document}")]
        public async Task<GenericResult<PropostaLcTodasReceitasDto>> GetTodasReceitas(string document)
        {
            var result = new GenericResult<PropostaLcTodasReceitasDto>();
            try
            {
                result.Result = await _propostaLc.GetTodasReceitas(document);
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
    }
}
