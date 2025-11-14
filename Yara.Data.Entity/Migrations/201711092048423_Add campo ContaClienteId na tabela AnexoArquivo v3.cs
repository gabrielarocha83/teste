namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcampoContaClienteIdnatabelaAnexoArquivov3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AnexoArquivo", new[] { "PropostaLCID" });
            AlterColumn("dbo.AnexoArquivo", "PropostaLCID", c => c.Guid());
            CreateIndex("dbo.AnexoArquivo", "PropostaLCID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AnexoArquivo", new[] { "PropostaLCID" });
            AlterColumn("dbo.AnexoArquivo", "PropostaLCID", c => c.Guid(nullable: false));
            CreateIndex("dbo.AnexoArquivo", "PropostaLCID");
        }
    }
}
