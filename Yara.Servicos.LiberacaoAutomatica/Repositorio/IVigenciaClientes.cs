using System;
using System.Threading.Tasks;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public interface IVigenciaClientes
    {
        Task<int> ProcessarVigencias(string codigo, Guid carteira);
    }
}