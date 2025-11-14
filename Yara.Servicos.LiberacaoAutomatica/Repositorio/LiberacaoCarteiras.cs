using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Yara.Servicos.LiberacaoAutomatica.YaraSAP;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public class LiberacaoCarteiras:ILiberacaoCarteiras
    {

        public async Task<IEnumerable<SoapAtualizaBloqueioORDEM_VENDA>> ObterDivisoesLiberadasSAP()
        {
            var retorno = new List<SoapAtualizaBloqueioORDEM_VENDA>();
            try
            {
                var stringConexao = Config.ConnectionStringYara;
                using (var conexao =
                    new SqlConnection(stringConexao))
                {
                    var commandoSQL = new StringBuilder(string.Empty);

                    commandoSQL.Append("SELECT L.divisao          AS NUM_DIVISAO_REMESSA,");
                    commandoSQL.Append("L.item             AS NUM_ITEM, ");
                    commandoSQL.Append("L.numero           AS NUM_ORDEM_VENDA, ");
                    commandoSQL.Append("D.datacarregamento AS DT_DIVISAO_REMESSA, ");
                    commandoSQL.Append("D.bloqueio         AS BLOQUEIO, ");
                    commandoSQL.Append("D.qtdpedida        AS QTDE_BLOQUEIO, ");
                    commandoSQL.Append("D.unidademedida    AS UNIDADE ");
                    commandoSQL.Append("FROM   [Yara].[dbo].[liberacaocarteira] L ");
                    commandoSQL.Append("INNER JOIN divisaoremessa D ");
                    commandoSQL.Append("ON D.divisao = L.divisao ");
                    commandoSQL.Append("AND D.itemordemvendaitem = L.item ");
                    commandoSQL.Append("AND D.ordemvendanumero = L.numero ");
                    commandoSQL.Append("WHERE  L.enviadosap = 0 ");

                     var resposta = await conexao.QueryAsync<SoapAtualizaBloqueioORDEM_VENDA>(commandoSQL.ToString());

                    retorno = resposta.ToList();

                    return retorno.ToList();

                }
            }
            catch (Exception e)
            {
                var logs = new Logs();
                logs.LogError(e);
               
            }
            return retorno;
        }

    }
}