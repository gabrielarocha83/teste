namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnidadeMedidaCulturaSiglanãoobrigatória : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UnidadeMedidaCultura", "Sigla", c => c.String(maxLength: 10, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UnidadeMedidaCultura", "Sigla", c => c.String(nullable: false, maxLength: 10, unicode: false));
        }
    }
}
