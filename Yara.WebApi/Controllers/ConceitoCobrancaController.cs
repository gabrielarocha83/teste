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
    [RoutePrefix("billingconcepts")]
    [Authorize]
    public class ConceitoCobrancaController : ApiController
    {
        private readonly IAppServiceConceitoCobranca _conceitocobranca;
        private readonly ConceitoCobrancaValidator _validator;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="conceitocobranca"></param>
        /// <param name="log"></param>
        public ConceitoCobrancaController(IAppServiceConceitoCobranca conceitocobranca, IAppServiceLog log)
        {
            _validator = new ConceitoCobrancaValidator();
            _conceitocobranca = conceitocobranca;
            _log = log;
        }

        /// <summary>
        /// Retorna todos os conceitos de cobrança, possibilitando utilizar filtros OData
        /// </summary>
        /// <param name="options">Filtros OData</param>
        /// <returns>Lista de conceito de cobrança</returns>
        [HttpGet]
        [Route("v1/billingconcepts")]
        public async Task<GenericResult<IQueryable<ConceitoCobrancaDto>>> Get(ODataQueryOptions<ConceitoCobrancaDto> options)
        {
           
            var result = new GenericResult<IQueryable<ConceitoCobrancaDto>>();
            try
            {
                var conceito = await _conceitocobranca.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(conceito.AsQueryable(), new ODataQuerySettings()).Cast<ConceitoCobrancaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(conceito.AsQueryable()).Cast<ConceitoCobrancaDto>();
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
        /// Busca Conceito de cobrança por código
        /// </summary>
        /// <param name="id">Código do Conceito</param>
        /// <returns>Unico Grupo</returns>
        [HttpGet]
        [Route("v1/getcodbillingconcept/{id:guid}")]
        public async Task<GenericResult<ConceitoCobrancaDto>> Get(Guid id)
        {
            var result = new GenericResult<ConceitoCobrancaDto>();
            try
            {
                result.Result = await _conceitocobranca.GetAsync(c=>c.ID.Equals(id));
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
        /// Insere um novo conceito de cobrança
        /// </summary>
        /// <param name="value">ConceitoCobranca</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ConceitoCobrancaDto))]
        [Route("v1/insertbillingconcept")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ConceitoCobranca_Inserir")]
        public GenericResult<ConceitoCobrancaDto> Post(ConceitoCobrancaDto value)
        {
            var user = User.Identity as ClaimsIdentity;
            var userID = user.Claims.First(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<ConceitoCobrancaDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    value.ID = Guid.NewGuid();
                    value.UsuarioIDCriacao = new Guid(userID);
                    result.Success = _conceitocobranca.Insert(value);

                    var descricao = $"Inseriu um Conceito de Cobrança com o nome {value.Nome}";
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
        /// Atualiza um Conceito de Cobrança
        /// </summary>
        /// <param name="value">Conceito de Cobrança</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(GrupoDto))]
        [Route("v1/updatebillingconcept")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ConceitoCobranca_Editar")]
        public async Task<GenericResult<ConceitoCobrancaDto>> Put(ConceitoCobrancaDto value)
        {
            var result = new GenericResult<ConceitoCobrancaDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    var user = User.Identity as ClaimsIdentity;
                    var userID = user.Claims.First(c => c.Type.Equals("Usuario")).Value;



                    value.UsuarioIDAlteracao = new Guid(userID);
                    result.Success = await _conceitocobranca.Update(value);
                    var descricao = $"Atualizou um Conceito de cobrança com o nome {value.Nome}";
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
        /// Inativa um Conceito de Cobrança
        /// </summary>
        /// <param name="id">Código do conceito de cobrança</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactivebillingconcept/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ConceitoCobranca_Excluir")]
        public async Task<GenericResult<ConceitoCobrancaDto>> Delete(Guid id)
        {
            var result = new GenericResult<ConceitoCobrancaDto>();
            LogDto logDto;
            try
            {
                var grupo = await _conceitocobranca.GetAsync(c => c.ID.Equals(id));
                result.Success = await _conceitocobranca.Inactive(id);

                var descricao = $"Inativou um Conceito de Cobrança com o nome {grupo.Nome}";
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
