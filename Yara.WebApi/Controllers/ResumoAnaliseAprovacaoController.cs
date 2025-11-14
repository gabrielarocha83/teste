using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("resumoanaliseaprovacao")]
    [Authorize]
    public class ResumoAnaliseAprovacaoController : ApiController
    {
        private readonly IAppServiceResumoAnaliseAprovacao _resumoAnaliseAprovacao;

        public ResumoAnaliseAprovacaoController(IAppServiceResumoAnaliseAprovacao resumoAnaliseAprovacao)
        {
            _resumoAnaliseAprovacao = resumoAnaliseAprovacao;
        }

        [HttpPost]
        [Route("v1/getresumoanalise")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ResumoAnalises_Visualizar")]
        public async Task<GenericResult<IEnumerable<ResumoAnaliseAprovacaoDto>>> GetResumoAnalise(List<string> gcs)
        {
            var result = new GenericResult<IEnumerable<ResumoAnaliseAprovacaoDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var usuarioID = userClaims?.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaID = Request.Properties["Empresa"].ToString();

                var resumoAnaliseAprovacaoDto = await _resumoAnaliseAprovacao.BuscaResumoAnalise(new Guid(usuarioID), empresaID, gcs);
                if (resumoAnaliseAprovacaoDto != null)
                {
                    result.Count = resumoAnaliseAprovacaoDto.Count();
                    result.Result = resumoAnaliseAprovacaoDto;
                }

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

        [HttpPost]
        [Route("v1/getresumoaprovacao")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "ResumoAprovacoes_Visualizar")]
        public async Task<GenericResult<IEnumerable<ResumoAnaliseAprovacaoDto>>> GetResumoAprovacao(List<string> gcs)
        {
            var result = new GenericResult<IEnumerable<ResumoAnaliseAprovacaoDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var usuarioID = userClaims?.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaID = Request.Properties["Empresa"].ToString();

                var resumoAnaliseAprovacaoDto = await _resumoAnaliseAprovacao.BuscaResumoAprovacao(new Guid(usuarioID), empresaID, gcs);
                if (resumoAnaliseAprovacaoDto != null)
                {
                    result.Count = resumoAnaliseAprovacaoDto.Count();
                    result.Result = resumoAnaliseAprovacaoDto;
                }

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
    }
}
