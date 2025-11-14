using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.Extensions;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("users")]
    public class UsuariosController : ApiController
    {
        private readonly IAppServiceUsuario _appServiceUsuario;
        private readonly UsuarioValidator _usuarioValidator;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServiceUsuario"></param>
        /// <param name="log"></param>
        public UsuariosController(IAppServiceUsuario appServiceUsuario, IAppServiceLog log)
        {
            _appServiceUsuario = appServiceUsuario;
            _usuarioValidator = new UsuarioValidator();
            _log = log;
        }

        /// <summary>
        /// Responsável de retornar uma lista de permissões que o usuário tem acesso
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Nome eq José</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/permissions")]
        public Dictionary<string, string> GetPermissoes(ODataQueryOptions<UsuarioDto> options)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var result = userIdentity.Claims.Where(c => c.Type.Equals("Permissao")).Distinct();
            return result.ToDictionary(c => c.ValueType, d => d.Value);
        }

        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Nome eq José</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getuser")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Usuario_Acesso")]
        public async Task<GenericResult<IQueryable<UsuarioDto>>> Get(ODataQueryOptions<UsuarioDto> options,BuscaUsuariosDto filtros)
        {
            var result = new GenericResult<IQueryable<UsuarioDto>>();
            try
            {

                IEnumerable<UsuarioDto> todosUsuarios = null;

                todosUsuarios = await _appServiceUsuario.GetListUsers(filtros);
                int totalReg = todosUsuarios.Count();
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(todosUsuarios.AsQueryable(), new ODataQuerySettings()).Cast<UsuarioDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(todosUsuarios.AsQueryable()).Cast<UsuarioDto>();
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
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Nome eq José</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getsimpleuserlist")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Usuario_Acesso")]
        public async Task<GenericResult<IQueryable<UsuarioDto>>> GetSimpleUserList()
        {
            var result = new GenericResult<IQueryable<UsuarioDto>>();
            try
            {
                var usuarios = await _appServiceUsuario.GetSimpleUserList();
                result.Result = usuarios.AsQueryable();
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
        /// Igual simpleuserlist, porém sem permissão de acesso para consultar a lista de usuário para enviar o blog.
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Nome eq José</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getbloguserlist")]
        public async Task<GenericResult<IQueryable<UsuarioDto>>> GetBlogUserList()
        {
            var result = new GenericResult<IQueryable<UsuarioDto>>();
            try
            {
                var usuarios = await _appServiceUsuario.GetSimpleUserList();
                result.Result = usuarios.AsQueryable();
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
        /// Busca usuário por Código
        /// </summary>
        /// <param name="id">Código do Usuário</param>
        /// <returns>Usuário</returns>
        [HttpGet]
        [Route("v1/getcoduser/{id:guid}")]
        public async Task<GenericResult<UsuarioDto>> Get(Guid id)
        {
            var result = new GenericResult<UsuarioDto>();
            try
            {
                result.Result = await _appServiceUsuario.GetAsync(g => g.ID.Equals(id));
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
        /// Busca usuário logado
        /// </summary>
        /// <returns>Usuário</returns>
        [HttpGet]
        [Route("v1/getcoduseractive")]
        public async Task<GenericResult<UsuarioDto>> GetActive()
        {
            var result = new GenericResult<UsuarioDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var user = new Guid(userLogin);
                result.Result = await _appServiceUsuario.GetAsync(g => g.ID.Equals(user));
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
        /// Adiciona um novo usuário
        /// </summary>
        /// <param name="usuarioDto">Adiciona um novo usuário</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertuser")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Usuario_Inserir")]
        public async Task<GenericResult<UsuarioDto>> Post(UsuarioDto usuarioDto)
        {
            var result = new GenericResult<UsuarioDto>();
            var usuarioValidation = _usuarioValidator.Validate(usuarioDto);

            if (usuarioValidation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    usuarioDto.UsuarioIDCriacao = new Guid(userLogin);

                    result.Success = await _appServiceUsuario.InsertAsync(usuarioDto);

                    var descricao = $"Inseriu um Usuário com o nome {usuarioDto.Nome} e Login {usuarioDto.Login}";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
                    _log.Create(logDto);
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Errors = new[] { e.Message };
                    var error = new ErrorsYara();
                    error.ErrorYara(e);
                }
            }
            else
                result.Errors = usuarioValidation.GetErrors();

            return result;
        }

        /// <summary>
        /// Atualiza a preferencia do Usuário para Yara(Y) ou Galvani(G) 
        /// </summary>
        /// <param name="usuarioDto">Usuário</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updateuserpreferences")]
        public async Task<GenericResult<UsuarioDto>> PutLogado(UsuarioDto usuarioDto)
        {
            var result = new GenericResult<UsuarioDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                usuarioDto.UsuarioIDAlteracao = new Guid(userLogin);

                result.Success = await _appServiceUsuario.UpdatePreferences(usuarioDto);

                Request.Properties.Clear();
                Request.Properties.Add("Empresa", usuarioDto.EmpresaLogada);

                var empresa = usuarioDto.EmpresaLogada.Equals("Y") ? "Yara" : "Galvani";
                var descricao = $"Atualizou a preferencia do Usuário com do nome {usuarioDto.Nome} para a empresa {empresa}";
                var level = EnumLogLevelDto.Info;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, usuarioDto.ID);
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

        /// <summary>
        /// Atualiza um usuário 
        /// </summary>
        /// <param name="usuarioDto">Usuário</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updateuser")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Usuario_Editar")]
        public async Task<GenericResult<UsuarioDto>> Put(UsuarioDto usuarioDto)
        {
            var result = new GenericResult<UsuarioDto>();
            var usuarioValidation = _usuarioValidator.Validate(usuarioDto);
       
            if (usuarioValidation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    usuarioDto.UsuarioIDAlteracao = new Guid(userLogin);

                    result.Success = await _appServiceUsuario.Update(usuarioDto);

                    var descricao = $"Atualizou um Usuário com do nome {usuarioDto.Nome} e Login {usuarioDto.Login}";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, usuarioDto.ID);
                    _log.Create(logDto);
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Errors = new[] { e.Message };
                    var error = new ErrorsYara();
                    error.ErrorYara(e);
                }

            }
            else
            {
                result.Errors = usuarioValidation.GetErrors();
            }
            return result;
        }

        /// <summary>
        /// Atualiza um usuário 
        /// </summary>
        /// <param name="usuarioDto">Usuário</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updateuserstruct")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Usuario_Editar")]
        public async Task<GenericResult<UsuarioDto>> PutStruct(UsuarioDto usuarioDto)
        {
            var result = new GenericResult<UsuarioDto>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                usuarioDto.UsuarioIDAlteracao = new Guid(userLogin);

                result.Success = await _appServiceUsuario.UpdateStructs(usuarioDto);

                var descricao = $"Atualizou um Usuário com do nome {usuarioDto.Nome} e Login {usuarioDto.Login}";
                var level = EnumLogLevelDto.Info;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, usuarioDto.ID);
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

        /// <summary>
        /// Inativa um usuário do sistema
        /// </summary>
        /// <param name="id">Código do Usuário</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactiveuser/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Usuario_Excluir")]
        public async Task<GenericResult<UsuarioDto>> Delete(Guid id)
        {
            var result = new GenericResult<UsuarioDto>();

            try
            {
                var user = await _appServiceUsuario.GetAsync(c => c.ID.Equals(id));

                result.Success = await _appServiceUsuario.Inactive(id);

                var descricao = $"Inativou o Usuário {user.Nome}";
                var level = EnumLogLevelDto.Info;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, id);
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