namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campo_DataCriacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaCliente_EstruturaComercial", "DataCriacao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaCliente_EstruturaComercial", "DataCriacao");
        }
    }
}
