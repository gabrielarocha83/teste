namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterCulturasFormularioLC : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContaCliente", "PropostaLC_ID", "dbo.PropostaLC");
            DropForeignKey("dbo.Cultura", "PropostaLC_ID", "dbo.PropostaLC");
            DropIndex("dbo.ContaCliente", new[] { "PropostaLC_ID" });
            DropIndex("dbo.Cultura", new[] { "PropostaLC_ID" });
            AddColumn("dbo.ContaClienteFinanceiro", "DescricaoConceito", c => c.String(maxLength: 120, unicode: false));
            DropColumn("dbo.ContaCliente", "PropostaLC_ID");
            DropColumn("dbo.Cultura", "PropostaLC_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cultura", "PropostaLC_ID", c => c.Guid());
            AddColumn("dbo.ContaCliente", "PropostaLC_ID", c => c.Guid());
            DropColumn("dbo.ContaClienteFinanceiro", "DescricaoConceito");
            CreateIndex("dbo.Cultura", "PropostaLC_ID");
            CreateIndex("dbo.ContaCliente", "PropostaLC_ID");
            AddForeignKey("dbo.Cultura", "PropostaLC_ID", "dbo.PropostaLC", "ID");
            AddForeignKey("dbo.ContaCliente", "PropostaLC_ID", "dbo.PropostaLC", "ID");
        }
    }
}
