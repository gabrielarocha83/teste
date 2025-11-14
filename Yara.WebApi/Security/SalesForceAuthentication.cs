using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Salesforce.Common;
using Salesforce.Common.Models;
using Salesforce.Force;

namespace Yara.WebApi.Security
{
    public static class SalesForceAuthentication
    {
        public static async Task<bool> Authentication(string username, string password)
        {
            using (var auth = new AuthenticationClient())
            {
                var ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
                var ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
                var Username = username;
                var Password = password;
                var AuthorizationEndpointUrl = ConfigurationManager.AppSettings["TokenEndpointUrl"];

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                try
                {
                    await auth.UsernamePasswordAsync(ConsumerKey, ConsumerSecret, Username, Password, AuthorizationEndpointUrl);

                    var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static async Task<UserInfo> AuthenticationSSO(string code, bool rep)
        {
            var logger = log4net.LogManager.GetLogger("YaraLog");

            using (var auth = new AuthenticationClient())
            {
                var ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
                var ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
                var CallbackUrl = ConfigurationManager.AppSettings["CallbackUrl"];

                var authorizationEndpointUrl = ConfigurationManager.AppSettings["TokenEndpointUrl"];
                var userInfoEndpointUrl = ConfigurationManager.AppSettings["UserInfoEndpointUrl"];

                if (rep)
                {
                    authorizationEndpointUrl = ConfigurationManager.AppSettings["TokenEndpointUrlPartner"];
                    userInfoEndpointUrl = ConfigurationManager.AppSettings["UserInfoEndpointUrlPartner"];
                }

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                try
                {
                    logger.Error(string.Format("ConsumerKey: {0} / ConsumerSecret: {1} / CallbackUrl: {2} / authorizationEndpointUrl: {3} / userInfoEndpointUrl: {4} / Code: {5}", ConsumerKey, ConsumerSecret, CallbackUrl, authorizationEndpointUrl, userInfoEndpointUrl, code));

                    await auth.WebServerAsync(ConsumerKey, ConsumerSecret, CallbackUrl, code, authorizationEndpointUrl);

                    logger.Error(string.Format("auth.InstanceUrl: {0} / auth.AccessToken: {1} / auth.ApiVersion: {2}", auth.InstanceUrl, auth.AccessToken, auth.ApiVersion));

                    var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);
                    var userInfo = await client.UserInfo<UserInfo>(userInfoEndpointUrl);

                    return userInfo;
                }
                catch (ForceAuthException fae)
                {
                    var error = new ErrorsYara();
                    error.ErrorYara(fae);

                    throw new Exception(fae.Message);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}