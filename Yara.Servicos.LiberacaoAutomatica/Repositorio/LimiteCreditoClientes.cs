using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Yara.Servicos.LiberacaoAutomatica.Entidades;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public class LimiteCreditoClientes:ILimiteCreditoCliente
    {
        public async Task<LimiteCreditoCliente> ObterLimiteCreditoCliente(string codigo)
        {
            var result = new LimiteCreditoCliente();
            try
            {
                var stringConexao = Config.ConnectionStringYara;
                using (var conexao =
                    new SqlConnection(stringConexao))
                {
                    var commandoSQL = "spBuscaLimiteLC @Numero";
                    var resposta =  await conexao.QueryAsync<LimiteCreditoCliente>(commandoSQL, new { Numero= codigo });
                    result = resposta.First();

                }
            }
            catch (Exception e)
            {
                var logs = new Logs();
                logs.LogError(e);
              
            }
            return result;
        }

        public async Task ProcessarLimiteCreditoCliente(string codigo, Guid carteira)
        {
            try
            {
                var stringConexao = Config.ConnectionStringYara;
                using (var conexao =
                    new SqlConnection(stringConexao))
                {
                    var commandoSQL = "Spfluxolcdisponivel @Numero, @Carteira";
                   var retorno =  await conexao.QueryAsync(commandoSQL, new { Numero = codigo, Carteira=carteira }).ConfigureAwait(false);
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