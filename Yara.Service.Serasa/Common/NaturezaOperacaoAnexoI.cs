using System.Collections.Generic;

namespace Yara.Service.Serasa.Common
{
    public static class NaturezaOperacaoAnexoI
    {
        public static Dictionary<string, string> NaturezaAnexoI = new Dictionary<string, string>{
            {"BA","BUSCA E APREENSÃO"},
            {"EX","EXECUÇÃO"},
            {"FE","EXECUÇÃO FISCAL ESTADUAL"},
            {"FM","EXECUÇÃO FISCAL MUNICIPAL"},
            {"JB","BUSCA E APREENSÃO - PEQUENAS CAUSAS"},
            {"JE","EXECUÇÃO - PEQUENAS CAUSAS"},
            {"JF","EXECUÇÃO FISCAL FEDERAL"},
            {"TR","EXECUÇÃO DE TÍTULO JUDICIAL TRABALHISTA"},
            {"AF","AUTO FALÊNCIA"},
            {"CD","CONCORDATA DEFERIDA"},
            {"CR","CONCORDATA REQUERIDA"},
            {"CS","CONCORDATA SUSPENSIVA"},
            {"FR","FALÊNCIA REQUERIDA"},
            {"RC","RECUPERAÇÃO JUDICIAL CONCEDIDA"},
            {"RE","RECUPERAÇÃO EXTRAJUDICIAL REQUERIDA"},
            {"RH","RECUPERAÇÃO EXTRAJUDICIAL HOMOLOGADA"},
            {"RR","RECUPERAÇÃO JUDICIAL REQUERIDA"},
            {"FD","FALÊNCIA DECRETADA"},
            {"12","CHEQUE SEM FUNDOS - 2A. APRESENTAÇÃO"},
            {"13","CONTA ENCERRADA"},
            {"14","PRÁTICA ESPÚRIA"}
        };
    }
}
