using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository.Procedures;

namespace Yara.Data.Entity.Repository.Procedures
{
    internal class RepositoryBuscaGrupoCookpit : RepositoryBase<BuscaGrupoEconomico>, IRepositoryBuscaGrupoCookpit
    {
        public RepositoryBuscaGrupoCookpit(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BuscaGrupoEconomico>> BuscaGrupoEconomico(Guid usuarioID, string EmpresaID)
        {
            IEnumerable<BuscaGrupoEconomico> list = await _context.Database.SqlQuery<BuscaGrupoEconomico>("EXEC spBuscaGruposCookpit @UsuarioID, @EmpresaID",
                new SqlParameter("UsuarioID", usuarioID),
                new SqlParameter("EmpresaID", EmpresaID)
            ).ToListAsync();
            return list;
        }
    }
}