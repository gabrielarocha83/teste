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
    [RoutePrefix("blog")]
    [Authorize]
    public class BlogController : ApiController
    {
        private readonly IAppServiceBlog _blog;
        private readonly IAppServiceLog _log;
        private readonly BlogValidator _validator;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="log"></param>
        public BlogController(IAppServiceBlog blog, IAppServiceLog log)
        {
            _blog = blog;
            _log = log;
            _validator = new BlogValidator();
        }

        /// <summary>
        /// Método que Retorna todos os dados de blog
        /// </summary>
        /// <param name="options"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/allbyarea/{id:guid}")]
        public async Task<GenericResult<IQueryable<BlogDto>>> Get(ODataQueryOptions<BlogDto> options, Guid id)
        {
            var result = new GenericResult<IQueryable<BlogDto>>();
            try
            {
                var empresa = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;
                var blog = await _blog.GetByArea(id, empresa, url);
                blog = blog.OrderByDescending(c => c.DataCriacao);
                var totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(blog.AsQueryable(), new ODataQuerySettings()).Cast<BlogDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(blog.AsQueryable()).Cast<BlogDto>();
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
        /// Adiciona um nova Mensagem Blog
        /// </summary>
        /// <param name="value">Blog</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/insert")]
        [ResponseType(typeof(BlogDto))]
        public async Task<GenericResult<BlogDto>> Post(BlogDto value)
        {
            var result = new GenericResult<BlogDto>();
            var validationResult = _validator.Validate(value);

            if (validationResult.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    var empresa = Request.Properties["Empresa"].ToString();
                    var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                    value.UsuarioCriacaoID = new Guid(userid);
                    value.EmpresaID = empresa;
                    value.Area = value.Area;

                    result.Success = await _blog.InsertAsync(value, url);

                    var descricao = $"Inseriu um comentário no blog.";
                    var level = EnumLogLevelDto.Info;
                    var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, value.ContaClienteID);
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
