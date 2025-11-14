using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceGrupoPermissao : IAppServiceBase<GrupoPermissaoDto>
    {
        Task<bool> Inactive(Guid id);
    }
}