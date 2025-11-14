using System.Collections.Generic;

namespace Yara.Service.Serasa.Common
{
    public static class NaturezaOperacao
    {
        //public static Dictionary<string, string> situacao =
        //    new Dictionary<string, string>
        //    {
        //        {"02", "ATIVA"},
        //        {"03", "INATIVA"},
        //        {"00", "INAPTA"},
        //        {"04", "NÃO LOCALIZADA"},
        //        {"05", "EM LIQUIDAÇÃO"},
        //        {"07", "NÃO CADASTRADA"},
        //        {"06", "SUSPENSO"},
        //        {"09", "CANCELADO"}
        //    };

        //public static Dictionary<string, string> situacaocadastral = new Dictionary<string, string>
        //{
        //    {"2", "ATIVA"},
        //    {"0", "INAPTA"},
        //    {"4", "NULA"},
        //    {"7", "BAIXADA"},
        //    {"6", "SUSPENSA"}
        //};

        public static Dictionary<string, string> NaturezaDictionary = new Dictionary<string, string>
            {
                {"AD","adiantamento a depositantes - c/c devedores em geral"},
                {"AG","empréstimos agrícolas e industriais - financiamentos de custeio de investimentos agrícolas e industriais"},
                {"AR","arrendamentos, inclusive leasing"},
                {"CA","operações de câmbio - operações e financiamentos de câmbio em geral"},
                {"CB","CDC outros bens móveis"},
                {"CH","adiantamento a depositantes - cheques sem fundos acolhidos"},
                {"CL","CDC veículos leves"},
                {"CM","CDC motocicletas e motonetas"},
                {"CR","impedidos pelo Banco Central de obtenção de crédito rural"},
                {"CT","cartão de crédito"},
                {"CV","CDC veículos pesados: tratores, ônibus, caminhões, barcos e aviões"},
                {"C2","consórcio veículos pesados: tratores, ônibus, caminhões, barcos e aviões"},
                {"C3","consórcio veículos leves"},
                {"C4","consórcio motocicletas e motonetas"},
                {"C5","consórcio outros bens móveis"},
                {"EC","empréstimos em contas - c/c garantidas, financiamentos de capital de giro, programas  especiais, etc"},
                {"FI","créditos e financiamentos - empréstimos a pessoas físicas , financiamentos ao consumidor final , etc"},
                {"IM","operações imobiliárias em geral, inclusive de sociedades de poupança"},
                {"IP","débito IPVA"},
                {"LL","leasing veículos leves"},
                {"LM","leasing motocicletas e motonetas"},
                {"LV","leasing veículos pesados: tratores, ônibus, caminhões, barcos e aviões"},
                {"OJ","operações ajuizadas"},
                {"OO","outras operações - diversas operações que não se enquadram nas demais"},
                {"RE","operações de repasse - operações 63, FINAME, REINVEST, RECON, PROALCOOL, etc"},
                {"SR","seguro de risco decorrido"},
                {"TD","títulos descontados - descontos de duplicatas, promissórias e outros títulos"}
            };
    }
}
