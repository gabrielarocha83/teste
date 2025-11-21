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
    public class RepositoryTituloComentario : RepositoryBase<TituloComentario>, IRepositoryTituloComentario
    {
        public RepositoryTituloComentario(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<BuscaTituloComentario>> GetAllComments(string numeroDocumento, string linha, string anoExercicio, string empresa)
        {
            try
            {
                IEnumerable<BuscaTituloComentario> list = await _context.Database.SqlQuery<BuscaTituloComentario>("EXEC spBuscaTitulosComentarios @NumeroDocumento, @Linha, @AnoExercicio, @Empresa",
                    new SqlParameter("@NumeroDocumento", numeroDocumento),
                    new SqlParameter("@Linha", linha),
                    new SqlParameter("@AnoExercicio", anoExercicio),
                    new SqlParameter("@Empresa", empresa)
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
