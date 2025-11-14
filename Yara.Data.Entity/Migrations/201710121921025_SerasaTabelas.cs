namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SerasaTabelas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PendenciaSerasa",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SolicitanteSerasaID = c.Guid(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Modalidade = c.String(maxLength: 120, unicode: false),
                        Quantidade = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SolicitanteSerasa", t => t.SolicitanteSerasaID)
                .Index(t => t.SolicitanteSerasaID);
            
            CreateTable(
                "dbo.SolicitanteSerasa",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        TipoClienteID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        TipoSerasa = c.Int(nullable: false),
                        Json = c.String(unicode: false, storeType: "text"),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.TipoCliente", t => t.TipoClienteID)
                .Index(t => t.TipoClienteID)
                .Index(t => t.ContaClienteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PendenciaSerasa", "SolicitanteSerasaID", "dbo.SolicitanteSerasa");
            DropForeignKey("dbo.SolicitanteSerasa", "TipoClienteID", "dbo.TipoCliente");
            DropForeignKey("dbo.SolicitanteSerasa", "ContaClienteID", "dbo.ContaCliente");
            DropIndex("dbo.SolicitanteSerasa", new[] { "ContaClienteID" });
            DropIndex("dbo.SolicitanteSerasa", new[] { "TipoClienteID" });
            DropIndex("dbo.PendenciaSerasa", new[] { "SolicitanteSerasaID" });
            DropTable("dbo.SolicitanteSerasa");
            DropTable("dbo.PendenciaSerasa");
        }
    }
}
