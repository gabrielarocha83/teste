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
    public class RepositoryDivisaoRemessa : RepositoryBase<DivisaoRemessa>, IRepositoryDivisaoRemessa
    {

        public RepositoryDivisaoRemessa(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DivisaoRemessaCockPit>> GetAllPendencyByUser(Guid UserID, string empresaID)
        {
            IEnumerable<DivisaoRemessaCockPit> divisoes = await _context.Database.SqlQuery<DivisaoRemessaCockPit>("EXEC spBuscaOrdemVendaCockpit @pUser, @EmpresaID",
                new SqlParameter("pUser", UserID.ToString()),
                new SqlParameter("EmpresaID", empresaID.ToString())
            ).ToListAsync();

            return divisoes;
        }

        public async Task<IEnumerable<DivisaoRemessaCockPit>> GetAllPendencyByAccountClient(Guid ContaClienteID, string empresaID)
        {
            IEnumerable<DivisaoRemessaCockPit> divisoes = await _context.Database.SqlQuery<DivisaoRemessaCockPit>("EXEC spBuscaOrdemVendaProposta @ContaClienteID, @EmpresaID",
                new SqlParameter("ContaClienteID", ContaClienteID.ToString()),
                new SqlParameter("EmpresaID", empresaID.ToString())
            ).ToListAsync();

            return divisoes;
        }

        public async Task<DivisaoRemessa> GetDivisaoAsync(int Divisao, int Item, string Numero)
        {
            return await _context.Set<DivisaoRemessa>().Include(c => c.ItemOrdemVenda).FirstOrDefaultAsync(c => c.Divisao.Equals(Divisao) && c.ItemOrdemVendaItem.Equals(Item) && c.OrdemVendaNumero.Equals(Numero));
        }

        public void UpdateDivisao(Guid SolicitanteID)
        {
             _context.Database.ExecuteSqlCommand(@"EXEC spUpdateDivisoes @SolicitanteID",new SqlParameter("SolicitanteID", SolicitanteID.ToString()));
        }

        public async Task<IEnumerable<DivisaoRemessaLogFluxo>> GetAllLogByDivisao(int Divisao, int Item, string Numero, Guid SolicitanteID)
        {
            var divisoes = await _context.Database.SqlQuery<DivisaoRemessaLogFluxo>("EXEC spBuscaLogDivisoes @pDivisao, @pItemOrdem, @pNumeroOrdem, @pSolicitanteID",
                new SqlParameter("pDivisao", Divisao),
                new SqlParameter("pItemOrdem", Item),
                new SqlParameter("pSolicitanteID", SolicitanteID),
                new SqlParameter("pNumeroOrdem", Numero)
            ).ToListAsync();

            return divisoes;
        }
    }
}
