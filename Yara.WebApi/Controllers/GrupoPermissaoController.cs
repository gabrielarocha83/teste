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
    [Authorize]
    [RoutePrefix("v1/groupspermissions")]
    public class GrupoPermissaoController : ApiController
    {
        private readonly IAppServiceGrupoPermissao _grupopermissao;
        private readonly GrupoPermissaoValidator _validator;
        private readonly ClaimsIdentity _user;
        private readonly IAppServiceLog _log;

        public GrupoPermissaoController(IAppServiceGrupoPermissao grupopermissao, IAppServiceLog log)
        {
            _grupopermissao = grupopermissao;
            _validator = new GrupoPermissaoValidator();
            _user = (ClaimsIdentity)User.Identity;
            _log = log;

        }

        [HttpGet]
        [Route("")]
        public async Task<GenericResult<IQueryable<GrupoPermissaoDto>>> Get(ODataQueryOptions<GrupoPermissaoDto> options)
        {
            var result = new GenericResult<IQueryable<GrupoPermissaoDto>>();

            try
            {
                var grupopermissoes = await _grupopermissao.GetAllAsync();
                result.Result = options.ApplyTo(grupopermissoes.AsQueryable()).Cast<GrupoPermissaoDto>();
                result.Success = true;

                
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
                var level = EnumLogLevelDto.Error;
               var logDto = ApiLogDto.GetLog(_user, e.Message, level);
                _log.Create(logDto);
            }
          

            return result;
        }

        [HttpGet]
        [Route("{GrupoID:guid}/{PermissaoID:guid}")]
        public async Task<GenericResult<GrupoPermissaoDto>> Get(Guid GrupoID, Guid PermissaoID)
        {
            var result = new GenericResult<GrupoPermissaoDto>();
         
            try
            {
                
                result.Result = await _grupopermissao.GetAsync(g => g.GrupoID.Equals(GrupoID) && g.PermissaoID.Equals(PermissaoID));
                result.Success = true;

               

                
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };

                var Level = EnumLogLevelDto.Error;
               var  logDto = ApiLogDto.GetLog(_user, e.Message, Level);
                _log.Create(logDto);

            }

          

            return result;
        }

        [HttpPost]
        [ResponseType(typeof(GrupoPermissaoDto))]
        [Route("")]
        public GenericResult<GrupoPermissaoDto> Post(GrupoPermissaoDto value)
        {
            var result = new GenericResult<GrupoPermissaoDto>();
            var validateResult = _validator.Validate(value);
            LogDto logDto = null;
            if (validateResult.IsValid)
            {
                try
                {
                    
                    result.Success = _grupopermissao.Insert(value);

                    var descricao = "Insert Grupo Permissao";
                    var Level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(_user, descricao, Level);

                   
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Errors = new[] { e.Message };

                    var Level = EnumLogLevelDto.Error;
                    logDto = ApiLogDto.GetLog(_user, e.Message, Level);
                   
                }
            }
            else
            {
                result.Errors = validateResult.GetErrors();
            }
            _log.Create(logDto);
            return result;
        }

        [HttpPut]
        [ResponseType(typeof(GrupoPermissaoDto))]
        [Route("")]
        public GenericResult<GrupoPermissaoDto> Put(GrupoPermissaoDto value)
        {
            var result = new GenericResult<GrupoPermissaoDto>();
            var validateResult = _validator.Validate(value);
            LogDto logDto = null;

            if (validateResult.IsValid)
            {
                try
                {
                   
                    result.Success = _grupopermissao.Update(value);
                    var descricao = "Update Grupo Permissao";
                    var Level = EnumLogLevelDto.Info;
                    logDto = ApiLogDto.GetLog(_user, descricao, Level);

                  
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Errors = new[] {e.Message};
                    var Level = EnumLogLevelDto.Error;
                    logDto = ApiLogDto.GetLog(_user, e.Message, Level);

                   
                }
            }
            else
            {
                result.Errors = validateResult.GetErrors();
            }
            _log.Create(logDto);
            return result;
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Route("")]
        public async Task<GenericResult<GrupoPermissaoDto>> Delete(Guid id)
        {
            var result = new GenericResult<GrupoPermissaoDto>();
            LogDto logDto = null;
            try
            {
                result.Success = await _grupopermissao.Inactive(id);


                var descricao = "Delete Grupo Permissao";
                var Level = EnumLogLevelDto.Info;
                logDto = ApiLogDto.GetLog(_user, descricao, Level);

              
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };

                var Level = EnumLogLevelDto.Error;
                logDto = ApiLogDto.GetLog(_user, e.Message, Level);

               
            }
            _log.Create(logDto);
            return result;
        }
    }
}
