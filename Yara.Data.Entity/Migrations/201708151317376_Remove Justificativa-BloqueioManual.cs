namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveJustificativaBloqueioManual : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaCliente", "PropostaLC_ID", c => c.Guid());
            AddColumn("dbo.Cultura", "PropostaLC_ID", c => c.Guid());
            CreateIndex("dbo.ContaCliente", "PropostaLC_ID");
            CreateIndex("dbo.Cultura", "PropostaLC_ID");
            AddForeignKey("dbo.ContaCliente", "PropostaLC_ID", "dbo.PropostaLC", "ID");
            AddForeignKey("dbo.Cultura", "PropostaLC_ID", "dbo.PropostaLC", "ID");
            DropColumn("dbo.ContaCliente", "DescricaoBloqueio");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContaCliente", "DescricaoBloqueio", c => c.String(maxLength: 120, unicode: false));
            DropForeignKey("dbo.Cultura", "PropostaLC_ID", "dbo.PropostaLC");
            DropForeignKey("dbo.ContaCliente", "PropostaLC_ID", "dbo.PropostaLC");
            DropIndex("dbo.Cultura", new[] { "PropostaLC_ID" });
            DropIndex("dbo.ContaCliente", new[] { "PropostaLC_ID" });
            DropColumn("dbo.Cultura", "PropostaLC_ID");
            DropColumn("dbo.ContaCliente", "PropostaLC_ID");
        }
    }
}
