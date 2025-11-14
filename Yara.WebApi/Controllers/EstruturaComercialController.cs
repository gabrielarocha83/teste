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
using Yara.WebApi.Extensions;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("commercialstructure")]
    [Authorize]
    public class EstruturaComercialController : ApiController
    {

        private readonly IAppServiceLog _appServiceLog;
        private readonly IAppServiceUsuario _appServiceUsuario;
        private readonly IAppServiceEstruturaComercial _appServiceEstruturaComercial;
        private readonly IAppServiceEstruturaPerfilUsuario _appServiceEstruturaPerfilUsuario;
        private readonly IAppServiceContaClienteEstruturaComercial _appServiceContaClienteEstrutura;
        private readonly EstruturaComercialValidator _commercialstructureoValidator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="appServiceEstruturaComercial"></param>
        /// <param name="appServiceLog"></param>
        /// <param name="appServiceContaClienteEstrutura"></param>
        /// <param name="appServiceUsuario"></param>
        /// <param name="appServiceEstruturaPerfilUsuario"></param>
        public EstruturaComercialController(IAppServiceLog appServiceLog, IAppServiceUsuario appServiceUsuario, IAppServiceEstruturaComercial appServiceEstruturaComercial, IAppServiceEstruturaPerfilUsuario appServiceEstruturaPerfilUsuario, IAppServiceContaClienteEstruturaComercial appServiceContaClienteEstrutura)
        {
            _appServiceLog = appServiceLog;
            _appServiceUsuario = appServiceUsuario;
            _appServiceEstruturaComercial = appServiceEstruturaComercial;
            _appServiceContaClienteEstrutura = appServiceContaClienteEstrutura;
            _appServiceEstruturaPerfilUsuario = appServiceEstruturaPerfilUsuario;
            _commercialstructureoValidator = new EstruturaComercialValidator();
        }

        /// <summary>
        /// Lista todas as Estruturas comerciais de Diretorias cadastrados
        /// </summary>
        /// <param name="options">Filtros OData</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/commercialstructures")]
        public async Task<GenericResult<IQueryable<EstruturaComercialDto>>> Get(ODataQueryOptions<EstruturaComercialDto> options)
        {
            var result = new GenericResult<IQueryable<EstruturaComercialDto>>();
            try
            {
                var estruturaComerciais = await _appServiceEstruturaComercial.GetAllFilterAsync(c => c.EstruturaComercialPapelID == "D");
                result.Result = options.ApplyTo(estruturaComerciais.AsQueryable()).Cast<EstruturaComercialDto>();
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
        /// Busca estrutura comercial por código SAP
        /// </summary>
        /// <param name="id">Código SAP</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcommercialstructurebyid/{id}")]
        public async Task<GenericResult<EstruturaComercialDto>> GetID(string id)
        {
            var result = new GenericResult<EstruturaComercialDto>();
            try
            {
                result.Result = await _appServiceEstruturaComercial.GetEstruturaComercialByID(id);
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
        /// Busca estrutura comercial por conta do cliente
        /// </summary>
        /// <param name="id">Código do cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcommercialstructurebyaccountclient/{id:guid}")]
        public async Task<GenericResult<IEnumerable<ContaClienteEstruturaComercialDto>>> GetContaCliente(Guid id)
        {
            var result = new GenericResult<IEnumerable<ContaClienteEstruturaComercialDto>>();
            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                result.Result = await _appServiceContaClienteEstrutura.GetAllFilterAsync(c => c.ContaClienteId.Equals(id) && c.EmpresasId.Equals(empresa));
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
        /// Busca CTC por conta do cliente
        /// </summary>
        /// <param name="id">Código do cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getctcbyaccountclient/{id:guid}")]
        public async Task<GenericResult<IEnumerable<EstruturaComercialDto>>> GetContaClienteCTC(Guid id)
        {
            var result = new GenericResult<IEnumerable<EstruturaComercialDto>>();

            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                var contaestrutura = await _appServiceContaClienteEstrutura.GetAllFilterAsync(c => c.ContaClienteId.Equals(id) && c.EmpresasId.Equals(empresa) && c.EstruturaComercial.EstruturaComercialPapelID == "C" && c.EstruturaComercial.Ativo);

                result.Result = contaestrutura.Select(c => c.EstruturaComercial).OrderByDescending(c => c.DataCriacao);

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
        /// Busca estrutura comercial por código de Papel
        /// </summary>
        /// <param name="id">Código do papel</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcommercialstructurebypapel/{id}")]
        public async Task<GenericResult<IEnumerable<EstruturaComercialDto>>> Get(string id)
        {
            var result = new GenericResult<IEnumerable<EstruturaComercialDto>>();
            try
            {
                result.Result = await _appServiceEstruturaComercial.GetEstruturaComercialByPaper(id);
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
        /// Busca estrutura comercial por código de Papel e retorna apenas os que o usuário tem acesso
        /// </summary>
        /// <param name="id">Código do papel</param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcommercialstructurebypapelrestrict/{id}")]
        public async Task<GenericResult<IEnumerable<EstruturaComercialDto>>> GetEstruturaComercial(ODataQueryOptions<EstruturaComercialDto> options, string id)
        {
            var result = new GenericResult<IEnumerable<EstruturaComercialDto>>();
            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var user = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                var listaEstruturaComercial = await _appServiceEstruturaComercial.GetEstruturaComercialByPaper(id);
                var usuario = await _appServiceUsuario.GetAsync(c => c.Ativo && c.ID.Equals(new Guid(user)));
                var ctcPerfilUsuario = await _appServiceEstruturaPerfilUsuario.BuscaContaCliente(usuario.Login, "");

                IEnumerable<string> ctcUsuario = null;

                switch (id)
                {
                    case "C": ctcUsuario = ctcPerfilUsuario.Select(s => s.CTC).ToList(); break;
                    case "G": ctcUsuario = ctcPerfilUsuario.Select(s => s.GC).ToList(); break;
                    case "D": ctcUsuario = ctcPerfilUsuario.Select(s => s.DI).ToList(); break;
                    default: ctcUsuario = new List<string>(); break;
                }

                var fresult = listaEstruturaComercial.Where(l => ctcUsuario.Contains(l.Nome)).ToList();

                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(fresult.AsQueryable(), new ODataQuerySettings()).Cast<EstruturaComercialDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(fresult.AsQueryable()).Cast<EstruturaComercialDto>();
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
        /// Busca estrutura comercial por código Sap do Superior
        /// </summary>
        /// <param name="idsuperior"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/getcommercialstructurebymaster/{idsuperior}")]
        public async Task<GenericResult<IEnumerable<EstruturaComercialDto>>> GetSuperior(string idsuperior)
        {
            var result = new GenericResult<IEnumerable<EstruturaComercialDto>>();
            try
            {
                result.Result = await _appServiceEstruturaComercial.GetAllFilterAsync(c => c.Superior.CodigoSap.Equals(idsuperior));
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
        /// Adiciona uma Nova Estrutura de Diretorias
        /// </summary>
        /// <param name="EstruturaComercialDto">Estrutura Diretoria</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insertcommercialstructuredirector")]
        [ResponseType(typeof(EstruturaComercialDiretoriaDto))]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "EstruturaComercialDiretoria_Inserir")]
        public async Task<GenericResult<EstruturaComercialDiretoriaDto>> PostNovocommercialstructureo(EstruturaComercialDiretoriaDto EstruturaComercialDto)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var user = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
            var result = new GenericResult<EstruturaComercialDiretoriaDto>();
            var commercialstructureoValidation = _commercialstructureoValidator.Validate(EstruturaComercialDto);
            LogDto logDto = null;
            if (commercialstructureoValidation.IsValid)
            {
                try
                {
                    EstruturaComercialDto.UsuarioIDCriacao = new Guid(user);
                    result.Success = await _appServiceEstruturaComercial.Insert(EstruturaComercialDto);
                    var descricao = $"Inseriu uma estrutura comercial do nome {EstruturaComercialDto.Nome}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
                    _appServiceLog.Create(logDto);


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
                result.Errors = commercialstructureoValidation.GetErrors();
            }
            return result;
        }

        /// <summary>
        /// Atualiza uma estrutura  
        /// </summary>
        /// <param name="EstruturaComercialDto">Estrutura Diretoria</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(EstruturaComercialDiretoriaDto))]
        [Route("v1/updatecommercialstructure")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "EstruturaComercialDiretoria_Editar")]
        public async Task<GenericResult<EstruturaComercialDiretoriaDto>> PutAlteraUsuario(EstruturaComercialDiretoriaDto EstruturaComercialDto)
        {
            var result = new GenericResult<EstruturaComercialDiretoriaDto>();
            var commercialstructureoValidation = _commercialstructureoValidator.Validate(EstruturaComercialDto);
            LogDto logDto = null;
            if (commercialstructureoValidation.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var user = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;


                    EstruturaComercialDto.UsuarioIDAlteracao = new Guid(user);

                    result.Success = await _appServiceEstruturaComercial.Update(EstruturaComercialDto);
                    var descricao = $"Atualizou uma estrutura comercial do nome {EstruturaComercialDto.Nome}";
                    var level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level);
                    _appServiceLog.Create(logDto);
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
                result.Errors = commercialstructureoValidation.GetErrors();
            }
            return result;
        }

    }
}
