using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePermissao:IAppServiceBase<PermissaoDto>
    {
        Task<List<PermissaoDto>> GetListPermissao(Guid idUsuarioGuid);
        Task<bool> Inactive(string nome);
    }
}