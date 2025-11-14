using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryContaCliente : IRepositoryBase<ContaCliente>
    {
        BuscaOrdemVendaSumarizado OrdemVendaSumarizado(Guid ContaClienteID, string Empresa);
        Task<ContaCliente> GetOneByIDAsync(Guid id);
       
        Task<IEnumerable<BuscaOrdemVendasPrazo>> OrdemVendaPorClientePrazo(BuscaOrdemVendasPrazo vendasPrazo);
        Task<IEnumerable<BuscaOrdemVendasAVista>> OrdemVendaPorClienteVista(BuscaOrdemVendasAVista vendasAVista);
        Task<IEnumerable<BuscaOrdemVendasPagaRetira>> OrdemVendaPorClienteRetira(BuscaOrdemVendasPagaRetira vendasPagaRetira);
        Task<IEnumerable<BuscaContaCliente>> BuscaContaCliente(BuscaContaCliente contaCliente, Guid usuarioId);
        Task<IEnumerable<BuscaContaClienteEstComl>> BuscaContaClienteEstComl(BuscaContaClienteEstComl contaCliente);
        Task<IEnumerable<TitulosGrupoEconomicoMembros>> TitulosGrupoEconomicoMembroContaCliente(Guid contaClienteId, string empresa);
        Task<bool> ValidaAcessoContaCliente(Guid contaClienteId, Guid usuarioId);
    }
    
}