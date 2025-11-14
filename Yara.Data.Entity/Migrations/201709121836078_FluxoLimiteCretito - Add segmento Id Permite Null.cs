namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluxoLimiteCretitoAddsegmentoIdPermiteNull : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FluxoLimiteCredito", new[] { "SegmentoID" });
            AlterColumn("dbo.FluxoLimiteCredito", "SegmentoID", c => c.Guid());
            CreateIndex("dbo.FluxoLimiteCredito", "SegmentoID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FluxoLimiteCredito", new[] { "SegmentoID" });
            AlterColumn("dbo.FluxoLimiteCredito", "SegmentoID", c => c.Guid(nullable: false));
            CreateIndex("dbo.FluxoLimiteCredito", "SegmentoID");
        }
    }
}
