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
    [RoutePrefix("classificationgroup")]
    [Authorize]
    public class ClassificacaoGrupoEconomicoController : ApiController
    {
        private readonly IAppServiceClassificacaoGrupoEconomico _classificacaoGrupoEconomico;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="classificacaoGrupoEconomico"></param>
        public ClassificacaoGrupoEconomicoController(IAppServiceClassificacaoGrupoEconomico classificacaoGrupoEconomico)
        {
            _classificacaoGrupoEconomico = classificacaoGrupoEconomico;
        }

        /// <summary>
        /// Metodo que retorna a lista as classificações do Grupo Economico
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getclassificationgroup")]
        public async Task<GenericResult<IEnumerable<ClassificacaoGrupoEconomicoDto>>> Get()
        {
            var result = new GenericResult<IEnumerable<ClassificacaoGrupoEconomicoDto>>();
            try
            {
                result.Result = await _classificacaoGrupoEconomico.GetAll();
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
