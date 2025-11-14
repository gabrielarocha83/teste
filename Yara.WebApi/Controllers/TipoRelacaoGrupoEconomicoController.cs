using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.WebApi.Extensions;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("relationshiptypegroup")]
    [Authorize]
    public class TipoRelacaoGrupoEconomicoController : ApiController
    {
        private readonly IAppServiceTipoRelacaoGrupoEconomico _tipoRelacaoGrupoEconomico;
        private readonly IAppServiceLog _log;
        private readonly TipoRelacaoGrupoEconomicoValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="tipoRelacaoGrupoEconomico"></param>
        /// <param name="log"></param>
        public TipoRelacaoGrupoEconomicoController(IAppServiceTipoRelacaoGrupoEconomico tipoRelacaoGrupoEconomico, IAppServiceLog log)
        {
            _tipoRelacaoGrupoEconomico = tipoRelacaoGrupoEconomico;
            _log = log;
            _validator = new TipoRelacaoGrupoEconomicoValidator();
            
        }

        /// <summary>
        /// Metodo que retorna uma lista de Relação por Classificação 1 - LC Compartilhado, 2 - LC Individual, 3 - Relação
        /// </summary>
        /// <param name="id">Código da Classificação</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getrelationshiptypegroupbyclassificationid/{id:int}")]
        public async Task<GenericResult<IEnumerable<TipoRelacaoGrupoEconomicoDto>>> GetGroup(int id)
        {
            var result = new GenericResult<IEnumerable<TipoRelacaoGrupoEconomicoDto>>();
            try
            {
                result.Result = await _tipoRelacaoGrupoEconomico.GetAllFilterAsync(c=>c.ClassificacaoGrupoEconomicoID.Equals(id));
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
        /// Metodo que retorna uma lista de Tipos de Relações de Grupos Economicos
        /// </summary>
        /// <param name="options">OData</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getrelationshiptypegroupall")]
        public async Task<GenericResult<IQueryable<TipoRelacaoGrupoEconomicoDto>>> GetAll(ODataQueryOptions<TipoRelacaoGrupoEconomicoDto> options)
        {
            var result = new GenericResult<IQueryable<TipoRelacaoGrupoEconomicoDto>>();
            try
            {
                var relacao = await _tipoRelacaoGrupoEconomico.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(relacao.AsQueryable(), new ODataQuerySettings()).Cast<TipoRelacaoGrupoEconomicoDto>();
                    totalReg = filtro.Count();
                }

                result.Result = options.ApplyTo(relacao.AsQueryable()).Cast<TipoRelacaoGrupoEconomicoDto>();
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
        ///Busca Tipos de Relações de Grupos Economicos por código
        /// </summary>
        /// <param name="id">Código do Tipos de Relações de Grupos Economicos</param>
        /// <returns>Unica Relação de Grupo Economico</returns>
        [HttpGet]
        [Route("v1/getrelationshiptypegroup/{id:guid}")]
        public async Task<GenericResult<TipoRelacaoGrupoEconomicoDto>> Get(Guid id)
        {
            var result = new GenericResult<TipoRelacaoGrupoEconomicoDto>();
            try
            {
                var groups = await _tipoRelacaoGrupoEconomico.GetAsync(c => c.ID.Equals(id));
               
                result.Result = groups;
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
        /// Insere um tipo de relação do Grupo Economico
        /// </summary>
        /// <param name="value">tipo de relação do Grupo Economico</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(TipoRelacaoGrupoEconomicoDto))]
        [Route("v1/insertrelationshiptypegroup")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoRelacaoGrupoEconomico_Inserir")]
        public GenericResult<TipoRelacaoGrupoEconomicoDto> Post(TipoRelacaoGrupoEconomicoDto value)
        {
            var user = User.Identity as ClaimsIdentity;
            var userId = user?.Claims.First(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<TipoRelacaoGrupoEconomicoDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    value.ID = Guid.NewGuid();
                    if (userId != null) value.UsuarioIDCriacao = new Guid(userId);
                    result.Success =  _tipoRelacaoGrupoEconomico.Insert(value);

                    var descricao = $"Inseriu um Tipo de Relação com o nome {value.Nome}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(user, descricao, level);
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
        /// Atualiza um tipo de relação do Grupo Economico
        /// </summary>
        /// <param name="value">tipo de relação do Grupo Economico</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(TipoRelacaoGrupoEconomicoDto))]
        [Route("v1/updaterelationshiptypegroup")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "TipoRelacaoGrupoEconomico_Editar")]
        public async  Task<GenericResult<TipoRelacaoGrupoEconomicoDto>> Put(TipoRelacaoGrupoEconomicoDto value)
        {
            var user = User.Identity as ClaimsIdentity;
            var userId = user?.Claims.First(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<TipoRelacaoGrupoEconomicoDto>();
            var validationResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validationResult.IsValid)
            {
                try
                {
                    if (userId != null) value.UsuarioIDAlteracao = new Guid(userId);
                    result.Success = await _tipoRelacaoGrupoEconomico.Update(value);

                    var descricao = $"Atualizou um Tipo de Relação com o nome {value.Nome}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(user, descricao, level);
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
