using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("report")]
    [Authorize]
    public class RelatorioController : ApiController
    {
        private readonly IAppServiceRelatorios _relatorios;
        
        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="relatorio"></param>
        public RelatorioController(IAppServiceRelatorios relatorio)
        {
            _relatorios = relatorio;
        }

        /// <summary>
        /// Metodo que retorna uma tela de consulta de Propostas com Filtros
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Tipo eq Bovino</param>
        /// <param name="filter">Filtros</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/proposal/search")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "RelatorioProposta_Visualizar")]
        public async Task<GenericResult<IQueryable<BuscaPropostasDto>>> Get(ODataQueryOptions<BuscaPropostasDto> options, BuscaPropostasSearchDto filter)
        {
            var result = new GenericResult<IQueryable<BuscaPropostasDto>>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();
                filter.EmpresaID = empresaId;

                var ret = await _relatorios.GetConsultaProposta(filter);

                int totalReg = ret.Count();
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(ret.AsQueryable(), new ODataQuerySettings()).Cast<BuscaPropostasDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(ret.AsQueryable()).Cast<BuscaPropostasDto>();
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

        /// <summary>
        /// Método exportar Excel a consulta de Propostas
        /// </summary>
        /// <param name="filter">Filtros para Busca de Propostas</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/proposal/export")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "RelatorioProposta_Visualizar")]
        public async Task<HttpResponseMessage> PostPropostalExcel(BuscaPropostasSearchDto filter)
        {
            HttpResponseMessage result = null;

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();
                filter.EmpresaID = empresaId;

                var arquivo = await _relatorios.GetConsultaPropostaExcel(filter);

                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(arquivo);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = $"Propostas{DateTime.Now:yyyyMMddHHmm}.xlsx"
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            catch (Exception e)
            {
                var error = new ErrorsYara();
                error.ErrorYara(e);
                result = Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/proposal/status")]
        public async Task<GenericResult<IEnumerable<string>>> Status()
        {
            var result = new GenericResult<IEnumerable<string>>();

            try
            {
                result.Result = await _relatorios.GetStatusProposal();
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
