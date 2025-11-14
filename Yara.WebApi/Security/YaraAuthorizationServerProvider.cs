using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net.Http;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using SimpleInjector.Lifestyles;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.WebApi.Security
{
    public class YaraAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private string Codigo;
        private bool rep = false;
        private UsuarioDto _usuarioDto;
        private IEnumerable<PermissaoDto> _permissoes;

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            Codigo = HttpUtility.UrlDecode(context.Parameters["code"]);

            rep = (context.Parameters["r"] == "1"); // Instancia representante

            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            bool salesForceLogin = false;
            bool authorization = false;
            var container = Startup.container;
            var Login = string.Empty;

            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            #region SSO-SalesForce

            if (!string.IsNullOrWhiteSpace(Codigo))
            {
                salesForceLogin = true;

                try
                {
                    var sso = await SalesForceAuthentication.AuthenticationSSO(Codigo, rep);
                    var EmailSalesForce = sso.email;

                    var logger = log4net.LogManager.GetLogger("YaraLog");
                    logger.Warn(sso.email);
                    logger.Warn(sso.username);
                    logger.Warn(sso.display_name);

                    if (!string.IsNullOrWhiteSpace(EmailSalesForce))
                    {
                        Login = EmailSalesForce;
                    }
                    else
                    {
                        context.SetError("invalid_grant", Resources.Resources.userInactive);
                        return;
                    }

                }
                catch (Exception sfe)
                {
                    context.SetError("invalid_grant", string.Format("Erro Autenticação Saleforce: {0}.", sfe.Message));
                    return;
                }

            }

            #endregion

            #region AD Login

            if (!salesForceLogin)
            {
                Login = context.UserName;

                #if !DEBUG

                if (ConfigurationManager.AppSettings["DomainLocal"] != "" || Login == "testeportal")
                {
                    using (PrincipalContext pc = new PrincipalContext(ContextType.Machine, null))
                    {
                        authorization = pc.ValidateCredentials(Login, context.Password);
                    }
                }
                else
                {
                    string domain1 = ConfigurationManager.AppSettings["DomainAD"];
                    string domain2 = ConfigurationManager.AppSettings["DomainAD2"];

                    if (!string.IsNullOrEmpty(domain1))
                    {
                        using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain1))
                        {
                            authorization = pc.ValidateCredentials(Login, context.Password);
                        }
                    }

                    if (!string.IsNullOrEmpty(domain2) && !authorization)
                    {
                        using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain2))
                        {
                            authorization = pc.ValidateCredentials(Login, context.Password);
                        }
                    }

                }

                if (!authorization)
                {
                    context.SetError("invalid_grant", Resources.Resources.tokenInvalid);
                    return;
                }

                #endif
            }
            //else
            //{
            //    #region SalesForce Login

            //    authorization = await SalesForceAuthentication.Authentication(Login, context.Password);

            //    if (!authorization)
            //    {
            //        context.SetError("invalid_grant", Resources.Resources.tokenInvalid);
            //        return;
            //    }

            //    #endregion
            //}

            #endregion

            try
            {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    //TipoAcessoDto tipoAcesso = TipoAcessoDto.AD;

                    //if (salesForceLogin)
                    //    tipoAcesso = TipoAcessoDto.SF;

                    _usuarioDto = await container.GetInstance<IAppServiceUsuario>().GetAsync(c => c.Login.Equals(Login));

                    if (_usuarioDto == null)
                    {
                        context.SetError("invalid_grant", Resources.Resources.tokenInvalid);
                        return;
                    }

                    _usuarioDto = await container.GetInstance<IAppServiceUsuario>().SetToken(_usuarioDto.ID);

                    _permissoes = await container.GetInstance<IAppServicePermissao>().GetListPermissao(_usuarioDto.ID);
                }
            }
            catch (Exception e)
            {
                context.SetError("invalid_grant", e.Message);
                return;
            }

            if (_usuarioDto.Ativo)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, _usuarioDto.Nome));
                identity.AddClaim(new Claim("Usuario", _usuarioDto.ID.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Email, Login));

                _permissoes.ForEach(c => identity.AddClaim(new Claim("Permissao", c.Descricao, c.Nome)));
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                Thread.CurrentPrincipal = principal;

                context.Validated(identity);

                authorization = true;
            }
            else
            {
                context.SetError("invalid_grant", Resources.Resources.userInactive);
                return;
            }
        }
    }
}