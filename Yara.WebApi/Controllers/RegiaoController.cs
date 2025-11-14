using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("region")]
    [Authorize]
    public class RegiaoController : ApiController
    {
        private readonly IAppServiceRegiao _regiao;
        
        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="regiao"></param>
        /// <param name="appServiceLog"></param>
        public RegiaoController(IAppServiceRegiao regiao)
        {
            _regiao = regiao;
        }

        /// <summary>
        /// Metodo que retorna a lista de regiões
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Tipo eq Bovino</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getregion")]
        public async Task<GenericResult<IQueryable<RegiaoDto>>> Get(ODataQueryOptions<RegiaoDto> options)
        {
            var result = new GenericResult<IQueryable<RegiaoDto>>();
            try
            {
                var ret = await _regiao.GetAllRegion();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(ret.AsQueryable(), new ODataQuerySettings()).Cast<RegiaoDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(ret.AsQueryable()).Cast<RegiaoDto>();
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
    }
}
