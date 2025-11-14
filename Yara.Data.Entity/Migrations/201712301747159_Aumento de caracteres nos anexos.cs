namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Aumentodecaracteresnosanexos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AnexoArquivo", "NomeArquivo", c => c.String(nullable: false, maxLength: 512, unicode: false));
            AlterColumn("dbo.Anexo", "Descricao", c => c.String(nullable: false, maxLength: 512, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Anexo", "Descricao", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.AnexoArquivo", "NomeArquivo", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
    }
}
