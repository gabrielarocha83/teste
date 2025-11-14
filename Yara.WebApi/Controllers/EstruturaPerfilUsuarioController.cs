using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Microsoft.Ajax.Utilities;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591
#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("profilestructure")]
    [Authorize]
    public class EstruturaPerfilUsuarioController : ApiController
    {
        private readonly IAppServiceEstruturaPerfilUsuario _perfilUsuario;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="perfilUsuario"></param>
        /// <param name="log"></param>
        public EstruturaPerfilUsuarioController(IAppServiceEstruturaPerfilUsuario perfilUsuario, IAppServiceLog log)
        {
            _perfilUsuario = perfilUsuario;
            _log = log;
        }

        /// <summary>
        /// Metodo que retorna a lista de toda a estrutura de perfil do usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getprofile")]
        public async Task<GenericResult<IQueryable<EstruturaPerfilUsuarioDto>>> Get(ODataQueryOptions<EstruturaPerfilUsuarioDto> options)
        {
            var result = new GenericResult<IQueryable<EstruturaPerfilUsuarioDto>>();

            try
            {
                var retPerfil = await _perfilUsuario.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retPerfil.AsQueryable(), new ODataQuerySettings()).Cast<CulturaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retPerfil.AsQueryable()).Cast<EstruturaPerfilUsuarioDto>();
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
        /// Metodo que retorna a lista de toda de usuários de um Perfil Analista de Cobrança ou Crédito
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/profile/analyst")]
        public async Task<GenericResult<IQueryable<UsuarioDto>>> GetAnalyst(ODataQueryOptions<UsuarioDto> options)
        {
            var result = new GenericResult<IQueryable<UsuarioDto>>();

            try
            {
                var retPerfil = await _perfilUsuario.GetAllFilterAsync(c=>c.Perfil.Descricao.Contains("Analista") && c.UsuarioId.HasValue);
                var retorno = retPerfil.Select(c => c.Usuario);
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retorno.AsQueryable(), new ODataQuerySettings()).Cast<UsuarioDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retorno.AsQueryable()).Cast<UsuarioDto>();
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
        ///  Método que retorna os Fluxos de Perfil por filtros de usuário e CTC
        /// </summary>
        /// <param name="busca">Codigo CTC ou Nome ou Usuário ou Login</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getprofileedit")]
        public async Task<GenericResult<IEnumerable<BuscaCTCPerfilUsuarioDto>>> GetProfile(BuscaCTCUsuarioDto busca)
        {
            var result = new GenericResult<IEnumerable<BuscaCTCPerfilUsuarioDto>>();

            try
            {
                result.Result = await _perfilUsuario.BuscaContaCliente(busca.Usuario, busca.CTC);
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
        /// Metodo para inserir Estrutura
        /// </summary>
        /// <param name="perfilUsuarioDtos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertprofile")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "EstruturaPerfilUsuario_Inserir")]
        public async Task<GenericResult<EstruturaPerfilUsuarioDto>> Post(EstruturaPerfilUsuarioDto perfilUsuarioDtos)
        {
            var result = new GenericResult<EstruturaPerfilUsuarioDto>();
            //var validationResult = _validator.Validate(culturaDto);

            //if (validationResult.IsValid)
            //{
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                perfilUsuarioDtos.ID = Guid.NewGuid();
                perfilUsuarioDtos.UsuarioIDCriacao = new Guid(userLogin);
                perfilUsuarioDtos.DataCriacao = DateTime.Now;

                result.Success = _perfilUsuario.Insert(perfilUsuarioDtos);
                var descricao = $"Usuario {userLogin}, adicionou estrutura de perfil para usuarios.";
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
            //}
            //else
            //{
            //    result.Errors = validationResult.GetErrors();
            //}

            return result;
        }

        /// <summary>
        /// Metodo para inserir Estrutura
        /// </summary>
        /// <param name="perfilUsuarioDtos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertlistprofile")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "EstruturaPerfilUsuario_Inserir")]
        public async Task<GenericResult<EstruturaPerfilUsuarioDto>> PostList(List<EstruturaPerfilUsuarioDto> perfilUsuarioDtos)
        {
            var result = new GenericResult<EstruturaPerfilUsuarioDto>();
            //var validationResult = _validator.Validate(culturaDto);

            //if (validationResult.IsValid)
            //{
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                result.Success = await _perfilUsuario.InsertListAsync(perfilUsuarioDtos, new Guid(userLogin));
                var descricao = $"Usuario {userLogin}, adicionou estrutura de perfil para usuarios.";
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
            //}
            //else
            //{
            //    result.Errors = validationResult.GetErrors();
            //}

            return result;
        }

        /// <summary>
        /// Metodo para alterar estrutura de perfil de usuario
        /// </summary>
        /// <param name="perfilUsuarioDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updateprofile")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "EstruturaPerfilUsuario_Editar")]
        public async Task<GenericResult<EstruturaPerfilUsuarioDto>> Put(EstruturaPerfilUsuarioDto perfilUsuarioDto)
        {
            var result = new GenericResult<EstruturaPerfilUsuarioDto>();
            //var validationResult = _validator.Validate(culturaDto);

            //if (validationResult.IsValid)
            //{
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                result.Success = await _perfilUsuario.Update(perfilUsuarioDto);
                var descricao = $"Usuario {userLogin}, alterou a estrutura de perfil para usuarios.";
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
            //}
            //else
            //{
            //    result.Errors = validationResult.GetErrors();
            //}

            return result;
        }

        /// <summary>
        /// Metodo para alterar estrutura de perfil de usuario
        /// </summary>
        /// <param name="perfilUsuarioDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updatelistprofile")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "EstruturaPerfilUsuario_Editar")]
        public async Task<GenericResult<EstruturaPerfilUsuarioDto>> PutList(List<EstruturaPerfilUsuarioDto> perfilUsuarioDto)
        {
            var result = new GenericResult<EstruturaPerfilUsuarioDto>();

            //var validationResult = _validator.Validate(culturaDto);
            //if (validationResult.IsValid)
            //{
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _perfilUsuario.UpdateListAsync(perfilUsuarioDto, new Guid(userLogin));
                var descricao = $"Usuario {userLogin}, alterou a estrutura de perfil para usuarios.";
                var level = EnumLogLevelDto.Info;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
                _log.Create(logDto);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { Resources.Resources.Error };
                var error = new ErrorsYara();
                error.ErrorYara(e);
            }

            //}
            //else
            //{
            //    result.Errors = validationResult.GetErrors();
            //}
            return result;
        }

        /// <summary>
        /// Metodo para inserir ou editar Estrutura
        /// </summary>
        /// <param name="perfilUsuarioDtos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertupdateprofile")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "EstruturaPerfilUsuario_Inserir")]
        public async Task<GenericResult<EstruturaPerfilUsuarioDto>> PostInsertUpdate(EstruturaPerfilUsuarioDto perfilUsuarioDtos)
        {
            var objuserLogin = User.Identity as ClaimsIdentity;
            var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

            var result = new GenericResult<EstruturaPerfilUsuarioDto>();
            try
            {
                result.Success = await _perfilUsuario.InsertOrUpdateAsync(perfilUsuarioDtos, new Guid(userLogin));
                var descricao = $"Usuario {userLogin}, adicionou ou alterou estrutura de perfil para usuarios.";
                var level = EnumLogLevelDto.Info;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
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
