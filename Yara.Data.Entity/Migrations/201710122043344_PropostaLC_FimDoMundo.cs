namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaLC_FimDoMundo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PropostaLCCultura", "CustoHaRegiaoID", "dbo.CustoHaRegiao");
            DropForeignKey("dbo.PropostaLCCultura", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.PropostaLCCultura", "ProdutividadeMediaID", "dbo.ProdutividadeMedia");
            DropForeignKey("dbo.PropostaLCNecessidadeProduto", "ProdutoServicoID", "dbo.ProdutoServico");
            DropForeignKey("dbo.PropostaLCNecessidadeProduto", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCParceriaAgricula", "ContaClienteID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCParceriaAgricula", "PropostaLCID", "dbo.ContaCliente");
            DropForeignKey("dbo.PropostaLCPrincipaisMercadosAtuacao", "CulturaID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCPrincipaisMercadosAtuacao", "PropostaLCID", "dbo.Cultura");
            DropForeignKey("dbo.PropostaLCOutrasReceitas", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCOutrasReceitas", "ReceitaID", "dbo.Receita");
            DropForeignKey("dbo.PropostaLC", "SegmentoID", "dbo.Segmento");
            DropIndex("dbo.PropostaLC", new[] { "SegmentoID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "EstadoID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "ProdutividadeMediaID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "CustoHaRegiaoID" });
            DropIndex("dbo.PropostaLCGarantia", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCPecuaria", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCOutrasReceitas", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCOutrasReceitas", new[] { "ReceitaID" });
            DropIndex("dbo.PropostaLCReferencia", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCReferencia", new[] { "TipoEmpresaID" });
            DropIndex("dbo.PropostaLCParceriaAgricula", new[] { "ContaClienteID" });
            DropIndex("dbo.PropostaLCParceriaAgricula", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCPrincipaisMercadosAtuacao", new[] { "CulturaID" });
            DropIndex("dbo.PropostaLCPrincipaisMercadosAtuacao", new[] { "PropostaLCID" });
            CreateTable(
                "dbo.PropostaLCBemRural",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        GarantiaID = c.Guid(),
                        IR = c.String(maxLength: 50, unicode: false),
                        CidadeID = c.Guid(),
                        AreaTotalHa = c.Decimal(precision: 18, scale: 2),
                        Benfeitorias = c.Decimal(precision: 18, scale: 2),
                        Onus = c.Decimal(precision: 18, scale: 2),
                        PropostaLCGarantia_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cidade", t => t.CidadeID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.PropostaLCGarantia", t => t.PropostaLCGarantia_ID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.CidadeID)
                .Index(t => t.PropostaLCGarantia_ID);
            
            CreateTable(
                "dbo.PropostaLCPrecoPorRegiao",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        CidadeID = c.Guid(),
                        ValorHaCultivavel = c.Decimal(precision: 18, scale: 2),
                        ValorHaNaoCultivavel = c.Decimal(precision: 18, scale: 2),
                        ModuloRural = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cidade", t => t.CidadeID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.CidadeID);
            
            CreateTable(
                "dbo.PropostaLCBemUrbano",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        GarantiaID = c.Guid(),
                        IR = c.String(maxLength: 50, unicode: false),
                        Descricao = c.String(maxLength: 128, unicode: false),
                        AreaTotal = c.Decimal(precision: 18, scale: 2),
                        ValorComBenfeitorias = c.Decimal(precision: 18, scale: 2),
                        Onus = c.Decimal(precision: 18, scale: 2),
                        ValorAvaliado = c.Decimal(precision: 18, scale: 2),
                        PropostaLCGarantia_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.PropostaLCGarantia", t => t.PropostaLCGarantia_ID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.PropostaLCGarantia_ID);
            
            CreateTable(
                "dbo.PropostaLCMercado",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        CulturaID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cultura", t => t.CulturaID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.CulturaID);
            
            CreateTable(
                "dbo.PropostaLCMaquinaEquipamento",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        GarantiaID = c.Guid(),
                        Descricao = c.String(maxLength: 128, unicode: false),
                        Ano = c.Int(),
                        Valor = c.Decimal(precision: 18, scale: 2),
                        PropostaLCGarantia_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.PropostaLCGarantia", t => t.PropostaLCGarantia_ID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.PropostaLCGarantia_ID);
            
            CreateTable(
                "dbo.PropostaLCNecessidadeProduto",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        ProdutoServicoID = c.Guid(),
                        Quantidade = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProdutoServico", t => t.ProdutoServicoID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID);
            
            CreateTable(
                "dbo.PropostaLCOutraReceita",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        ReceitaID = c.Guid(),
                        AnoOutrasReceitas = c.Int(),
                        ReceitaPrevista = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.Receita", t => t.ReceitaID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.ReceitaID);
            
            CreateTable(
                "dbo.PropostaLCParceriaAgricola",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        Documento = c.String(maxLength: 20, unicode: false),
                        InscricaoEstadual = c.String(maxLength: 40, unicode: false),
                        Nome = c.String(maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .Index(t => t.PropostaLCID);
            
            AddColumn("dbo.PropostaLC", "RegimeCasamento", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.PropostaLC", "PossuiAreaIrrigada", c => c.Boolean());
            AddColumn("dbo.PropostaLC", "ClienteGarantia", c => c.Boolean());
            AddColumn("dbo.PropostaLC", "PossuiCriacaoDeAnimais", c => c.Boolean());
            AddColumn("dbo.PropostaLC", "PossuiOutrasReceitas", c => c.Boolean());
            AddColumn("dbo.PropostaLC", "ComentarioMercado", c => c.String(maxLength: 256, unicode: false));
            AddColumn("dbo.PropostaLC", "PrecoMedioFt", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLC", "NecessidadeAnualFoliar", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLC", "PrecoMedioLt", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLC", "RestricaoSERASA", c => c.String(maxLength: 20, unicode: false));
            AddColumn("dbo.PropostaLC", "RestricaoTJ", c => c.String(maxLength: 20, unicode: false));
            AddColumn("dbo.PropostaLC", "RestricaoIBAMA", c => c.String(maxLength: 20, unicode: false));
            AddColumn("dbo.PropostaLC", "RestricaoTrabalhoEscravo", c => c.String(maxLength: 20, unicode: false));
            AddColumn("dbo.PropostaLC", "ComentarioPatrimonio", c => c.String(unicode: false, storeType: "text"));
            AddColumn("dbo.PropostaLCCultura", "Area", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCCultura", "ProdutividadeMedia", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCCultura", "CustoHa", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCCultura", "Quebra", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCCultura", "CustoArrendamentoSacaHa", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCPecuaria", "AnoPecuaria", c => c.Int());
            AddColumn("dbo.PropostaLCPecuaria", "Preco", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCPecuaria", "Despesa", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCGarantia", "GarantiaRecebida", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLCGarantia", "Documento", c => c.String(maxLength: 20, unicode: false));
            AddColumn("dbo.PropostaLCGarantia", "Nome", c => c.String(maxLength: 128, unicode: false));
            AddColumn("dbo.PropostaLCGarantia", "Comentario", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaLCReferencia", "TipoReferencia", c => c.String(maxLength: 20, unicode: false));
            AddColumn("dbo.PropostaLCReferencia", "NomeEmpresa", c => c.String(maxLength: 128, unicode: false));
            AddColumn("dbo.PropostaLCReferencia", "NomeBanco", c => c.String(maxLength: 128, unicode: false));
            AddColumn("dbo.PropostaLCReferencia", "NomeContato", c => c.String(maxLength: 128, unicode: false));
            AddColumn("dbo.PropostaLCReferencia", "Desde", c => c.String(maxLength: 20, unicode: false));
            AddColumn("dbo.PropostaLCReferencia", "LCAtual", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCReferencia", "Vigencia", c => c.DateTime());
            AddColumn("dbo.PropostaLCReferencia", "Garantias", c => c.String(maxLength: 128, unicode: false));
            AddColumn("dbo.PropostaLCReferencia", "Comentarios", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("dbo.PropostaLC", "EstadoCivil", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.PropostaLC", "NomeConjugue", c => c.String(maxLength: 128, unicode: false));
            AlterColumn("dbo.PropostaLC", "CPFConjugue", c => c.String(maxLength: 14, unicode: false));
            AlterColumn("dbo.PropostaLC", "SharePretentido", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLC", "ToneladasArmazem", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLC", "PrincipaisFornecedores", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("dbo.PropostaLC", "PrincipaisClientes", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("dbo.PropostaLC", "QtdeSacas", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLC", "Trading", c => c.String(maxLength: 128, unicode: false));
            AlterColumn("dbo.PropostaLC", "FonteRecursosCarteira", c => c.String(maxLength: 128, unicode: false));
            AlterColumn("dbo.PropostaLC", "NecessidadeAnualFertilizantes", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLC", "NumeroComprasAno", c => c.Int());
            AlterColumn("dbo.PropostaLC", "ResultadoUltimaSafra", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLC", "TotalProducaoAlcool", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLC", "TotalProducaoAcucar", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLC", "VolumeMoagemPropria", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLC", "CustoMedioProducao", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLC", "CapacidadeMoagem", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLC", "TotalMWAno", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLC", "CustoCarregamentoTransporte", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLCCultura", "PropostaLCID", c => c.Guid(nullable: false));
            AlterColumn("dbo.PropostaLCCultura", "Arrendamento", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLCPecuaria", "PropostaLCID", c => c.Guid(nullable: false));
            AlterColumn("dbo.PropostaLCPecuaria", "Quantidade", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLCGarantia", "PropostaLCID", c => c.Guid(nullable: false));
            AlterColumn("dbo.PropostaLCReferencia", "PropostaLCID", c => c.Guid(nullable: false));
            AlterColumn("dbo.PropostaLCReferencia", "TipoEmpresaID", c => c.Guid());
            AlterColumn("dbo.PropostaLCReferencia", "Municipio", c => c.String(maxLength: 128, unicode: false));
            AlterColumn("dbo.PropostaLCReferencia", "Telefone", c => c.String(maxLength: 64, unicode: false));
            CreateIndex("dbo.PropostaLCGarantia", "PropostaLCID");
            CreateIndex("dbo.PropostaLCPecuaria", "PropostaLCID");
            CreateIndex("dbo.PropostaLCCultura", "PropostaLCID");
            CreateIndex("dbo.PropostaLCReferencia", "PropostaLCID");
            CreateIndex("dbo.PropostaLCReferencia", "TipoEmpresaID");
            DropColumn("dbo.PropostaLC", "TipoProposta");
            DropColumn("dbo.PropostaLC", "SegmentoID");
            DropColumn("dbo.PropostaLC", "Pecuaria");
            DropColumn("dbo.PropostaLC", "Garantia");
            DropColumn("dbo.PropostaLC", "IsAreaIrrigada");
            DropColumn("dbo.PropostaLC", "Documento");
            DropColumn("dbo.PropostaLC", "Prazo");
            DropColumn("dbo.PropostaLCCultura", "EstadoID");
            DropColumn("dbo.PropostaLCCultura", "ProdutividadeMediaID");
            DropColumn("dbo.PropostaLCCultura", "CustoHaRegiaoID");
            DropColumn("dbo.PropostaLCCultura", "Ativo");
            DropColumn("dbo.PropostaLCCultura", "UsuarioIDCriacao");
            DropColumn("dbo.PropostaLCCultura", "UsuarioIDAlteracao");
            DropColumn("dbo.PropostaLCCultura", "DataCriacao");
            DropColumn("dbo.PropostaLCCultura", "DataAlteracao");
            DropColumn("dbo.PropostaLCPecuaria", "Ano");
            DropColumn("dbo.PropostaLCPecuaria", "Ativo");
            DropColumn("dbo.PropostaLCPecuaria", "UsuarioIDCriacao");
            DropColumn("dbo.PropostaLCPecuaria", "UsuarioIDAlteracao");
            DropColumn("dbo.PropostaLCPecuaria", "DataCriacao");
            DropColumn("dbo.PropostaLCPecuaria", "DataAlteracao");
            DropColumn("dbo.PropostaLCGarantia", "Ativo");
            DropColumn("dbo.PropostaLCGarantia", "UsuarioIDCriacao");
            DropColumn("dbo.PropostaLCGarantia", "UsuarioIDAlteracao");
            DropColumn("dbo.PropostaLCGarantia", "DataCriacao");
            DropColumn("dbo.PropostaLCGarantia", "DataAlteracao");
            DropColumn("dbo.PropostaLCReferencia", "Tipo");
            DropColumn("dbo.PropostaLCReferencia", "Contato");
            DropColumn("dbo.PropostaLCReferencia", "Ativo");
            DropColumn("dbo.PropostaLCReferencia", "UsuarioIDCriacao");
            DropColumn("dbo.PropostaLCReferencia", "UsuarioIDAlteracao");
            DropColumn("dbo.PropostaLCReferencia", "DataCriacao");
            DropColumn("dbo.PropostaLCReferencia", "DataAlteracao");
            DropTable("dbo.PropostaLCNecessidadeProduto");
            DropTable("dbo.PropostaLCOutrasReceitas");
            DropTable("dbo.PropostaLCParceriaAgricula");
            DropTable("dbo.PropostaLCPrincipaisMercadosAtuacao");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PropostaLCPrincipaisMercadosAtuacao",
                c => new
                    {
                        CulturaID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CulturaID, t.PropostaLCID });
            
            CreateTable(
                "dbo.PropostaLCParceriaAgricula",
                c => new
                    {
                        ContaClienteID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteID, t.PropostaLCID });
            
            CreateTable(
                "dbo.PropostaLCOutrasReceitas",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(),
                        Ano = c.Int(nullable: false),
                        ReceitaID = c.Guid(nullable: false),
                        ReceitaPrevista = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaLCNecessidadeProduto",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(),
                        ProdutoServicoID = c.Guid(nullable: false),
                        Quantidade = c.Single(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.PropostaLCReferencia", "DataAlteracao", c => c.DateTime());
            AddColumn("dbo.PropostaLCReferencia", "DataCriacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.PropostaLCReferencia", "UsuarioIDAlteracao", c => c.Guid());
            AddColumn("dbo.PropostaLCReferencia", "UsuarioIDCriacao", c => c.Guid(nullable: false));
            AddColumn("dbo.PropostaLCReferencia", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLCReferencia", "Contato", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaLCReferencia", "Tipo", c => c.Int(nullable: false));
            AddColumn("dbo.PropostaLCGarantia", "DataAlteracao", c => c.DateTime());
            AddColumn("dbo.PropostaLCGarantia", "DataCriacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.PropostaLCGarantia", "UsuarioIDAlteracao", c => c.Guid());
            AddColumn("dbo.PropostaLCGarantia", "UsuarioIDCriacao", c => c.Guid(nullable: false));
            AddColumn("dbo.PropostaLCGarantia", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLCPecuaria", "DataAlteracao", c => c.DateTime());
            AddColumn("dbo.PropostaLCPecuaria", "DataCriacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.PropostaLCPecuaria", "UsuarioIDAlteracao", c => c.Guid());
            AddColumn("dbo.PropostaLCPecuaria", "UsuarioIDCriacao", c => c.Guid(nullable: false));
            AddColumn("dbo.PropostaLCPecuaria", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLCPecuaria", "Ano", c => c.Int(nullable: false));
            AddColumn("dbo.PropostaLCCultura", "DataAlteracao", c => c.DateTime());
            AddColumn("dbo.PropostaLCCultura", "DataCriacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.PropostaLCCultura", "UsuarioIDAlteracao", c => c.Guid());
            AddColumn("dbo.PropostaLCCultura", "UsuarioIDCriacao", c => c.Guid(nullable: false));
            AddColumn("dbo.PropostaLCCultura", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLCCultura", "CustoHaRegiaoID", c => c.Guid());
            AddColumn("dbo.PropostaLCCultura", "ProdutividadeMediaID", c => c.Guid());
            AddColumn("dbo.PropostaLCCultura", "EstadoID", c => c.Guid());
            AddColumn("dbo.PropostaLC", "Prazo", c => c.Int());
            AddColumn("dbo.PropostaLC", "Documento", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaLC", "IsAreaIrrigada", c => c.Boolean());
            AddColumn("dbo.PropostaLC", "Garantia", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLC", "Pecuaria", c => c.Boolean());
            AddColumn("dbo.PropostaLC", "SegmentoID", c => c.Guid());
            AddColumn("dbo.PropostaLC", "TipoProposta", c => c.String(maxLength: 120, unicode: false));
            DropForeignKey("dbo.PropostaLCParceriaAgricola", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCOutraReceita", "ReceitaID", "dbo.Receita");
            DropForeignKey("dbo.PropostaLCOutraReceita", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCNecessidadeProduto", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCNecessidadeProduto", "ProdutoServicoID", "dbo.ProdutoServico");
            DropForeignKey("dbo.PropostaLCMaquinaEquipamento", "PropostaLCGarantia_ID", "dbo.PropostaLCGarantia");
            DropForeignKey("dbo.PropostaLCMaquinaEquipamento", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCMercado", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCMercado", "CulturaID", "dbo.Cultura");
            DropForeignKey("dbo.PropostaLCBemUrbano", "PropostaLCGarantia_ID", "dbo.PropostaLCGarantia");
            DropForeignKey("dbo.PropostaLCBemUrbano", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCBemRural", "PropostaLCGarantia_ID", "dbo.PropostaLCGarantia");
            DropForeignKey("dbo.PropostaLCBemRural", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCPrecoPorRegiao", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCPrecoPorRegiao", "CidadeID", "dbo.Cidade");
            DropForeignKey("dbo.PropostaLCBemRural", "CidadeID", "dbo.Cidade");
            DropIndex("dbo.PropostaLCReferencia", new[] { "TipoEmpresaID" });
            DropIndex("dbo.PropostaLCReferencia", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCParceriaAgricola", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCOutraReceita", new[] { "ReceitaID" });
            DropIndex("dbo.PropostaLCOutraReceita", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCMaquinaEquipamento", new[] { "PropostaLCGarantia_ID" });
            DropIndex("dbo.PropostaLCMaquinaEquipamento", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCMercado", new[] { "CulturaID" });
            DropIndex("dbo.PropostaLCMercado", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCPecuaria", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCBemUrbano", new[] { "PropostaLCGarantia_ID" });
            DropIndex("dbo.PropostaLCBemUrbano", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCGarantia", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCPrecoPorRegiao", new[] { "CidadeID" });
            DropIndex("dbo.PropostaLCPrecoPorRegiao", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCBemRural", new[] { "PropostaLCGarantia_ID" });
            DropIndex("dbo.PropostaLCBemRural", new[] { "CidadeID" });
            DropIndex("dbo.PropostaLCBemRural", new[] { "PropostaLCID" });
            AlterColumn("dbo.PropostaLCReferencia", "Telefone", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLCReferencia", "Municipio", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLCReferencia", "TipoEmpresaID", c => c.Guid(nullable: false));
            AlterColumn("dbo.PropostaLCReferencia", "PropostaLCID", c => c.Guid());
            AlterColumn("dbo.PropostaLCGarantia", "PropostaLCID", c => c.Guid());
            AlterColumn("dbo.PropostaLCPecuaria", "Quantidade", c => c.Single(nullable: false));
            AlterColumn("dbo.PropostaLCPecuaria", "PropostaLCID", c => c.Guid());
            AlterColumn("dbo.PropostaLCCultura", "Arrendamento", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLCCultura", "PropostaLCID", c => c.Guid());
            AlterColumn("dbo.PropostaLC", "CustoCarregamentoTransporte", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PropostaLC", "TotalMWAno", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "CapacidadeMoagem", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "CustoMedioProducao", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "VolumeMoagemPropria", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "TotalProducaoAcucar", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "TotalProducaoAlcool", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "ResultadoUltimaSafra", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "NumeroComprasAno", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "NecessidadeAnualFertilizantes", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "FonteRecursosCarteira", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "Trading", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "QtdeSacas", c => c.Int());
            AlterColumn("dbo.PropostaLC", "PrincipaisClientes", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "PrincipaisFornecedores", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "ToneladasArmazem", c => c.Single());
            AlterColumn("dbo.PropostaLC", "SharePretentido", c => c.Int());
            AlterColumn("dbo.PropostaLC", "CPFConjugue", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "NomeConjugue", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "EstadoCivil", c => c.String(maxLength: 120, unicode: false));
            DropColumn("dbo.PropostaLCReferencia", "Comentarios");
            DropColumn("dbo.PropostaLCReferencia", "Garantias");
            DropColumn("dbo.PropostaLCReferencia", "Vigencia");
            DropColumn("dbo.PropostaLCReferencia", "LCAtual");
            DropColumn("dbo.PropostaLCReferencia", "Desde");
            DropColumn("dbo.PropostaLCReferencia", "NomeContato");
            DropColumn("dbo.PropostaLCReferencia", "NomeBanco");
            DropColumn("dbo.PropostaLCReferencia", "NomeEmpresa");
            DropColumn("dbo.PropostaLCReferencia", "TipoReferencia");
            DropColumn("dbo.PropostaLCGarantia", "Comentario");
            DropColumn("dbo.PropostaLCGarantia", "Nome");
            DropColumn("dbo.PropostaLCGarantia", "Documento");
            DropColumn("dbo.PropostaLCGarantia", "GarantiaRecebida");
            DropColumn("dbo.PropostaLCPecuaria", "Despesa");
            DropColumn("dbo.PropostaLCPecuaria", "Preco");
            DropColumn("dbo.PropostaLCPecuaria", "AnoPecuaria");
            DropColumn("dbo.PropostaLCCultura", "CustoArrendamentoSacaHa");
            DropColumn("dbo.PropostaLCCultura", "Quebra");
            DropColumn("dbo.PropostaLCCultura", "CustoHa");
            DropColumn("dbo.PropostaLCCultura", "ProdutividadeMedia");
            DropColumn("dbo.PropostaLCCultura", "Area");
            DropColumn("dbo.PropostaLC", "ComentarioPatrimonio");
            DropColumn("dbo.PropostaLC", "RestricaoTrabalhoEscravo");
            DropColumn("dbo.PropostaLC", "RestricaoIBAMA");
            DropColumn("dbo.PropostaLC", "RestricaoTJ");
            DropColumn("dbo.PropostaLC", "RestricaoSERASA");
            DropColumn("dbo.PropostaLC", "PrecoMedioLt");
            DropColumn("dbo.PropostaLC", "NecessidadeAnualFoliar");
            DropColumn("dbo.PropostaLC", "PrecoMedioFt");
            DropColumn("dbo.PropostaLC", "ComentarioMercado");
            DropColumn("dbo.PropostaLC", "PossuiOutrasReceitas");
            DropColumn("dbo.PropostaLC", "PossuiCriacaoDeAnimais");
            DropColumn("dbo.PropostaLC", "ClienteGarantia");
            DropColumn("dbo.PropostaLC", "PossuiAreaIrrigada");
            DropColumn("dbo.PropostaLC", "RegimeCasamento");
            DropTable("dbo.PropostaLCParceriaAgricola");
            DropTable("dbo.PropostaLCOutraReceita");
            DropTable("dbo.PropostaLCNecessidadeProduto");
            DropTable("dbo.PropostaLCMaquinaEquipamento");
            DropTable("dbo.PropostaLCMercado");
            DropTable("dbo.PropostaLCBemUrbano");
            DropTable("dbo.PropostaLCPrecoPorRegiao");
            DropTable("dbo.PropostaLCBemRural");
            CreateIndex("dbo.PropostaLCPrincipaisMercadosAtuacao", "PropostaLCID");
            CreateIndex("dbo.PropostaLCPrincipaisMercadosAtuacao", "CulturaID");
            CreateIndex("dbo.PropostaLCParceriaAgricula", "PropostaLCID");
            CreateIndex("dbo.PropostaLCParceriaAgricula", "ContaClienteID");
            CreateIndex("dbo.PropostaLCReferencia", "TipoEmpresaID");
            CreateIndex("dbo.PropostaLCReferencia", "PropostaLCID");
            CreateIndex("dbo.PropostaLCOutrasReceitas", "ReceitaID");
            CreateIndex("dbo.PropostaLCOutrasReceitas", "PropostaLCID");
            CreateIndex("dbo.PropostaLCPecuaria", "PropostaLCID");
            CreateIndex("dbo.PropostaLCGarantia", "PropostaLCID");
            CreateIndex("dbo.PropostaLCCultura", "CustoHaRegiaoID");
            CreateIndex("dbo.PropostaLCCultura", "ProdutividadeMediaID");
            CreateIndex("dbo.PropostaLCCultura", "EstadoID");
            CreateIndex("dbo.PropostaLCCultura", "PropostaLCID");
            CreateIndex("dbo.PropostaLC", "SegmentoID");
            AddForeignKey("dbo.PropostaLC", "SegmentoID", "dbo.Segmento", "ID");
            AddForeignKey("dbo.PropostaLCOutrasReceitas", "ReceitaID", "dbo.Receita", "ID");
            AddForeignKey("dbo.PropostaLCOutrasReceitas", "PropostaLCID", "dbo.PropostaLC", "ID");
            AddForeignKey("dbo.PropostaLCPrincipaisMercadosAtuacao", "PropostaLCID", "dbo.Cultura", "ID");
            AddForeignKey("dbo.PropostaLCPrincipaisMercadosAtuacao", "CulturaID", "dbo.PropostaLC", "ID");
            AddForeignKey("dbo.PropostaLCParceriaAgricula", "PropostaLCID", "dbo.ContaCliente", "ID");
            AddForeignKey("dbo.PropostaLCParceriaAgricula", "ContaClienteID", "dbo.PropostaLC", "ID");
            AddForeignKey("dbo.PropostaLCNecessidadeProduto", "PropostaLCID", "dbo.PropostaLC", "ID");
            AddForeignKey("dbo.PropostaLCNecessidadeProduto", "ProdutoServicoID", "dbo.ProdutoServico", "ID");
            AddForeignKey("dbo.PropostaLCCultura", "ProdutividadeMediaID", "dbo.ProdutividadeMedia", "ID");
            AddForeignKey("dbo.PropostaLCCultura", "EstadoID", "dbo.Estado", "ID");
            AddForeignKey("dbo.PropostaLCCultura", "CustoHaRegiaoID", "dbo.CustoHaRegiao", "ID");
        }
    }
}
