using System.Text;
using Yara.Service.Serasa.Concentre.Entities;
using Yara.Service.Serasa.Interface;

namespace Yara.Service.Serasa.Concentre.Send
{
    public class HeaderSerasaConcentre : SerasaService<ReturnConcentre>
    {
        private readonly string _codDocumento;
        private string _usuarioSerasa;
        private string _senhaSerasa;
        private bool _persistirPesquisa;

        public HeaderSerasaConcentre(string codDocumento, string usuarioSerasa, string senhaSerasa)
        {
            _codDocumento = codDocumento;
            _usuarioSerasa = usuarioSerasa;
            _senhaSerasa = senhaSerasa;
            _persistirPesquisa = false;
        }
        
        public override string Header()
        {
            var headerSendConcentre = new StringBuilder();

            #region Dados de Acesso

            headerSendConcentre.Append(_usuarioSerasa);                         //Seq.01 - logon
            headerSendConcentre.Append(_senhaSerasa);                           //Seq.02 - senha
            headerSendConcentre.Append(' ', 8);                                 //Seq.03 - novasenha

            #endregion

            #region Cabeçalho Concentre B49C

            headerSendConcentre.Append("B49C");                                 //Seq.01 - FILLER
            headerSendConcentre.Append(' ', 6);                                 //Seq.02 - FILLER
            headerSendConcentre.Append(_codDocumento.PadLeft(15, '0'));         //Seq.03 - NUM DOC
            headerSendConcentre.Append(_codDocumento.Length == 11 ? "F" : "J"); //Seq.04 - TIPO PESSOA
            headerSendConcentre.Append("C".PadRight(6, ' '));                   //Seq.05 - BASE CONS
            headerSendConcentre.Append("FI");                                   //Seq.06 - MODALIDADE
            headerSendConcentre.Append(' ', 7);                                 //Seq.07 - VLR CONSUL
            headerSendConcentre.Append(' ', 12);                                //Seq.08 - CENTRO CUST
            headerSendConcentre.Append("S");                                    //Seq.09 - CODIFICADO
            headerSendConcentre.Append("99");                                   //Seq.10 - QTD REG
            headerSendConcentre.Append("S");                                    //Seq.11 - CONVERSA
            headerSendConcentre.Append(_persistirPesquisa ? "CON" : "INI");     //Seq.12 - FUNÇÃO
            headerSendConcentre.Append("A");                                    //Seq.13 - TP CONSULTA
            headerSendConcentre.Append("N");                                    //Seq.14 - ATUALIZA
            headerSendConcentre.Append(' ', 18);                                //Seq.15 - IDENT_TERM
            headerSendConcentre.Append(' ', 10);                                //Seq.16 - RESCLI
            headerSendConcentre.Append(' ', 1);                                 //Seq.17 - DELTS
            headerSendConcentre.Append(' ', 1);                                 //Seq.18 - COBRA
            headerSendConcentre.Append(' ', 1);                                 //Seq.19 - PASSA
            headerSendConcentre.Append("N");                                    //Seq.20 - CONS.COLLEC
            headerSendConcentre.Append(' ', 57);                                //Seq.21 - FILLER
            headerSendConcentre.Append(' ', 15);                                //Seq.22 - CONSULTANTE
            headerSendConcentre.Append(' ', 234);                               //Seq.23 - FILLER

            #endregion

            #region Complemento Cabeçalho Concentre P002

            headerSendConcentre.Append("P002");                                 //Seq.01 - TIPO–REG
            headerSendConcentre.Append("RSPU");                                 //Seq.02 - COD1
            headerSendConcentre.Append(' ', 21);                                //Seq.03 - CHAVE1
            headerSendConcentre.Append(' ' ,4);                                 //Seq.04 - COD2
            headerSendConcentre.Append(' ', 21);                                //Seq.05 - CHAVE2
            headerSendConcentre.Append(' ', 4);                                 //Seq.06 - COD3
            headerSendConcentre.Append(' ', 21);                                //Seq.07 - CHAVE3
            headerSendConcentre.Append(' ', 4);                                 //Seq.08 - COD4
            headerSendConcentre.Append(' ', 21);                                //Seq.09 - CHAVE4
            headerSendConcentre.Append(' ', 11);                                //Seq.11 - FILLER

            #endregion

            #region Complemento Cabeçalho Concentre I001

            headerSendConcentre.Append("I001");                                 //Seq.01 - Tipo_Reg
            headerSendConcentre.Append("00");                                   //Seq.02 - Subtipo
            headerSendConcentre.Append("D");                                    //Seq.03 - FILLER
            headerSendConcentre.Append("S");                                    //Seq.04 - FILLER
            headerSendConcentre.Append(' ', 1);                                 //Seq.05 - FILLER
            headerSendConcentre.Append(' ', 1);                                 //Seq.06 - FILLER
            headerSendConcentre.Append(' ', 4);                                 //Seq.07 - FILLER
            headerSendConcentre.Append(' ', 1);                                 //Seq.08 - FILLER
            headerSendConcentre.Append(' ', 4);                                 //Seq.09 - FILLER
            headerSendConcentre.Append(' ', 10);                                //Seq.10 - FILLER
            headerSendConcentre.Append(' ', 1);                                 //Seq.11 - FILLER
            headerSendConcentre.Append(' ', 1);                                 //Seq.12 - FILLER
            headerSendConcentre.Append(' ', 2);                                 //Seq.13 - FILLER
            headerSendConcentre.Append(' ', 1);                                 //Seq.14 - FILLER - ALERT SCORING 
            headerSendConcentre.Append(' ', 1);                                 //Seq.15 - FILLER - ALERTA IDENTIDADE
            headerSendConcentre.Append("S");                                    //Seq.16 - FILLER - PARTICIPAÇÃO SOCIETÁRIA
            headerSendConcentre.Append(' ', 2);                                 //Seq.17 - FILLER - AÇÃO POR UF ESPECÍFICO
            headerSendConcentre.Append(' ', 2);                                 //Seq.18 - FILLER - PROTESTO POR UF ESPECÍFICO
            headerSendConcentre.Append(' ', 2);                                 //Seq.19 - FILLER - CONVEM DEVEDORES POR UF ESPECÍFICO
            headerSendConcentre.Append(' ', 2);                                 //Seq.20 - FILLER - PEFIN POR UF ESPECÍFICO
            headerSendConcentre.Append(' ', 2);                                 //Seq.21 - FILLER - REFIN POR UF ESPECÍFICO
            headerSendConcentre.Append(' ', 1);                                 //Seq.22 - FILLER - FATURAMENTO PRESUMIDO
            headerSendConcentre.Append(' ', 1);                                 //Seq.23 - FILLER - RENDA PRESUMIDA
            headerSendConcentre.Append(' ', 1);                                 //Seq.24 - FILLER - SÓCIOS E ADMINISTRADORES
            headerSendConcentre.Append(' ', 1);                                 //Seq.25 - FILLER - PARTICIPAÇÕES EM EMPRESAS
            headerSendConcentre.Append(' ', 1);                                 //Seq.26 - FILLER - INDICADOR DE OPERACIONALIDADE
            headerSendConcentre.Append(' ', 1);                                 //Seq.27 - FILLER - IRM - INDICE RELACIONAMENTO MERCADO
            headerSendConcentre.Append(' ', 4);                                 //Seq.28 - FILLER - Modelo de Alerta de Identidade desejado(PF)
            headerSendConcentre.Append(' ', 4);                                 //Seq.29 - FILLER - Modelo de Limite de Crédito desejado
            headerSendConcentre.Append(' ', 1);                                 //Seq.30 - FILLER - Meio de acesso
            headerSendConcentre.Append(' ', 6);                                 //Seq.31 - FILLER - Uso da SERASA
            headerSendConcentre.Append(' ', 47);                                //Seq.32 - FILLER - Mensagem de erro
            headerSendConcentre.Append(' ', 1);                                 //Seq.31 - FILLER - Uso da SERASA

            headerSendConcentre.Append("T999");                                 //Seq.32 - FILLER - Uso da SERASA

            #endregion

            return headerSendConcentre.ToString();
        }

        public override ReturnConcentre Serializar(string retorno)
        {
            var serializer = new Return.ReturnConcentreNet();
            return serializer.ReturnConcentre(retorno);
        }

        public void ChangeHeader(bool persistirPesquisa)
        {
            _persistirPesquisa = persistirPesquisa;
        }
    }
}
