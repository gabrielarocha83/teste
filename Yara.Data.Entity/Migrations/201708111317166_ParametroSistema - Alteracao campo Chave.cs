namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParametroSistemaAlteracaocampoChave : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ParametroSistema", "Chave", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ParametroSistema", "Chave", c => c.String(nullable: false, maxLength: 120, unicode: false));
        }
    }
}
