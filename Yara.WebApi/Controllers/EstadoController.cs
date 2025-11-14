using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("state")]
    [Authorize]
    public class EstadoController : ApiController
    {
        private readonly IAppServiceEstado _estado;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="estado"></param>
        public EstadoController(IAppServiceEstado estado)
        {
            _estado = estado;
        }

        /// <summary>
        /// Metodo que retorna a lista de estados por região
        /// </summary>
        /// <param name="id">Código da Região</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getstatebyregion/{id:guid}")]
        public async Task<GenericResult<IEnumerable<EstadoDto>>> Get(Guid id)
        {
            var result = new GenericResult<IEnumerable<EstadoDto>>();
            try
            {
                result.Result = await _estado.GetAllStatesByRegion(id);
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
        /// Metodo que retorna a lista de todos os estados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getstates")]
        public async Task<GenericResult<IEnumerable<EstadoDto>>> Get()
        {
            var result = new GenericResult<IEnumerable<EstadoDto>>();
            try
            {
                var stateList = await _estado.GetAllStates();
                result.Result = stateList.OrderBy(c => c.Nome);
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
