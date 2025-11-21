using AutoMapper;
using Yara.AppService.Dtos;
using Yara.Domain;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.AppService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EstruturaComercialPapelDto, EstruturaComercialPapel>().ReverseMap();
            CreateMap<GrupoDto, Grupo>().ReverseMap();
            CreateMap<PermissaoDto, Permissao>().ReverseMap();
            CreateMap<LogLevelDto, LogLevel>().ReverseMap();
            CreateMap<ContaClienteCodigoDto, ContaClienteCodigo>().ReverseMap();
            CreateMap<LogDto, Log>().ReverseMap();
            CreateMap<LogRetorno, LogDto>();
            CreateMap<LogRetorno, LogContaClienteDto>();
            CreateMap<BuscaLogFluxoAutomaticoDto, BuscaLogFluxoAutomatico>().ReverseMap();
            CreateMap<UsuarioDto, Usuario>().ReverseMap().MaxDepth(3);
            CreateMap<ContaClienteTelefoneDto, ContaClienteTelefone>().ReverseMap();
            CreateMap<SegmentoDto, Segmento>().ReverseMap();
            CreateMap<GrupoEconomicoDto, GrupoEconomico>().ReverseMap();
            CreateMap<EstruturaComercialDto, EstruturaComercial>()
                .ForMember(c => c.ContaClienteEstruturaComercial, opt => opt.Ignore())
                .ForMember(c => c.EstruturaComercialPapel, opt => opt.Ignore())
                .ForMember(c => c.Usuarios, x => x.Ignore())
                .ReverseMap()
                .ForMember(c => c.ContaClienteEstruturaComercial, opt => opt.Ignore())
                .ForMember(c => c.EstruturaComercialPapel, opt => opt.Ignore())
                .ForMember(c => c.Usuarios, x => x.Ignore());
            CreateMap<RepresentanteDto, Representante>()
                .ForMember(r => r.Usuarios, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(r => r.Usuarios, opt => opt.Ignore());
            CreateMap<TipoClienteDto, TipoCliente>().ReverseMap();
            CreateMap<ContaClienteComentarioDto, ContaClienteComentario>().ReverseMap();
            CreateMap<BuscaContaClienteDto, BuscaContaCliente>().ReverseMap();
            CreateMap<BuscaContaClienteEstComlDto, BuscaContaClienteEstComl>().ReverseMap();
            CreateMap<BuscaLogsDto, BuscaLogs>().ReverseMap();
            CreateMap<ConceitoCobrancaDto, ConceitoCobranca>().ReverseMap();
            CreateMap<ContaClienteFinanceiroDto, ContaClienteFinanceiro>().ReverseMap()
                 .ForMember(c => c.ContaCliente, opt => opt.Ignore())
                 .ForMember(c => c.Empresas, opt => opt.Ignore());
            CreateMap<ExperienciaDto, Experiencia>();
            CreateMap<ContaClienteDto, ContaCliente>()
                .ForMember(c => c.ContaClienteFinanceiro, opt => opt.Ignore())
                //.ForMember(c => c.Segmento, opt => opt.Ignore())
                .ForMember(c => c.TipoCliente, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(c => c.ContaClienteFinanceiro, opt => opt.Ignore())
                //.ForMember(c => c.Segmento, opt => opt.Ignore())
                //.ForMember(c => c.TipoCliente, opt => opt.Ignore())
                .ForMember(c => c.PropostaLcs, opt => opt.Ignore())
                .MaxDepth(3);
            CreateMap<ExperienciaDto, Experiencia>().ReverseMap();
            CreateMap<CulturaDto, Cultura>().ReverseMap();
            CreateMap<ReceitaDto, Receita>().ReverseMap();
            CreateMap<TipoEmpresaDto, TipoEmpresa>().ReverseMap();
            CreateMap<TipoGarantiaDto, TipoGarantia>().ReverseMap();
            CreateMap<AreaIrrigadaDto, AreaIrrigada>().ReverseMap();
            CreateMap<ProdutoServicoDto, ProdutoServico>().ReverseMap();
            CreateMap<IdadeMediaCanavialDto, IdadeMediaCanavial>().ReverseMap();
            CreateMap<PorcentagemQuebraDto, PorcentagemQuebra>().ReverseMap();
            CreateMap<ProdutividadeMediaDto, ProdutividadeMedia>().ReverseMap();
            CreateMap<RegiaoDto, Regiao>().ReverseMap();
            CreateMap<CustoHaRegiaoDto, CustoHaRegiao>().ReverseMap();
            CreateMap<MediaSacaDto, MediaSaca>().ReverseMap();
            CreateMap<TipoPecuariaDto, TipoPecuaria>().ReverseMap();
            CreateMap<FeriadoDto, Feriado>().ReverseMap();
            CreateMap<ParametroSistemaDto, ParametroSistema>().ReverseMap();
            CreateMap<TipoEndividamentoDto, TipoEndividamento>().ReverseMap();
            CreateMap<AnexoDto, Anexo>().ReverseMap();
            CreateMap<FluxoLiberacaoManualDto, FluxoLiberacaoManual>().ReverseMap();
            CreateMap<FeriasDto, Ferias>().MaxDepth(1).ReverseMap().MaxDepth(1);
            CreateMap<EstadoDto, Estado>().ReverseMap();
            CreateMap<CidadeDto, Cidade>().ReverseMap();
            CreateMap<LogEnvioOrdensSAPDto, LogEnvioOrdensSAP>().ReverseMap();

            #region Proposta de Limite de Credito

            CreateMap<PropostaLCDto, PropostaLC>().ReverseMap();
            CreateMap<PropostaLCComiteSolicitanteDto, PropostaLCComiteSolicitante>().ReverseMap();
            CreateMap<PropostaLCStatusDto, PropostaLCStatus>().ReverseMap();
            CreateMap<PropostaLCParceriaAgricolaDto, PropostaLCParceriaAgricola>().ReverseMap();
            CreateMap<PropostaLCGarantiaDto, PropostaLCGarantia>()
                .ForMember(p => p.TipoGarantia, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PropostaLCCulturaDto, PropostaLCCultura>()
                .ForMember(p => p.Cultura, opt => opt.Ignore())
                //.ForMember(p => p.Cidade, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PropostaLCNecessidadeProdutoDto, PropostaLCNecessidadeProduto>()
                // .ForMember(p => p.ProdutoServico, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<PropostaLCMercadoDto, PropostaLCMercado>()
                .ForMember(p => p.Cultura, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PropostaLCPecuariaDto, PropostaLCPecuaria>()
                .ForMember(p => p.TipoPecuaria, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PropostaLCOutraReceitaDto, PropostaLCOutraReceita>()
                .ForMember(p => p.Receita, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PropostaLCReferenciaDto, PropostaLCReferencia>()
                .ForMember(p => p.TipoEmpresa, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PropostaLCBemRuralDto, PropostaLCBemRural>()
                .ForMember(p => p.PropostaLCGarantia, opt => opt.Ignore())
                .ForMember(p => p.Cidade, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PropostaLCBemUrbanoDto, PropostaLCBemUrbano>()
                .ForMember(p => p.PropostaLCGarantia, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PropostaLCMaquinaEquipamentoDto, PropostaLCMaquinaEquipamento>()
                .ForMember(p => p.PropostaLCGarantia, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PropostaLCPrecoPorRegiaoDto, PropostaLCPrecoPorRegiao>()
                .ForMember(p => p.Cidade, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PropostaLCTipoEndividamentoDto, PropostaLCTipoEndividamento>()
                .ForMember(p => p.TipoEndividamento, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PropostaLCDemonstrativoDto, PropostaLCDemonstrativo>()
                .ReverseMap();
            //.ForMember(p => p.Conteudo, opt => opt.Ignore());

            #endregion

            #region Proposta de Abono
            CreateMap<FluxoLiberacaoAbonoDto, FluxoLiberacaoAbono>().ReverseMap();
            CreateMap<MotivoAbonoDto, MotivoAbono>().ReverseMap();
            CreateMap<PropostaAbonoDto, PropostaAbono>().ReverseMap();
            CreateMap<PropostaAbonoComiteDto, PropostaAbonoComite>(); //.ReverseMap();
            CreateMap<PropostaAbonoComite, PropostaAbonoComiteDto>()
                .ForMember(
                    dest => dest.NomeUsuario,
                    opt => opt.MapFrom(src => src.Usuario.Nome)
                )
                .ForMember(
                    dest => dest.NomePerfil,
                    opt => opt.MapFrom(src => src.Perfil.Descricao)
                )
                .ForMember(
                    dest => dest.NomeSolicitante,
                    opt => opt.MapFrom(src => src.PropostaAbonoComiteSolicitante.Usuario.Nome)
                );
            CreateMap<PropostaAbonoComiteSolicitanteDto, PropostaAbonoComiteSolicitante>().ReverseMap();
            CreateMap<PropostaAbonoTituloDto, PropostaAbonoTitulo>().ReverseMap();
            CreateMap<BuscaPropostaAbonoDto, BuscaPropostaAbono>().ReverseMap();
            #endregion

            #region Proposta de Prorrogacao
            CreateMap<FluxoLiberacaoProrrogacaoDto, FluxoLiberacaoProrrogacao>().ReverseMap();
            CreateMap<MotivoProrrogacaoDto, MotivoProrrogacao>().ReverseMap();
            CreateMap<PropostaProrrogacaoDto, PropostaProrrogacao>().ReverseMap();
            CreateMap<PropostaProrrogacaoComiteDto, PropostaProrrogacaoComite>(); //.ReverseMap();
            CreateMap<PropostaProrrogacaoComite, PropostaProrrogacaoComiteDto>()
                .ForMember(
                    dest => dest.NomeUsuario,
                    opt => opt.MapFrom(src => src.Usuario.Nome)
                )
                .ForMember(
                    dest => dest.NomePerfil,
                    opt => opt.MapFrom(src => src.Perfil.Descricao)
                )
                .ForMember(
                    dest => dest.NomeSolicitante,
                    opt => opt.MapFrom(src => src.PropostaProrrogacaoComiteSolicitante.Usuario.Nome)
                );
            CreateMap<PropostaProrrogacaoComiteSolicitanteDto, PropostaProrrogacaoComiteSolicitante>().ReverseMap();
            CreateMap<PropostaProrrogacaoTituloDto, PropostaProrrogacaoTitulo>().ReverseMap();
            CreateMap<BuscaPropostaProrrogacaoDto, BuscaPropostaProrrogacao>().ReverseMap();

            CreateMap<BuscaDetalhesPropostaProrrogacaoTituloDto, BuscaDetalhesPropostaProrrogacaoTitulo>().ReverseMap();
            CreateMap<PropostaProrrogacaoParcelamentoDto, PropostaProrrogacaoParcelamento>().ReverseMap();
            #endregion

            CreateMap<OrigemRecursoDto, OrigemRecurso>().ReverseMap();
            CreateMap<StatusCobrancaDto, StatusCobranca>().ReverseMap();
            CreateMap<TituloDto, Titulo>().ReverseMap();
            CreateMap<TituloContaClienteDto, TituloContaCliente>().ReverseMap();
            CreateMap<CobrancaResultadoDto, CobrancaResultado>().ReverseMap();
            CreateMap<CobrancaVencidosResultadoDto, CobrancaVencidosResultado>().ReverseMap();
            CreateMap<CobrancaListaClienteDto, CobrancaListaCliente>().ReverseMap();
            CreateMap<CobrancaListaClienteResultadoDto, CobrancaListaClienteResultado>().ReverseMap();
            CreateMap<BuscaHistoricoGrupoDto, BuscaHistoricoGrupo>().ReverseMap();
            CreateMap<OrdemVendaDto, OrdemVenda>().ReverseMap().MaxDepth(3);
            CreateMap<ItemOrdemVendaDto, ItemOrdemVenda>().ReverseMap();
            CreateMap<DivisaoRemessaDto, DivisaoRemessa>().ReverseMap();
            CreateMap<OrdemVendaFluxoDto, OrdemVendaFluxo>().ReverseMap();
            CreateMap<DivisaoRemessaCockPitDto, DivisaoRemessaCockPit>().ReverseMap();
            CreateMap<DivisaoRemessaLogFluxoDto, DivisaoRemessaLogFluxo>().ReverseMap();
            CreateMap<BuscaOrdemVendaDto, BuscaOrdemVenda>().ReverseMap();
            CreateMap<AnexoArquivoDto, AnexoArquivo>().ReverseMap();
            CreateMap<ProcessamentoCarteiraDto, ProcessamentoCarteira>().ReverseMap();
            CreateMap<LogDivisaoRemessaLiberacaoDto, LogDivisaoRemessaLiberacao>().ReverseMap();
            CreateMap<LimiteCreditoClienteDto, LimiteCreditoCliente>().ReverseMap();
            CreateMap<EmpresasDto, Empresas>().ReverseMap();
            CreateMap<TipoRelacaoGrupoEconomicoDto, TipoRelacaoGrupoEconomico>().ReverseMap();
            CreateMap<HistoricoContaClienteDto, HistoricoContaCliente>().ReverseMap();
            CreateMap<ContaClienteEstruturaComercialDto, ContaClienteEstruturaComercial>()
                .ForMember(c => c.ContaCliente, opt => opt.Ignore())
                .ForMember(c => c.Empresas, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(c => c.ContaCliente, opt => opt.Ignore())
                .ForMember(c => c.Empresas, opt => opt.Ignore());
            CreateMap<ContaClienteRepresentanteDto, ContaClienteRepresentante>()
                .ForMember(c => c.ContaCliente, opt => opt.Ignore())
                .ForMember(c => c.Empresas, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(c => c.ContaCliente, opt => opt.Ignore())
                .ForMember(c => c.Empresas, opt => opt.Ignore());
            CreateMap<BuscaOrdemClienteDto, BuscaOrdemCliente>().ReverseMap();
            CreateMap<BuscaOrdemVendaMultiDto, BuscaOrdemVendaMulti>().ReverseMap();
            CreateMap<BuscaOrdemVendasPrazoDto, BuscaOrdemVendasPrazo>().ReverseMap();
            CreateMap<BuscaOrdemVendasAVistaDto, BuscaOrdemVendasAVista>().ReverseMap();
            CreateMap<BuscaOrdemVendasPagaRetiraDto, BuscaOrdemVendasPagaRetira>().ReverseMap();
            CreateMap<BuscaOrdemVendaSumarizadoDto, BuscaOrdemVendaSumarizado>().ReverseMap();
            CreateMap<OrdemVendaSumarizadoDto, OrdemVendaSumarizado>().ReverseMap();
            CreateMap<ClassificacaoGrupoEconomicoDto, ClassificacaoGrupoEconomico>().ReverseMap();
            CreateMap<StatusGrupoEconomicoFluxoDto, StatusGrupoEconomicoFluxo>().ReverseMap();
            CreateMap<LiberacaoGrupoEconomicoFluxoDto, LiberacaoGrupoEconomicoFluxo>().ReverseMap();
            CreateMap<CulturaEstadoDto, CulturaEstado>().ReverseMap();
            CreateMap<FluxoGrupoEconomicoDto, FluxoGrupoEconomico>()
                .ForMember(c => c.ClassificacaoGrupoEconomico, x => x.Ignore())
                .ReverseMap()
                .ForMember(c => c.ClassificacaoGrupoEconomico, x => x.Ignore());
            CreateMap<GrupoEconomicoMembrosDto, GrupoEconomicoMembros>().ReverseMap();
            CreateMap<BuscaGrupoEconomicoDto, BuscaGrupoEconomico>().ReverseMap();
            CreateMap<SolicitanteGrupoEconomicoDto, SolicitanteGrupoEconomico>().ReverseMap();
            CreateMap<EstruturaPerfilUsuarioDto, EstruturaPerfilUsuario>()
                .ReverseMap();
            CreateMap<PerfilDto, Perfil>().ReverseMap();
            CreateMap<LogWithUserDto, LogwithUser>().ReverseMap();
            CreateMap<SolicitanteSerasaDto, SolicitanteSerasa>().ReverseMap();
            CreateMap<PendenciasSerasaDto, PendenciasSerasa>().ReverseMap();
            CreateMap<BlogDto, Blog>().ReverseMap();
            CreateMap<BuscaCockpitPropostaLCDto, BuscaCockpitPropostaLC>().ReverseMap();
            CreateMap<BuscaPropostaLCPorStatusDto, BuscaPropostaLCPorStatus>().ReverseMap();
            CreateMap<FluxoLiberacaoLimiteCreditoDto, FluxoLiberacaoLimiteCredito>().ReverseMap();
            CreateMap<PropostaLCComiteDto, PropostaLCComite>()
                .ReverseMap()
                .ForMember(c => c.Aprovadores, x => x.Ignore());
            CreateMap<PropostaLCGrupoEconomicoDto, PropostaLCGrupoEconomico>().ReverseMap();
            CreateMap<BuscaGrupoEconomicoPropostaLCDto, BuscaGrupoEconomicoPropostaLC>().ReverseMap();
            CreateMap<BuscaPropostaLCContaClienteDto, BuscaPropostaLCContaCliente>().ReverseMap();
            CreateMap<FluxoLiberacaoOrdemVendaDto, FluxoLiberacaoOrdemVenda>().ReverseMap();
            CreateMap<DadosFinanceiroTituloDto, DadosFinanceiroTitulo>().ReverseMap();
            CreateMap<TitulosGrupoEconomicoMembrosDto, TitulosGrupoEconomicoMembros>().ReverseMap();
            CreateMap<ContaClienteVisitaDto, ContaClienteVisita>().ReverseMap();
            CreateMap<ContaClienteBuscaBensDto, ContaClienteBuscaBens>().ReverseMap();
            CreateMap<TituloComentarioDto, TituloComentario>().ReverseMap();
            CreateMap<TituloComentario, TituloComentarioDto>().ForMember(
                dest => dest.Usuario,
                opt => opt.MapFrom(src => src.Usuario.Nome));
            CreateMap<LiberacaoOrdemVendaFluxoDto, LiberacaoOrdemVendaFluxo>();
            CreateMap<LiberacaoOrdemVendaFluxo, LiberacaoOrdemVendaFluxoDto>()
                .ForMember(
                    dest => dest.Usuario,
                    opt => opt.MapFrom(src => src.Usuario.Nome)
                )
                .ForMember(
                    dest => dest.Perfil,
                    opt => opt.MapFrom(src => src.FluxoLiberacaoOrdemVenda.Perfil.Descricao)
                )
                .ForMember(
                    dest => dest.Nivel,
                    opt => opt.MapFrom(src => src.FluxoLiberacaoOrdemVenda.Nivel)
                )
                .ForMember(
                    dest => dest.StatusOrdemVendaNome,
                    opt => opt.MapFrom(src => src.StatusOrdemVendas.Descricao)
                )
                .ForMember(
                    dest => dest.Sigla,
                    opt => opt.MapFrom(src => src.StatusOrdemVendas.Status)
                );

            CreateMap<BuscaTituloComentarioDto, BuscaTituloComentario>().ReverseMap();
            CreateMap<SolicitanteFluxoDto, SolicitanteFluxo>().ReverseMap();
            CreateMap<SolicitanteFluxo, SolicitanteFluxoDto>().ForMember(
                    dest => dest.DataSolicitacao,
                    opt => opt.MapFrom(src => src.DataCriacao)
                );
            CreateMap<StatusOrdemVendasDto, StatusOrdemVendas>().ReverseMap();
            CreateMap<PropostaJuridicoHistoricoPagamentoDto, PropostaJuridicoHistoricoPagamento>().ReverseMap();
            CreateMap<PropostaJuridicoDto, PropostaJuridico>().ReverseMap();
            CreateMap<PropostaJuridicoTituloDto, PropostaJuridicoTitulo>().ReverseMap();
            CreateMap<PropostaJuridicoGarantiaDto, PropostaJuridicoGarantia>().ReverseMap();
            CreateMap<PropostaCobrancaStatusDto, PropostaCobrancaStatus>().ReverseMap();
            CreateMap<ControleCobrancaEnvioJuridicoDto, ControleCobrancaEnvioJuridico>().ReverseMap();
            CreateMap<AnexoArquivoCobrancaDto, AnexoArquivoCobranca>().ReverseMap();
            CreateMap<AnexoArquivoCobrancaWithOutFileDto, AnexoArquivoCobranca>().ReverseMap();
            CreateMap<ContaClienteGarantiaDto, ContaClienteGarantia>().ReverseMap()
                // .ForMember(c => c.ContaCliente, opt => opt.Ignore())
                .ForMember(c => c.Empresas, opt => opt.Ignore());
            CreateMap<ContaClienteParticipanteGarantiaDto, ContaClienteParticipanteGarantia>().ReverseMap();
            CreateMap<ContaClienteResponsavelGarantiaDto, ContaClienteResponsavelGarantia>().ReverseMap();
            CreateMap<PropostaAlcadaComercial, PropostaAlcadaComercialDto>()
                .ForMember(
                    dest => dest.ResponsavelNome,
                    opt => opt.MapFrom(src => src.Responsavel.Nome)
                )
                .ForMember(
                    dest => dest.ClienteNome,
                    opt => opt.MapFrom(src => src.ContaCliente.Nome)
                )
                .ForMember(
                    dest => dest.TipoClienteNome,
                    opt => opt.MapFrom(src => src.TipoCliente.Nome)
                )
                .ForMember(
                    dest => dest.TipoGarantiaNome,
                    opt => opt.MapFrom(src => src.TipoGarantia.Nome)
                )
                .ForMember(
                    dest => dest.PropostaStatus,
                    opt => opt.MapFrom(src => src.PropostaCobrancaStatus.Status)
                )
                .ForMember(
                    dest => dest.DocumentoContaCliente,
                    opt => opt.MapFrom(src => src.ContaCliente.Documento)
                )
                .ForMember(
                    dest => dest.ExperienciaNome,
                    opt => opt.MapFrom(src => src.Experiencia.Descricao)
                );
            CreateMap<PropostaAlcadaComercialDto, PropostaAlcadaComercial>();
            CreateMap<PropostaAlcadaComercialCultura, PropostaAlcadaComercialCulturaDto>()
                .ForMember(
                    dest => dest.CidadeNome,
                    opt => opt.MapFrom(src => src.Cidade.Nome)
                )
                .ForMember(
                    dest => dest.EstadoNome,
                    opt => opt.MapFrom(src => src.Estado.Nome)
                )
                .ForMember(
                    dest => dest.CulturaNome,
                    opt => opt.MapFrom(src => src.Cultura.Descricao)
                );
            CreateMap<PropostaAlcadaComercialCulturaDto, PropostaAlcadaComercialCultura>();
            CreateMap<PropostaAlcadaComercialProdutoServico, PropostaAlcadaComercialProdutoServicoDto>()
                .ForMember(
                    dest => dest.ProdutoServicoNome,
                    opt => opt.MapFrom(src => src.ProdutoServico.Nome)
                );
            CreateMap<PropostaAlcadaComercialProdutoServicoDto, PropostaAlcadaComercialProdutoServico>();
            CreateMap<PropostaAlcadaComercialParceriaAgricolaDto, PropostaAlcadaComercialParceriaAgricola>().ReverseMap();
            CreateMap<PropostaAlcadaComercialProdutoServicoDto, PropostaAlcadaComercialProdutoServico>().ReverseMap();
            CreateMap<PropostaAlcadaComercialRestricoesDto, PropostaAlcadaComercialRestricoes>().ReverseMap();
            CreateMap<PropostaAlcadaComercialDocumentosDto, PropostaAlcadaComercialDocumentos>().ReverseMap();
            CreateMap<PropostaAlcadaComercialAcompanhamentoDto, PropostaAlcadaComercialAcompanhamento>().ReverseMap();
            CreateMap<BuscaCockpitPropostaAlcadaDto, BuscaCockpitPropostaAlcada>().ReverseMap();
            CreateMap<BuscaPropostasDto, BuscaPropostas>().ReverseMap();
            CreateMap<BuscaPropostasSearchDto, BuscaPropostasSearch>().ReverseMap();

            CreateMap<MonitorQuantidadesFilas, MonitorQuantidadesFilasDto>();
            CreateMap<MonitorTotalProcessado, MonitorTotalProcessadoDto>();
            CreateMap<MonitorInfoGraficoProcessamento, MonitorInfoGraficoProcessamentoDto>();
            CreateMap<MonitorMensagemErro, MonitorMensagemErroDto>();
            CreateMap<MonitorOVNotificacao, MonitorOVNotificacaoDto>();
            CreateMap<MonitorOVResultado, MonitorOVResultadoDto>();
            CreateMap<MonitorOVMensagemErro, MonitorOVMensagemErroDto>();

            CreateMap<FluxoRenovacaoVigenciaLCDto, FluxoRenovacaoVigenciaLC>().ReverseMap();
            CreateMap<FiltroContaClientePropostaRenovacaoVigenciaLCDto, FiltroContaClientePropostaRenovacaoVigenciaLC>().ReverseMap();
            CreateMap<BuscaContaClientePropostaRenovacaoLCDto, BuscaContaClientePropostaRenovacaoLC>().ReverseMap();
            CreateMap<PropostaRenovacaoVigenciaLCDto, PropostaRenovacaoVigenciaLC>().ReverseMap();
            CreateMap<PropostaRenovacaoVigenciaLC, PropostaRenovacaoVigenciaLCDto>().ForMember(
                    dest => dest.ResponsavelNome,
                    opt => opt.MapFrom(src => src.Responsavel.Nome)
                );
            CreateMap<PropostaRenovacaoVigenciaLCClienteDto, PropostaRenovacaoVigenciaLCCliente>().ReverseMap();
            CreateMap<PropostaRenovacaoVigenciaLCComiteDto, PropostaRenovacaoVigenciaLCComite>().ReverseMap();
            CreateMap<BuscaCockpitPropostaRenovacaoVigenciaLCDto, BuscaCockpitPropostaRenovacaoVigenciaLC>().ReverseMap();

            CreateMap<FluxoLiberacaoLCAdicionalDto, FluxoLiberacaoLCAdicional>().ReverseMap();
            CreateMap<PropostaLCAdicionalDto, PropostaLCAdicional>().ReverseMap();
            CreateMap<PropostaLCAdicional, PropostaLCAdicionalDto>().ForMember(
                    dest => dest.ResponsavelNome,
                    opt => opt.MapFrom(src => src.Responsavel.Nome)
                );
            CreateMap<PropostaLCAdicionalAcompanhamentoDto, PropostaLCAdicionalAcompanhamento>().ReverseMap();
            CreateMap<PropostaLCAdicionalStatusComiteDto, PropostaLCAdicionalStatusComite>().ReverseMap();
            CreateMap<PropostaLCAdicionalComiteSolicitanteDto, PropostaLCAdicionalComiteSolicitante>().ReverseMap();
            CreateMap<PropostaLCAdicionalComiteDto, PropostaLCAdicionalComite>().ReverseMap();
            CreateMap<BuscaPropostaLCAdicionalDto, BuscaPropostaLCAdicional>().ReverseMap();
            
            CreateMap<FluxoAlcadaAnaliseDto, FluxoAlcadaAnalise>().ReverseMap();
            CreateMap<FluxoAlcadaAprovacaoDto, FluxoAlcadaAprovacao>().ReverseMap();

            CreateMap<BuscaRemessasCliente, BuscaRemessasClienteDto>();

            CreateMap<TribunalJusticaDto, TribunalJustica>()
                .ReverseMap()
                .ForMember(r => r.ContaClienteID, opt => opt.Ignore());

            CreateMap<NotificacaoUsuario, NotificacaoUsuarioDto>();

        }
    }
}
