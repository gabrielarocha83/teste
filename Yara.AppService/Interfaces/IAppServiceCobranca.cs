using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceCobranca
    {
        Task<IEnumerable<CobrancaResultadoDto>> CobrancaGeralPorDiretoria(string empresaId, string diretoriaId = null);
        Task<IEnumerable<CobrancaResultadoDto>> CobrancaEfetivaPorDiretoria(string empresaId, string diretoriaId = null);
        Task<IEnumerable<CobrancaResultadoDto>> CobrancaEfetivaPorStatus(string empresaId, string diretoriaId = null);
        Task<IEnumerable<CobrancaResultadoDto>> CobrancaEfetivaPorEstado(string empresaId, string diretoriaId = null);
        Task<IEnumerable<CobrancaVencidosResultadoDto>> CobrancaVencidosMenosDias(string empresaId, string diretoriaId = null);
        Task<IEnumerable<CobrancaResultadoDto>> CobrancaMaioresDevedores(string empresaId, string diretoriaId = null);
        Task<IEnumerable<CobrancaResultadoDto>> CobrancaEfetivaPorCultura(string empresaId, string diretoriaId = null);
        Task<CobrancaListaClienteResultadoDto> CobrancaClientes(string empresaId, string tipo, string chave, int dias, string diretoriaId = null);
        Task<CobrancaListaClienteResultadoDto> JuridicoClientes(string empresaId, int mes, int ano);
        Task<CobrancaListaClienteResultadoDto> CobrancaClientesGrandTotal(string empresaId, string tipo, string chave, string diretoriaId = null);
        Task<CobrancaListaClienteResultadoDto> CobrancaClientesGrandTotalSum(string empresaId, string tipo);
        Task<byte[]> CobrancaClientesExcel(string empresaId, string tipo, string chave, int dias, string diretoriaId = null);
        Task<byte[]> CobrancaClientesExcelGrandTotal(string empresaId, string tipo, string chave, string diretoriaId = null);
        Task<byte[]> CobrancaClientesExcelGrandTotalSum(string empresaId, string tipo);
        Task<IEnumerable<TituloContaClienteDto>> BuscaTitulosContaCliente(Guid contaClienteId, string empresaId);
        Task<byte[]> BuscaTitulosContaClienteExcel(Guid contaClienteId, string empresaId, bool acessoCompleto);
        Task<CobrancaContaDto> ExistPropostas(Guid ID, string EmpresaID);
    }
}
