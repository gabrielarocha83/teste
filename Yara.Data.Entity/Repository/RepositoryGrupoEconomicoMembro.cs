using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryGrupoEconomicoMembro : RepositoryBase<GrupoEconomicoMembros>, IRepositoryGrupoEconomicoMembro
    {

        public RepositoryGrupoEconomicoMembro(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BuscaGrupoEconomicoMembros>> BuscaContaCliente(Guid grupoId, string EmpresaID)
        {
            try
            {
                IEnumerable<BuscaGrupoEconomicoMembros> list = await _context.Database.SqlQuery<BuscaGrupoEconomicoMembros>("EXEC spBuscaGrupoEconomicoMembros @pGrupoId, @EmpresaID",
                    new SqlParameter("pGrupoId", string.IsNullOrEmpty(grupoId.ToString()) ? DBNull.Value : (object)grupoId),
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
