using System;
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
    [RoutePrefix("revenue")]
    [Authorize]
    public class ReceitaController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceReceita _appServiceReceita;
        private readonly ReceitaValidator _validator;

        /// <summary>
        /// Construtor da classe ReceitaController
        /// </summary>
        /// <param name="appServiceReceita"></param>
        /// <param name="appServiceLog"></param>
        public ReceitaController(IAppServiceReceita appServiceReceita, IAppServiceLog appServiceLog)
        {
            _log = appServiceLog;
            _appServiceReceita = appServiceReceita;
            _validator = new ReceitaValidator();
        }

        /// <summary>
        /// Metodo que retorna a lista de receitas
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Descricao eq Estoque</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getrevenue")]
        public async Task<GenericResult<IQueryable<ReceitaDto>>> Get(ODataQueryOptions<ReceitaDto> options)
        {
            var result = new GenericResult<IQueryable<ReceitaDto>>();
            try
            {
                var retReceita = await _appServiceReceita.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retReceita.AsQueryable(), new ODataQuerySettings()).Cast<ReceitaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retReceita.AsQueryable()).Cast<ReceitaDto>();
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
        /// Metodo que retorna os dados da receita de acordo com Guid
        /// </summary>
        /// <param name="id">Guid da Receita</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodrevenue/{id:guid}")]
        public async Task<GenericResult<ReceitaDto>> Get(Guid id)
        {
            var result = new GenericResult<ReceitaDto>();
            try
            {
                result.Result = await _appServiceReceita.GetAsync(g => g.ID.Equals(id));
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
        /// Metodo para inserir nova receita
        /// </summary>
        /// <param name="receitaDto">Object ReceitaDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertrevenue")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoReceita_Inserir")]
        public async Task<GenericResult<ReceitaDto>> Post(ReceitaDto receitaDto)
        {

            receitaDto.ID = Guid.NewGuid();
            var objuserLogin = User.Identity as ClaimsIdentity;
            var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

            var result = new GenericResult<ReceitaDto>();
            var validationResult = _validator.Validate(receitaDto);
            LogDto logDto = null;

            if (validationResult.IsValid)
            {
                try
                {
                    receitaDto.DataCriacao = DateTime.Now;
                    receitaDto.UsuarioIDCriacao = new Guid(userLogin);
                    result.Success = await _appServiceReceita.InsertAsync(receitaDto);
                    var descricao = $"Inseriu uma receita com a descrição {receitaDto.Descricao}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
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
                result.Errors = validationResult.GetErrors();
            }
            return result;
        }

        /// <summary>
        /// Metodo para alterar receita
        /// </summary>
        /// <param name="receitaDto">Object CulturaDto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updaterevenue")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoReceita_Editar")]
        public async Task<GenericResult<ReceitaDto>> Put(ReceitaDto receitaDto)
        {
            var result = new GenericResult<ReceitaDto>();
            var validationResult = _validator.Validate(receitaDto);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    receitaDto.UsuarioIDAlteracao = new Guid(userLogin);
                    receitaDto.DataAlteracao = DateTime.Now;
                    result.Success = await _appServiceReceita.Update(receitaDto);
                    var descricao = $"Atualizou a receita com a descrição {receitaDto.Descricao}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, receitaDto.ID);
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
                result.Errors = validationResult.GetErrors();
            }
            return result;
        }

        /// <summary>
        /// Metodo para inativar uma receita
        /// </summary>
        /// <param name="id">Guid da receita</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactiverevenue/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoReceita_Excluir")]
        public async Task<GenericResult<ReceitaDto>> Delete(Guid id)
        {
            var result = new GenericResult<ReceitaDto>();
            LogDto logDto;
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _appServiceReceita.Inactive(id);

                var descricao = $" Usuario {userLogin} desativou a receita ID: {id}";
                var level = EnumLogLevelDto.Info;
                logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, id);
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
