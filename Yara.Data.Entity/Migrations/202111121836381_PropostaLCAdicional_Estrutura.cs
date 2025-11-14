namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaLCAdicional_Estrutura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaLCAdicional",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        NumeroInternoProposta = c.Int(nullable: false),
                        EmpresaID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        LCAdicional = c.Decimal(precision: 18, scale: 2),
                        VigenciaAdicional = c.DateTime(),
                        Parecer = c.String(nullable: false, unicode: false, storeType: "text"),
                        AcompanharProposta = c.Boolean(nullable: false),
                        CodigoSap = c.String(nullable: false, maxLength: 10, unicode: false),
                        ContaClienteID = c.Guid(nullable: false),
                        TipoClienteID = c.Guid(),
                        ResponsavelID = c.Guid(),
                        PropostaLCStatusID = c.String(maxLength: 2, unicode: false),
                        GrupoEconomicoID = c.Guid(),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.GrupoEconomico", t => t.GrupoEconomicoID)
                .ForeignKey("dbo.PropostaLCStatus", t => t.PropostaLCStatusID)
                .ForeignKey("dbo.Usuario", t => t.ResponsavelID)
                .ForeignKey("dbo.TipoCliente", t => t.TipoClienteID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.TipoClienteID)
                .Index(t => t.ResponsavelID)
                .Index(t => t.PropostaLCStatusID)
                .Index(t => t.GrupoEconomicoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCAdicional", "TipoClienteID", "dbo.TipoCliente");
            DropForeignKey("dbo.PropostaLCAdicional", "ResponsavelID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaLCAdicional", "PropostaLCStatusID", "dbo.PropostaLCStatus");
            DropForeignKey("dbo.PropostaLCAdicional", "GrupoEconomicoID", "dbo.GrupoEconomico");
            DropForeignKey("dbo.PropostaLCAdicional", "ContaClienteID", "dbo.ContaCliente");
            DropIndex("dbo.PropostaLCAdicional", new[] { "GrupoEconomicoID" });
            DropIndex("dbo.PropostaLCAdicional", new[] { "PropostaLCStatusID" });
            DropIndex("dbo.PropostaLCAdicional", new[] { "ResponsavelID" });
            DropIndex("dbo.PropostaLCAdicional", new[] { "TipoClienteID" });
            DropIndex("dbo.PropostaLCAdicional", new[] { "ContaClienteID" });
            DropTable("dbo.PropostaLCAdicional");
        }
    }
}
