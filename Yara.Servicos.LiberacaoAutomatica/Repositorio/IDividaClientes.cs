using System;
using System.Threading.Tasks;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public interface IDividaClientes
    {
        Task ProcessarDividas(string codigo, Guid carteira);
    }
}