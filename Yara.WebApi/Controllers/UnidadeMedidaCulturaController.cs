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
    [Authorize]
    [RoutePrefix("unitofmeasurementculture")]
    public class UnidadeMedidaCulturaController : ApiController
    {
        private readonly IAppServiceUnidadeMedidaCultura _unidadeMedidaCultura;
        private readonly UnidadeMedidaCulturaValidator _validator;
        private readonly IAppServiceLog _log;

        public UnidadeMedidaCulturaController(IAppServiceUnidadeMedidaCultura unidademedidacultura, IAppServiceLog log)
        {
            _validator = new UnidadeMedidaCulturaValidator();
            _unidadeMedidaCultura = unidademedidacultura;
            _log = log;
        }
        /// <summary>
        /// Lista todas as unidades de medida de culturas
        /// </summary>
        /// <param name="options">Lista de unidades de medida de culturas</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getunitofmeasurementculture")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "UnidadeMedidaCultura_Acesso")]
        public async Task<GenericResult<IQueryable<UnidadeMedidaCulturaDto>>> Get(ODataQueryOptions<UnidadeMedidaCulturaDto> options)
        {

            var result = new GenericResult<IQueryable<UnidadeMedidaCulturaDto>>();
            try
            {
                var unidadeMedidaCultura = await _unidadeMedidaCultura.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(unidadeMedidaCultura.AsQueryable(), new ODataQuerySettings()).Cast<UnidadeMedidaCulturaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(unidadeMedidaCultura.AsQueryable()).Cast<UnidadeMedidaCulturaDto>();
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
        /// Busca unidades de medida de culturas por Código
        /// </summary>
        /// <param name="id">Código da unidades de medida de culturas</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodunitofmeasurementculture/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "UnidadeMedidaCultura_Editar")]
        public async Task<GenericResult<UnidadeMedidaCulturaDto>> Get(Guid id)
        {
            var result = new GenericResult<UnidadeMedidaCulturaDto>();

            try
            {
                result.Result = await _unidadeMedidaCultura.GetAsync(g => g.ID.Equals(id));
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
        /// Adiciona uma nova unidade de medida de cultura
        /// </summary>
        /// <param name="value">Unidade de medida de culturas </param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertunitofmeasurementculture")]
        [ResponseType(typeof(UnidadeMedidaCulturaDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "UnidadeMedidaCultura_Inserir")]
        public GenericResult<UnidadeMedidaCulturaDto> Post(UnidadeMedidaCulturaDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<UnidadeMedidaCulturaDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    value.UsuarioIDCriacao = new Guid(userid);
                    result.Success = _unidadeMedidaCultura.Insert(value);
                    var descricao = $"Inseriu uma unidade de medida de cultura do nome {value.Nome}";
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
                result.Errors = validationResult.GetErrors();


            return result;
        }
        /// <summary>
        /// Atualiza uma unidade de medida de cultura
        /// </summary>
        /// <param name="value">Unidade de medida de cultura</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(TipoGarantiaDto))]
        [Route("v1/updateunitofmeasurementculture")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "UnidadeMedidaCultura_Editar")]
        public async Task<GenericResult<UnidadeMedidaCulturaDto>> Put(UnidadeMedidaCulturaDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<UnidadeMedidaCulturaDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {

                    value.UsuarioIDAlteracao = new Guid(userid);
                    result.Success = await _unidadeMedidaCultura.Update(value);
                    var descricao = $"Atualizou uma unidade de medida de cultura com o nome {value.Nome}";
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
                result.Errors = validationResult.GetErrors();


            return result;
        }

    }
}
