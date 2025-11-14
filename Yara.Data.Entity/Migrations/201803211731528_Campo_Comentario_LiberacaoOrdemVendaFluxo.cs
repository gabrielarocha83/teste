namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campo_Comentario_LiberacaoOrdemVendaFluxo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LiberacaoOrdemVendaFluxo", "Comentario", c => c.String(maxLength: 500, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LiberacaoOrdemVendaFluxo", "Comentario", c => c.String(maxLength: 120, unicode: false));
        }
    }
}
