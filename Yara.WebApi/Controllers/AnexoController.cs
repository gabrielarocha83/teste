using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    [RoutePrefix("documents")]
    [Authorize]
    public class AnexoController : ApiController
    {
        private readonly IAppServiceAnexo _anexo;
        private readonly IAppServiceLog _log;
        private readonly AnexoValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="anexo"></param>
        /// <param name="appServiceLog"></param>
        public AnexoController(IAppServiceAnexo anexo, IAppServiceLog appServiceLog)
        {
            _anexo = anexo;
            _log = appServiceLog;
            _validator = new AnexoValidator();
        }

        /// <summary>
        /// Metodo que retorna a lista de tipo de anexo
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Nome eq irrigação</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getattachment")]
        public async Task<GenericResult<IQueryable<AnexoDto>>> Get(ODataQueryOptions<AnexoDto> options)
        {
            var result = new GenericResult<IQueryable<AnexoDto>>();

            try
            {
                var retarea = await _anexo.GetAllAsync();
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(retarea.AsQueryable(), new ODataQuerySettings()).Cast<AnexoDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(retarea.AsQueryable()).Cast<AnexoDto>();
                result.Count = totalReg > 0 ? totalReg : result.Result.Count();

                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { Resources.Resources.Error };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;
        }

        /// <summary>
        /// Metodo que retorna a lista de tipo de anexo
        /// </summary>
        /// <param name="anexo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/getbylayout")]
        public async Task<GenericResult<IQueryable<AnexoDto>>> Get(AnexoArquivoByPropostaContaClienteDto anexo)
        {
            var result = new GenericResult<IQueryable<AnexoDto>>();

            try
            {
                var retarea = await _anexo.GetAllFilterAsyncEspecifico(anexo);

                result.Result = retarea.Where(r => r.LayoutsProposta.Split(',').Any(a => a == anexo.LayoutProposta)).AsQueryable();
                result.Count = result.Result.Count();

                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { Resources.Resources.Error };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }
            return result;
        }

        /// <summary>
        /// Metodo que retorna os dados tipo de anexo de acordo com Guid
        /// </summary>
        /// <param name="id">Guid da area</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodattachment/{id:guid}")]
        public async Task<GenericResult<AnexoDto>> Get(Guid id)
        {
            var result = new GenericResult<AnexoDto>();

            try
            {
                result.Result = await _anexo.GetAsync(g => g.ID.Equals(id));
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { Resources.Resources.Error };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;
        }

        /// <summary>
        /// Metodo para inserir um tipo de anexo
        /// </summary>
        /// <param name="anexoDto">Object AnexoDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertattachment")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Anexo_Inserir")]
        public async Task<GenericResult<AnexoDto>> Post(AnexoDto anexoDto)
        {
            var result = new GenericResult<AnexoDto>();
            var validation = _validator.Validate(anexoDto);

            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    anexoDto.ID = Guid.NewGuid();
                    anexoDto.DataCriacao = DateTime.Now;
                    anexoDto.UsuarioIDCriacao = new Guid(userLogin);

                    result.Success = await _anexo.InsertAsync(anexoDto);

                    var descricao = $"Inseriu um novo tipo de anexo chamado '{anexoDto.Descricao}'";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
                    _log.Create(logDto);
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Errors = new[] { Resources.Resources.Error };
                    var logger = log4net.LogManager.GetLogger("YaraLog");
                    logger.Error(e.Message);
                }
            }
            else
            {
                result.Errors = validation.GetErrors();
            }
            return result;
        }

        /// <summary>
        /// Metodo para alterar um tipo de anexo
        /// </summary>
        /// <param name="anexoDto">Object AnexoDto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updateattachment")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Anexo_Editar")]
        public async Task<GenericResult<AnexoDto>> Put(AnexoDto anexoDto)
        {
            var result = new GenericResult<AnexoDto>();
            var validation = _validator.Validate(anexoDto);

            if (validation.IsValid)
            {
                try
                {
                    var objuserLogin = User.Identity as ClaimsIdentity;
                    var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    anexoDto.UsuarioIDAlteracao = new Guid(userLogin);
                    anexoDto.DataAlteracao = DateTime.Now;

                    result.Success = await _anexo.Update(anexoDto);

                    var descricao = $"Atualizou anexo de documento da descrição: {anexoDto.Descricao}";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, anexoDto.ID);
                    _log.Create(logDto);
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Errors = new[] { Resources.Resources.Error };
                    var logger = log4net.LogManager.GetLogger("YaraLog");
                    logger.Error(e.Message);
                }
            }
            else
                result.Errors = validation.GetErrors();

            return result;
        }

        /// <summary>
        /// Metodo para inativar um tipo de anexo
        /// </summary>
        /// <param name="id">Guid da area</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactiveattachment/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Anexo_Excluir")]
        public async Task<GenericResult<AnexoDto>> Delete(Guid id)
        {
            var result = new GenericResult<AnexoDto>();
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _anexo.Inactive(id);

                var descricao = $" Usuario {userLogin} desativou um anexo ID: {id}";
                var level = EnumLogLevelDto.Info;
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, id);
                _log.Create(logDto);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { Resources.Resources.Error };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;
        }
    }
}
