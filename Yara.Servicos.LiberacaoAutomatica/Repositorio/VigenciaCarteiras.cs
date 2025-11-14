using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public class VigenciaCarteiras:IVigenciaClientes
    {
        public async Task<int> ProcessarVigencias(string codigo, Guid carteira)
        {
            var retorno = 0;
            try
            {
                var stringConexao = Config.ConnectionStringYara;
                using (var conexao =
                    new SqlConnection(stringConexao))
                {
                    var commandoSQL = "spFluxoAnaliseLiberacaoVigenciaLimiteCredito @Numero, @Carteira";
                    var resposta = await conexao.QueryAsync<int>(commandoSQL, new { Numero = codigo, Carteira = carteira });
                    retorno = resposta.FirstOrDefault();
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