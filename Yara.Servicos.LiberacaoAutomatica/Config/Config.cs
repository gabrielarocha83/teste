using System.Configuration;

// ReSharper disable once CheckNamespace
namespace Yara.Servicos.LiberacaoAutomatica
{
    public static class Config
    {
        public static string ConnectionStringYara => ConfigurationManager.ConnectionStrings["ConnectionYara"]
            .ToString();

        public static string UserNameSAP => @"WS-USER";
        public static string PasswordSAP => @"R3moto#!2015";

        public static string URLEndpointSAP =>
            @"http://yarpodev01.eastus2.cloudapp.azure.com:50000/XISOAPAdapter/MessageServlet?senderParty=&amp;senderService=BS_CreditoCobranca&amp;receiverParty=&amp;receiverService=&amp;interface=SoapAtualizaBloqueio_Out&amp;interfaceNamespace=http%3A%2F%2Fcreditocobranca%2FAtualizaBloqueio";
    }
}
