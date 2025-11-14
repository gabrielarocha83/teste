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
    [RoutePrefix("livestocktype")]
    [Authorize]
    public class TipoPecuariaController : ApiController
    {
        private readonly IAppServiceTipoPecuaria _tipopecuaria;
        private readonly TipoPecuariaValidator _validator;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="tipoCliente"></param>
        /// <param name="log"></param>
        public TipoPecuariaController(IAppServiceTipoPecuaria tipoCliente, IAppServiceLog log)
        {
            _validator = new TipoPecuariaValidator();
            _tipopecuaria = tipoCliente;
            _log = log;
        }

        /// <summary>
        /// Lista todos os tipos de clientes
        /// </summary>
        /// <param name="options">Lista de Tipos de Cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getlivestocktypes")]
        public async Task<GenericResult<IQueryable<TipoPecuariaDto>>> Get(ODataQueryOptions<TipoPecuariaDto> options)
        {

            var result = new GenericResult<IQueryable<TipoPecuariaDto>>();
            try
            {
                var tipocliente = await _tipopecuaria.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(tipocliente.AsQueryable(), new ODataQuerySettings()).Cast<TipoPecuariaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(tipocliente.AsQueryable()).Cast<TipoPecuariaDto>();
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
        /// Busca tipo de cliente por Código
        /// </summary>
        /// <param name="id">Código do tipo de cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodlivestocktype/{id:guid}")]
        public async Task<GenericResult<TipoPecuariaDto>> Get(Guid id)
        {
            var result = new GenericResult<TipoPecuariaDto>();

            try
            {
                result.Result = await _tipopecuaria.GetAsync(g => g.ID.Equals(id));
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
        /// Adiciona um novo tipo de cliente
        /// </summary>
        /// <param name="value">Tipo de cliente</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertlivestocktype")]
        [ResponseType(typeof(TipoPecuariaDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoPecuaria_Inserir")]
        public GenericResult<TipoPecuariaDto> Post(TipoPecuariaDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<TipoPecuariaDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    value.UsuarioIDCriacao = new Guid(userid);
                    result.Success = _tipopecuaria.Insert(value);

                    var descricao = $"Inseriu um Tipo de Pecuária {value.Tipo}";
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
        /// Atualiza um tipo de cliente 
        /// </summary>
        /// <param name="value">Tipo de cliente</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updatelivestocktype")]
        [ResponseType(typeof(TipoPecuariaDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoPecuaria_Editar")]
        public async Task<GenericResult<TipoPecuariaDto>> Put(TipoPecuariaDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<TipoPecuariaDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                   
                    value.UsuarioIDAlteracao = new Guid(userid);
                    result.Success = await _tipopecuaria.Update(value);
                    var descricao = $"Atualizou um Tipo de Pecuária {value.Tipo}";
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
        /// Inativa um tipo de cliente do sistema
        /// </summary>
        /// <param name="id">Código do tipo de cliente</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactivelivestocktype/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoPecuaria_Excluir")]
        public async Task<GenericResult<TipoPecuariaDto>> Delete(Guid id)
        {
            var result = new GenericResult<TipoPecuariaDto>();
            LogDto logDto;
            try
            {
                var tipocliente = await _tipopecuaria.GetAsync(c => c.ID.Equals(id));
                result.Success = await _tipopecuaria.Inactive(id);

                var descricao = $"Inativou um Tipo de Pecuária {tipocliente.Tipo}";
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
