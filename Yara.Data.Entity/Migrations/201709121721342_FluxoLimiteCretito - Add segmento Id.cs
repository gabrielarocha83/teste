namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluxoLimiteCretitoAddsegmentoId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FluxoLimiteCredito", "SegmentoID", c => c.Guid(nullable: false));
            CreateIndex("dbo.FluxoLimiteCredito", "SegmentoID");
            AddForeignKey("dbo.FluxoLimiteCredito", "SegmentoID", "dbo.Segmento", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FluxoLimiteCredito", "SegmentoID", "dbo.Segmento");
            DropIndex("dbo.FluxoLimiteCredito", new[] { "SegmentoID" });
            DropColumn("dbo.FluxoLimiteCredito", "SegmentoID");
        }
    }
}
