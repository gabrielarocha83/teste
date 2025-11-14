using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository.Procedures
{
    public interface IRepositoryCobrancaResultado : IRepositoryBase<CobrancaResultado>
    {

        Task<IEnumerable<CobrancaResultado>> CobrancaGeralPorDiretoria(string empresaId, string diretoriaId = null);
        Task<IEnumerable<CobrancaResultado>> CobrancaEfetivaPorDiretoria(string empresaId, string diretoriaId = null);
        Task<IEnumerable<CobrancaResultado>> CobrancaEfetivaPorStatus(string empresaId, string diretoriaId = null);
        Task<IEnumerable<CobrancaResultado>> CobrancaEfetivaPorEstado(string empresaId, string diretoriaId = null);
        Task<IEnumerable<CobrancaVencidosResultado>> CobrancaVencidosMenosDias(string empresaId, string diretoriaId = null);
        Task<IEnumerable<CobrancaResultado>> CobrancaMaioresDevedores(string empresaId, string diretoriaId = null);
        Task<IEnumerable<CobrancaResultado>> CobrancaEfetivaPorCultura(string empresaId, string diretoriaId = null);

        Task<IEnumerable<CobrancaListaCliente>> CobrancaGeralPorDiretoria_Clientes(string empresaId, string chave, int dias, string diretoriaId = null);
        Task<IEnumerable<CobrancaListaCliente>> CobrancaEfetivaPorDiretoria_Clientes(string empresaId, string chave, int dias, string diretoriaId = null);
        Task<IEnumerable<CobrancaListaCliente>> CobrancaEfetivaPorStatus_Clientes(string empresaId, string chave, int dias, string diretoriaId = null);
        Task<IEnumerable<CobrancaListaCliente>> CobrancaEfetivaPorEstado_Clientes(string empresaId, string chave, int dias, string diretoriaId = null);
        Task<IEnumerable<CobrancaListaCliente>> CobrancaEfetivaPorCultura_Clientes(string empresaId, string chave, int dias, string diretoriaId = null);
        Task<IEnumerable<CobrancaListaCliente>> Juridico_Clientes(string empresaId, int mes, int ano);
        Task<IEnumerable<TituloContaCliente>> BuscaTitulosContaCliente(Guid contaClienteId, string empresaId);

    }
}
