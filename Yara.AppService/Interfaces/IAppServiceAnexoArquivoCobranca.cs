using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceAnexoArquivoCobranca : IAppServiceBase<AnexoArquivoCobrancaDto>
    {
        Task<bool> InsertAsync(AnexoArquivoCobrancaDto obj, string empresaId);
        Task<IEnumerable<AnexoArquivoCobrancaWithOutFileDto>> GetAllFilterWithOutFileAsync(Guid propostaid, int tipo);
    }
}
