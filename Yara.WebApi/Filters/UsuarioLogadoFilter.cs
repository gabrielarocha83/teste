using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using SimpleInjector.Lifestyles;
using Yara.AppService.Interfaces;

namespace Yara.WebApi.Controllers
{
    public class UsuarioLogadoFilter : AuthorizationFilterAttribute
    {
        public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (principal.Claims.Any())
            {
                var container = Startup.container;
                var usuario = principal.Claims.FirstOrDefault(d => d.Type.Equals("Usuario")).Value;

                if (usuario != null)
                {
                    using (AsyncScopedLifestyle.BeginScope(container))
                    {
                        try
                        {
                            var user = await container.GetInstance<IAppServiceUsuario>().GetAsync(c => c.ID.Equals(new Guid(usuario)));

                            if (user != null)
                            {
                                Guid? tokenID;

                                if (actionContext.Request.RequestUri.AbsolutePath.ToLower().Contains("/getcoduseractive"))
                                    tokenID = user.TokenID;
                                else
                                {
                                    var tokenIDFromHeader = actionContext.Request.Headers.FirstOrDefault(kvp => kvp.Key.ToLower().Equals("tokenid"));
                                    var tokenValue = tokenIDFromHeader.Equals(new KeyValuePair<String, IEnumerable<String>>()) ? null : ((string[])tokenIDFromHeader.Value)[0];

                                    tokenID = !string.IsNullOrEmpty(tokenValue) ? new Guid(tokenValue) : (Guid?)null;
                                }                                    

                                if (user.TokenID != tokenID)
                                {
                                    actionContext.Response = new HttpResponseMessage
                                    {
                                        StatusCode = HttpStatusCode.Unauthorized,
                                        ReasonPhrase = "InvalidToken",
                                    };
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(user.EmpresaLogada))
                                        actionContext.Request.Properties.Add("Empresa", user.EmpresaLogada);
                                    else
                                    {
                                        if (string.IsNullOrWhiteSpace(user.EmpresasID))
                                            actionContext.Request.Properties.Add("Empresa", "Y");
                                        else if (user.EmpresasID.Equals("A"))
                                            actionContext.Request.Properties.Add("Empresa", !string.IsNullOrWhiteSpace(user.EmpresaLogada) ? user.EmpresaLogada : "Y");
                                        else
                                            actionContext.Request.Properties.Add("Empresa", user.EmpresasID);
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            var error = new ErrorsYara();
                            error.ErrorYara(e);
                        }
                    }
                }
            }
        }
    }
}