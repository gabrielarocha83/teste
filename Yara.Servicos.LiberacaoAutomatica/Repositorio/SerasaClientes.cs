using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public class SerasaClientes:ISerasaClientes
    {
        public async Task ProcessarSerasa(string codigo, Guid carteira)
        {
            try
            {
                var stringConexao = Config.ConnectionStringYara;
                using (var conexao =
                    new SqlConnection(stringConexao))
                {
                    var commandoSQL = "spFluxoAnaliseLiberacaoClienteRestricaoSerasa @Numero, @Carteira";
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