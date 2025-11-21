using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.Extensions;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591
#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("accounts")]
    [Authorize]
    public class ContaClienteController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceContaCliente _contaCliente;
        private readonly IAppServiceHistoricoContaCliente _historico;
        private readonly IAppServiceContaClienteVisita _visita;
        private readonly IAppServiceContaClienteBuscaBens _buscaBens;
        private readonly ContaClienteDadosPessoaisValidator _validator;
        private readonly BloqueioManualContaClienteValidator _bloqueioManual;
        private readonly ContaClienteValidator _contaClienteValidator;
        private readonly ContaClienteVisitaValidator _visitaValidator;
        private readonly ContaClienteBuscaBensValidator _buscaBensValidator;
        private readonly HttpResponseMessage _response;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="contaCliente"></param>
        /// <param name="historico"></param>
        /// <param name="log"></param>
        /// <param name="visita"></param>
        /// <param name="buscaBens"></param>
        public ContaClienteController(IAppServiceContaCliente contaCliente, IAppServiceHistoricoContaCliente historico, IAppServiceLog log, IAppServiceContaClienteVisita visita, IAppServiceContaClienteBuscaBens buscaBens)
        {
            _validator = new ContaClienteDadosPessoaisValidator();
            _bloqueioManual = new BloqueioManualContaClienteValidator();
            _contaClienteValidator = new ContaClienteValidator();
            _visitaValidator = new ContaClienteVisitaValidator();
            _buscaBensValidator = new ContaClienteBuscaBensValidator();
            _response = new HttpResponseMessage();
            _log = log;
            _contaCliente = contaCliente;
            _historico = historico;
            _visita = visita;
            _buscaBens = buscaBens; 
        }

        /// <summary>
        /// Metodo que valida se existe já um Apelido com o mesmo nome
        /// </summary>
        /// <param name="id">Conta Cliente ID</param>
        /// <param name="nickname">Apelido</param>
        /// <returns>True ou False no Result</returns>
        [HttpGet]
        [Route("v1/getcustomerbynickname/{id}/{nickname}", Order = 1)]
        [Route("v1/getcustomerbynickname/{id}", Order = 2)]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Visualizar")]
        public async Task<GenericResult<bool>> ExistNickName(Guid id, string nickname = null)
        {
            var result = new GenericResult<bool>();

            try
            {
                if (nickname != null && nickname != string.Empty)
                {
                    var validacao = await _contaCliente.GetAsync(c => c.Apelido == nickname && c.ID != id);
                    result.Result = validacao.Apelido != null;
                }
                else
                {
                    result.Result = true;
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
        /// Método que retorna dados da Conta Cliente pelo Código Principal
        /// </summary>
        /// <param name="id">Código Principal da  Conta Cliente</param>
        /// <returns>Objeto com os dados da Conta Cliente</returns>
        [HttpGet]
        [Route("v1/getcustomerbyprincipalcode/{id}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Visualizar")]
        public async Task<GenericResult<ContaClienteDto>> GetPrincipalCode(string id)
        {
            var result = new GenericResult<ContaClienteDto>();

            try
            {
                result.Result = await _contaCliente.GetByCodePrincipal(id);
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
        /// Método que retorna dados da Conta Cliente pelo Código
        /// </summary>
        /// <param name="code">Código da Conta Cliente</param>
        /// <returns>Objeto com os dados da Conta Cliente</returns>
        [HttpGet]
        [Route("v1/getcustomerbycode/{code}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Visualizar")]
        public async Task<GenericResult<ContaClienteDto>> GetByCode(string code)
        {
            var result = new GenericResult<ContaClienteDto>();

            try
            {
                var contaClienteID = await _contaCliente.GetIdByCode(code) ?? Guid.Empty;

                if (!contaClienteID.Equals(Guid.Empty))
                    result.Result = await _contaCliente.GetByID(contaClienteID);

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

        [HttpGet]
        [Route("v1/getcustomeridbycode/{code}")]
        public async Task<GenericResult<Guid?>> GetIdByCode(string code)
        {
            var result = new GenericResult<Guid?>();

            try
            {
                result.Result = await _contaCliente.GetIdByCode(code);
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
        /// Método que retorna dados da Conta Cliente pelo ID
        /// </summary>
        /// <param name="id">Código da Conta Cliente</param>
        /// <returns>Objeto com os dados da Conta Cliente</returns>
        [HttpGet]
        [Route("v1/getcustomer/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Visualizar")]
        public async Task<GenericResult<ContaClienteDto>> Get(Guid id)
        {
            var result = new GenericResult<ContaClienteDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();
                var usuario = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                var contaCliente = await _contaCliente.GetByID(id, new Guid(usuario), empresa);

                result.Result = contaCliente;
                result.Success = true;
            }
            catch (UnauthorizedAccessException)
            {
                result.Result = null;
                result.Success = false;
                result.Errors = new[] { "Este cliente não está vinculado a sua Estrutura Comercial no Portal C&C. Favor entrar em contato com a Equipe de Crédito e Cobrança." };
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
        /// Metodo que retorna o histórico do cliente
        /// </summary>
        /// <param name="account">ID Cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/gethistoryaccountclient/{account:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Historico")]
        public async Task<GenericResult<IEnumerable<HistoricoContaClienteDto>>> GetHistory(Guid account)
        {
            var result = new GenericResult<IEnumerable<HistoricoContaClienteDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();

                result.Result = await _historico.GetAllHistoryAccountClient(account, empresa);
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
        /// Metodo que retorna o histórico do cliente
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/gethistoryaccountcodigo/{codigo}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Historico")]
        public async Task<GenericResult<IEnumerable<HistoricoContaClienteDto>>> GetHistoryByCode(string codigo)
        {
            var result = new GenericResult<IEnumerable<HistoricoContaClienteDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();
                var buscaClienteID = await _contaCliente.GetIdByCode(codigo) ?? Guid.Empty;

                if (!buscaClienteID.Equals(Guid.Empty))
                    result.Result = await _historico.GetAllHistoryAccountClient(buscaClienteID, empresa);

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
        ///// Método que retorna Lista de Ordens de Venda do Cliente
        ///// </summary>
        ///// <param name="ordemCliente"></param>
        ///// <returns>Objeto com os dados da Conta Cliente</returns>
        //[HttpPost]
        //[ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Visualizar")]
        //[Route("v1/postordercustomer")]
        //public async Task<GenericResult<BuscaOrdemClienteDto>> GetOrdemVendaCliente(BuscaOrdemClienteDto ordemCliente)
        //{
        //    var result = new GenericResult<BuscaOrdemClienteDto>();
        //    try
        //    {
        //        var empresa = Request.Properties["Empresa"].ToString();
        //        ordemCliente.EmpresaId = empresa;
        //        var buscaClienteOrdemVenda = await _contaCliente.GetOrdemVendaPorCliente(ordemCliente);
        //        result.Result = buscaClienteOrdemVenda;
        //        result.Count = buscaClienteOrdemVenda.BuscaOrdemVendaPrazos.Count();
        //        result.Success = true;
        //    }
        //    catch (Exception e)
        //    {
        //        result.Success = false;
        //        result.Errors = new[] { Resources.Resources.Error };
        //        var error = new ErrorsYara();
        //        error.ErrorYara(e);
        //    }
        //    return result;
        //}

        /// <summary>
        /// Método que busca os sumários das ordens de um cliente
        /// </summary>
        /// <param name="codigo">ID do cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getordercustomersummary/{codigo:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Visualizar")]
        public GenericResult<BuscaOrdemVendaSumarizadoDto> GetOrdemSumario(Guid codigo)
        {
            var result = new GenericResult<BuscaOrdemVendaSumarizadoDto>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                var buscaClienteOrdemVenda = _contaCliente.GetOrdemVendaSumarizado(codigo, empresa);
                result.Result = buscaClienteOrdemVenda;
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
        /// Método que busca dados da conta cliente de acordo com informações preenchida pelo usuario.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="buscaContaClienteDto">Dados de pesquisa.</param>
        /// <returns>Listagem da busca de acordo com os itens do filtro.</returns>
        [HttpPost]
        [Route("v1/searchcustomer")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Acesso")]
        public async Task<GenericResult<IQueryable<BuscaContaClienteDto>>> GetContaClienteConsulta(ODataQueryOptions<BuscaContaClienteDto> options, BuscaContaClienteDto buscaContaClienteDto)
        {
            var result = new GenericResult<IQueryable<BuscaContaClienteDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                buscaContaClienteDto.EmpresaId = empresa;

                var buscaContaCliente = await _contaCliente.GetListAccountClient(buscaContaClienteDto, new Guid(userid));
                if (buscaContaCliente != null)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(buscaContaCliente.AsQueryable(), new ODataQuerySettings()).Cast<BuscaContaClienteDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(buscaContaCliente.AsQueryable()).Cast<BuscaContaClienteDto>();
                    result.Count = buscaContaCliente.Count();
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
        /// Método que busca dados da conta cliente de acordo com a estrutura comercial
        /// </summary>
        /// <param name="options"></param>
        /// <param name="busca">Dados de pesquisa.</param>
        /// <returns>Listagem da busca de acordo com os itens do filtro.</returns>
        [HttpPost]
        [Route("v1/searchbycomlstruc")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Acesso")]
        public async Task<GenericResult<IQueryable<BuscaContaClienteEstComlDto>>> GetContaClienteEstruturaComercial(ODataQueryOptions<BuscaContaClienteEstComlDto> options, BuscaContaClienteEstComlDto busca)
        {
            var result = new GenericResult<IQueryable<BuscaContaClienteEstComlDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                busca.EmpresaId = empresa;

                var buscaContaCliente = await _contaCliente.GetListByComlStruc(busca);
                if (buscaContaCliente != null)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(buscaContaCliente.AsQueryable(), new ODataQuerySettings()).Cast<BuscaContaClienteEstComlDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(buscaContaCliente.AsQueryable()).Cast<BuscaContaClienteEstComlDto>();
                    result.Count = buscaContaCliente.Count();
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
        /// Metódo de log de bloqueio manual da conta cliente
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/postcustomermanual")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_BloqueioManual")]
        public async Task<GenericResult<BloqueioManualContaClienteDto>> LogBloqueioContaCliente(BloqueioManualContaClienteDto clienteDto)
        {
            var result = new GenericResult<BloqueioManualContaClienteDto>();
            var validationResult = _bloqueioManual.Validate(clienteDto);

            if (validationResult.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();

                    clienteDto.EmpresaID = empresa;
                    clienteDto.UsuarioIDAlteracao = new Guid(userid);

                    result.Success = await _contaCliente.UpdateAsyncManualLock(clienteDto);

                    var descricao = clienteDto.BloqueioManual ?
                                    $"Cliente: {clienteDto.Nome}, foi bloqueado manualmente pelo usuário: {User.Identity.Name}." :
                                    $"Cliente: {clienteDto.Nome}, foi desbloqueado manualmente pelo usuário: {User.Identity.Name}.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.AccountClient, clienteDto.ID);
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
                result.Errors = validationResult.GetErrors();

            return result;
        }

        /// <summary>
        /// Metódo de Liberação manual da conta cliente
        /// </summary>
        /// <param name="clienteDto">ID e LiberacaoManual</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/postallowmanual")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_LiberacaoManual")]
        public async Task<GenericResult<ContaClienteDto>> PostLiberacaoManual(ContaClienteDto clienteDto)
        {
            var result = new GenericResult<ContaClienteDto>();
            // var validationResult = _bloqueioManual.Validate(clienteDto);

            //  if (validationResult.IsValid)
            //{
            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                clienteDto.UsuarioIDAlteracao = new Guid(userid);

                result.Success = await _contaCliente.UpdateAsyncAllowManualLock(clienteDto);

                var descricao = clienteDto.LiberacaoManual ?
                    $"O usuário: {User.Identity.Name} realizou a liberação manual do Cliente: {clienteDto.Nome}." :
                    $"O usuário: {User.Identity.Name} retirou a liberação manual do Cliente: {clienteDto.Nome}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.AccountClient, clienteDto.ID);
                _log.Create(logDto);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { Resources.Resources.Error };
                var error = new ErrorsYara();
                error.ErrorYara(e);
            }
            // }
            // else
            // result.Errors = validationResult.GetErrors();

            return result;
        }

        /// <summary>
        /// Metodo para inserir nova conta cliente simplificado
        /// </summary>
        /// <param name="contaClienteDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertcustomer")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Inserir")]
        public async Task<GenericResult<ContaClienteDto>> Post(ContaClienteDto contaClienteDto)
        {
            var result = new GenericResult<ContaClienteDto>();
            var validationResult = _contaClienteValidator.Validate(contaClienteDto);

            if (validationResult.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    contaClienteDto.ID = Guid.NewGuid();
                    contaClienteDto.UsuarioIDCriacao = new Guid(userLogin);

                    result.Success = await _contaCliente.InsertAsync(contaClienteDto);

                    var descricao = $"Usuario {userLogin}, adicionou um novo cliente simplificado.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
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
        /// Método que retorna Lista de Ordens de Venda do Cliente Venda a Prazo
        /// </summary>
        /// <param name="options"></param>
        /// <param name="vendasPrazo"></param>
        /// <returns>Objeto com os dados da Conta Cliente</returns>
        [HttpPost]
        [Route("v1/postterm")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Visualizar")]
        public async Task<GenericResult<IQueryable<BuscaOrdemVendasPrazoDto>>> PostOrdemVendaClientePrazo(ODataQueryOptions<BuscaOrdemVendasPrazoDto> options, BuscaOrdemVendasPrazoDto vendasPrazo)
        {
            var result = new GenericResult<IQueryable<BuscaOrdemVendasPrazoDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                vendasPrazo.EmpresaId = empresa;

                var buscaClienteOrdemVenda = await _contaCliente.GetOrdemVendaPorClientePrazo(vendasPrazo);
                if (buscaClienteOrdemVenda != null)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(buscaClienteOrdemVenda.AsQueryable(), new ODataQuerySettings()).Cast<BuscaOrdemVendasPrazoDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(buscaClienteOrdemVenda.AsQueryable()).Cast<BuscaOrdemVendasPrazoDto>();
                    result.Count = buscaClienteOrdemVenda.Count();
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
        /// Método que retorna Lista de Ordens de Venda do Cliente Venda a Vista
        /// </summary>
        /// <param name="options"></param>
        /// <param name="vendasAVista"></param>
        /// <returns>Objeto com os dados da Conta Cliente</returns>
        [HttpPost]
        [Route("v1/postcash")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Visualizar")]
        public async Task<GenericResult<IQueryable<BuscaOrdemVendasAVistaDto>>> PostOrdemVendaClienteVista(ODataQueryOptions<BuscaOrdemVendasAVistaDto> options, BuscaOrdemVendasAVistaDto vendasAVista)
        {
            var result = new GenericResult<IQueryable<BuscaOrdemVendasAVistaDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                vendasAVista.EmpresaId = empresa;

                var buscaClienteOrdemVenda = await _contaCliente.GetOrdemVendaPorClienteVista(vendasAVista);
                if (buscaClienteOrdemVenda != null)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(buscaClienteOrdemVenda.AsQueryable(), new ODataQuerySettings()).Cast<BuscaOrdemVendasAVistaDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(buscaClienteOrdemVenda.AsQueryable()).Cast<BuscaOrdemVendasAVistaDto>();
                    result.Count = buscaClienteOrdemVenda.Count();
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
        /// Método que retorna Lista de Ordens de Venda do Cliente Paga e Retira
        /// </summary>
        /// <param name="options"></param>
        /// <param name="vendasPagaRetira"></param>
        /// <returns>Objeto com os dados da Conta Cliente</returns>
        [HttpPost]
        [Route("v1/postpay")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Visualizar")]
        public async Task<GenericResult<IQueryable<BuscaOrdemVendasPagaRetiraDto>>> PostOrdemVendaClienteRetira(ODataQueryOptions<BuscaOrdemVendasPagaRetiraDto> options, BuscaOrdemVendasPagaRetiraDto vendasPagaRetira)
        {
            var result = new GenericResult<IQueryable<BuscaOrdemVendasPagaRetiraDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                vendasPagaRetira.EmpresaId = empresa;

                var buscaClienteOrdemVenda = await _contaCliente.GetOrdemVendaPorClienteRetira(vendasPagaRetira);
                if (buscaClienteOrdemVenda != null)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(buscaClienteOrdemVenda.AsQueryable(), new ODataQuerySettings()).Cast<BuscaOrdemVendasPagaRetiraDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(buscaClienteOrdemVenda.AsQueryable()).Cast<BuscaOrdemVendasPagaRetiraDto>();
                    result.Count = buscaClienteOrdemVenda.Count();
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
        /// Método que altera dados da Conta Cliente
        /// </summary>
        /// <param name="contaClienteDto">Objeto do formulario</param>
        /// <returns>Booleano informando status da operação.</returns>
        [HttpPut]
        [Route("v1/updatecustomer")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Editar")]
        [ResponseType(typeof(ContaClienteDto))]
        public async Task<GenericResult<ContaClienteDto>> Put(ContaClienteAlteracaoDadosPessoaisDto contaClienteDto)
        {
            var result = new GenericResult<ContaClienteDto>();
            var validationResult = _validator.Validate(contaClienteDto);

            if (validationResult.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    contaClienteDto.UsuarioIDAlteracao = new Guid(userid);

                    var alteracoes = await _contaCliente.UpdateAsync(contaClienteDto);

                    result.Success = true;

                    var descricao = $"Atualizou dados basicos da conta cliente {contaClienteDto.Nome} referente aos campos " + alteracoes;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.AccountClient, contaClienteDto.ID);
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
                result.Errors = validationResult.GetErrors();

            return result;
        }

        /// <summary>
        /// Método que altera dados da Conta Cliente
        /// </summary>
        /// <param name="id">ContaClienteID</param>
        /// <returns>Booleano informando status da operação.</returns>
        [HttpGet]
        [Route("v1/reavaliar/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Editar")]
        [ResponseType(typeof(ContaClienteDto))]
        public async Task<GenericResult<bool>> Reavaliar(Guid id)
        {
            var result = new GenericResult<bool>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                result.Result = await _contaCliente.ReavaliarContaCliente(id, empresa);
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
        /// Método que altera a estrutura comercial da Conta Cliente
        /// </summary>
        /// <param name="movimentacao">Objeto do Movimentação</param>
        /// <returns>Booleano informando status da operação.</returns>
        [HttpPut]
        [Route("v1/updatecustomerstructure")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteEstrutura_Editar")]
        [ResponseType(typeof(MovimentacaoEstruturaComercialDto))]
        public async Task<GenericResult<MovimentacaoEstruturaComercialDto>> PutCustomerStructure(MovimentacaoEstruturaComercialDto movimentacao)
        {
            var result = new GenericResult<MovimentacaoEstruturaComercialDto>();
            
            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();
                var usuarioId = new Guid(userid);

                movimentacao.UsuarioIDAlteracao = usuarioId;
                movimentacao.EmpresaId = empresa;

                result.Success = await _contaCliente.UpdateContaClienteEstruturaComercial(movimentacao);
                
                if (result.Success)
                {
                    var dadosCliente = await _contaCliente.GetByID(movimentacao.ContaClientes.FirstOrDefault().ID, usuarioId, empresa);

                    var descricao = $"Atualizou a estrutura comercial para o CTC: {movimentacao.CodigoSap} do cliente: {dadosCliente.Nome}";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.AccountClient, dadosCliente.ID);
                    _log.Create(logDto);
                }
            }
            catch (UnauthorizedAccessException)
            {
                result.Result = null;
                result.Success = false;
                result.Errors = new[] { "Este cliente não está vinculado a sua Estrutura Comercial no Portal C&C. Favor entrar em contato com a Equipe de Crédito e Cobrança." };
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
        /// Método que atualiza o representante da estrutura comercial da Conta Cliente
        /// </summary>
        /// <param name="movimentacao">Objeto do Movimentação</param>
        /// <returns>Booleano informando status da operação.</returns>
        [HttpPut]
        [Route("v1/updatecustomerrep")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteEstrutura_Editar")]
        [ResponseType(typeof(MovimentacaoEstruturaComercialDto))]
        public async Task<GenericResult<MovimentacaoEstruturaComercialDto>> PutCustomerRep(MovimentacaoEstruturaComercialDto movimentacao)
        {
            var result = new GenericResult<MovimentacaoEstruturaComercialDto>();
            
            try
            {
                var userLogin = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();
                var userId = userLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var usuarioId = new Guid(userId);

                movimentacao.UsuarioIDAlteracao = usuarioId;
                movimentacao.EmpresaId = empresa;

                result.Success = await _contaCliente.UpdateRepresentanteContaCliente(movimentacao);

                if (result.Success)
                {
                    var dadosCliente = await _contaCliente.GetByID(movimentacao.ContaClientes.FirstOrDefault().ID, usuarioId, empresa);
                    var descricao = $"Atualizou a estrutura comercial para o CTC: {movimentacao.CodigoSap} e o Representante: {movimentacao.RepresentanteID} do cliente: {dadosCliente.Nome}";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.AccountClient, dadosCliente.ID);
                    _log.Create(logDto);
                }
            }
            catch (UnauthorizedAccessException)
            {
                result.Result = null;
                result.Success = false;
                result.Errors = new[]
                {
                    "Este cliente não está vinculado a sua Estrutura Comercial no Portal C&C. Favor entrar em contato com a Equipe de Crédito e Cobrança."
                };
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
        /// Método que busca dados da conta cliente de acordo com informações preenchida pelo usuario.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Listagem da busca de acordo com os itens do filtro.</returns>
        [HttpGet]
        [Route("v1/titlecustomergroup/{Id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaCliente_Acesso")]
        public async Task<GenericResult<IEnumerable<TitulosGrupoEconomicoMembrosDto>>> GetTitulosContaCliente(Guid Id)
        {
            var result = new GenericResult<IEnumerable<TitulosGrupoEconomicoMembrosDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                var buscaTitulos = await _contaCliente.TitulosGrupoEconomicoMembroContaCliente(Id, empresa);
                result.Result = buscaTitulos;
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
        ///  Método que insere data de solicitação de visita do cliente.
        /// </summary>
        /// <param name="clienteDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/postInsertVisit")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteVisita_Inserir")]
        public async Task<GenericResult<ContaClienteVisitaDto>> PostInsereVisita(ContaClienteVisitaDto clienteDto)
        {
            var result = new GenericResult<ContaClienteVisitaDto>();
            //var validationResult = _visitaValidator.Validate(clienteDto);

            //if (validationResult.IsValid)
            //{
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                clienteDto.ID = Guid.NewGuid();
                clienteDto.UsuarioIDCriacao = new Guid(userLogin);
                clienteDto.DataSolicitacao = DateTime.Now;
                clienteDto.EmpresasID = empresa;
                result.Success = _visita.Insert(clienteDto);

                var descricao = $"Usuario {userLogin}, adicionou solicitação de visita para o cliente {clienteDto.ContaClienteID}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, clienteDto.ContaClienteID);
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
            //}
            //else
            //    result.Errors = validationResult.GetErrors();

            return result;
        }

        /// <summary>
        /// Método que registra data do parecer da visita do cliente.
        /// </summary>
        /// <param name="clienteDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/postUpdateVisit")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteVisita_Editar")]
        public async Task<GenericResult<ContaClienteVisitaDto>> PostAlteraVisita(ContaClienteVisitaDto clienteDto)
        {
            var result = new GenericResult<ContaClienteVisitaDto>();
            var validationResult = _visitaValidator.Validate(clienteDto);

            if (validationResult.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();

                    clienteDto.UsuarioIDAlteracao = new Guid(userLogin);
                    clienteDto.DataAlteracao = DateTime.Now;
                    clienteDto.EmpresasID = empresa;

                    result.Success = await _visita.Update(clienteDto);
                    var descricao = $"Usuario {userLogin}, adicionou na solicitação de visita do cliente {clienteDto.ContaClienteID} a data do parecer.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, clienteDto.ContaClienteID);
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
        /// Método que lista todas a visitas do cliente.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getlistVisit/{Id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteVisita_Acesso")]
        public async Task<GenericResult<IEnumerable<ContaClienteVisitaDto>>> GetVisitByCode(Guid id)
        {
            var result = new GenericResult<IEnumerable<ContaClienteVisitaDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();
                var visitas = await _visita.GetAllFilterAsync(c => c.ContaClienteID.Equals(id) && c.EmpresasID.Equals(empresa));

                result.Result = visitas.OrderByDescending(c => c.DataSolicitacao);
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
        /// Método que insere uma data de solicitação de busca de bens para o cliente.
        /// </summary>
        /// <param name="clienteDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/postInsertPatrimony")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteBuscaBens_Inserir")]
        public async Task<GenericResult<ContaClienteBuscaBensDto>> PostInsereBuscaBens(ContaClienteBuscaBensDto clienteDto)
        {
            var result = new GenericResult<ContaClienteBuscaBensDto>();
            //var validationResult = _buscaBensValidator.Validate(clienteDto);
            //if (validationResult.IsValid)
            //{
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                clienteDto.ID = Guid.NewGuid();
                clienteDto.UsuarioIDCriacao = new Guid(userLogin);
                clienteDto.DataSolicitacao = DateTime.Now;
                clienteDto.EmpresasID = empresa;

                result.Success = _buscaBens.Insert(clienteDto);

                var descricao = $"Usuario {userLogin}, adicionou solicitação de busca de bens para o cliente {clienteDto.ContaClienteID}.";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, clienteDto.ContaClienteID);
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
            //}
            //else
            //    result.Errors = validationResult.GetErrors();

            return result;
        }

        /// <summary>
        /// Método que registra uma data de patrimônio de busca de bens para o cliente.
        /// </summary>
        /// <param name="clienteDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/postUpdatePatrimony")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteBuscaBens_Editar")]
        public async Task<GenericResult<ContaClienteBuscaBensDto>> PostAlteraBuscaBens(ContaClienteBuscaBensDto clienteDto)
        {
            var result = new GenericResult<ContaClienteBuscaBensDto>();
            var validationResult = _buscaBensValidator.Validate(clienteDto);

            if (validationResult.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();

                    clienteDto.UsuarioIDAlteracao = new Guid(userLogin);
                    clienteDto.DataAlteracao = DateTime.Now;
                    clienteDto.EmpresasID = empresa;

                    result.Success = await _buscaBens.Update(clienteDto);

                    var descricao = $"Usuario {userLogin}, adicionou na solicitação de busca de bens do cliente {clienteDto.ContaClienteID} a data do patrimônio.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, clienteDto.ContaClienteID);
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
        /// Método que lista todas as buscas de bens para o cliente.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getListPatrimony/{Id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteBuscaBens_Acesso")]
        public async Task<GenericResult<IEnumerable<ContaClienteBuscaBensDto>>> GetBuscaBensByCode(Guid id)
        {
            var result = new GenericResult<IEnumerable<ContaClienteBuscaBensDto>>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();

                var buscaBens = await _buscaBens.GetAllFilterAsync(c => c.ContaClienteID.Equals(id) && c.EmpresasID.Equals(empresa));

                result.Result = buscaBens.OrderByDescending(c => c.DataSolicitacao);
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
        /// Método que lista todas as buscas de bens para o cliente.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/existproposal/{Id:guid}")]
        public async Task<GenericResult<PropostaAtualDto>> ExistProposal(Guid id)
        {
            var result = new GenericResult<PropostaAtualDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();

                result.Result = await _contaCliente.ValidProposalReturn(id, empresa);
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
