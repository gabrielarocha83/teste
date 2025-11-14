using System.Configuration;
using System.Web;
using System.Web.Http;
using Salesforce.Common;
using Salesforce.Common.Models;

#pragma warning disable 1591

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("v1/auth")]
    public class SSOController : ApiController
    {
        private readonly string _consumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
        private readonly string _consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
        private readonly string _callbackUrl = ConfigurationManager.AppSettings["CallbackUrl"];
        private readonly string _authorizationEndpointUrl = ConfigurationManager.AppSettings["AuthorizationEndpointUrl"];
        private readonly string _authorizationEndpointUrlPartner = ConfigurationManager.AppSettings["AuthorizationEndpointUrlPartner"];

        [HttpGet]
        [Route("rep/{flag}")]
        public IHttpActionResult SalesForce(string flag)
        {
            var authEndpoint = _authorizationEndpointUrl;

            //var referer = HttpContext.Current.Request.Headers["Referer"];

            // SF Partner Redirection
            if (!string.IsNullOrEmpty(flag) && flag == "1")
            {
                authEndpoint = _authorizationEndpointUrlPartner;
            }

            return Redirect(Common.FormatAuthUrl(authEndpoint, ResponseTypes.Code, _consumerKey, HttpUtility.UrlEncode(_callbackUrl)));
        }

        [HttpGet]
        [Route("cc/{cc}/rep/{flag}")]
        public IHttpActionResult SalesForceCC(string cc, string flag)
        {
            var authEndpoint = _authorizationEndpointUrl;

            //var referer = HttpContext.Current.Request.Headers["Referer"];

            // SF Partner Redirection
            if (!string.IsNullOrEmpty(flag) && flag == "1")
            {
                authEndpoint = _authorizationEndpointUrlPartner;
            }

            return Redirect(Common.FormatAuthUrl(authEndpoint, ResponseTypes.Code, _consumerKey, HttpUtility.UrlEncode(_callbackUrl), DisplayTypes.Page, false, cc));
        }
    }
}