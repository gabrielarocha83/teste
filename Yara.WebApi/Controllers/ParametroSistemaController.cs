using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
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
    [RoutePrefix("system")]
    [Authorize]
    public class ParametroSistemaController : ApiController
    {
        private readonly IAppServiceParametroSistema _sistema;
        private readonly ParametroSistemaValidator _validator;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sistema"></param>
        /// <param name="log"></param>
        public ParametroSistemaController(IAppServiceParametroSistema sistema, IAppServiceLog log)
        {
            _sistema = sistema;
            _log = log;
            _validator = new ParametroSistemaValidator();
        }

        /// <summary>
        /// Lista todos parametros do sistema cadastrado
        /// </summary>
        /// <param name="options">Lista de parametros</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getparameters")]
        public async Task<GenericResult<IQueryable<ParametroSistemaDto>>> Get(ODataQueryOptions<ParametroSistemaDto> options)
        {
            var result = new GenericResult<IQueryable<ParametroSistemaDto>>();

            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();

                var param = await _sistema.GetAllAsync(empresa);
                var totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(param.AsQueryable(), new ODataQuerySettings()).Cast<ParametroSistemaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(param.AsQueryable()).Cast<ParametroSistemaDto>();
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
        /// Busca parametro de sistema cadastro pelo ID
        /// </summary>
        /// <param name="id">ID do parametro</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodparameters/{id:guid}")]
        public async Task<GenericResult<ParametroSistemaDto>> Get(Guid id)
        {
            var result = new GenericResult<ParametroSistemaDto>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                result.Result = await _sistema.GetAsync(g => g.ID.Equals(id) && g.EmpresasID.Equals(empresa));
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
        /// Busca parametro de sistema cadastro pelo Tipo
        /// </summary>
        /// <param name="tipo">Tipo do Parametro</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodparameterstype/{tipo}")]
        public async Task<GenericResult<IEnumerable<ParametroSistemaDto>>> Get(string tipo)
        {
            var result = new GenericResult<IEnumerable<ParametroSistemaDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                result.Result = await _sistema.GetAllFilterAsync(g => g.Tipo.Equals(tipo) && g.EmpresasID.Equals(empresa));
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
        /// Adiciona um novo parametro de sistema
        /// </summary>
        /// <param name="value">Obj ParametroSistemaDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertparameters")]
        [ResponseType(typeof(ParametroSistemaDto))]
        public GenericResult<ParametroSistemaDto> Post(ParametroSistemaDto value)
        {
            var result = new GenericResult<ParametroSistemaDto>();
            var validationResult = _validator.Validate(value);

            if (validationResult.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    value.UsuarioIDCriacao = new Guid(userid);
                   
                    var empresa = Request.Properties["Empresa"].ToString();
                    value.EmpresasID = empresa;
                    result.Success = _sistema.Insert(value);

                    var descricao = $"Inseriu um parametro de sistema com chave: {value.Chave}, e valor: {value.Valor} da Empresa {empresa}";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
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
        /// Altera um parametro de sistema 
        /// </summary>
        /// <param name="value">Obj ParametroSistemaDto</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ParametroSistemaDto))]
        [Route("v1/updateparameters")]
        public async Task<GenericResult<ParametroSistemaDto>> Put(ParametroSistemaDto value)
        {
            var result = new GenericResult<ParametroSistemaDto>();
      
            var validationResult = _validator.Validate(value);
            if (validationResult.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();

                    value.UsuarioIDAlteracao = new Guid(userid);
                    value.EmpresasID = empresa;
                    result.Success = await _sistema.Update(value);
                   
                    var descricao = $"Alterou o parametro de sistema com a chave: {value.Chave}, e valor: {value.Valor} da Empresa {empresa}";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
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
        /// Inativa um parametro de sistema
        /// </summary>
        /// <param name="id">ID do parametro de sistema</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactiveparameters/{id:guid}")]
        public async Task<GenericResult<ParametroSistemaDto>> Delete(Guid id)
        {
            var result = new GenericResult<ParametroSistemaDto>();
            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var empresa = Request.Properties["Empresa"].ToString();

                result.Success = await _sistema.Inactive(id);

                var param = await _sistema.GetAsync(c => c.ID.Equals(id));
                param.EmpresasID = empresa;

                var descricao = $"Inativou um o parametro da chave: {param.Chave}, e valor: {param.Valor} da Empresa {empresa}";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, id);
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

        /// <summary>
        /// Insere ou altera um parametro do sistema
        /// </summary>
        /// <param name="value">Obj ParametroSistemaDto</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/generalparameters")]
        [ResponseType(typeof(ParametroSistemaDto))]
        public async Task<GenericResult<ParametroSistemaDto>> PostGeral(ParametroSistemaDto value)
        {
            var result = new GenericResult<ParametroSistemaDto>();

            var validationResult = _validator.Validate(value);
            if (validationResult.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();
                    value.EmpresasID = empresa;

                    result.Result = await _sistema.GetAsync(g => g.Tipo.Equals(value.Tipo) && g.EmpresasID.Equals(empresa));

                    var descricao = "";
                    if (result.Result == null)
                    {
                        //Insert
                        descricao = $"Inseriu um parametro de sistema com tipo: {value.Tipo}, e valor: {value.Valor} da Empresa {empresa}";
                        value.UsuarioIDCriacao = new Guid(userid);
                        result.Success = _sistema.Insert(value);
                    }
                    else
                    {
                        //Update
                        descricao = $"Alteradou um parametro de sistema com tipo: {value.Tipo}, e valor: {value.Valor} da Empresa {empresa}";
                        value.ID = result.Result.ID;
                        value.UsuarioIDAlteracao = new Guid(userid);
                        result.Result = null;
                        result.Success = await _sistema.Update(value);
                    }

                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
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
        /// Lista de um parametro de sistema 
        /// </summary>
        /// <param name="value">Obj ParametroSistemaDto</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ParametroSistemaDto))]
        [Route("v1/updatelistparameters")]
        public async Task<GenericResult<ParametroSistemaDto>> PutList(List<ParametroSistemaDto> value)
        {
            var result = new GenericResult<ParametroSistemaDto>();

            //var validationResult = _validator.Validate(value);
            //if (validationResult.IsValid)
            //{
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                    foreach (var item in value)
                    {
                        item.DataAlteracao = DateTime.Now;
                        item.UsuarioIDAlteracao = new Guid(userid);

                        result.Success = await _sistema.Update(item);

                        var descricao = $"Alterou o parametro de sistema {item.Grupo}, {item.Chave} : {item.Valor}";
                        var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
                        _log.Create(logDto);
                    }
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Errors = new[] { Resources.Resources.Error };
                    var error = new ErrorsYara();
                    error.ErrorYara(e);
                }
            //}
            //else
            //    result.Errors = validationResult.GetErrors();

            return result;
        }
    }
}
