using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    internal class RepositoryUsuario : RepositoryBase<Usuario>, IRepositoryUsuario
    {
        public RepositoryUsuario(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Usuario>> ListaUsuariosPorGrupo(string grupoId)
        {
            IEnumerable<Usuario> list = await _context.Database.SqlQuery<Usuario>("EXEC spBuscaUsuarioPorGrupo @pGrupo",
                    new SqlParameter("pGrupo", string.IsNullOrEmpty(grupoId) ? DBNull.Value : (object)grupoId)
            ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<Usuario>> ListaAllUserNotTracking()
        {
            return await _context.Set<Usuario>().ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> ListaUsuariosPorEstruturaPapel(string papel, string ordemVendaNumero)
        {
            IEnumerable<Usuario> list = await _context.Database.SqlQuery<Usuario>("EXEC spBuscaUsuarioPorEstrutura @pPapel, @pNumero",
                new SqlParameter("pPapel", string.IsNullOrEmpty(papel) ? DBNull.Value : (object)papel),
                new SqlParameter("pNumero", string.IsNullOrEmpty(papel) ? DBNull.Value : (object)ordemVendaNumero)
            ).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<Usuario>> GetSimpleUserList()
        {

            List<Usuario> ret = new List<Usuario>();

            var list = await _context.Set<Usuario>().Where(u => u.Ativo == true).Select(u => new { ID = u.ID, Nome = u.Nome, Login = u.Login }).ToListAsync();
            list.ForEach(l => ret.Add(new Usuario() { ID = l.ID, Nome = l.Nome, Login = l.Login }));

            return ret;

        }

        public async Task<IEnumerable<Usuario>> GetListUsers(bool? Ativo, string EmpresaID, int? TipoAcesso, Guid? GrupoID, string Usuario, string Login)
        {
            IEnumerable<Usuario> list = await _context.Database.SqlQuery<Usuario>("EXEC Spbuscausuarios @Ativo, @Grupo,  @EmpresaID, @TipoAcesso, @Usuario, @Login",
                new SqlParameter("Ativo", Ativo.HasValue ? (object)Ativo: DBNull.Value),
                new SqlParameter("Grupo", GrupoID.HasValue?(object)GrupoID:DBNull.Value),
                new SqlParameter("EmpresaID", string.IsNullOrEmpty(EmpresaID)?DBNull.Value:(object) EmpresaID),
                new SqlParameter("TipoAcesso",TipoAcesso.HasValue ? (object)TipoAcesso : DBNull.Value),
                new SqlParameter("Usuario", string.IsNullOrEmpty(Usuario) ? DBNull.Value : (object)Usuario),
                new SqlParameter("Login", string.IsNullOrEmpty(Login) ? DBNull.Value : (object)Login)
            ).ToListAsync();

            return list;
        }
    }
}
