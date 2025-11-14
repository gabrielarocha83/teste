namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campo_SegmentoId_FluxoLiberacaoLCAdicional : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FluxoLiberacaoLCAdicional", "SegmentoID", c => c.Guid(nullable: false));
            CreateIndex("dbo.FluxoLiberacaoLCAdicional", "SegmentoID");
            AddForeignKey("dbo.FluxoLiberacaoLCAdicional", "SegmentoID", "dbo.Segmento", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FluxoLiberacaoLCAdicional", "SegmentoID", "dbo.Segmento");
            DropIndex("dbo.FluxoLiberacaoLCAdicional", new[] { "SegmentoID" });
            DropColumn("dbo.FluxoLiberacaoLCAdicional", "SegmentoID");
        }
    }
}
