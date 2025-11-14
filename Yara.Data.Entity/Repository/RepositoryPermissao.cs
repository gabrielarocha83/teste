using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    internal class RepositoryPermissao : RepositoryBase<Permissao>, IRepositoryPermissao
    {

        public RepositoryPermissao(DbContext context) : base(context)
        {
        }

        public async Task<List<Permissao>> ListaPermissoes(Guid idUsuarioGuid)
        {
            var list = await _context.Database.SqlQuery<Permissao>("exec spBuscaPermissoes @IdUsuario", 
                new SqlParameter("IdUsuario", (object)idUsuarioGuid)).ToListAsync();
            return list;
        }
    }
}