using System;
using System.Data.Entity;
using Yara.Data.Entity.Repository.Procedures;
using Yara.Domain.Repository;
using Yara.Domain.Repository.Procedures;

namespace Yara.Data.Entity.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepositoryLog LogRepository { get; }
        public IRepositoryGrupo GrupoRepository { get; }
        public IRepositoryPermissao PermissaoRepository { get; }
        public IRepositoryUsuario UsuarioRepository { get; }
        public IRepositorySegmento SegmentoRepository { get; }
        public IRepositoryTipoCliente TipoClienteRepository { get; }
        public IRepositoryContaClienteComentario ContaClienteComentarioRepository { get; }
        public IRepositoryContaCliente ContaClienteRepository { get; }
        public IRepositoryContaClienteFinanceiro ContaClienteFinanceiroRepository { get; }
        public IRepositoryConceitoCobranca ConceitoCobrancaRepository { get; }
        public IRepositoryContaClienteCodigo ContaClienteCodigoRepository { get; }
        public IRepositoryContaClienteTelefone ContaClienteTelefoneRepository { get; }
        public IRepositoryExperiencia ExperienciaRepository { get; set; }
        public IRepositoryCultura CulturaRepository { get; set; }
        public IRepositoryReceita ReceitaRepository { get; set; }
        public IRepositoryTipoEmpresa TipoEmpresaRepository { get; }
        public IRepositoryTipoGarantia TipoGarantiaRepository { get; }
        public IRepositoryAreaIrrigada AreaIrrigadaRepository { get; }
        public IRepositoryProdutoServico ProdutoServicoRepository { get; }
        public IRepositoryOrigemRecurso OrigemRecursoRepository { get; }
        public IRepositoryIdadeMediaCanavial IdateIdadeMediaCanavialRepository { get; }
        public IRepositoryPorcentagemQuebra PorcentagemQuebraRepository { get; }
        public IRepositoryProdutividadeMedia ProdutividadeMediaRepository { get; }
        public IRepositoryRegiao RegiaoRepository { get; }
        public IRepositoryCustoHaRegiao CustoHaRegiaoRepository { get; }
        public IRepositoryMediaSaca MediaSacaRepository { get; }
        public IRepositoryTipoPecuaria TipoPecuariaRepository { get; }
        public IRepositoryFeriado FeriadoRepository { get; }
        public IRepositoryEstruturaComercial EstruturaComercialRepository { get; }
        public IRepositoryEstruturaComercialPapel EstruturaComercialPapelRepository { get; }
        public IRepositoryParametroSistema ParametroSistemaRepository { get; }
        public IRepositoryTipoEndividamento TipoEndividamentoRepository { get; }
        public IRepositoryAnexo AnexoRepository { get; }
        public IRepositoryFluxoLiberacaoManual FluxoLimiteCreditoRepository { get; set; }
        public IRepositoryFerias FeriasRepository { get; }
        public IRepositoryOrdemVenda OrdemVendaRepository { get; set; }
        public IRepositoryPropostaLC PropostaLCRepository { get; }
        public IRepositoryPropostaLCParceriaAgricola PropostaLCParceriaAgricolaRepository { get; }
        public IRepositoryPropostaLCGarantia PropostaLCGarantiaRepository { get; }
        public IRepositoryPropostaLCCultura PropostaLCCulturaRepository { get; }
        public IRepositoryPropostaLCNecessidadeProduto PropostaLCNecessidadeProdutoRepository { get; }
        public IRepositoryPropostaLCMercado PropostaLCMercadoRepository { get; }
        public IRepositoryPropostaLCPecuaria PropostaLCPecuariaRepository { get; }
        public IRepositoryPropostaLCOutraReceita PropostaLCOutraReceitaRepository { get; }
        public IRepositoryPropostaLCReferencia PropostaLCReferenciaRepository { get; }
        public IRepositoryPropostaLCBemRural PropostaLCBemRuralRepository { get; }
        public IRepositoryPropostaLCBemUrbano PropostaLCBemUrbanoRepository { get; }
        public IRepositoryPropostaLCMaquinaEquipamento PropostaLCMaquinaEquipamentoRepository { get; }
        public IRepositoryPropostaLCPrecoPorRegiao PropostaLCPrecoPorRegiaoRepository { get; }
        public IRepositoryPropostaLCTipoEndividamento PropostaLCTipoEndividamentoRepository { get; }
        public IRepositoryPropostaLCDemonstrativo PropostaLCDemonstrativoRepository { get; }
        public IRepositoryStatusCobranca StatusCobrancaRepository { get; }
        public IRepositoryCobrancaResultado CobrancaResultadoRepository { get; }
        public IRepositoryCidade CidadeRepository { get; }
        public IRepositoryEstado EstadoRepository { get; }
        public IRepositoryOrdemVendaFluxo OrdemVendaFluxoRepository { get; }
        public IRepositoryDivisaoRemessa DivisaoRemessaRepository { get; }
        public IRepositoryAnexoArquivo AnexoArquivoRepository { get; }
        public IRepositoryLogDivisaoRemessaLiberacao LogDivisaoRemessaLiberacaoRepository { get; }
        public IRepositoryEmpresa EmpresaRepository { get; }
        public IRepositoryHistoricoContaCliente HistoricoContaClienteRepository { get; }
        public IRepositoryContaClienteEstruturaComercial ContaClienteEstruturaComercialRepository { get; }
        public IRepositoryContaClienteRepresentante ContaClienteRepresentanteRepository { get; }
        public IRepositoryRepresentante RepresentanteRepository { get; }
        public IRepositoryProcessamentoCarteira ProcessamentoCarteiraRepository { get; }
        public IRepositoryTipoRelacaoGrupoEconomico TipoRelacaoGrupoEconomicoRepository { get; }
        public IRepositoryClassificacaoGrupoEconomico ClassificacaoGrupoEconomicoRepository { get; }
        public IRepositoryFluxoGrupoEconomico FluxoGrupoEconomicoRepository { get; }
        public IRepositoryLiberacaoGrupoEconomicoFluxo LiberacaoGrupoEconomicoFluxoRepository { get; }
        public IRepositoryGrupoEconomicoMembro GrupoEconomicoMembroReporitory { get; }
        public IRepositorySolicitanteGrupoEconomico SolicitanteGrupoEconomicoRepository { get; }
        public IRepositoryGrupoEconomico GrupoEconomicoReporitory { get; }
        public IRepositoryCulturaEstado CulturaEstadoReporitory { get; }
        public IRepositoryEstruturaPerfilUsuario EstruturaPerfilUsuarioRepository { get; set; }
        public IRepositoryBuscaGrupoCookpit BuscaGrupoCookpitRepository { get; }
        public IRepositoryPerfil PerfilRepository { get; }
        public IRepositorySolicitanteSerasa SolicitanteSerasaRepository { get; }
        public IRepositoryPendenciaSerasa PendenciaSerasaRepository { get; }
        public IRepositoryFluxoLiberacaoLimiteCredito FluxoLiberacaoLimiteCreditoRepository { get; }
        public IRepositoryFluxoLiberacaoOrdemVenda FluxoLiberacaoOrdemVendaRepository { get; }
        public IRepositoryPropostaLCHistorico PropostaLCHistorico { get; }
        public IRepositoryBlog BlogRepository { get; }
        public IRepositoryPropostaLCAcompanhamento AcompanhamentoPropostaLCRepository { get; }
        public IRepositoryPropostaLCComite PropostaLcComiteRepository { get; }
        public IRepositoryPropostaLCStatusComite PropostaLcStatusComiteRepository { get; }
        public IRepositoryPropostaLCComiteSolicitante PropostaLCComiteSolicitanteRepository { get; }
        public IRepositoryPropostaLCGrupoEconomico PropostaLcGrupoEconomico { get; }
        public IRepositoryFluxoLiberacaoAbono FluxoLiberacaoAbonoRepository { get; }
        public IRepositoryFluxoLiberacaoProrrogacao FluxoLiberacaoProrrogacaoRepository { get; }
        public IRepositoryMotivoAbono MotivoAbonoRepository { get; }
        public IRepositoryMotivoProrrogacao MotivoProrrogacaoRepository { get; }
        public IRepositoryContaClienteVisita ContaClienteVisitaRepository { get; set; }
        public IRepositoryContaClienteBuscaBens ContaClienteBuscaBensRepository { get; set; }
        public IRepositoryTitulo TituloRepository { get; }
        public IRepositoryLiberacaoOrdemVendaFluxo LiberacaoOrdemVendaFluxoRepository { get; }
        public IRepositoryStatusOrdemVenda StatusOrdemVendaRepository { get; }
        public IRepositoryTituloComentario TituloComentarioRepository { get; }
        public IRepositorySolicitanteFluxo SolicitanteFluxoRepository { get; }
        public IRepositoryPropostaJuridico PropostaJuridicoRepository { get; }
        public IRepositoryPropostaJuridicoHistoricoPagamento PropostaJuridicoHistoricoPagamentoRepository { get; set; }
        public IRepositoryPropostaAbono PropostaAbonoRepository { get; }
        public IRepositoryPropostaAbonoTitulo PropostaAbonoTituloRepository { get; }
        public IRepositoryPropostaJuridicoTitulo PropostaJuridicoTituloRepository { get; }
        public IRepositoryPropostaAbonoComite PropostaAbonoComite { get; }
        public IRepositoryBloqueioLiberacaoCarregamento BloqueioLiberacaoCarregamentoRepository { get; }
        public IRepositoryPropostaJuridicoGarantia PropostaJuridicoGarantiaRepository { get; }
        public IRepositoryAnexoArquivoCobranca AnexoArquivoCobrancaRepository { get; }
        public IRepositoryContaClienteGarantia ContaClienteGarantiaRepository { get; }
        public IRepositoryContaClienteParticipanteGarantia ContaClienteParticipanteGarantiaRepository { get; }
        public IRepositoryContaClienteResponsavelGarantia ContaClienteResponsavelGarantiaRepository { get; }
        public IRepositoryPropostaAbonoAcompanhamento PropostaAbonoAcompanhamento { get; }
        public IRepositoryPropostaProrrogacao PropostaProrrogacao { get; }
        public IRepositoryPropostaProrrogacaoTitulo PropostaProrrogacaoTitulo { get; }
        public IRepositoryPropostaProrrogacaoComite PropostaProrrogacaoComite { get; }
        public IRepositoryPropostaProrrogacaoParcelamento PropostaProrrogacaoParcelamento { get; }
        public IRepositoryPropostaProrrogacaoAcompanhamento PropostaProrrogacaoAcompanhamento { get; }
        public IRepositoryPropostaAlcadaComercial PropostaAlcadaComercial { get; }
        public IRepositoryPropostaAlcadaComercialCultura PropostaAlcadaComercialCultura { get; }
        public IRepositoryPropostaAlcadaComercialDocumento PropostaAlcadaComercialDocumento { get; }
        public IRepositoryPropostaAlcadaComercialParceriaAgricola PropostaAlcadaComercialParceriaAgricola { get; }
        public IRepositoryPropostaAlcadaComercialProdutoServico PropostaAlcadaComercialProdutoServico { get; }
        public IRepositoryPropostaAlcadaComercialRestricao PropostaAlcadaComercialRestricao { get; }
        public IRepositoryPropostaAlcadaComercialAcompanhamento PropostaAlcadaComercialAcompanhamentoRepository { get; }
        public IRepositoryRelatorios Relatorios { get; }
        public IRepositoryMonitor MonitorRepository { get; }
        public IRepositoryFluxoRenovacaoVigenciaLC FluxoRenovacaoVigenciaLCRepository { get; }
        public IRepositoryPropostaRenovacaoVigenciaLC PropostaRenovacaoVigenciaLCRepository { get; }
        public IRepositoryPropostaRenovacaoVigenciaLCComite PropostaRenovacaoVigenciaLCComiteRepository { get; }
        public IRepositoryFluxoLiberacaoLCAdicional FluxoLiberacaoLCAdicionalRepository { get; }
        public IRepositoryPropostaLCAdicional PropostaLCAdicionalRepository { get; }
        public IRepositoryPropostaLCAdicionalAcompanhamento PropostaLCAdicionalAcompanhamentoRepository { get; }
        public IRepositoryPropostaLCAdicionalComite PropostaLCAdicionalComiteRepository { get; }
        public IRepositoryPropostaLCAdicionalHistorico PropostaLCAdicionalHistoricoRepository { get; }
        public IRepositoryFluxoAlcadaAnalise FluxoAlcadaAnaliseRepository { get; }
        public IRepositoryFluxoAlcadaAprovacao FluxoAlcadaAprovacaoRepository { get; }
        public IRepositoryTribunalJustica TribunalJusticaRepository { get; }
        public IRepositoryNotificacoes NotificacoesRepository { get; }

        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
            LogRepository = new RepositoryLog(_context);
            GrupoRepository = new RepositoryGrupo(_context);
            PermissaoRepository = new RepositoryPermissao(_context);
            UsuarioRepository = new RepositoryUsuario(_context);
            SegmentoRepository = new RepositorySegmento(_context);
            TipoClienteRepository = new RepositoryTipoCliente(_context);
            ContaClienteComentarioRepository = new RepositoryContaClienteComentario(_context);
            ContaClienteRepository = new RepositoryContaCliente(_context);
            ContaClienteFinanceiroRepository = new RepositoryContaClienteFinanceiro(_context);
            ConceitoCobrancaRepository = new RepositoryConceitoCobranca(_context);
            ContaClienteCodigoRepository = new RepositoryContaClienteCodigo(_context);
            ContaClienteTelefoneRepository = new RepositoryContaClienteTelefone(_context);
            ExperienciaRepository = new RepositoryExperiencia(_context);
            CulturaRepository = new RepositoryCultura(_context);
            ReceitaRepository = new RepositoryReceita(_context);
            TipoEmpresaRepository = new RepositoryTipoEmpresa(_context);
            TipoGarantiaRepository = new RepositoryTipoGarantia(_context);
            AreaIrrigadaRepository = new RepositoryAreaIrrigada(_context);
            IdateIdadeMediaCanavialRepository = new RepositoryIdadeMediaCanavial(_context);
            ProdutoServicoRepository = new RepositoryProdutoServico(_context);
            RegiaoRepository = new RepositoryRegiao(_context);
            CustoHaRegiaoRepository = new RepositoryCustoHaRegiao(_context);
            PorcentagemQuebraRepository = new RepositoryPorcentagemQuebra(_context);
            ProdutividadeMediaRepository = new RepositoryProdutividadeMedia(_context);
            MediaSacaRepository = new RepositoryMediaSaca(_context);
            TipoPecuariaRepository = new RepositoryTipoPecuaria(_context);
            FeriadoRepository = new RepositoryFeriado(_context);
            EstruturaComercialRepository = new RepositoryEstruturaComercial(_context);
            ParametroSistemaRepository = new RepositoryParametroSistema(_context);
            TipoEndividamentoRepository = new RepositoryTipoEndividamento(_context);
            AnexoRepository = new RepositoryAnexo(_context);
            FluxoLimiteCreditoRepository = new RepositoryFluxoLiberacaoManual(_context);
            FeriasRepository = new RepositoryFerias(_context);
            OrdemVendaRepository = new RepositoryOrdemVenda(_context);
            PropostaLCRepository = new RepositoryPropostaLC(_context);
            PropostaLCParceriaAgricolaRepository = new RepositoryPropostaLCParceriaAgricola(_context);
            PropostaLCGarantiaRepository = new RepositoryPropostaLCGarantia(_context);
            PropostaLCCulturaRepository = new RepositoryPropostaLCCultura(_context);
            PropostaLCNecessidadeProdutoRepository = new RepositoryPropostaLCNecessidadeProduto(_context);
            PropostaLCMercadoRepository = new RepositoryPropostaLCMercado(_context);
            PropostaLCPecuariaRepository = new RepositoryPropostaLCPecuaria(_context);
            PropostaLCOutraReceitaRepository = new RepositoryPropostaLCOutraReceita(_context);
            PropostaLCReferenciaRepository = new RepositoryPropostaLCReferencia(_context);
            PropostaLCBemRuralRepository = new RepositoryPropostaLCBemRural(_context);
            PropostaLCBemUrbanoRepository = new RepositoryPropostaLCBemUrbano(_context);
            PropostaLCMaquinaEquipamentoRepository = new RepositoryPropostaLCMaquinaEquipamento(_context);
            PropostaLCPrecoPorRegiaoRepository = new RepositoryPropostaLCPrecoPorRegiao(_context);
            PropostaLCTipoEndividamentoRepository = new RepositoryPropostaLCTipoEndividamento(_context);
            PropostaLCDemonstrativoRepository = new RepositoryPropostaLCDemonstrativo(_context);
            StatusCobrancaRepository = new RepositoryStatusCobranca(_context);
            CobrancaResultadoRepository = new RepositoryCobrancaResultado(_context);
            CidadeRepository = new RepositoryCidade(_context);
            EstadoRepository = new RepositoryEstado(_context);
            EstruturaComercialPapelRepository = new RepositoryEstruturaComercialPapel(_context);
            OrdemVendaFluxoRepository = new RepositoryOrdemVendaFluxo(_context);
            DivisaoRemessaRepository = new RepositoryDivisaoRemessa(_context);
            AnexoArquivoRepository = new RepositoryAnexoArquivo(_context);
            LogDivisaoRemessaLiberacaoRepository = new RepositoryLogDivisaoRemessaLiberacao(_context);
            HistoricoContaClienteRepository = new RepositoryHistoricoContaCliente(_context);
            ContaClienteEstruturaComercialRepository = new RepositoryContaClienteEstruturaComercial(_context);
            EmpresaRepository = new RepositoryEmpresa(_context);
            ContaClienteRepresentanteRepository = new RepositoryContaClienteRepresentante(_context);
            RepresentanteRepository = new RepositoryRepresentante(_context);
            ProcessamentoCarteiraRepository = new RepositoryProcessamentoCarteira(_context);
            ClassificacaoGrupoEconomicoRepository = new RepositoryClassificacaoGrupoEconomico(_context);
            TipoRelacaoGrupoEconomicoRepository = new RepositoryTipoRelacaoGrupoEconomico(_context);
            FluxoGrupoEconomicoRepository = new RepositoryFluxoGrupoEconomico(_context);
            LiberacaoGrupoEconomicoFluxoRepository = new RepositoryLiberacaoGrupoEconomicoFluxo(_context);
            GrupoEconomicoReporitory = new RepositoryGrupoEconomico(_context);
            GrupoEconomicoMembroReporitory = new RepositoryGrupoEconomicoMembro(_context);
            SolicitanteGrupoEconomicoRepository = new RepositorySolicitanteGrupoEconomico(_context);
            CulturaEstadoReporitory = new RepositoryCulturaEstado(_context);
            EstruturaPerfilUsuarioRepository = new RepositoryEstruturaPerfilUsuario(_context);
            BuscaGrupoCookpitRepository = new RepositoryBuscaGrupoCookpit(_context);
            PerfilRepository = new RepositoryPerfil(_context);
            SolicitanteSerasaRepository = new RepositorySolicitanteSerasa(_context);
            PendenciaSerasaRepository = new RepositoryPendenciaSerasa(_context);
            FluxoLiberacaoLimiteCreditoRepository = new RepositoryFluxoLiberacaoLimiteCredito(_context);
            PropostaLCHistorico = new RepositoryPropostaLCHistorico(_context);
            AcompanhamentoPropostaLCRepository = new RepositoryPropostaLCAcompanhamento(_context);
            BlogRepository = new RepositoryBlog(_context);
            PropostaLcComiteRepository = new RepositoryPropostaLCComite(_context);
            PropostaLcStatusComiteRepository = new RepositoryPropostaLCStatusComite(_context);
            PropostaLCComiteSolicitanteRepository = new RepositoryPropostaLCComiteSolicitante(_context);
            FluxoLiberacaoOrdemVendaRepository = new RepositoryFluxoLiberacaoOrdemVenda(_context);
            PropostaLcGrupoEconomico = new RepositoryPropostaLCGrupoEconomico(_context);
            FluxoLiberacaoAbonoRepository = new RepositoryFluxoLiberacaoAbono(_context);
            FluxoLiberacaoProrrogacaoRepository = new RepositoryFluxoLiberacaoProrrogacao(_context);
            MotivoAbonoRepository = new RepositoryMotivoAbono(_context);
            MotivoProrrogacaoRepository = new RepositoryMotivoProrrogacao(_context);
            ContaClienteVisitaRepository = new RepositoryContaClienteVisita(_context);
            ContaClienteBuscaBensRepository = new RepositoryContaClienteBuscaBens(_context);
            TituloRepository = new RepositoryTitulo(_context);
            LiberacaoOrdemVendaFluxoRepository = new RepositoryLiberacaoOrdemVendaFluxo(_context);
            StatusOrdemVendaRepository = new RepositoryStatusOrdemVenda(_context);
            TituloComentarioRepository = new RepositoryTituloComentario(_context);
            OrigemRecursoRepository = new RepositoryOrigemRecurso(_context);
            SolicitanteFluxoRepository = new RepositorySolicitanteFluxo(_context);
            PropostaJuridicoHistoricoPagamentoRepository = new RepositoryPropostaJuridicoHistoricoPagamento(_context);
            PropostaJuridicoRepository = new RepositoryPropostaJuridico(_context);
            PropostaJuridicoTituloRepository = new RepositoryPropostaJuridicoTitulo(_context);
            PropostaAbonoRepository = new RepositoryPropostaAbono(_context);
            PropostaAbonoTituloRepository = new RepositoryPropostaAbonoTitulo(_context);
            PropostaAbonoComite = new RepositoryPropostaAbonoComite(_context);
            PropostaJuridicoGarantiaRepository = new RepositoryPropostaJuridicoGarantia(_context);
            AnexoArquivoCobrancaRepository = new RepositoryAnexoArquivoCobranca(_context);
            BloqueioLiberacaoCarregamentoRepository = new RepositoryBloqueioLiberacaoCarregamento(_context);
            ContaClienteGarantiaRepository = new RepositoryContaClienteGarantia(_context);
            ContaClienteParticipanteGarantiaRepository = new RepositoryContaClienteParticipanteGarantia(_context);
            ContaClienteResponsavelGarantiaRepository = new RepositoryContaClienteResponsavelGarantia(_context);
            PropostaAbonoAcompanhamento = new RepositoryPropostaAbonoAcompanhamento(_context);
            PropostaProrrogacao = new RepositoryPropostaProrrogacao(_context);
            PropostaProrrogacaoTitulo = new RepositoryPropostaProrrogacaoTitulo(_context);
            PropostaProrrogacaoComite = new RepositoryPropostaProrrogacaoComite(_context);
            PropostaProrrogacaoParcelamento = new RepositoryPropostaProrrogacaoParcelamento(_context);
            PropostaProrrogacaoAcompanhamento = new RepositoryPropostaProrrogacaoAcompanhamento(_context);
            PropostaAlcadaComercial = new RepositoryPropostaAlcadaComercial(_context);
            PropostaAlcadaComercialCultura = new RepositoryPropostaAlcadaComercialCultura(_context);
            PropostaAlcadaComercialDocumento = new RepositoryPropostaAlcadaComercialDocumento(_context);
            PropostaAlcadaComercialParceriaAgricola = new RepositoryPropostaAlcadaComercialParceriaAgricola(_context);
            PropostaAlcadaComercialProdutoServico = new RepositoryPropostaAlcadaComercialProdutoServico(_context);
            PropostaAlcadaComercialRestricao = new RepositoryPropostaAlcadaComercialRestricao(_context);
            PropostaAlcadaComercialAcompanhamentoRepository = new RepositoryPropostaAlcadaComercialAcompanhamento(_context);
            Relatorios = new RepositoryRelatorios(_context);
            MonitorRepository = new RepositoryMonitor(_context);
            FluxoRenovacaoVigenciaLCRepository = new RepositoryFluxoRenovacaoVigenciaLC(_context);
            PropostaRenovacaoVigenciaLCRepository = new RepositoryPropostaRenovacaoVigenciaLC(_context);
            PropostaRenovacaoVigenciaLCComiteRepository = new RepositoryPropostaRenovacaoVigenciaLCComite(_context);
            FluxoLiberacaoLCAdicionalRepository = new RepositoryFluxoLiberacaoLCAdicional(_context);
            PropostaLCAdicionalRepository = new RepositoryPropostaLCAdicional(_context);
            PropostaLCAdicionalAcompanhamentoRepository = new RepositoryPropostaLCAdicionalAcompanhamento(_context);
            PropostaLCAdicionalComiteRepository = new RepositoryPropostaLCAdicionalComite(_context);
            PropostaLCAdicionalHistoricoRepository = new RepositoryPropostaLCAdicionalHistorico(_context);
            FluxoAlcadaAnaliseRepository = new RepositoryFluxoAlcadaAnalise(_context);
            FluxoAlcadaAprovacaoRepository = new RepositoryFluxoAlcadaAprovacao(_context);
            TribunalJusticaRepository = new RepositoryTribunalJustica(_context);
            NotificacoesRepository = new RepositoryNotificacoes(_context);
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}