using System;
using System.Threading.Tasks;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public interface ILiberacoesManuais
    {
        Task ProcessarLiberacaoManual(string codigo, Guid carteira);
    }
}