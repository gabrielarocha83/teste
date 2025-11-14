using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("guarantee")]
    [Authorize]
    public class ContaClienteGarantiaController : ApiController
    {
        private readonly IAppServiceContaClienteGarantia _clienteGarantia;
        private IAppServiceContaClienteParticipanteGarantia _clienteParticipanteGarantia;
        private IAppServiceContaClienteResponsavelGarantia _clienteResponsavelGarantia;
        private readonly IAppServiceLog _log;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="log"></param>
        /// <param name="clienteResponsavelGarantia"></param>
        /// <param name="clienteParticipanteGarantia"></param>
        /// <param name="clienteGarantia"></param>
        public ContaClienteGarantiaController(IAppServiceLog log, IAppServiceContaClienteResponsavelGarantia clienteResponsavelGarantia, IAppServiceContaClienteParticipanteGarantia clienteParticipanteGarantia, IAppServiceContaClienteGarantia clienteGarantia)
        {
            _clienteGarantia = clienteGarantia;
            _clienteParticipanteGarantia = clienteParticipanteGarantia;
            _clienteResponsavelGarantia = clienteResponsavelGarantia;
            _log = log;
        }

        /// <summary>
        /// Metodo que retorna todas as garantias do cliente
        /// </summary>
        /// <param name="options">OData Filtros ex: $filter=Tipo eq Bovino</param>
        /// <param name="documento">Numero do documento do cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getguarantee/{documento}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Garantias_Visualizar")]
        public async Task<GenericResult<IQueryable<ContaClienteGarantiaDto>>> Get(ODataQueryOptions<ContaClienteGarantiaDto> options, string documento)
        {
            var result = new GenericResult<IQueryable<ContaClienteGarantiaDto>>();

            try
            {
                var ret = await _clienteGarantia.GetAllFilterAsyncAll(documento);
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(ret.AsQueryable(), new ODataQuerySettings()).Cast<ContaClienteGarantiaDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(ret.AsQueryable()).Cast<ContaClienteGarantiaDto>();
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
        /// Metodo que retorna os dados da garantia de acordo com Guid
        /// </summary>
        /// <param name="id">Guid da Garantia</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcodguarantee/{id:guid}")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Garantias_Visualizar")]
        public async Task<GenericResult<ContaClienteGarantiaDto>> Get(Guid id)
        {
            var result = new GenericResult<ContaClienteGarantiaDto>();

            try
            {
                result.Result = await _clienteGarantia.GetAsync(g => g.ID.Equals(id));
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
        /// Metodo para inserir garantias
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertguarantee")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Garantias_Inserir")]
        public async Task<GenericResult<ContaClienteGarantiaDto>> Insert(ContaClienteGarantiaDto options)
        {
            var result = new GenericResult<ContaClienteGarantiaDto>();
            //var validationResult = _validator.Validate(culturaDto);

            //if (validationResult.IsValid)
            //{
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                options.ID = Guid.NewGuid();
                options.DataCriacao = DateTime.Now;
                options.UsuarioIDCriacao = new Guid(userLogin);
                options.EmpresasID = empresa;

                var retorno = await _clienteGarantia.InsertAsync(options);
                result.Success = retorno != null;

                var descricao = $"Adicionou a garantia {retorno.CodigoAmigavel} do tipo '{options.TipoGarantiaAmigavel}'";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, options.ContaClienteID);
                _log.Create(logDto);
            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { ex.Message };
                var error = new ErrorsYara();
                error.ErrorYara(ex);
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
            //{
            //    result.Errors = validationResult.GetErrors();
            //}

            return result;
        }

        /// <summary>
        /// Metodo para inserir uma listagem de garantias
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertlistguarantee")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Garantias_Inserir")]
        public async Task<GenericResult<ContaClienteGarantiaDto>> InsertList(List<ContaClienteGarantiaDto> options)
        {
            var result = new GenericResult<ContaClienteGarantiaDto>();
            //var validationResult = _validator.Validate(culturaDto);

            //if (validationResult.IsValid)
            //{
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                foreach (var item in options)
                {
                    item.ID = Guid.NewGuid();
                    item.DataCriacao = DateTime.Now;
                    item.UsuarioIDCriacao = new Guid(userLogin);
                    item.EmpresasID = empresa;

                    var retorno = await _clienteGarantia.InsertAsync(item);
                    result.Success = retorno != null;

                    var descricao = $"Adicionou a garantia {retorno.CodigoAmigavel} do tipo '{item.TipoGarantiaAmigavel}'";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, item.ContaClienteID);
                    _log.Create(logDto);
                }
            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { ex.Message };
                var error = new ErrorsYara();
                error.ErrorYara(ex);
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
            //{
            //    result.Errors = validationResult.GetErrors();
            //}

            return result;
        }

        /// <summary>
        /// Metodo para alterar garantias
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updatelistguarantee")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "Garantias_Editar")]
        public async Task<GenericResult<ContaClienteGarantiaDto>> Put(List<ContaClienteGarantiaDto> options)
        {
            var result = new GenericResult<ContaClienteGarantiaDto>();

            //var validationResult = _validator.Validate(culturaDto);
            //if (validationResult.IsValid)
            //{
            try
            {
                var objuserLogin = User.Identity as ClaimsIdentity;
                var userLogin = objuserLogin.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresa = Request.Properties["Empresa"].ToString();

                foreach (var item in options)
                {
                    item.UsuarioIDAlteracao = new Guid(userLogin);
                    item.DataAlteracao = DateTime.Now;
                    item.EmpresasID = empresa;

                    var retorno = await _clienteGarantia.UpdateAsync(item);
                    result.Success = retorno != null;

                    var descricao = $"Atualizou a garantia {retorno.CodigoAmigavel} do tipo '{retorno.TipoGarantiaAmigavel}'";
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info, item.ContaClienteID);
                    _log.Create(logDto);
                }
            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { ex.Message };
                var error = new ErrorsYara();
                error.ErrorYara(ex);
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
            //{
            //result.Errors = validationResult.GetErrors();
            //}

            return result;
        }
    }
}
