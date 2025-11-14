using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.UI.WebControls;
using SimpleInjector.Lifestyles;
using Yara.AppService.Interfaces;

namespace Yara.WebApi.Controllers
{
    public class ClaimsAutorizeAttribute : AuthorizationFilterAttribute
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
            
            if (principal == null) return Task.FromResult<object>(null);
            
            if (!principal.Identity.IsAuthenticated)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            if (principal.HasClaim(c => c.Type.Equals(ClaimType) && c.ValueType.Equals(ClaimValue)))
                return Task.FromResult<object>(null);

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

            return Task.FromResult<object>(null);
        }
    }
}