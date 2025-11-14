using System;
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
    [RoutePrefix("commercialstructurepaper")]
    [Authorize]
    public class EstruturaComercialPapelController : ApiController
    {
        private readonly IAppServiceEstruturaComercialPapel _appServiceEstruturaComercialPapel;
        private readonly IAppServiceEstruturaPerfilUsuario _appServiceEstruturaPerfilUsuario;
        private readonly IAppServiceLog _appServiceLog;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServiceEstruturaComercialPapel"></param>
        /// <param name="appServiceEstruturaPerfilUsuario"></param>
        /// <param name="appServiceLog"></param>
        public EstruturaComercialPapelController(IAppServiceEstruturaComercialPapel appServiceEstruturaComercialPapel,
                                                 IAppServiceEstruturaPerfilUsuario appServiceEstruturaPerfilUsuario,
                                                 IAppServiceLog appServiceLog)
        {
            _appServiceEstruturaComercialPapel = appServiceEstruturaComercialPapel;
            _appServiceEstruturaPerfilUsuario = appServiceEstruturaPerfilUsuario;
            _appServiceLog = appServiceLog;
        }

        /// <summary>
        /// Lista todos os papeis das Estruturas comerciais ativas
        /// </summary>
        /// <param name="options">Filtros OData</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/commercialstructurespaper")]
        public async Task<GenericResult<IQueryable<EstruturaComercialPapelDto>>> Get(ODataQueryOptions<EstruturaComercialPapelDto> options)
        {
            var result = new GenericResult<IQueryable<EstruturaComercialPapelDto>>();
            try
            {
                var estruturaComerciais = await _appServiceEstruturaComercialPapel.GetAllFilterAsync(c=>c.Ativo);
                result.Result = options.ApplyTo(estruturaComerciais.AsQueryable()).Cast<EstruturaComercialPapelDto>();
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
        /// Busca estrutura comercial por código SAP
        /// </summary>
        /// <param name="id">Código SAP</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcommercialstructurespaperbyid/{id}")]
        public async Task<GenericResult<EstruturaComercialPapelDto>> GetID(string id)
        {
            var result = new GenericResult<EstruturaComercialPapelDto>();
            try
            {
                result.Result = await _appServiceEstruturaComercialPapel.GetByPaper(id);
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
        /// Busca perfil do usuário no cliente
        /// </summary>
        /// <param name="id">Conta Cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getuserprofile/{id:guid}")]
        public async Task<GenericResult<string>> GetUserProfile(Guid id)
        {
            var result = new GenericResult<string>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userIdString = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                var empresaId = Request.Properties["Empresa"].ToString();

                result.Result = await _appServiceEstruturaPerfilUsuario.GetActiveProfileByCustomer(id, new Guid(userIdString), empresaId);
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
