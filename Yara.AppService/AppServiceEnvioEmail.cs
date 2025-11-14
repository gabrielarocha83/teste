using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.Domain.Repository;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceEnvioEmail : IAppServiceEnvioEmail
    {
        private readonly IUnitOfWork _unitOfWork;

        private string _host;
        private int _port;
        private string _credentialsName;
        private string _credentialsPass;
        private string _from;
        private readonly bool _enableSsl = false;
        private List<string> To { get; set; }
        private string Subject { get; set; }
        private string Body { get; set; }

        public AppServiceEnvioEmail(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<KeyValuePair<bool, string>> SendMailLiberacaoManual(Guid solicitacaoId, UsuarioDto usuarioDto, string comentario, string EmpresaID, string URL)
        {
            var wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;

            #region ParametrosConfig
            var param = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.Tipo.Equals("email") && c.EmpresasID.Equals(EmpresaID));
            var smtpServer = new SmtpClient();

            foreach (var item in param)
            {
                if (item.Chave.Equals("Host")) { _host = item.Valor; }
                if (item.Chave.Equals("Port")) { _port = Convert.ToInt32(item.Valor); }
                if (item.Chave.Equals("CredentialName")) { _credentialsName = item.Valor; }
                if (item.Chave.Equals("CredentialPassword")) { _credentialsPass = item.Valor; }
                if (item.Chave.Equals("From")) { _from = item.Valor; }
            }
            #endregion

            try
            {
                smtpServer.Host = _host;
                smtpServer.Port = _port;
                smtpServer.Credentials = new NetworkCredential(_credentialsName, _credentialsPass);
                smtpServer.EnableSsl = _enableSsl;

                var mail = new MailMessage();

                //Obtendo o conteúdo do template
                var sTemplate = wc.DownloadString($"{AppDomain.CurrentDomain.BaseDirectory}\\TemplateLiberacaoManual.html");

                //Altera variaveis no template
                sTemplate = sTemplate.Replace("##USUARIO##", usuarioDto.Nome);
                sTemplate = sTemplate.Replace("##COMENTARIO##", comentario);
                sTemplate = sTemplate.Replace("##LINKLOGO##", URL + "/assets/images/logo-yara-email.jpg");
                sTemplate = sTemplate.Replace("##LINKCOCKPIT##", URL + "/#/ordem-requisicao/" + solicitacaoId.ToString());

                mail.From = new MailAddress(_from, "Fluxo de Aprovação"); //To send
                mail.To.Add(new MailAddress(usuarioDto.Email, usuarioDto.Nome));
                mail.Subject = "Liberação de Ordem de Venda";

                mail.Body = sTemplate;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                await smtpServer.SendMailAsync(mail);

                return new KeyValuePair<bool, string>(true, "Enviado email com sucesso.");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, "Ocorreu um erro ao tentar encaminhar o email. Erro: " + e.Message);
            }
        }

        public async Task<KeyValuePair<bool, string>> SendMailGrupoEconomico(Guid grupoId, UsuarioDto usuarioDto, string grupoNome, string URL)
        {
            var wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;

            #region ParametrosConfig
            var param = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.Tipo.Equals("email") && c.EmpresasID.Equals(usuarioDto.EmpresaLogada));
            var smtpServer = new SmtpClient();

            foreach (var item in param)
            {
                if (item.Chave.Equals("Host")) { _host = item.Valor; }
                if (item.Chave.Equals("Port")) { _port = Convert.ToInt32(item.Valor); }
                if (item.Chave.Equals("CredentialName")) { _credentialsName = item.Valor; }
                if (item.Chave.Equals("CredentialPassword")) { _credentialsPass = item.Valor; }
                if (item.Chave.Equals("From")) { _from = item.Valor; }
            }
            #endregion

            try
            {
                smtpServer.Host = _host;
                smtpServer.Port = _port;
                smtpServer.Credentials = new NetworkCredential(_credentialsName, _credentialsPass);
                smtpServer.EnableSsl = _enableSsl;

                var mail = new MailMessage();

                //Obtendo o conteúdo do template
                var sTemplate = wc.DownloadString($"{AppDomain.CurrentDomain.BaseDirectory}TemplateFluxoGrupoEconomico.html");

                //Altera variaveis no template
                sTemplate = sTemplate.Replace("##USUARIO##", usuarioDto.Nome);
                sTemplate = sTemplate.Replace("##GRUPONOME##", grupoNome);
                sTemplate = sTemplate.Replace("##LINKLOGO##", URL + "/assets/images/logo-yara-email.jpg");
                sTemplate = sTemplate.Replace("##LINKCOCKPIT##", URL + "/#/grupoeconomico-aprovar/" + grupoId);

                mail.From = new MailAddress(_from, "Fluxo de Grupo Econômico"); //To send
                mail.To.Add(new MailAddress(usuarioDto.Email, usuarioDto.Nome));
                mail.Subject = "Liberação de Grupo Econômico";

                mail.Body = sTemplate;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                await smtpServer.SendMailAsync(mail);

                return new KeyValuePair<bool, string>(true, "Enviado email com sucesso.");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, "Ocorreu um erro ao tentar encaminhar o email. Erro: " + e.Message);
            }
        }

        public async Task<KeyValuePair<bool, string>> SendMailSolicitanteGrupoEconomico(UsuarioDto usuarioDto, bool status, string grupoNome, string URL)
        {
            var wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;

            #region ParametrosConfig
            var param = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.Tipo.Equals("email") && c.EmpresasID.Equals(usuarioDto.EmpresaLogada));
            var smtpServer = new SmtpClient();

            foreach (var item in param)
            {
                if (item.Chave.Equals("Host")) { _host = item.Valor; }
                if (item.Chave.Equals("Port")) { _port = Convert.ToInt32(item.Valor); }
                if (item.Chave.Equals("CredentialName")) { _credentialsName = item.Valor; }
                if (item.Chave.Equals("CredentialPassword")) { _credentialsPass = item.Valor; }
                if (item.Chave.Equals("From")) { _from = item.Valor; }
            }
            #endregion

            try
            {
                smtpServer.Host = _host;
                smtpServer.Port = _port;
                smtpServer.Credentials = new NetworkCredential(_credentialsName, _credentialsPass);
                smtpServer.EnableSsl = _enableSsl;

                var mail = new MailMessage();

                var sTemplate = wc.DownloadString($"{AppDomain.CurrentDomain.BaseDirectory}TemplateSolicitanteGrupoEconomico.html");

                //Altera variaveis no template
                sTemplate = sTemplate.Replace("##USUARIO##", usuarioDto.Nome);
                sTemplate = sTemplate.Replace("##GRUPOECONOMICO##", grupoNome);
                sTemplate = sTemplate.Replace("##STATUS##", status ? "APROVADA" : "REPROVADA");
                sTemplate = sTemplate.Replace("##LINKLOGO##", URL + "/assets/images/logo-yara-email.jpg");
                sTemplate = sTemplate.Replace("##LINKCOCKPIT##", URL + "/#/cockpit");

                mail.From = new MailAddress(_from, "Retorno Fluxo de Grupo Econômico"); //To send
                mail.To.Add(new MailAddress(usuarioDto.Email, usuarioDto.Nome));
                mail.Subject = "Retorno Automático";

                mail.Body = sTemplate;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                await smtpServer.SendMailAsync(mail);

                return new KeyValuePair<bool, string>(true, "Enviado email com sucesso.");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, "Ocorreu um erro ao tentar encaminhar o email. Erro: " + e.Message);
            }
        }

        public async Task<KeyValuePair<bool, string>> SendMailFeedBackPropostas(UsuarioDto usuarioDto, Guid PropostaID, string comentario, Guid contaClienteID, string URL)
        {
            var wc = new WebClient { Encoding = System.Text.Encoding.UTF8 };

            #region ParametrosConfig
            var param = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.Tipo.Equals("email") && c.EmpresasID.Equals(usuarioDto.EmpresaLogada));
            var smtpServer = new SmtpClient();

            foreach (var item in param)
            {
                if (item.Chave.Equals("Host")) { _host = item.Valor; }
                if (item.Chave.Equals("Port")) { _port = Convert.ToInt32(item.Valor); }
                if (item.Chave.Equals("CredentialName")) { _credentialsName = item.Valor; }
                if (item.Chave.Equals("CredentialPassword")) { _credentialsPass = item.Valor; }
                if (item.Chave.Equals("From")) { _from = item.Valor; }
            }
            #endregion

            var cliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(contaClienteID));

            var propostalc = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(PropostaID));
            var propostalca = await _unitOfWork.PropostaLCAdicionalRepository.GetAsync(c => c.ID.Equals(PropostaID));
            var propostaabono = await _unitOfWork.PropostaAbonoRepository.GetAsync(c => c.ID.Equals(PropostaID));
            var propostaalcada = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(PropostaID));
            var propostaprorrogacao = await _unitOfWork.PropostaProrrogacao.GetAsync(c => c.ID.Equals(PropostaID));
            var propostarenovacaovigencialc = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetAsync(c => c.ID.Equals(PropostaID));

            string propostaNumero = "";
            string linkTipoProposta = "";
            string tipoProposta = "";

            if (propostalc != null)
            {
                propostaNumero = $"LC{propostalc.NumeroInternoProposta:00000}/{propostalc.DataCriacao:yyyy}";
                linkTipoProposta = "/proposta-lc";
                tipoProposta = "de limite de crédito";
            }
            else if (propostalca != null)
            {
                propostaNumero = $"LA{propostalca.NumeroInternoProposta:00000}/{propostalca.DataCriacao:yyyy}";
                linkTipoProposta = "/proposta-la";
                tipoProposta = "de limite de crédito adicional";
            }
            else if (propostaabono != null)
            {
                propostaNumero = $"A{propostaabono.NumeroInternoProposta:00000}/{propostaabono.DataCriacao:yyyy}";
                linkTipoProposta = "/proposta-abono";
                tipoProposta = "de abono";
            }
            else if (propostaalcada != null)
            {
                propostaNumero = $"AC{propostaalcada.NumeroInternoProposta:00000}/{propostaalcada.DataCriacao:yyyy}";
                linkTipoProposta = "/proposta-alcada";
                tipoProposta = "de alçada comercial";
            }
            else if (propostaprorrogacao != null)
            {
                propostaNumero = $"P{propostaprorrogacao.NumeroInternoProposta:00000}/{propostaprorrogacao.DataCriacao:yyyy}";
                linkTipoProposta = "/proposta-prorrogacao";
                tipoProposta = "de prorrogação";
            }
            else if (propostarenovacaovigencialc != null)
            {
                propostaNumero = $"RV{propostarenovacaovigencialc.NumeroInternoProposta:00000}/{propostarenovacaovigencialc.DataCriacao:yyyy}";
                linkTipoProposta = "/renovacao-vigencia";
                tipoProposta = "de renovação de vigência de LC";
            }

            try
            {
                smtpServer.Host = _host;
                smtpServer.Port = _port;
                smtpServer.Credentials = new NetworkCredential(_credentialsName, _credentialsPass);
                smtpServer.EnableSsl = _enableSsl;

                var mail = new MailMessage();

                //Obtendo o conteúdo do template
                var sTemplate = wc.DownloadString($"{AppDomain.CurrentDomain.BaseDirectory}TemplatePropostaFeedBack.html");

                //Altera variaveis no template
                sTemplate = sTemplate.Replace("##USUARIO##", usuarioDto.Nome);
                sTemplate = sTemplate.Replace("##TIPOPROPOSTA##", tipoProposta);
                sTemplate = sTemplate.Replace("##PROPOSTANUMERO##", propostaNumero);
                sTemplate = sTemplate.Replace("#MENSAGEM#", comentario);
                sTemplate = sTemplate.Replace("##LINKLOGO##", URL + "/assets/images/logo-yara-email.jpg");
                sTemplate = sTemplate.Replace("##TIPOACESSO##", $"Para maiores informações {(usuarioDto.TipoAcesso == TipoAcessoDto.AD ? $"<a style=\"color: #3498db; cursor: pointer; font-size: 14px; font-weight: bold; text-decoration: none;\" href=\"{URL + "/#/conta-cliente/detalhe/" + contaClienteID + linkTipoProposta}\" target=\"_blank\">acesse</a> o portal de crédito e cobrança" : "acesse o portal de crédito e cobrança pelo Salesforce")}.");

                mail.From = new MailAddress(_from, $"Informações da Proposta {propostaNumero}"); //To send
                mail.To.Add(new MailAddress(usuarioDto.Email, usuarioDto.Nome));
                mail.Subject = "Retorno Automático - " + cliente.Nome;

                mail.Body = sTemplate;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                await smtpServer.SendMailAsync(mail);

                return new KeyValuePair<bool, string>(true, "Enviado email com sucesso.");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, "Ocorreu um erro ao tentar encaminhar o email. Erro: " + e.Message);
            }
        }

        public async Task<KeyValuePair<bool, string>> SendMailBlogProposta(BlogDto blogDto, Guid contaClienteID, string URL)
        {
            var wc = new WebClient { Encoding = System.Text.Encoding.UTF8 };

            #region ParametrosConfig
            var param = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.Tipo.Equals("email") && c.EmpresasID.Equals(blogDto.EmpresaID));
            var smtpServer = new SmtpClient();
            var usuariode = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(blogDto.UsuarioCriacaoID));
            foreach (var item in param)
            {
                if (item.Chave.Equals("Host")) { _host = item.Valor; }
                if (item.Chave.Equals("Port")) { _port = Convert.ToInt32(item.Valor); }
                if (item.Chave.Equals("CredentialName")) { _credentialsName = item.Valor; }
                if (item.Chave.Equals("CredentialPassword")) { _credentialsPass = item.Valor; }
                //if (item.Chave.Equals("From")) { _from = item.Valor; }
                _from = usuariode.Email;
            }
            #endregion

            var usuario = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(blogDto.ParaID.Value));
            var propostaLc = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(blogDto.Area));
            var propostaAb = await _unitOfWork.PropostaAbonoRepository.GetAsync(c => c.ID.Equals(blogDto.Area));
            var propostaPr = await _unitOfWork.PropostaProrrogacao.GetAsync(c => c.ID.Equals(blogDto.Area));

            var propostaNumero = "";
            var tipoProposta = "";

            if (propostaLc != null)
            {
                propostaNumero = $"LC{propostaLc.NumeroInternoProposta:00000}/{propostaLc.DataCriacao:yyyy}";
                tipoProposta = "/proposta-lc";
            }
            else if (propostaAb != null)
            {
                propostaNumero = $"A{propostaAb.NumeroInternoProposta:00000}/{propostaAb.DataCriacao:yyyy}";
                tipoProposta = "/proposta-abono";
            }
            else if (propostaPr != null)
            {
                propostaNumero = $"P{propostaPr.NumeroInternoProposta:00000}/{propostaPr.DataCriacao:yyyy}";
                tipoProposta = "/proposta-prorrogacao";
            }

            try
            {
                smtpServer.Host = _host;
                smtpServer.Port = _port;
                smtpServer.Credentials = new NetworkCredential(_credentialsName, _credentialsPass);
                smtpServer.EnableSsl = _enableSsl;

                var mail = new MailMessage();

                var sTemplate = wc.DownloadString($"{AppDomain.CurrentDomain.BaseDirectory}TemplateBlog.html");

                //Altera variaveis no template
                sTemplate = sTemplate.Replace("##USUARIO##", usuario.Nome);
                sTemplate = sTemplate.Replace("##PROPOSTANUMERO##", propostaNumero);
                sTemplate = sTemplate.Replace("#MENSAGEM#", blogDto.Mensagem);
                sTemplate = sTemplate.Replace("##LINKLOGO##", URL + "/assets/images/logo-yara-email.jpg");
                sTemplate = sTemplate.Replace("##LINKPROPOSTA##", URL + "/#/conta-cliente/detalhe/" + contaClienteID + tipoProposta);

                mail.From = new MailAddress(_from, usuariode.Nome); //To send
                mail.To.Add(new MailAddress(usuario.Email, usuario.Nome));
                mail.Subject = "Nova mensagem de Blog da proposta " + propostaNumero;

                mail.Body = sTemplate;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                await smtpServer.SendMailAsync(mail);

                return new KeyValuePair<bool, string>(true, "Enviado email com sucesso.");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, "Ocorreu um erro ao tentar encaminhar o email. Erro: " + e.Message);
            }
        }

        // NO REFERENCES, IS THIS DEPRECATED?
        /*
        public async Task<KeyValuePair<bool, string>> SendMailPropostaResponsavel(List<UsuarioDto> usuarioDto, bool status, string EmpresaID, string propostaNumero, string URL)
        {
            var wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;

            #region ParametrosConfig
            var param = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.Tipo.Equals("email") && c.EmpresasID.Equals(EmpresaID));
            var smtpServer = new SmtpClient();

            foreach (var item in param)
            {
                if (item.Chave.Equals("Host")) { _host = item.Valor; }
                if (item.Chave.Equals("Port")) { _port = Convert.ToInt32(item.Valor); }
                if (item.Chave.Equals("CredentialName")) { _credentialsName = item.Valor; }
                if (item.Chave.Equals("CredentialPassword")) { _credentialsPass = item.Valor; }
                if (item.Chave.Equals("From")) { _from = item.Valor; }
            }
            #endregion

            try
            {
                smtpServer.Host = _host;
                smtpServer.Port = _port;
                smtpServer.Credentials = new NetworkCredential(_credentialsName, _credentialsPass);
                smtpServer.EnableSsl = _enableSsl;

                foreach (var user in usuarioDto)
                {
                    var mail = new MailMessage();

                    var sTemplate = wc.DownloadString($"{AppDomain.CurrentDomain.BaseDirectory}TemplatePropostaResponsavel.html");

                    //Altera variaveis no template
                    sTemplate = sTemplate.Replace("##USUARIO##", user.Nome);
                    sTemplate = sTemplate.Replace("##PROPOSTANUMERO##", propostaNumero);

                    // sTemplate = sTemplate.Replace("##USUARIO##", usuarioDto.Nome);
                    // sTemplate = sTemplate.Replace("##GRUPOECONOMICO##", grupoNome);
                    // sTemplate = sTemplate.Replace("##STATUS##", status ? "APROVADA" : "REPROVADA");
                    // sTemplate = sTemplate.Replace("##LINKLOGO##", URL + "/assets/images/logo-yara-email.jpg");
                    // sTemplate = sTemplate.Replace("##LINKCOCKPIT##", URL + "/#/cockpit");

                    mail.From = new MailAddress(_from, "Retorno Fluxo de Grupo Econômico"); //To send
                    mail.To.Add(new MailAddress(user.Email, user.Nome));
                    mail.Subject = $"Proposta {propostaNumero} para Acompanhamento";

                    mail.Body = sTemplate;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                    await smtpServer.SendMailAsync(mail);
                }

                return new KeyValuePair<bool, string>(true, "Enviado email com sucesso.");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, "Ocorreu um erro ao tentar encaminhar o email. Erro: " + e.Message);
            }
        }
        */

        public async Task<KeyValuePair<bool, string>> SendMailComiteLC(PropostaLCComiteDto comite, string URL)
        {
            var wc = new WebClient { Encoding = System.Text.Encoding.UTF8 };

            #region ParametrosConfig
            var param = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.Tipo.Equals("email") && c.EmpresasID.Equals(comite.EmpresaID));
            var smtpServer = new SmtpClient();

            foreach (var item in param)
            {
                if (item.Chave.Equals("Host")) { _host = item.Valor; }
                if (item.Chave.Equals("Port")) { _port = Convert.ToInt32(item.Valor); }
                if (item.Chave.Equals("CredentialName")) { _credentialsName = item.Valor; }
                if (item.Chave.Equals("CredentialPassword")) { _credentialsPass = item.Valor; }
                if (item.Chave.Equals("From")) { _from = item.Valor; }
            }
            #endregion

            var proposta = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.EmpresaID.Equals(comite.EmpresaID) && c.ID.Equals(comite.PropostaLCID));
            var propostaNumero = string.Format("LC{0:00000}/{1:yyyy}", proposta.NumeroInternoProposta, proposta.DataCriacao);
            var cliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(proposta.ContaClienteID));
            var propostaComite = await _unitOfWork.PropostaLcComiteRepository.GetAsync(c => c.PropostaLCID.Equals(comite.PropostaLCID) && (c.StatusComiteID == "AA" || c.StatusComiteID == "PE"));

            try
            {
                smtpServer.Host = _host;
                smtpServer.Port = _port;
                smtpServer.Credentials = new NetworkCredential(_credentialsName, _credentialsPass);
                smtpServer.EnableSsl = _enableSsl;

                var mail = new MailMessage();

                //Obtendo o conteúdo do template
                var sTemplate = wc.DownloadString($"{AppDomain.CurrentDomain.BaseDirectory}\\TemplatePropostaComite.html");

                //Altera variaveis no template
                sTemplate = sTemplate.Replace("##USUARIO##", proposta.Responsavel.Nome);
                sTemplate = sTemplate.Replace("##TIPOPROPOSTA##", "de limite de crédito");
                sTemplate = sTemplate.Replace("##PROPOSTANUMERO##", propostaNumero);
                sTemplate = sTemplate.Replace("##LINKLOGO##", URL + "/assets/images/logo-yara-email.jpg");
                sTemplate = sTemplate.Replace("##STATUSPROPOSTA##", "de sua " + (propostaComite != null ? "aprovação" : "atuação"));
                sTemplate = sTemplate.Replace("##LINKPROPOSTA##", URL + "/#/conta-cliente/detalhe/" + proposta.ContaClienteID + "/proposta-lc/" + comite.PropostaLCID);

                mail.From = new MailAddress(_from, "Fluxo de Comite"); //To send
                mail.To.Add(new MailAddress(proposta.Responsavel.Email, proposta.Responsavel.Nome));
                mail.Subject = "Comitê de liberação de Limite de Crédito - " + cliente.Nome;

                mail.Body = sTemplate;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                await smtpServer.SendMailAsync(mail);

                return new KeyValuePair<bool, string>(true, "Enviado email com sucesso.");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, "Ocorreu um erro ao tentar encaminhar o email. Erro: " + e.Message);
            }
        }

        public async Task<KeyValuePair<bool, string>> SendMailComiteLCAdicional(PropostaLCAdicionalComiteDto comite, string URL)
        {
            var wc = new WebClient { Encoding = System.Text.Encoding.UTF8 };

            #region ParametrosConfig
            var param = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.Tipo.Equals("email") && c.EmpresasID.Equals(comite.EmpresaID));
            var smtpServer = new SmtpClient();

            foreach (var item in param)
            {
                if (item.Chave.Equals("Host")) { _host = item.Valor; }
                if (item.Chave.Equals("Port")) { _port = Convert.ToInt32(item.Valor); }
                if (item.Chave.Equals("CredentialName")) { _credentialsName = item.Valor; }
                if (item.Chave.Equals("CredentialPassword")) { _credentialsPass = item.Valor; }
                if (item.Chave.Equals("From")) { _from = item.Valor; }
            }
            #endregion

            var proposta = await _unitOfWork.PropostaLCAdicionalRepository.GetAsync(c => c.EmpresaID.Equals(comite.EmpresaID) && c.ID.Equals(comite.PropostaLCAdicionalID));
            var propostaNumero = string.Format("LA{0:00000}/{1:yyyy}", proposta.NumeroInternoProposta, proposta.DataCriacao);
            var cliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(proposta.ContaClienteID));
            var propostaComite = await _unitOfWork.PropostaLCAdicionalComiteRepository.GetAsync(c => c.PropostaLCAdicionalID.Equals(comite.PropostaLCAdicionalID) && (c.PropostaLCAdicionalStatusComiteID == "AA" || c.PropostaLCAdicionalStatusComiteID == "PE"));

            try
            {
                smtpServer.Host = _host;
                smtpServer.Port = _port;
                smtpServer.Credentials = new NetworkCredential(_credentialsName, _credentialsPass);
                smtpServer.EnableSsl = _enableSsl;

                var mail = new MailMessage();

                //Obtendo o conteúdo do template
                var sTemplate = wc.DownloadString($"{AppDomain.CurrentDomain.BaseDirectory}\\TemplatePropostaComite.html");

                //Altera variaveis no template
                sTemplate = sTemplate.Replace("##USUARIO##", proposta.Responsavel.Nome);
                sTemplate = sTemplate.Replace("##TIPOPROPOSTA##", "de limite de crédito adicional");
                sTemplate = sTemplate.Replace("##PROPOSTANUMERO##", propostaNumero);
                sTemplate = sTemplate.Replace("##LINKLOGO##", URL + "/assets/images/logo-yara-email.jpg");
                sTemplate = sTemplate.Replace("##STATUSPROPOSTA##", "de sua " + (propostaComite != null ? "aprovação" : "atuação"));
                sTemplate = sTemplate.Replace("##LINKPROPOSTA##", URL + "/#/conta-cliente/detalhe/" + proposta.ContaClienteID + "/proposta-la/" + comite.PropostaLCAdicionalID);

                mail.From = new MailAddress(_from, "Fluxo de Comite Adicional"); //To send
                mail.To.Add(new MailAddress(proposta.Responsavel.Email, proposta.Responsavel.Nome));
                mail.Subject = "Comitê de liberação de Limite de Crédito Adicional - " + cliente.Nome;

                mail.Body = sTemplate;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                await smtpServer.SendMailAsync(mail);

                return new KeyValuePair<bool, string>(true, "Enviado email com sucesso.");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, "Ocorreu um erro ao tentar encaminhar o email. Erro: " + e.Message);
            }
        }

        public async Task<KeyValuePair<bool, string>> SendMailComiteAbono(PropostaAbonoComiteDto comite, string URL)
        {
            var wc = new WebClient { Encoding = System.Text.Encoding.UTF8 };

            #region ParametrosConfig
            var param = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.Tipo.Equals("email") && c.EmpresasID.Equals(comite.EmpresaID));
            var smtpServer = new SmtpClient();

            foreach (var item in param)
            {
                if (item.Chave.Equals("Host")) { _host = item.Valor; }
                if (item.Chave.Equals("Port")) { _port = Convert.ToInt32(item.Valor); }
                if (item.Chave.Equals("CredentialName")) { _credentialsName = item.Valor; }
                if (item.Chave.Equals("CredentialPassword")) { _credentialsPass = item.Valor; }
                if (item.Chave.Equals("From")) { _from = item.Valor; }
            }
            #endregion

            var proposta = await _unitOfWork.PropostaAbonoRepository.GetAsync(c => c.EmpresaID.Equals(comite.EmpresaID) && c.ID.Equals(comite.PropostaAbonoID));
            var propostaNumero = string.Format("AB{0:00000}/{1:yyyy}", proposta.NumeroInternoProposta, proposta.DataCriacao);
            var cliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(proposta.ContaClienteID));
            var propostaComite = await _unitOfWork.PropostaAbonoComite.GetAsync(c => c.PropostaAbonoID.Equals(comite.PropostaAbonoID) && !c.Aprovado);

            try
            {
                smtpServer.Host = _host;
                smtpServer.Port = _port;
                smtpServer.Credentials = new NetworkCredential(_credentialsName, _credentialsPass);
                smtpServer.EnableSsl = _enableSsl;

                var mail = new MailMessage();

                //Obtendo o conteúdo do template
                var sTemplate = wc.DownloadString($"{AppDomain.CurrentDomain.BaseDirectory}\\TemplatePropostaComite.html");

                //Altera variaveis no template
                sTemplate = sTemplate.Replace("##USUARIO##", proposta.Responsavel.Nome);
                sTemplate = sTemplate.Replace("##TIPOPROPOSTA##", "de abono");
                sTemplate = sTemplate.Replace("##PROPOSTANUMERO##", propostaNumero);
                sTemplate = sTemplate.Replace("##LINKLOGO##", URL + "/assets/images/logo-yara-email.jpg");
                sTemplate = sTemplate.Replace("##STATUSPROPOSTA##", (propostaComite != null ? "de sua aprovação" : "do seu abono"));
                sTemplate = sTemplate.Replace("##LINKPROPOSTA##", URL + "/#/conta-cliente/detalhe/" + proposta.ContaClienteID + "/proposta-abono");

                mail.From = new MailAddress(_from, "Fluxo de Comite"); //To send
                mail.To.Add(new MailAddress(proposta.Responsavel.Email, proposta.Responsavel.Nome));
                mail.Subject = "Comitê de liberação de Abono - " + cliente.Nome;

                mail.Body = sTemplate;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                await smtpServer.SendMailAsync(mail);

                return new KeyValuePair<bool, string>(true, "Enviado email com sucesso.");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, "Ocorreu um erro ao tentar encaminhar o email. Erro: " + e.Message);
            }
        }

        public async Task<KeyValuePair<bool, string>> SendMailComiteProrrogacao(PropostaProrrogacaoComiteDto comite, string URL)
        {
            var wc = new WebClient { Encoding = System.Text.Encoding.UTF8 };

            #region ParametrosConfig
            var param = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.Tipo.Equals("email") && c.EmpresasID.Equals(comite.EmpresaID));
            var smtpServer = new SmtpClient();

            foreach (var item in param)
            {
                if (item.Chave.Equals("Host")) { _host = item.Valor; }
                if (item.Chave.Equals("Port")) { _port = Convert.ToInt32(item.Valor); }
                if (item.Chave.Equals("CredentialName")) { _credentialsName = item.Valor; }
                if (item.Chave.Equals("CredentialPassword")) { _credentialsPass = item.Valor; }
                if (item.Chave.Equals("From")) { _from = item.Valor; }
            }
            #endregion

            var proposta = await _unitOfWork.PropostaProrrogacao.GetAsync(c => c.EmpresaID.Equals(comite.EmpresaID) && c.ID.Equals(comite.PropostaProrrogacaoID));
            var propostaNumero = string.Format("PR{0:00000}/{1:yyyy}", proposta.NumeroInternoProposta, proposta.DataCriacao);
            var cliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(proposta.ContaClienteID));
            var propostaComite = await _unitOfWork.PropostaProrrogacaoComite.GetAsync(c => c.PropostaProrrogacaoID.Equals(comite.PropostaProrrogacaoID) && !c.Aprovado);

            try
            {
                smtpServer.Host = _host;
                smtpServer.Port = _port;
                smtpServer.Credentials = new NetworkCredential(_credentialsName, _credentialsPass);
                smtpServer.EnableSsl = _enableSsl;

                var mail = new MailMessage();

                //Obtendo o conteúdo do template
                var sTemplate = wc.DownloadString($"{AppDomain.CurrentDomain.BaseDirectory}\\TemplatePropostaComite.html");

                //Altera variaveis no template
                sTemplate = sTemplate.Replace("##USUARIO##", proposta.Responsavel.Nome);
                sTemplate = sTemplate.Replace("##TIPOPROPOSTA##", "de prorrogação");
                sTemplate = sTemplate.Replace("##PROPOSTANUMERO##", propostaNumero);
                sTemplate = sTemplate.Replace("##LINKLOGO##", URL + "/assets/images/logo-yara-email.jpg");
                sTemplate = sTemplate.Replace("##STATUSPROPOSTA##", "de sua " + (propostaComite != null ? "aprovação" : "prorrogação"));
                sTemplate = sTemplate.Replace("##LINKPROPOSTA##", URL + "/#/conta-cliente/detalhe/" + proposta.ContaClienteID + "/proposta-prorrogacao");

                mail.From = new MailAddress(_from, "Fluxo de Comite"); //To send
                mail.To.Add(new MailAddress(proposta.Responsavel.Email, proposta.Responsavel.Nome));
                mail.Subject = "Comitê de liberação de Prorrogação - " + cliente.Nome;

                mail.Body = sTemplate;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                await smtpServer.SendMailAsync(mail);

                return new KeyValuePair<bool, string>(true, "Enviado email com sucesso.");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, "Ocorreu um erro ao tentar encaminhar o email. Erro: " + e.Message);
            }
        }

        public async Task<KeyValuePair<bool, string>> SendMailComiteRenovacaoVigenciaLC(PropostaRenovacaoVigenciaLCComiteDto comite, string URL)
        {
            var wc = new WebClient { Encoding = System.Text.Encoding.UTF8 };

            #region ParametrosConfig
            var param = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.Tipo.Equals("email") && c.EmpresasID.Equals(comite.EmpresaID));
            var smtpServer = new SmtpClient();

            foreach (var item in param)
            {
                if (item.Chave.Equals("Host")) { _host = item.Valor; }
                if (item.Chave.Equals("Port")) { _port = Convert.ToInt32(item.Valor); }
                if (item.Chave.Equals("CredentialName")) { _credentialsName = item.Valor; }
                if (item.Chave.Equals("CredentialPassword")) { _credentialsPass = item.Valor; }
                if (item.Chave.Equals("From")) { _from = item.Valor; }
            }
            #endregion

            var proposta = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetAsync(c => c.EmpresaID.Equals(comite.EmpresaID) && c.ID.Equals(comite.PropostaRenovacaoVigenciaLCID));
            var propostaNumero = string.Format("RV{0:00000}/{1:yyyy}", proposta.NumeroInternoProposta, proposta.DataCriacao);
            var propostaComite = await _unitOfWork.PropostaRenovacaoVigenciaLCComiteRepository.GetAsync(c => c.PropostaRenovacaoVigenciaLCID.Equals(comite.PropostaRenovacaoVigenciaLCID) && (c.StatusComiteID == "AA" || c.StatusComiteID == "PE"));

            try
            {
                smtpServer.Host = _host;
                smtpServer.Port = _port;
                smtpServer.Credentials = new NetworkCredential(_credentialsName, _credentialsPass);
                smtpServer.EnableSsl = _enableSsl;

                var mail = new MailMessage();

                //Obtendo o conteúdo do template
                var sTemplate = wc.DownloadString($"{AppDomain.CurrentDomain.BaseDirectory}\\TemplatePropostaComite.html");

                //Altera variaveis no template
                sTemplate = sTemplate.Replace("##USUARIO##", proposta.Responsavel.Nome);
                sTemplate = sTemplate.Replace("##TIPOPROPOSTA##", "de renovação de vigência de limite de crédito");
                sTemplate = sTemplate.Replace("##PROPOSTANUMERO##", propostaNumero);
                sTemplate = sTemplate.Replace("##LINKLOGO##", URL + "/assets/images/logo-yara-email.jpg");
                sTemplate = sTemplate.Replace("##STATUSPROPOSTA##", "de sua " + (propostaComite != null ? "aprovação" : "atuação"));
                sTemplate = sTemplate.Replace("##LINKPROPOSTA##", URL + "/#/renovacao-vigencia/" + proposta.ID);

                mail.From = new MailAddress(_from, "Fluxo de Comite"); //To send
                mail.To.Add(new MailAddress(proposta.Responsavel.Email, proposta.Responsavel.Nome));
                mail.Subject = "Comitê de liberação de Renovação de Vigência de Limite de Crédito - " + propostaNumero;

                mail.Body = sTemplate;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                await smtpServer.SendMailAsync(mail);

                return new KeyValuePair<bool, string>(true, "Enviado email com sucesso.");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, "Ocorreu um erro ao tentar encaminhar o email. Erro: " + e.Message);
            }
        }

        public async Task<string> SendMailPropostaJuridica(PropostaJuridicoDto juridicoDto, ContaClienteDto clienteDto, string URL)
        {
            var wc = new WebClient { Encoding = Encoding.UTF8 };

            try
            {
                //Obtendo o conteúdo do template
                var sTemplate = wc.DownloadString($"{AppDomain.CurrentDomain.BaseDirectory}TemplatePropostaJuridica.html");

                sTemplate = sTemplate.Replace("##NOMECLIENTE##", clienteDto.Nome);
                sTemplate = sTemplate.Replace("##APELIDOCLIENTE##", clienteDto.Apelido);
                sTemplate = sTemplate.Replace("##DOCUMENTOCLIENTE##", clienteDto.Documento);
                sTemplate = sTemplate.Replace("##CODIGOCLIENTE##", clienteDto.CodigoPrincipal);
                sTemplate = sTemplate.Replace("##GRUPOECONOMICOCLIENTE##", (clienteDto.GrupoEconomicos.Count > 0 ? "Sim" : "Não"));
                sTemplate = sTemplate.Replace("##VALORTOTAL##", juridicoDto.ValorEnvio.HasValue ? juridicoDto.ValorEnvio.Value.ToString("#,##0.00") : 0.ToString("#,##0.00"));
                sTemplate = sTemplate.Replace("##LINKLOGO##", URL + "/assets/images/logo-yara-email.jpg");

                var sHtmlTitulos = new StringBuilder();

                foreach (var item in juridicoDto.PropostaJuridicoTitulos)
                {
                    sHtmlTitulos.Append("<tr style='text-align:center'>");
                    sHtmlTitulos.Append("<td>" + item.NotaFiscal + "</td>");
                    sHtmlTitulos.Append("<td>" + item.OrdemVendaNumero + "</td>");
                    sHtmlTitulos.Append("<td>" + item.DataEmissaoDocumento + "</td>");
                    sHtmlTitulos.Append("<td>" + item.ValorInterno + "</td>");
                    sHtmlTitulos.Append("<td>" + item.ValorInterno + "</td>");
                    sHtmlTitulos.Append("<td></td>");
                    sHtmlTitulos.Append("<td>" + item.TaxaJuros + "</td>");
                    sHtmlTitulos.Append("<td>" + item.DataOriginal + "</td>");
                    sHtmlTitulos.Append("<td>" + item.DataDuplicata + "</td>");
                    sHtmlTitulos.Append("<td>" + item.DataPefinInclusao + "</td>");
                    sHtmlTitulos.Append("<td>" + item.DataPefinExclusao + "</td>");
                    sHtmlTitulos.Append("</tr>");
                }

                sTemplate = sTemplate.Replace("##TABELATITULOS##", sHtmlTitulos.ToString());

                return sTemplate;
            }
            catch (Exception e)
            {
                return "Ocorreu um erro ao tentar encaminhar o email. Erro: " + e.Message;
            }
        }

        public async Task<KeyValuePair<bool, string>> SendMailCockpitNotificacaoUsuario(IEnumerable<NotificacaoUsuarioDto> notificacaoUsuarioDto, UsuarioDto usuarioDto, string empresa, string URLcockpit, string URLcadastroCliente)
        {
            var wc = new WebClient { Encoding = System.Text.Encoding.UTF8 };

            #region ParametrosConfig
            var param = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.Tipo.Equals("email") && c.EmpresasID.Equals(empresa));
            var smtpServer = new SmtpClient();

            foreach (var item in param)
            {
                if (item.Chave.Equals("Host")) { _host = item.Valor; }
                if (item.Chave.Equals("Port")) { _port = Convert.ToInt32(item.Valor); }
                if (item.Chave.Equals("CredentialName")) { _credentialsName = item.Valor; }
                if (item.Chave.Equals("CredentialPassword")) { _credentialsPass = item.Valor; }
                if (item.Chave.Equals("From")) { _from = item.Valor; }
            }
            #endregion

            try
            {
                smtpServer.Host = _host;
                smtpServer.Port = _port;
                smtpServer.Credentials = new NetworkCredential(_credentialsName, _credentialsPass);
                smtpServer.EnableSsl = _enableSsl;

                var mail = new MailMessage();

                //Obtendo o conteúdo do template
                var sTemplate = wc.DownloadString($"{AppDomain.CurrentDomain.BaseDirectory}TemplateNotificacaoCockpit.html");

                //Altera variaveis no template
                sTemplate = sTemplate.Replace("##USUARIO##", usuarioDto.Nome);
                sTemplate = sTemplate.Replace("##LINKCOCKPIT##", URLcockpit);

                var stbTabela = new StringBuilder();

                if (notificacaoUsuarioDto != null && notificacaoUsuarioDto.Count() > 0)
                {
                    string tituloTabela = string.Empty;
                    string linkProposta = string.Empty;
                    string linkTipoProposta = String.Empty;

                    foreach (var linhaCockpi in notificacaoUsuarioDto)
                    {
                        if (!tituloTabela.Equals(linhaCockpi.Interacao))
                        {
                            if (!string.IsNullOrEmpty(tituloTabela))
                            {
                                stbTabela.Append("</tbody>");
                                stbTabela.Append("</table>");
                            }
                            tituloTabela = linhaCockpi.Interacao;

                            stbTabela.Append("<br /><br />");
                            stbTabela.Append("<div class='title-proposal'>" + tituloTabela + "</div>");
                            stbTabela.Append("<table border='0' cellpadding='0' cellspacing='1'>");
                            stbTabela.Append("<thead>");
                            stbTabela.Append("<tr class='color-tile-table'>");
                            stbTabela.Append("<th>ID da Proposta</th>");
                            stbTabela.Append("<th>Nome do Cliente</th>");
                            stbTabela.Append("<th>CG</th>");
                            stbTabela.Append("<th>CTC</th>");
                            stbTabela.Append("<th>Valor Proposto (R$)</th>");
                            stbTabela.Append("<th>Status</th>");
                            stbTabela.Append("<th>LC Atual</th>");
                            stbTabela.Append("<th>Vigência LC</th>");
                            stbTabela.Append("<th>Leadtime (dias)</th>");
                            stbTabela.Append("</tr>");
                            stbTabela.Append("</thead>");
                            stbTabela.Append("<tbody>");
                        }

                        if (tituloTabela.Equals("Porposta de Crédito"))
                        {
                            linkTipoProposta = "/proposta-lc";
                        }
                        else if (tituloTabela.Equals("Porposta de Crédito Adicional"))
                        {
                            linkTipoProposta = "/proposta-la";
                        }
                        else if (tituloTabela.Equals("Propostas de Abono"))
                        {
                            linkTipoProposta = "/proposta-abono";
                        }
                        else if (tituloTabela.Equals("Alçada Comercial"))
                        {
                            linkTipoProposta = "/proposta-alcada";
                        }
                        else if (tituloTabela.Equals("Proposta de Prorrogação"))
                        {
                            linkTipoProposta = "/proposta-prorrogacao";
                        }
                        else if (tituloTabela.Equals("Renovação de Vigência de LC"))
                        {
                            linkTipoProposta = "/renovacao-vigencia";
                        }
                        linkProposta = URLcadastroCliente + linhaCockpi.ContaClienteId + linkTipoProposta;

                        stbTabela.Append("<tr>");
                        stbTabela.Append("<td class='align-center'> <a href='" + linkProposta + "' target='_blank'>" + linhaCockpi.IdProposta + "</td>");
                        stbTabela.Append("<td class='align-left'>" + linhaCockpi.NomeCliente + "</a></td>");
                        stbTabela.Append("<td class='align-center'>" + linhaCockpi.CG + "</td>");
                        stbTabela.Append("<td class='align-center'>" + linhaCockpi.CTC + "</td>");
                        stbTabela.Append("<td class='align-right'>" + linhaCockpi.Valor.ToString("#,##0.00") + "</td>");
                        stbTabela.Append("<td class='align-center'>" + linhaCockpi.Status + "</td>");
                        stbTabela.Append("<td class='align-right'>" + linhaCockpi.LCAtual.ToString("#,##0.00") + "</td>");
                        stbTabela.Append("<td class='align-center'>" + (linhaCockpi.VigenciaLC == DateTime.MinValue ? "" : linhaCockpi.VigenciaLC.ToString("dd/MM/yyyy")) + "</td>");
                        stbTabela.Append("<td class='align-center'>" + linhaCockpi.Leadtime.ToString("#,##0") + "</td>");
                        stbTabela.Append("</tr>");
                    }

                    stbTabela.Append("</tbody>");
                    stbTabela.Append("</table>");
                }
                sTemplate = sTemplate.Replace("##TABELAS_COCKPIT##", stbTabela.ToString());

                mail.From = new MailAddress(_from, "Fluxo de Comite"); //To send
                mail.To.Add(new MailAddress(usuarioDto.Email, usuarioDto.Nome));
                mail.Subject = "Notificação de Propostas pendentes no Cockpit";

                mail.Body = sTemplate;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                await smtpServer.SendMailAsync(mail);

                return new KeyValuePair<bool, string>(true, "Enviado email com sucesso.");
            }
            catch (Exception e)
            {
                return new KeyValuePair<bool, string>(false, "Ocorreu um erro ao tentar encaminhar o email. Erro: " + e.Message);
            }
        }

    }
}
