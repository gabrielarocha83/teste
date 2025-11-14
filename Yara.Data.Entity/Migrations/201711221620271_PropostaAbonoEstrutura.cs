namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaAbonoEstrutura : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.FluxoLiberacaoAbono", name: "PerfilID", newName: "PrimeiroPerfilID");
            RenameColumn(table: "dbo.FluxoLiberacaoProrrogacao", name: "PerfilID", newName: "PrimeiroPerfilID");
            RenameIndex(table: "dbo.FluxoLiberacaoAbono", name: "IX_PerfilID", newName: "IX_PrimeiroPerfilID");
            RenameIndex(table: "dbo.FluxoLiberacaoProrrogacao", name: "IX_PerfilID", newName: "IX_PrimeiroPerfilID");
            CreateTable(
                "dbo.PropostaAbono",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        MotivoAbonoID = c.Guid(),
                        NumeroInternoProposta = c.Int(nullable: false),
                        ConceitoH = c.Boolean(nullable: false),
                        ParecerComercial = c.String(unicode: false, storeType: "text"),
                        ParecerCobranca = c.String(unicode: false, storeType: "text"),
                        PropostaAbonoStatusID = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        Motivo = c.String(unicode: false, storeType: "text"),
                        ResponsavelID = c.Guid(nullable: false),
                        EmpresaID = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        CodigoSap = c.String(maxLength: 10, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .ForeignKey("dbo.MotivoAbono", t => t.MotivoAbonoID)
                .ForeignKey("dbo.PropostaAbonoStatus", t => t.PropostaAbonoStatusID)
                .ForeignKey("dbo.Usuario", t => t.ResponsavelID)
                .Index(t => t.MotivoAbonoID)
                .Index(t => t.PropostaAbonoStatusID)
                .Index(t => t.ResponsavelID)
                .Index(t => t.EmpresaID);
            
            CreateTable(
                "dbo.PropostaAbonoStatus",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                        Status = c.String(maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaAbonoComite",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaAbonoID = c.Guid(nullable: false),
                        PropostaAbonoComiteSolicitanteID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        Nivel = c.Int(nullable: false),
                        Round = c.Int(nullable: false),
                        PerfilID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        DataAcao = c.DateTime(),
                        ValorDe = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorAte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConceitoH = c.Boolean(nullable: false),
                        Aprovado = c.Boolean(nullable: false),
                        Comentario = c.String(unicode: false, storeType: "text"),
                        Ativo = c.Boolean(nullable: false),
                        FluxoLiberacaoAbonoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FluxoLiberacaoAbono", t => t.FluxoLiberacaoAbonoID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID)
                .ForeignKey("dbo.PropostaAbono", t => t.PropostaAbonoID)
                .ForeignKey("dbo.PropostaAbonoComiteSolicitante", t => t.PropostaAbonoComiteSolicitanteID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.PropostaAbonoID)
                .Index(t => t.PropostaAbonoComiteSolicitanteID)
                .Index(t => t.PerfilID)
                .Index(t => t.UsuarioID)
                .Index(t => t.FluxoLiberacaoAbonoID);
            
            CreateTable(
                "dbo.PropostaAbonoComiteSolicitante",
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
                "dbo.PropostaAbonoTitulo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaAbonoID = c.Guid(nullable: false),
                        NumeroDocumento = c.String(maxLength: 120, unicode: false),
                        Linha = c.String(maxLength: 120, unicode: false),
                        AnoExercicio = c.String(maxLength: 120, unicode: false),
                        Empresa = c.String(maxLength: 120, unicode: false),
                        TipoDocumento = c.String(maxLength: 120, unicode: false),
                        DataEmissaoDocumento = c.DateTime(nullable: false),
                        CodigoRazao = c.String(maxLength: 120, unicode: false),
                        CodigoCliente = c.String(maxLength: 120, unicode: false),
                        OrdemVendaNumero = c.String(maxLength: 120, unicode: false),
                        OrdemVendaItem = c.Int(nullable: false),
                        NotaFiscal = c.String(maxLength: 120, unicode: false),
                        ValorInterno = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorDocumento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataVencimento = c.DateTime(nullable: false),
                        DataOriginal = c.DateTime(),
                        TextoDocumento = c.String(maxLength: 120, unicode: false),
                        InstrumentoPagamento = c.String(maxLength: 120, unicode: false),
                        NumeroDocumentoCompensacao = c.String(maxLength: 120, unicode: false),
                        MoedaInterna = c.String(maxLength: 120, unicode: false),
                        MoedaDocumento = c.String(maxLength: 120, unicode: false),
                        CreditoDebito = c.String(maxLength: 120, unicode: false),
                        CobrancaAutomatica = c.String(maxLength: 120, unicode: false),
                        Aberto = c.Boolean(nullable: false),
                        TaxaJuros = c.Decimal(precision: 18, scale: 2),
                        DataDuplicata = c.DateTime(),
                        DataTriplicata = c.DateTime(),
                        DataPefinInclusao = c.DateTime(),
                        DataPefinExclusao = c.DateTime(),
                        DataProtesto = c.DateTime(),
                        DataAceite = c.DateTime(),
                        DataPrevisaoPagamento = c.DateTime(),
                        DataPR = c.DateTime(),
                        DataREPR = c.DateTime(),
                        DataProtestoRealizado = c.DateTime(),
                        StatusCobrancaID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaAbono", t => t.PropostaAbonoID)
                .Index(t => t.PropostaAbonoID);
            
            AddColumn("dbo.FluxoLiberacaoAbono", "SegundoPerfilID", c => c.Guid());
            AddColumn("dbo.FluxoLiberacaoProrrogacao", "SegundoPerfilID", c => c.Guid());
            CreateIndex("dbo.FluxoLiberacaoAbono", "SegundoPerfilID");
            CreateIndex("dbo.FluxoLiberacaoProrrogacao", "SegundoPerfilID");
            AddForeignKey("dbo.FluxoLiberacaoAbono", "SegundoPerfilID", "dbo.Perfil", "ID");
            AddForeignKey("dbo.FluxoLiberacaoProrrogacao", "SegundoPerfilID", "dbo.Perfil", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaAbonoTitulo", "PropostaAbonoID", "dbo.PropostaAbono");
            DropForeignKey("dbo.PropostaAbonoComite", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaAbonoComite", "PropostaAbonoComiteSolicitanteID", "dbo.PropostaAbonoComiteSolicitante");
            DropForeignKey("dbo.PropostaAbonoComiteSolicitante", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaAbonoComite", "PropostaAbonoID", "dbo.PropostaAbono");
            DropForeignKey("dbo.PropostaAbonoComite", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.PropostaAbonoComite", "FluxoLiberacaoAbonoID", "dbo.FluxoLiberacaoAbono");
            DropForeignKey("dbo.PropostaAbono", "ResponsavelID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaAbono", "PropostaAbonoStatusID", "dbo.PropostaAbonoStatus");
            DropForeignKey("dbo.PropostaAbono", "MotivoAbonoID", "dbo.MotivoAbono");
            DropForeignKey("dbo.PropostaAbono", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.FluxoLiberacaoProrrogacao", "SegundoPerfilID", "dbo.Perfil");
            DropForeignKey("dbo.FluxoLiberacaoAbono", "SegundoPerfilID", "dbo.Perfil");
            DropIndex("dbo.PropostaAbonoTitulo", new[] { "PropostaAbonoID" });
            DropIndex("dbo.PropostaAbonoComiteSolicitante", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaAbonoComite", new[] { "FluxoLiberacaoAbonoID" });
            DropIndex("dbo.PropostaAbonoComite", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaAbonoComite", new[] { "PerfilID" });
            DropIndex("dbo.PropostaAbonoComite", new[] { "PropostaAbonoComiteSolicitanteID" });
            DropIndex("dbo.PropostaAbonoComite", new[] { "PropostaAbonoID" });
            DropIndex("dbo.PropostaAbono", new[] { "EmpresaID" });
            DropIndex("dbo.PropostaAbono", new[] { "ResponsavelID" });
            DropIndex("dbo.PropostaAbono", new[] { "PropostaAbonoStatusID" });
            DropIndex("dbo.PropostaAbono", new[] { "MotivoAbonoID" });
            DropIndex("dbo.FluxoLiberacaoProrrogacao", new[] { "SegundoPerfilID" });
            DropIndex("dbo.FluxoLiberacaoAbono", new[] { "SegundoPerfilID" });
            DropColumn("dbo.FluxoLiberacaoProrrogacao", "SegundoPerfilID");
            DropColumn("dbo.FluxoLiberacaoAbono", "SegundoPerfilID");
            DropTable("dbo.PropostaAbonoTitulo");
            DropTable("dbo.PropostaAbonoComiteSolicitante");
            DropTable("dbo.PropostaAbonoComite");
            DropTable("dbo.PropostaAbonoStatus");
            DropTable("dbo.PropostaAbono");
            RenameIndex(table: "dbo.FluxoLiberacaoProrrogacao", name: "IX_PrimeiroPerfilID", newName: "IX_PerfilID");
            RenameIndex(table: "dbo.FluxoLiberacaoAbono", name: "IX_PrimeiroPerfilID", newName: "IX_PerfilID");
            RenameColumn(table: "dbo.FluxoLiberacaoProrrogacao", name: "PrimeiroPerfilID", newName: "PerfilID");
            RenameColumn(table: "dbo.FluxoLiberacaoAbono", name: "PrimeiroPerfilID", newName: "PerfilID");
        }
    }
}
