namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnexoArquivoEstruturadetabela2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AnexoArquivo", "NomeArquivo", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.AnexoArquivo", "ExtensaoArquivo", c => c.String(nullable: false, maxLength: 6, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AnexoArquivo", "ExtensaoArquivo", c => c.String(nullable: false, maxLength: 120, unicode: false));
            AlterColumn("dbo.AnexoArquivo", "NomeArquivo", c => c.String(nullable: false, maxLength: 120, unicode: false));
        }
    }
}
