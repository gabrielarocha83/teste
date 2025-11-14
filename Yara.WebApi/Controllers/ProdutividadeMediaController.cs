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
    [RoutePrefix("productivity")]
    [Authorize]
    public class ProdutividadeMediaController : ApiController
    {
        private readonly IAppServiceProdutividadeMedia _appServiceProdutividade;
        private readonly IAppServiceLog _log;
        private readonly ProdutividadeMediaValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        public ProdutividadeMediaController(IAppServiceProdutividadeMedia appServiceProdutividadeMedia, IAppServiceLog appServiceLog)
        {
            _appServiceProdutividade = appServiceProdutividadeMedia;
            _log = appServiceLog;
            _validator = new ProdutividadeMediaValidator();
        }

        /// <summary>
        /// Metodo que retorna a lista de Produtividade Medias
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Tipo eq Bovino</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getaverage")]
        public async Task<GenericResult<IQueryable<ProdutividadeMediaDto>>> Get(ODataQueryOptions<ProdutividadeMediaDto> options)
        {
            var result = new GenericResult<IQueryable<ProdutividadeMediaDto>>();
            try
            {
                var retProdutividadeMedia = await _appServiceProdutividade.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retProdutividadeMedia.AsQueryable(), new ODataQuerySettings()).Cast<ProdutividadeMediaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retProdutividadeMedia.AsQueryable()).Cast<ProdutividadeMediaDto>();
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
        /// Metodo que retorna os dados da Produtividade Media de acordo com Guid
        /// </summary>
        /// <param name="id">Guid da Produtividade Media</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodaverage/{id:guid}")]
        public async Task<GenericResult<ProdutividadeMediaDto>> Get(Guid id)
        {
            var result = new GenericResult<ProdutividadeMediaDto>();
            try
            {
                result.Result = await _appServiceProdutividade.GetAsync(g => g.ID.Equals(id));
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
        /// Metodo para inserir nova Produtividade Media
        /// </summary>
        /// <param name="produtividadeMediaDto">Object ProdutividadeMediaDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertaverage")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ProdutividadeMedia_Inserir")]
        public async Task<GenericResult<ProdutividadeMediaDto>> Post(ProdutividadeMediaDto produtividadeMediaDto)
        {

            produtividadeMediaDto.ID = Guid.NewGuid();
            var objuserLogin = User.Identity as ClaimsIdentity;
            var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

            var result = new GenericResult<ProdutividadeMediaDto>();
            var validation = _validator.Validate(produtividadeMediaDto);
            LogDto logDto = null;

            if (validation.IsValid)
            {
                try
                {
                    produtividadeMediaDto.DataCriacao = DateTime.Now;
                    produtividadeMediaDto.UsuarioIDCriacao = new Guid(userLogin);
                    result.Success = await _appServiceProdutividade.InsertAsync(produtividadeMediaDto);
                    var descricao = $"Inseriu uma Produtividade Media com o nome {produtividadeMediaDto.Nome}";
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
        /// Metodo para alterar ProdutividadeMedia
        /// </summary>
        /// <param name="produtividadeMediaDto">Object ProdutividadeMediaDto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updateaverage")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ProdutividadeMedia_Editar")]
        public async Task<GenericResult<ProdutividadeMediaDto>> Put(ProdutividadeMediaDto produtividadeMediaDto)
        {
            var result = new GenericResult<ProdutividadeMediaDto>();
            var validation = _validator.Validate(produtividadeMediaDto);
            LogDto logDto = null;
            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    produtividadeMediaDto.UsuarioIDAlteracao = new Guid(userLogin);
                    produtividadeMediaDto.DataAlteracao = DateTime.Now;
                    result.Success = await _appServiceProdutividade.Update(produtividadeMediaDto);
                    var descricao = $"Atualizou a Produtividade Media com o nome {produtividadeMediaDto.Nome}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, produtividadeMediaDto.ID);
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
        /// Metodo para inativar uma ProdutividadeMedia
        /// </summary>
        /// <param name="id">Guid da ProdutividadeMedia</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactiveProdutividadeMedia/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ProdutividadeMedia_Excluir")]
        public async Task<GenericResult<ProdutividadeMediaDto>> Delete(Guid id)
        {
            var result = new GenericResult<ProdutividadeMediaDto>();
            LogDto logDto;
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _appServiceProdutividade.Inactive(id);

                var descricao = $" Usuario {userLogin} desativou a Produtividade Media ID: {id}";
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
