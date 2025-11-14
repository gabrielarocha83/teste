using System;
using System.Threading.Tasks;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public interface IClientesBloqueados
    {
        Task ProcessarClienteBloqueado(string codigo, Guid carteira);
    }
}