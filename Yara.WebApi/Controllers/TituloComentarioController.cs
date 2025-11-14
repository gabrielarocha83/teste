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
    [RoutePrefix("commentstitle")]
    [Authorize]
    public class TituloComentarioController : ApiController
    {
        private readonly IAppServiceTituloComentario _tituloComentario;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="tituloComentario"></param>
        public TituloComentarioController(IAppServiceTituloComentario tituloComentario)
        {
            _tituloComentario = tituloComentario;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="linha"></param>
        /// <param name="ano"></param>
        /// <param name="empresa"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcomments/{numero}/{linha}/{ano}/{empresa}")]
        public async Task<GenericResult<IQueryable<TituloComentarioDto>>> Get(string numero, string linha, string ano, string empresa, ODataQueryOptions<TituloComentarioDto> options)
        {
            var result = new GenericResult<IQueryable<TituloComentarioDto>>();

            try
            {
                var stateList = await _tituloComentario.GetAllFilterAsync(c => c.NumeroDocumento.Equals(numero) && c.Linha.Equals(linha) && c.AnoExercicio.Equals(ano) && c.Empresa.Equals(empresa));

                var totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(stateList.AsQueryable(), new ODataQuerySettings()).Cast<TituloComentarioDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(stateList.AsQueryable()).Cast<TituloComentarioDto>();
                result.Count = totalReg > 0 ? totalReg : stateList.Count();
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
