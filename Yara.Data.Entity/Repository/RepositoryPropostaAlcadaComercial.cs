using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaAlcadaComercial : RepositoryBase<PropostaAlcadaComercial>, IRepositoryPropostaAlcadaComercial
    {

        public RepositoryPropostaAlcadaComercial(DbContext context) : base(context)
        {
        }

        public int GetMaxNumeroInterno()
        {
            if (!_context.Set<PropostaAlcadaComercial>().Any()) return 1;
            var maxNumero = _context.Set<PropostaAlcadaComercial>().Max(p => p.NumeroInternoProposta);
            return maxNumero + 1;
        }

        public async Task<PropostaAlcadaComercial> GetMaxProposal()
        {
            if (!_context.Set<PropostaAlcadaComercial>().Any()) return null;
            var maxNumero = _context.Set<PropostaAlcadaComercial>().Max(p => p.NumeroInternoProposta);
            return await _context.Set<PropostaAlcadaComercial>()
                .FirstOrDefaultAsync(c => c.NumeroInternoProposta.Equals(maxNumero));
        }

        public async Task<IEnumerable<PropostaAlcadaComercialRestricoes>> BuscaRestricaoAlcada(Guid contaClienteId, string empresaId)
        {
            try
            {
                IEnumerable<PropostaAlcadaComercialRestricoes> list = await _context.Database.SqlQuery<PropostaAlcadaComercialRestricoes>("EXEC spBuscaRestricaoPropostaAlcadaComercial @pContaClienteID, @pEmpresaID",
                    new SqlParameter("@pContaClienteID", contaClienteId),
                    new SqlParameter("@pEmpresaID", empresaId)
                ).ToListAsync();

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BuscaCockpitPropostaAlcada>> BuscaPropostaCockpit(Guid usuarioId, string empresaId, bool acompanhar)
        {
            try
            {
                IEnumerable<BuscaCockpitPropostaAlcada> list = await _context.Database.SqlQuery<BuscaCockpitPropostaAlcada>("EXEC spBuscaCockpitPropostaAlcadaComercial @Usuario, @EmpresaID, @Acompanhar",
                    new SqlParameter("Usuario", usuarioId),
                    new SqlParameter("EmpresaID", empresaId),
                    new SqlParameter("Acompanhar", acompanhar)
                ).ToListAsync();

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PropostaAlcadaComercial> GetLatest(Expression<Func<PropostaAlcadaComercial, bool>> expression)
        {
            try
            {
                return await _context.Set<PropostaAlcadaComercial>().Where(expression).OrderByDescending(p => p.DataCriacao).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
