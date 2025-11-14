using System.Text;
using Yara.Service.Serasa.Relato.Entities;
using Yara.Service.Serasa.Interface;

namespace Yara.Service.Serasa.Relato.Send
{
    public class SerasaRelato: SerasaService<ReturnRelato>
    {
        private readonly string _documento;
        private string _usuarioSerasa;
        private string _senhaSerasa;

        public SerasaRelato(string documento, string usuarioSerasa, string senhaSerasa)
        {
            _documento = documento;
            _usuarioSerasa = usuarioSerasa;
            _senhaSerasa = senhaSerasa;
        }

        public override string Header()
        {
            var cabecalho = new StringBuilder(string.Empty);

            #region Dados de Acesso

            cabecalho.Append(_usuarioSerasa); //username
            cabecalho.Append(_senhaSerasa); //SENHA
            cabecalho.Append(' ', 8); // NOVA SENHA

            #endregion

            #region IP20

            cabecalho.Append("IP20"); // CODTRAN
            cabecalho.Append("RELA"); // CDOPÇÃO
            cabecalho.Append("S"); // COMPACTA
            cabecalho.Append("2"); //TIPOTS
            cabecalho.Append(' ', 8); // NOMETS
            cabecalho.Append("0" + _documento.Substring(0, 8)); // CNPJ
            cabecalho.Append("2"); // MOEDA
            cabecalho.Append("2"); //TPSTRING
            cabecalho.Append("N"); //INDCCUSTO
            cabecalho.Append(' ', 12); //CCUSTO
            cabecalho.Append("2"); //QDSOC = GERA QUADRO SOCIAL  E GERA PARTICIPAÇÕES
            cabecalho.Append("3"); //IDIOMA = PORTUGUÊS

            #endregion

            return cabecalho.ToString();
        }

        public override ReturnRelato Serializar(string retorno)
        {
            var serializar = new Return.ReturnRelato();
            return serializar.Serasa(retorno);
        }
    }
}