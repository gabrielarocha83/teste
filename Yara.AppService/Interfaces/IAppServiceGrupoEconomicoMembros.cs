using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceGrupoEconomicoMembros : IAppServiceBase<GrupoEconomicoMembrosDto>
    {
        Task<bool> Inactive(Guid id);
        Task<bool> InsertAsync(GrupoEconomicoMembrosDto obj);
        Task<BuscaGrupoEconomicoDetalheDto> BuscaContaCliente(Guid grupoId, string EmpresaID);
        Task<KeyValuePair<Guid?, bool>> InsertAsyncList(List<GrupoEconomicoMembrosDto> obj, Guid usuarioId, string EmpresaID, string URL);
        Task<KeyValuePair<Guid?, bool>> InactiveAsyncList(List<GrupoEconomicoMembrosDto> obj, Guid usuarioId, string EmpresaID, string URL);
    }
}
