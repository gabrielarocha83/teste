using System;
using System.Collections.Generic;
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
    [RoutePrefix("city")]
    [Authorize]
    public class CidadeController : ApiController
    {
        private readonly IAppServiceCidade _cidade;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="cidade"></param>
        public CidadeController(IAppServiceCidade cidade)
        {
            _cidade = cidade;
        }

        /// <summary>
        /// Metodo que retorna a lista de cidades por estado
        /// </summary>
        /// <param name="id">Código do Estado</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcitybystate/{id:guid}")]
        public async Task<GenericResult<IEnumerable<CidadeDto>>> Get(Guid id)
        {
            var result = new GenericResult<IEnumerable<CidadeDto>>();
            try
            {
                var cityList = await _cidade.GetAllStatesByState(id);
                result.Result = cityList.OrderBy(c => c.Nome);
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
    }
}
