namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdddemaiscamposnatabelaAnexoArquivo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnexoArquivo", "Status", c => c.Int());
            AddColumn("dbo.AnexoArquivo", "DataValidade", c => c.DateTime());
            AddColumn("dbo.AnexoArquivo", "Comentario", c => c.String(maxLength: 400, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnexoArquivo", "Comentario");
            DropColumn("dbo.AnexoArquivo", "DataValidade");
            DropColumn("dbo.AnexoArquivo", "Status");
        }
    }
}
