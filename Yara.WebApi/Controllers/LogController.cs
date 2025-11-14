using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.Extensions;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("logs")]
    [Authorize]
    public class LogController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly LogFluxoAutomaticoValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="log"></param>
        public LogController(IAppServiceLog log)
        {
            _log = log;
            _validator = new LogFluxoAutomaticoValidator();
        }
        
        /// <summary>
        /// Lista todos os usuários cadastrados com filtros OData
        /// </summary>
        /// <param name="options"></param>
        /// <param name="log">Filtros</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/logs")]
        public async Task<GenericResult<IQueryable<LogDto>>> Get(ODataQueryOptions<LogDto> options, BuscaLogsDto log)
        {
            var result = new GenericResult<IQueryable<LogDto>>();

            try
            {
                var logs = await _log.BuscaLog(log);
                if (logs != null)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(logs.AsQueryable(), new ODataQuerySettings()).Cast<LogDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(logs.AsQueryable()).Cast<LogDto>();
                    result.Count = logs.Count();
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
        /// Lista os logs da ContaCliente com filtros OData
        /// </summary>
        /// <param name="options"></param>
        /// <param name="log">Filtros</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/logsaccountclient")]
        public async Task<GenericResult<IQueryable<LogContaClienteDto>>> GetAccountClientLog(ODataQueryOptions<LogContaClienteDto> options, BuscaLogsDto log)
        {
            var result = new GenericResult<IQueryable<LogContaClienteDto>>();

            try
            {
                var logs = await _log.BuscaLogContaCliente(log);
                if (logs != null)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(logs.AsQueryable(), new ODataQuerySettings()).Cast<LogContaClienteDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(logs.AsQueryable()).Cast<LogContaClienteDto>();
                    result.Count = logs.Count();
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
        /// Lista os logs de Liberação Automatica com filtros OData
        /// </summary>
        /// <param name="options"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getlogliberacao")]
        public async Task<GenericResult<IQueryable<BuscaLogFluxoAutomaticoDto>>> GetFluxo(ODataQueryOptions<BuscaLogFluxoAutomaticoDto> options, BuscaLogFluxoAutomaticoDto log)
        {
            var result = new GenericResult<IQueryable<BuscaLogFluxoAutomaticoDto>>();
            var validationResult = _validator.Validate(log);

            if (validationResult.IsValid)
            {
                try
                {
                    var logs = await _log.BuscaLogFluxoAutomatico(log);
                    if (logs != null)
                    {
                        int totalReg = 0;
                        if (options.Filter != null)
                        {
                            var filtro = options.Filter.ApplyTo(logs.AsQueryable(), new ODataQuerySettings()).Cast<BuscaLogFluxoAutomaticoDto>();
                            totalReg = filtro.Count();
                        }
                        result.Result = options.ApplyTo(logs.AsQueryable()).Cast<BuscaLogFluxoAutomaticoDto>();
                        result.Count = logs.Count();
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
            }
            else
                result.Errors = validationResult.GetErrors();

            return result;
        }

        /// <summary>
        /// Lista os logs do Grupo Econômico com filtros OData
        /// </summary>
        /// <param name="options">OData</param>
        /// <param name="id">Código da ContaCliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/logseconomicgroup/{id:guid}")]
        public async Task<GenericResult<IQueryable<LogWithUserDto>>> GetGrupoLog(ODataQueryOptions<LogWithUserDto> options, Guid id)
        {
            var result = new GenericResult<IQueryable<LogWithUserDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                var logs = await _log.BuscaLogGrupoEconomico(id, empresa);

                if (logs != null)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(logs.AsQueryable(), new ODataQuerySettings()).Cast<LogWithUserDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(logs.AsQueryable()).Cast<LogWithUserDto>();
                    result.Count = logs.Count();
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
        /// Lista os logs de Propostas com filtros OData
        /// </summary>
        /// <param name="options">OData</param>
        /// <param name="id">Código da Proposta</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/logsproposal/{id:guid}")]
        public async Task<GenericResult<IQueryable<LogWithUserDto>>> GetProposalLog(ODataQueryOptions<LogWithUserDto> options, Guid id)
        {
            var result = new GenericResult<IQueryable<LogWithUserDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                var logs = await _log.BuscaLogProposta(id);

                if (logs != null)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(logs.AsQueryable(), new ODataQuerySettings()).Cast<LogWithUserDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(logs.AsQueryable()).Cast<LogWithUserDto>();
                    result.Count = logs.Count();
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
        
        ///// <summary>
        ///// Metodo que retorna a lista de Logs de Liberação Automatica
        ///// </summary>
        ///// <param name="options">OData Filtros ex: $filter=Nome eq irrigação</param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("v1/getlogliberacao")]
        //public async Task<GenericResult<IQueryable<LogDivisaoRemessaLiberacaoDto>>> Get(ODataQueryOptions<LogDivisaoRemessaLiberacaoDto> options)
        //{
        //    var result = new GenericResult<IQueryable<LogDivisaoRemessaLiberacaoDto>>();
        //    try
        //    {
        //        var retLogs = await _logDivisaoRemessaLiberacao.GetAllAsync();
        //        int totalReg = 0;
        //        if (options.Filter != null)
        //        {
        //            var filtro = options.Filter.ApplyTo(retLogs.AsQueryable(), new ODataQuerySettings()).Cast<LogDivisaoRemessaLiberacaoDto>();
        //            totalReg = filtro.Count();
        //        }
        //        result.Result = options.ApplyTo(retLogs.AsQueryable()).Cast<LogDivisaoRemessaLiberacaoDto>();
        //        result.Count = totalReg > 0 ? totalReg : result.Result.Count();
        //        result.Success = true;
        //    }
        //    catch (Exception e)
        //    {
        //        result.Success = false;
        //        result.Errors = new[] { Resources.Resources.Error };
        //        var logger = log4net.LogManager.GetLogger("YaraLog");
        //        logger.Error(e.Message);
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="Usuario"></param>
        ///// <param name="dataInicio"></param>
        ///// <param name="dataFim"></param>
        ///// <param name="totalregistros"></param>
        ///// <param name="pagina"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("Usuario,dataInicio,dataFim,totalregistros,pagina")]
        //public async Task<GenericResult<IEnumerable<LogDto>>> Buscar(string Usuario, DateTime dataInicio, DateTime dataFim, int totalregistros, int pagina)
        //{
        //    var result = new GenericResult<IEnumerable<LogDto>>();
        //    LogDto logDto;
        //    try
        //    {
        //        var logs = await _log.GetAllFilter(
        //            c => c.Usuario.Equals(Usuario) && c.DataCriacao >= dataInicio && c.DataCriacao <= dataFim, c => c.DataCriacao,
        //            totalregistros, pagina, true);
        //        result.Result = logs;
        //        result.Success = true;
        //    }
        //    catch (Exception e)
        //    {
        //        result.Success = false;
        //        result.Erros = new[] { e.Message };
        //        var descricao = "Get  Permissao";
        //        var level = EnumLogLevelDto.Info;
        //        logDto = ApiLogDto.GetLog(User.Identity, e.Message, level);
        //        _log.Create(logDto);
        //    }
        //    return result;
        //}
    }
}
