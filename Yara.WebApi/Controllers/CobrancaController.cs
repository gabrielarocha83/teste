using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    [RoutePrefix("collect")]
    [Authorize]
    public class CobrancaController : ApiController
    {
        private readonly IAppServiceCobranca _cobranca;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="cobranca"></param>
        public CobrancaController(IAppServiceCobranca cobranca)
        {
            _cobranca = cobranca;
        }

        /// <summary>
        /// Método para buscar os dados do Controle de Cobrança Geral por Diretoria
        /// </summary>
        /// <param name="id">Diretoria (opcional)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/generaldir/{id}", Order = 1)]
        [Route("v1/generaldir", Order = 2)]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ControleCobranca_Acesso")]
        public async Task<TotalGenericResult<IEnumerable<CobrancaResultadoDto>>> GetGeralDir(string id = null)
        {
            var result = new TotalGenericResult<IEnumerable<CobrancaResultadoDto>>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();

                var retorno = await _cobranca.CobrancaGeralPorDiretoria(empresaId, id);
                result.Result = retorno;
                result.Count = result.Result.Count();

                // Calcular total geral
                result.Total = retorno != null && retorno.Any() ? retorno.Sum(r => r.Total) : 0;

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
        /// Método para buscar os dados do Controle de Cobrança Efetiva por Diretoria
        /// </summary>
        /// <param name="id">Diretoria (opcional)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/effectivedir/{id}", Order = 1)]
        [Route("v1/effectivedir", Order = 2)]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ControleCobranca_Acesso")]
        public async Task<TotalGenericResult<IEnumerable<CobrancaResultadoDto>>> GetEfetivaDir(string id = null)
        {
            var result = new TotalGenericResult<IEnumerable<CobrancaResultadoDto>>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();

                var retorno = await _cobranca.CobrancaEfetivaPorDiretoria(empresaId, id);
                result.Result = retorno;
                result.Count = result.Result.Count();

                // Calcular total geral
                result.Total = retorno != null && retorno.Any() ? retorno.Sum(r => r.Total) : 0;

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
        /// Método para buscar os dados do Controle de Cobrança Efetiva por Status
        /// </summary>
        /// <param name="id">Diretoria (opcional)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/effectivestat/{id}", Order = 1)]
        [Route("v1/effectivestat", Order = 2)]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ControleCobranca_Acesso")]
        public async Task<TotalGenericResult<IEnumerable<CobrancaResultadoDto>>> GetEfetivaStat(string id = null)
        {
            var result = new TotalGenericResult<IEnumerable<CobrancaResultadoDto>>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();

                var retorno = await _cobranca.CobrancaEfetivaPorStatus(empresaId, id);
                result.Result = retorno;
                result.Count = result.Result.Count();

                // Calcular total geral
                result.Total = retorno != null && retorno.Any() ? retorno.Sum(r => r.Total) : 0;

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
        /// Método para buscar os dados do Controle de Cobrança Efetiva por Estado
        /// </summary>
        /// <param name="options">Parametros ODATA</param>
        /// <param name="id">Diretoria (opcional)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/effectiveuf/{id}", Order = 1)]
        [Route("v1/effectiveuf", Order = 2)]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ControleCobranca_Acesso")]
        public async Task<TotalGenericResult<IEnumerable<CobrancaResultadoDto>>> GetEfetivaUF(ODataQueryOptions<CobrancaResultadoDto> options, string id = null)
        {
            var result = new TotalGenericResult<IEnumerable<CobrancaResultadoDto>>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();

                var retorno = await _cobranca.CobrancaEfetivaPorEstado(empresaId, id);
                result.Result = retorno;
                result.Count = result.Result.Count();

                // Calcular total geral
                result.Total = retorno != null && retorno.Any() ? retorno.Sum(r => r.Total) : 0;

                if (retorno != null && retorno.Count() > 0)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(retorno.AsQueryable(), new ODataQuerySettings()).Cast<CobrancaResultadoDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(retorno.AsQueryable()).Cast<CobrancaResultadoDto>();
                    result.Count = result.Result.Count();
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
        /// Método para buscar os dados do Controle de Cobrança Efetiva por Cultura
        /// </summary>
        /// <param name="options">Parametros ODATA</param>
        /// <param name="id">Diretoria (opcional)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/effectivecult/{id}", Order = 1)]
        [Route("v1/effectivecult", Order = 2)]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ControleCobranca_Acesso")]
        public async Task<TotalGenericResult<IEnumerable<CobrancaResultadoDto>>> GetEfetivaCultura(ODataQueryOptions<CobrancaResultadoDto> options, string id = null)
        {
            var result = new TotalGenericResult<IEnumerable<CobrancaResultadoDto>>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();

                var retorno = await _cobranca.CobrancaEfetivaPorCultura(empresaId, id);
                result.Result = retorno;
                result.Count = result.Result.Count();

                // Calcular total geral
                result.Total = retorno != null && retorno.Any() ? retorno.Sum(r => r.Total) : 0;

                if (retorno != null && retorno.Count() > 0)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(retorno.AsQueryable(), new ODataQuerySettings()).Cast<CobrancaResultadoDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(retorno.AsQueryable()).Cast<CobrancaResultadoDto>();
                    result.Count = result.Result.Count();
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
        /// Método para buscar os dados do Controle de Cobrança - Vencidos 1 a X Dias
        /// </summary>
        /// <param name="options">Parametros ODATA</param>
        /// <param name="id">Diretoria (opcional)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/recentoverdue/{id}", Order = 1)]
        [Route("v1/recentoverdue", Order = 2)]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ControleCobranca_Acesso")]
        public async Task<TotalGenericResult<IEnumerable<CobrancaVencidosResultadoDto>>> GetVencidosMenosDias(ODataQueryOptions<CobrancaVencidosResultadoDto> options, string id = null)
        {
            var result = new TotalGenericResult<IEnumerable<CobrancaVencidosResultadoDto>>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();

                var retorno = await _cobranca.CobrancaVencidosMenosDias(empresaId, id);

                // Calcular total geral
                result.Total = retorno != null && retorno.Any() ? retorno.Sum(r => r.Valor) : 0;

                if (retorno != null && retorno.Count() > 0)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(retorno.AsQueryable(), new ODataQuerySettings()).Cast<CobrancaVencidosResultadoDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(retorno.AsQueryable()).Cast<CobrancaVencidosResultadoDto>();
                    result.Count = result.Result.Count();
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
        /// Método para buscar os dados do Controle de Cobrança - Maiores Devedores
        /// </summary>
        /// <param name="options">Parametros ODATA</param>
        /// <param name="id">Diretoria (opcional)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/greatoverdue/{id}", Order = 1)]
        [Route("v1/greatoverdue", Order = 2)]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ControleCobranca_Acesso")]
        public async Task<TotalGenericResult<IEnumerable<CobrancaResultadoDto>>> GetMaioresDevedores(ODataQueryOptions<CobrancaResultadoDto> options, string id = null)
        {
            var result = new TotalGenericResult<IEnumerable<CobrancaResultadoDto>>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();

                var retorno = await _cobranca.CobrancaMaioresDevedores(empresaId, id);

                // Calcular total geral
                result.Total = retorno != null && retorno.Any() ? retorno.Sum(r => r.Total) : 0;

                if (retorno != null && retorno.Count() > 0)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(retorno.AsQueryable(), new ODataQuerySettings()).Cast<CobrancaResultadoDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(retorno.AsQueryable()).Cast<CobrancaResultadoDto>();
                    result.Count = result.Result.Count();
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
        /// Método para buscar os dados do Controle de Cobrança - Lista de Clientes
        /// </summary>
        /// <param name="options">Parametros ODATA</param>
        /// <param name="filtros">Filtros para Busca de Clientes</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/customers")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ControleCobranca_Acesso")]
        public async Task<GenericResult<CobrancaListaClienteResultadoDto>> PostClientes(ODataQueryOptions<CobrancaListaClienteDto> options, CobrancaFiltroDto filtros)
        {
            var result = new GenericResult<CobrancaListaClienteResultadoDto>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();

                CobrancaListaClienteResultadoDto retorno = null;

                if (filtros.Tipo == "JD")
                {
                    int mes = 0;
                    int ano = DateTime.Now.Year;

                    if (filtros.Chave != null && filtros.Chave.Length == 4)
                        ano = int.Parse(filtros.Chave);
                    else
                        mes = int.Parse(filtros.Chave);

                    retorno = await _cobranca.JuridicoClientes(empresaId, mes, ano);
                }
                else
                {
                    if (filtros.Dias == 360)
                        retorno = await _cobranca.CobrancaClientesGrandTotal(empresaId, filtros.Tipo, filtros.Chave, filtros.Diretoria);
                    else if (filtros.Chave == "all") // as chaves serão buscadas dentro do método
                        retorno = await _cobranca.CobrancaClientesGrandTotalSum(empresaId, filtros.Tipo);
                    else
                        retorno = await _cobranca.CobrancaClientes(empresaId, filtros.Tipo, filtros.Chave, filtros.Dias, filtros.Diretoria);
                }

                if (retorno != null && retorno.Clientes != null)
                {
                    result.Result = retorno;

                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(retorno.Clientes.AsQueryable(), new ODataQuerySettings()).Cast<CobrancaListaClienteDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result.Clientes = options.ApplyTo(retorno.Clientes.AsQueryable()).Cast<CobrancaListaClienteDto>();
                    result.Count = result.Result.Clientes.Count();
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
        /// Método exportar Excel da Lista de Clientes
        /// </summary>
        /// <param name="filtros">Filtros para Busca de Clientes</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/customersexcel")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ControleCobranca_Acesso")]
        public async Task<HttpResponseMessage> PostClientesExcel(CobrancaFiltroDto filtros)
        {
            HttpResponseMessage result = null;

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();

                byte[] arquivo = null;

                if (filtros.Dias == 360)
                    arquivo = await _cobranca.CobrancaClientesExcelGrandTotal(empresaId, filtros.Tipo, filtros.Chave, filtros.Diretoria);
                else if (filtros.Chave == "all") // as chaves serão buscadas dentro do método
                    arquivo = await _cobranca.CobrancaClientesExcelGrandTotalSum(empresaId, filtros.Tipo);
                else
                    arquivo = await _cobranca.CobrancaClientesExcel(empresaId, filtros.Tipo, filtros.Chave, filtros.Dias, filtros.Diretoria);

                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(arquivo);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format("Clientes_{0}_{1:yyyyMMddHHmm}.xlsx", filtros.Tipo, DateTime.Now)
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
        /// Método para buscar os títulos da conta cliente
        /// </summary>
        /// <param name="options">Filtros e Opções ODATA</param>
        /// <param name="id">ID da Conta Cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/customertit/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Cobranca_Acesso")]
        public async Task<GenericResult<TituloContaClienteTotalizadoDto>> GetTitulosCliente(ODataQueryOptions<TituloContaClienteDto> options, Guid id)
        {
            var result = new GenericResult<TituloContaClienteTotalizadoDto>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();

                var retorno = await _cobranca.BuscaTitulosContaCliente(id, empresaId);

                result.Result = new TituloContaClienteTotalizadoDto();

                // Calcular total geral
                result.Result.TotalAVencer = retorno.Where(t => t.Dias <= 0).Sum(t => t.ValorInterno) ?? 0;
                result.Result.QtdAVencer = retorno.Where(t => t.Dias <= 0).Count();
                result.Result.TotalVencido = retorno.Where(t => t.Dias > 0).Sum(t => t.ValorInterno) ?? 0;
                result.Result.QtdVencido = retorno.Where(t => t.Dias > 0).Count();

                if (retorno != null && retorno.Count() > 0)
                {
                    int totalReg = retorno.Count();
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(retorno.AsQueryable(), new ODataQuerySettings()).Cast<TituloContaClienteDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result.Titulos = options.ApplyTo(retorno.AsQueryable()).Cast<TituloContaClienteDto>();
                    result.Count = totalReg;
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
        /// Método exportar Excel da Lista de Títulos de um Cliente
        /// </summary>
        /// <param name="id">ID da Conta Cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/customertitexcel/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Cobranca_Acesso")]
        public async Task<HttpResponseMessage> GetTitulosClienteExcel(Guid id)
        {
            HttpResponseMessage result = null;

            try
            {
                var user = User.Identity as ClaimsIdentity;
                var empresaId = Request.Properties["Empresa"].ToString();
                var acessoCompleto = false;

                if (user.Claims.Any(c => c.ValueType.Equals("Cobranca_Completo")))
                    acessoCompleto = true;

                var arquivo = await _cobranca.BuscaTitulosContaClienteExcel(id, empresaId, acessoCompleto);

                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(arquivo);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format("Titulos_{0:yyyyMMddHHmm}.xlsx", DateTime.Now)
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
        /// Método que retona os dados de Cobrança
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/exists/{id:guid}")]
        public async Task<GenericResult<CobrancaContaDto>> CobrancaPropostas(Guid id)
        {
            var result = new GenericResult<CobrancaContaDto>();

            try
            {
                var empresaId = Request.Properties["Empresa"].ToString();

                var dados = await _cobranca.ExistPropostas(id, empresaId);
                result.Result = dados;
                result.Success = true;
            }
            catch (Exception e)
            {
                var error = new ErrorsYara();
                error.ErrorYara(e);
                result.Success = false;
                result.Errors = new string[] { Resources.Resources.Error };
            }

            return result;
        }
    }
}
