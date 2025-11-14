using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("title")]
    [Authorize]
    public class TituloController : ApiController
    {
        private readonly IAppServiceLog _log;
        private readonly IAppServiceTitulo _titulo;
        private readonly IAppServiceContaCliente _contaCliente;
        private readonly IAppServiceEnvioEmail _email;
        private readonly IAppServiceUsuario _usuario;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="log"></param>
        /// <param name="titulo"></param>
        /// <param name="contaCliente"></param>
        /// <param name="email"></param>
        /// <param name="usuario"></param>
        public TituloController(IAppServiceLog log, IAppServiceTitulo titulo, IAppServiceContaCliente contaCliente , IAppServiceEnvioEmail email, IAppServiceUsuario usuario)
        {
            _log = log;
            _titulo = titulo;
            _contaCliente = contaCliente;
            _email = email;
            _usuario = usuario;
        }

        /// <summary>
        /// Atualiza titulos
        /// </summary>
        /// <param name="value">Férias</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updatetitle")]
        [ResponseType(typeof(TituloDto))]
        public async Task<GenericResult<TituloDto>> Put(List<TituloDto> value)
        {
            var result = new GenericResult<TituloDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;

                result.Success = await _titulo.UpdateList(value);

                var descricao = "";
                if (value[0].IncluiuPefin)
                    descricao = $"Usuario {User.Identity.Name} solicitou inclusão de Pefin.";
                else if (value[0].ExcluiuPefin)
                    descricao = $"Usuario {User.Identity.Name} solicitou exclusão de Pefin.";
                else if (value[0].IncluiuProtesto)
                    descricao = $"Usuario {User.Identity.Name} solicitou inclusão de protesto.";
                else if (value[0].EmitiuDuplicata)
                    descricao = $"Usuario {User.Identity.Name} solicitou emissão de duplicata.";

                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
                _log.Create(logDto);
            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { Resources.Resources.Error };
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

            return result;
        }

        /// <summary>
        /// Atualiza Comentário, Data Prevista, Status, Data Aceite e % Juros titulos
        /// </summary>
        /// <param name="value">Titulos</param>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/updatestatustitle")]
        public async Task<GenericResult<TituloAtualizacaoStatus>> PutStatus(TituloAtualizacaoStatus value)
        {
            var result = new GenericResult<TituloAtualizacaoStatus>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaId = Request.Properties["Empresa"].ToString();

                value.UsuarioCriacao = new Guid(userid);
                result.Success = await _titulo.UpdateStatus(value, empresaId);

                var descricao = $"Usuario {User.Identity.Name} atualizou dados nos titulos";
                var logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, EnumLogLevelDto.Info);
                _log.Create(logDto);
            }
            catch (ArgumentException ex)
            {
                result.Success = false;
                result.Errors = new[] { Resources.Resources.Error };
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

            return result;
        }
    }
}
