using System;
using System.Collections;
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
    [RoutePrefix("cookpit")]
    [Authorize]
    public class CookpitController : ApiController
    {
        private readonly IAppServiceCookpit _cookpit;
        private readonly IAppServiceLog _log;
        public CookpitController(IAppServiceCookpit cookpit, IAppServiceLog log)
        {
            
            _log = log;
            _cookpit = cookpit;
        }

        /// <summary>
        /// Busca o Cookpit do Grupo Economico do usuário logado
        /// </summary>
        /// <returns>Lista de Grupo</returns>
        [HttpGet]
        [Route("v1/geteconomicgroup")]
        public async Task<GenericResult<IEnumerable<BuscaGrupoEconomicoDto>>> GetGrupoPorCliente()
        {
            var result = new GenericResult<IEnumerable<BuscaGrupoEconomicoDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims?.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                var groups = await _cookpit.BuscaGrupoEconomico(new Guid(userid), empresa);

                result.Result = groups;
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
        /// Metodo que retorna as divisões para acompanhamento do usuário logado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getshippingdivisioncockpit")]
        public async Task<GenericResult<IEnumerable<DivisaoRemessaCockPitDto>>> Get()
        {
            var result = new GenericResult<IEnumerable<DivisaoRemessaCockPitDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                var acompanhamento = await _cookpit.GetAllPendencyByUser(new Guid(userLogin), empresa);

                result.Result = acompanhamento;
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
        /// Método que retorna as pendencias de cockpit das ordens
        /// </summary>
        /// <param name="options">OData</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getsalesorderfollow")]
        public async Task<GenericResult<IQueryable<DivisaoRemessaCockPitDto>>> GetOrdemVenda(ODataQueryOptions<DivisaoRemessaCockPitDto> options)
        {
            var result = new GenericResult<IQueryable<DivisaoRemessaCockPitDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims?.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                var retpropostal = await _cookpit.BuscaOrdemVenda(new Guid(userid), empresa);
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retpropostal.AsQueryable(), new ODataQuerySettings()).Cast<DivisaoRemessaCockPitDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retpropostal.AsQueryable()).Cast<DivisaoRemessaCockPitDto>();
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
        /// Método de pendencias CockPit de Proposta de LC
        /// </summary>
        /// <param name="options">OData</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getproposal")]
        public async Task<GenericResult<IQueryable<BuscaCockpitPropostaLCDto>>> GetPropostaLC(ODataQueryOptions<BuscaCockpitPropostaLCDto> options)
        {
            var result = new GenericResult<IQueryable<BuscaCockpitPropostaLCDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims?.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                var retpropostal = await _cookpit.BuscaPropostaLC(new Guid(userid), empresa);
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retpropostal.AsQueryable(), new ODataQuerySettings()).Cast<BuscaCockpitPropostaLCDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retpropostal.AsQueryable()).Cast<BuscaCockpitPropostaLCDto>();
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
        /// Método de acompanhamento de Proposta de LC
        /// </summary>
        /// <param name="options">OData</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getproposalfollow")]
        public async Task<GenericResult<IQueryable<BuscaCockpitPropostaLCDto>>> GetPropostaLCAcompanhamento(ODataQueryOptions<BuscaCockpitPropostaLCDto> options)
        {
            var result = new GenericResult<IQueryable<BuscaCockpitPropostaLCDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims?.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                var retpropostal = await _cookpit.BuscaPropostaLCAcompanhamento(new Guid(userid), empresa);
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retpropostal.AsQueryable(), new ODataQuerySettings()).Cast<BuscaCockpitPropostaLCDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retpropostal.AsQueryable()).Cast<BuscaCockpitPropostaLCDto>();
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
        /// Método de pendencias CockPit de Proposta de Renovação de Vigência de LC
        /// </summary>
        /// <returns>Lista de Grupo</returns>
        [HttpGet]
        [Route("v1/getrenewalproposal")]
        public async Task<GenericResult<IQueryable<BuscaCockpitPropostaRenovacaoVigenciaLCDto>>> GetPropostaRenovacao(ODataQueryOptions<BuscaCockpitPropostaRenovacaoVigenciaLCDto> options)
        {
            var result = new GenericResult<IQueryable<BuscaCockpitPropostaRenovacaoVigenciaLCDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims?.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                var rwpropostal = await _cookpit.BuscaPropostaRenovacaoVigenciaLC(new Guid(userid), empresa);
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(rwpropostal.AsQueryable(), new ODataQuerySettings()).Cast<BuscaCockpitPropostaRenovacaoVigenciaLCDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(rwpropostal.AsQueryable()).Cast<BuscaCockpitPropostaRenovacaoVigenciaLCDto>();
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
        /// Método de pendencias Cockpit e acompanhamento de Proposta Abono
        /// </summary>
        /// <param name="options">OData</param>
        /// <param name="Acompanhar">true ou false para acompanhamento</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getallowance/{Acompanhar}")]
        public async Task<GenericResult<IQueryable<BuscaPropostaAbonoDto>>> GetPropostaAbono(ODataQueryOptions<BuscaPropostaAbonoDto> options, bool Acompanhar)
        {
            var result = new GenericResult<IQueryable<BuscaPropostaAbonoDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims?.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                var retpropostal = await _cookpit.BuscaPropostaAbono(new Guid(userid), empresa, Acompanhar);
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retpropostal.AsQueryable(), new ODataQuerySettings()).Cast<BuscaPropostaAbonoDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retpropostal.AsQueryable()).Cast<BuscaPropostaAbonoDto>();
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
        /// Método de pendencias Cockpit e acompanhamento de Proposta Alçada Comercial
        /// </summary>
        /// <param name="options">OData</param>
        /// <param name="acompanhar">true ou false para acompanhamento</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/gettradearea/{acompanhar}")]
        public async Task<GenericResult<IEnumerable<BuscaCockpitPropostaAlcadaDto>>> GetPropostaAlcada(ODataQueryOptions<BuscaCockpitPropostaAlcadaDto> options, bool acompanhar)
        {
            var result = new GenericResult<IEnumerable<BuscaCockpitPropostaAlcadaDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims?.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                var retpropostal = await _cookpit.BuscaPropostaCockpitAlcada(new Guid(userid), empresa, acompanhar);
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retpropostal.AsQueryable(), new ODataQuerySettings()).Cast<BuscaCockpitPropostaAlcadaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retpropostal.AsQueryable()).Cast<BuscaCockpitPropostaAlcadaDto>();
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
        /// Método de pendencias Cockpit e acompanhamento de Proposta Prorrogação
        /// </summary>
        /// <param name="options">OData</param>
        /// <param name="Acompanhar">true ou false para acompanhamento</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getprolongation/{Acompanhar}")]
        public async Task<GenericResult<IQueryable<BuscaPropostaProrrogacaoDto>>> GetPropostaProrrogacao(ODataQueryOptions<BuscaPropostaProrrogacaoDto> options, bool Acompanhar)
        {
            var result = new GenericResult<IQueryable<BuscaPropostaProrrogacaoDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims?.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                var retpropostal = await _cookpit.BuscaPropostaProrrogacao(new Guid(userid), empresa, Acompanhar);
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retpropostal.AsQueryable(), new ODataQuerySettings()).Cast<BuscaPropostaProrrogacaoDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retpropostal.AsQueryable()).Cast<BuscaPropostaProrrogacaoDto>();
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

        [HttpGet]
        [Route("v1/getextraproposal/{acompanhar}")]
        public async Task<GenericResult<IQueryable<BuscaPropostaLCAdicionalDto>>> GetPropostaLCAdicional(ODataQueryOptions<BuscaPropostaLCAdicionalDto> options, bool Acompanhar)
        {
            var result = new GenericResult<IQueryable<BuscaPropostaLCAdicionalDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims?.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                var retpropostal = await _cookpit.BuscaPropostaLCAdicional(new Guid(userid), empresa, Acompanhar);
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retpropostal.AsQueryable(), new ODataQuerySettings()).Cast<BuscaPropostaLCAdicionalDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retpropostal.AsQueryable()).Cast<BuscaPropostaLCAdicionalDto>();
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
