namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturadeVisitasdeCliente : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContaClienteVisita",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        DataSolicitacao = c.DateTime(nullable: false),
                        DataParecer = c.DateTime(),
                        Parecer = c.String(nullable: false, unicode: false, storeType: "text"),
                        EmpresasID = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.Empresa", t => t.EmpresasID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.EmpresasID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContaClienteVisita", "EmpresasID", "dbo.Empresa");
            DropForeignKey("dbo.ContaClienteVisita", "ContaClienteID", "dbo.ContaCliente");
            DropIndex("dbo.ContaClienteVisita", new[] { "EmpresasID" });
            DropIndex("dbo.ContaClienteVisita", new[] { "ContaClienteID" });
            DropTable("dbo.ContaClienteVisita");
        }
    }
}
