namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SegmentoContaCliente : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContaCliente_Segmento", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.ContaCliente_Segmento", "SegmentoID", "dbo.Segmento");
            DropIndex("dbo.ContaCliente_Segmento", new[] { "ContaClienteID" });
            DropIndex("dbo.ContaCliente_Segmento", new[] { "SegmentoID" });
            AddColumn("dbo.ContaCliente", "SegmentoID", c => c.Guid());
            CreateIndex("dbo.ContaCliente", "SegmentoID");
            AddForeignKey("dbo.ContaCliente", "SegmentoID", "dbo.Segmento", "ID");
            DropTable("dbo.ContaCliente_Segmento");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ContaCliente_Segmento",
                c => new
                    {
                        ContaClienteID = c.Guid(nullable: false),
                        SegmentoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteID, t.SegmentoID });
            
            DropForeignKey("dbo.ContaCliente", "SegmentoID", "dbo.Segmento");
            DropIndex("dbo.ContaCliente", new[] { "SegmentoID" });
            DropColumn("dbo.ContaCliente", "SegmentoID");
            CreateIndex("dbo.ContaCliente_Segmento", "SegmentoID");
            CreateIndex("dbo.ContaCliente_Segmento", "ContaClienteID");
            AddForeignKey("dbo.ContaCliente_Segmento", "SegmentoID", "dbo.Segmento", "ID");
            AddForeignKey("dbo.ContaCliente_Segmento", "ContaClienteID", "dbo.ContaCliente", "ID");
        }
    }
}
