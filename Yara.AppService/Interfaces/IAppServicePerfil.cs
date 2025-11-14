using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePerfil : IAppServiceBase<PerfilDto>
    {
        Task<Guid> GetPerfilID(string descricao);
    }
}