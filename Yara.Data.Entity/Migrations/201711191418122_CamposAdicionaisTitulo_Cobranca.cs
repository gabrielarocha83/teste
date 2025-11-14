namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposAdicionaisTitulo_Cobranca : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Titulo", "DataPrevisaoPagamento", c => c.DateTime());
            AddColumn("dbo.Titulo", "DataPR", c => c.DateTime());
            AddColumn("dbo.Titulo", "DataREPR", c => c.DateTime());
            AddColumn("dbo.Titulo", "DataProtestoRealizado", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Titulo", "DataProtestoRealizado");
            DropColumn("dbo.Titulo", "DataREPR");
            DropColumn("dbo.Titulo", "DataPR");
            DropColumn("dbo.Titulo", "DataPrevisaoPagamento");
        }
    }
}
