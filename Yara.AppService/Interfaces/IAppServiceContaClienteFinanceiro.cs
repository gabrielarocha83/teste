using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.Domain.Entities;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceContaClienteFinanceiro : IAppServiceBase<ContaClienteFinanceiroDto>
    {
        Task<bool> UpdateConceitoCobranca(ConceitoCobrancaLiberacaoLogDto obj);
        Task<ConceitoManualContaClienteDto> UpdateConceitoCobranca(ConceitoManualContaClienteDto conceitoManualContaClienteDto);
        Task<LimiteCreditoClienteDto> GetLimiteCreditoContaCliente(string codigoprincipal);
        Task<ContaClienteFinanceiroDto> GetCodSAPFinanceiro(string codigoSAP, string Empresa);
        Task<DadosFinanceiroTituloDto> GetDadosFinanceiroSomatoriaTitulos(Guid contaClienteId, string empresaId);
        Task<ContaClienteResumoCobrancaDto> GetResumoCobranca(Guid contaClienteId, string empresaId);
        Task<LimiteYaraGalvaniDto> UpdateLimiteCredito(LimiteYaraGalvaniDto limiteYaraGalvaniDto);
        Task<ContaClienteFinanceiroDto> GetRawAsync(Expression<Func<ContaClienteFinanceiroDto, bool>> expression);
        Task<bool> UpdateDividaAtiva(ContaClienteFinanceiroDto obj);
    }
}