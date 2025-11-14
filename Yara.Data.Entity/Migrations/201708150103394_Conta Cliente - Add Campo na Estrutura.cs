namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContaClienteAddCamponaEstrutura : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaCliente", "DescricaoBloqueio", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaCliente", "DescricaoBloqueio");
        }
    }
}
