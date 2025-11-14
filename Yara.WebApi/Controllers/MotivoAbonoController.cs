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
    [RoutePrefix("motivoabono")]
    [Authorize]
    public class MotivoAbonoController : ApiController
    {
        private readonly IAppServiceMotivoAbono _motivoabono;
        private readonly MotivoAbonoValidator _validator;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="motivoabono"></param>
        /// <param name="log"></param>
        public MotivoAbonoController(IAppServiceMotivoAbono motivoabono, IAppServiceLog log)
        {
            _validator = new MotivoAbonoValidator();
            _motivoabono = motivoabono;
            _log = log;
        }

        /// <summary>
        /// Retorna todos os motivos de abonos, possibilitando utilizar filtros OData.
        /// </summary>
        /// <param name="options">Filtros OData</param>
        /// <returns>Lista de MotivoAbonoDto</returns>
        [HttpGet]
        [Route("v1/motivosabonos")]
        public async Task<GenericResult<IQueryable<MotivoAbonoDto>>> Get(ODataQueryOptions<MotivoAbonoDto> options)
        {
            var result = new GenericResult<IQueryable<MotivoAbonoDto>>();

            try
            {
                var motivo = await _motivoabono.GetAllAsync();
                int totalReg = 0;
          
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(motivo.AsQueryable(), new ODataQuerySettings()).Cast<MotivoAbonoDto>();
                    totalReg = filtro.Count();
                }

                result.Result = options.ApplyTo(motivo.AsQueryable()).Cast<MotivoAbonoDto>();
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
        /// Busca motivo do abono pelo código.
        /// </summary>
        /// <param name="id">Código do Motivo do Abono</param>
        /// <returns>MotivoAbonoDto</returns>
        [HttpGet]
        [Route("v1/consultarmotivoabono/{id:guid}")]
        public async Task<GenericResult<MotivoAbonoDto>> Get(Guid id)
        {
            var result = new GenericResult<MotivoAbonoDto>();

            try
            {
                result.Result = await _motivoabono.GetAsync(c => c.ID.Equals(id));
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
        /// Insere um novo motivo do abono.
        /// </summary>
        /// <param name="value">MotivoAbonoDto</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(MotivoAbonoDto))]
        [Route("v1/inserirmotivoabono")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "MotivoAbono_Inserir")]
        public GenericResult<MotivoAbonoDto> Post(MotivoAbonoDto value)
        {
            var result = new GenericResult<MotivoAbonoDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;

            if (validationResult.IsValid)
            {
                try
                {
                    var user = User.Identity as ClaimsIdentity;
                    var userID = user.Claims.First(c => c.Type.Equals("Usuario")).Value;

                    value.ID = Guid.NewGuid();
                    value.UsuarioIDCriacao = new Guid(userID);
                    result.Success = _motivoabono.Insert(value);

                    var descricao = $"Inseriu um Motivo do Abono com o nome {value.Nome}";
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
        /// Atualiza um Motivo do Abono
        /// </summary>
        /// <param name="value">MotivoAbonoDto</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(GrupoDto))]
        [Route("v1/atualizarmotivoabono")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "MotivoAbono_Editar")]
        public async Task<GenericResult<MotivoAbonoDto>> Put(MotivoAbonoDto value)
        {
            var result = new GenericResult<MotivoAbonoDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;

            if (validationResult.IsValid)
            {
                try
                {
                    var user = User.Identity as ClaimsIdentity;
                    var userID = user.Claims.First(c => c.Type.Equals("Usuario")).Value;

                    value.UsuarioIDAlteracao = new Guid(userID);
                    result.Success = await _motivoabono.Update(value);

                    var descricao = $"Atualizou um Motivo do Abono com o nome {value.Nome}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, value.ID);
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
        /// Inativa um Motivo de Abono.
        /// </summary>
        /// <param name="id">Código do Motivo do Abono</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inativarmotivoabono/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "MotivoAbono_Excluir")]
        public async Task<GenericResult<MotivoAbonoDto>> Delete(Guid id)
        {
            var result = new GenericResult<MotivoAbonoDto>();
            LogDto logDto = null;

            try
            {
                var grupo = await _motivoabono.GetAsync(c => c.ID.Equals(id));
                result.Success = await _motivoabono.Inactive(id);

                var descricao = $"Inativou um Motivo do Abono com o nome {grupo.Nome}";
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
