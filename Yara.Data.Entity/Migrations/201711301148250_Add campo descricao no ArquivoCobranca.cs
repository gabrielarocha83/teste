namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcampodescricaonoArquivoCobranca : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnexoArquivoCobranca", "Descricao", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnexoArquivoCobranca", "Descricao");
        }
    }
}
