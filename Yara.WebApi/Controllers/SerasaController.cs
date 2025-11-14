using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;
using System.Configuration;
using Yara.Service.Serasa;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("serasa")]
    [Authorize]
    public class SerasaController : ApiController
    {
        private readonly IAppServiceContaCliente _contaCliente;
        private readonly IAppServiceSerasa _serasa;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="serasa"></param>
        /// <param name="log"></param>
        /// <param name="contaCliente"></param>
        public SerasaController(IAppServiceSerasa serasa, IAppServiceLog log, IAppServiceContaCliente contaCliente)
        {
            _serasa = serasa;
            _log = log;
            _contaCliente = contaCliente;
        }

        /// <summary>
        /// Método que valida se possuí historico do serasa
        /// </summary>
        /// <param name="id">Código da Conta Cliente</param>
        /// <returns>Retorna o objeto do Solicitante do Serasa</returns>
        [HttpGet]
        [Route("v1/checkexist/{id:guid}")]
        public async Task<GenericResult<SolicitanteSerasaDto>> Exist(Guid id)
        {
            var result = new GenericResult<SolicitanteSerasaDto>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();

                var serasa = await _serasa.ExistSerasa(id, empresa);

                if (!string.IsNullOrWhiteSpace(serasa.Json))
                {
                    result.Success = true;
                    result.Result = serasa;
                }
                else
                {
                    result.Success = false;
                    result.Errors = new string[] { "Esta conta cliente não possuí histórico." };
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
        /// Método que envia uma solicitação de serasa de uma conta cliente
        /// </summary>
        /// <param name="solicitante">Objeto Solicitante</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/search")]
        [ResponseType(typeof(SolicitanteSerasaDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Serasa_Consulta")]
        public async Task<GenericResult<dynamic>> ConsultaSerasa(SolicitanteSerasaDto solicitante)
        {
            var result = new GenericResult<dynamic>();

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

                // solicitante.TipoSerasa = vem preenchido do front;
                // solicitante.ContaClienteID = vem preenchido do front;
                solicitante.UsuarioIDCriacao = new Guid(userLogin);

                result.Result = await _serasa.ConsultarSerasa(solicitante, empresaId, urlSerasa, usuarioSerasa, senhaSerasa);
                result.Success = true;
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
        /// Método que envia uma solicitação de serasa de várias contas clientes
        /// </summary>
        /// <param name="solicitanteLista">Lista de IDs de contas clientes a serem consultadas</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/searchList")]
        //[ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Serasa_Consulta")]
        public async Task<GenericResult<bool>> ConsultaSerasaLista(ListaSolicitanteSerasaDto solicitanteLista)
        {
            var result = new GenericResult<bool>();

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

                foreach(var contaClienteID in solicitanteLista.ContaClienteID)
                {
                    var solicitante = new SolicitanteSerasaDto
                    {
                        UsuarioIDCriacao = new Guid(userLogin),
                        ContaClienteID = contaClienteID
                    };

                    // solicitante.TipoSerasa = vem preenchido do front;
                    await _serasa.ConsultarSerasa(solicitante , empresaId, urlSerasa, usuarioSerasa, senhaSerasa);
                }

                result.Result = true;
                result.Success = true;
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
        /// Método que envia uma solicitação de serasa de uma proposta de limite de crédito
        /// </summary>
        /// <param name="PropostaID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/proposalLC/{PropostaID}")]
        public async Task<GenericResult<dynamic>> ConsultaSerasaPropostaLC(Guid PropostaID)
        {
            var result = new GenericResult<dynamic>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                string urlSerasa;
                string usuarioSerasa;
                string senhaSerasa;

                if (empresa == "G")
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

                result.Result = await _serasa.ConsultarSerasaPropostaLC(new SolicitanteSerasaDto { UsuarioIDCriacao = new Guid(userLogin) }, PropostaID, empresa, urlSerasa, usuarioSerasa, senhaSerasa);
                result.Success = true;
            }
            catch (ArgumentException arg)
            {
                result.Success = false;
                result.Errors = new[] { arg.Message };
            }
            catch (SerasaException e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
                var error = new ErrorsYara();
                error.ErrorYara(e);
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
        /// Método que envia uma solicitação de serasa de uma proposta de alçada comercial.
        /// </summary>
        /// <param name="PropostaID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/tradearea/{PropostaID}")]
        public async Task<GenericResult<dynamic>> ConsultaSerasaAlcada(Guid PropostaID)
        {
            var result = new GenericResult<dynamic>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                string urlSerasa;
                string usuarioSerasa;
                string senhaSerasa;

                if (empresa == "G")
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

                result.Result = await _serasa.ConsultarSerasaAlcadaComercial(new SolicitanteSerasaDto { UsuarioIDCriacao = new Guid(userLogin) }, PropostaID, empresa, urlSerasa, usuarioSerasa, senhaSerasa);
                result.Success = true;
            }
            catch (ArgumentException arg)
            {
                result.Success = false;
                result.Errors = new[] { arg.Message };
            }
            catch (SerasaException e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
                var error = new ErrorsYara();
                error.ErrorYara(e);
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
        /// Método que lista o histórico da conta cliente
        /// </summary>
        /// <param name="options"></param>
        /// <param name="contacliente"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/history/{contacliente:guid}")]
        public async Task<GenericResult<IQueryable<SolicitanteSerasaDto>>> Historico(ODataQueryOptions<SolicitanteSerasaDto> options, Guid contacliente)
        {
            var result = new GenericResult<IQueryable<SolicitanteSerasaDto>>();

            try
            {
                var historico = await _serasa.Historico(contacliente);
                result.Result = options.ApplyTo(historico.AsQueryable()).Cast<SolicitanteSerasaDto>();
                result.Count = historico.Count();
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
        /// Método que retorna os detalhes do serasa do Histórico
        /// </summary>
        /// <param name="id">Código do Histórico</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/detailhistory/{id:guid}")]
        public async Task<GenericResult<dynamic>> DetailsHistorico(Guid id)
        {
            var result = new GenericResult<dynamic>();

            try
            {
                var historico = await _serasa.HitoricoDetalhe(id);
                result.Result = historico;
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
        /// Método que inclui uma restrição e pendencia Serasa.
        /// </summary>
        /// <param name="solicitanteDto"></param>
        /// <param name="tipoRestricao"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updatestatus")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "CobrancaAlterarRestricaoSerasa_Editar")]
        public async Task<GenericResult<SolicitanteSerasaDto>> AddRestricaoPendencia(SolicitanteSerasaDto solicitanteDto, int tipoRestricao)
        {
            var result = new GenericResult<SolicitanteSerasaDto>();
            var userClaims = User.Identity as ClaimsIdentity;
            LogDto logDto = null;
            string userid = null;

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();
                if (userClaims != null) userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                var solicitacao = await _serasa.AlterarStatusSerasa(solicitanteDto.ContaClienteID, empresa, tipoRestricao);
                result.Success = solicitacao;

                if (result.Success)
                {
                    var contaClienteDto = await _contaCliente.GetAsync(c => c.ID.Equals(solicitanteDto.ContaClienteID));
                    var descricao = $"Atualizou as informações do Serasa da Conta Cliente do nome {contaClienteDto.Nome}";
                    var level = EnumLogLevelDto.AccountClient;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, contaClienteDto.ID);
                    _log.Create(logDto);
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
