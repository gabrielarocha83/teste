using System;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryContaClienteFinanceiro : IRepositoryBase<ContaClienteFinanceiro>
    {
        void UpdateConceito(ContaClienteFinanceiro obj);
        Task<LimiteCreditoCliente> GetLimiteCreditoContaCliente(string codigoprincipal);
        Task<DadosFinanceiroTitulo> GetDadosFinanceiroSomatoriaTitulos(Guid contaClienteId, string empresaId);
    }
}