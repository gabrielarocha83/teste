using System;
using System.Threading.Tasks;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public interface ILiberacoesPrazosEntregas
    {
        Task ProcessarLiberacaoPrazoEntrega(string codigo, Guid carteira);
    }
}