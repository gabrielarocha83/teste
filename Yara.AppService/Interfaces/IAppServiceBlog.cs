using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceBlog : IAppServiceBase<BlogDto>
    {
        Task<bool> InsertAsync(BlogDto obj, string URL);
        Task<IEnumerable<BlogDto>> GetByArea(Guid Area, string EmpresaID, string URL);
    }
}
