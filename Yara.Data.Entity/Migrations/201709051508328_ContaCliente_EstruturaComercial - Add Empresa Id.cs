namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContaCliente_EstruturaComercialAddEmpresaId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContaCliente_EstruturaComercial", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.ContaCliente_EstruturaComercial", "EstruturaComercialID", "dbo.EstruturaComercial");
            DropIndex("dbo.ContaCliente_EstruturaComercial", new[] { "ContaClienteID" });
            DropIndex("dbo.ContaCliente_EstruturaComercial", new[] { "EstruturaComercialID" });

            DropTable("dbo.ContaCliente_EstruturaComercial");

            CreateTable(
                "dbo.ContaCliente_EstruturaComercial",
                c => new
                    {
                        ContaClienteId = c.Guid(nullable: false),
                        EstruturaComercialId = c.String(nullable: false, maxLength: 10, unicode: false),
                        EmpresasId = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteId, t.EstruturaComercialId, t.EmpresasId })
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteId)
                .ForeignKey("dbo.Empresa", t => t.EmpresasId)
                .ForeignKey("dbo.EstruturaComercial", t => t.EstruturaComercialId)
                .Index(t => t.ContaClienteId)
                .Index(t => t.EstruturaComercialId)
                .Index(t => t.EmpresasId);
            
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ContaCliente_EstruturaComercial",
                c => new
                    {
                        ContaClienteID = c.Guid(nullable: false),
                        EstruturaComercialID = c.String(nullable: false, maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteID, t.EstruturaComercialID });
            
            DropForeignKey("dbo.ContaCliente_EstruturaComercial", "EstruturaComercialId", "dbo.EstruturaComercial");
            DropForeignKey("dbo.ContaCliente_EstruturaComercial", "EmpresasId", "dbo.Empresa");
            DropForeignKey("dbo.ContaCliente_EstruturaComercial", "ContaClienteId", "dbo.ContaCliente");
            DropIndex("dbo.ContaCliente_EstruturaComercial", new[] { "EmpresasId" });
            DropIndex("dbo.ContaCliente_EstruturaComercial", new[] { "EstruturaComercialId" });
            DropIndex("dbo.ContaCliente_EstruturaComercial", new[] { "ContaClienteId" });
            DropTable("dbo.ContaCliente_EstruturaComercial");
            CreateIndex("dbo.ContaCliente_EstruturaComercial", "EstruturaComercialID");
            CreateIndex("dbo.ContaCliente_EstruturaComercial", "ContaClienteID");
            AddForeignKey("dbo.ContaCliente_EstruturaComercial", "EstruturaComercialID", "dbo.EstruturaComercial", "CodigoSap");
            AddForeignKey("dbo.ContaCliente_EstruturaComercial", "ContaClienteID", "dbo.ContaCliente", "ID");
        }
    }
}
