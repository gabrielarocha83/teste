using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaLCAdicional : RepositoryBase<PropostaLCAdicional>, IRepositoryPropostaLCAdicional
    {
        public RepositoryPropostaLCAdicional(DbContext context) : base(context)
        {
        }

        public int GetMaxNumeroInterno()
        {
            try
            {
                if (!_context.Set<PropostaLCAdicional>().Any())
                    return 1;

                return (_context.Set<PropostaLCAdicional>().Max(p => p.NumeroInternoProposta)) + 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PropostaLCAdicional> GetLatest(Expression<Func<PropostaLCAdicional, bool>> expression)
        {
            try
            {
                return await _context.Set<PropostaLCAdicional>().Where(expression).OrderByDescending(p => p.DataCriacao).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BuscaPropostaLCAdicional>> BuscaPendenciasLCAdicional(Guid usuarioID, string EmpresaID, bool Acompanhar)
        {
            try
            {
                IEnumerable<BuscaPropostaLCAdicional> list = await _context.Database.SqlQuery<BuscaPropostaLCAdicional>("EXEC spBuscaCockpitPropostaLCAdicional @Usuario, @EmpresaID, @Acompanhar",
                    new SqlParameter("Usuario", usuarioID),
                    new SqlParameter("EmpresaID", EmpresaID),
                    new SqlParameter("Acompanhar", Acompanhar)
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
