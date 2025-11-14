using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceAnexoArquivo : IAppServiceBase<AnexoArquivoDto>
    {
        Task<bool> InsertAsync(AnexoArquivoDto obj);
        Task<bool> UpdateList(List<AnexoArquivoDto> obj, Guid userId);
        Task<byte[]> GetAccountZip(Guid contaClienteId, string empresa);
        //Task<bool> Inactive(Guid id);
        Task<IEnumerable<AnexoArquivoDto>> CustomGetAllFilterAsync(Expression<Func<AnexoArquivoDto, bool>> expression);
    }
}
