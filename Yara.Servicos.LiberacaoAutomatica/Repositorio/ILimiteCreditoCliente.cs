using System;
using System.Threading.Tasks;
using Yara.Servicos.LiberacaoAutomatica.Entidades;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public interface ILimiteCreditoCliente
    {
        Task<LimiteCreditoCliente> ObterLimiteCreditoCliente(string codigo);

        Task ProcessarLimiteCreditoCliente(string codigo, Guid carteira);
    }
}