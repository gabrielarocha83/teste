using System;
using System.Threading.Tasks;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public interface IConceitoCobrancaClientes
    {
        Task ProcessarConceitoCobranca(string codigo, Guid carteira);
    }
}