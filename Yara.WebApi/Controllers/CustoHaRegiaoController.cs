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
    [RoutePrefix("costharegion")]
    [Authorize]
    public class CustoHaRegiaoController : ApiController
    {
        private readonly IAppServiceCustoHaRegiao _appServiceCustoHaRegiao;
        private readonly IAppServiceLog _log;
        private readonly CustoHaRegiaoValidator _custoValidator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServiceCustoHaRegiao"></param>
        /// <param name="appServiceLog"></param>
        public CustoHaRegiaoController(IAppServiceCustoHaRegiao appServiceCustoHaRegiao, IAppServiceLog appServiceLog)
        {
            _appServiceCustoHaRegiao = appServiceCustoHaRegiao;
            _log = appServiceLog;
            _custoValidator = new CustoHaRegiaoValidator();
        }

        /// <summary>
        /// Metodo que retorna todos os custos de Ha por Região
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Tipo eq Bovino</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcostharegion")]
        public async Task<GenericResult<IQueryable<CustoHaRegiaoDto>>> Get(ODataQueryOptions<CustoHaRegiaoDto> options)
        {
            var result = new GenericResult<IQueryable<CustoHaRegiaoDto>>();
            try
            {
                var allCostRegion = await _appServiceCustoHaRegiao.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(allCostRegion.AsQueryable(), new ODataQuerySettings()).Cast<CustoHaRegiaoDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(allCostRegion.AsQueryable()).Cast<CustoHaRegiaoDto>();
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
        /// Metodo que retorna todos os custos de Ha da Cidade Solicitada
        /// </summary>
        /// <param name="id">ID da Cidade</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getbycity/{id:guid}")]
        public async Task<GenericResult<CustoHaRegiaoDto>> GetByCity(Guid id)
        {
            var result = new GenericResult<CustoHaRegiaoDto>();
            try
            {
                var cost = await _appServiceCustoHaRegiao.GetAsync(asc => asc.CidadeID == id);
                result.Result = cost;
                result.Count = cost == null ? 0 : 1;

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
        /// Metodo que retorna os os custos de Ha por Região de acordo com o ID
        /// </summary>
        /// <param name="id">ID da custos de Ha por Região</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodcostharegion/{id:guid}")]
        public async Task<GenericResult<CustoHaRegiaoDto>> Get(Guid id)
        {
            var result = new GenericResult<CustoHaRegiaoDto>();
            try
            {
                result.Result = await _appServiceCustoHaRegiao.GetAsync(g => g.ID.Equals(id));
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
        /// Metodo para inserir novo custo de Ha por Região
        /// </summary>
        /// <param name="CustoHaRegiaoDto">Object  custo de Ha por Região</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertcostharegion")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "CustoHaRegiao_Inserir")]
        public async Task<GenericResult<CustoHaRegiaoDto>> Post(CustoHaRegiaoDto CustoHaRegiaoDto)
        {

            CustoHaRegiaoDto.ID = Guid.NewGuid();
            var objuserLogin = User.Identity as ClaimsIdentity;
            var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

            var result = new GenericResult<CustoHaRegiaoDto>();
            var costRegion = _custoValidator.Validate(CustoHaRegiaoDto);

            if (costRegion.IsValid)
            {

                try
                {
                    var custo = await _appServiceCustoHaRegiao.GetAsync(g => g.CidadeID.Equals(CustoHaRegiaoDto.CidadeID));

                    if (custo != null)
                    {
                        result.Success = false;
                        result.Errors = new[] { "Esse Custo do Ha por Região já está cadastrado." };
                        return result;
                    }
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Errors = new[] { Resources.Resources.Error };
                    var error = new ErrorsYara();
                    error.ErrorYara(e);
                    return result;
                }

                try
                {



                    CustoHaRegiaoDto.DataCriacao = DateTime.Now;
                    CustoHaRegiaoDto.UsuarioIDCriacao = new Guid(userLogin);
                    result.Success = await _appServiceCustoHaRegiao.InsertAsync(CustoHaRegiaoDto);
                    var descricao = $"Inseriu um Custo do Ha por Região com o valor cultivável de {CustoHaRegiaoDto.ValorHaCultivavel}, não cultivável de {CustoHaRegiaoDto.ValorHaNaoCultivavel}";
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

            }
            else
            {
                result.Errors = costRegion.GetErrors();
            }
            return result;
        }

        /// <summary>
        /// Metodo para alterar custo de Ha por Região
        /// </summary>
        /// <param name="CustoHaRegiaoDto">Object  custo de Ha por Região</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updatecostharegion")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "CustoHaRegiao_Editar")]
        public async Task<GenericResult<CustoHaRegiaoDto>> Put(CustoHaRegiaoDto CustoHaRegiaoDto)
        {
            var result = new GenericResult<CustoHaRegiaoDto>();
            var costRegionValidation = _custoValidator.Validate(CustoHaRegiaoDto);
            if (costRegionValidation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    CustoHaRegiaoDto.UsuarioIDAlteracao = new Guid(userLogin);
                    CustoHaRegiaoDto.DataAlteracao = DateTime.Now;
                    result.Success = await _appServiceCustoHaRegiao.Update(CustoHaRegiaoDto);
                    var descricao = $"Atualizou um Custo do Ha por Região com o valor cultivável de {CustoHaRegiaoDto.ValorHaCultivavel}, não cultivável de {CustoHaRegiaoDto.ValorHaNaoCultivavel}";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, CustoHaRegiaoDto.ID);
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
                result.Errors = costRegionValidation.GetErrors();
            }
            return result;
        }
    }
}
