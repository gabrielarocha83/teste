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
    /// <summary>
    /// GRUD para Região
    /// </summary>
    [RoutePrefix("representative")]
    public class RepresentanteController : ApiController
    {
        private readonly IAppServiceRepresentante _representante;
        private readonly IAppServiceUsuario _appServiceUsuario;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="representante"></param>
        /// <param name="appServiceUsuario"></param>
        public RepresentanteController(IAppServiceRepresentante representante, IAppServiceUsuario appServiceUsuario)
        {
            _representante = representante;
            _appServiceUsuario = appServiceUsuario;
        }

        /// <summary>
        /// Metodo que retorna a lista de representantes
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Tipo eq Bovino</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getrepresentative")]
        public async Task<GenericResult<IQueryable<RepresentanteDto>>> Get(ODataQueryOptions<RepresentanteDto> options)
        {
            var result = new GenericResult<IQueryable<RepresentanteDto>>();

            try
            {
                var representante = await _representante.GetAllRepresentation();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(representante.AsQueryable(), new ODataQuerySettings()).Cast<RepresentanteDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(representante.AsQueryable()).Cast<RepresentanteDto>();
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
        /// Metodo que retorna a lista de representantes
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Tipo eq Bovino</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getrepresentativerestrict")]
        public async Task<GenericResult<IQueryable<RepresentanteDto>>> GetRepresentantesEstruturaComercial(ODataQueryOptions<RepresentanteDto> options)
        {
            var result = new GenericResult<IQueryable<RepresentanteDto>>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userId = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                
                UsuarioDto usuarioDto = await _appServiceUsuario.GetAsync(g => g.ID.Equals(new Guid(userId)));
                var representante = usuarioDto.Representantes;

                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(representante.AsQueryable(), new ODataQuerySettings()).Cast<RepresentanteDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(representante.AsQueryable()).Cast<RepresentanteDto>();
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
        /// Método que retorna os representantes de uma conta cliente
        /// </summary>
        /// <param name="options">ODATA</param>
        /// <param name="id">Código Conta Cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getrepresentativeaccountclient/{id:guid}")]
        public async Task<GenericResult<IQueryable<RepresentanteDto>>> Get(ODataQueryOptions<RepresentanteDto> options,Guid id)
        {
            var result = new GenericResult<IQueryable<RepresentanteDto>>();

            try
            {
                var empresaID = Request.Properties["Empresa"].ToString();
                var representante = await _representante.GetAllRepresentationByAccountClient(id, empresaID);
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(representante.AsQueryable(), new ODataQuerySettings()).Cast<RepresentanteDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(representante.AsQueryable()).Cast<RepresentanteDto>();
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
