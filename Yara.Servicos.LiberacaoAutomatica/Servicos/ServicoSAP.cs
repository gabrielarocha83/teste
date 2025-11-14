using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Yara.Servicos.LiberacaoAutomatica.Repositorio;
using Yara.Servicos.LiberacaoAutomatica.YaraSAP;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedVariable

namespace Yara.Servicos.LiberacaoAutomatica.Servicos
{
    public class ServicoSAP:IDisposable
    {
        Logs registroLog = new Logs();

        public async Task Iniciar()
        {
            try
            {
                var binding =
                    new BasicHttpBinding
                    {
                        Security =
                        {
                            Mode = BasicHttpSecurityMode.TransportCredentialOnly,
                            Transport = {ClientCredentialType = HttpClientCredentialType.Basic, Realm = "XISOAPApps"}
                        }
                    };

                var endpoint = new EndpointAddress(Config.URLEndpointSAP);
            
                using (var valores = new SoapAtualizaBloqueio_OutClient(binding, endpoint))
                {
                    if (valores.ClientCredentials != null)
                    {
                        valores.ClientCredentials.UserName.UserName = Config.UserNameSAP;
                        valores.ClientCredentials.UserName.Password = Config.PasswordSAP;
                    }

                    valores.Open();

                    var liberacao = new LiberacaoCarteiras();
                    var liberacoes = await liberacao.ObterDivisoesLiberadasSAP();

                    var response = await valores.SoapAtualizaBloqueio_OutAsync(liberacoes.ToArray());

                    valores.Close();
                }
            }
            catch (Exception e)
            {
                registroLog.LogError(e);
            }




        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}