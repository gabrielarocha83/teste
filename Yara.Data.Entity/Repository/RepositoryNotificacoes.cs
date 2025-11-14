using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;
namespace Yara.Data.Entity.Repository
{
    public class RepositoryNotificacoes : IRepositoryNotificacoes
    {

        private readonly DbContext _context;

        public RepositoryNotificacoes(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotificacaoUsuario>> BuscaUsuariosCockpit(string EmpresaID)
        {
            try
            {
                IEnumerable<NotificacaoUsuario> list = await _context.Database.SqlQuery<NotificacaoUsuario>("EXEC spUsuariosNotificacaoCockpit @EmpresaID",
                    new SqlParameter("EmpresaID", EmpresaID)
                ).ToListAsync();

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
