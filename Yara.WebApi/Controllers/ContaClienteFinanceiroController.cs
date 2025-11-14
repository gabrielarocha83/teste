using System;
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
    [RoutePrefix("financialaccounts")]
    [Authorize]
    public class ContaClienteFinanceiroController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceContaClienteFinanceiro _contaClienteFinanceiro;
        private readonly ConceitoCobrancaLiberacaoLogValidation _validator;
        private readonly ContaClienteFinanceiroValidator _financeiroValidator;
        private readonly LimiteYaraGalvaniValidator _limiteValidator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="contaClienteFinanceiro"></param>
        /// <param name="log"></param>
        public ContaClienteFinanceiroController(IAppServiceContaClienteFinanceiro contaClienteFinanceiro, IAppServiceLog log)
        {
            _log = log;
            _contaClienteFinanceiro = contaClienteFinanceiro;
            _validator = new ConceitoCobrancaLiberacaoLogValidation();
            _financeiroValidator = new ContaClienteFinanceiroValidator();
            _limiteValidator = new LimiteYaraGalvaniValidator();
        }

        /// <summary>
        /// Método que consulta os dados financeiros por código SAP
        /// </summary>
        /// <param name="codsap">Código SAP</param>
        /// <returns>Dados da Financeiros</returns>
        [HttpGet]
        [Route("v1/getfinancialcode/{codsap}")]
        public async Task<GenericResult<ContaClienteFinanceiroDto>> GetCodSAP(string codsap)
        {
            var result = new GenericResult<ContaClienteFinanceiroDto>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();

                result.Result = await _contaClienteFinanceiro.GetCodSAPFinanceiro(codsap, empresa);
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
        /// Método que consulta de exibição dos dados financeiros de uma conta cliente
        /// </summary>
        /// <param name="id">Código da Conta Cliente</param>
        /// <returns>Dados da ContaCliente Financeiro</returns>
        [HttpGet]
        [Route("v1/getfinancial/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteFinanceiro_Visualizar")]
        public async Task<GenericResult<ContaClienteFinanceiroDto>> Get(Guid id)
        {
            var result = new GenericResult<ContaClienteFinanceiroDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();

                result.Result = await _contaClienteFinanceiro.GetAsync(c => c.ContaClienteID.Equals(id) && c.EmpresasID.Equals(empresa));
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
        [Route("v1/getrawfinancial/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteFinanceiro_Visualizar")]
        public async Task<GenericResult<ContaClienteFinanceiroDto>> GetRaw(Guid id)
        {
            var result = new GenericResult<ContaClienteFinanceiroDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();

                result.Result = await _contaClienteFinanceiro.GetRawAsync(c => c.ContaClienteID.Equals(id) && c.EmpresasID.Equals(empresa));
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
        /// 
        /// </summary>
        /// <param name="clienteFinanceiroDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertfinancial")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteFinanceiro_Inserir")]
        public async Task<GenericResult<ContaClienteFinanceiroDto>> Insert(ContaClienteFinanceiroDto clienteFinanceiroDto)
        {
            var result = new GenericResult<ContaClienteFinanceiroDto>();
            var validatorFinanceiro = _financeiroValidator.Validate(clienteFinanceiroDto);

            if (validatorFinanceiro.IsValid)
            {
                try
                {
                    var user = User.Identity as ClaimsIdentity;
                    var userId = user.Claims.First(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();

                    clienteFinanceiroDto.EmpresasID = empresa;
                    clienteFinanceiroDto.UsuarioIDCriacao = Guid.Empty;
                    clienteFinanceiroDto.DataCriacao = DateTime.Now;
                    if (user.Claims.Any(c => c.ValueType.Equals("ContaCliente_EnvioSeguradora")))
                    {
                        clienteFinanceiroDto.PermiteEnviarSeguradora = true;
                    }

                    result.Success = _contaClienteFinanceiro.Insert(clienteFinanceiroDto);

                    var descricao = $"Inseriu dados financeiro para o cliente {clienteFinanceiroDto.ContaClienteID}";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
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
                result.Errors = validatorFinanceiro.GetErrors();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conta"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/enablefinancial")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteFinanceiro_Editar")]
        public async Task<GenericResult<ConceitoCobrancaLiberacaoLogDto>> EnableConceitoCobranca(ConceitoCobrancaLiberacaoLogDto conta)
        {
            var result = new GenericResult<ConceitoCobrancaLiberacaoLogDto>();
            var validationResult = _validator.Validate(conta);

            if (validationResult.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var user = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();

                    conta.UsuarioID = new Guid(user);
                    conta.EmpresaID = empresa;

                    result.Success = await _contaClienteFinanceiro.UpdateConceitoCobranca(conta);

                    string descricao = $"O Conceito de Cobrança do cliente {conta.ContaClienteId} foi { (conta.Status ? "liberado" : "bloqueado") } pelo usuario {User.Identity.Name}.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.ConceitoCobranca);
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
        /// 
        /// </summary>
        /// <param name="financeiroDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/updatefinancial")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteFinanceiro_Editar")]
        public async Task<GenericResult<ContaClienteFinanceiroDto>> UpdateContaClienteFinanceiro(ContaClienteFinanceiroDto financeiroDto)
        {
            var result = new GenericResult<ContaClienteFinanceiroDto>();
            var validationResult = _financeiroValidator.Validate(financeiroDto);
            var userClaims = User.Identity as ClaimsIdentity;

            string userid = null;
            if (validationResult.IsValid)
            {
                try
                {
                    financeiroDto.EmpresasID = Request.Properties["Empresa"].ToString();
                    if (userClaims != null) userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    if (userClaims.Claims.Any(c => c.ValueType.Equals("CobrancaAlterarRestricaoSerasa_Editar"))) { financeiroDto.PermiteSerasa = true; }
                    if (userClaims.Claims.Any(c => c.ValueType.Equals("CobrancaIgnorarConceitoCobranca_Editar"))) { financeiroDto.Conceito = true; }
                    if (userClaims.Claims.Any(c => c.ValueType.Equals("CobrancaPercentualPdd_Editar"))) { financeiroDto.PermitePdd = true; }
                    if (userClaims.Claims.Any(c => c.ValueType.Equals("CobrancaAlteraSinistro_Editar"))) { financeiroDto.PermiteSinistro = true; }
                    if (userClaims.Claims.Any(c => c.ValueType.Equals("ContaCliente_EnvioSeguradora"))) { financeiroDto.PermiteEnviarSeguradora = true; }

                    financeiroDto.UsuarioIDAlteracao = new Guid(userid);
                    financeiroDto.DataAlteracao = DateTime.Now;
                    result.Success = await _contaClienteFinanceiro.Update(financeiroDto);

                    var descricao = $"Usuario {User.Identity.Name}, fez alteração de dados financeiros na conta do cliente {financeiroDto.ContaClienteID}";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, financeiroDto.ContaClienteID);
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
        /// Método que busca somatoria de valores de titulos por cliente e exibe na conta cliente financeiro
        /// </summary>
        /// <param name="id">Código da Conta Cliente</param>
        /// <returns>Dados da ContaCliente Financeiro</returns>
        [HttpGet]
        [Route("v1/gettitlesfinancial/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteFinanceiro_Visualizar")]
        public async Task<GenericResult<DadosFinanceiroTituloDto>> GetSomatoriaTitulosFinanceiro(Guid id)
        {
            var result = new GenericResult<DadosFinanceiroTituloDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();
                result.Result = await _contaClienteFinanceiro.GetDadosFinanceiroSomatoriaTitulos(id, empresa);
                result.Success = true;
            }
            catch (ArgumentException e)
            {
                result.Success = false;
                result.Errors = new[] { Resources.Resources.Error };
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
        /// Método para buscar os dados do de Resumo de Cobrança da Conta Cliente
        /// </summary>
        /// <param name="id">ID da Conta Cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/colsummary/{id:guid}")]
        // [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ControleCobranca_Acesso")]
        public async Task<GenericResult<ContaClienteResumoCobrancaDto>> GetResumoCobranca(Guid id)
        {
            var result = new GenericResult<ContaClienteResumoCobrancaDto>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();

                var retorno = await _contaClienteFinanceiro.GetResumoCobranca(id, empresaId);
                result.Result = retorno;

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
        /// 
        /// </summary>
        /// <param name="conceitoManualDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/updateconcept")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ContaClienteFinanceiro_Editar")]
        public async Task<GenericResult<ConceitoManualContaClienteDto>> UpdateConceitoContaCliente(ConceitoManualContaClienteDto conceitoManualDto)
        {
            var result = new GenericResult<ConceitoManualContaClienteDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                conceitoManualDto.EmpresasID = Request.Properties["Empresa"].ToString();
                conceitoManualDto.UsuarioID = new Guid(userid);

                result.Result = await _contaClienteFinanceiro.UpdateConceitoCobranca(conceitoManualDto);
                result.Success = true;

                var descricao = result.Result.LogMessage;
                var level = EnumLogLevelDto.AccountClient;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, conceitoManualDto.ContaClienteID);
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

            return result;
        }

        [HttpPost]
        [Route("v1/updatelimit")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "EditarLC")]
        public async Task<GenericResult<LimiteYaraGalvaniDto>> UpdateYaraGalvaniLimite(LimiteYaraGalvaniDto limite)
        {
            var result = new GenericResult<LimiteYaraGalvaniDto>();
            var validationResult = _limiteValidator.Validate(limite);

            if (validationResult.IsValid)
            {
                try
                {
                    var dadosFinanceiros = await _contaClienteFinanceiro.UpdateLimiteCredito(limite);
                    result.Success = dadosFinanceiros != null ? true : false;

                    string descricaoYara = $"LC Yara editado de {dadosFinanceiros.LimiteYara.Value.ToString("C")} para {limite.LimiteYara.Value.ToString("C")}.";
                    string descricaoGalvani = $"LC Galvani editado de { dadosFinanceiros.LimiteGalvani.Value.ToString("C")} para { limite.LimiteGalvani.Value.ToString("C")}.";
                    string descricao = "";

                    if (dadosFinanceiros.LimiteYara.Value != limite.LimiteYara.Value && dadosFinanceiros.LimiteGalvani.Value != limite.LimiteGalvani.Value)
                        descricao = descricaoYara + " e " + descricaoGalvani;
                    else if (dadosFinanceiros.LimiteYara.Value != limite.LimiteYara.Value)
                        descricao = descricaoYara;
                    else if (dadosFinanceiros.LimiteGalvani.Value != limite.LimiteGalvani.Value)
                        descricao = descricaoGalvani;

                    var logDtoYara = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.AccountClient, limite.ContaClienteId);
                    _log.Create(logDtoYara);
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
    }
}
