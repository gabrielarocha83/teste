using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.Extensions;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("permissions")]
    [Authorize]
    public class PermissaoController : ApiController
    {
        private readonly IAppServicePermissao _permissao;
        private readonly IAppServiceLog _log;
        private readonly ClaimsIdentity _user;
        private readonly PermissaoValidator _validator;

        public PermissaoController(IAppServicePermissao permissao, IAppServiceLog log)
        {

            _permissao = permissao;
            _log = log;
            _validator = new PermissaoValidator();
            _user = (ClaimsIdentity)User.Identity;

        }

        /// <summary>
        /// Retorna todos os grupos, possibilitando utilizar filtros OData
        /// </summary>
        /// <param name="options">Filtros OData</param>
        /// <returns> Lista de Permissões</returns>
        [HttpGet]
        [Route("v1/getpermissions")]
        public async Task<GenericResult<IQueryable<PermissaoDto>>> Get(ODataQueryOptions<PermissaoDto> options)
        {

            var result = new GenericResult<IQueryable<PermissaoDto>>();
            try
            {
                var permissao = await _permissao.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(permissao.AsQueryable(), new ODataQuerySettings()).Cast<PermissaoDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(permissao.AsQueryable()).Cast<PermissaoDto>();
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
        /// Busca Permissão por código
        /// </summary>
        /// <param name="nome">Nome da Permissão</param>
        /// <returns>Unica Permissão</returns>
        [HttpGet]
        [Route("v1/getcodpermission/{nome}")]
        public async Task<GenericResult<PermissaoDto>> Get(string nome)
        {
            var result = new GenericResult<PermissaoDto>();
        
            try
            {
                result.Result = await _permissao.GetAsync(g => g.Nome.Equals(nome));
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
        /// Insere uma nova Permissão
        /// </summary>
        /// <param name="value">Permissão</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(PermissaoDto))]
        [Route("v1/insertpermission")]
        public GenericResult<PermissaoDto> Post(PermissaoDto value)
        {
            var result = new GenericResult<PermissaoDto>();
            var validateResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validateResult.IsValid)
            {
                try
                {
                    var user = User.Identity as ClaimsIdentity;
                    var userID = user.Claims.First(c => c.Type.Equals("Usuario")).Value;
                    result.Success = _permissao.Insert(value);

                    var descricao = $"Inseriu uma Permissão com o nome {value.Nome}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(_user, descricao, level);
                    _log.Create(logDto);

                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Errors = new[] { Resources.Resources.Error };
                   var error = new ErrorsYara();
                     error.ErrorYara(e);
                }
            }
            else
            {
                result.Errors = validateResult.GetErrors();
            }
           
            return result;
        }

        /// <summary>
        /// Atualiza uma  Permissão
        /// </summary>
        /// <param name="value">Permissão</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(PermissaoDto))]
        [Route("v1/updatepermission")]
        public async Task<GenericResult<PermissaoDto>> Put(PermissaoDto value)
        {
            var result = new GenericResult<PermissaoDto>();
            var validateResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validateResult.IsValid)
            {
                try
                {
                    var user = User.Identity as ClaimsIdentity;
                    var userID = user.Claims.First(c => c.Type.Equals("Usuario")).Value;
                   
                    result.Success = await _permissao.Update(value);
                    var descricao = $"Atualizou uma Permissão com o nome {value.Nome}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(_user, descricao, level, null);
                    _log.Create(logDto);
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Errors = new[] { Resources.Resources.Error };
                   var error = new ErrorsYara();
                     error.ErrorYara(e);
                }
            }
            else
            {
                result.Errors = validateResult.GetErrors();
            }
          
            return result;
        }

        /// <summary>
        /// Inativa uma Permissão
        /// </summary>
        /// <param name="nome">Nome da Permissão</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactivegpermission/{nome}")]
        public async Task<GenericResult<PermissaoDto>> Delete(string nome)
        {
            var result = new GenericResult<PermissaoDto>();
            try
            {
                var permissao = await _permissao.GetAsync(c => c.Nome.Equals(nome));
                result.Success = await _permissao.Inactive(nome);
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
