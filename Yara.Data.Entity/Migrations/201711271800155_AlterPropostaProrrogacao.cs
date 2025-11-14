namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterPropostaProrrogacao : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PropostaAbonoStatus", newName: "PropostaCobrancaStatus");
            RenameColumn(table: "dbo.PropostaAbono", name: "PropostaAbonoStatusID", newName: "PropostaCobrancaStatusID");
            RenameIndex(table: "dbo.PropostaAbono", name: "IX_PropostaAbonoStatusID", newName: "IX_PropostaCobrancaStatusID");
            CreateTable(
                "dbo.PropostaProrrogacao",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        NumeroInternoProposta = c.Int(nullable: false),
                        ContaClienteID = c.Guid(),
                        ResponsavelID = c.Guid(),
                        MotivoProrrogacaoID = c.Guid(),
                        OrigemRecursoID = c.Guid(),
                        TaxaSugerida = c.Decimal(precision: 18, scale: 2),
                        ValorProrrogado = c.Decimal(precision: 18, scale: 2),
                        Favoravel = c.Boolean(),
                        RestricaoSerasa = c.Boolean(),
                        Parcelamento = c.Boolean(),
                        AgregaGarantia = c.Boolean(),
                        ParecerComercial = c.String(unicode: false, storeType: "text"),
                        ParecerCobranca = c.String(unicode: false, storeType: "text"),
                        PropostaCobrancaStatusID = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        CodigoSap = c.String(maxLength: 10, unicode: false),
                        EmpresaID = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .ForeignKey("dbo.MotivoProrrogacao", t => t.MotivoProrrogacaoID)
                .ForeignKey("dbo.OrigemRecurso", t => t.OrigemRecursoID)
                .ForeignKey("dbo.PropostaCobrancaStatus", t => t.PropostaCobrancaStatusID)
                .ForeignKey("dbo.Usuario", t => t.ResponsavelID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.ResponsavelID)
                .Index(t => t.MotivoProrrogacaoID)
                .Index(t => t.OrigemRecursoID)
                .Index(t => t.PropostaCobrancaStatusID)
                .Index(t => t.EmpresaID);
            
            CreateTable(
                "dbo.PropostaProrrogacaoAcompanhamento",
                c => new
                    {
                        PropostaProrrogacaoID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PropostaProrrogacaoID, t.UsuarioID })
                .ForeignKey("dbo.PropostaProrrogacao", t => t.PropostaProrrogacaoID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.PropostaProrrogacaoID)
                .Index(t => t.UsuarioID);
            
            CreateTable(
                "dbo.PropostaProrrogacaoComite",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaProrrogacaoID = c.Guid(nullable: false),
                        PropostaProrrogacaoComiteSolicitanteID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        Nivel = c.Int(nullable: false),
                        Round = c.Int(nullable: false),
                        PerfilID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        DataAcao = c.DateTime(),
                        ValorDe = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorAte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Aprovado = c.Boolean(nullable: false),
                        NovasLiberacoes = c.Boolean(nullable: false),
                        TaxaJuros = c.Single(nullable: false),
                        Comentario = c.String(unicode: false, storeType: "text"),
                        Ativo = c.Boolean(nullable: false),
                        FluxoLiberacaoProrrogacaoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FluxoLiberacaoProrrogacao", t => t.FluxoLiberacaoProrrogacaoID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID)
                .ForeignKey("dbo.PropostaProrrogacao", t => t.PropostaProrrogacaoID)
                .ForeignKey("dbo.PropostaProrrogacaoComiteSolicitante", t => t.PropostaProrrogacaoComiteSolicitanteID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.PropostaProrrogacaoID)
                .Index(t => t.PropostaProrrogacaoComiteSolicitanteID)
                .Index(t => t.PerfilID)
                .Index(t => t.UsuarioID)
                .Index(t => t.FluxoLiberacaoProrrogacaoID);
            
            CreateTable(
                "dbo.PropostaProrrogacaoComiteSolicitante",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.UsuarioID);
            
            CreateTable(
                "dbo.PropostaProrrogacaoTitulo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaProrrogacaoID = c.Guid(nullable: false),
                        Pedido = c.String(maxLength: 120, unicode: false),
                        Emissao = c.DateTime(),
                        VencimentoOriginal = c.DateTime(),
                        VencimentoProrrogado = c.DateTime(),
                        PayT = c.String(maxLength: 120, unicode: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ComentarioHistorico = c.String(maxLength: 120, unicode: false),
                        PRRPR = c.String(maxLength: 120, unicode: false),
                        Aceite = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaProrrogacao", t => t.PropostaProrrogacaoID)
                .Index(t => t.PropostaProrrogacaoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaProrrogacaoTitulo", "PropostaProrrogacaoID", "dbo.PropostaProrrogacao");
            DropForeignKey("dbo.PropostaProrrogacaoComite", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaProrrogacaoComite", "PropostaProrrogacaoComiteSolicitanteID", "dbo.PropostaProrrogacaoComiteSolicitante");
            DropForeignKey("dbo.PropostaProrrogacaoComiteSolicitante", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaProrrogacaoComite", "PropostaProrrogacaoID", "dbo.PropostaProrrogacao");
            DropForeignKey("dbo.PropostaProrrogacaoComite", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.PropostaProrrogacaoComite", "FluxoLiberacaoProrrogacaoID", "dbo.FluxoLiberacaoProrrogacao");
            DropForeignKey("dbo.PropostaProrrogacaoAcompanhamento", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaProrrogacaoAcompanhamento", "PropostaProrrogacaoID", "dbo.PropostaProrrogacao");
            DropForeignKey("dbo.PropostaProrrogacao", "ResponsavelID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaProrrogacao", "PropostaCobrancaStatusID", "dbo.PropostaCobrancaStatus");
            DropForeignKey("dbo.PropostaProrrogacao", "OrigemRecursoID", "dbo.OrigemRecurso");
            DropForeignKey("dbo.PropostaProrrogacao", "MotivoProrrogacaoID", "dbo.MotivoProrrogacao");
            DropForeignKey("dbo.PropostaProrrogacao", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.PropostaProrrogacao", "ContaClienteID", "dbo.ContaCliente");
            DropIndex("dbo.PropostaProrrogacaoTitulo", new[] { "PropostaProrrogacaoID" });
            DropIndex("dbo.PropostaProrrogacaoComiteSolicitante", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaProrrogacaoComite", new[] { "FluxoLiberacaoProrrogacaoID" });
            DropIndex("dbo.PropostaProrrogacaoComite", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaProrrogacaoComite", new[] { "PerfilID" });
            DropIndex("dbo.PropostaProrrogacaoComite", new[] { "PropostaProrrogacaoComiteSolicitanteID" });
            DropIndex("dbo.PropostaProrrogacaoComite", new[] { "PropostaProrrogacaoID" });
            DropIndex("dbo.PropostaProrrogacaoAcompanhamento", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaProrrogacaoAcompanhamento", new[] { "PropostaProrrogacaoID" });
            DropIndex("dbo.PropostaProrrogacao", new[] { "EmpresaID" });
            DropIndex("dbo.PropostaProrrogacao", new[] { "PropostaCobrancaStatusID" });
            DropIndex("dbo.PropostaProrrogacao", new[] { "OrigemRecursoID" });
            DropIndex("dbo.PropostaProrrogacao", new[] { "MotivoProrrogacaoID" });
            DropIndex("dbo.PropostaProrrogacao", new[] { "ResponsavelID" });
            DropIndex("dbo.PropostaProrrogacao", new[] { "ContaClienteID" });
            DropTable("dbo.PropostaProrrogacaoTitulo");
            DropTable("dbo.PropostaProrrogacaoComiteSolicitante");
            DropTable("dbo.PropostaProrrogacaoComite");
            DropTable("dbo.PropostaProrrogacaoAcompanhamento");
            DropTable("dbo.PropostaProrrogacao");
            RenameIndex(table: "dbo.PropostaAbono", name: "IX_PropostaCobrancaStatusID", newName: "IX_PropostaAbonoStatusID");
            RenameColumn(table: "dbo.PropostaAbono", name: "PropostaCobrancaStatusID", newName: "PropostaAbonoStatusID");
            RenameTable(name: "dbo.PropostaCobrancaStatus", newName: "PropostaAbonoStatus");
        }
    }
}
