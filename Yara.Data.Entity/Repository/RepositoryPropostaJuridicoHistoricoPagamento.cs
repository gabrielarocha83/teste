using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaJuridicoHistoricoPagamento : RepositoryBase<PropostaJuridicoHistoricoPagamento>, IRepositoryPropostaJuridicoHistoricoPagamento
    {

        public RepositoryPropostaJuridicoHistoricoPagamento(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PropostaJuridicoHistoricoPagamento>> BuscaHistoricoPagamento(Guid propostaJuridicoId, Guid contaClienteId)
        {
            try
            {
                IEnumerable<PropostaJuridicoHistoricoPagamento> list = await _context.Database.SqlQuery<PropostaJuridicoHistoricoPagamento>("EXEC spBuscaPropostaJuridicoHistoricoPagamento @propostaJuridicoId, @contaClienteId",
                    new SqlParameter("@propostaJuridicoId", propostaJuridicoId),
                    new SqlParameter("@contaClienteId", contaClienteId)
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
