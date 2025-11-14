using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public class ConceitoCobrancaClientes:IConceitoCobrancaClientes
    {

        public async Task ProcessarConceitoCobranca(string codigo, Guid carteira)
        {
            try
            {
                var stringConexao = Config.ConnectionStringYara;
                using (var conexao =
                    new SqlConnection(stringConexao))
                {
                    var commandoSQL = "spFluxoAnaliseLiberacaoConceitoCobranca @Numero, @Carteira";
                    await conexao.QueryAsync(commandoSQL, new { Numero = codigo, Carteira = carteira });

                }
            }
            catch (Exception e)
            {
                var logs = new Logs();
                logs.LogError(e);
            }
        }
    }
}