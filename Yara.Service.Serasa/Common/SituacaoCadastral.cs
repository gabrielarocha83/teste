using System.Collections.Generic;

namespace Yara.Service.Serasa.Common
{
    public static class SituacaoCadastral
    {
        public static Dictionary<string, string> CPF =
            new Dictionary<string, string>
            {
                {"2", "REGULAR"},
                {"3", "PENDENTE DE REGULARIZAÇÃO"},
                {"0", "INAPTA"},
                {"4", "NULA"},
                {"6", "SUSPENSO"},
                {"9", "CANCELADO"}
            };

        public static Dictionary<string, string> CNPJ = 
            new Dictionary<string, string>
            {
                {"2", "ATIVA"},
                {"0", "INAPTA"},
                {"4", "NULA"},
                {"7", "BAIXADA"},
                {"6", "SUSPENSA"},
                {"9", "CANCELADA"}
            };
    }
}