using System.Text;
using Yara.Service.Serasa.Crednet.Entities;
using Yara.Service.Serasa.Interface;

namespace Yara.Service.Serasa.Crednet.Send
{
    public class HeaderSerasaCrednet : SerasaService<ReturnCrednet>
    {
        private readonly string _codDocumento;
        private string _usuarioSerasa;
        private string _senhaSerasa;
    
        public HeaderSerasaCrednet(string codDocumento, string usuarioSerasa, string senhaSerasa)
        {
            _codDocumento = codDocumento;
            _usuarioSerasa = usuarioSerasa;
            _senhaSerasa = senhaSerasa;
        }
        
        public override string Header()
        {
            var headerSendCrednet = new StringBuilder();

            #region Dados de Acesso

            headerSendCrednet.Append(_usuarioSerasa);                             //Seq.01 - logon
            headerSendCrednet.Append(_senhaSerasa);                             //Seq.02 - senha
            headerSendCrednet.Append(' ', 8);                                 //Seq.03 - novasenha

            #endregion

            #region Cabeçalho Crednet B49C

            var strDocumento = _codDocumento.Trim();

            headerSendCrednet.Append("B49C");                                 //Seq.01 - FILLER
            headerSendCrednet.Append(' ', 6);                                 //Seq.02 - FILLER
            headerSendCrednet.Append(_codDocumento.Trim().PadLeft(15, '0'));  //Seq.03 - NUM DOC
            headerSendCrednet.Append(strDocumento.Length == 11 ? "F" : "J");  //Seq.04 - TIPO PESSOA
            headerSendCrednet.Append("C".PadRight(6, ' '));                   //Seq.05 - BASE CONS
            headerSendCrednet.Append("FI");                                   //Seq.06 - MODALIDADE
            headerSendCrednet.Append(' ', 7);                                 //Seq.07 - VLR CONSUL
            headerSendCrednet.Append(' ', 12);                                //Seq.08 - CENTRO CUST
            headerSendCrednet.Append("N");                                    //Seq.09 - CODIFICADO
            headerSendCrednet.Append("99");                                   //Seq.10 - QTD REG
            headerSendCrednet.Append("S");                                    //Seq.11 - CONVERSA
            headerSendCrednet.Append("INI");                                  //Seq.12 - FUNÇÃO
            headerSendCrednet.Append("A");                                    //Seq.13 - TP CONSULTA
            headerSendCrednet.Append("N");                                    //Seq.14 - ATUALIZA
            headerSendCrednet.Append(' ', 18);                                //Seq.15 - IDENT_TERM
            headerSendCrednet.Append(' ', 10);                                //Seq.16 - RESCLI
            headerSendCrednet.Append(' ', 1);                                 //Seq.17 - DELTS
            headerSendCrednet.Append(' ', 1);                                 //Seq.18 - COBRA
            headerSendCrednet.Append("D");                                    //Seq.19 - PASSA
            headerSendCrednet.Append(' ', 1);                                 //Seq.20 - CONS.COLLEC
            headerSendCrednet.Append(' ', 1);                                 //Seq.21 - LOCALIZADOR
            headerSendCrednet.Append(' ', 9);                                 //Seq.22 - DOC.CREDOR
            headerSendCrednet.Append(' ', 2);                                 //Seq.23 - QTDE.CHEQUE
            headerSendCrednet.Append("N");                                    //Seq.24 - END + TEL
            headerSendCrednet.Append(' ', 2);                                 //Seq.25 - QTD–CHO1
            headerSendCrednet.Append(' ', 1);                                 //Seq.26 - SCO–CHO1
            headerSendCrednet.Append(' ', 1);                                 //Seq.27 - TAR–CHO1
            headerSendCrednet.Append(' ', 1);                                 //Seq.28 - NAO–COBR–BUREAU
            headerSendCrednet.Append(' ', 1);                                 //Seq.29 - AUTO–POSIT
            headerSendCrednet.Append(' ', 1);                                 //Seq.30 - BUREAU-VIA-SITE-TRANSACIONAL
            headerSendCrednet.Append(' ', 1);                                 //Seq.31 - QUER-TEL-9-DIG-X
            headerSendCrednet.Append(' ', 10);                                //Seq.32 - CTA CORRENT
            headerSendCrednet.Append(' ', 1);                                 //Seq.33 - DG.CTA CORR
            headerSendCrednet.Append(' ', 4);                                 //Seq.34 - AGENCIA
            headerSendCrednet.Append(' ', 1);                                 //Seq.35 - ALERTA
            headerSendCrednet.Append(' ', 8);                                 //Seq.36 - LOGON
            headerSendCrednet.Append(' ', 1);                                 //Seq.37 - VIA–INTERNET
            headerSendCrednet.Append(' ', 1);                                 //Seq.38 - RESPOSTA
            headerSendCrednet.Append(' ', 1);                                 //Seq.39 - PERIODO COMPRO
            headerSendCrednet.Append(' ', 1);                                 //Seq.40 - PERIODO ENDEREÇO
            headerSendCrednet.Append(' ', 1);                                 //Seq.41 - BACKTEST
            headerSendCrednet.Append(' ', 1);                                 //Seq.42 - DT.QUALITY
            headerSendCrednet.Append(' ', 2);                                 //Seq.43 - PRDORIGEM
            headerSendCrednet.Append(' ', 4);                                 //Seq.44 - TRNORIGEM
            headerSendCrednet.Append("06217362000180".PadRight(15, ' '));     //Seq.45 - CONSULTANTE
            headerSendCrednet.Append(' ', 1);                                 //Seq.46 - TP–OR
            headerSendCrednet.Append(' ', 9);                                 //Seq.47 - CNPJ–SOFTW
            headerSendCrednet.Append(' ', 15);                                //Seq.48 - FILLER
            headerSendCrednet.Append(' ', 2);                                 //Seq.49 - QTD COMPR
            headerSendCrednet.Append(' ', 1);                                 //Seq.50 - NEGATIVOS
            headerSendCrednet.Append(' ', 1);                                 //Seq.51 - CHEQUE
            headerSendCrednet.Append(' ', 8);                                 //Seq.52 - DATA CONSUL
            headerSendCrednet.Append(' ', 6);                                 //Seq.53 - HORA CONSUL
            headerSendCrednet.Append(' ', 4);                                 //Seq.54 - TOTAL REG
            headerSendCrednet.Append(' ', 4);                                 //Seq.55 - QTD REG1
            headerSendCrednet.Append(' ', 4);                                 //Seq.56 - COD TAB
            headerSendCrednet.Append(' ', 4);                                 //Seq.57 - ITEMTSDADOS
            headerSendCrednet.Append(' ', 16);                                //Seq.58 - TS DADOS
            headerSendCrednet.Append(' ', 16);                                //Seq.59 - TS SCORE1
            headerSendCrednet.Append(' ', 16);                                //Seq.60 - TS BP49
            headerSendCrednet.Append(' ', 16);                                //Seq.61 - TS AUTOR
            headerSendCrednet.Append(' ', 4);                                 //Seq.62 - ITEMTS AUTOR
            headerSendCrednet.Append(' ', 4);                                 //Seq.63 - ITEMTS SCOR1
            headerSendCrednet.Append(' ', 4);                                 //Seq.64 - ITEMTS BP49
            headerSendCrednet.Append(' ', 4);                                 //Seq.65 - ITEMTS DADOS2
            headerSendCrednet.Append(' ', 16);                                //Seq.66 - TS DADOS2
            headerSendCrednet.Append(' ', 1);                                 //Seq.67 - FASE
            headerSendCrednet.Append(' ', 1);                                 //Seq.68 - FASE
            headerSendCrednet.Append(' ', 30);                                //Seq.69 - DBTABELA
            headerSendCrednet.Append(' ', 1);                                 //Seq.70 - COD AUT
            headerSendCrednet.Append(' ', 3);                                 //Seq.71 - OPERID
            headerSendCrednet.Append(' ', 1);                                 //Seq.72 - RECI–COMPR
            headerSendCrednet.Append(' ', 1);                                 //Seq.73 - RECI–PAGTO
            headerSendCrednet.Append(' ', 38);                                //Seq.74 - FILLER
            headerSendCrednet.Append(' ', 1);                                 //Seq.75 - ACESS RECHQ
            headerSendCrednet.Append(' ', 1);                                 //Seq.76 - TEM OCOR RECHQ
            headerSendCrednet.Append(' ', 1);                                 //Seq.77 - RESERVADO

            #endregion

            #region Complemento Cabeçalho Crednet P002

            headerSendCrednet.Append("P002");                                 //Seq.01 - TIPO–REG
            headerSendCrednet.Append("RE02");                                 //Seq.02 - COD1
            headerSendCrednet.Append(' ', 21);                                //Seq.03 - CHAVE1
            headerSendCrednet.Append(' ', 4);                                 //Seq.04 - COD2
            headerSendCrednet.Append(' ', 21);                                //Seq.05 - CHAVE2
            headerSendCrednet.Append(' ', 4);                                 //Seq.06 - COD3
            headerSendCrednet.Append(' ', 21);                                //Seq.07 - CHAVE3
            headerSendCrednet.Append(' ', 4);                                 //Seq.08 - COD4
            headerSendCrednet.Append(' ', 21);                                //Seq.09 - CHAVE4
            headerSendCrednet.Append(' ', 11);                                //Seq.11 - FILLER

            #endregion

            #region Complemento Cabeçalho Crednet N001

            headerSendCrednet.Append("N001");                                 //Seq.01 - TPREG
            headerSendCrednet.Append("00");                                   //Seq.01 - SUBTP
            headerSendCrednet.Append("PP");                                   //Seq.03 - TP CONS
            headerSendCrednet.Append("X21P");                                 //Seq.04 - TRANS CONS
            headerSendCrednet.Append(' ', 1);                                 //Seq.05 - SOL GDE VAR
            headerSendCrednet.Append("0");                                    //Seq.06 - ID CHEQUE
            headerSendCrednet.Append(' ', 1);                                 //Seq.07 - AGRUPA
            headerSendCrednet.Append(' ', 1);                                 //Seq.08 - CONS SINT
            headerSendCrednet.Append(' ', 1);                                 //Seq.09 - Reservado
            headerSendCrednet.Append(' ', 1);                                 //Seq.10 - ANOT RESUM
            headerSendCrednet.Append(' ', 6);                                 //Seq.11 - CHAVE CONS
            headerSendCrednet.Append(' ', 12);                                //Seq.12 - FANTASIA
            headerSendCrednet.Append(' ', 1);                                 //Seq.13 - STATUS BCO
            headerSendCrednet.Append(' ', 13);                                //Seq.14 - FILLER
            headerSendCrednet.Append(' ', 1);                                 //Seq.15 - Trata-Tel
            headerSendCrednet.Append(' ', 1);                                 //Seq.16 - Meio
            headerSendCrednet.Append(' ', 63);                                //Seq.17 - FILLER

            #endregion

            #region Complemento Cabeçalho Crednet N003

            headerSendCrednet.Append("N003");                                 //Seq.01 - TPREG
            headerSendCrednet.Append("00");                                   //Seq.02 - SUBTP
            headerSendCrednet.Append(' ', 4);                                 //Seq.03 - DDD
            headerSendCrednet.Append(' ', 8);                                 //Seq.04 - TELEFONE
            headerSendCrednet.Append(' ', 9);                                 //Seq.05 - CEP
            headerSendCrednet.Append("SP");                                   //Seq.06 - UF
            //headerSendCrednet.Append("RXPSNRC5".PadRight(80, ' '));           //Seq.07 - FEAT/SCOR (ocorre 20 vezes)
            headerSendCrednet.Append(' ', 80);           //Seq.07 - FEAT/SCOR (ocorre 20 vezes)
            headerSendCrednet.Append(' ', 6);                                 //Seq.08 - FILLER

            #endregion

            headerSendCrednet.Append("T999"); //Seq

            return headerSendCrednet.ToString();
        }

        public override ReturnCrednet Serializar(string retorno)
        {
            var serializer = new Return.ReturnCredNet();
            return serializer.ReturnCrednet(retorno);
        }
    }
}
