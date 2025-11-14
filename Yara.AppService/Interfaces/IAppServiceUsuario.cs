using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceUsuario : IAppServiceBase<UsuarioDto>
    {
        Task<bool> Inactive(Guid id);
        Task<bool> InsertAsync(UsuarioDto obj);
        Task<IEnumerable<UsuarioDto>> ListaUsuariosPorGrupo(string grupoId);
        Task<IEnumerable<UsuarioDto>> ListaUsuariosPorEstruturaPapel(string papel, string ordemVendaNumero);
        Task<bool> UpdatePreferences(UsuarioDto usuarioDto);
        Task<bool> UpdateStructs(UsuarioDto usuarioDto);
        Task<IEnumerable<UsuarioDto>> GetSimpleUserList();
        Task<IEnumerable<UsuarioDto>> GetListUsers(BuscaUsuariosDto filtros);
        Task<UsuarioDto> SetToken(Guid id);
    }
}
