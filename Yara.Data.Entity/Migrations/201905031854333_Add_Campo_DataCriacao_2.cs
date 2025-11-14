namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campo_DataCriacao_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaCliente_Representante", "DataCriacao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaCliente_Representante", "DataCriacao");
        }
    }
}
