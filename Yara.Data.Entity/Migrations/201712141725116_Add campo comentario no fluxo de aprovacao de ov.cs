namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcampocomentarionofluxodeaprovacaodeov : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LiberacaoOrdemVendaFluxo", "Comentario", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LiberacaoOrdemVendaFluxo", "Comentario");
        }
    }
}
