namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstadoCidadePropostaLC : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                        EstadoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Estado", t => t.EstadoID)
                .Index(t => t.EstadoID);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                        Sigla = c.String(maxLength: 120, unicode: false),
                        RegiaoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Regiao", t => t.RegiaoID)
                .Index(t => t.RegiaoID);
            
            CreateTable(
                "dbo.PropostaLC",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        ExperienciaID = c.Guid(nullable: false),
                        EstadoCivil = c.String(maxLength: 120, unicode: false),
                        NomeConjugue = c.String(maxLength: 120, unicode: false),
                        CPFConjugue = c.String(maxLength: 120, unicode: false),
                        Pecuaria = c.Boolean(),
                        Garantia = c.Boolean(nullable: false),
                        LCProposto = c.Decimal(precision: 18, scale: 2),
                        SharePretentido = c.Int(),
                        PossuiArmazem = c.Boolean(),
                        ToneladasArmazem = c.Single(),
                        PossuiGerenteTecnico = c.Boolean(),
                        PossuiMaquinarioProprio = c.Boolean(),
                        AreaIrrigadaID = c.Guid(),
                        PrincipaisFornecedores = c.String(maxLength: 120, unicode: false),
                        PrincipaisClientes = c.String(maxLength: 120, unicode: false),
                        PossuiContratoProximaSafra = c.Boolean(),
                        QtdeSacas = c.Int(),
                        PrecoSaca = c.Decimal(precision: 18, scale: 2),
                        Trading = c.String(maxLength: 120, unicode: false),
                        ParecerRepresentante = c.String(unicode: false, storeType: "text"),
                        FonteRecursosCarteira = c.String(maxLength: 120, unicode: false),
                        ParecerCTC = c.String(unicode: false, storeType: "text"),
                        Documento = c.String(maxLength: 120, unicode: false),
                        NecessidadeAnualFertilizantes = c.String(maxLength: 120, unicode: false),
                        NumeroComprasAno = c.String(maxLength: 120, unicode: false),
                        Prazo = c.Int(),
                        ResultadoUltimaSafra = c.String(maxLength: 120, unicode: false),
                        NumeroClienteCooperados = c.Int(),
                        PrazoMedioVendas = c.Int(),
                        IdadeMediaCanavialID = c.Guid(),
                        TotalProducaoAlcool = c.String(maxLength: 120, unicode: false),
                        TotalProducaoAcucar = c.String(maxLength: 120, unicode: false),
                        VolumeMoagemPropria = c.String(maxLength: 120, unicode: false),
                        CustoMedioProducao = c.String(maxLength: 120, unicode: false),
                        CapacidadeMoagem = c.String(maxLength: 120, unicode: false),
                        TotalMWAno = c.String(maxLength: 120, unicode: false),
                        CustoCarregamentoTransporte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AreaIrrigada", t => t.AreaIrrigadaID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.Experiencia", t => t.ExperienciaID)
                .ForeignKey("dbo.IdadeMediaCanavial", t => t.IdadeMediaCanavialID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.ExperienciaID)
                .Index(t => t.AreaIrrigadaID)
                .Index(t => t.IdadeMediaCanavialID);
            
            CreateTable(
                "dbo.PropostaLCCultura",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(),
                        CulturaID = c.Guid(),
                        EstadoID = c.Guid(),
                        CidadeID = c.Guid(),
                        MesPlantio = c.Int(),
                        MesColheita = c.Int(),
                        ProdutividadeMediaID = c.Guid(),
                        Preço = c.Decimal(precision: 18, scale: 2),
                        Arrendamento = c.String(maxLength: 120, unicode: false),
                        CustoHaRegiaoID = c.Guid(),
                        ConsumoFertilizante = c.Decimal(precision: 18, scale: 2),
                        PrecoMedioFertilizante = c.Decimal(precision: 18, scale: 2),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cidade", t => t.CidadeID)
                .ForeignKey("dbo.Cultura", t => t.CulturaID)
                .ForeignKey("dbo.CustoHaRegiao", t => t.CustoHaRegiaoID)
                .ForeignKey("dbo.Estado", t => t.EstadoID)
                .ForeignKey("dbo.ProdutividadeMedia", t => t.ProdutividadeMediaID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.CulturaID)
                .Index(t => t.EstadoID)
                .Index(t => t.CidadeID)
                .Index(t => t.ProdutividadeMediaID)
                .Index(t => t.CustoHaRegiaoID);
            
            CreateTable(
                "dbo.PropostaLCGarantia",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(),
                        TipoGarantiaID = c.Guid(nullable: false),
                        Descricao = c.String(maxLength: 120, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.TipoGarantia", t => t.TipoGarantiaID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.TipoGarantiaID);
            
            CreateTable(
                "dbo.PropostaLCNecessidadeProduto",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(),
                        ProdutoServicoID = c.Guid(nullable: false),
                        Quantidade = c.Single(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProdutoServico", t => t.ProdutoServicoID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.ProdutoServicoID);
            
            CreateTable(
                "dbo.PropostaLCOutrasReceitas",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(),
                        Ano = c.Int(nullable: false),
                        ReceitaID = c.Guid(nullable: false),
                        ReceitaPrevista = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.Receita", t => t.ReceitaID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.ReceitaID);
            
            CreateTable(
                "dbo.PropostaLCPecuaria",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(),
                        TipoPecuariaID = c.Guid(),
                        Ano = c.Int(nullable: false),
                        Quantidade = c.Single(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.TipoPecuaria", t => t.TipoPecuariaID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.TipoPecuariaID);
            
            CreateTable(
                "dbo.PropostaLCReferencia",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(),
                        Tipo = c.Int(nullable: false),
                        TipoEmpresaID = c.Guid(nullable: false),
                        Municipio = c.String(maxLength: 120, unicode: false),
                        Telefone = c.String(maxLength: 120, unicode: false),
                        Contato = c.String(maxLength: 120, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.TipoEmpresa", t => t.TipoEmpresaID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.TipoEmpresaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCReferencia", "TipoEmpresaID", "dbo.TipoEmpresa");
            DropForeignKey("dbo.PropostaLCReferencia", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCPecuaria", "TipoPecuariaID", "dbo.TipoPecuaria");
            DropForeignKey("dbo.PropostaLCPecuaria", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCOutrasReceitas", "ReceitaID", "dbo.Receita");
            DropForeignKey("dbo.PropostaLCOutrasReceitas", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCNecessidadeProduto", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCNecessidadeProduto", "ProdutoServicoID", "dbo.ProdutoServico");
            DropForeignKey("dbo.PropostaLCGarantia", "TipoGarantiaID", "dbo.TipoGarantia");
            DropForeignKey("dbo.PropostaLCGarantia", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCCultura", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCCultura", "ProdutividadeMediaID", "dbo.ProdutividadeMedia");
            DropForeignKey("dbo.PropostaLCCultura", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.PropostaLCCultura", "CustoHaRegiaoID", "dbo.CustoHaRegiao");
            DropForeignKey("dbo.PropostaLCCultura", "CulturaID", "dbo.Cultura");
            DropForeignKey("dbo.PropostaLCCultura", "CidadeID", "dbo.Cidade");
            DropForeignKey("dbo.PropostaLC", "IdadeMediaCanavialID", "dbo.IdadeMediaCanavial");
            DropForeignKey("dbo.PropostaLC", "ExperienciaID", "dbo.Experiencia");
            DropForeignKey("dbo.PropostaLC", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.PropostaLC", "AreaIrrigadaID", "dbo.AreaIrrigada");
            DropForeignKey("dbo.Cidade", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.Estado", "RegiaoID", "dbo.Regiao");
            DropIndex("dbo.PropostaLCReferencia", new[] { "TipoEmpresaID" });
            DropIndex("dbo.PropostaLCReferencia", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCPecuaria", new[] { "TipoPecuariaID" });
            DropIndex("dbo.PropostaLCPecuaria", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCOutrasReceitas", new[] { "ReceitaID" });
            DropIndex("dbo.PropostaLCOutrasReceitas", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCNecessidadeProduto", new[] { "ProdutoServicoID" });
            DropIndex("dbo.PropostaLCNecessidadeProduto", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCGarantia", new[] { "TipoGarantiaID" });
            DropIndex("dbo.PropostaLCGarantia", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "CustoHaRegiaoID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "ProdutividadeMediaID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "CidadeID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "EstadoID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "CulturaID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLC", new[] { "IdadeMediaCanavialID" });
            DropIndex("dbo.PropostaLC", new[] { "AreaIrrigadaID" });
            DropIndex("dbo.PropostaLC", new[] { "ExperienciaID" });
            DropIndex("dbo.PropostaLC", new[] { "ContaClienteID" });
            DropIndex("dbo.Estado", new[] { "RegiaoID" });
            DropIndex("dbo.Cidade", new[] { "EstadoID" });
            DropTable("dbo.PropostaLCReferencia");
            DropTable("dbo.PropostaLCPecuaria");
            DropTable("dbo.PropostaLCOutrasReceitas");
            DropTable("dbo.PropostaLCNecessidadeProduto");
            DropTable("dbo.PropostaLCGarantia");
            DropTable("dbo.PropostaLCCultura");
            DropTable("dbo.PropostaLC");
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
        }
    }
}
