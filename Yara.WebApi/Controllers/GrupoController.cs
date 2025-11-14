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
    [RoutePrefix("groups")]
    [Authorize]
    public class GrupoController : ApiController
    {
        private readonly IAppServiceGrupo _grupo;
        private readonly GrupoValidator _validator;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="grupo"></param>
        /// <param name="log"></param>
        public GrupoController(IAppServiceGrupo grupo, IAppServiceLog log)
        {
            _validator = new GrupoValidator();
            _grupo = grupo;
            _log = log;
        }

        /// <summary>
        /// Retorna todos os grupos, possibilitando utilizar filtros OData
        /// </summary>
        /// <param name="options">Filtros OData</param>
        /// <returns>Lista de Grupos</returns>
        [HttpGet]
        [Route("v1/getgroups")]
        public async Task<GenericResult<IQueryable<GrupoDto>>> Get(ODataQueryOptions<GrupoDto> options)
        {
            var result = new GenericResult<IQueryable<GrupoDto>>();

            try
            {
                var grupo = await _grupo.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(grupo.AsQueryable(), new ODataQuerySettings()).Cast<GrupoDto>();
                    totalReg = filtro.Count();
                }

                result.Result = options.ApplyTo(grupo.AsQueryable()).Cast<GrupoDto>();
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
        ///Busca Grupo por código
        /// </summary>
        /// <param name="id">Código do Grupo</param>
        /// <returns>Unico Grupo</returns>
        [HttpGet]
        [Route("v1/getcodgroups/{id:guid}")]
        public async Task<GenericResult<GrupoDto>> Get(Guid id)
        {
            var result = new GenericResult<GrupoDto>();

            try
            {
                var groups = await _grupo.GetAsync(c => c.ID.Equals(id));
                //foreach (var group in groups.GruposPermissoes)
                //{
                //    group.Permissao = await _permissao.GetAsync(C => C.ID.Equals(group.PermissaoID));
                //}
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
        /// Busca Grupo por nome
        /// </summary>
        /// <param name="value">Objeto GrupoDto</param>
        /// <returns>Objeto GrupoDto</returns>
        [HttpPost]
        [ResponseType(typeof(GrupoDto))]
        [Route("v1/getnamegroups")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "GrupoUsuario_Acesso")]
        public async Task<GenericResult<GrupoDto>> Get(GrupoDto value)
        {
            var result = new GenericResult<GrupoDto>();

            try
            {
                result.Result = await _grupo.GetAsync(c => c.Nome.Equals(value.Nome));
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
        /// Insere um novo Grupo
        /// </summary>
        /// <param name="value">Grupo</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(GrupoDto))]
        [Route("v1/insertgroup")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "GrupoUsuario_Inserir")]
        public async Task<GenericResult<GrupoDto>> Post(GrupoDto value)
        {
            var result = new GenericResult<GrupoDto>();
            var validationResult = _validator.Validate(value);

            if (validationResult.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    value.ID = Guid.NewGuid();
                    value.UsuarioIDCriacao = new Guid(userLogin);

                    result.Success = await _grupo.InsertAsync(value);

                    var descricao = $"Inseriu um Grupo com o nome {value.Nome}.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
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
                result.Errors = validationResult.GetErrors();

            return result;
        }

        /// <summary>
        /// Atualize um Grupo
        /// </summary>
        /// <param name="value">Grupo</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(GrupoDto))]
        [Route("v1/updategroups")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "GrupoUsuario_Editar")]
        public async Task<GenericResult<GrupoDto>> Put(GrupoDto value)
        {
            var result = new GenericResult<GrupoDto>();
            var validationResult = _validator.Validate(value);

            if (validationResult.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    value.UsuarioIDAlteracao = new Guid(userLogin);

                    result.Success = await _grupo.Update(value);

                    var descricao = $"Atualizou um Grupo com o nome {value.Nome}.";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, value.ID);
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
                result.Errors = validationResult.GetErrors();

            return result;
        }

        /// <summary>
        /// Inativa um Grupo
        /// </summary>
        /// <param name="id">Código do Grupo</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactivegroup/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "GrupoUsuario_Excluir")]
        public async Task<GenericResult<GrupoDto>> Delete(Guid id)
        {
            var result = new GenericResult<GrupoDto>();

            try
            {
                var grupo = await _grupo.GetAsync(c => c.ID.Equals(id));

                result.Success = await _grupo.Inactive(id);

                var descricao = $"Inativou um Grupo com o nome {grupo.Nome}";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, id);
                _log.Create(logDto);
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
