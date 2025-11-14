using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    internal class RepositoryGrupoEconomico : RepositoryBase<GrupoEconomico>, IRepositoryGrupoEconomico
    {

        public RepositoryGrupoEconomico(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BuscaGrupoEconomico>> BuscaGrupoEconomico(Guid codCliente, string empresaId)
        {
            try
            {
                IEnumerable<BuscaGrupoEconomico> list = await _context.Database.SqlQuery<BuscaGrupoEconomico>("EXEC spBuscaGrupoEconomico @pClienteId, @pEmpresaID",
                    new SqlParameter("pClienteId", string.IsNullOrEmpty(codCliente.ToString()) ? DBNull.Value : (object)codCliente),
                    new SqlParameter("pEmpresaID", empresaId)
                ).ToListAsync();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BuscaGrupoEconomico>> BuscaGrupoEconomicoPorGrupo(Guid codGrupo, string empresaId)
        {
            try
            {
                IEnumerable<BuscaGrupoEconomico> list = await _context.Database.SqlQuery<BuscaGrupoEconomico>("EXEC spBuscaGrupoEconomicoPorGrupo @pGrupoId, @pEmpresaID",
                    new SqlParameter("pGrupoId", string.IsNullOrEmpty(codGrupo.ToString()) ? DBNull.Value : (object)codGrupo),
                    new SqlParameter("pEmpresaID", empresaId)
                ).ToListAsync();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BuscaHistoricoGrupo>> BuscaHistoricoPorGrupo(Guid codGrupo, string empresaId)
        {

            // Parametro de Atraso a Considerar
            const int diasAtraso = 0;
            const int pesoAtraso = 0;
            //const int pesoPr = 0;
            //const int diasRePr = 0;
            //const int pesoRePr = 0;
            const int diasMaiorAtraso = 0;
            const int pesoMaiorAtraso = 0;

            List<BuscaHistoricoGrupo> retorno = new List<BuscaHistoricoGrupo>();

            var anoInicio = DateTime.Now.Year - 3;

            var listaContas = await _context.Set<GrupoEconomicoMembros>()
                .Where(gem => gem.GrupoEconomicoID.Equals(codGrupo) && gem.GrupoEconomico.EmpresasID.Equals(empresaId) && gem.Ativo)
                .Select(gem => gem.ContaClienteID)
                .ToListAsync();

            var historicos = await _context.Set<HistoricoContaCliente>().Include("ContaCliente").Where(hc => hc.Ano >= anoInicio && listaContas.Contains(hc.ContaClienteID) && hc.EmpresasID.Equals(empresaId)).ToListAsync();

            retorno = historicos.GroupBy(g => g.Ano).Select(g => new BuscaHistoricoGrupo()
            {
                Ano = g.Key,
                Montante = g.Sum(h => h.Montante),
                MontanteAVista = g.Sum(h => h.MontanteAVista),
                MontantePrazo = g.Sum(h => h.MontantePrazo),
                Atraso = g.Any(h => h.DiasAtraso > diasAtraso ||
                                    h.PesoAtraso > pesoAtraso ||
                                    h.PRDias ||
                                    h.DiasMaiorAtraso > diasMaiorAtraso ||
                                    h.PesoMaiorAtraso > pesoMaiorAtraso),
                                    //h.PRPeso > pesoPr ||
                                    //h.REPRDias > diasRePr ||
                                    //h.REPRPeso > pesoRePr),
                ClientesAtraso = g.Where(h => h.DiasAtraso > diasAtraso ||
                                    h.PesoAtraso > pesoAtraso ||
                                    h.PRDias ||
                                    h.DiasMaiorAtraso > diasMaiorAtraso ||
                                    h.PesoMaiorAtraso > pesoMaiorAtraso)
                                    //h.PRPeso > pesoPr ||
                                    //h.REPRDias > diasRePr ||
                                    //h.REPRPeso > pesoRePr
                                    .Select(h => h.ContaCliente.Nome).ToList(),
                Pefin = g.Any(h => h.Pefin),
                ClientesPefin = g.Where(h => h.Pefin).Select(h => h.ContaCliente.Nome).ToList(),
                OpFinanciamento = g.Any(h => h.OpFinanciamento),
                ClientesOpFinanciamento = g.Where(h => h.OpFinanciamento).Select(h => h.ContaCliente.Nome).ToList()
            }).OrderByDescending(c => c.Ano).ToList();

            return retorno;

        }
    }
}