using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using Yara.Domain;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Context
{
    public class YaraDataContext : DbContext
    {
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<LogLevel> LogLevels { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<Permissao> Permissao { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Segmento> Segmento { get; set; }
        public DbSet<TipoCliente> TipoCliente { get; set; }
        public DbSet<ContaClienteComentario> ContaClienteComentario { get; set; }
        public DbSet<EstruturaComercialPapel> EstruturaComercialPapel { get; set; }
        public DbSet<GrupoEconomico> GrupoEconomico { get; set; }
        public DbSet<EstruturaComercial> EstruturaComercial { get; set; }
        public DbSet<ContaCliente> ContaCliente { get; set; }
        public DbSet<ContaClienteCodigo> ContaClienteCodigo { get; set; }
        public DbSet<ContaClienteTelefone> ContaClienteTelefone { get; set; }
        public DbSet<Representante> Representante { get; set; }
        public DbSet<ContaClienteFinanceiro> ContaClienteFinanceiro { get; set; }
        public DbSet<ConceitoCobranca> ConceitoCobranca { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Experiencia> Experiencias { get; set; }
        public DbSet<Cultura> Culturas { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<TipoEmpresa> TipoEmpresas { get; set; }
        public DbSet<TipoGarantia> TipoGarantias { get; set; }
        public DbSet<AreaIrrigada> AreaIrrigadas { get; set; }
        public DbSet<ProdutoServico> ProdutoServicos { get; set; }
        public DbSet<IdadeMediaCanavial> IdadeMediaCanvial { get; set; }
        public DbSet<PorcentagemQuebra> PorcentagemQuebras { get; set; }
        public DbSet<ProdutividadeMedia> ProdutividadesMedia { get; set; }
        public DbSet<MediaSaca> MediaSacas { get; set; }
        public DbSet<Regiao> Regioes { get; set; }
        public DbSet<CustoHaRegiao> CustoHaRegioes { get; set; }
        public DbSet<TipoPecuaria> TipoPecuarias { get; set; }
        public DbSet<ProcessamentoCarteira> ProcessamentoCarteira { get; set; }
        public DbSet<Feriado> Feriados { get; set; }
        public DbSet<ParametroSistema> ParametroSistemas { get; set; }
        public DbSet<TipoEndividamento> TipoEndividamento { get; set; }
        public DbSet<Anexo> Anexos { get; set; }
        public DbSet<FluxoLiberacaoManual> FluxoLimiteCreditos { get; set; }
        public DbSet<Ferias> Ferias { get; set; }
        public DbSet<OrdemVenda> OrdemVendas { get; set; }
        public DbSet<ItemOrdemVenda> ItemOrdemVendas { get; set; }
        public DbSet<DivisaoRemessa> DivisaoRemessas { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<PropostaLC> PropostaLC { get; set; }
        public DbSet<PropostaLCParceriaAgricola> PropostaLCParceriaAgricola { get; set; }
        public DbSet<PropostaLCGarantia> PropostaLCGarantia { get; set; }
        public DbSet<PropostaLCCultura> PropostaLCCultura { get; set; }
        public DbSet<PropostaLCNecessidadeProduto> PropostaLCNecessidadeProduto { get; set; }
        public DbSet<PropostaLCMercado> PropostaLCMercado { get; set; }
        public DbSet<PropostaLCPecuaria> PropostaLCPecuaria { get; set; }
        public DbSet<PropostaLCOutraReceita> PropostaLCOutrasReceitas { get; set; }
        public DbSet<PropostaLCReferencia> PropostaLCReferencias { get; set; }
        public DbSet<PropostaLCBemRural> PropostaLCBensRurais { get; set; }
        public DbSet<PropostaLCBemUrbano> PropostaLCBensUrbanos { get; set; }
        public DbSet<PropostaLCMaquinaEquipamento> PropostaLCMaquinasEquipamentos { get; set; }
        public DbSet<PropostaLCPrecoPorRegiao> PropostaLCPrecosPorRegiao { get; set; }
        public DbSet<PropostaLCTipoEndividamento> PropostaLCTipoEndividamento { get; set; }
        public DbSet<PropostaLCDemonstrativo> PropostaLCDemonstrativos { get; set; }
        public DbSet<StatusOrdemVendas> StatusOrdemVendas { get; set; }
        public DbSet<SolicitanteFluxo> SolicitanteFluxos { get; set; }
        public DbSet<OrdemVendaFluxo> OrdemVendaFluxos { get; set; }
        public DbSet<PropostaLCStatus> PropostaLCStatus { get; set; }
        public DbSet<AnexoArquivo> AnexoArquivos { get; set; }
        public DbSet<Titulo> Titulos { get; set; }
        public DbSet<LogDivisaoRemessaLiberacao> LogDivisaoRemessaLiberacao { get; set; }
        public DbSet<BloqueioLiberacaoCarteira> BloqueioLiberacaoCarteira { get; set; }
        public DbSet<BloqueioLiberacaoCarregamento> BloqueioLiberacaoCarregamento { get; set; }
        public DbSet<HistoricoContaCliente> HistoricoContaCliente { get; set; }
        public DbSet<Empresas> Empresas { get; set; }
        public DbSet<ContaClienteEstruturaComercial> ContaClienteEstruturaComercial { get; set; }
        public DbSet<ContaClienteRepresentante> ContaClienteRepresentantes { get; set; }
        public DbSet<TipoRelacaoGrupoEconomico> TipoRelacaoGrupoEconomico { get; set; }
        public DbSet<ClassificacaoGrupoEconomico> ClassificacaoGrupoEconomico { get; set; }
        public DbSet<FluxoGrupoEconomico> FluxoGrupoEconomico { get; set; }
        public DbSet<LiberacaoGrupoEconomicoFluxo> LiberacaoGrupoEconomicoFluxo { get; set; }
        public DbSet<StatusGrupoEconomicoFluxo> StatusGrupoEconomicoFluxo { get; set; }
        public DbSet<StatusCobranca> StatusCobranca { get; set; }
        public DbSet<GrupoEconomicoMembros> GrupoEconomicoMembros { get; set; }
        public DbSet<CulturaEstado> CulturaEstado { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<EstruturaPerfilUsuario> EstruturaPerfilUsuario { get; set; }
        public DbSet<SolicitanteSerasa> SolicitanteSerasa { get; set; }
        public DbSet<PendenciasSerasa> PendenciasSerasa { get; set; }
        public DbSet<FluxoLiberacaoLimiteCredito> FluxoLiberacaoLimiteCredito { get; set; }
        public DbSet<PropostaLCHistorico> PropostaLCHistorico { get; set; }
        public DbSet<PropostaLCAcompanhamento> PropostaLCAcompanhamento { get; set; }
        public DbSet<PropostaLCComite> PropostaLcCmComite { get; set; }
        public DbSet<PropostaLCStatusComite> PropostaLCStatusComite { get; set; }
        public DbSet<Cambio> Cambio { get; set; }
        public DbSet<PropostaLCComiteSolicitante> PropostaLCSolicitanteComite { get; set; }
        public DbSet<MotivoAbono> MotivoAbono { get; set; }
        public DbSet<OrigemRecurso> OrigemRecurso { get; set; }
        public DbSet<FluxoLiberacaoOrdemVenda> FluxoLiberacaoOrdemVenda { get; set; }
        public DbSet<FluxoLiberacaoAbono> FluxoLiberacaoAbono { get; set; }
        public DbSet<FluxoLiberacaoProrrogacao> FluxoLiberacaoProrrogacao { get; set; }
        public DbSet<ContaClienteVisita> ContaClienteVisitas { get; set; }
        public DbSet<ContaClienteBuscaBens> ContaClienteBuscaBens { get; set; }
        public DbSet<LiberacaoOrdemVendaFluxo> LiberacaoOrdemVendaFluxo { get; set; }
        public DbSet<TituloComentario> TituloComentarios { get; set; }
        public DbSet<PropostaAbono> PropostaAbono { get; set; }
        public DbSet<PropostaAbonoTitulo> PropostaAbonoTitulo { get; set; }
        public DbSet<PropostaAbonoComite> PropostaAbonoComite { get; set; }
        public DbSet<PropostaAbonoComiteSolicitante> PropostaAbonoComiteSolicitante { get; set; }
        public DbSet<PropostaCobrancaStatus> PropostaAbonoStatus { get; set; }
        public DbSet<PropostaAbonoAcompanhamento> PropostaAbonoAcompanhamento { get; set; }
        public DbSet<PropostaJuridicoGarantia> PropostaJuridicoGarantias { get; set; }
        public DbSet<AnexoArquivoCobranca> AnexoArquivoCobranca { get; set; }
        public DbSet<ContaClienteResponsavelGarantia> ContaClienteResponsavelGarantia { get; set; }
        public DbSet<ContaClienteGarantia> ContaClienteGarantia { get; set; }
        public DbSet<ContaClienteParticipanteGarantia> ContaClienteParticipanteGarantia { get; set; }
        public DbSet<PropostaProrrogacao> PropostaProrrogacao { get; set; }
        public DbSet<PropostaProrrogacaoAcompanhamento> PropostaProrrogacaoAcompanhamento { get; set; }
        public DbSet<PropostaProrrogacaoComiteSolicitante> PropostaProrrogacaoComiteSolicitante { get; set; }
        public DbSet<PropostaProrrogacaoComite> PropostaProrrogacaoComite { get; set; }
        public DbSet<PropostaProrrogacaoTitulo> PropostaProrrogacaoTitulo { get; set; }
        public DbSet<PropostaProrrogacaoParcelamento> PropostaProrrogacaoParcelamento { get; set; }
        public DbSet<PropostaProrrogacaoDetalhe> PropostaProrrogacaoDetalhe { get; set; }
        public DbSet<PropostaJuridico> PropostaJuridico { get; set; }
        public DbSet<PropostaJuridicoTitulo> PropostaJuridicoTitulo { get; set; }
        public DbSet<PropostaJuridicoHistoricoPagamento> PropostaJuridicoHistoricoPagamento { get; set; }
        public DbSet<PropostaAlcadaComercial> PropostaAlcadaComercials { get; set; }
        public DbSet<PropostaAlcadaComercialCultura> PropostaAlcadaComercialCultura { get; set; }
        public DbSet<PropostaAlcadaComercialDocumentos> PropostaAlcadaComercialDocumento { get; set; }
        public DbSet<PropostaAlcadaComercialParceriaAgricola> PropostaAlcadaComercialParceriaAgricola { get; set; }
        public DbSet<PropostaAlcadaComercialProdutoServico> PropostaAlcadaComercialProdutoServico { get; set; }
        public DbSet<PropostaAlcadaComercialRestricoes> PropostaAlcadaComercialRestricoes { get; set; }
        public DbSet<PropostaAlcadaComercialAcompanhamento> PropostaAlcadaComercialAcompanhamento { get; set; }
        public DbSet<FluxoRenovacaoVigenciaLC> FluxoRenovacaoVigenciaLC { get; set; }
        public DbSet<PropostaRenovacaoVigenciaLC> PropostaRenovacaoVigenciaLC { get; set; }
        public DbSet<PropostaRenovacaoVigenciaLCCliente> PropostaRenovacaoVigenciaLCCliente { get; set; }
        public DbSet<PropostaRenovacaoVigenciaLCComite> PropostaRenovacaoVigenciaLCComite { get; set; }
        public DbSet<PropostaLCAdicionalComiteSolicitante> PropostaLCAdicionalSolicitanteComite { get; set; }
        public DbSet<FluxoLiberacaoLCAdicional> FluxoLiberacaoLCAdicional { get; set; }
        public DbSet<PropostaLCAdicional> PropostaLCAdicional { get; set; }
        public DbSet<PropostaLCAdicionalAcompanhamento> PropostaLCAdicionalAcompanhamento { get; set; }
        public DbSet<PropostaLCAdicionalHistorico> PropostaLCAdicionalHistorico { get; set; }
        public DbSet<PropostaLCAdicionalStatusComite> PropostaLCAdicionalStatusComite { get; set; }
        public DbSet<PropostaLCAdicionalComite> PropostaLCAdicionalComite { get; set; }
        public DbSet<FluxoAlcadaAnalise> FluxoAlcadaAnalise { get; set; }
        public DbSet<FluxoAlcadaAprovacao> FluxoAlcadaAprovacao { get; set; }

        public DbSet<TribunalJustica> TribunalJustica { get; set; }

        public YaraDataContext() : base("YaraConnectionString")
        {
            this.Configuration.ProxyCreationEnabled = true;
            //this.Configuration.ProxyCreationEnabled = false;
            //this.Configuration.ProxyCreationEnabled = false;
            // this.Configuration.LazyLoadingEnabled = false;
            //Database.SetInitializer(new YaraDatabaseInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<SolicitanteSerasa>().Ignore(c => c.Usuario);
            modelBuilder.Ignore<LogRetorno>();

            //Padrão chave primária classe Base
            //modelBuilder.Properties().Where(p => p.Name == "ID").Configure(p => p.IsKey());

            //Padrão campos String como Varchar
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));

            //Padrão campos String em 120 caracteres
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(120));

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(type => !string.IsNullOrEmpty(type.Namespace)).Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
