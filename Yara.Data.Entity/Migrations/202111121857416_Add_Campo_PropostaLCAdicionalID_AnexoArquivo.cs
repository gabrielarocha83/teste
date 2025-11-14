namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campo_PropostaLCAdicionalID_AnexoArquivo : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PropostaLCAdicional", new[] { "PropostaLCStatusID" });
            AddColumn("dbo.AnexoArquivo", "PropostaLCAdicionalID", c => c.Guid());
            AlterColumn("dbo.PropostaLCAdicional", "PropostaLCStatusID", c => c.String(maxLength: 2, unicode: false));
            CreateIndex("dbo.AnexoArquivo", "PropostaLCAdicionalID");
            CreateIndex("dbo.PropostaLCAdicional", "PropostaLCStatusID");
            AddForeignKey("dbo.AnexoArquivo", "PropostaLCAdicionalID", "dbo.PropostaLCAdicional", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnexoArquivo", "PropostaLCAdicionalID", "dbo.PropostaLCAdicional");
            DropIndex("dbo.PropostaLCAdicional", new[] { "PropostaLCStatusID" });
            DropIndex("dbo.AnexoArquivo", new[] { "PropostaLCAdicionalID" });
            AlterColumn("dbo.PropostaLCAdicional", "PropostaLCStatusID", c => c.String(maxLength: 120, unicode: false));
            DropColumn("dbo.AnexoArquivo", "PropostaLCAdicionalID");
            CreateIndex("dbo.PropostaLCAdicional", "PropostaLCStatusID");
        }
    }
}
