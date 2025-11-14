namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Novocamponatabelacontaclientegarantia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaClienteGarantia", "DescricaoOutros", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaClienteGarantia", "DescricaoOutros");
        }
    }
}
