using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("profile")]
    [Authorize]
    public class PerfilController : ApiController
    {
        private readonly IAppServicePerfil _perfil;
        private readonly IAppServiceLog _log;
        private readonly ClaimsIdentity _user;
        private readonly PermissaoValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="permissao"></param>
        /// <param name="log"></param>
        public PerfilController(IAppServicePerfil permissao, IAppServiceLog log)
        {

            _perfil = permissao;
            _log = log;
            _validator = new PermissaoValidator();
            _user = (ClaimsIdentity)User.Identity;

        }

        /// <summary>
        /// Método que retorna uma lista de perfils
        /// </summary>
        /// <param name="options">Filtros OData</param>
        /// <returns> Lista de Perfis</returns>
        [HttpGet]
        [Route("v1/getprofiles")]
        public async Task<GenericResult<IQueryable<PerfilDto>>> Get(ODataQueryOptions<PerfilDto> options)
        {

            var result = new GenericResult<IQueryable<PerfilDto>>();
            try
            {
                var permissao = await _perfil.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(permissao.AsQueryable(), new ODataQuerySettings()).Cast<PerfilDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(permissao.AsQueryable()).Cast<PerfilDto>();
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
