using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaRenovacaoVigenciaLC : RepositoryBase<PropostaRenovacaoVigenciaLC>, IRepositoryPropostaRenovacaoVigenciaLC
    {
        public RepositoryPropostaRenovacaoVigenciaLC(DbContext context) : base(context)
        {
        }

        public int GetMaxNumeroInterno()
        {
            try
            {
                if (!_context.Set<PropostaRenovacaoVigenciaLC>().Any())
                    return 1;

                return (_context.Set<PropostaRenovacaoVigenciaLC>().Max(p => p.NumeroInternoProposta)) + 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BuscaContaClientePropostaRenovacaoLC>> GetClientListByFilterAsync(FiltroContaClientePropostaRenovacaoVigenciaLC filter)
        {
            try
            {
                IEnumerable<BuscaContaClientePropostaRenovacaoLC> list = await _context.Database.SqlQuery<BuscaContaClientePropostaRenovacaoLC>("EXEC spBuscaClientePropostaRenovacaoLC " +
                    "@pContaClienteID," +
                    "@pEmpresasID," +
                    "@pCodigo," +
                    "@pNome," +
                    "@pApelido," +
                    "@pDocumento," +
                    "@pSegmentacao," +
                    "@pCategorizacao," +
                    "@pNomeGrupo," +
                    "@pVigenciaInicial," +
                    "@pVigenciaFinal," +
                    "@pRating," +
                    "@pValorLCInicial," +
                    "@pValorLCFinal," +
                    "@pClienteTop10Sim," +
                    "@pClienteTop10Nao," +
                    "@pConsultaSerasaInicial," +
                    "@pConsultaSerasaFinal," +
                    "@pRestricaoSerasa," +
                    "@pRestricoesYaraSim," +
                    "@pRestricoesYaraNao," +
                    "@pRestricoesSerasaGrupoSim," +
                    "@pRestricoesSerasaGrupoNao," +
                    "@pRestricoesYaraGrupoSim," +
                    "@pRestricoesYaraGrupoNao," +
                    "@pLCAndamentoSim," +
                    "@pLCAndamentoNao," +
                    "@pAlcadaAndamentoSim," +
                    "@pAlcadaAndamentoNao," +
                    "@pComComprasInicial," +
                    "@pComComprasFinal," +
                    "@pVigenciaGarantiaInicial," +
                    "@pVigenciaGarantiaFinal," +
                    "@pRepresentanteID," +
                    "@pAnalistaID," +
                    "@pCTC," +
                    "@pGC," +
                    "@pDiretoria," +
                    "@pPropostaRenovacaoInicial," +
                    "@pPropostaRenovacaoFinal," +
                    "@pClientesRenovadosSim," +
                    "@pClientesRenovadosNao," +
                    "@pXMLGuidList",
                    new SqlParameter("pContaClienteID", ((filter.ContaClienteID == null || filter.ContaClienteID == Guid.Empty) ? Convert.DBNull : filter.ContaClienteID)),
                    new SqlParameter("pEmpresasID", filter.EmpresasID),
                    new SqlParameter("pCodigo", (string.IsNullOrEmpty(filter.Codigo) ? Convert.DBNull : filter.Codigo)),
                    new SqlParameter("pNome", (string.IsNullOrEmpty(filter.NomeCliente) ? Convert.DBNull : filter.NomeCliente)),
                    new SqlParameter("pApelido", (string.IsNullOrEmpty(filter.Apelido) ? Convert.DBNull : filter.Apelido)),
                    new SqlParameter("pDocumento", (string.IsNullOrEmpty(filter.Documento) ? Convert.DBNull : filter.Documento)),
                    new SqlParameter("pSegmentacao", (string.IsNullOrEmpty(filter.Segmentacao) ? Convert.DBNull : filter.Segmentacao)),
                    new SqlParameter("pCategorizacao", (string.IsNullOrEmpty(filter.Categorizacao) ? Convert.DBNull : filter.Categorizacao)),
                    new SqlParameter("pNomeGrupo", (string.IsNullOrEmpty(filter.NomeGrupo) ? Convert.DBNull : filter.NomeGrupo)),
                    new SqlParameter("pVigenciaInicial", (filter.DeVigenciaLC == null ? Convert.DBNull : filter.DeVigenciaLC)),
                    new SqlParameter("pVigenciaFinal", (filter.AteVigenciaLC == null ? Convert.DBNull : filter.AteVigenciaLC)),
                    new SqlParameter("pRating", (string.IsNullOrEmpty(filter.Rating) ? Convert.DBNull : filter.Rating)),
                    new SqlParameter("pValorLCInicial", (filter.DeValorLC == null ? Convert.DBNull : filter.DeValorLC)),
                    new SqlParameter("pValorLCFinal", (filter.AteValorLC == null ? Convert.DBNull : filter.AteValorLC)),
                    new SqlParameter("pClienteTop10Sim", (filter.ClienteTop10Sim == null ? Convert.DBNull : filter.ClienteTop10Sim)),
                    new SqlParameter("pClienteTop10Nao", (filter.ClienteTop10Nao == null ? Convert.DBNull : filter.ClienteTop10Nao)),
                    new SqlParameter("pConsultaSerasaInicial", (filter.DeConsultaSerasa == null ? Convert.DBNull : filter.DeConsultaSerasa)),
                    new SqlParameter("pConsultaSerasaFinal", (filter.AteConsultaSerasa == null ? Convert.DBNull : filter.AteConsultaSerasa)),
                    new SqlParameter("pRestricaoSerasa", (filter.RestricaoSerasa == null ? Convert.DBNull : filter.RestricaoSerasa)),
                    new SqlParameter("pRestricoesYaraSim", (filter.RestricoesYaraSim == null ? Convert.DBNull : filter.RestricoesYaraSim)),
                    new SqlParameter("pRestricoesYaraNao", (filter.RestricoesYaraNao == null ? Convert.DBNull : filter.RestricoesYaraNao)),
                    new SqlParameter("pRestricoesSerasaGrupoSim", (filter.RestricaoSerasaGrupoSim == null ? Convert.DBNull : filter.RestricaoSerasaGrupoSim)),
                    new SqlParameter("pRestricoesSerasaGrupoNao", (filter.RestricaoSerasaGrupoNao == null ? Convert.DBNull : filter.RestricaoSerasaGrupoNao)),
                    new SqlParameter("pRestricoesYaraGrupoSim", (filter.RestricoesYaraGrupoSim == null ? Convert.DBNull : filter.RestricoesYaraGrupoSim)),
                    new SqlParameter("pRestricoesYaraGrupoNao", (filter.RestricoesYaraGrupoNao == null ? Convert.DBNull : filter.RestricoesYaraGrupoNao)),
                    new SqlParameter("pLCAndamentoSim", (filter.LCAndamentoSim == null ? Convert.DBNull : filter.LCAndamentoSim)),
                    new SqlParameter("pLCAndamentoNao", (filter.LCAndamentoNao == null ? Convert.DBNull : filter.LCAndamentoNao)),
                    new SqlParameter("pAlcadaAndamentoSim", (filter.AlcadaAndamentoSim == null ? Convert.DBNull : filter.AlcadaAndamentoSim)),
                    new SqlParameter("pAlcadaAndamentoNao", (filter.AlcadaAndamentoNao == null ? Convert.DBNull : filter.AlcadaAndamentoNao)),
                    new SqlParameter("pComComprasInicial", (filter.DeComCompras == null ? Convert.DBNull : filter.DeComCompras)),
                    new SqlParameter("pComComprasFinal", (filter.AteComCompras == null ? Convert.DBNull : filter.AteComCompras)),
                    new SqlParameter("pVigenciaGarantiaInicial", (filter.DeVigenciaGarantia == null ? Convert.DBNull : filter.DeVigenciaGarantia)),
                    new SqlParameter("pVigenciaGarantiaFinal", (filter.AteVigenciaGarantia == null ? Convert.DBNull : filter.AteVigenciaGarantia)),
                    new SqlParameter("pRepresentanteID", (filter.RepresentanteID == null ? Convert.DBNull : filter.RepresentanteID)),
                    new SqlParameter("pAnalistaID", (filter.AnalistaID == null ? Convert.DBNull : filter.AnalistaID)),
                    new SqlParameter("pCTC", (string.IsNullOrEmpty(filter.CTC) ? Convert.DBNull : filter.CTC)),
                    new SqlParameter("pGC", (string.IsNullOrEmpty(filter.GC) ? Convert.DBNull : filter.GC)),
                    new SqlParameter("pDiretoria", (string.IsNullOrEmpty(filter.Diretoria) ? Convert.DBNull : filter.Diretoria)),
                    new SqlParameter("pPropostaRenovacaoInicial", (filter.DePropostaRenovacao == null ? Convert.DBNull : filter.DePropostaRenovacao)),
                    new SqlParameter("pPropostaRenovacaoFinal", (filter.AtePropostaRenovacao == null ? Convert.DBNull : filter.AtePropostaRenovacao)),
                    new SqlParameter("pClientesRenovadosSim", (filter.ClientesRenovadosSim == null ? Convert.DBNull : filter.ClientesRenovadosSim)),
                    new SqlParameter("pClientesRenovadosNao", (filter.ClientesRenovadosNao == null ? Convert.DBNull : filter.ClientesRenovadosNao)),
                    new SqlParameter("pXMLGuidList", (string.IsNullOrEmpty(filter.XMLGuidList) ? Convert.DBNull : filter.XMLGuidList))
                    ).ToListAsync();

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Guid?> GetProposalByAccountClient(Guid contaClienteID)
        {
            try
            {
                Guid? proposalID = await _context.Database.SqlQuery<Guid?>($"Select PropostaRenovacaoVigenciaLCID From PropostaRenovacaoVigenciaLCCliente PRVC Inner Join PropostaRenovacaoVigenciaLC PRV On PRV.ID = PRVC.PropostaRenovacaoVigenciaLCID Where PRVC.ContaClienteID = '{contaClienteID}' And PRV.PropostaLCStatusID Not In ('AA', 'XE', 'XR')").SingleOrDefaultAsync();

                return proposalID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<BuscaCockpitPropostaRenovacaoVigenciaLC>> GetCockpit(Guid usuarioID, string empresaID)
        {
            try
            {
                IEnumerable<BuscaCockpitPropostaRenovacaoVigenciaLC> list = await _context.Database.SqlQuery<BuscaCockpitPropostaRenovacaoVigenciaLC>("EXEC spBuscaCockpitPropostaRenovacaoVigenciaLC @pUsuarioID, @pEmpresasID",
                    new SqlParameter("pUsuarioID", usuarioID),
                    new SqlParameter("pEmpresasID", empresaID)
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
