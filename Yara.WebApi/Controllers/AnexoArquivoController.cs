using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

namespace Yara.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("attachment")]
    [Authorize]
    public class AnexoArquivoController : ApiController
    {
        private readonly IAppServiceAnexoArquivo _anexo;
        private readonly IAppServiceAnexo _appServiceAnexo;
        private readonly IAppServiceUsuario _usuario;
        private readonly IAppServiceContaCliente _contaCliente;
        private readonly IAppServicePropostaLC _propostaLC;
        private readonly IAppServicePropostaLCAdicional _propostaLCAdicional;
        private readonly IAppServiceAnexoArquivoCobranca _anexoArquivoCobranca;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="anexoArquivoCobranca"></param>
        /// <param name="anexo"></param>
        /// <param name="appServiceLog"></param>
        /// <param name="usuario"></param>
        /// <param name="propostaLC"></param>
        /// <param name="appServiceAnexo"></param>
        public AnexoArquivoController(IAppServiceAnexoArquivoCobranca anexoArquivoCobranca, IAppServiceAnexoArquivo anexo, IAppServiceLog appServiceLog, IAppServiceUsuario usuario, IAppServicePropostaLC propostaLC, IAppServiceAnexo appServiceAnexo, IAppServiceContaCliente cliente, IAppServicePropostaLCAdicional propostaLCAdicional)
        {
            _anexo = anexo;
            _log = appServiceLog;
            _usuario = usuario;
            _contaCliente = cliente;
            _propostaLC = propostaLC;
            _propostaLCAdicional = propostaLCAdicional;
            _appServiceAnexo = appServiceAnexo;
            _anexoArquivoCobranca = anexoArquivoCobranca;
            //_validator = new AnexoValidator();
        }

        #region Conta Cliente & PropostaLC

        /// <summary>
        /// Metodo para Upload do arquivo de anexo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/uploadattachment")]
        public async Task<GenericResult<AnexoArquivoDto>> Post()
        {
            var result = new GenericResult<AnexoArquivoDto>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                var request = HttpContext.Current.Request;

                if (request.Files.Count <= 0)
                    throw new ArgumentException("Arquivo é obrigatório");

                var propostaLCRequest = request.Form["PropostaLCID"];
                var propostaLCAdicionalRequest = request.Form["PropostaLCAdicionalID"];
                var contaClienteRequest = request.Form["ContaClienteID"];
                var anexoRequest = request.Form["AnexoID"];
                var arquivo = request.Files["Arquivo"];
                var descricaoAnexoRequest = request.Form["DescricaoAnexo"];
                var categoriaConverted = int.TryParse(request.Form["CategoriaDocumento"], out int categoriaDocumentoRequest);
                var complementoAnexoRequest = request.Form["Complemento"];
                    
                    
                var anexo = new AnexoArquivoDto();
                anexo.EmpresaID = empresaId;
                anexo.DataCriacao = DateTime.Now;
                anexo.UsuarioIDCriacao = new Guid(userLogin);

                if (!string.IsNullOrEmpty(anexoRequest))
                {
                    anexo.AnexoID = new Guid(anexoRequest);
                }
                else if (string.IsNullOrEmpty(anexoRequest) && !string.IsNullOrEmpty(descricaoAnexoRequest) && categoriaConverted)
                {
                    var anexoDto = await _appServiceAnexo.GetAsync(c => c.Ativo && c.Descricao.Contains(descricaoAnexoRequest) && c.CategoriaDocumento == categoriaDocumentoRequest); // Confirmar se a busca é por descrição e categoria / descrição ou categoria e qual é a prioridade
                    if (anexoDto == null)
                    {
                        throw new ArgumentException("A descrição e categoria informadas não foram encontradas no cadastro de Anexos e Obrigatoriedade.");
                    }
                    
                    anexo.AnexoID = anexoDto.ID;
                }
                else
                {
                    throw new ArgumentException("Nenhuma informação foi fornecida para a busca do Anexo.");
                }

                anexo.PropostaLCID = string.IsNullOrEmpty(propostaLCRequest) ? (Guid?)null : new Guid(propostaLCRequest);
                anexo.PropostaLCAdicionalID = string.IsNullOrEmpty(propostaLCAdicionalRequest) ? (Guid?)null : new Guid(propostaLCAdicionalRequest);
                anexo.ContaClienteID = string.IsNullOrEmpty(contaClienteRequest) ? (Guid?)null : new Guid(contaClienteRequest);
                anexo.ExtensaoArquivo = Path.GetExtension(arquivo.FileName);
                anexo.Complemento = complementoAnexoRequest;

                var resultado = Regex.Replace(Path.GetFileNameWithoutExtension(arquivo.FileName).ToLower(), "[áàãâä]+", "a");
                resultado = Regex.Replace(resultado, "[éèêë]+", "e");
                resultado = Regex.Replace(resultado, "[íìîï]+", "i");
                resultado = Regex.Replace(resultado, "[óòõôö]+", "o");
                resultado = Regex.Replace(resultado, "[úùûü]+", "u");
                resultado = Regex.Replace(resultado, "[ç]+", "c");
                resultado = Regex.Replace(resultado, "[^a-z0-9]+", "_");

                anexo.NomeArquivo = $"{resultado}{anexo.ExtensaoArquivo}";

                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(request.Files[0].InputStream))
                {
                    fileData = binaryReader.ReadBytes(request.Files[0].ContentLength);
                }

                anexo.Arquivo = fileData;

                Guid transacao;
                ContaClienteDto cliente;
                if (anexo.ContaClienteID == null && anexo.PropostaLCID != null)
                {
                    var proposta = await _propostaLC.GetProposalByID((Guid)anexo.PropostaLCID, empresaId);
                    cliente = await _contaCliente.GetAsync(c => c.ID.Equals(proposta.ContaClienteID));
                    transacao = proposta.ID;
                }
                else if (anexo.ContaClienteID == null && anexo.PropostaLCAdicionalID != null)
                {
                    var proposta = await _propostaLCAdicional.GetAsync(c => c.ID.Equals((Guid)anexo.PropostaLCAdicionalID) && c.EmpresaID.Equals(empresaId));
                    cliente = await _contaCliente.GetAsync(c => c.ID.Equals(proposta.ContaClienteID));
                    transacao = proposta.ID;
                }
                else
                {
                    cliente = await _contaCliente.GetAsync(c => c.ID.Equals((Guid)anexo.ContaClienteID));
                    transacao = (Guid)anexo.ContaClienteID;
                }

                result.Success = await _anexo.InsertAsync(anexo);

                var descricao = $"O usuário: {User.Identity.Name} adicionou o documento {anexo.NomeArquivo} para o cliente {cliente.Nome}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, transacao);
                _log.Create(logDto);
            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { ex.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(ex.Message);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;
        }

        /// <summary>
        /// Metodo para Download do arquivo de anexo
        /// </summary>
        /// <param name="id">Código do AnexoArquivo</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/downloadattachment/{id:guid}")]
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            HttpResponseMessage result = null;

            try
            {
                var anexo = await _anexo.GetAsync(c => c.Ativo && c.ID == id);

                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(anexo.Arquivo);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = anexo.NomeArquivo
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var descricao = $"Baixou o documento {anexo.NomeArquivo}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
                _log.Create(logDto);
            }
            catch (Exception e)
            {
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;
        }

        /// <summary>
        /// Metodo para Download de um Zip de Todos os Anexos
        /// </summary>
        /// <param name="id">ID da Conta Cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/downloadzip/{id:guid}")]
        public async Task<HttpResponseMessage> GetZip(Guid id)
        {
            HttpResponseMessage result = null;

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();
                var zipFile = await _anexo.GetAccountZip(id, empresaId);

                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(zipFile);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "anexos.zip"
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var descricao = $"Baixou todos os anexos em formato comprimido (ZIP).";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
                _log.Create(logDto);
            }
            catch (Exception e)
            {
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;
        }

        /// <summary>
        /// Metodo para Inativar um Anexo Arquivo
        /// </summary>
        /// <param name="id">Código do AnexoArquivo</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactiveattachment/{id:guid}")]
        public async Task<GenericResult<AnexoArquivoDto>> Delete(Guid id)
        {
            var result = new GenericResult<AnexoArquivoDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                var Anexo = new AnexoArquivoDto
                {
                    ID = id,
                    Ativo = false,
                    DataAlteracao = DateTime.Now,
                    UsuarioIDAlteracao = new Guid(userLogin),
                };

                result.Success = await _anexo.Update(Anexo);

                var descricao = $"Removeu o documento {Anexo.NomeArquivo}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
                _log.Create(logDto);
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
        /// Metodo que retorna os anexos por código de proposta somente ativas
        /// </summary>
        /// <param name="id">Código do Formulário de Proposta</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getlistattachmentbyproposalid/{id:guid}")]
        public async Task<GenericResult<IEnumerable<AnexoArquivoDto>>> GetProposal(Guid id)
        {
            var result = new GenericResult<IEnumerable<AnexoArquivoDto>>();

            try
            {
                result.Result = await _anexo.CustomGetAllFilterAsync(c => (c.PropostaLCID == id || c.PropostaLCAdicionalID == id) && c.Ativo);
                result.Count = result.Result.Count();
                result.Success = true;
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
        /// Metodo que busca todos os anexos do cliente
        /// </summary>
        /// <param name="id">Código do Anexo</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getattachmentbyclient/{id:guid}")]
        public async Task<GenericResult<IEnumerable<AnexoArquivoDto>>> GetAttachmentClient(ODataQueryOptions<AnexoArquivoDto> options, Guid id)
        {
            var result = new GenericResult<IEnumerable<AnexoArquivoDto>>();

            try
            {
                var anexo = await _anexo.CustomGetAllFilterAsync(c => c.ContaClienteID == id && c.Ativo);
                if (anexo != null)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(anexo.AsQueryable(), new ODataQuerySettings()).Cast<AnexoArquivoDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(anexo.AsQueryable()).Cast<AnexoArquivoDto>();
                    result.Count = anexo.Count();
                }
                result.Success = true;
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
        /// Metodo que busca todos os anexos do cliente pelo id da proposta de lc
        /// </summary>
        /// <param name="id">ID da Proposta de Limite de Credito</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getattachmentbyproposal/{id:guid}")]
        public async Task<GenericResult<IEnumerable<AnexoArquivoDto>>> GetAttachmentClientProposal(Guid id)
        {
            var result = new GenericResult<IEnumerable<AnexoArquivoDto>>();

            try
            {
                result.Result = await _anexo.CustomGetAllFilterAsync(c => (c.PropostaLCID == id || c.PropostaLCAdicionalID == id) && c.Ativo);
                result.Count = result.Result.Count();
                result.Success = true;
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
        /// Metodo para alterar dados do anexo na conta cliente
        /// </summary>
        /// <param name="anexoArquivo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/updateattachment")]
        public async Task<GenericResult<AnexoArquivoDto>> PostAttachment(List<AnexoArquivoDto> anexoArquivo)
        {
            var result = new GenericResult<AnexoArquivoDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                result.Success = await _anexo.UpdateList(anexoArquivo, new Guid(userLogin));

                // Busca nome do usuario para logar.
                // var user = await _usuario.GetAsync(c => c.ID == new Guid(userLogin));

                foreach (var itemAnexo in anexoArquivo)
                {
                    var descricao = $"O usuário: {User.Identity.Name} alterou o documento {itemAnexo.NomeArquivo} para o status {itemAnexo.Status} com validade até {itemAnexo.DataValidade}.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, itemAnexo.ContaClienteID);
                    _log.Create(logDto);
                }
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

        #endregion

        #region Cobrança

        /// <summary>
        /// Metodo para Upload dos arquivos de proposta de cobrança
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/chargesuploadattachment")]
        public async Task<GenericResult<AnexoArquivoCobrancaDto>> PostCobranca()
        {
            var result = new GenericResult<AnexoArquivoCobrancaDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();

                var anexoDto = new AnexoArquivoCobrancaDto();
                var request = HttpContext.Current.Request;

                if (request.Files.Count <= 0)
                    throw new ArgumentException("Arquivo é obrigatório");

                var arquivo = request.Files["Arquivo"];
                var propostaRequest = request.Form["Proposta"];
                var contaCliente = request.Form["ContaClienteID"];
                var tipoProposta = request.Form["TipoProposta"];
                var descricaoArquivo = request.Form["Descricao"];

                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(request.Files[0].InputStream))
                {
                    fileData = binaryReader.ReadBytes(request.Files[0].ContentLength);
                }
                anexoDto.PropostaCobranca = new Guid(propostaRequest);
                anexoDto.ContaClienteID = new Guid(contaCliente);
                anexoDto.TipoProposta = Convert.ToInt32(tipoProposta);
                anexoDto.Arquivo = fileData;
                anexoDto.Descricao = descricaoArquivo;
                anexoDto.NomeArquivo = arquivo.FileName;
                anexoDto.ExtensaoArquivo = arquivo.FileName.Split('.')[1];
                anexoDto.UsuarioIDCriacao = new Guid(userLogin);
                anexoDto.DataCriacao = DateTime.Now;

                result.Success = await _anexoArquivoCobranca.InsertAsync(anexoDto, empresaId);

                // Busca nome do usuario para logar.
                // var user = await _usuario.GetAsync(c => c.ID == new Guid(userLogin));

                var descricao = $"O usuário: {User.Identity.Name} adicionou o documento {anexoDto.NomeArquivo} para o cliente {anexoDto.ContaClienteID}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, anexoDto.ContaClienteID);
                _log.Create(logDto);
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
        /// Metodo para Download dos arquivos de proposta de cobrança
        /// </summary>
        /// <param name="id">Código do Anexo</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/chargesdownloadattachment/{id:guid}")]
        public async Task<HttpResponseMessage> GetCobranca(Guid id)
        {
            HttpResponseMessage result = null;

            try
            {
                var anexo = await _anexoArquivoCobranca.GetAsync(c => c.ID.Equals(id));

                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(anexo.Arquivo);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = anexo.NomeArquivo
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var descricao = $"Baixou o documento {anexo.NomeArquivo}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, anexo.ContaClienteID);
                _log.Create(logDto);
            }
            catch (Exception e)
            {
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }
            return result;
        }

        /// <summary>
        /// Metodo para Inativar os arquivos de proposta de cobrança
        /// </summary>
        /// <param name="id">Código do Anexo</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactiveproposalattachment/{id:guid}")]
        public async Task<GenericResult<AnexoArquivoCobrancaDto>> DeleteCobranca(Guid id)
        {
            var result = new GenericResult<AnexoArquivoCobrancaDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                var anexo = new AnexoArquivoCobrancaDto
                {
                    ID = id,
                    Ativo = false,
                    DataAlteracao = DateTime.Now,
                    UsuarioIDAlteracao = new Guid(userLogin)
                };

                result.Success = await _anexoArquivoCobranca.Update(anexo);

                var descricao = $"Removeu o documento {anexo.NomeArquivo}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
                _log.Create(logDto);
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
        /// Metodo que retorna os anexos de cobrança de acordo com id proposta, tipo da proposta e conta cliente id
        /// </summary>
        /// <param name="propostaid"></param>
        /// <param name="contaclienteid"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getlistattachment/{propostaid:guid}/{tipo}")]
        public async Task<GenericResult<IEnumerable<AnexoArquivoCobrancaWithOutFileDto>>> GetPropostaCobranca(Guid propostaid, int tipo)
        {
            var result = new GenericResult<IEnumerable<AnexoArquivoCobrancaWithOutFileDto>>();

            try
            {
                result.Result = await _anexoArquivoCobranca.GetAllFilterWithOutFileAsync(propostaid, tipo);
                result.Count = result.Result.Count();
                result.Success = true;
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

        #endregion
    }
}
