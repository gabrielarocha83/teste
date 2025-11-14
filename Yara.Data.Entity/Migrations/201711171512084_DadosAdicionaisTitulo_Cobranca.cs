namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DadosAdicionaisTitulo_Cobranca : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Titulo", "TaxaJuros", c => c.Decimal(precision: 13, scale: 5));
            AddColumn("dbo.Titulo", "DataDuplicata", c => c.DateTime());
            AddColumn("dbo.Titulo", "DataTriplicata", c => c.DateTime());
            AddColumn("dbo.Titulo", "DataPefinInclusao", c => c.DateTime());
            AddColumn("dbo.Titulo", "DataPefinExclusao", c => c.DateTime());
            AddColumn("dbo.Titulo", "DataProtesto", c => c.DateTime());
            AddColumn("dbo.Titulo", "DataAceite", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Titulo", "DataAceite");
            DropColumn("dbo.Titulo", "DataProtesto");
            DropColumn("dbo.Titulo", "DataPefinExclusao");
            DropColumn("dbo.Titulo", "DataPefinInclusao");
            DropColumn("dbo.Titulo", "DataTriplicata");
            DropColumn("dbo.Titulo", "DataDuplicata");
            DropColumn("dbo.Titulo", "TaxaJuros");
        }
    }
}
