using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Ajax.Utilities;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.Extensions;
using Yara.WebApi.Validations;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("accountsphone")]
    [Authorize]
    public class ContaClienteTelefoneController : ApiController
    {
        private readonly IAppServiceContaClienteTelefone _contaClienteTelefone;
        private readonly IAppServiceLog _log;
        private readonly ContaClienteTelefoneValidator _contaClienteTelefoneValidator;
        public ContaClienteTelefoneController(IAppServiceContaClienteTelefone contaClienteTelefone, IAppServiceLog log)
        {
            _log = log;
            _contaClienteTelefone = contaClienteTelefone;
            _contaClienteTelefoneValidator = new ContaClienteTelefoneValidator();
        }

        /// <summary>
        /// Método que consulta de exibição dos telefones de uma conta cliente
        /// </summary>
        /// <param name="id">Código da Conta Cliente</param>
        /// <returns>Dados da ContaCliente Telefones</returns>
        [HttpGet]
        [Route("v1/getccountsphone/{id:guid}")]
        public async Task<GenericResult<IEnumerable<ContaClienteTelefoneDto>>> Get(Guid id)
        {
            var result = new GenericResult<IEnumerable<ContaClienteTelefoneDto>>();

            try
            {
                result.Result = await _contaClienteTelefone.GetAllFilterAsync(c => c.ContaClienteID.Equals(id) && c.Ativo.Equals(true));
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
        /// Método para inserir um telefone na contacliente.
        /// </summary>
        /// <param name="clienteTelefoneDto">Objeto com as informações para inserir dados</param>
        /// <returns>Booleano com sucesso ou erro da insersão.</returns>
        [HttpPost]
        [Route("v1/insertaccountphone")]
        [ResponseType(typeof(ContaClienteTelefoneDto))]
        public GenericResult<ContaClienteTelefoneDto> PostNovoTelefone(ContaClienteTelefoneDto clienteTelefoneDto)
        {
            var result = new GenericResult<ContaClienteTelefoneDto>();
            var contaClienteTelefoneValidation = _contaClienteTelefoneValidator.Validate(clienteTelefoneDto);
            var logDto = new LogDto();
            if (contaClienteTelefoneValidation.IsValid)
            {
                try
                {
                    var userClaims = User.Identity as ClaimsIdentity;
                    var userid = userClaims?.FindFirst(c => c.Type.Equals("Usuario")).Value;
                    clienteTelefoneDto.Ativo = true;
                    clienteTelefoneDto.UsuarioIDCriacao = new Guid(userid);
                    result.Success = _contaClienteTelefone.Insert(clienteTelefoneDto);
                    var descricao = $"Inseriu um telefone na conta cliente com o número {clienteTelefoneDto.Telefone}";
                    var level = EnumLogLevelDto.AccountClient;
                    logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, clienteTelefoneDto.ContaClienteID);
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
            {
                result.Errors = contaClienteTelefoneValidation.GetErrors();
            }
            return result;
        }

        /// <summary>
        /// Método para inserir ou atualizar vários telefone de uma contacliente.
        /// </summary>
        /// <param name="clienteTelefones">Lista de telefones a serem atualizados ou inseridos</param>
        /// <returns>Booleano com sucesso ou erro da insersão.</returns>
        [HttpPost]
        [Route("v1/insertaccountphones")]
        [ResponseType(typeof(ContaClienteTelefoneDto))]
        public async Task<GenericResult<ContaClienteTelefoneDto>> PosTelefones(IEnumerable<ContaClienteTelefoneDto> clienteTelefones)
        {
            var contaClienteTelefone = new GenericResult<ContaClienteTelefoneDto>();

            var logDto = new LogDto();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims?.FindFirst(c => c.Type.Equals("Usuario")).Value;

                var contacliente = clienteTelefones.First();
                clienteTelefones.ForEach(c => c.UsuarioIDCriacao = new Guid(userid));
                clienteTelefones.ForEach(c => c.UsuarioIDAlteracao = new Guid(userid));
                var telefones = clienteTelefones.Count() > 1 ? string.Join(",", clienteTelefones.Select(c => c.Telefone.ToString()).ToArray()) : clienteTelefones.First().Telefone;

                contaClienteTelefone.Success = await _contaClienteTelefone.InsertOrUpdateManyAsync(clienteTelefones);

                var descricao = $"Alterou os telefones na conta cliente com os números { telefones}";
                const EnumLogLevelDto level = EnumLogLevelDto.AccountClient;
                logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, contacliente.ContaClienteID);
                _log.Create(logDto);
            }
            catch (Exception e)
            {
                contaClienteTelefone.Success = false;
                contaClienteTelefone.Errors = new[] { Resources.Resources.Error };
               var error = new ErrorsYara();
                 error.ErrorYara(e);
            }
          

            return contaClienteTelefone;
        }

        /// <summary>
        /// Inativa um telefone da conta Cliente
        /// </summary>
        /// <param name="contaclienteTelefoneoDto">Dados de telefone, ID, ContaClienteID, Telefone</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/inactiveaccountphone")]
        [ResponseType(typeof(ContaClienteTelefoneDto))]
        public async Task<GenericResult<ContaClienteTelefoneDto>> Delete(ContaClienteTelefoneDto contaclienteTelefoneoDto)
        {
            var result = new GenericResult<ContaClienteTelefoneDto>();
            LogDto logDto;
            try
            {

                result.Success = await _contaClienteTelefone.Inactive(contaclienteTelefoneoDto);

                var descricao = $"Inseriu um telefone na conta cliente com o número {contaclienteTelefoneoDto.Telefone}";
                var level = EnumLogLevelDto.Info;
                logDto = ApiLogDto.GetLog(User.Identity as ClaimsIdentity, descricao, level, contaclienteTelefoneoDto.ID);
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