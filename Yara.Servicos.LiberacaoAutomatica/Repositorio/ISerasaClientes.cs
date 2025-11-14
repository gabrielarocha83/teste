using System;
using System.Threading.Tasks;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public interface ISerasaClientes
    {
        Task ProcessarSerasa(string codigo, Guid carteira);
    }
}