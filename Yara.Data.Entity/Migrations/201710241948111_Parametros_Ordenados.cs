namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Parametros_Ordenados : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParametroSistema", "Ordem", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParametroSistema", "Ordem");
        }
    }
}
