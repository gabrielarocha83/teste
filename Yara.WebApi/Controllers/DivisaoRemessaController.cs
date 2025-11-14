using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("shippingdivision")]
    [Authorize]
    public class DivisaoRemessaController : ApiController
    {
        private readonly IAppServiceDivisaoRemessa _appServiceDivisaoRemessa;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServiceDivisaoRemessa"></param>
        /// <param name="appServiceLog"></param>
        public DivisaoRemessaController(IAppServiceDivisaoRemessa appServiceDivisaoRemessa, IAppServiceLog appServiceLog)
        {
            _appServiceDivisaoRemessa = appServiceDivisaoRemessa;
            _log = appServiceLog;
          
        }

        /// <summary>
        /// Metodo todas o log da divisão
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getshippingdivisionlogs/{Divisao}/{Item}/{Numero}/{Solicitante}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "DivisaoRemessa_Log")]
        public async Task<GenericResult<IEnumerable<DivisaoRemessaLogFluxoDto>>> GetLogs(int Divisao, int Item, string Numero, Guid Solicitante)
        {
            var result = new GenericResult<IEnumerable<DivisaoRemessaLogFluxoDto>>();
            try
            {
                var acompanhamento = await _appServiceDivisaoRemessa.GetAllLogByDivisao(Divisao, Item, Numero, Solicitante);

                result.Result = acompanhamento;
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
