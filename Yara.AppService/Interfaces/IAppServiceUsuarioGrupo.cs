using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceUsuarioGrupo:IAppServiceBase<UsuarioGrupoDto>
    {
        Task<bool> Inactive(Guid id);
    }
}
