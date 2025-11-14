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
    [RoutePrefix("price")]
    [Authorize]
    public class MediaSacaController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceMediaSaca _saca;
        private readonly MediaSacaValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        public MediaSacaController(IAppServiceMediaSaca saca, IAppServiceLog log)
        {
            _saca = saca;
            _log = log;
            _validator = new MediaSacaValidator();
        }

        /// <summary>
        /// Metodo que retorna a lista de Preço Medio por Saca
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Tipo eq Bovino</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getaverage")]
        public async Task<GenericResult<IQueryable<MediaSacaDto>>> Get(ODataQueryOptions<MediaSacaDto> options)
        {
            var result = new GenericResult<IQueryable<MediaSacaDto>>();
            try
            {
                var retmedia = await _saca.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retmedia.AsQueryable(), new ODataQuerySettings()).Cast<MediaSacaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retmedia.AsQueryable()).Cast<MediaSacaDto>();
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
        /// Metodo que retorna os dados da Preço Medio por Saca de acordo com Guid
        /// </summary>
        /// <param name="id">Guid da Produtividade Media</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodaverage/{id:guid}")]
        public async Task<GenericResult<MediaSacaDto>> Get(Guid id)
        {
            var result = new GenericResult<MediaSacaDto>();
            try
            {
                result.Result = await _saca.GetAsync(g => g.ID.Equals(id));
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
        /// Metodo para inserir novo Preço Medio por Saca
        /// </summary>
        /// <param name="mediaSacaDto">Object MediaSacaDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertaverage")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "MediaSaca_Inserir")]
        public async Task<GenericResult<MediaSacaDto>> Post(MediaSacaDto mediaSacaDto)
        {

            mediaSacaDto.ID = Guid.NewGuid();
            var objuserLogin = User.Identity as ClaimsIdentity;
            var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

            var result = new GenericResult<MediaSacaDto>();
            var validation = _validator.Validate(mediaSacaDto);
            LogDto logDto = null;

            if (validation.IsValid)
            {
                try
                {
                    mediaSacaDto.DataCriacao = DateTime.Now;
                    mediaSacaDto.UsuarioIDCriacao = new Guid(userLogin);
                    result.Success = await _saca.InsertAsync(mediaSacaDto);
                    var descricao = $"Inseriu um Preço Medio por Saca com o nome {mediaSacaDto.Nome}";
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
                result.Errors = validation.GetErrors();
            }
            return result;
        }

        /// <summary>
        /// Metodo para alterar Preço Medio por Saca
        /// </summary>
        /// <param name="mediaSacaDto">Object MediaSacaDto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updateaverage")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "MediaSaca_Editar")]
        public async Task<GenericResult<MediaSacaDto>> Put(MediaSacaDto mediaSacaDto)
        {
            var result = new GenericResult<MediaSacaDto>();
            var validation = _validator.Validate(mediaSacaDto);
            LogDto logDto = null;
            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    mediaSacaDto.UsuarioIDAlteracao = new Guid(userLogin);
                    mediaSacaDto.DataAlteracao = DateTime.Now;
                    result.Success = await _saca.Update(mediaSacaDto);
                    var descricao = $"Atualizou a Preço Medio por Saca com o nome {mediaSacaDto.Nome}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, mediaSacaDto.ID);
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
                result.Errors = validation.GetErrors();
            }
            return result;
        }

        /// <summary>
        /// Metodo para inativar um Preço Medio por Saca
        /// </summary>
        /// <param name="id">Guid da media</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactivemedia/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "MediaSaca_Excluir")]
        public async Task<GenericResult<MediaSacaDto>> Delete(Guid id)
        {
            var result = new GenericResult<MediaSacaDto>();
            LogDto logDto;
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _saca.Inactive(id);

                var descricao = $" Usuario {userLogin} desativou o Preço Medio por Saca ID: {id}";
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
