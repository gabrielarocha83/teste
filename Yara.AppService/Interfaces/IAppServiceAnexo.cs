using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceAnexo : IAppServiceBase<AnexoDto>
    {
        Task<bool> InsertAsync(AnexoDto obj);
        Task<bool> Inactive(Guid id);

        Task<IEnumerable<AnexoDto>> GetAllFilterAsyncEspecifico(AnexoArquivoByPropostaContaClienteDto anexoArquivoByPropostaContaCliente);
    }
}
