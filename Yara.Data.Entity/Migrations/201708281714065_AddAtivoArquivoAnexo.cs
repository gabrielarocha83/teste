namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAtivoArquivoAnexo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnexoArquivo", "Ativo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnexoArquivo", "Ativo");
        }
    }
}
