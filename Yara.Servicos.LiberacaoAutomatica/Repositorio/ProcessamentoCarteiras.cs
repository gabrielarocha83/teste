using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Yara.Servicos.LiberacaoAutomatica.Entidades;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public class ProcessamentoCarteiras : IProcessamentoCarteiras
    {

        /// <summary>
        /// Método que retorna todos os processamento por Status
        /// </summary>
        /// <param name="status">1=Aguardando, 2=EmProcessamento, 3=Concluido</param>
        /// <returns></returns>
        public IEnumerable<ProcessamentoCarteira> ListaCarteiras(ProcessamentoCarteiraStatus status)
        {
            var result =  new List<ProcessamentoCarteira>();
            try
            {
                var stringConexao = Config.ConnectionStringYara;
                using (var conexao =
                    new SqlConnection(stringConexao))
                {
                    var commandoSQL = "SELECT * FROM ProcessamentoCarteira WHERE Status=@Status ORDER BY Cliente ASC";
                     result = conexao.Query<ProcessamentoCarteira>(commandoSQL, new {Status = status}).ToList();
                    
                }
            }
            catch (Exception e)
            {
                var logs = new Logs();
                logs.LogError(e);
            }
           
                return result;
            

        }

        public void AtualizarStatus(Guid id, ProcessamentoCarteiraStatus Status)
        {
            try
            {
                var stringConexao = Config.ConnectionStringYara;
                using (var conexao =
                    new SqlConnection(stringConexao))
                {
                    var commandoSQL = $"UPDATE ProcessamentoCarteira SET Status={(int)Status} WHERE ID=@ID";
                     conexao.Query(commandoSQL, new { ID = id });
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