using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.WebApi.ViewModel;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("comite")]
    public class PropostaComiteController : ApiController
    {
        // private readonly IAppServicePerfil _perfil;
        private readonly IAppServicePropostaLCComite _comite;
        private readonly IAppServiceEstruturaPerfilUsuario _perfilUsuario;

        /// <summary>
        /// Construtor da classe controller
        /// </summary>
        /// <param name="comite"></param>
        /// <param name="perfilUsuario"></param>
        public PropostaComiteController(IAppServicePropostaLCComite comite, IAppServiceEstruturaPerfilUsuario perfilUsuario)
        {
            _comite = comite;
            _perfilUsuario = perfilUsuario;
        }

        /// <summary>
        /// Método que retorna o fluxo de comite de uma proposta
        /// </summary>
        /// <param name="propostaID">Código da Proposta</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/proposta/{propostaID:guid}")]
        public async Task<GenericResult<IEnumerable<PropostaLCComiteDto>>> GetComiteProposta(Guid propostaID)
        {
            var result = new GenericResult<IEnumerable<PropostaLCComiteDto>>();

            try
            {
                var comites = await _comite.GetAllFilterAsync(c => c.Ativo && c.PropostaLCID.Equals(propostaID));

                if (comites.Any())
                {
                    foreach (var comite in comites)
                    {

                        switch (comite?.Perfil?.Descricao ?? "")
                        {
                            case "Analista de Crédito Jr": comite.Aprovadores = new List<string>() { "Analista de Crédito Pl", "Analista de Crédito Sr" }; break;
                            case "Analista de Crédito Pl": comite.Aprovadores = new List<string>() { "Analista de Crédito Sr" }; break;
                            case "Analista de Crédito Sr":
                            case "":
                            default: break;
                        }

                    }

                    result.Count = comites.Count();
                    result.Result = comites.OrderBy(c => c.Nivel).ThenBy(c => c.Round).ThenBy(c => c.DataCriacao);
                    result.Success = true;
                }
                else
                {
                    result.Count = 0;
                    result.Result = null;
                    result.Success = false;
                    result.Errors = new[] { "Nenhum comitê encontrado." };
                }
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
        /// Método que retorna uma proposta para acompanhamento do Cockpit
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/cockpit/propostas")]
        public async Task<GenericResult<IQueryable<PropostaLCComiteDto>>> GetComiteProposta(ODataQueryOptions<PropostaLCComiteDto> options)
        {
            var result = new GenericResult<IQueryable<PropostaLCComiteDto>>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var comites = await _comite.GetAllFilterAsync(c => c.Ativo && c.UsuarioID.Equals(new Guid(userid)) && c.StatusComiteID.Equals("AP"));
                int totalReg = 0;
                if (options.Filter != null)
                {
                    var filtro = options.Filter.ApplyTo(comites.AsQueryable(), new ODataQuerySettings()).Cast<PropostaLCComiteDto>();
                    totalReg = filtro.Count();
                }
                result.Result = options.ApplyTo(comites.AsQueryable()).Cast<PropostaLCComiteDto>();
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
        /// Método que retorna uma proposta para acompanhamento do Cockpit
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/adicionarNivel")]
        public async Task<GenericResult<PropostaLCComiteDto>> AdicionaNivel(PropostaLCComiteDto comite)
        {
            var result = new GenericResult<PropostaLCComiteDto>();
            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaID = Request.Properties["Empresa"].ToString();
                comite.EmpresaID = empresaID;
                comite.UsuarioID = new Guid(userid);
                result.Success = await _comite.InsertNivel(comite);
                result.Count = 0;
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
        /// Método que atualiza os dados do aprovador do comite 
        /// </summary>
        /// <param name="comite">Dados do Comite</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/atualizar")]
        public async Task<GenericResult<PropostaLCComiteDto>> Atualizar(PropostaLCComiteDto comite)
        {
            var result = new GenericResult<PropostaLCComiteDto>();

            try
            {
                var userClaims = User.Identity as ClaimsIdentity;
                var userid = userClaims.FindFirst(c => c.Type.Equals("Usuario")).Value;
                var empresaID = Request.Properties["Empresa"].ToString();
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host;

                comite.EmpresaID = empresaID;
                comite.UsuarioID = new Guid(userid);

                var retorno = await _comite.UpdateValuePair(comite, url);
                if (!retorno.Key)
                {
                    result.Errors = new[] { retorno.Value };
                }
                result.Success = true;
                result.Count = 0;
            }
            catch (ArgumentException arg)
            {
                result.Success = false;
                result.Errors = new[] { arg.Message };
                var error = new ErrorsYara();
                error.ErrorYara(arg);
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
