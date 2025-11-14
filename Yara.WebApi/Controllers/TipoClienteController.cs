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
    [RoutePrefix("clienttypes")]
    public class TipoClienteController : ApiController
    {
        private readonly IAppServiceTipoCliente _tipocliente;
        private readonly TipoClienteValidator _validator;
        private readonly IAppServiceLog _log;

        public TipoClienteController(IAppServiceTipoCliente tipoCliente, IAppServiceLog log)
        {
            _validator = new TipoClienteValidator();
            _tipocliente = tipoCliente;
            _log = log;
        }

        /// <summary>
        /// Lista todos os tipos de clientes
        /// </summary>
        /// <param name="options">Lista de Tipos de Cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getclienttypes")]
        public async Task<GenericResult<IQueryable<TipoClienteDto>>> Get(ODataQueryOptions<TipoClienteDto> options)
        {

            var result = new GenericResult<IQueryable<TipoClienteDto>>();
            try
            {
                var tipocliente = await _tipocliente.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(tipocliente.AsQueryable(), new ODataQuerySettings()).Cast<TipoClienteDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(tipocliente.AsQueryable()).Cast<TipoClienteDto>();
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
        [Route("v1/getcodclienttype/{id:guid}")]
        public async Task<GenericResult<TipoClienteDto>> Get(Guid id)
        {
            var result = new GenericResult<TipoClienteDto>();

            try
            {
                result.Result = await _tipocliente.GetAsync(g => g.ID.Equals(id));
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
        [Route("v1/getcodclienttypeaccount/{id:guid}")]
        public async Task<GenericResult<TipoClienteDto>> GetAccount(Guid id)
        {
            var result = new GenericResult<TipoClienteDto>();

            try
            {
                result.Result = await _tipocliente.GetAsync(g => g.ID.Equals(id));
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
        [Route("v1/insertclienttype")]
        [ResponseType(typeof(TipoClienteDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoCliente_Inserir")]
        public GenericResult<TipoClienteDto> Post(TipoClienteDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<TipoClienteDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    value.UsuarioIDCriacao = new Guid(userid);
                    result.Success = _tipocliente.Insert(value);

                    var descricao = $"Inseriu um Tipo de Cliente do nome {value.Nome}";
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
        [ResponseType(typeof(TipoClienteDto))]
        [Route("v1/updateclienttype")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoCliente_Editar")]
        public async Task<GenericResult<TipoClienteDto>> Put(TipoClienteDto value)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<TipoClienteDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                   
                    value.UsuarioIDAlteracao = new Guid(userid);
                    result.Success = await _tipocliente.Update(value);
                    var descricao = $"Atualizou um Tipo de Cliente do nome {value.Nome}";
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
        [Route("v1/inactiveclienttype/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoCliente_Excluir")]
        public async Task<GenericResult<TipoClienteDto>> Delete(Guid id)
        {
            var result = new GenericResult<TipoClienteDto>();
            LogDto logDto;
            try
            {
                var tipocliente = await _tipocliente.GetAsync(c => c.ID.Equals(id));
                result.Success = await _tipocliente.Inactive(id);

                var descricao = $"Inativou um Tipo de Cliente do nome {tipocliente.Nome}";
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
