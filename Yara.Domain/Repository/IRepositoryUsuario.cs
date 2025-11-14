using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryUsuario : IRepositoryBase<Usuario>
    {
        Task<IEnumerable<Usuario>> ListaUsuariosPorGrupo(string grupoId);
        Task<IEnumerable<Usuario>> ListaAllUserNotTracking();
        Task<IEnumerable<Usuario>> ListaUsuariosPorEstruturaPapel(string papel, string ordemVendaNumero);
        Task<IEnumerable<Usuario>> GetSimpleUserList();
        Task<IEnumerable<Usuario>> GetListUsers(bool? Ativo, string EmpresaID, int? TipoAcesso, Guid? GrupoID, string Usuario, string Login);
    }
}
