namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_ContaCliente_EstruturaComercial_Ativo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContaCliente_EstruturaComercial", "Ativo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContaCliente_EstruturaComercial", "Ativo", c => c.Boolean());
        }
    }
}
